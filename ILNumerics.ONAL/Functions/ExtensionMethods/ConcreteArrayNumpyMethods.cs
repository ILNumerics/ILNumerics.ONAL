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
using ILNumerics.Core;
using ILNumerics.Core.Arrays;
using ILNumerics.Core.DeviceManagement;
using ILNumerics.Core.Functions.Builtin;
using ILNumerics.Core.Global;
using ILNumerics.Core.Internal;
using ILNumerics.Core.StorageLayer;
using System;
using System.ComponentModel;
using System.Security;

/*
/* This file implements all numpy ndarray _methods_. (ndarray attributes are implemented in MutableNumpyArrayAttributes and ImmutableNumpyArrayAttributes.)
/* 
/* We have two categories of methods here: 
/* 1) Methods showing up on all arrays. Implement them as extension method on ConcreteArray{....} <![CDATA[
/*       public static unsafe void item<T1, LocalT, InT, OutT, RetT, StorageT>(this ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> A) 
/*           where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
/*           where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
/*           where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
/*           where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
/*           where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {
/*           try {
/*
/*           } finally {
/*               (A as RetT)?.Release();
/*           }
/*       }]]>
/*   2) Methods showing up on mutable arrays only. Implement them as extension method on Mutable{...} <![CDATA[
/*       public static unsafe void squeeze<T1, LocalT, InT, OutT, RetT, StorageT>(this Mutable<T1, LocalT, InT, OutT, RetT, StorageT> A) 
/*           where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
/*           where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
/*           where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
/*           where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
/*           where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {
/*       // no RetT handling required here! 
/*       } ]]>
/**/

namespace ILNumerics {

    /// <summary>
    /// Importing this class / its namespace makes the array methods from the [numpy API] visible on suitable arrays. 
    /// </summary>
    public static partial class ExtensionMethods {

        #region ndarray.item

        /// <summary>
        /// [numpy API] Returns a single element from this array according to the provided dimension index.
        /// </summary>
        /// <typeparam name="T1">Element type.</typeparam>
        /// <param name="A">The ILNumerics n-dim array.</param>
        /// <param name="d0">Sequential index of the element to retrieve, as if the elements were stored in row-major order. -A.S.NumberOfElements &lt;=d0&lt;A.S.NumberOfElements.</param>
        /// <returns>Copy of the element as located by the index argument <paramref name="d0"/>.</returns>
        /// <remarks><para>An index for dimension i can be negative and must evaluate to an element position in the range 0...A.S[i]-1.</para>
        /// <para>This function does support the merging of trailing, unspecified dimensions as known from GetValue(BaseArray{bool}, long, long). However, the 
        /// element position is computed by assuming the elements to be laid-out sequentially in memory in row-major order. This function 
        /// handles arbitrary storage orders, though, using its strides to compute the actual element position in memory.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if d0 exceeds the number of elements in the array.</exception>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static T1 item<T1>(this BaseArray<T1> A, long d0) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<T1, Array<T1>, InArray<T1>, OutArray<T1>, Array<T1>, Storage<T1>>, out var storage);
            return storage.GetValueSeq(storage.Size.GetSeqIndex_NP(d0));

        }

        /// <summary>
        /// [numpy API] Returns a single element from this array according to the provided dimension indices.
        /// </summary>
        /// <typeparam name="T1">Element type.</typeparam>
        /// <param name="A">The ILNumerics n-dim array.</param>
        /// <param name="d0">Index into dimension #0.</param>
        /// <param name="d1">Index into dimension #1.</param>
        /// <returns>Copy of the element as located by the index arguments <paramref name="d0"/> and <paramref name="d1"/>.</returns>
        /// <remarks><para>An index for dimension i can be negative and must evaluate to an element position in the range 0...A.S[i]-1.</para>
        /// <para>This function does not support the merging of trailing, unspecified dimensions as known from GetValue(BaseArray{bool}, long, long)"/> or from 
        /// <see cref="ILNumerics.ExtensionMethods.item{T1}(BaseArray{T1}, long)"/>.</para>
        /// <para>Unspecified dimension indices are assumed to be 0. Optional, provided indices for virtual dimensions must be 0.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if an index exceeds the allowed range of the corresponding dimension.</exception>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static T1 item<T1>(this BaseArray<T1> A, long d0, long d1) {
                using var _1 = ReaderLock.Create(A as ConcreteArray<T1, Array<T1>, InArray<T1>, OutArray<T1>, Array<T1>, Storage<T1>>, out var storage);
                if (d1 < 0) d1 += storage.S[1];
                if ((ulong)d1 >= (ulong)storage.S[1]) throw new IndexOutOfRangeException("Index into dimension 1 exceeds the range of existing elements. Consider using GetValue() instead.");
                return storage.GetValueSeq(storage.Size.GetSeqIndex(d0,/*!HC:invocation*/ d1));
        }

        /// <summary>
        /// [numpy API] Returns a single element from this array according to the provided dimension indices.
        /// </summary>
        /// <typeparam name="T1">Element type.</typeparam>
        /// <param name="A">The ILNumerics n-dim array.</param>
        /// <param name="d0">Index into dimension #0.</param>
        /// <param name="d1">Index into dimension #1.</param>
        /// <param name="d2">Index into dimension #2.</param>
        /// <returns>Copy of the element as located by the index arguments <paramref name="d0"/>...<paramref name="d2"/>.</returns>
        /// <remarks><para>An index for dimension i can be negative and must evaluate to an element position in the range 0...A.S[i]-1.</para>
        /// <para>This function does not support the merging of trailing, unspecified dimensions as known from 
        /// <see cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{bool}, long, long, long, long, long, long, long)"/> or from 
        /// <see cref="ILNumerics.ExtensionMethods.item{T1}(BaseArray{T1}, long)"/>.</para>
        /// <para>Unspecified dimension indices are assumed to be 0. Optional indices for virtual dimensions must be 0.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if an index exceeds the allowed range of the corresponding dimension.</exception>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{bool}, long, long, long, long, long, long, long)"/>
        public static T1 item<T1>(this BaseArray<T1> A, long d0,  long d1, long  d2) {
                using var _1 = ReaderLock.Create(A as ConcreteArray<T1, Array<T1>, InArray<T1>, OutArray<T1>, Array<T1>, Storage<T1>>, out var storage);
                if (d2 < 0) d2 += storage.S[2];
                if ((ulong)d2 >= (ulong)storage.S[2]) throw new IndexOutOfRangeException("Index into dimension 0 exceeds the range of existing elements. Consider using GetValue() instead.");
                return storage.GetValueSeq(storage.Size.GetSeqIndex(d0,  d1,  d2));
        }

        /// <summary>
        /// [numpy API] Returns a single element from this array according to the provided dimension indices.
        /// </summary>
        /// <typeparam name="T1">Element type.</typeparam>
        /// <param name="A">The ILNumerics n-dim array.</param>
        /// <param name="d0">Index into dimension #0.</param>
        /// <param name="d1">Index into dimension #1.</param>
        /// <param name="d2">Index into dimension #2.</param>
        /// <param name="d3">Index into dimension #3.</param>
        /// <returns>Copy of the element as located by the index arguments <paramref name="d0"/>...<paramref name="d3"/>.</returns>
        /// <remarks><para>An index for dimension i can be negative and must evaluate to an element position in the range 0...A.S[i]-1.</para>
        /// <para>This function does not support the merging of trailing, unspecified dimensions as known from 
        /// <see cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{bool}, long, long, long, long, long, long, long)"/> or from 
        /// <see cref="ILNumerics.ExtensionMethods.item{T1}(BaseArray{T1}, long)"/>.</para>
        /// <para>Unspecified dimension indices are assumed to be 0. Optional indices for virtual dimensions must be 0.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if an index exceeds the allowed range of the corresponding dimension.</exception>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{bool}, long, long, long, long, long, long, long)"/>
        public static T1 item<T1>(this BaseArray<T1> A, long d0,  long d1, long d2, long  d3) {
            using var _1 = ReaderLock.Create(A as ConcreteArray<T1, Array<T1>, InArray<T1>, OutArray<T1>, Array<T1>, Storage<T1>>, out var storage);
            if (d3 < 0) d3 += storage.S[3];
                if ((ulong)d3 >= (ulong)storage.S[3]) throw new IndexOutOfRangeException("Index into dimension 0 exceeds the range of existing elements. Consider using GetValue() instead.");
                return storage.GetValueSeq(storage.Size.GetSeqIndex(d0,  d1,  d2,  d3));
        }

        /// <summary>
        /// [numpy API] Returns a single element from this array according to the provided dimension indices.
        /// </summary>
        /// <typeparam name="T1">Element type.</typeparam>
        /// <param name="A">The ILNumerics n-dim array.</param>
        /// <param name="d0">Index into dimension #0.</param>
        /// <param name="d1">Index into dimension #1.</param>
        /// <param name="d2">Index into dimension #2.</param>
        /// <param name="d3">Index into dimension #3.</param>
        /// <param name="d4">Index into dimension #4.</param>
        /// <returns>Copy of the element as located by the index arguments <paramref name="d0"/>...<paramref name="d4"/>.</returns>
        /// <remarks><para>An index for dimension i can be negative and must evaluate to an element position in the range 0...A.S[i]-1.</para>
        /// <para>This function does not support the merging of trailing, unspecified dimensions as known from 
        /// <see cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{bool}, long, long, long, long, long, long, long)"/> or from 
        /// <see cref="ILNumerics.ExtensionMethods.item{T1}(BaseArray{T1}, long)"/>.</para>
        /// <para>Unspecified dimension indices are assumed to be 0. Optional indices for virtual dimensions must be 0.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if an index exceeds the allowed range of the corresponding dimension.</exception>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{bool}, long, long, long, long, long, long, long)"/>
        public static T1 item<T1>(this BaseArray<T1> A, long d0,  long d1, long d2, long d3, long  d4) {
            using var _1 = ReaderLock.Create(A as ConcreteArray<T1, Array<T1>, InArray<T1>, OutArray<T1>, Array<T1>, Storage<T1>>, out var storage);
            if (d4 < 0) d4 += storage.S[4];
                if ((ulong)d4 >= (ulong)storage.S[4]) throw new IndexOutOfRangeException("Index into dimension 0 exceeds the range of existing elements. Consider using GetValue() instead.");
                return storage.GetValueSeq(storage.Size.GetSeqIndex(d0,  d1,  d2,  d3,  d4));
        }

        /// <summary>
        /// [numpy API] Returns a single element from this array according to the provided dimension indices.
        /// </summary>
        /// <typeparam name="T1">Element type.</typeparam>
        /// <param name="A">The ILNumerics n-dim array.</param>
        /// <param name="d0">Index into dimension #0.</param>
        /// <param name="d1">Index into dimension #1.</param>
        /// <param name="d2">Index into dimension #2.</param>
        /// <param name="d3">Index into dimension #3.</param>
        /// <param name="d4">Index into dimension #4.</param>
        /// <param name="d5">Index into dimension #5.</param>
        /// <returns>Copy of the element as located by the index arguments <paramref name="d0"/>...<paramref name="d5"/>.</returns>
        /// <remarks><para>An index for dimension i can be negative and must evaluate to an element position in the range 0...A.S[i]-1.</para>
        /// <para>This function does not support the merging of trailing, unspecified dimensions as known from 
        /// <see cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{bool}, long, long, long, long, long, long, long)"/> or from 
        /// <see cref="ILNumerics.ExtensionMethods.item{T1}(BaseArray{T1}, long)"/>.</para>
        /// <para>Unspecified dimension indices are assumed to be 0. Optional indices for virtual dimensions must be 0.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if an index exceeds the allowed range of the corresponding dimension.</exception>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{bool}, long, long, long, long, long, long, long)"/>
        public static T1 item<T1>(this BaseArray<T1> A, long d0,  long d1, long d2, long d3, long d4, long  d5) {
            using var _1 = ReaderLock.Create(A as ConcreteArray<T1, Array<T1>, InArray<T1>, OutArray<T1>, Array<T1>, Storage<T1>>, out var storage);
            if (d5 < 0) d5 += storage.S[5];
                if ((ulong)d5 >= (ulong)storage.S[5]) throw new IndexOutOfRangeException("Index into dimension 0 exceeds the range of existing elements. Consider using GetValue() instead.");
                return storage.GetValueSeq(storage.Size.GetSeqIndex(d0,  d1,  d2,  d3,  d4,  d5));
        }

        /// <summary>
        /// [numpy API] Returns a single element from this array according to the provided dimension indices.
        /// </summary>
        /// <typeparam name="T1">Element type.</typeparam>
        /// <param name="A">The ILNumerics n-dim array.</param>
        /// <param name="d0">Index into dimension #0.</param>
        /// <param name="d1">Index into dimension #1.</param>
        /// <param name="d2">Index into dimension #2.</param>
        /// <param name="d3">Index into dimension #3.</param>
        /// <param name="d4">Index into dimension #4.</param>
        /// <param name="d5">Index into dimension #5.</param>
        /// <param name="d6">Index into dimension #6.</param>
        /// <returns>Copy of the element as located by the index arguments <paramref name="d0"/>...<paramref name="d6"/>.</returns>
        /// <remarks><para>An index for dimension i can be negative and must evaluate to an element position in the range 0...A.S[i]-1.</para>
        /// <para>This function does not support the merging of trailing, unspecified dimensions as known from 
        /// <see cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{bool}, long, long, long, long, long, long, long)"/> or from 
        /// <see cref="ILNumerics.ExtensionMethods.item{T1}(BaseArray{T1}, long)"/>.</para>
        /// <para>Unspecified dimension indices are assumed to be 0. Optional indices for virtual dimensions must be 0.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if an index exceeds the allowed range of the corresponding dimension.</exception>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{bool}, long, long, long, long, long, long, long)"/>
        public static T1 item<T1>(this BaseArray<T1> A, long d0,  long d1, long d2, long d3, long d4, long d5, long  d6) {
            using var _1 = ReaderLock.Create(A as ConcreteArray<T1, Array<T1>, InArray<T1>, OutArray<T1>, Array<T1>, Storage<T1>>, out var storage);
            if (d6 < 0) d6 += storage.S[6];
                if ((ulong)d6 >= (ulong)storage.S[6]) throw new IndexOutOfRangeException("Index into dimension 0 exceeds the range of existing elements. Consider using GetValue() instead.");
                return storage.GetValueSeq(storage.Size.GetSeqIndex(d0,  d1,  d2,  d3,  d4,  d5,  d6));
        }

        /// <summary>
        /// [numpy API] Returns a single element from this array according to the provided dimension indices.
        /// </summary>
        /// <typeparam name="T1">Element type.</typeparam>
        /// <param name="A">The ILNumerics n-dim array.</param>
        /// <param name="dims">Indices into the dimensions of A, values can be negative.</param>
        /// <returns>Copy of the element as located by the indices given by <paramref name="dims"/>.</returns>
        /// <remarks><para>An index for dimension i can be negative and must evaluate to an element position in the range 0...A.S[i]-1, where negative indices count backwards, starting from the last element index in dimension i.</para>
        /// <para>This function does not support the merging of trailing, unspecified dimensions as known from 
        /// <see cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{bool}, long, long, long, long, long, long, long)"/> or from 
        /// <see cref="ILNumerics.ExtensionMethods.item{T1}(BaseArray{T1}, long)"/>.</para>
        /// <para>Unspecified dimension indices are assumed to be 0. Optional indices for virtual dimensions must be 0.</para>
        /// <para>Consequently, providing <paramref name="dims"/> as null or as empty array returns the first element of <paramref name="A"/> or 
        /// the only element for a scalar array <paramref name="A"/>. However, if <paramref name="A"/> is empty an <see cref="IndexOutOfRangeException"/> will be thrown.</para>
        /// <para>If <paramref name="dims"/> has exactly one element, this element is considered as the sequential/ linear index into the <see cref="flatten{T1}(BaseArray{T1}, StorageOrders)">flattened</see> 
        /// array.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if an index exceeds the allowed range of the corresponding dimension.</exception>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{bool}, long, long, long, long, long, long, long)"/>
        public static T1 item<T1>(this BaseArray<T1> A, params long[] dims) {
            using var _1 = ReaderLock.Create(A as ConcreteArray<T1, Array<T1>, InArray<T1>, OutArray<T1>, Array<T1>, Storage<T1>>, out var storage);
            if (Equals(dims, null) || dims.Length == 0) {
                    return storage.GetValue(0); 
                }
                if (dims.Length > Size.MaxNumberOfDimensions) {
                    throw new ArgumentException($"Too many indices. The number of indices may not exceed the maximum number of dimensions ({Size.MaxNumberOfDimensions}).");
                } else if (dims.Length == 1) {
                    return storage.GetValueSeq(storage.Size.GetSeqIndex_NP(dims[0]));
                }
                return storage.GetValueSeq(storage.Size.GetSeqIndex(dims));
        }

        #endregion

        #region ndarray.itemset

        #region HYCALPER LOOPSTART NUMPY_METHODS_itemset
        /*!HC:TYPELIST:
        <hycalper>
        <type>
            <source locate="here">
            d1
            </source>
            <destination>d2</destination>
            <destination>d3</destination>
            <destination>d4</destination>
            <destination>d5</destination>
            <destination>d6</destination>
       </type>
        <type>
            <source locate="after" endmark=" u[])">
            lastDimIdx
            </source>
            <destination>2</destination>
            <destination>3</destination>
            <destination>4</destination>
            <destination>5</destination>
            <destination>6</destination>
        </type>
        <type>
            <source locate="after" endmark=" ();,">
            declaration
            </source>
            <destination>long d1, long </destination>
            <destination>long d1, long d2, long </destination>
            <destination>long d1, long d2, long d3, long </destination>
            <destination>long d1, long d2, long d3, long d4, long </destination>
            <destination>long d1, long d2, long d3, long d4, long d5, long </destination>
       </type>
        <type>
            <source locate="after" endmark=" ();,">
            invocation
            </source>
            <destination> d1,  d2</destination>
            <destination> d1,  d2,  d3</destination>
            <destination> d1,  d2,  d3,  d4</destination>
            <destination> d1,  d2,  d3,  d4,  d5</destination>
            <destination> d1,  d2,  d3,  d4,  d5,  d6</destination>
       </type>
        </hycalper>
        */
#pragma warning disable CS1573 // ...parameter has no matching doc tag in XML doc

        /// <summary>
        /// [numpy API] Replaces a single element of this array at the element location according to the provided dimension indices.
        /// </summary>
        /// <typeparam name="T1">Element type.</typeparam>
        /// <param name="A">The ILNumerics n-dim array.</param>
        /// <param name="d0">The index into dimension #0.</param>
        /// <param name="value">The single value to be copied to the element at the specified position.</param>
        /// <remarks><para>The index for dimension i can be negative and must evaluate to an element position in the range 0...A.S[i]-1.</para>
        /// <para>This function does not support to expand the array for indices outside of the existing range (as SetValue() does). An 
        /// exception will be thrown instead.</para>
        /// <para>Unspecified dimension indices are assumed to be 0. Optional indices for virtual dimensions must be 0.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if an index exceeds the allowed range of the corresponding dimension.</exception>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static void itemset<T1>(this Mutable<T1, Array<T1>, InArray<T1>, OutArray<T1>, Array<T1>, Storage<T1>> A, long d0,/*!HC:declaration*/  long d1, T1 value) {
            lock (A.SynchObj) {
                //using var _1 = ReaderLock.Create(A, out var storage);
                var storage = A.Storage; 
                if (d1 < 0) d1 += storage.S[/*!HC:lastDimIdx*/1u];
                if ((ulong)d1 >= (ulong)storage.S[/*!HC:lastDimIdx*/1u]) throw new IndexOutOfRangeException("Index into dimension 0 exceeds the range of existing elements. Consider using SetValue() instead, which supports expansion of the array.");
                storage.SetValueSeq(value, storage.Size.GetSeqIndex(d0,/*!HC:invocation*/ d1) * Storage<T1>.SizeOfT);
            }
        }

        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       
#pragma warning disable CS1573 // ...parameter has no matching doc tag in XML doc

        /// <summary>
        /// [numpy API] Replaces a single element of this array at the element location according to the provided dimension indices.
        /// </summary>
        /// <typeparam name="T1">Element type.</typeparam>
        /// <param name="A">The ILNumerics n-dim array.</param>
        /// <param name="d0">The index into dimension #0.</param>
        /// <param name="value">The single value to be copied to the element at the specified position.</param>
        /// <remarks><para>The index for dimension i can be negative and must evaluate to an element position in the range 0...A.S[i]-1.</para>
        /// <para>This function does not support to expand the array for indices outside of the existing range (as SetValue() does). An 
        /// exception will be thrown instead.</para>
        /// <para>Unspecified dimension indices are assumed to be 0. Optional indices for virtual dimensions must be 0.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if an index exceeds the allowed range of the corresponding dimension.</exception>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static void itemset<T1>(this Mutable<T1, Array<T1>, InArray<T1>, OutArray<T1>, Array<T1>, Storage<T1>> A, long d0,  long d1, long d2, long d3, long d4, long d5, long  d6, T1 value) {
            lock (A.SynchObj) {
                //using var _1 = ReaderLock.Create(A, out var storage);
                var storage = A.Storage; 
                if (d6 < 0) d6 += storage.S[6u];
                if ((ulong)d6 >= (ulong)storage.S[6u]) throw new IndexOutOfRangeException("Index into dimension 0 exceeds the range of existing elements. Consider using SetValue() instead, which supports expansion of the array.");
                storage.SetValueSeq(value, storage.Size.GetSeqIndex(d0,  d1,  d2,  d3,  d4,  d5,  d6) * Storage<T1>.SizeOfT);
            }
        }

       
#pragma warning disable CS1573 // ...parameter has no matching doc tag in XML doc

        /// <summary>
        /// [numpy API] Replaces a single element of this array at the element location according to the provided dimension indices.
        /// </summary>
        /// <typeparam name="T1">Element type.</typeparam>
        /// <param name="A">The ILNumerics n-dim array.</param>
        /// <param name="d0">The index into dimension #0.</param>
        /// <param name="value">The single value to be copied to the element at the specified position.</param>
        /// <remarks><para>The index for dimension i can be negative and must evaluate to an element position in the range 0...A.S[i]-1.</para>
        /// <para>This function does not support to expand the array for indices outside of the existing range (as SetValue() does). An 
        /// exception will be thrown instead.</para>
        /// <para>Unspecified dimension indices are assumed to be 0. Optional indices for virtual dimensions must be 0.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if an index exceeds the allowed range of the corresponding dimension.</exception>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static void itemset<T1>(this Mutable<T1, Array<T1>, InArray<T1>, OutArray<T1>, Array<T1>, Storage<T1>> A, long d0,  long d1, long d2, long d3, long d4, long  d5, T1 value) {
            lock (A.SynchObj) {
                //using var _1 = ReaderLock.Create(A, out var storage);
                var storage = A.Storage; 
                if (d5 < 0) d5 += storage.S[5u];
                if ((ulong)d5 >= (ulong)storage.S[5u]) throw new IndexOutOfRangeException("Index into dimension 0 exceeds the range of existing elements. Consider using SetValue() instead, which supports expansion of the array.");
                storage.SetValueSeq(value, storage.Size.GetSeqIndex(d0,  d1,  d2,  d3,  d4,  d5) * Storage<T1>.SizeOfT);
            }
        }

       
#pragma warning disable CS1573 // ...parameter has no matching doc tag in XML doc

        /// <summary>
        /// [numpy API] Replaces a single element of this array at the element location according to the provided dimension indices.
        /// </summary>
        /// <typeparam name="T1">Element type.</typeparam>
        /// <param name="A">The ILNumerics n-dim array.</param>
        /// <param name="d0">The index into dimension #0.</param>
        /// <param name="value">The single value to be copied to the element at the specified position.</param>
        /// <remarks><para>The index for dimension i can be negative and must evaluate to an element position in the range 0...A.S[i]-1.</para>
        /// <para>This function does not support to expand the array for indices outside of the existing range (as SetValue() does). An 
        /// exception will be thrown instead.</para>
        /// <para>Unspecified dimension indices are assumed to be 0. Optional indices for virtual dimensions must be 0.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if an index exceeds the allowed range of the corresponding dimension.</exception>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static void itemset<T1>(this Mutable<T1, Array<T1>, InArray<T1>, OutArray<T1>, Array<T1>, Storage<T1>> A, long d0,  long d1, long d2, long d3, long  d4, T1 value) {
            lock (A.SynchObj) {
                //using var _1 = ReaderLock.Create(A, out var storage);
                var storage = A.Storage; 
                if (d4 < 0) d4 += storage.S[4u];
                if ((ulong)d4 >= (ulong)storage.S[4u]) throw new IndexOutOfRangeException("Index into dimension 0 exceeds the range of existing elements. Consider using SetValue() instead, which supports expansion of the array.");
                storage.SetValueSeq(value, storage.Size.GetSeqIndex(d0,  d1,  d2,  d3,  d4) * Storage<T1>.SizeOfT);
            }
        }

       
#pragma warning disable CS1573 // ...parameter has no matching doc tag in XML doc

        /// <summary>
        /// [numpy API] Replaces a single element of this array at the element location according to the provided dimension indices.
        /// </summary>
        /// <typeparam name="T1">Element type.</typeparam>
        /// <param name="A">The ILNumerics n-dim array.</param>
        /// <param name="d0">The index into dimension #0.</param>
        /// <param name="value">The single value to be copied to the element at the specified position.</param>
        /// <remarks><para>The index for dimension i can be negative and must evaluate to an element position in the range 0...A.S[i]-1.</para>
        /// <para>This function does not support to expand the array for indices outside of the existing range (as SetValue() does). An 
        /// exception will be thrown instead.</para>
        /// <para>Unspecified dimension indices are assumed to be 0. Optional indices for virtual dimensions must be 0.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if an index exceeds the allowed range of the corresponding dimension.</exception>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static void itemset<T1>(this Mutable<T1, Array<T1>, InArray<T1>, OutArray<T1>, Array<T1>, Storage<T1>> A, long d0,  long d1, long d2, long  d3, T1 value) {
            lock (A.SynchObj) {
                //using var _1 = ReaderLock.Create(A, out var storage);
                var storage = A.Storage; 
                if (d3 < 0) d3 += storage.S[3u];
                if ((ulong)d3 >= (ulong)storage.S[3u]) throw new IndexOutOfRangeException("Index into dimension 0 exceeds the range of existing elements. Consider using SetValue() instead, which supports expansion of the array.");
                storage.SetValueSeq(value, storage.Size.GetSeqIndex(d0,  d1,  d2,  d3) * Storage<T1>.SizeOfT);
            }
        }

       
#pragma warning disable CS1573 // ...parameter has no matching doc tag in XML doc

        /// <summary>
        /// [numpy API] Replaces a single element of this array at the element location according to the provided dimension indices.
        /// </summary>
        /// <typeparam name="T1">Element type.</typeparam>
        /// <param name="A">The ILNumerics n-dim array.</param>
        /// <param name="d0">The index into dimension #0.</param>
        /// <param name="value">The single value to be copied to the element at the specified position.</param>
        /// <remarks><para>The index for dimension i can be negative and must evaluate to an element position in the range 0...A.S[i]-1.</para>
        /// <para>This function does not support to expand the array for indices outside of the existing range (as SetValue() does). An 
        /// exception will be thrown instead.</para>
        /// <para>Unspecified dimension indices are assumed to be 0. Optional indices for virtual dimensions must be 0.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if an index exceeds the allowed range of the corresponding dimension.</exception>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static void itemset<T1>(this Mutable<T1, Array<T1>, InArray<T1>, OutArray<T1>, Array<T1>, Storage<T1>> A, long d0,  long d1, long  d2, T1 value) {
            lock (A.SynchObj) {
                //using var _1 = ReaderLock.Create(A, out var storage);
                var storage = A.Storage; 
                if (d2 < 0) d2 += storage.S[2u];
                if ((ulong)d2 >= (ulong)storage.S[2u]) throw new IndexOutOfRangeException("Index into dimension 0 exceeds the range of existing elements. Consider using SetValue() instead, which supports expansion of the array.");
                storage.SetValueSeq(value, storage.Size.GetSeqIndex(d0,  d1,  d2) * Storage<T1>.SizeOfT);
            }
        }


#endregion HYCALPER AUTO GENERATED CODE

        /// <summary>
        /// [numpy API] Replaces a single element of this array at the sequential element location provided.
        /// </summary>
        /// <typeparam name="T1">Element type.</typeparam>
        /// <param name="A">The ILNumerics n-dim array.</param>
        /// <param name="d0">Sequential index into the elements of the array. Assuming row-major storage order.</param>
        /// <param name="value">The single value to be copied to the element at the specified position.</param>
        /// <remarks><para>The index for dimension i can be negative and must evaluate to an element position in the range -A.S.NumberOfElements...A.S.NumberOfElements-1.</para>
        /// <para>This function does not support expanding the array for indices outside of the existing range (as SetValue() does). An 
        /// exception will be thrown instead when addressing any non-existing element.</para>
        /// <para><paramref name="d0"/> is the position of the element to be overwritten by assuming that the arrays elements are sequentially 
        /// laid-out in memory in row major order. This function handles arbitrary storage orders, though, by using its strides to compute the 
        /// actual element position in memory.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if an index exceeds the allowed range of the corresponding dimension.</exception>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.SetValue{T1, LocalT, InT, OutT, RetT, StorageT}(Mutable{T1, LocalT, InT, OutT, RetT, StorageT}, T1, long, long, long, long, long, long, long)"></seealso>
        public static void itemset<T1>(this Mutable<T1, Array<T1>, InArray<T1>, OutArray<T1>, Array<T1>, Storage<T1>> A, long d0, T1 value) {
            var storage = A.Storage;
            storage.SetValueSeq(value, storage.Size.GetSeqIndex_NP(d0) * Storage<T1>.SizeOfT);
        }

        /// <summary>
        /// [numpy API] Replaces a single element of this array at the specified location.
        /// </summary>
        /// <typeparam name="T1">Element type.</typeparam>
        /// <param name="A">The mutable ILNumerics n-dim array.</param>
        /// <param name="dims">Index array addressing the position of the element in <paramref name="A"/> to replace.</param>
        /// <param name="value">The single value to be copied to the element at the specified position.</param>
        /// <remarks><para>Elements in <paramref name="dims"/> can be negative and must evaluate to an existing element.</para>
        /// <para>This function does not support expanding the array for indices outside of the existing range (as SetValue() does). An 
        /// exception will be thrown instead when addressing any non-existing element.</para>
        /// <para>For <paramref name="dims"/> being null or empty the first element of <paramref name="A"/> is addressed. For numpy 
        /// scalars <paramref name="A"/> the only element stored is replaced.</para>
        /// <para>If <paramref name="dims"/> has a length of 1 the only element in <paramref name="dims"/> is considered the sequential
        /// /linear index into the flattened version of <paramref name="A"/>.</para>
        /// <para>
        /// This function handles arbitrary storage orders.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if an index exceeds the allowed range of the corresponding dimension.</exception>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        /// <seealso cref="put{T1, LocalT, InT, OutT, RetT, StorageT, IndT}(Mutable{T1, LocalT, InT, OutT, RetT, StorageT}, BaseArray{IndT}, InArray{T1}, PutModes)"/>
        /// <seealso cref="item{T1}(BaseArray{T1}, long[])"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.SetValue{T1, LocalT, InT, OutT, RetT, StorageT}(Mutable{T1, LocalT, InT, OutT, RetT, StorageT}, T1, long, long, long, long, long, long, long)"></seealso>
        public static void itemset<T1>(this Mutable<T1, Array<T1>, InArray<T1>, OutArray<T1>, Array<T1>, Storage<T1>> A, InArray<long> dims, T1 value) {
            using (Scope.Enter(dims)) {
                var storage = A.Storage;
                if (Equals(dims, null) || dims.Length == 0) {
                    storage.SetValueSeq(value, storage.Size.BaseOffset * Storage<T1>.SizeOfT);
                } else if (dims.Length == 1) {
                    storage.SetValueSeq(value, storage.Size.GetSeqIndex_NP(dims.GetValue(0)) * Storage<T1>.SizeOfT);
                } else {
                    storage.SetValueSeq(value, storage.Size.GetSeqIndex(dims) * Storage<T1>.SizeOfT);
                }
            }
        }

        #endregion

        #region ndarray.astype
        #region HYCALPER LOOPSTART 
        /*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="here">
        double
    </source>
    <destination>float</destination>
    <destination>byte</destination>
    <destination>sbyte</destination>
    <destination>ushort</destination>
    <destination>short</destination>
    <destination>uint</destination>
    <destination>int</destination>
    <destination>long</destination>
    <destination>ulong</destination>
    <destination>complex</destination>
    <destination>fcomplex</destination>
</type>
</hycalper>
*/

        /// <summary>
        /// [numpy API] Create a new array of specific element type and storage order based on this array.
        /// </summary>
        /// <typeparam name="Tout">The target element type. Must be a non-nullable value type.</typeparam>
        /// <param name="A">The source array.</param>
        /// <param name="order">[Optional] Storage order for the target array. Choices: null (default) - auto, <see cref="StorageOrders.ColumnMajor"/> or <see cref="StorageOrders.RowMajor"/>.</param>
        /// <returns>New array with the same shape as this array, having the element type converted to <typeparamref name="Tout"/>.</returns>
        public static Array<Tout> astype<Tout>(this BaseArray<double> A, StorageOrders? order = null) where Tout : struct {
            return astype<double, Tout>(A, order);
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        /// <summary>
        /// [numpy API] Create a new array of specific element type and storage order based on this array.
        /// </summary>
        /// <typeparam name="Tout">The target element type. Must be a non-nullable value type.</typeparam>
        /// <param name="A">The source array.</param>
        /// <param name="order">[Optional] Storage order for the target array. Choices: null (default) - auto, <see cref="StorageOrders.ColumnMajor"/> or <see cref="StorageOrders.RowMajor"/>.</param>
        /// <returns>New array with the same shape as this array, having the element type converted to <typeparamref name="Tout"/>.</returns>
        public static Array<Tout> astype<Tout>(this BaseArray<fcomplex> A, StorageOrders? order = null) where Tout : struct {
            return astype<fcomplex, Tout>(A, order);
        }
       

        /// <summary>
        /// [numpy API] Create a new array of specific element type and storage order based on this array.
        /// </summary>
        /// <typeparam name="Tout">The target element type. Must be a non-nullable value type.</typeparam>
        /// <param name="A">The source array.</param>
        /// <param name="order">[Optional] Storage order for the target array. Choices: null (default) - auto, <see cref="StorageOrders.ColumnMajor"/> or <see cref="StorageOrders.RowMajor"/>.</param>
        /// <returns>New array with the same shape as this array, having the element type converted to <typeparamref name="Tout"/>.</returns>
        public static Array<Tout> astype<Tout>(this BaseArray<complex> A, StorageOrders? order = null) where Tout : struct {
            return astype<complex, Tout>(A, order);
        }
       

        /// <summary>
        /// [numpy API] Create a new array of specific element type and storage order based on this array.
        /// </summary>
        /// <typeparam name="Tout">The target element type. Must be a non-nullable value type.</typeparam>
        /// <param name="A">The source array.</param>
        /// <param name="order">[Optional] Storage order for the target array. Choices: null (default) - auto, <see cref="StorageOrders.ColumnMajor"/> or <see cref="StorageOrders.RowMajor"/>.</param>
        /// <returns>New array with the same shape as this array, having the element type converted to <typeparamref name="Tout"/>.</returns>
        public static Array<Tout> astype<Tout>(this BaseArray<ulong> A, StorageOrders? order = null) where Tout : struct {
            return astype<ulong, Tout>(A, order);
        }
       

        /// <summary>
        /// [numpy API] Create a new array of specific element type and storage order based on this array.
        /// </summary>
        /// <typeparam name="Tout">The target element type. Must be a non-nullable value type.</typeparam>
        /// <param name="A">The source array.</param>
        /// <param name="order">[Optional] Storage order for the target array. Choices: null (default) - auto, <see cref="StorageOrders.ColumnMajor"/> or <see cref="StorageOrders.RowMajor"/>.</param>
        /// <returns>New array with the same shape as this array, having the element type converted to <typeparamref name="Tout"/>.</returns>
        public static Array<Tout> astype<Tout>(this BaseArray<long> A, StorageOrders? order = null) where Tout : struct {
            return astype<long, Tout>(A, order);
        }
       

        /// <summary>
        /// [numpy API] Create a new array of specific element type and storage order based on this array.
        /// </summary>
        /// <typeparam name="Tout">The target element type. Must be a non-nullable value type.</typeparam>
        /// <param name="A">The source array.</param>
        /// <param name="order">[Optional] Storage order for the target array. Choices: null (default) - auto, <see cref="StorageOrders.ColumnMajor"/> or <see cref="StorageOrders.RowMajor"/>.</param>
        /// <returns>New array with the same shape as this array, having the element type converted to <typeparamref name="Tout"/>.</returns>
        public static Array<Tout> astype<Tout>(this BaseArray<int> A, StorageOrders? order = null) where Tout : struct {
            return astype<int, Tout>(A, order);
        }
       

        /// <summary>
        /// [numpy API] Create a new array of specific element type and storage order based on this array.
        /// </summary>
        /// <typeparam name="Tout">The target element type. Must be a non-nullable value type.</typeparam>
        /// <param name="A">The source array.</param>
        /// <param name="order">[Optional] Storage order for the target array. Choices: null (default) - auto, <see cref="StorageOrders.ColumnMajor"/> or <see cref="StorageOrders.RowMajor"/>.</param>
        /// <returns>New array with the same shape as this array, having the element type converted to <typeparamref name="Tout"/>.</returns>
        public static Array<Tout> astype<Tout>(this BaseArray<uint> A, StorageOrders? order = null) where Tout : struct {
            return astype<uint, Tout>(A, order);
        }
       

        /// <summary>
        /// [numpy API] Create a new array of specific element type and storage order based on this array.
        /// </summary>
        /// <typeparam name="Tout">The target element type. Must be a non-nullable value type.</typeparam>
        /// <param name="A">The source array.</param>
        /// <param name="order">[Optional] Storage order for the target array. Choices: null (default) - auto, <see cref="StorageOrders.ColumnMajor"/> or <see cref="StorageOrders.RowMajor"/>.</param>
        /// <returns>New array with the same shape as this array, having the element type converted to <typeparamref name="Tout"/>.</returns>
        public static Array<Tout> astype<Tout>(this BaseArray<short> A, StorageOrders? order = null) where Tout : struct {
            return astype<short, Tout>(A, order);
        }
       

        /// <summary>
        /// [numpy API] Create a new array of specific element type and storage order based on this array.
        /// </summary>
        /// <typeparam name="Tout">The target element type. Must be a non-nullable value type.</typeparam>
        /// <param name="A">The source array.</param>
        /// <param name="order">[Optional] Storage order for the target array. Choices: null (default) - auto, <see cref="StorageOrders.ColumnMajor"/> or <see cref="StorageOrders.RowMajor"/>.</param>
        /// <returns>New array with the same shape as this array, having the element type converted to <typeparamref name="Tout"/>.</returns>
        public static Array<Tout> astype<Tout>(this BaseArray<ushort> A, StorageOrders? order = null) where Tout : struct {
            return astype<ushort, Tout>(A, order);
        }
       

        /// <summary>
        /// [numpy API] Create a new array of specific element type and storage order based on this array.
        /// </summary>
        /// <typeparam name="Tout">The target element type. Must be a non-nullable value type.</typeparam>
        /// <param name="A">The source array.</param>
        /// <param name="order">[Optional] Storage order for the target array. Choices: null (default) - auto, <see cref="StorageOrders.ColumnMajor"/> or <see cref="StorageOrders.RowMajor"/>.</param>
        /// <returns>New array with the same shape as this array, having the element type converted to <typeparamref name="Tout"/>.</returns>
        public static Array<Tout> astype<Tout>(this BaseArray<sbyte> A, StorageOrders? order = null) where Tout : struct {
            return astype<sbyte, Tout>(A, order);
        }
       

        /// <summary>
        /// [numpy API] Create a new array of specific element type and storage order based on this array.
        /// </summary>
        /// <typeparam name="Tout">The target element type. Must be a non-nullable value type.</typeparam>
        /// <param name="A">The source array.</param>
        /// <param name="order">[Optional] Storage order for the target array. Choices: null (default) - auto, <see cref="StorageOrders.ColumnMajor"/> or <see cref="StorageOrders.RowMajor"/>.</param>
        /// <returns>New array with the same shape as this array, having the element type converted to <typeparamref name="Tout"/>.</returns>
        public static Array<Tout> astype<Tout>(this BaseArray<byte> A, StorageOrders? order = null) where Tout : struct {
            return astype<byte, Tout>(A, order);
        }
       

        /// <summary>
        /// [numpy API] Create a new array of specific element type and storage order based on this array.
        /// </summary>
        /// <typeparam name="Tout">The target element type. Must be a non-nullable value type.</typeparam>
        /// <param name="A">The source array.</param>
        /// <param name="order">[Optional] Storage order for the target array. Choices: null (default) - auto, <see cref="StorageOrders.ColumnMajor"/> or <see cref="StorageOrders.RowMajor"/>.</param>
        /// <returns>New array with the same shape as this array, having the element type converted to <typeparamref name="Tout"/>.</returns>
        public static Array<Tout> astype<Tout>(this BaseArray<float> A, StorageOrders? order = null) where Tout : struct {
            return astype<float, Tout>(A, order);
        }

#endregion HYCALPER AUTO GENERATED CODE


        /// <summary>
        /// [numpy API] Create a new array from generic input array, specify element type and storage order. This function is provided for compatibility with open generic array types.
        /// </summary>
        /// <typeparam name="Tin">The element type of the input. Must be a non-nullable value type.</typeparam>
        /// <typeparam name="Tout">The target element type. Must be a non-nullable value type.</typeparam>
        /// <param name="A">This array.</param>
        /// <param name="order">[Optional] Storage order for the target array. Choices: null (default) - auto (depends on current value of <see cref="Settings.ArrayStyle"/>, <see cref="StorageOrders.ColumnMajor"/> or <see cref="StorageOrders.RowMajor"/>.</param>
        /// <returns>New array with the same shape as this array, having the element type converted to <typeparamref name="Tout"/>.</returns>
        public static Array<Tout> astype<Tin, Tout>(this BaseArray<Tin> A, StorageOrders? order = null) where Tout : struct where Tin : struct {
            using (Scope.Enter()) {
                Array<Tout> R = MathInternal.convert<Tin, Tout>(A); // in case: releases A
                if (order == null) {
                    order = R.S.StorageOrder;
                }
                R.Storage.EnsureStorageOrder(order.GetValueOrDefault());
                return R;
            }
        }

        /// <summary>
        /// [numpy API] Create a new array of specific element type and storage order based on this array.
        /// </summary>
        /// <typeparam name="Tout">The target element type. Must be a non-nullable value type.</typeparam>
        /// <param name="A">This array.</param>
        /// <param name="order">[Optional] Storage order for the target array. Choices: null (default) - auto, <see cref="StorageOrders.ColumnMajor"/> or <see cref="StorageOrders.RowMajor"/>.</param>
        /// <returns>New array with the same shape as this array, having the element type converted to <typeparamref name="Tout"/>.</returns>
        public static Array<Tout> astype<Tout>(this Logical A, StorageOrders? order = null) where Tout : struct {
            using (Scope.Enter()) {
                Array<Tout> ret = MathInternal.convert<bool, Tout>(A); // in case: releases A
                if (order == null) {
                    order = ret.S.StorageOrder;
                }
                ret.Storage.EnsureStorageOrder(order.GetValueOrDefault());
                return ret;
            }
        }

        #endregion

        #region ndarray.copy
        /// <summary>
        /// [numpy API] Returns a copy of this array, optionally determining the storage order.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="LocalT"></typeparam>
        /// <typeparam name="InT"></typeparam>
        /// <typeparam name="OutT"></typeparam>
        /// <typeparam name="RetT"></typeparam>
        /// <typeparam name="StorageT"></typeparam>
        /// <param name="A">The source array.</param>
        /// <param name="order">[Optional] The destination storage order. Default: (null) - automatically determine the best (fastest) storage order. If no preferred storage order can be determined <see cref="Settings.DefaultStorageOrder"/> is used.</param>
        /// <returns>Copy of this array or this array if no copy is necessary.</returns>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.C"/>
        [SecuritySafeCritical]
        public static RetT copy<T1, LocalT, InT, OutT, RetT, StorageT>(this ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> A, StorageOrders? order = null)
            where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {

            var ret = BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>.Create();
            using (ReaderLock.Create(A, out var storage)) {
                ret.Handles[0] = storage.New((ulong)Math.Max(storage.S.NumberOfElements, 1));
                if (!order.HasValue) {
                    order = storage.S.StorageOrder;
                }
                if (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor) {
                    order = Settings.DefaultStorageOrder;
                }
                if (order == storage.S.StorageOrder) {
                    return (storage.Clone() as StorageT).RetArray;
                } else {
                    storage.CopyTo(ret.Handles[0], ret.S, order);
                }
            }
            return ret.RetArray;
        }
        #endregion

        #region numpyInternal.fill
        /// <summary>
        /// [numpy API] Overwrites all elements of <paramref name="A"/> with the same value <paramref name="scalar"/>.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="LocalT"></typeparam>
        /// <typeparam name="InT"></typeparam>
        /// <typeparam name="OutT"></typeparam>
        /// <typeparam name="RetT"></typeparam>
        /// <typeparam name="StorageT"></typeparam>
        /// <param name="A">The array.</param>
        /// <param name="scalar">The new value as scalar.</param>
        /// <remarks><para>This function alters the source array and is only available for mutable array types.</para></remarks>
        public static void fill<T1, LocalT, InT, OutT, RetT, StorageT>(this Mutable<T1, LocalT, InT, OutT, RetT, StorageT> A, T1 scalar)
            where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {

            using (ReaderLock.Create(A, out var storage)) {
                if (storage.Size.NumberOfElements == 0) {
                    return;
                }
                storage.GetLocalArray(retain: false).SetRange(scalar, Globals.ellipsis);
            }
        }
        #endregion

        #region numpyInternal.flatten, ravel
        /*  From the official numpy documentation: 
         *  Return a copy of the array collapsed into one dimension.
            Parameters:	

            order : ï¿½Cï¿½, ï¿½Fï¿½, ï¿½Aï¿½, ï¿½Kï¿½, optional

            ï¿½Cï¿½ means to flatten in row-major (C-style) order. ï¿½Fï¿½ means to flatten in column-major (Fortran- style) order. ï¿½Aï¿½ means to flatten in column-major order if a is Fortran contiguous in memory, row-major order otherwise. ï¿½Kï¿½ means to flatten a in the order the elements occur in memory. The default is ï¿½Cï¿½.

            Returns:	

            y : ndarray

            A copy of the input array, flattened to one dimension.

            See also

            ravel
            Return a flattened array.
            flat
            A 1-D flat iterator over the array.

            Examples

            >>> a = np.array([[1,2], [3,4]])
            >>> a.flatten()
            array([1, 2, 3, 4])
            >>> a.flatten('F')
            array([1, 3, 2, 4])
        */

        /// <summary>
        /// [numpy API] Returns a flattened copy of the source array. This is the same as <see cref="ravel{T1}(BaseArray{T1}, StorageOrders)"/>. See remarks for details.
        /// </summary>
        /// <typeparam name="T1">Element type.</typeparam>
        /// <param name="A">The source array.</param>
        /// <param name="order">[Optional] The storage order of the array returned. Default: row major.</param>
        /// <returns>A new array having the same element number and -type as <paramref name="A"/> flattened to a vector.</returns>
        /// <remarks><para>This is the same as 'A.Reshape(-1,order)'. Note that in ILNumerics all arrays semantically behave 
        /// like copies. I.e.: they have value semantic: the array returned cannot be used to alter element values of A (as with numpy views).</para></remarks>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Reshape(long, StorageOrders?)"/>
        /// <seealso cref="ravel{T1}(BaseArray{T1}, StorageOrders)"/>
        public static Array<T1> flatten<T1>(this BaseArray<T1> A, StorageOrders order = StorageOrders.RowMajor) {
            return (A as ConcreteArray<T1, Array<T1>, InArray<T1>, OutArray<T1>, Array<T1>, Storage<T1>>).Reshape(-1, order);
        }
        /// <summary>
        /// [numpy API] Returns a flattened copy of the source array. This is the same as <see cref="ravel{T1}(BaseArray{T1}, StorageOrders)"/>. See remarks for details.
        /// </summary>
        /// <param name="A">The source array.</param>
        /// <param name="order">[Optional] The storage order of the array returned. Default: row major.</param>
        /// <returns>A new array having the same element number and -type as <paramref name="A"/> flattened to a vector.</returns>
        /// <remarks><para>This is the same as 'A.Reshape(-1,order)'. Note that in ILNumerics all arrays semantically behave 
        /// like copies. I.e.: they have value semantic: the array returned cannot be used to alter element values of A (as with numpy views).</para></remarks>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Reshape(long, StorageOrders?)"/>
        /// <seealso cref="ravel{T1}(BaseArray{T1}, StorageOrders)"/>
        public static Logical flatten(this BaseArray<bool> A, StorageOrders order = StorageOrders.RowMajor) {
            return (A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>).Reshape(-1, order);
        }

        /// <summary>
        /// [numpy API] Returns a flattened copy of the source array. This is the same as <see cref="flatten{T1}(BaseArray{T1}, StorageOrders)"/>. See remarks for details.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="A">The input array.</param>
        /// <param name="order">[Optional] The storage order of the array returned. Default: row major.</param>
        /// <returns>A new array having the same element number and -type as <paramref name="A"/> flattened to a vector.</returns>
        /// <remarks><para>This is the same as 'A.Reshape(-1,order)'. Note that in ILNumerics all arrays semantically behave 
        /// like copies. I.e.: they have value semantic: the array returned cannot be used to alter element values of A (as with numpy views).</para></remarks>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Reshape(long, StorageOrders?)"/>
        /// <seealso cref="flatten{T1}(BaseArray{T1}, StorageOrders)"/>
        public static Array<T1> ravel<T1>(this BaseArray<T1> A, StorageOrders order = StorageOrders.RowMajor) {
            return flatten(A, order);
        }
        #endregion

        #region real, imag
        /// <summary>
        /// [numpy API] Extract real parts from this array.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>Array with the real parts of <paramref name="A"/>.</returns>
        public static unsafe Array<double> real(this BaseArray<complex> A) {

            return MathInternal.real(A);
        }
        /// <summary>
        /// [numpy API] Extract real parts from this array.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>Array with the real parts of <paramref name="A"/>.</returns>
        public static unsafe Array<float> real(this BaseArray<fcomplex> A) {

            return MathInternal.real(A);
        }
        /// <summary>
        /// [numpy API] Extract imaginary parts from this array.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>Array with the imaginary parts of <paramref name="A"/>.</returns>
        public static unsafe Array<double> imag(this BaseArray<complex> A) {

            return MathInternal.imag(A);
        }
        /// <summary>
        /// [numpy API] Extract imaginary parts from this array.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>Array with the imaginary parts of <paramref name="A"/>.</returns>
        public static unsafe Array<float> imag(this BaseArray<fcomplex> A) {

            return MathInternal.imag(A);
        }
        #endregion

        #region resize
        /// <summary>
        /// [numpy API] Resize this array. This may allocates new memory if the new size is larger than the current size.
        /// </summary>
        /// <param name="size">The new size. Vector of non-negative elements.</param>
        /// <remarks>The storage order of this array is retained after resize. By providing an empty array for <paramref name="size"/> a 
        /// scalar array is produced. The result respects the current setting of <see cref="Settings.MinNumberOfArrayDimensions"/>.</remarks>
        /// <exception cref="ArgumentException">If <paramref name="size"/> is null.</exception>
        /// <exception cref="InvalidOperationException"> if the elements are not stored in a contiguous storage layout.</exception>
        [SecuritySafeCritical]
        public static unsafe void resize<T1, LocalT, InT, OutT, RetT, StorageT>(this Mutable<T1, LocalT, InT, OutT, RetT, StorageT> A, InArray<long> size) 
            where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {

            lock(A.SynchObj)

            using (Scope.Enter(size)) {

                var storage = A.Storage;
                if (storage.Size.StorageOrder != StorageOrders.ColumnMajor && storage.Size.StorageOrder != StorageOrders.RowMajor) {
                    throw new InvalidOperationException("The resize() function requires the array's elements to be stored either in row major (C-style) or column major (Fortran style) order.");
                }
                if (Equals(size, null)) {
                    throw new ArgumentException("The size parameter may not be 'null'. Provide an empty array if resizing to a scalar is intended!");
                }
                if (size.Size.NumberOfElements > Size.MaxNumberOfDimensions) {
                    throw new ArgumentException($"The size parameter cannot have more elements than {nameof(Size)}.{nameof(Size.MaxNumberOfDimensions)} ({Size.MaxNumberOfDimensions}).");
                }
                long nelem = 1;
                foreach (var s in size) {
                    nelem *= s;
                }
                //if (storage.ReferenceCount > 1) {
                //    throw new InvalidOperationException("This array is part of a shared array set and cannot be changed. Following the ILNumerics function rules should prevent from situations like this.");
                //}
                if ((ulong)(nelem * Storage<T1>.SizeOfT) > storage.Handles[0].Length.ToUInt64()) {
                    // allocate new memory & copy over existing elements
                    var newMemHandle = DeviceManager.GetDevice(0).New<T1>((ulong)(nelem * Storage<T1>.SizeOfT), true);
                    storage.Handles[0].CopyTo(0, newMemHandle, 0, 0, /*continous storage! */ new IntPtr(storage.Size.NumberOfElements * Storage<T1>.SizeOfT));
                    storage.Handles.Release();
                    storage.Handles = CountableArray.Create();
                    storage.Handles[0] = newMemHandle;
                }
                if (size.IsEmpty) {
                    // turns into a scalar
                    storage.Size.SetScalar(storage.Size.BaseOffset, Math.Max(0, Settings.MinNumberOfArrayDimensions));
                } else {
                    storage.Size.SetAll(size, storage.Size.StorageOrder);
                }
            }
        }
        #endregion

        #region transpose(*axes), transpose()
        /// <summary>
        /// [numpy API] Returns a version of this array having the dimensions reordered according to <paramref name="axes"/>.
        /// </summary>
        /// <param name="axes">The new dimension indices order.</param>
        /// <returns>New array based on this array with the same number of elements but reordered dimensions.</returns>
        /// <remarks><para>This function does not alter the source array. Instead, it creates a clone of this array and reorders the 
        /// dimensions on the clone.</para></remarks>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.T"/>
        /// <seealso cref="Size.GetShifted(int, long*)"/>
        /// <seealso cref="Size.SwapDimensions()"/>
        /// <seealso cref="Size.GetPermuted(long*, uint*, uint)"/>
        public static unsafe RetT transpose<T1, LocalT, InT, OutT, RetT, StorageT>(this ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> A, InArray<long> axes) where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {

            using var _1 = ReaderLock.Create(A, out var aStorage); 
            using (Scope.Enter()) {

                Array<long> myAxes = axes; 
                // no inplace / self return here: Size.GetPermuted is the limitation. It cannot work inplace.
                var ret = BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>.Create(aStorage.Handles);

                Array<uint> axesUint = MathInternal.touint32(myAxes);
                aStorage.Size.GetPermuted(ret.S.GetBSD(true), (uint*)axesUint.GetHostPointerForRead(), (uint)myAxes.S.NumberOfElements);

                return ret.RetArray;
            }
        }
        /// <summary>
        /// [numpy API] Returns a version of this array having the dimensions swapped (reversed).
        /// </summary>
        /// <returns>New array based on this array with the same number of elements and reversed dimensions.</returns>
        /// <remarks><para>This function does not alter the source array. Instead, it creates a lazy copy and reorders the 
        /// dimensions on the copy.</para></remarks>
        /// <seealso cref="transpose{T1, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}, InArray{long})"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.T"/>
        /// <seealso cref="Size.GetShifted(int, long*)"/>
        /// <seealso cref="Size.SwapDimensions()"/>
        /// <seealso cref="Size.GetPermuted(long*, uint*, uint)"/>
        public static unsafe RetT transpose<T1, LocalT, InT, OutT, RetT, StorageT>(this ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> A) where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {

            using var _1 = ReaderLock.Create(A, out var aStorage);

            StorageT ret = BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>.Create(aStorage.Handles, aStorage.Size);

            ret.Size.SwapDimensions();
            return ret.RetArray;
        }
        #endregion

        #region squeeze
        /// <summary>
        /// [numpy API] Remove individual or all singleton dimensions from this array.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="LocalT"></typeparam>
        /// <typeparam name="InT"></typeparam>
        /// <typeparam name="OutT"></typeparam>
        /// <typeparam name="RetT"></typeparam>
        /// <typeparam name="StorageT"></typeparam>
        /// <param name="A">The array whose size is to be altered.</param>
        /// <param name="axes">[Optional] Axis definition array. This holds the indices of the axes to be removed. Default: (null) or empty: remove all singleton dimensions.</param>
        /// <remarks><para>This function respect the value of <see cref="Settings.MinNumberOfArrayDimensions"/>. Thus, 
        /// if <see cref="Settings.ArrayStyle"/> is <see cref="ArrayStyles.ILNumericsV4"/> (default) at least 2 dimensions will remain in the array.</para>
        /// </remarks>
        /// <exception cref="ArgumentException"> if any dimension index provided in <paramref name="axes"/> points to a non-singleton dimension.</exception>
        public static unsafe void squeeze<T1, LocalT, InT, OutT, RetT, StorageT>(this Mutable<T1, LocalT, InT, OutT, RetT, StorageT> A, InArray<long> axes = null)
            where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {

            // instead of read-locking we can also 'detach' axes to a local array. This way it will not be publicly visible and becomes thread safe.
            // using var _1 = ReaderLock.Create(axes, out var axStorage);    

            lock(A.SynchObj)
            using (Scope.Enter()) {

                Array<long> myAxes = axes; 

                if (A.S.NumberOfDimensions == 0) {
                    return;  //all dimensions are virtual singletons and are all removed already.
                }

                Array<long> remAxes = MathInternal.sort(Equals(myAxes, null) || myAxes.IsEmpty ? MathInternal.find(A.shape == 1) : myAxes[Globals.full], descending: true);

                if (MathInternal.anyall(remAxes < 0)) {
                    throw new IndexOutOfRangeException("The 'axes' argument does not support negative indices!");
                }
                if (MathInternal.anyall(A.shape[remAxes] != 1)) {
                    throw new ArgumentException("Invalid axes argument. All dimension indices provided must reference non-virtual, singleton dimensions only!");
                }
                if (remAxes.Length == 0) {
                    return; 
                }
                var storage = A.Storage.Clone() as StorageT; 
                
                foreach (var i in remAxes) {
                    storage.S.RemoveDimension((uint)i);
                }
                A.Storage.Assign(storage,true, true, false); 
            }

        }
        #endregion

        #region swapaxes
        /// <summary>
        /// [numpy API] Returns a similar array having two axes exchanged.
        /// </summary>
        /// <param name="A">The source array. Will not be changed.</param>
        /// <param name="axis1">The first axis.</param>
        /// <param name="axis2">The second axis.</param>
        /// <returns>An array having the same number of dimensions, number of elements and elements as <paramref name="A"/> with dimensions axis1 and axis2 exchanged.</returns>
        /// <exception cref="ArgumentException">if <paramref name="axis1"/> or <paramref name="axis2"/> are out of range of <see cref="Size.NumberOfDimensions"/>.</exception>
        public static unsafe RetT swapaxes<T1, LocalT, InT, OutT, RetT, StorageT>(
            this ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> A, uint axis1, uint axis2)
            where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {

            using var _1 = ReaderLock.Create(A, out var storage); 
            return storage.Swapaxis(axis1, axis2, false).RetArray; 
        }
        #endregion

        #region put values
        /// <summary>
        /// [numpy API] Replaces elements of <paramref name="A"/> with <paramref name="values"/> at positions given by the sequential indices (flatten, row-major) <paramref name="indices"/>.
        /// </summary>
        /// <typeparam name="T1">Element type of <paramref name="A"/>.</typeparam>
        /// <typeparam name="LocalT">(subtype of <paramref name="A"/>)</typeparam>
        /// <typeparam name="InT">(subtype of <paramref name="A"/>)</typeparam>
        /// <typeparam name="OutT">(subtype of <paramref name="A"/>)</typeparam>
        /// <typeparam name="RetT">(subtype of <paramref name="A"/>)</typeparam>
        /// <typeparam name="StorageT">(subtype of <paramref name="A"/>)</typeparam>
        /// <typeparam name="IndT">Element type of indices. Must be numeric.</typeparam>
        /// <param name="A">The array storing the elements to be replaced.</param>
        /// <param name="indices">Index array. The shape is ignored. Values must be numeric and are read in row-major order.</param>
        /// <param name="values">Values array. The shape is ignored. Values are read in row-major order.</param>
        /// <param name="mode">[Optionl] Specifies how to handle index values in <paramref name="indices"/> which are out-of-range. Default: error.</param>
        /// <remarks>
        /// <para>This function has a similar effect as doing <c>A.flat[indices] = values</c> in numpyInternal. However, in ILNumerics the iterator 
        /// returned from <c>A.flat</c> is read-only. Use <see cref="put{T1, LocalT, InT, OutT, RetT, StorageT, IndT}(Mutable{T1, LocalT, InT, OutT, RetT, StorageT}, BaseArray{IndT}, InArray{T1}, PutModes)"/> as 
        /// a replacement.</para>
        /// <para>Note that the values in <paramref name="indices"/> are considered <i>sequential</i> indices, i.e. they correspond to the 
        /// element position in a flattened array, where the flattening is performed in row-major order. For performance reasons and if 
        /// <paramref name="A"/>.<see cref="Size.StorageOrder"/> is not <see cref="StorageOrders.RowMajor"/> A is converted to row-major 
        /// storage layout first and remains in row-major storage after the function returns. Note further, that the conversion happens 
        /// proactively, and is not rolled back in case of errors during the iteration.</para>
        /// <para>Elements of <paramref name="indices"/> can be negative, which corresponds to indexing from the end of flattened <paramref name="A"/>.</para>
        /// <para>Repeated values in <paramref name="indices"/> lead to only the last corresponding value in <paramref name="values"/> to be stored 
        /// at the respective position in <paramref name="A"/>.</para>
        /// <para>If <paramref name="values"/> has more elements than are indices provided in <paramref name="indices"/> superfluent values are ignored. No exception 
        /// is thrown in this case. If <paramref name="values"/> has fewer elements than indices are provided in <paramref name="indices"/> existing values 
        /// are repeated as necessary.</para>
        /// <para>The <paramref name="mode"/> parameter determines what happens with indices laying outside of the bounds of A. The default value
        /// of <see cref="PutModes.Raise"/> throws an <see cref="IndexOutOfRangeException"/> in this case. Two other options exist which bring the
        /// index back into the valid range: <see cref="PutModes.Wrap"/> computes the modulus, <see cref="PutModes.Clip"/> limits the indices to the allowed range. 
        /// Note that negative indices behave as usual (counting from the end of A) for modes <see cref="PutModes.Raise"/> and <see cref="PutModes.Wrap"/> only.</para>
        /// <para><see cref="put{T1, LocalT, InT, OutT, RetT, StorageT, IndT}(Mutable{T1, LocalT, InT, OutT, RetT, StorageT}, BaseArray{IndT}, InArray{T1}, PutModes)"/> requires 
        /// the element type of <paramref name="A"/> to be value types (struct, commonly numeric).</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if <paramref name="mode"/> is <see cref="PutModes.Raise"/> and any element 
        /// in <paramref name="indices"/> is out of the allowed range of [-A.S.NumberOfElements...&gt;= A.<see cref="Size.NumberOfElements"/>].</exception>
        /// <exception cref="ArgumentException">if either of <paramref name="indices"/> or <paramref name="values"/> is null, 
        /// if <paramref name="values"/> is empty but <paramref name="indices"/> is not, 
        /// if the specified <paramref name="mode"/> cannot successfully be applied (for example, because <paramref name="A"/> is empty).</exception>
        public static unsafe void put<T1, LocalT, InT, OutT, RetT, StorageT, IndT>(this Mutable<T1, LocalT, InT, OutT, RetT, StorageT> A,
                                        BaseArray<IndT> indices, InArray<T1> values, PutModes mode = PutModes.Raise)
            where IndT : struct, IConvertible
            where T1 : struct
            where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {

            numpyInternal.put(A, indices, values, mode);

        }
        #endregion

        #region repeat 
        /// <summary>
        /// [numpy API] Repeat elements along a flattened array or a specific axis.
        /// </summary>
        /// <typeparam name="T1">Element type of <paramref name="A"/>.</typeparam>
        /// <typeparam name="LocalT">(subtype of <paramref name="A"/>)</typeparam>
        /// <typeparam name="InT">(subtype of <paramref name="A"/>)</typeparam>
        /// <typeparam name="OutT">(subtype of <paramref name="A"/>)</typeparam>
        /// <typeparam name="RetT">(subtype of <paramref name="A"/>)</typeparam>
        /// <typeparam name="StorageT">(subtype of <paramref name="A"/>)</typeparam>
        /// <typeparam name="IndT">Element type of <paramref name="repeats"/>. Must be an integer type.</typeparam>
        /// <param name="A">The array storing the elements to be repeated.</param>
        /// <param name="repeats">Counts for element repetitions.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) flatten A and repeat all values along dimension #0.</param>
        /// <remarks>
        /// <para>This function repeats elements of <paramref name="A"/> along a single dimension. By default, where no <paramref name="axis"/> 
        /// is defined <paramref name="A"/> is reshaped to a vector in row-major order and all elements are repeated according to <paramref name="repeats"/>.
        /// Otherwise, if an <paramref name="axis"/> was specified, repetitions are performed along that dimension only. In this case, the array returned
        /// has the same shape as <paramref name="A"/>, except that the working dimension <paramref name="axis"/> is enlarged.</para>
        /// <para>The shape of <paramref name="repeats"/> is ignored. Its values give the counts for each element along the axis <paramref name="axis"/>. 
        /// Values must be numeric, positive integers and are read in row-major order. If <paramref name="repeats"/> has exactly one element 
        /// all elements along the working dimension of <paramref name="A"/> are repeated by the same number. Alternatively, the length of <paramref name="repeats"/> 
        /// must match A.S[axis] to specify individual repetition counts for each element along the working dimension. Thus, if <paramref name="axis"/> is 
        /// null (default) <paramref name="repeats"/> can be a scalar or an array with 'A.S.NumberOfElements == repeats.S.NumberOfElements'.</para>
        /// <para>This function returns a new array and does not alter <paramref name="A"/> or any input parameters.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"> if any of the <paramref name="A"/> or <paramref name="repeats"/> is null</exception>
        /// <exception cref="ArgumentException">
        /// if <paramref name="axis"/> points to a virtual dimension; 
        /// if <paramref name="repeats"/> is not a numeric array, is of a shape which is not broadcastable to the length of the working dimension or 
        /// contains elements which are not convertible to positive integer values; 
        /// </exception>
        /// <seealso cref="repeat{T1, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}, long, uint?)"/>
        public static unsafe RetT repeat<T1, LocalT, InT, OutT, RetT, StorageT, IndT>(this ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> A,
                                         BaseArray<IndT> repeats, uint? axis = null)
            where IndT : struct, IConvertible
            where T1 : struct
            where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {

            return numpyInternal.repeat(A, repeats, axis);
        }
        /// <summary>
        /// [numpy API] Repeat elements along a flattened array or a specific axis.
        /// </summary>
        /// <typeparam name="T1">Element type of <paramref name="A"/>.</typeparam>
        /// <typeparam name="LocalT">(subtype of <paramref name="A"/>)</typeparam>
        /// <typeparam name="InT">(subtype of <paramref name="A"/>)</typeparam>
        /// <typeparam name="OutT">(subtype of <paramref name="A"/>)</typeparam>
        /// <typeparam name="RetT">(subtype of <paramref name="A"/>)</typeparam>
        /// <typeparam name="StorageT">(subtype of <paramref name="A"/>)</typeparam>
        /// <param name="A">The array storing the elements to be repeated.</param>
        /// <param name="repeats">Number of repetitions for all elements (scalar value).</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) flatten A and repeat all values along dimension #0.</param>
        /// <remarks>
        /// <para>This function repeats elements of <paramref name="A"/> along a single dimension. By default, where no <paramref name="axis"/> 
        /// is defined <paramref name="A"/> is reshaped to a vector in row-major order and all elements are repeated according to <paramref name="repeats"/>.
        /// Otherwise, if an <paramref name="axis"/> was specified, repetitions are performed along that dimension only. In this case, the array returned
        /// has the same shape as <paramref name="A"/>, except that the working dimension <paramref name="axis"/> is modified.</para>
        /// <para>The value of <paramref name="repeats"/> gives the counts for each element along the axis <paramref name="axis"/>. 
        /// This value must be a positive integer.</para>
        /// <para>This function returns a new array and does not alter <paramref name="A"/> or any input parameters.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"> if <paramref name="A"/> is null</exception>
        /// <exception cref="ArgumentException">
        /// if <paramref name="axis"/> points to a virtual dimension; 
        /// or if <paramref name="repeats"/> is negative. 
        /// </exception>
        /// <seealso cref="repeat{T1, LocalT, InT, OutT, RetT, StorageT, IndT}(ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}, BaseArray{IndT}, uint?)"/>
        public static unsafe RetT repeat<T1, LocalT, InT, OutT, RetT, StorageT>(this ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> A,
                                         long repeats, uint? axis = null)
            where T1 : struct
            where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {

            return numpyInternal.repeat(A, Storage<long>.Create(repeats, 1).RetArray, axis);
        }
        #endregion

        #region sort

        #region HYCALPER LOOPSTART
        /*!HC:TYPELIST:
        <hycalper>
        <type>
        <source locate="here">
            double
        </source>
        <destination>byte</destination>
        <destination>sbyte</destination>
        <destination>ushort</destination>
        <destination>short</destination>
        <destination>uint</destination>
        <destination>int</destination>
        <destination>ulong</destination>
        <destination>long</destination>
        <destination>float</destination>
        <destination>complex</destination>
        <destination>fcomplex</destination>
        </type>
        <type>
        <source locate="here">
            Double
        </source>
        <destination>Byte</destination>
        <destination>SByte</destination>
        <destination>UInt16</destination>
        <destination>Int16</destination>
        <destination>UInt32</destination>
        <destination>Int32</destination>
        <destination>UInt64</destination>
        <destination>Int64</destination>
        <destination>Single</destination>
        <destination>Complex</destination>
        <destination>FComplex</destination>
        </type>
        </hycalper>
        */

        /// <summary>
        /// [numpy API] Sort elements of <paramref name="A"/> inplace.
        /// </summary>
        /// <param name="A">Array to get sorted.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (-1) sorts the last dimension.</param>
        /// <param name="descending">[Optional] Sort in descending order. Default: (false) sort in ascending order.</param>
        /// <remarks><para>The sort is performed in parallel, using the quicksort algorithm.</para>
        /// <para>See <see cref="MathInternal.sort(BaseArray{double}, int, bool)"/> for details.</para>
        /// <para>This function works on the elements of <paramref name="A"/> directly. No copy will be made. Therefore, 
        /// it is only available for mutable arrays.</para></remarks>
        /// <seealso cref="MathInternal.sort(BaseArray{double}, int, bool)"/>
        public static unsafe void sort(this Mutable<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A, int axis = -1, bool descending = false) {
            
            lock (A.SynchObj) {
                var storage = A.Storage; 
                if (storage.S.NumberOfDimensions == 0 || storage.S.NumberOfElements == 0) {
                    return;
                }
                while (axis < 0) {
                    axis += (int)storage.S.NumberOfDimensions;
                }
                
                if (storage.S[axis] == 1) {
                    return; 
                }

                A.Detach(); 

                if (descending) {
                    ILNumerics.Core.Functions.Builtin.InnerLoops.SortDesc.Double.Instance.operate(A, axis);
                } else {
                    ILNumerics.Core.Functions.Builtin.InnerLoops.SortAsc.Double.Instance.operate(A, axis);
                }
            }
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        /// <summary>
        /// [numpy API] Sort elements of <paramref name="A"/> inplace.
        /// </summary>
        /// <param name="A">Array to get sorted.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (-1) sorts the last dimension.</param>
        /// <param name="descending">[Optional] Sort in descending order. Default: (false) sort in ascending order.</param>
        /// <remarks><para>The sort is performed in parallel, using the quicksort algorithm.</para>
        /// <para>See <see cref="MathInternal.sort(BaseArray{fcomplex}, int, bool)"/> for details.</para>
        /// <para>This function works on the elements of <paramref name="A"/> directly. No copy will be made. Therefore, 
        /// it is only available for mutable arrays.</para></remarks>
        /// <seealso cref="MathInternal.sort(BaseArray{fcomplex}, int, bool)"/>
        public static unsafe void sort(this Mutable<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A, int axis = -1, bool descending = false) {
            
            lock (A.SynchObj) {
                var storage = A.Storage; 
                if (storage.S.NumberOfDimensions == 0 || storage.S.NumberOfElements == 0) {
                    return;
                }
                while (axis < 0) {
                    axis += (int)storage.S.NumberOfDimensions;
                }
                
                if (storage.S[axis] == 1) {
                    return; 
                }

                A.Detach(); 

                if (descending) {
                    ILNumerics.Core.Functions.Builtin.InnerLoops.SortDesc.FComplex.Instance.operate(A, axis);
                } else {
                    ILNumerics.Core.Functions.Builtin.InnerLoops.SortAsc.FComplex.Instance.operate(A, axis);
                }
            }
        }
       

        /// <summary>
        /// [numpy API] Sort elements of <paramref name="A"/> inplace.
        /// </summary>
        /// <param name="A">Array to get sorted.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (-1) sorts the last dimension.</param>
        /// <param name="descending">[Optional] Sort in descending order. Default: (false) sort in ascending order.</param>
        /// <remarks><para>The sort is performed in parallel, using the quicksort algorithm.</para>
        /// <para>See <see cref="MathInternal.sort(BaseArray{complex}, int, bool)"/> for details.</para>
        /// <para>This function works on the elements of <paramref name="A"/> directly. No copy will be made. Therefore, 
        /// it is only available for mutable arrays.</para></remarks>
        /// <seealso cref="MathInternal.sort(BaseArray{complex}, int, bool)"/>
        public static unsafe void sort(this Mutable<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A, int axis = -1, bool descending = false) {
            
            lock (A.SynchObj) {
                var storage = A.Storage; 
                if (storage.S.NumberOfDimensions == 0 || storage.S.NumberOfElements == 0) {
                    return;
                }
                while (axis < 0) {
                    axis += (int)storage.S.NumberOfDimensions;
                }
                
                if (storage.S[axis] == 1) {
                    return; 
                }

                A.Detach(); 

                if (descending) {
                    ILNumerics.Core.Functions.Builtin.InnerLoops.SortDesc.Complex.Instance.operate(A, axis);
                } else {
                    ILNumerics.Core.Functions.Builtin.InnerLoops.SortAsc.Complex.Instance.operate(A, axis);
                }
            }
        }
       

        /// <summary>
        /// [numpy API] Sort elements of <paramref name="A"/> inplace.
        /// </summary>
        /// <param name="A">Array to get sorted.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (-1) sorts the last dimension.</param>
        /// <param name="descending">[Optional] Sort in descending order. Default: (false) sort in ascending order.</param>
        /// <remarks><para>The sort is performed in parallel, using the quicksort algorithm.</para>
        /// <para>See <see cref="MathInternal.sort(BaseArray{float}, int, bool)"/> for details.</para>
        /// <para>This function works on the elements of <paramref name="A"/> directly. No copy will be made. Therefore, 
        /// it is only available for mutable arrays.</para></remarks>
        /// <seealso cref="MathInternal.sort(BaseArray{float}, int, bool)"/>
        public static unsafe void sort(this Mutable<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A, int axis = -1, bool descending = false) {
            
            lock (A.SynchObj) {
                var storage = A.Storage; 
                if (storage.S.NumberOfDimensions == 0 || storage.S.NumberOfElements == 0) {
                    return;
                }
                while (axis < 0) {
                    axis += (int)storage.S.NumberOfDimensions;
                }
                
                if (storage.S[axis] == 1) {
                    return; 
                }

                A.Detach(); 

                if (descending) {
                    ILNumerics.Core.Functions.Builtin.InnerLoops.SortDesc.Single.Instance.operate(A, axis);
                } else {
                    ILNumerics.Core.Functions.Builtin.InnerLoops.SortAsc.Single.Instance.operate(A, axis);
                }
            }
        }
       

        /// <summary>
        /// [numpy API] Sort elements of <paramref name="A"/> inplace.
        /// </summary>
        /// <param name="A">Array to get sorted.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (-1) sorts the last dimension.</param>
        /// <param name="descending">[Optional] Sort in descending order. Default: (false) sort in ascending order.</param>
        /// <remarks><para>The sort is performed in parallel, using the quicksort algorithm.</para>
        /// <para>See <see cref="MathInternal.sort(BaseArray{long}, int, bool)"/> for details.</para>
        /// <para>This function works on the elements of <paramref name="A"/> directly. No copy will be made. Therefore, 
        /// it is only available for mutable arrays.</para></remarks>
        /// <seealso cref="MathInternal.sort(BaseArray{long}, int, bool)"/>
        public static unsafe void sort(this Mutable<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> A, int axis = -1, bool descending = false) {
            
            lock (A.SynchObj) {
                var storage = A.Storage; 
                if (storage.S.NumberOfDimensions == 0 || storage.S.NumberOfElements == 0) {
                    return;
                }
                while (axis < 0) {
                    axis += (int)storage.S.NumberOfDimensions;
                }
                
                if (storage.S[axis] == 1) {
                    return; 
                }

                A.Detach(); 

                if (descending) {
                    ILNumerics.Core.Functions.Builtin.InnerLoops.SortDesc.Int64.Instance.operate(A, axis);
                } else {
                    ILNumerics.Core.Functions.Builtin.InnerLoops.SortAsc.Int64.Instance.operate(A, axis);
                }
            }
        }
       

        /// <summary>
        /// [numpy API] Sort elements of <paramref name="A"/> inplace.
        /// </summary>
        /// <param name="A">Array to get sorted.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (-1) sorts the last dimension.</param>
        /// <param name="descending">[Optional] Sort in descending order. Default: (false) sort in ascending order.</param>
        /// <remarks><para>The sort is performed in parallel, using the quicksort algorithm.</para>
        /// <para>See <see cref="MathInternal.sort(BaseArray{ulong}, int, bool)"/> for details.</para>
        /// <para>This function works on the elements of <paramref name="A"/> directly. No copy will be made. Therefore, 
        /// it is only available for mutable arrays.</para></remarks>
        /// <seealso cref="MathInternal.sort(BaseArray{ulong}, int, bool)"/>
        public static unsafe void sort(this Mutable<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> A, int axis = -1, bool descending = false) {
            
            lock (A.SynchObj) {
                var storage = A.Storage; 
                if (storage.S.NumberOfDimensions == 0 || storage.S.NumberOfElements == 0) {
                    return;
                }
                while (axis < 0) {
                    axis += (int)storage.S.NumberOfDimensions;
                }
                
                if (storage.S[axis] == 1) {
                    return; 
                }

                A.Detach(); 

                if (descending) {
                    ILNumerics.Core.Functions.Builtin.InnerLoops.SortDesc.UInt64.Instance.operate(A, axis);
                } else {
                    ILNumerics.Core.Functions.Builtin.InnerLoops.SortAsc.UInt64.Instance.operate(A, axis);
                }
            }
        }
       

        /// <summary>
        /// [numpy API] Sort elements of <paramref name="A"/> inplace.
        /// </summary>
        /// <param name="A">Array to get sorted.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (-1) sorts the last dimension.</param>
        /// <param name="descending">[Optional] Sort in descending order. Default: (false) sort in ascending order.</param>
        /// <remarks><para>The sort is performed in parallel, using the quicksort algorithm.</para>
        /// <para>See <see cref="MathInternal.sort(BaseArray{int}, int, bool)"/> for details.</para>
        /// <para>This function works on the elements of <paramref name="A"/> directly. No copy will be made. Therefore, 
        /// it is only available for mutable arrays.</para></remarks>
        /// <seealso cref="MathInternal.sort(BaseArray{int}, int, bool)"/>
        public static unsafe void sort(this Mutable<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> A, int axis = -1, bool descending = false) {
            
            lock (A.SynchObj) {
                var storage = A.Storage; 
                if (storage.S.NumberOfDimensions == 0 || storage.S.NumberOfElements == 0) {
                    return;
                }
                while (axis < 0) {
                    axis += (int)storage.S.NumberOfDimensions;
                }
                
                if (storage.S[axis] == 1) {
                    return; 
                }

                A.Detach(); 

                if (descending) {
                    ILNumerics.Core.Functions.Builtin.InnerLoops.SortDesc.Int32.Instance.operate(A, axis);
                } else {
                    ILNumerics.Core.Functions.Builtin.InnerLoops.SortAsc.Int32.Instance.operate(A, axis);
                }
            }
        }
       

        /// <summary>
        /// [numpy API] Sort elements of <paramref name="A"/> inplace.
        /// </summary>
        /// <param name="A">Array to get sorted.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (-1) sorts the last dimension.</param>
        /// <param name="descending">[Optional] Sort in descending order. Default: (false) sort in ascending order.</param>
        /// <remarks><para>The sort is performed in parallel, using the quicksort algorithm.</para>
        /// <para>See <see cref="MathInternal.sort(BaseArray{uint}, int, bool)"/> for details.</para>
        /// <para>This function works on the elements of <paramref name="A"/> directly. No copy will be made. Therefore, 
        /// it is only available for mutable arrays.</para></remarks>
        /// <seealso cref="MathInternal.sort(BaseArray{uint}, int, bool)"/>
        public static unsafe void sort(this Mutable<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> A, int axis = -1, bool descending = false) {
            
            lock (A.SynchObj) {
                var storage = A.Storage; 
                if (storage.S.NumberOfDimensions == 0 || storage.S.NumberOfElements == 0) {
                    return;
                }
                while (axis < 0) {
                    axis += (int)storage.S.NumberOfDimensions;
                }
                
                if (storage.S[axis] == 1) {
                    return; 
                }

                A.Detach(); 

                if (descending) {
                    ILNumerics.Core.Functions.Builtin.InnerLoops.SortDesc.UInt32.Instance.operate(A, axis);
                } else {
                    ILNumerics.Core.Functions.Builtin.InnerLoops.SortAsc.UInt32.Instance.operate(A, axis);
                }
            }
        }
       

        /// <summary>
        /// [numpy API] Sort elements of <paramref name="A"/> inplace.
        /// </summary>
        /// <param name="A">Array to get sorted.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (-1) sorts the last dimension.</param>
        /// <param name="descending">[Optional] Sort in descending order. Default: (false) sort in ascending order.</param>
        /// <remarks><para>The sort is performed in parallel, using the quicksort algorithm.</para>
        /// <para>See <see cref="MathInternal.sort(BaseArray{short}, int, bool)"/> for details.</para>
        /// <para>This function works on the elements of <paramref name="A"/> directly. No copy will be made. Therefore, 
        /// it is only available for mutable arrays.</para></remarks>
        /// <seealso cref="MathInternal.sort(BaseArray{short}, int, bool)"/>
        public static unsafe void sort(this Mutable<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> A, int axis = -1, bool descending = false) {
            
            lock (A.SynchObj) {
                var storage = A.Storage; 
                if (storage.S.NumberOfDimensions == 0 || storage.S.NumberOfElements == 0) {
                    return;
                }
                while (axis < 0) {
                    axis += (int)storage.S.NumberOfDimensions;
                }
                
                if (storage.S[axis] == 1) {
                    return; 
                }

                A.Detach(); 

                if (descending) {
                    ILNumerics.Core.Functions.Builtin.InnerLoops.SortDesc.Int16.Instance.operate(A, axis);
                } else {
                    ILNumerics.Core.Functions.Builtin.InnerLoops.SortAsc.Int16.Instance.operate(A, axis);
                }
            }
        }
       

        /// <summary>
        /// [numpy API] Sort elements of <paramref name="A"/> inplace.
        /// </summary>
        /// <param name="A">Array to get sorted.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (-1) sorts the last dimension.</param>
        /// <param name="descending">[Optional] Sort in descending order. Default: (false) sort in ascending order.</param>
        /// <remarks><para>The sort is performed in parallel, using the quicksort algorithm.</para>
        /// <para>See <see cref="MathInternal.sort(BaseArray{ushort}, int, bool)"/> for details.</para>
        /// <para>This function works on the elements of <paramref name="A"/> directly. No copy will be made. Therefore, 
        /// it is only available for mutable arrays.</para></remarks>
        /// <seealso cref="MathInternal.sort(BaseArray{ushort}, int, bool)"/>
        public static unsafe void sort(this Mutable<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> A, int axis = -1, bool descending = false) {
            
            lock (A.SynchObj) {
                var storage = A.Storage; 
                if (storage.S.NumberOfDimensions == 0 || storage.S.NumberOfElements == 0) {
                    return;
                }
                while (axis < 0) {
                    axis += (int)storage.S.NumberOfDimensions;
                }
                
                if (storage.S[axis] == 1) {
                    return; 
                }

                A.Detach(); 

                if (descending) {
                    ILNumerics.Core.Functions.Builtin.InnerLoops.SortDesc.UInt16.Instance.operate(A, axis);
                } else {
                    ILNumerics.Core.Functions.Builtin.InnerLoops.SortAsc.UInt16.Instance.operate(A, axis);
                }
            }
        }
       

        /// <summary>
        /// [numpy API] Sort elements of <paramref name="A"/> inplace.
        /// </summary>
        /// <param name="A">Array to get sorted.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (-1) sorts the last dimension.</param>
        /// <param name="descending">[Optional] Sort in descending order. Default: (false) sort in ascending order.</param>
        /// <remarks><para>The sort is performed in parallel, using the quicksort algorithm.</para>
        /// <para>See <see cref="MathInternal.sort(BaseArray{sbyte}, int, bool)"/> for details.</para>
        /// <para>This function works on the elements of <paramref name="A"/> directly. No copy will be made. Therefore, 
        /// it is only available for mutable arrays.</para></remarks>
        /// <seealso cref="MathInternal.sort(BaseArray{sbyte}, int, bool)"/>
        public static unsafe void sort(this Mutable<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> A, int axis = -1, bool descending = false) {
            
            lock (A.SynchObj) {
                var storage = A.Storage; 
                if (storage.S.NumberOfDimensions == 0 || storage.S.NumberOfElements == 0) {
                    return;
                }
                while (axis < 0) {
                    axis += (int)storage.S.NumberOfDimensions;
                }
                
                if (storage.S[axis] == 1) {
                    return; 
                }

                A.Detach(); 

                if (descending) {
                    ILNumerics.Core.Functions.Builtin.InnerLoops.SortDesc.SByte.Instance.operate(A, axis);
                } else {
                    ILNumerics.Core.Functions.Builtin.InnerLoops.SortAsc.SByte.Instance.operate(A, axis);
                }
            }
        }
       

        /// <summary>
        /// [numpy API] Sort elements of <paramref name="A"/> inplace.
        /// </summary>
        /// <param name="A">Array to get sorted.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (-1) sorts the last dimension.</param>
        /// <param name="descending">[Optional] Sort in descending order. Default: (false) sort in ascending order.</param>
        /// <remarks><para>The sort is performed in parallel, using the quicksort algorithm.</para>
        /// <para>See <see cref="MathInternal.sort(BaseArray{byte}, int, bool)"/> for details.</para>
        /// <para>This function works on the elements of <paramref name="A"/> directly. No copy will be made. Therefore, 
        /// it is only available for mutable arrays.</para></remarks>
        /// <seealso cref="MathInternal.sort(BaseArray{byte}, int, bool)"/>
        public static unsafe void sort(this Mutable<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> A, int axis = -1, bool descending = false) {
            
            lock (A.SynchObj) {
                var storage = A.Storage; 
                if (storage.S.NumberOfDimensions == 0 || storage.S.NumberOfElements == 0) {
                    return;
                }
                while (axis < 0) {
                    axis += (int)storage.S.NumberOfDimensions;
                }
                
                if (storage.S[axis] == 1) {
                    return; 
                }

                A.Detach(); 

                if (descending) {
                    ILNumerics.Core.Functions.Builtin.InnerLoops.SortDesc.Byte.Instance.operate(A, axis);
                } else {
                    ILNumerics.Core.Functions.Builtin.InnerLoops.SortAsc.Byte.Instance.operate(A, axis);
                }
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

        #endregion

        #region argsort

        #region HYCALPER LOOPSTART
        /*!HC:TYPELIST:
        <hycalper>
        <type>
        <source locate="here">
            double
        </source>
        <destination>byte</destination>
        <destination>sbyte</destination>
        <destination>ushort</destination>
        <destination>short</destination>
        <destination>uint</destination>
        <destination>int</destination>
        <destination>ulong</destination>
        <destination>long</destination>
        <destination>float</destination>
        <destination>complex</destination>
        <destination>fcomplex</destination>
        </type>
        <type>
        <source locate="here">
            Double
        </source>
        <destination>Byte</destination>
        <destination>SByte</destination>
        <destination>UInt16</destination>
        <destination>Int16</destination>
        <destination>UInt32</destination>
        <destination>Int32</destination>
        <destination>UInt64</destination>
        <destination>Int64</destination>
        <destination>Single</destination>
        <destination>Complex</destination>
        <destination>FComplex</destination>
        </type>
        </hycalper>
        */

        /// <summary>
        /// [numpy API] Computes the indices for elements of sorted <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (-1) sorts the last dimension.</param>
        /// <param name="descending">[Optional] Sort in descending order. Default: (false) sort in ascending order.</param>
        /// <param name="sorted">[Optional] Returns the sorted values of <paramref name="A"/> also. Default: (null) sorted values are not returned.</param>
        /// <remarks><para>The sort is performed in parallel, using the quicksort algorithm.</para>
        /// <para>See <see cref="MathInternal.sort(BaseArray{double}, int, bool)"/> for details.</para>
        /// <para>This function internally creates a copy of <paramref name="A"/>. Therefore, the sorted values can be returned 
        /// without significant overhead.</para></remarks>
        /// <seealso cref="MathInternal.sort(BaseArray{double}, int, bool)"/>
        /// <seealso cref="MathInternal.sort(BaseArray{double}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> argsort(this ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A,
            int axis = -1, bool descending = false, OutArray<double> sorted = null) {
            using (Scope.Enter()) {

                using var _1 = ReaderLock.Create(A, out var storage, releaseRetT: false); 
                while (axis < 0) {
                    axis += (int)storage.S.NumberOfDimensions;
                }
                Array<long> indices = 0;
                Array<double> values = MathInternal.sort(storage.AsRetArray(), indices, axis, descending);
                if (!Equals(sorted, null)) {
                    sorted.a = values;
                }
                return indices;
            }
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        /// <summary>
        /// [numpy API] Computes the indices for elements of sorted <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (-1) sorts the last dimension.</param>
        /// <param name="descending">[Optional] Sort in descending order. Default: (false) sort in ascending order.</param>
        /// <param name="sorted">[Optional] Returns the sorted values of <paramref name="A"/> also. Default: (null) sorted values are not returned.</param>
        /// <remarks><para>The sort is performed in parallel, using the quicksort algorithm.</para>
        /// <para>See <see cref="MathInternal.sort(BaseArray{fcomplex}, int, bool)"/> for details.</para>
        /// <para>This function internally creates a copy of <paramref name="A"/>. Therefore, the sorted values can be returned 
        /// without significant overhead.</para></remarks>
        /// <seealso cref="MathInternal.sort(BaseArray{fcomplex}, int, bool)"/>
        /// <seealso cref="MathInternal.sort(BaseArray{fcomplex}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> argsort(this ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A,
            int axis = -1, bool descending = false, OutArray<fcomplex> sorted = null) {
            using (Scope.Enter()) {

                using var _1 = ReaderLock.Create(A, out var storage, releaseRetT: false); 
                while (axis < 0) {
                    axis += (int)storage.S.NumberOfDimensions;
                }
                Array<long> indices = 0;
                Array<fcomplex> values = MathInternal.sort(storage.AsRetArray(), indices, axis, descending);
                if (!Equals(sorted, null)) {
                    sorted.a = values;
                }
                return indices;
            }
        }
       

        /// <summary>
        /// [numpy API] Computes the indices for elements of sorted <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (-1) sorts the last dimension.</param>
        /// <param name="descending">[Optional] Sort in descending order. Default: (false) sort in ascending order.</param>
        /// <param name="sorted">[Optional] Returns the sorted values of <paramref name="A"/> also. Default: (null) sorted values are not returned.</param>
        /// <remarks><para>The sort is performed in parallel, using the quicksort algorithm.</para>
        /// <para>See <see cref="MathInternal.sort(BaseArray{complex}, int, bool)"/> for details.</para>
        /// <para>This function internally creates a copy of <paramref name="A"/>. Therefore, the sorted values can be returned 
        /// without significant overhead.</para></remarks>
        /// <seealso cref="MathInternal.sort(BaseArray{complex}, int, bool)"/>
        /// <seealso cref="MathInternal.sort(BaseArray{complex}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> argsort(this ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A,
            int axis = -1, bool descending = false, OutArray<complex> sorted = null) {
            using (Scope.Enter()) {

                using var _1 = ReaderLock.Create(A, out var storage, releaseRetT: false); 
                while (axis < 0) {
                    axis += (int)storage.S.NumberOfDimensions;
                }
                Array<long> indices = 0;
                Array<complex> values = MathInternal.sort(storage.AsRetArray(), indices, axis, descending);
                if (!Equals(sorted, null)) {
                    sorted.a = values;
                }
                return indices;
            }
        }
       

        /// <summary>
        /// [numpy API] Computes the indices for elements of sorted <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (-1) sorts the last dimension.</param>
        /// <param name="descending">[Optional] Sort in descending order. Default: (false) sort in ascending order.</param>
        /// <param name="sorted">[Optional] Returns the sorted values of <paramref name="A"/> also. Default: (null) sorted values are not returned.</param>
        /// <remarks><para>The sort is performed in parallel, using the quicksort algorithm.</para>
        /// <para>See <see cref="MathInternal.sort(BaseArray{float}, int, bool)"/> for details.</para>
        /// <para>This function internally creates a copy of <paramref name="A"/>. Therefore, the sorted values can be returned 
        /// without significant overhead.</para></remarks>
        /// <seealso cref="MathInternal.sort(BaseArray{float}, int, bool)"/>
        /// <seealso cref="MathInternal.sort(BaseArray{float}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> argsort(this ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A,
            int axis = -1, bool descending = false, OutArray<float> sorted = null) {
            using (Scope.Enter()) {

                using var _1 = ReaderLock.Create(A, out var storage, releaseRetT: false); 
                while (axis < 0) {
                    axis += (int)storage.S.NumberOfDimensions;
                }
                Array<long> indices = 0;
                Array<float> values = MathInternal.sort(storage.AsRetArray(), indices, axis, descending);
                if (!Equals(sorted, null)) {
                    sorted.a = values;
                }
                return indices;
            }
        }
       

        /// <summary>
        /// [numpy API] Computes the indices for elements of sorted <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (-1) sorts the last dimension.</param>
        /// <param name="descending">[Optional] Sort in descending order. Default: (false) sort in ascending order.</param>
        /// <param name="sorted">[Optional] Returns the sorted values of <paramref name="A"/> also. Default: (null) sorted values are not returned.</param>
        /// <remarks><para>The sort is performed in parallel, using the quicksort algorithm.</para>
        /// <para>See <see cref="MathInternal.sort(BaseArray{long}, int, bool)"/> for details.</para>
        /// <para>This function internally creates a copy of <paramref name="A"/>. Therefore, the sorted values can be returned 
        /// without significant overhead.</para></remarks>
        /// <seealso cref="MathInternal.sort(BaseArray{long}, int, bool)"/>
        /// <seealso cref="MathInternal.sort(BaseArray{long}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> argsort(this ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> A,
            int axis = -1, bool descending = false, OutArray<long> sorted = null) {
            using (Scope.Enter()) {

                using var _1 = ReaderLock.Create(A, out var storage, releaseRetT: false); 
                while (axis < 0) {
                    axis += (int)storage.S.NumberOfDimensions;
                }
                Array<long> indices = 0;
                Array<long> values = MathInternal.sort(storage.AsRetArray(), indices, axis, descending);
                if (!Equals(sorted, null)) {
                    sorted.a = values;
                }
                return indices;
            }
        }
       

        /// <summary>
        /// [numpy API] Computes the indices for elements of sorted <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (-1) sorts the last dimension.</param>
        /// <param name="descending">[Optional] Sort in descending order. Default: (false) sort in ascending order.</param>
        /// <param name="sorted">[Optional] Returns the sorted values of <paramref name="A"/> also. Default: (null) sorted values are not returned.</param>
        /// <remarks><para>The sort is performed in parallel, using the quicksort algorithm.</para>
        /// <para>See <see cref="MathInternal.sort(BaseArray{ulong}, int, bool)"/> for details.</para>
        /// <para>This function internally creates a copy of <paramref name="A"/>. Therefore, the sorted values can be returned 
        /// without significant overhead.</para></remarks>
        /// <seealso cref="MathInternal.sort(BaseArray{ulong}, int, bool)"/>
        /// <seealso cref="MathInternal.sort(BaseArray{ulong}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> argsort(this ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> A,
            int axis = -1, bool descending = false, OutArray<ulong> sorted = null) {
            using (Scope.Enter()) {

                using var _1 = ReaderLock.Create(A, out var storage, releaseRetT: false); 
                while (axis < 0) {
                    axis += (int)storage.S.NumberOfDimensions;
                }
                Array<long> indices = 0;
                Array<ulong> values = MathInternal.sort(storage.AsRetArray(), indices, axis, descending);
                if (!Equals(sorted, null)) {
                    sorted.a = values;
                }
                return indices;
            }
        }
       

        /// <summary>
        /// [numpy API] Computes the indices for elements of sorted <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (-1) sorts the last dimension.</param>
        /// <param name="descending">[Optional] Sort in descending order. Default: (false) sort in ascending order.</param>
        /// <param name="sorted">[Optional] Returns the sorted values of <paramref name="A"/> also. Default: (null) sorted values are not returned.</param>
        /// <remarks><para>The sort is performed in parallel, using the quicksort algorithm.</para>
        /// <para>See <see cref="MathInternal.sort(BaseArray{int}, int, bool)"/> for details.</para>
        /// <para>This function internally creates a copy of <paramref name="A"/>. Therefore, the sorted values can be returned 
        /// without significant overhead.</para></remarks>
        /// <seealso cref="MathInternal.sort(BaseArray{int}, int, bool)"/>
        /// <seealso cref="MathInternal.sort(BaseArray{int}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> argsort(this ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> A,
            int axis = -1, bool descending = false, OutArray<int> sorted = null) {
            using (Scope.Enter()) {

                using var _1 = ReaderLock.Create(A, out var storage, releaseRetT: false); 
                while (axis < 0) {
                    axis += (int)storage.S.NumberOfDimensions;
                }
                Array<long> indices = 0;
                Array<int> values = MathInternal.sort(storage.AsRetArray(), indices, axis, descending);
                if (!Equals(sorted, null)) {
                    sorted.a = values;
                }
                return indices;
            }
        }
       

        /// <summary>
        /// [numpy API] Computes the indices for elements of sorted <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (-1) sorts the last dimension.</param>
        /// <param name="descending">[Optional] Sort in descending order. Default: (false) sort in ascending order.</param>
        /// <param name="sorted">[Optional] Returns the sorted values of <paramref name="A"/> also. Default: (null) sorted values are not returned.</param>
        /// <remarks><para>The sort is performed in parallel, using the quicksort algorithm.</para>
        /// <para>See <see cref="MathInternal.sort(BaseArray{uint}, int, bool)"/> for details.</para>
        /// <para>This function internally creates a copy of <paramref name="A"/>. Therefore, the sorted values can be returned 
        /// without significant overhead.</para></remarks>
        /// <seealso cref="MathInternal.sort(BaseArray{uint}, int, bool)"/>
        /// <seealso cref="MathInternal.sort(BaseArray{uint}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> argsort(this ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> A,
            int axis = -1, bool descending = false, OutArray<uint> sorted = null) {
            using (Scope.Enter()) {

                using var _1 = ReaderLock.Create(A, out var storage, releaseRetT: false); 
                while (axis < 0) {
                    axis += (int)storage.S.NumberOfDimensions;
                }
                Array<long> indices = 0;
                Array<uint> values = MathInternal.sort(storage.AsRetArray(), indices, axis, descending);
                if (!Equals(sorted, null)) {
                    sorted.a = values;
                }
                return indices;
            }
        }
       

        /// <summary>
        /// [numpy API] Computes the indices for elements of sorted <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (-1) sorts the last dimension.</param>
        /// <param name="descending">[Optional] Sort in descending order. Default: (false) sort in ascending order.</param>
        /// <param name="sorted">[Optional] Returns the sorted values of <paramref name="A"/> also. Default: (null) sorted values are not returned.</param>
        /// <remarks><para>The sort is performed in parallel, using the quicksort algorithm.</para>
        /// <para>See <see cref="MathInternal.sort(BaseArray{short}, int, bool)"/> for details.</para>
        /// <para>This function internally creates a copy of <paramref name="A"/>. Therefore, the sorted values can be returned 
        /// without significant overhead.</para></remarks>
        /// <seealso cref="MathInternal.sort(BaseArray{short}, int, bool)"/>
        /// <seealso cref="MathInternal.sort(BaseArray{short}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> argsort(this ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> A,
            int axis = -1, bool descending = false, OutArray<short> sorted = null) {
            using (Scope.Enter()) {

                using var _1 = ReaderLock.Create(A, out var storage, releaseRetT: false); 
                while (axis < 0) {
                    axis += (int)storage.S.NumberOfDimensions;
                }
                Array<long> indices = 0;
                Array<short> values = MathInternal.sort(storage.AsRetArray(), indices, axis, descending);
                if (!Equals(sorted, null)) {
                    sorted.a = values;
                }
                return indices;
            }
        }
       

        /// <summary>
        /// [numpy API] Computes the indices for elements of sorted <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (-1) sorts the last dimension.</param>
        /// <param name="descending">[Optional] Sort in descending order. Default: (false) sort in ascending order.</param>
        /// <param name="sorted">[Optional] Returns the sorted values of <paramref name="A"/> also. Default: (null) sorted values are not returned.</param>
        /// <remarks><para>The sort is performed in parallel, using the quicksort algorithm.</para>
        /// <para>See <see cref="MathInternal.sort(BaseArray{ushort}, int, bool)"/> for details.</para>
        /// <para>This function internally creates a copy of <paramref name="A"/>. Therefore, the sorted values can be returned 
        /// without significant overhead.</para></remarks>
        /// <seealso cref="MathInternal.sort(BaseArray{ushort}, int, bool)"/>
        /// <seealso cref="MathInternal.sort(BaseArray{ushort}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> argsort(this ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> A,
            int axis = -1, bool descending = false, OutArray<ushort> sorted = null) {
            using (Scope.Enter()) {

                using var _1 = ReaderLock.Create(A, out var storage, releaseRetT: false); 
                while (axis < 0) {
                    axis += (int)storage.S.NumberOfDimensions;
                }
                Array<long> indices = 0;
                Array<ushort> values = MathInternal.sort(storage.AsRetArray(), indices, axis, descending);
                if (!Equals(sorted, null)) {
                    sorted.a = values;
                }
                return indices;
            }
        }
       

        /// <summary>
        /// [numpy API] Computes the indices for elements of sorted <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (-1) sorts the last dimension.</param>
        /// <param name="descending">[Optional] Sort in descending order. Default: (false) sort in ascending order.</param>
        /// <param name="sorted">[Optional] Returns the sorted values of <paramref name="A"/> also. Default: (null) sorted values are not returned.</param>
        /// <remarks><para>The sort is performed in parallel, using the quicksort algorithm.</para>
        /// <para>See <see cref="MathInternal.sort(BaseArray{sbyte}, int, bool)"/> for details.</para>
        /// <para>This function internally creates a copy of <paramref name="A"/>. Therefore, the sorted values can be returned 
        /// without significant overhead.</para></remarks>
        /// <seealso cref="MathInternal.sort(BaseArray{sbyte}, int, bool)"/>
        /// <seealso cref="MathInternal.sort(BaseArray{sbyte}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> argsort(this ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> A,
            int axis = -1, bool descending = false, OutArray<sbyte> sorted = null) {
            using (Scope.Enter()) {

                using var _1 = ReaderLock.Create(A, out var storage, releaseRetT: false); 
                while (axis < 0) {
                    axis += (int)storage.S.NumberOfDimensions;
                }
                Array<long> indices = 0;
                Array<sbyte> values = MathInternal.sort(storage.AsRetArray(), indices, axis, descending);
                if (!Equals(sorted, null)) {
                    sorted.a = values;
                }
                return indices;
            }
        }
       

        /// <summary>
        /// [numpy API] Computes the indices for elements of sorted <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (-1) sorts the last dimension.</param>
        /// <param name="descending">[Optional] Sort in descending order. Default: (false) sort in ascending order.</param>
        /// <param name="sorted">[Optional] Returns the sorted values of <paramref name="A"/> also. Default: (null) sorted values are not returned.</param>
        /// <remarks><para>The sort is performed in parallel, using the quicksort algorithm.</para>
        /// <para>See <see cref="MathInternal.sort(BaseArray{byte}, int, bool)"/> for details.</para>
        /// <para>This function internally creates a copy of <paramref name="A"/>. Therefore, the sorted values can be returned 
        /// without significant overhead.</para></remarks>
        /// <seealso cref="MathInternal.sort(BaseArray{byte}, int, bool)"/>
        /// <seealso cref="MathInternal.sort(BaseArray{byte}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> argsort(this ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> A,
            int axis = -1, bool descending = false, OutArray<byte> sorted = null) {
            using (Scope.Enter()) {

                using var _1 = ReaderLock.Create(A, out var storage, releaseRetT: false); 
                while (axis < 0) {
                    axis += (int)storage.S.NumberOfDimensions;
                }
                Array<long> indices = 0;
                Array<byte> values = MathInternal.sort(storage.AsRetArray(), indices, axis, descending);
                if (!Equals(sorted, null)) {
                    sorted.a = values;
                }
                return indices;
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

        #endregion

        #region nonzero (requires Cell !)

        #endregion

        #region argmax & nanargmax

        #region HYCALPER LOOPSTART ARGNANMAX_TEMPLATE
        /*!HC:TYPELIST:
        <hycalper>
        <type>
        <source locate="here">
            double
        </source>
        <destination>byte</destination>
        <destination>sbyte</destination>
        <destination>ushort</destination>
        <destination>short</destination>
        <destination>uint</destination>
        <destination>int</destination>
        <destination>ulong</destination>
        <destination>long</destination>
        <destination>float</destination>
        <destination>complex</destination>
        <destination>fcomplex</destination>
        </type>
        <type>
        <source locate="here">
            Double
        </source>
        <destination>Byte</destination>
        <destination>SByte</destination>
        <destination>UInt16</destination>
        <destination>Int16</destination>
        <destination>UInt32</destination>
        <destination>Int32</destination>
        <destination>UInt64</destination>
        <destination>Int64</destination>
        <destination>Single</destination>
        <destination>Complex</destination>
        <destination>FComplex</destination>
        </type>
        <type>
        <source locate="after" endmark=");">
            axisFunc
        </source>
        <destination>MathInternal.max</destination>
        <destination>MathInternal.max</destination>
        <destination>MathInternal.max</destination>
        <destination>MathInternal.max</destination>
        <destination>MathInternal.max</destination>
        <destination>MathInternal.max</destination>
        <destination>MathInternal.max</destination>
        <destination>MathInternal.max</destination>
        <destination>numpyInternal.max</destination>
        <destination>numpyInternal.max</destination>
        <destination>numpyInternal.max</destination>
        </type>
        </hycalper>
        */

        /// <summary>
        /// [numpy API] Computes indices of maximum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum of the whole array.</param>
        /// <param name="values">[Optional] Returns the maximum values found from <paramref name="A"/> also. Default: (null) maximum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmax(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int?, OutArray{double})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmax(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int?, OutArray{double})"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{double}, OutArray{long}, int, bool)"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="nanargmax(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int?, OutArray{double})"/>
        /// <seealso cref="argmin(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int?, OutArray{double})"/>
        public static unsafe Array<long> argmax(this ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A,
            int? axis = null, OutArray<double> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, false, MathInternal.maxall, /*!HC:axisFunc*/ numpyInternal.max);
        }
        /// <summary>
        /// [numpy API] Computes the indices of maximum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum of the whole array.</param>
        /// <param name="values">[Optional] Returns the maximum values found from <paramref name="A"/> also. Default: (null) maximum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmax(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int?, OutArray{double})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmax(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int?, OutArray{double})"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// <para>See <see cref="MathInternal.max(BaseArray{double}, OutArray{long}, int, bool)"/> for details.</para></remarks>
        /// <seealso cref="MathInternal.max(BaseArray{double}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> nanargmax(this ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A,
            int? axis = null, OutArray<double> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, true, MathInternal.maxall, MathInternal.max);
        }

        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        /// <summary>
        /// [numpy API] Computes indices of maximum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum of the whole array.</param>
        /// <param name="values">[Optional] Returns the maximum values found from <paramref name="A"/> also. Default: (null) maximum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmax(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int?, OutArray{fcomplex})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmax(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int?, OutArray{fcomplex})"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{fcomplex}, OutArray{long}, int, bool)"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="nanargmax(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int?, OutArray{fcomplex})"/>
        /// <seealso cref="argmin(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int?, OutArray{fcomplex})"/>
        public static unsafe Array<long> argmax(this ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A,
            int? axis = null, OutArray<fcomplex> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, false, MathInternal.maxall,  numpyInternal.max);
        }
        /// <summary>
        /// [numpy API] Computes the indices of maximum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum of the whole array.</param>
        /// <param name="values">[Optional] Returns the maximum values found from <paramref name="A"/> also. Default: (null) maximum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmax(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int?, OutArray{fcomplex})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmax(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int?, OutArray{fcomplex})"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// <para>See <see cref="MathInternal.max(BaseArray{fcomplex}, OutArray{long}, int, bool)"/> for details.</para></remarks>
        /// <seealso cref="MathInternal.max(BaseArray{fcomplex}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> nanargmax(this ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A,
            int? axis = null, OutArray<fcomplex> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, true, MathInternal.maxall, MathInternal.max);
        }

       

        /// <summary>
        /// [numpy API] Computes indices of maximum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum of the whole array.</param>
        /// <param name="values">[Optional] Returns the maximum values found from <paramref name="A"/> also. Default: (null) maximum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmax(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int?, OutArray{complex})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmax(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int?, OutArray{complex})"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{complex}, OutArray{long}, int, bool)"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="nanargmax(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int?, OutArray{complex})"/>
        /// <seealso cref="argmin(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int?, OutArray{complex})"/>
        public static unsafe Array<long> argmax(this ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A,
            int? axis = null, OutArray<complex> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, false, MathInternal.maxall,  numpyInternal.max);
        }
        /// <summary>
        /// [numpy API] Computes the indices of maximum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum of the whole array.</param>
        /// <param name="values">[Optional] Returns the maximum values found from <paramref name="A"/> also. Default: (null) maximum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmax(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int?, OutArray{complex})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmax(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int?, OutArray{complex})"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// <para>See <see cref="MathInternal.max(BaseArray{complex}, OutArray{long}, int, bool)"/> for details.</para></remarks>
        /// <seealso cref="MathInternal.max(BaseArray{complex}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> nanargmax(this ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A,
            int? axis = null, OutArray<complex> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, true, MathInternal.maxall, MathInternal.max);
        }

       

        /// <summary>
        /// [numpy API] Computes indices of maximum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum of the whole array.</param>
        /// <param name="values">[Optional] Returns the maximum values found from <paramref name="A"/> also. Default: (null) maximum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmax(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int?, OutArray{float})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmax(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int?, OutArray{float})"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{float}, OutArray{long}, int, bool)"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="nanargmax(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int?, OutArray{float})"/>
        /// <seealso cref="argmin(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int?, OutArray{float})"/>
        public static unsafe Array<long> argmax(this ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A,
            int? axis = null, OutArray<float> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, false, MathInternal.maxall,  numpyInternal.max);
        }
        /// <summary>
        /// [numpy API] Computes the indices of maximum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum of the whole array.</param>
        /// <param name="values">[Optional] Returns the maximum values found from <paramref name="A"/> also. Default: (null) maximum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmax(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int?, OutArray{float})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmax(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int?, OutArray{float})"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// <para>See <see cref="MathInternal.max(BaseArray{float}, OutArray{long}, int, bool)"/> for details.</para></remarks>
        /// <seealso cref="MathInternal.max(BaseArray{float}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> nanargmax(this ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A,
            int? axis = null, OutArray<float> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, true, MathInternal.maxall, MathInternal.max);
        }

       

        /// <summary>
        /// [numpy API] Computes indices of maximum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum of the whole array.</param>
        /// <param name="values">[Optional] Returns the maximum values found from <paramref name="A"/> also. Default: (null) maximum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmax(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmax(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int?, OutArray{long})"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{long}, OutArray{long}, int, bool)"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="nanargmax(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int?, OutArray{long})"/>
        /// <seealso cref="argmin(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int?, OutArray{long})"/>
        public static unsafe Array<long> argmax(this ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> A,
            int? axis = null, OutArray<long> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, false, MathInternal.maxall,  MathInternal.max);
        }
        /// <summary>
        /// [numpy API] Computes the indices of maximum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum of the whole array.</param>
        /// <param name="values">[Optional] Returns the maximum values found from <paramref name="A"/> also. Default: (null) maximum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmax(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmax(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int?, OutArray{long})"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// <para>See <see cref="MathInternal.max(BaseArray{long}, OutArray{long}, int, bool)"/> for details.</para></remarks>
        /// <seealso cref="MathInternal.max(BaseArray{long}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> nanargmax(this ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> A,
            int? axis = null, OutArray<long> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, true, MathInternal.maxall, MathInternal.max);
        }

       

        /// <summary>
        /// [numpy API] Computes indices of maximum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum of the whole array.</param>
        /// <param name="values">[Optional] Returns the maximum values found from <paramref name="A"/> also. Default: (null) maximum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmax(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int?, OutArray{ulong})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmax(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int?, OutArray{ulong})"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{ulong}, OutArray{long}, int, bool)"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="nanargmax(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int?, OutArray{ulong})"/>
        /// <seealso cref="argmin(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int?, OutArray{ulong})"/>
        public static unsafe Array<long> argmax(this ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> A,
            int? axis = null, OutArray<ulong> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, false, MathInternal.maxall,  MathInternal.max);
        }
        /// <summary>
        /// [numpy API] Computes the indices of maximum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum of the whole array.</param>
        /// <param name="values">[Optional] Returns the maximum values found from <paramref name="A"/> also. Default: (null) maximum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmax(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int?, OutArray{ulong})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmax(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int?, OutArray{ulong})"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// <para>See <see cref="MathInternal.max(BaseArray{ulong}, OutArray{long}, int, bool)"/> for details.</para></remarks>
        /// <seealso cref="MathInternal.max(BaseArray{ulong}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> nanargmax(this ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> A,
            int? axis = null, OutArray<ulong> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, true, MathInternal.maxall, MathInternal.max);
        }

       

        /// <summary>
        /// [numpy API] Computes indices of maximum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum of the whole array.</param>
        /// <param name="values">[Optional] Returns the maximum values found from <paramref name="A"/> also. Default: (null) maximum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmax(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int?, OutArray{int})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmax(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int?, OutArray{int})"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{int}, OutArray{long}, int, bool)"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="nanargmax(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int?, OutArray{int})"/>
        /// <seealso cref="argmin(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int?, OutArray{int})"/>
        public static unsafe Array<long> argmax(this ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> A,
            int? axis = null, OutArray<int> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, false, MathInternal.maxall,  MathInternal.max);
        }
        /// <summary>
        /// [numpy API] Computes the indices of maximum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum of the whole array.</param>
        /// <param name="values">[Optional] Returns the maximum values found from <paramref name="A"/> also. Default: (null) maximum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmax(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int?, OutArray{int})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmax(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int?, OutArray{int})"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// <para>See <see cref="MathInternal.max(BaseArray{int}, OutArray{long}, int, bool)"/> for details.</para></remarks>
        /// <seealso cref="MathInternal.max(BaseArray{int}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> nanargmax(this ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> A,
            int? axis = null, OutArray<int> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, true, MathInternal.maxall, MathInternal.max);
        }

       

        /// <summary>
        /// [numpy API] Computes indices of maximum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum of the whole array.</param>
        /// <param name="values">[Optional] Returns the maximum values found from <paramref name="A"/> also. Default: (null) maximum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmax(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int?, OutArray{uint})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmax(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int?, OutArray{uint})"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{uint}, OutArray{long}, int, bool)"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="nanargmax(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int?, OutArray{uint})"/>
        /// <seealso cref="argmin(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int?, OutArray{uint})"/>
        public static unsafe Array<long> argmax(this ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> A,
            int? axis = null, OutArray<uint> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, false, MathInternal.maxall,  MathInternal.max);
        }
        /// <summary>
        /// [numpy API] Computes the indices of maximum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum of the whole array.</param>
        /// <param name="values">[Optional] Returns the maximum values found from <paramref name="A"/> also. Default: (null) maximum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmax(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int?, OutArray{uint})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmax(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int?, OutArray{uint})"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// <para>See <see cref="MathInternal.max(BaseArray{uint}, OutArray{long}, int, bool)"/> for details.</para></remarks>
        /// <seealso cref="MathInternal.max(BaseArray{uint}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> nanargmax(this ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> A,
            int? axis = null, OutArray<uint> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, true, MathInternal.maxall, MathInternal.max);
        }

       

        /// <summary>
        /// [numpy API] Computes indices of maximum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum of the whole array.</param>
        /// <param name="values">[Optional] Returns the maximum values found from <paramref name="A"/> also. Default: (null) maximum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmax(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int?, OutArray{short})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmax(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int?, OutArray{short})"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{short}, OutArray{long}, int, bool)"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="nanargmax(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int?, OutArray{short})"/>
        /// <seealso cref="argmin(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int?, OutArray{short})"/>
        public static unsafe Array<long> argmax(this ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> A,
            int? axis = null, OutArray<short> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, false, MathInternal.maxall,  MathInternal.max);
        }
        /// <summary>
        /// [numpy API] Computes the indices of maximum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum of the whole array.</param>
        /// <param name="values">[Optional] Returns the maximum values found from <paramref name="A"/> also. Default: (null) maximum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmax(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int?, OutArray{short})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmax(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int?, OutArray{short})"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// <para>See <see cref="MathInternal.max(BaseArray{short}, OutArray{long}, int, bool)"/> for details.</para></remarks>
        /// <seealso cref="MathInternal.max(BaseArray{short}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> nanargmax(this ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> A,
            int? axis = null, OutArray<short> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, true, MathInternal.maxall, MathInternal.max);
        }

       

        /// <summary>
        /// [numpy API] Computes indices of maximum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum of the whole array.</param>
        /// <param name="values">[Optional] Returns the maximum values found from <paramref name="A"/> also. Default: (null) maximum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmax(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int?, OutArray{ushort})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmax(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int?, OutArray{ushort})"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{ushort}, OutArray{long}, int, bool)"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="nanargmax(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int?, OutArray{ushort})"/>
        /// <seealso cref="argmin(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int?, OutArray{ushort})"/>
        public static unsafe Array<long> argmax(this ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> A,
            int? axis = null, OutArray<ushort> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, false, MathInternal.maxall,  MathInternal.max);
        }
        /// <summary>
        /// [numpy API] Computes the indices of maximum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum of the whole array.</param>
        /// <param name="values">[Optional] Returns the maximum values found from <paramref name="A"/> also. Default: (null) maximum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmax(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int?, OutArray{ushort})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmax(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int?, OutArray{ushort})"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// <para>See <see cref="MathInternal.max(BaseArray{ushort}, OutArray{long}, int, bool)"/> for details.</para></remarks>
        /// <seealso cref="MathInternal.max(BaseArray{ushort}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> nanargmax(this ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> A,
            int? axis = null, OutArray<ushort> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, true, MathInternal.maxall, MathInternal.max);
        }

       

        /// <summary>
        /// [numpy API] Computes indices of maximum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum of the whole array.</param>
        /// <param name="values">[Optional] Returns the maximum values found from <paramref name="A"/> also. Default: (null) maximum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmax(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int?, OutArray{sbyte})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmax(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int?, OutArray{sbyte})"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{sbyte}, OutArray{long}, int, bool)"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="nanargmax(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int?, OutArray{sbyte})"/>
        /// <seealso cref="argmin(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int?, OutArray{sbyte})"/>
        public static unsafe Array<long> argmax(this ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> A,
            int? axis = null, OutArray<sbyte> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, false, MathInternal.maxall,  MathInternal.max);
        }
        /// <summary>
        /// [numpy API] Computes the indices of maximum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum of the whole array.</param>
        /// <param name="values">[Optional] Returns the maximum values found from <paramref name="A"/> also. Default: (null) maximum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmax(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int?, OutArray{sbyte})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmax(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int?, OutArray{sbyte})"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// <para>See <see cref="MathInternal.max(BaseArray{sbyte}, OutArray{long}, int, bool)"/> for details.</para></remarks>
        /// <seealso cref="MathInternal.max(BaseArray{sbyte}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> nanargmax(this ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> A,
            int? axis = null, OutArray<sbyte> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, true, MathInternal.maxall, MathInternal.max);
        }

       

        /// <summary>
        /// [numpy API] Computes indices of maximum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum of the whole array.</param>
        /// <param name="values">[Optional] Returns the maximum values found from <paramref name="A"/> also. Default: (null) maximum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmax(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int?, OutArray{byte})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmax(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int?, OutArray{byte})"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{byte}, OutArray{long}, int, bool)"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="nanargmax(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int?, OutArray{byte})"/>
        /// <seealso cref="argmin(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int?, OutArray{byte})"/>
        public static unsafe Array<long> argmax(this ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> A,
            int? axis = null, OutArray<byte> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, false, MathInternal.maxall,  MathInternal.max);
        }
        /// <summary>
        /// [numpy API] Computes the indices of maximum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum of the whole array.</param>
        /// <param name="values">[Optional] Returns the maximum values found from <paramref name="A"/> also. Default: (null) maximum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmax(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int?, OutArray{byte})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmax(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int?, OutArray{byte})"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// <para>See <see cref="MathInternal.max(BaseArray{byte}, OutArray{long}, int, bool)"/> for details.</para></remarks>
        /// <seealso cref="MathInternal.max(BaseArray{byte}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> nanargmax(this ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> A,
            int? axis = null, OutArray<byte> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, true, MathInternal.maxall, MathInternal.max);
        }


#endregion HYCALPER AUTO GENERATED CODE

        #endregion argmax

        #region armgin & nanargmin

        #region HYCALPER LOOPSTART ARGNANMAX_TEMPLATE@Functions\ExtensionMethods\ConcreteArrayNumpyMethods.cs

        /*!HC:TYPELIST:
        <hycalper>
        <type>
        <source locate="here">
            double
        </source>
        <destination>byte</destination>
        <destination>sbyte</destination>
        <destination>ushort</destination>
        <destination>short</destination>
        <destination>uint</destination>
        <destination>int</destination>
        <destination>ulong</destination>
        <destination>long</destination>
        <destination>float</destination>
        <destination>complex</destination>
        <destination>fcomplex</destination>
        <destination>double</destination>
        </type>
        <type>
        <source locate="here">
            Double
        </source>
        <destination>Byte</destination>
        <destination>SByte</destination>
        <destination>UInt16</destination>
        <destination>Int16</destination>
        <destination>UInt32</destination>
        <destination>Int32</destination>
        <destination>UInt64</destination>
        <destination>Int64</destination>
        <destination>Single</destination>
        <destination>Complex</destination>
        <destination>FComplex</destination>
        <destination>Double</destination>
        </type>
        <type>
        <source locate="here">
            max
        </source>
        <destination>min</destination>
        <destination>min</destination>
        <destination>min</destination>
        <destination>min</destination>
        <destination>min</destination>
        <destination>min</destination>
        <destination>min</destination>
        <destination>min</destination>
        <destination>min</destination>
        <destination>min</destination>
        <destination>min</destination>
        <destination>min</destination>
        </type>
        <type>
        <source locate="after" endmark=");">
            axisFunc
        </source>
        <destination>MathInternal.min</destination>
        <destination>MathInternal.min</destination>
        <destination>MathInternal.min</destination>
        <destination>MathInternal.min</destination>
        <destination>MathInternal.min</destination>
        <destination>MathInternal.min</destination>
        <destination>MathInternal.min</destination>
        <destination>MathInternal.min</destination>
        <destination>numpyInternal.min</destination>
        <destination>numpyInternal.min</destination>
        <destination>numpyInternal.min</destination>
        <destination>numpyInternal.min</destination>
        </type>
        </hycalper>
        */

        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        /// <summary>
        /// [numpy API] Computes indices of minimum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum of the whole array.</param>
        /// <param name="values">[Optional] Returns the minimum values found from <paramref name="A"/> also. Default: (null) minimum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmin(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int?, OutArray{double})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmin(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int?, OutArray{double})"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{double}, OutArray{long}, int, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="nanargmin(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int?, OutArray{double})"/>
        /// <seealso cref="argmin(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int?, OutArray{double})"/>
        public static unsafe Array<long> argmin(this ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A,
            int? axis = null, OutArray<double> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, false, MathInternal.minall,  numpyInternal.min);
        }
        /// <summary>
        /// [numpy API] Computes the indices of minimum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum of the whole array.</param>
        /// <param name="values">[Optional] Returns the minimum values found from <paramref name="A"/> also. Default: (null) minimum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmin(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int?, OutArray{double})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmin(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int?, OutArray{double})"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// <para>See <see cref="MathInternal.min(BaseArray{double}, OutArray{long}, int, bool)"/> for details.</para></remarks>
        /// <seealso cref="MathInternal.min(BaseArray{double}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> nanargmin(this ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A,
            int? axis = null, OutArray<double> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, true, MathInternal.minall, MathInternal.min);
        }

       

        /// <summary>
        /// [numpy API] Computes indices of minimum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum of the whole array.</param>
        /// <param name="values">[Optional] Returns the minimum values found from <paramref name="A"/> also. Default: (null) minimum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmin(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int?, OutArray{fcomplex})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmin(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int?, OutArray{fcomplex})"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{fcomplex}, OutArray{long}, int, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="nanargmin(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int?, OutArray{fcomplex})"/>
        /// <seealso cref="argmin(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int?, OutArray{fcomplex})"/>
        public static unsafe Array<long> argmin(this ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A,
            int? axis = null, OutArray<fcomplex> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, false, MathInternal.minall,  numpyInternal.min);
        }
        /// <summary>
        /// [numpy API] Computes the indices of minimum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum of the whole array.</param>
        /// <param name="values">[Optional] Returns the minimum values found from <paramref name="A"/> also. Default: (null) minimum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmin(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int?, OutArray{fcomplex})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmin(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int?, OutArray{fcomplex})"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// <para>See <see cref="MathInternal.min(BaseArray{fcomplex}, OutArray{long}, int, bool)"/> for details.</para></remarks>
        /// <seealso cref="MathInternal.min(BaseArray{fcomplex}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> nanargmin(this ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A,
            int? axis = null, OutArray<fcomplex> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, true, MathInternal.minall, MathInternal.min);
        }

       

        /// <summary>
        /// [numpy API] Computes indices of minimum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum of the whole array.</param>
        /// <param name="values">[Optional] Returns the minimum values found from <paramref name="A"/> also. Default: (null) minimum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmin(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int?, OutArray{complex})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmin(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int?, OutArray{complex})"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{complex}, OutArray{long}, int, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="nanargmin(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int?, OutArray{complex})"/>
        /// <seealso cref="argmin(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int?, OutArray{complex})"/>
        public static unsafe Array<long> argmin(this ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A,
            int? axis = null, OutArray<complex> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, false, MathInternal.minall,  numpyInternal.min);
        }
        /// <summary>
        /// [numpy API] Computes the indices of minimum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum of the whole array.</param>
        /// <param name="values">[Optional] Returns the minimum values found from <paramref name="A"/> also. Default: (null) minimum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmin(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int?, OutArray{complex})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmin(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int?, OutArray{complex})"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// <para>See <see cref="MathInternal.min(BaseArray{complex}, OutArray{long}, int, bool)"/> for details.</para></remarks>
        /// <seealso cref="MathInternal.min(BaseArray{complex}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> nanargmin(this ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A,
            int? axis = null, OutArray<complex> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, true, MathInternal.minall, MathInternal.min);
        }

       

        /// <summary>
        /// [numpy API] Computes indices of minimum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum of the whole array.</param>
        /// <param name="values">[Optional] Returns the minimum values found from <paramref name="A"/> also. Default: (null) minimum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmin(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int?, OutArray{float})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmin(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int?, OutArray{float})"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{float}, OutArray{long}, int, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="nanargmin(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int?, OutArray{float})"/>
        /// <seealso cref="argmin(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int?, OutArray{float})"/>
        public static unsafe Array<long> argmin(this ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A,
            int? axis = null, OutArray<float> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, false, MathInternal.minall,  numpyInternal.min);
        }
        /// <summary>
        /// [numpy API] Computes the indices of minimum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum of the whole array.</param>
        /// <param name="values">[Optional] Returns the minimum values found from <paramref name="A"/> also. Default: (null) minimum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmin(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int?, OutArray{float})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmin(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int?, OutArray{float})"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// <para>See <see cref="MathInternal.min(BaseArray{float}, OutArray{long}, int, bool)"/> for details.</para></remarks>
        /// <seealso cref="MathInternal.min(BaseArray{float}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> nanargmin(this ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A,
            int? axis = null, OutArray<float> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, true, MathInternal.minall, MathInternal.min);
        }

       

        /// <summary>
        /// [numpy API] Computes indices of minimum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum of the whole array.</param>
        /// <param name="values">[Optional] Returns the minimum values found from <paramref name="A"/> also. Default: (null) minimum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmin(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmin(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int?, OutArray{long})"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{long}, OutArray{long}, int, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="nanargmin(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int?, OutArray{long})"/>
        /// <seealso cref="argmin(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int?, OutArray{long})"/>
        public static unsafe Array<long> argmin(this ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> A,
            int? axis = null, OutArray<long> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, false, MathInternal.minall,  MathInternal.min);
        }
        /// <summary>
        /// [numpy API] Computes the indices of minimum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum of the whole array.</param>
        /// <param name="values">[Optional] Returns the minimum values found from <paramref name="A"/> also. Default: (null) minimum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmin(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmin(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int?, OutArray{long})"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// <para>See <see cref="MathInternal.min(BaseArray{long}, OutArray{long}, int, bool)"/> for details.</para></remarks>
        /// <seealso cref="MathInternal.min(BaseArray{long}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> nanargmin(this ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> A,
            int? axis = null, OutArray<long> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, true, MathInternal.minall, MathInternal.min);
        }

       

        /// <summary>
        /// [numpy API] Computes indices of minimum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum of the whole array.</param>
        /// <param name="values">[Optional] Returns the minimum values found from <paramref name="A"/> also. Default: (null) minimum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmin(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int?, OutArray{ulong})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmin(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int?, OutArray{ulong})"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{ulong}, OutArray{long}, int, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="nanargmin(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int?, OutArray{ulong})"/>
        /// <seealso cref="argmin(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int?, OutArray{ulong})"/>
        public static unsafe Array<long> argmin(this ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> A,
            int? axis = null, OutArray<ulong> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, false, MathInternal.minall,  MathInternal.min);
        }
        /// <summary>
        /// [numpy API] Computes the indices of minimum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum of the whole array.</param>
        /// <param name="values">[Optional] Returns the minimum values found from <paramref name="A"/> also. Default: (null) minimum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmin(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int?, OutArray{ulong})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmin(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int?, OutArray{ulong})"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// <para>See <see cref="MathInternal.min(BaseArray{ulong}, OutArray{long}, int, bool)"/> for details.</para></remarks>
        /// <seealso cref="MathInternal.min(BaseArray{ulong}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> nanargmin(this ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> A,
            int? axis = null, OutArray<ulong> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, true, MathInternal.minall, MathInternal.min);
        }

       

        /// <summary>
        /// [numpy API] Computes indices of minimum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum of the whole array.</param>
        /// <param name="values">[Optional] Returns the minimum values found from <paramref name="A"/> also. Default: (null) minimum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmin(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int?, OutArray{int})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmin(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int?, OutArray{int})"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{int}, OutArray{long}, int, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="nanargmin(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int?, OutArray{int})"/>
        /// <seealso cref="argmin(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int?, OutArray{int})"/>
        public static unsafe Array<long> argmin(this ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> A,
            int? axis = null, OutArray<int> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, false, MathInternal.minall,  MathInternal.min);
        }
        /// <summary>
        /// [numpy API] Computes the indices of minimum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum of the whole array.</param>
        /// <param name="values">[Optional] Returns the minimum values found from <paramref name="A"/> also. Default: (null) minimum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmin(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int?, OutArray{int})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmin(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int?, OutArray{int})"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// <para>See <see cref="MathInternal.min(BaseArray{int}, OutArray{long}, int, bool)"/> for details.</para></remarks>
        /// <seealso cref="MathInternal.min(BaseArray{int}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> nanargmin(this ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> A,
            int? axis = null, OutArray<int> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, true, MathInternal.minall, MathInternal.min);
        }

       

        /// <summary>
        /// [numpy API] Computes indices of minimum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum of the whole array.</param>
        /// <param name="values">[Optional] Returns the minimum values found from <paramref name="A"/> also. Default: (null) minimum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmin(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int?, OutArray{uint})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmin(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int?, OutArray{uint})"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{uint}, OutArray{long}, int, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="nanargmin(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int?, OutArray{uint})"/>
        /// <seealso cref="argmin(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int?, OutArray{uint})"/>
        public static unsafe Array<long> argmin(this ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> A,
            int? axis = null, OutArray<uint> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, false, MathInternal.minall,  MathInternal.min);
        }
        /// <summary>
        /// [numpy API] Computes the indices of minimum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum of the whole array.</param>
        /// <param name="values">[Optional] Returns the minimum values found from <paramref name="A"/> also. Default: (null) minimum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmin(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int?, OutArray{uint})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmin(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int?, OutArray{uint})"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// <para>See <see cref="MathInternal.min(BaseArray{uint}, OutArray{long}, int, bool)"/> for details.</para></remarks>
        /// <seealso cref="MathInternal.min(BaseArray{uint}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> nanargmin(this ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> A,
            int? axis = null, OutArray<uint> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, true, MathInternal.minall, MathInternal.min);
        }

       

        /// <summary>
        /// [numpy API] Computes indices of minimum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum of the whole array.</param>
        /// <param name="values">[Optional] Returns the minimum values found from <paramref name="A"/> also. Default: (null) minimum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmin(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int?, OutArray{short})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmin(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int?, OutArray{short})"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{short}, OutArray{long}, int, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="nanargmin(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int?, OutArray{short})"/>
        /// <seealso cref="argmin(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int?, OutArray{short})"/>
        public static unsafe Array<long> argmin(this ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> A,
            int? axis = null, OutArray<short> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, false, MathInternal.minall,  MathInternal.min);
        }
        /// <summary>
        /// [numpy API] Computes the indices of minimum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum of the whole array.</param>
        /// <param name="values">[Optional] Returns the minimum values found from <paramref name="A"/> also. Default: (null) minimum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmin(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int?, OutArray{short})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmin(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int?, OutArray{short})"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// <para>See <see cref="MathInternal.min(BaseArray{short}, OutArray{long}, int, bool)"/> for details.</para></remarks>
        /// <seealso cref="MathInternal.min(BaseArray{short}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> nanargmin(this ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> A,
            int? axis = null, OutArray<short> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, true, MathInternal.minall, MathInternal.min);
        }

       

        /// <summary>
        /// [numpy API] Computes indices of minimum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum of the whole array.</param>
        /// <param name="values">[Optional] Returns the minimum values found from <paramref name="A"/> also. Default: (null) minimum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmin(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int?, OutArray{ushort})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmin(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int?, OutArray{ushort})"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{ushort}, OutArray{long}, int, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="nanargmin(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int?, OutArray{ushort})"/>
        /// <seealso cref="argmin(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int?, OutArray{ushort})"/>
        public static unsafe Array<long> argmin(this ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> A,
            int? axis = null, OutArray<ushort> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, false, MathInternal.minall,  MathInternal.min);
        }
        /// <summary>
        /// [numpy API] Computes the indices of minimum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum of the whole array.</param>
        /// <param name="values">[Optional] Returns the minimum values found from <paramref name="A"/> also. Default: (null) minimum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmin(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int?, OutArray{ushort})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmin(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int?, OutArray{ushort})"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// <para>See <see cref="MathInternal.min(BaseArray{ushort}, OutArray{long}, int, bool)"/> for details.</para></remarks>
        /// <seealso cref="MathInternal.min(BaseArray{ushort}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> nanargmin(this ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> A,
            int? axis = null, OutArray<ushort> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, true, MathInternal.minall, MathInternal.min);
        }

       

        /// <summary>
        /// [numpy API] Computes indices of minimum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum of the whole array.</param>
        /// <param name="values">[Optional] Returns the minimum values found from <paramref name="A"/> also. Default: (null) minimum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmin(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int?, OutArray{sbyte})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmin(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int?, OutArray{sbyte})"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{sbyte}, OutArray{long}, int, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="nanargmin(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int?, OutArray{sbyte})"/>
        /// <seealso cref="argmin(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int?, OutArray{sbyte})"/>
        public static unsafe Array<long> argmin(this ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> A,
            int? axis = null, OutArray<sbyte> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, false, MathInternal.minall,  MathInternal.min);
        }
        /// <summary>
        /// [numpy API] Computes the indices of minimum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum of the whole array.</param>
        /// <param name="values">[Optional] Returns the minimum values found from <paramref name="A"/> also. Default: (null) minimum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmin(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int?, OutArray{sbyte})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmin(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int?, OutArray{sbyte})"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// <para>See <see cref="MathInternal.min(BaseArray{sbyte}, OutArray{long}, int, bool)"/> for details.</para></remarks>
        /// <seealso cref="MathInternal.min(BaseArray{sbyte}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> nanargmin(this ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> A,
            int? axis = null, OutArray<sbyte> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, true, MathInternal.minall, MathInternal.min);
        }

       

        /// <summary>
        /// [numpy API] Computes indices of minimum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum of the whole array.</param>
        /// <param name="values">[Optional] Returns the minimum values found from <paramref name="A"/> also. Default: (null) minimum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmin(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int?, OutArray{byte})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmin(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int?, OutArray{byte})"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{byte}, OutArray{long}, int, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, OutArray{long},StorageOrders,bool)"/>
        /// <seealso cref="nanargmin(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int?, OutArray{byte})"/>
        /// <seealso cref="argmin(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int?, OutArray{byte})"/>
        public static unsafe Array<long> argmin(this ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> A,
            int? axis = null, OutArray<byte> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, false, MathInternal.minall,  MathInternal.min);
        }
        /// <summary>
        /// [numpy API] Computes the indices of minimum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the indices for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum of the whole array.</param>
        /// <param name="values">[Optional] Returns the minimum values found from <paramref name="A"/> also. Default: (null) minimum values are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="argmin(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int?, OutArray{byte})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="nanargmin(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int?, OutArray{byte})"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// <para>See <see cref="MathInternal.min(BaseArray{byte}, OutArray{long}, int, bool)"/> for details.</para></remarks>
        /// <seealso cref="MathInternal.min(BaseArray{byte}, OutArray{long}, int, bool)"/>
        public static unsafe Array<long> nanargmin(this ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> A,
            int? axis = null, OutArray<byte> values = null) {
            return performARGMINMAXInternal(A, ref axis, values, StorageOrders.RowMajor, true, MathInternal.minall, MathInternal.min);
        }


#endregion HYCALPER AUTO GENERATED CODE

        #endregion

        #region max & nanmax

        #region HYCALPER LOOPSTART AMAX_TEMPLATE
        /*!HC:TYPELIST:
        <hycalper>
        <type>
        <source locate="here">
            double
        </source>
        <destination>byte</destination>
        <destination>sbyte</destination>
        <destination>ushort</destination>
        <destination>short</destination>
        <destination>uint</destination>
        <destination>int</destination>
        <destination>ulong</destination>
        <destination>long</destination>
        <destination>float</destination>
        <destination>complex</destination>
        <destination>fcomplex</destination>
        </type>
        <type>
        <source locate="here">
            Double
        </source>
        <destination>Byte</destination>
        <destination>SByte</destination>
        <destination>UInt16</destination>
        <destination>Int16</destination>
        <destination>UInt32</destination>
        <destination>Int32</destination>
        <destination>UInt64</destination>
        <destination>Int64</destination>
        <destination>Single</destination>
        <destination>Complex</destination>
        <destination>FComplex</destination>
        </type>
        <type>
        <source locate="after" endmark=");">
            axisFunc
        </source>
        <destination>MathInternal.max</destination>
        <destination>MathInternal.max</destination>
        <destination>MathInternal.max</destination>
        <destination>MathInternal.max</destination>
        <destination>MathInternal.max</destination>
        <destination>MathInternal.max</destination>
        <destination>MathInternal.max</destination>
        <destination>MathInternal.max</destination>
        <destination>numpyInternal.max</destination>
        <destination>numpyInternal.max</destination>
        <destination>numpyInternal.max</destination>
        </type>
        </hycalper>
        */

        /// <summary>
        /// [numpy API] Computes maximum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the maximum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the maximum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="max(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.max(BaseArray{double}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{double}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmax(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int?, OutArray{double})"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<double> max(this ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, false, MathInternal.maxall, /*!HC:axisFunc*/numpyInternal.max);
        }
        /// <summary>
        /// [numpy API] Computes maximum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the maximum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the maximum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="max(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.max(BaseArray{double}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{double}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmax(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int?, OutArray{double})"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<double> nanmax(this ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, true, MathInternal.maxall, MathInternal.max);
        }

        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        /// <summary>
        /// [numpy API] Computes maximum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the maximum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the maximum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="max(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.max(BaseArray{fcomplex}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{fcomplex}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmax(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int?, OutArray{fcomplex})"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<fcomplex> max(this ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, false, MathInternal.maxall, numpyInternal.max);
        }
        /// <summary>
        /// [numpy API] Computes maximum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the maximum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the maximum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="max(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.max(BaseArray{fcomplex}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{fcomplex}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmax(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int?, OutArray{fcomplex})"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<fcomplex> nanmax(this ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, true, MathInternal.maxall, MathInternal.max);
        }

       

        /// <summary>
        /// [numpy API] Computes maximum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the maximum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the maximum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="max(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.max(BaseArray{complex}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{complex}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmax(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int?, OutArray{complex})"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<complex> max(this ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, false, MathInternal.maxall, numpyInternal.max);
        }
        /// <summary>
        /// [numpy API] Computes maximum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the maximum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the maximum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="max(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.max(BaseArray{complex}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{complex}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmax(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int?, OutArray{complex})"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<complex> nanmax(this ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, true, MathInternal.maxall, MathInternal.max);
        }

       

        /// <summary>
        /// [numpy API] Computes maximum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the maximum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the maximum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="max(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.max(BaseArray{float}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{float}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmax(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int?, OutArray{float})"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<float> max(this ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, false, MathInternal.maxall, numpyInternal.max);
        }
        /// <summary>
        /// [numpy API] Computes maximum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the maximum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the maximum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="max(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.max(BaseArray{float}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{float}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmax(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int?, OutArray{float})"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<float> nanmax(this ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, true, MathInternal.maxall, MathInternal.max);
        }

       

        /// <summary>
        /// [numpy API] Computes maximum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the maximum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the maximum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="max(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.max(BaseArray{long}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{long}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmax(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int?, OutArray{long})"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<long> max(this ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, false, MathInternal.maxall, MathInternal.max);
        }
        /// <summary>
        /// [numpy API] Computes maximum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the maximum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the maximum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="max(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.max(BaseArray{long}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{long}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmax(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int?, OutArray{long})"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<long> nanmax(this ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, true, MathInternal.maxall, MathInternal.max);
        }

       

        /// <summary>
        /// [numpy API] Computes maximum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the maximum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the maximum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="max(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.max(BaseArray{ulong}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{ulong}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmax(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int?, OutArray{ulong})"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<ulong> max(this ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, false, MathInternal.maxall, MathInternal.max);
        }
        /// <summary>
        /// [numpy API] Computes maximum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the maximum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the maximum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="max(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.max(BaseArray{ulong}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{ulong}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmax(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int?, OutArray{ulong})"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<ulong> nanmax(this ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, true, MathInternal.maxall, MathInternal.max);
        }

       

        /// <summary>
        /// [numpy API] Computes maximum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the maximum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the maximum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="max(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.max(BaseArray{int}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{int}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmax(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int?, OutArray{int})"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<int> max(this ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, false, MathInternal.maxall, MathInternal.max);
        }
        /// <summary>
        /// [numpy API] Computes maximum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the maximum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the maximum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="max(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.max(BaseArray{int}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{int}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmax(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int?, OutArray{int})"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<int> nanmax(this ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, true, MathInternal.maxall, MathInternal.max);
        }

       

        /// <summary>
        /// [numpy API] Computes maximum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the maximum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the maximum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="max(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.max(BaseArray{uint}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{uint}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmax(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int?, OutArray{uint})"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<uint> max(this ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, false, MathInternal.maxall, MathInternal.max);
        }
        /// <summary>
        /// [numpy API] Computes maximum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the maximum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the maximum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="max(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.max(BaseArray{uint}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{uint}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmax(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int?, OutArray{uint})"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<uint> nanmax(this ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, true, MathInternal.maxall, MathInternal.max);
        }

       

        /// <summary>
        /// [numpy API] Computes maximum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the maximum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the maximum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="max(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.max(BaseArray{short}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{short}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmax(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int?, OutArray{short})"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<short> max(this ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, false, MathInternal.maxall, MathInternal.max);
        }
        /// <summary>
        /// [numpy API] Computes maximum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the maximum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the maximum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="max(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.max(BaseArray{short}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{short}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmax(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int?, OutArray{short})"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<short> nanmax(this ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, true, MathInternal.maxall, MathInternal.max);
        }

       

        /// <summary>
        /// [numpy API] Computes maximum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the maximum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the maximum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="max(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.max(BaseArray{ushort}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{ushort}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmax(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int?, OutArray{ushort})"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<ushort> max(this ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, false, MathInternal.maxall, MathInternal.max);
        }
        /// <summary>
        /// [numpy API] Computes maximum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the maximum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the maximum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="max(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.max(BaseArray{ushort}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{ushort}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmax(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int?, OutArray{ushort})"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<ushort> nanmax(this ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, true, MathInternal.maxall, MathInternal.max);
        }

       

        /// <summary>
        /// [numpy API] Computes maximum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the maximum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the maximum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="max(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.max(BaseArray{sbyte}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{sbyte}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmax(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int?, OutArray{sbyte})"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<sbyte> max(this ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, false, MathInternal.maxall, MathInternal.max);
        }
        /// <summary>
        /// [numpy API] Computes maximum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the maximum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the maximum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="max(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.max(BaseArray{sbyte}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{sbyte}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmax(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int?, OutArray{sbyte})"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<sbyte> nanmax(this ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, true, MathInternal.maxall, MathInternal.max);
        }

       

        /// <summary>
        /// [numpy API] Computes maximum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the maximum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the maximum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="max(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.max(BaseArray{byte}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{byte}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmax(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int?, OutArray{byte})"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<byte> max(this ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, false, MathInternal.maxall, MathInternal.max);
        }
        /// <summary>
        /// [numpy API] Computes maximum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the maximum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the maximum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the maximum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="max(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.max(BaseArray{byte}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the maximum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{byte}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmax(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int?, OutArray{byte})"/>
        /// <seealso cref="MathInternal.maxall(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<byte> nanmax(this ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, true, MathInternal.maxall, MathInternal.max);
        }


#endregion HYCALPER AUTO GENERATED CODE

        #endregion

        #region min & nanmin

        #region HYCALPER LOOPSTART AMAX_TEMPLATE@Functions\ExtensionMethods\ConcreteArrayNumpyMethods.cs

        /*!HC:TYPELIST:
        <hycalper>
        <type>
        <source locate="here">
            double
        </source>
        <destination>byte</destination>
        <destination>sbyte</destination>
        <destination>ushort</destination>
        <destination>short</destination>
        <destination>uint</destination>
        <destination>int</destination>
        <destination>ulong</destination>
        <destination>long</destination>
        <destination>float</destination>
        <destination>complex</destination>
        <destination>fcomplex</destination>
        <destination>double</destination>
        </type>
        <type>
        <source locate="here">
            Double
        </source>
        <destination>Byte</destination>
        <destination>SByte</destination>
        <destination>UInt16</destination>
        <destination>Int16</destination>
        <destination>UInt32</destination>
        <destination>Int32</destination>
        <destination>UInt64</destination>
        <destination>Int64</destination>
        <destination>Single</destination>
        <destination>Complex</destination>
        <destination>FComplex</destination>
        <destination>Double</destination>
        </type>
        <type>
        <source locate="here">
            max
        </source>
        <destination>min</destination>
        <destination>min</destination>
        <destination>min</destination>
        <destination>min</destination>
        <destination>min</destination>
        <destination>min</destination>
        <destination>min</destination>
        <destination>min</destination>
        <destination>min</destination>
        <destination>min</destination>
        <destination>min</destination>
        <destination>min</destination>
        </type>
        <type>
        <source locate="after" endmark=");">
            axisFunc
        </source>
        <destination>MathInternal.min</destination>
        <destination>MathInternal.min</destination>
        <destination>MathInternal.min</destination>
        <destination>MathInternal.min</destination>
        <destination>MathInternal.min</destination>
        <destination>MathInternal.min</destination>
        <destination>MathInternal.min</destination>
        <destination>MathInternal.min</destination>
        <destination>numpyInternal.min</destination>
        <destination>numpyInternal.min</destination>
        <destination>numpyInternal.min</destination>
        <destination>numpyInternal.min</destination>
        </type>
        </hycalper>
        */

        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        /// <summary>
        /// [numpy API] Computes minimum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the minimum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the minimum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="min(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.min(BaseArray{double}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{double}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmin(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int?, OutArray{double})"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<double> min(this ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, false, MathInternal.minall, numpyInternal.min);
        }
        /// <summary>
        /// [numpy API] Computes minimum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the minimum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the minimum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="min(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.min(BaseArray{double}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{double}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmin(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int?, OutArray{double})"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<double> nanmin(this ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, true, MathInternal.minall, MathInternal.min);
        }

       

        /// <summary>
        /// [numpy API] Computes minimum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the minimum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the minimum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="min(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.min(BaseArray{fcomplex}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{fcomplex}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmin(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int?, OutArray{fcomplex})"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<fcomplex> min(this ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, false, MathInternal.minall, numpyInternal.min);
        }
        /// <summary>
        /// [numpy API] Computes minimum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the minimum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the minimum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="min(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.min(BaseArray{fcomplex}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{fcomplex}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmin(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int?, OutArray{fcomplex})"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<fcomplex> nanmin(this ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, true, MathInternal.minall, MathInternal.min);
        }

       

        /// <summary>
        /// [numpy API] Computes minimum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the minimum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the minimum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="min(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.min(BaseArray{complex}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{complex}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmin(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int?, OutArray{complex})"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<complex> min(this ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, false, MathInternal.minall, numpyInternal.min);
        }
        /// <summary>
        /// [numpy API] Computes minimum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the minimum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the minimum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="min(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.min(BaseArray{complex}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{complex}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmin(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int?, OutArray{complex})"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<complex> nanmin(this ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, true, MathInternal.minall, MathInternal.min);
        }

       

        /// <summary>
        /// [numpy API] Computes minimum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the minimum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the minimum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="min(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.min(BaseArray{float}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{float}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmin(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int?, OutArray{float})"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<float> min(this ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, false, MathInternal.minall, numpyInternal.min);
        }
        /// <summary>
        /// [numpy API] Computes minimum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the minimum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the minimum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="min(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.min(BaseArray{float}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{float}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmin(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int?, OutArray{float})"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<float> nanmin(this ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, true, MathInternal.minall, MathInternal.min);
        }

       

        /// <summary>
        /// [numpy API] Computes minimum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the minimum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the minimum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="min(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.min(BaseArray{long}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{long}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmin(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int?, OutArray{long})"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<long> min(this ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, false, MathInternal.minall, MathInternal.min);
        }
        /// <summary>
        /// [numpy API] Computes minimum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the minimum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the minimum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="min(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.min(BaseArray{long}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{long}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmin(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int?, OutArray{long})"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<long> nanmin(this ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, true, MathInternal.minall, MathInternal.min);
        }

       

        /// <summary>
        /// [numpy API] Computes minimum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the minimum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the minimum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="min(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.min(BaseArray{ulong}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{ulong}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmin(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int?, OutArray{ulong})"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<ulong> min(this ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, false, MathInternal.minall, MathInternal.min);
        }
        /// <summary>
        /// [numpy API] Computes minimum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the minimum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the minimum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="min(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.min(BaseArray{ulong}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{ulong}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmin(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int?, OutArray{ulong})"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<ulong> nanmin(this ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, true, MathInternal.minall, MathInternal.min);
        }

       

        /// <summary>
        /// [numpy API] Computes minimum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the minimum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the minimum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="min(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.min(BaseArray{int}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{int}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmin(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int?, OutArray{int})"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<int> min(this ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, false, MathInternal.minall, MathInternal.min);
        }
        /// <summary>
        /// [numpy API] Computes minimum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the minimum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the minimum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="min(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.min(BaseArray{int}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{int}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmin(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int?, OutArray{int})"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<int> nanmin(this ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, true, MathInternal.minall, MathInternal.min);
        }

       

        /// <summary>
        /// [numpy API] Computes minimum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the minimum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the minimum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="min(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.min(BaseArray{uint}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{uint}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmin(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int?, OutArray{uint})"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<uint> min(this ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, false, MathInternal.minall, MathInternal.min);
        }
        /// <summary>
        /// [numpy API] Computes minimum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the minimum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the minimum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="min(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.min(BaseArray{uint}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{uint}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmin(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int?, OutArray{uint})"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<uint> nanmin(this ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, true, MathInternal.minall, MathInternal.min);
        }

       

        /// <summary>
        /// [numpy API] Computes minimum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the minimum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the minimum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="min(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.min(BaseArray{short}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{short}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmin(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int?, OutArray{short})"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<short> min(this ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, false, MathInternal.minall, MathInternal.min);
        }
        /// <summary>
        /// [numpy API] Computes minimum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the minimum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the minimum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="min(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.min(BaseArray{short}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{short}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmin(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int?, OutArray{short})"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<short> nanmin(this ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, true, MathInternal.minall, MathInternal.min);
        }

       

        /// <summary>
        /// [numpy API] Computes minimum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the minimum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the minimum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="min(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.min(BaseArray{ushort}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{ushort}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmin(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int?, OutArray{ushort})"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<ushort> min(this ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, false, MathInternal.minall, MathInternal.min);
        }
        /// <summary>
        /// [numpy API] Computes minimum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the minimum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the minimum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="min(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.min(BaseArray{ushort}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{ushort}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmin(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int?, OutArray{ushort})"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<ushort> nanmin(this ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, true, MathInternal.minall, MathInternal.min);
        }

       

        /// <summary>
        /// [numpy API] Computes minimum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the minimum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the minimum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="min(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.min(BaseArray{sbyte}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{sbyte}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmin(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int?, OutArray{sbyte})"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<sbyte> min(this ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, false, MathInternal.minall, MathInternal.min);
        }
        /// <summary>
        /// [numpy API] Computes minimum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the minimum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the minimum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="min(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.min(BaseArray{sbyte}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{sbyte}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmin(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int?, OutArray{sbyte})"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<sbyte> nanmin(this ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, true, MathInternal.minall, MathInternal.min);
        }

       

        /// <summary>
        /// [numpy API] Computes minimum elements along the specified dimension of <paramref name="A"/>, recognizing NaNs.
        /// </summary>
        /// <param name="A">Array to compute the minimum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the minimum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="min(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.min(BaseArray{byte}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{byte}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmin(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int?, OutArray{byte})"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<byte> min(this ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, false, MathInternal.minall, MathInternal.min);
        }
        /// <summary>
        /// [numpy API] Computes minimum elements along the specified dimension of <paramref name="A"/>, ignoring NaNs.
        /// </summary>
        /// <param name="A">Array to compute the minimum(s) for. This is not altered.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) finds the minimum within the whole array.</param>
        /// <param name="indices">[Optional] Also return the indices of the minimum values found from <paramref name="A"/>. Default: (null) indices are not returned.</param>
        /// <remarks><para>Negative dimension specifier '<paramref name="axis"/>' will be shifted into the range of valid dimension indices. -1 
        /// corresponds to the last dimension.</para>
        /// <para><see cref="min(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int?, OutArray{long})"/> 
        /// prioritizes NaN values over non-NaN values. I.e.: if any of the elements is NaN the respective result value will be NaN also.  
        /// Conversely, <see cref="MathInternal.min(BaseArray{byte}, OutArray{long}, int, bool)"/>, 
        /// ignores NaN values and gives the minimum among the non-NaN values, if possible. This corresponds to the behavior of ILNumerics version 4, Matlab(R) a.s.f.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{byte}, OutArray{long}, int, bool)"/>
        /// <seealso cref="argmin(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int?, OutArray{byte})"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, OutArray{long}, StorageOrders, bool)"/>
        /// <seealso cref="MathInternal.minall(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, OutArray{long}, StorageOrders, bool)"/>
        public static unsafe Array<byte> nanmin(this ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> A,
            int? axis = null, OutArray<long> indices = null) {
            return performMINMAXInternal(A, ref axis, indices, StorageOrders.RowMajor, true, MathInternal.minall, MathInternal.min);
        }


#endregion HYCALPER AUTO GENERATED CODE

        #endregion

        #region conj
        /// <summary>[numpy API] Conjugates complex elements inplace.</summary>
        /// <param name="A">Mutable input array.</param>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/> inplace.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>Elements of the input array <paramref name="A"/> are directly altered. New memory is only used if the elements 
        /// of <paramref name="A"/> are currently shared with other arrays. Only in this case a copy is created automatically.</para></remarks>
        public static unsafe void conj(this Mutable<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A) {
            MathInternal.conjInplace(A);
        }
        /// <summary>[numpy API] Conjugates complex elements inplace.</summary>
        /// <param name="A">Mutable input array.</param>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/> inplace.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>Elements of the input array <paramref name="A"/> are directly altered. New memory is only used if the elements 
        /// of <paramref name="A"/> are currently shared with other arrays. Only in this case a copy is created automatically.</para></remarks>
        public static unsafe void conj(this Mutable<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A) {
            MathInternal.conjInplace(A);
        }
        #endregion

        #region round
        #region HYCALPER LOOPSTART 
        /*!HC:TYPELIST:
        <hycalper>
            <type>
            <source locate="here">
                double
            </source>
                <destination>float</destination>
                <destination>complex</destination>
                <destination>fcomplex</destination>
            </type>
            <type>
            <source locate="here">
                Double
            </source>
                <destination>Single</destination>
                <destination>Complex</destination>
                <destination>FComplex</destination>
            </type>
        </hycalper>
        */
        /// <summary>[numpy API] Creates an array with elements of <paramref name="A"/>, rounded to the specified number of fractional digits.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="decimals">[Optional] number of fractional digits to round to. Default: (0) round to integers.</param>
        /// <returns>Array of same size as A with elements of A rounded towards the nearest even integer value.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The storage order of the array returned depends on the order of <paramref name="A"/>. If 
        /// <paramref name="A"/>.<see cref="Size.IsContinuous"/> is <c>true</c> the array returned will have the same <see cref="Size.StorageOrder"/> 
        /// as <paramref name="A"/>. Otherwise, the elements of the returned array will be reordered in the order defined by 
        /// <see cref="Settings.DefaultStorageOrder"/> (default: <see cref="StorageOrders.ColumnMajor"/>).</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered. However, if <paramref name="A"/>'s storage is suitable and not 
        /// shared by other arrays its memory will be used for the result and the operation is performed 'inplace'.</para></remarks>
        public static unsafe Array<double> round(this ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A,
            int decimals = 0) {
            return MathInternal.round(A, decimals);
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       
        /// <summary>[numpy API] Creates an array with elements of <paramref name="A"/>, rounded to the specified number of fractional digits.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="decimals">[Optional] number of fractional digits to round to. Default: (0) round to integers.</param>
        /// <returns>Array of same size as A with elements of A rounded towards the nearest even integer value.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The storage order of the array returned depends on the order of <paramref name="A"/>. If 
        /// <paramref name="A"/>.<see cref="Size.IsContinuous"/> is <c>true</c> the array returned will have the same <see cref="Size.StorageOrder"/> 
        /// as <paramref name="A"/>. Otherwise, the elements of the returned array will be reordered in the order defined by 
        /// <see cref="Settings.DefaultStorageOrder"/> (default: <see cref="StorageOrders.ColumnMajor"/>).</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered. However, if <paramref name="A"/>'s storage is suitable and not 
        /// shared by other arrays its memory will be used for the result and the operation is performed 'inplace'.</para></remarks>
        public static unsafe Array<fcomplex> round(this ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A,
            int decimals = 0) {
            return MathInternal.round(A, decimals);
        }
       
        /// <summary>[numpy API] Creates an array with elements of <paramref name="A"/>, rounded to the specified number of fractional digits.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="decimals">[Optional] number of fractional digits to round to. Default: (0) round to integers.</param>
        /// <returns>Array of same size as A with elements of A rounded towards the nearest even integer value.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The storage order of the array returned depends on the order of <paramref name="A"/>. If 
        /// <paramref name="A"/>.<see cref="Size.IsContinuous"/> is <c>true</c> the array returned will have the same <see cref="Size.StorageOrder"/> 
        /// as <paramref name="A"/>. Otherwise, the elements of the returned array will be reordered in the order defined by 
        /// <see cref="Settings.DefaultStorageOrder"/> (default: <see cref="StorageOrders.ColumnMajor"/>).</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered. However, if <paramref name="A"/>'s storage is suitable and not 
        /// shared by other arrays its memory will be used for the result and the operation is performed 'inplace'.</para></remarks>
        public static unsafe Array<complex> round(this ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A,
            int decimals = 0) {
            return MathInternal.round(A, decimals);
        }
       
        /// <summary>[numpy API] Creates an array with elements of <paramref name="A"/>, rounded to the specified number of fractional digits.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="decimals">[Optional] number of fractional digits to round to. Default: (0) round to integers.</param>
        /// <returns>Array of same size as A with elements of A rounded towards the nearest even integer value.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The storage order of the array returned depends on the order of <paramref name="A"/>. If 
        /// <paramref name="A"/>.<see cref="Size.IsContinuous"/> is <c>true</c> the array returned will have the same <see cref="Size.StorageOrder"/> 
        /// as <paramref name="A"/>. Otherwise, the elements of the returned array will be reordered in the order defined by 
        /// <see cref="Settings.DefaultStorageOrder"/> (default: <see cref="StorageOrders.ColumnMajor"/>).</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered. However, if <paramref name="A"/>'s storage is suitable and not 
        /// shared by other arrays its memory will be used for the result and the operation is performed 'inplace'.</para></remarks>
        public static unsafe Array<float> round(this ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A,
            int decimals = 0) {
            return MathInternal.round(A, decimals);
        }

#endregion HYCALPER AUTO GENERATED CODE

        #endregion

        #region sum
        #region HYCALPER LOOPSTART 
        /*!HC:TYPELIST:
        <hycalper>
            <type>
            <source locate="here">
                double
            </source>
                <destination>float</destination>
                <destination>complex</destination>
                <destination>fcomplex</destination>
                <destination>sbyte</destination>
                <destination>byte</destination>
                <destination>short</destination>
                <destination>ushort</destination>
                <destination>int</destination>
                <destination>uint</destination>
                <destination>long</destination>
                <destination>ulong</destination>
            </type>
        </hycalper>
        */

        /// <summary>
        /// [numpy API] Create an array based on the sum of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build sum along. Default: (null) sum all elements.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of summing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Summing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="sum(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, bool)"/>
        public static unsafe Array<double> sum<IndT>(this ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.sum(A, axes, keepdims);
        }
        /// <summary>
        /// [numpy API] Create a scalar array with the sum of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the sum of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="sum(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, bool)"/>
        public static unsafe Array<double> sum(this ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A,
            bool keepdims = false) {
            return numpyInternal.sum<long>(A, null, keepdims);
        }
        /// <summary>
        /// [numpy API] Create an array with the sum of elements of <paramref name="A"/> along a single dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to sum elements of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="sum(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, bool)"/>
        public static unsafe Array<double> sum(this ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.sum<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        /// <summary>
        /// [numpy API] Create an array based on the sum of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build sum along. Default: (null) sum all elements.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of summing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Summing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="sum(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, bool)"/>
        public static unsafe Array<ulong> sum<IndT>(this ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.sum(A, axes, keepdims);
        }
        /// <summary>
        /// [numpy API] Create a scalar array with the sum of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the sum of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="sum(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, bool)"/>
        public static unsafe Array<ulong> sum(this ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> A,
            bool keepdims = false) {
            return numpyInternal.sum<long>(A, null, keepdims);
        }
        /// <summary>
        /// [numpy API] Create an array with the sum of elements of <paramref name="A"/> along a single dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to sum elements of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="sum(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, bool)"/>
        public static unsafe Array<ulong> sum(this ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.sum<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Create an array based on the sum of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build sum along. Default: (null) sum all elements.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of summing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Summing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="sum(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, bool)"/>
        public static unsafe Array<long> sum<IndT>(this ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.sum(A, axes, keepdims);
        }
        /// <summary>
        /// [numpy API] Create a scalar array with the sum of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the sum of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="sum(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, bool)"/>
        public static unsafe Array<long> sum(this ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> A,
            bool keepdims = false) {
            return numpyInternal.sum<long>(A, null, keepdims);
        }
        /// <summary>
        /// [numpy API] Create an array with the sum of elements of <paramref name="A"/> along a single dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to sum elements of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="sum(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, bool)"/>
        public static unsafe Array<long> sum(this ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.sum<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Create an array based on the sum of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build sum along. Default: (null) sum all elements.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of summing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Summing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="sum(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, bool)"/>
        public static unsafe Array<uint> sum<IndT>(this ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.sum(A, axes, keepdims);
        }
        /// <summary>
        /// [numpy API] Create a scalar array with the sum of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the sum of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="sum(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, bool)"/>
        public static unsafe Array<uint> sum(this ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> A,
            bool keepdims = false) {
            return numpyInternal.sum<long>(A, null, keepdims);
        }
        /// <summary>
        /// [numpy API] Create an array with the sum of elements of <paramref name="A"/> along a single dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to sum elements of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="sum(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, bool)"/>
        public static unsafe Array<uint> sum(this ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.sum<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Create an array based on the sum of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build sum along. Default: (null) sum all elements.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of summing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Summing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="sum(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, bool)"/>
        public static unsafe Array<int> sum<IndT>(this ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.sum(A, axes, keepdims);
        }
        /// <summary>
        /// [numpy API] Create a scalar array with the sum of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the sum of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="sum(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, bool)"/>
        public static unsafe Array<int> sum(this ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> A,
            bool keepdims = false) {
            return numpyInternal.sum<long>(A, null, keepdims);
        }
        /// <summary>
        /// [numpy API] Create an array with the sum of elements of <paramref name="A"/> along a single dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to sum elements of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="sum(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, bool)"/>
        public static unsafe Array<int> sum(this ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.sum<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Create an array based on the sum of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build sum along. Default: (null) sum all elements.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of summing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Summing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="sum(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, bool)"/>
        public static unsafe Array<ushort> sum<IndT>(this ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.sum(A, axes, keepdims);
        }
        /// <summary>
        /// [numpy API] Create a scalar array with the sum of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the sum of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="sum(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, bool)"/>
        public static unsafe Array<ushort> sum(this ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> A,
            bool keepdims = false) {
            return numpyInternal.sum<long>(A, null, keepdims);
        }
        /// <summary>
        /// [numpy API] Create an array with the sum of elements of <paramref name="A"/> along a single dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to sum elements of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="sum(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, bool)"/>
        public static unsafe Array<ushort> sum(this ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.sum<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Create an array based on the sum of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build sum along. Default: (null) sum all elements.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of summing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Summing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="sum(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, bool)"/>
        public static unsafe Array<short> sum<IndT>(this ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.sum(A, axes, keepdims);
        }
        /// <summary>
        /// [numpy API] Create a scalar array with the sum of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the sum of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="sum(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, bool)"/>
        public static unsafe Array<short> sum(this ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> A,
            bool keepdims = false) {
            return numpyInternal.sum<long>(A, null, keepdims);
        }
        /// <summary>
        /// [numpy API] Create an array with the sum of elements of <paramref name="A"/> along a single dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to sum elements of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="sum(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, bool)"/>
        public static unsafe Array<short> sum(this ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.sum<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Create an array based on the sum of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build sum along. Default: (null) sum all elements.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of summing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Summing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="sum(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, bool)"/>
        public static unsafe Array<byte> sum<IndT>(this ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.sum(A, axes, keepdims);
        }
        /// <summary>
        /// [numpy API] Create a scalar array with the sum of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the sum of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="sum(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, bool)"/>
        public static unsafe Array<byte> sum(this ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> A,
            bool keepdims = false) {
            return numpyInternal.sum<long>(A, null, keepdims);
        }
        /// <summary>
        /// [numpy API] Create an array with the sum of elements of <paramref name="A"/> along a single dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to sum elements of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="sum(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, bool)"/>
        public static unsafe Array<byte> sum(this ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.sum<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Create an array based on the sum of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build sum along. Default: (null) sum all elements.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of summing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Summing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="sum(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, bool)"/>
        public static unsafe Array<sbyte> sum<IndT>(this ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.sum(A, axes, keepdims);
        }
        /// <summary>
        /// [numpy API] Create a scalar array with the sum of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the sum of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="sum(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, bool)"/>
        public static unsafe Array<sbyte> sum(this ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> A,
            bool keepdims = false) {
            return numpyInternal.sum<long>(A, null, keepdims);
        }
        /// <summary>
        /// [numpy API] Create an array with the sum of elements of <paramref name="A"/> along a single dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to sum elements of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="sum(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, bool)"/>
        public static unsafe Array<sbyte> sum(this ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.sum<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Create an array based on the sum of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build sum along. Default: (null) sum all elements.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of summing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Summing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="sum(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, bool)"/>
        public static unsafe Array<fcomplex> sum<IndT>(this ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.sum(A, axes, keepdims);
        }
        /// <summary>
        /// [numpy API] Create a scalar array with the sum of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the sum of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="sum(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, bool)"/>
        public static unsafe Array<fcomplex> sum(this ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A,
            bool keepdims = false) {
            return numpyInternal.sum<long>(A, null, keepdims);
        }
        /// <summary>
        /// [numpy API] Create an array with the sum of elements of <paramref name="A"/> along a single dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to sum elements of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="sum(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, bool)"/>
        public static unsafe Array<fcomplex> sum(this ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.sum<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Create an array based on the sum of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build sum along. Default: (null) sum all elements.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of summing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Summing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        public static unsafe Array<complex> sum<IndT>(this ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.sum(A, axes, keepdims);
        }
        /// <summary>
        /// [numpy API] Create a scalar array with the sum of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the sum of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        public static unsafe Array<complex> sum(this ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A,
            bool keepdims = false) {
            return numpyInternal.sum<long>(A, null, keepdims);
        }
        /// <summary>
        /// [numpy API] Create an array with the sum of elements of <paramref name="A"/> along a single dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to sum elements of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        public static unsafe Array<complex> sum(this ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.sum<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Create an array based on the sum of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build sum along. Default: (null) sum all elements.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of summing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Summing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        public static unsafe Array<float> sum<IndT>(this ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.sum(A, axes, keepdims);
        }
        /// <summary>
        /// [numpy API] Create a scalar array with the sum of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the sum of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        public static unsafe Array<float> sum(this ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A,
            bool keepdims = false) {
            return numpyInternal.sum<long>(A, null, keepdims);
        }
        /// <summary>
        /// [numpy API] Create an array with the sum of elements of <paramref name="A"/> along a single dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to sum elements of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int, bool)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        public static unsafe Array<float> sum(this ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.sum<int>(A, MathInternal.vector<int>(axis), keepdims);
        }

#endregion HYCALPER AUTO GENERATED CODE

        #endregion

        #region cumsum
        #region HYCALPER LOOPSTART 
        /*!HC:TYPELIST:
        <hycalper>
            <type>
            <source locate="here">
                double
            </source>
                <destination>float</destination>
                <destination>complex</destination>
                <destination>fcomplex</destination>
                <destination>sbyte</destination>
                <destination>byte</destination>
                <destination>short</destination>
                <destination>ushort</destination>
                <destination>int</destination>
                <destination>uint</destination>
                <destination>long</destination>
                <destination>ulong</destination>
            </type>
        </hycalper>
        */

        /// <summary>
        /// [numpy API] Computes the cumulative sum of elements of <paramref name="A"/> along dimension <paramref name="axis"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axis"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">Dimension of <paramref name="A"/> to compute the cumulative sum along. A negative value counts from the last dimension.</param>
        /// <returns>Array of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para><paramref name="axis"/> must lay in the range of existing dimension indices.</para>
        /// <para>Elements of <paramref name="axis"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce empty arrays of the same shape.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axis"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="cumsum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}})"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<double> cumsum<IndT>(this ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A,
            IndT axis) where IndT : struct, IConvertible {
            return numpyInternal.cumsum<IndT>(A, axis);
        }

        /// <summary>
        /// [numpy API] Computes the cumulative sum for all elements of a flattened version of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <returns>Array of the same shape as <see cref="flatten(BaseArray{bool}, StorageOrders)"/> with cumulative sums of all elements..</returns>
        /// <remarks><para>Empty arrays <paramref name="A"/> produce empty arrays of the same shape.</para>
        /// </remarks>
        /// <seealso cref="cumsum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, IndT)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<double> cumsum(this ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A) {
            return numpyInternal.cumsum<long>(A, null);
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        /// <summary>
        /// [numpy API] Computes the cumulative sum of elements of <paramref name="A"/> along dimension <paramref name="axis"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axis"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">Dimension of <paramref name="A"/> to compute the cumulative sum along. A negative value counts from the last dimension.</param>
        /// <returns>Array of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para><paramref name="axis"/> must lay in the range of existing dimension indices.</para>
        /// <para>Elements of <paramref name="axis"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce empty arrays of the same shape.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axis"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="cumsum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}})"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<ulong> cumsum<IndT>(this ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> A,
            IndT axis) where IndT : struct, IConvertible {
            return numpyInternal.cumsum<IndT>(A, axis);
        }

        /// <summary>
        /// [numpy API] Computes the cumulative sum for all elements of a flattened version of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <returns>Array of the same shape as <see cref="flatten(BaseArray{bool}, StorageOrders)"/> with cumulative sums of all elements..</returns>
        /// <remarks><para>Empty arrays <paramref name="A"/> produce empty arrays of the same shape.</para>
        /// </remarks>
        /// <seealso cref="cumsum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, IndT)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<ulong> cumsum(this ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> A) {
            return numpyInternal.cumsum<long>(A, null);
        }
       

        /// <summary>
        /// [numpy API] Computes the cumulative sum of elements of <paramref name="A"/> along dimension <paramref name="axis"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axis"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">Dimension of <paramref name="A"/> to compute the cumulative sum along. A negative value counts from the last dimension.</param>
        /// <returns>Array of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para><paramref name="axis"/> must lay in the range of existing dimension indices.</para>
        /// <para>Elements of <paramref name="axis"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce empty arrays of the same shape.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axis"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="cumsum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}})"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<long> cumsum<IndT>(this ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> A,
            IndT axis) where IndT : struct, IConvertible {
            return numpyInternal.cumsum<IndT>(A, axis);
        }

        /// <summary>
        /// [numpy API] Computes the cumulative sum for all elements of a flattened version of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <returns>Array of the same shape as <see cref="flatten(BaseArray{bool}, StorageOrders)"/> with cumulative sums of all elements..</returns>
        /// <remarks><para>Empty arrays <paramref name="A"/> produce empty arrays of the same shape.</para>
        /// </remarks>
        /// <seealso cref="cumsum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, IndT)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<long> cumsum(this ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> A) {
            return numpyInternal.cumsum<long>(A, null);
        }
       

        /// <summary>
        /// [numpy API] Computes the cumulative sum of elements of <paramref name="A"/> along dimension <paramref name="axis"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axis"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">Dimension of <paramref name="A"/> to compute the cumulative sum along. A negative value counts from the last dimension.</param>
        /// <returns>Array of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para><paramref name="axis"/> must lay in the range of existing dimension indices.</para>
        /// <para>Elements of <paramref name="axis"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce empty arrays of the same shape.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axis"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="cumsum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}})"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<uint> cumsum<IndT>(this ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> A,
            IndT axis) where IndT : struct, IConvertible {
            return numpyInternal.cumsum<IndT>(A, axis);
        }

        /// <summary>
        /// [numpy API] Computes the cumulative sum for all elements of a flattened version of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <returns>Array of the same shape as <see cref="flatten(BaseArray{bool}, StorageOrders)"/> with cumulative sums of all elements..</returns>
        /// <remarks><para>Empty arrays <paramref name="A"/> produce empty arrays of the same shape.</para>
        /// </remarks>
        /// <seealso cref="cumsum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, IndT)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<uint> cumsum(this ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> A) {
            return numpyInternal.cumsum<long>(A, null);
        }
       

        /// <summary>
        /// [numpy API] Computes the cumulative sum of elements of <paramref name="A"/> along dimension <paramref name="axis"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axis"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">Dimension of <paramref name="A"/> to compute the cumulative sum along. A negative value counts from the last dimension.</param>
        /// <returns>Array of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para><paramref name="axis"/> must lay in the range of existing dimension indices.</para>
        /// <para>Elements of <paramref name="axis"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce empty arrays of the same shape.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axis"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="cumsum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}})"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<int> cumsum<IndT>(this ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> A,
            IndT axis) where IndT : struct, IConvertible {
            return numpyInternal.cumsum<IndT>(A, axis);
        }

        /// <summary>
        /// [numpy API] Computes the cumulative sum for all elements of a flattened version of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <returns>Array of the same shape as <see cref="flatten(BaseArray{bool}, StorageOrders)"/> with cumulative sums of all elements..</returns>
        /// <remarks><para>Empty arrays <paramref name="A"/> produce empty arrays of the same shape.</para>
        /// </remarks>
        /// <seealso cref="cumsum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, IndT)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<int> cumsum(this ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> A) {
            return numpyInternal.cumsum<long>(A, null);
        }
       

        /// <summary>
        /// [numpy API] Computes the cumulative sum of elements of <paramref name="A"/> along dimension <paramref name="axis"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axis"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">Dimension of <paramref name="A"/> to compute the cumulative sum along. A negative value counts from the last dimension.</param>
        /// <returns>Array of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para><paramref name="axis"/> must lay in the range of existing dimension indices.</para>
        /// <para>Elements of <paramref name="axis"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce empty arrays of the same shape.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axis"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="cumsum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}})"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<ushort> cumsum<IndT>(this ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> A,
            IndT axis) where IndT : struct, IConvertible {
            return numpyInternal.cumsum<IndT>(A, axis);
        }

        /// <summary>
        /// [numpy API] Computes the cumulative sum for all elements of a flattened version of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <returns>Array of the same shape as <see cref="flatten(BaseArray{bool}, StorageOrders)"/> with cumulative sums of all elements..</returns>
        /// <remarks><para>Empty arrays <paramref name="A"/> produce empty arrays of the same shape.</para>
        /// </remarks>
        /// <seealso cref="cumsum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, IndT)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<ushort> cumsum(this ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> A) {
            return numpyInternal.cumsum<long>(A, null);
        }
       

        /// <summary>
        /// [numpy API] Computes the cumulative sum of elements of <paramref name="A"/> along dimension <paramref name="axis"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axis"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">Dimension of <paramref name="A"/> to compute the cumulative sum along. A negative value counts from the last dimension.</param>
        /// <returns>Array of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para><paramref name="axis"/> must lay in the range of existing dimension indices.</para>
        /// <para>Elements of <paramref name="axis"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce empty arrays of the same shape.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axis"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="cumsum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}})"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<short> cumsum<IndT>(this ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> A,
            IndT axis) where IndT : struct, IConvertible {
            return numpyInternal.cumsum<IndT>(A, axis);
        }

        /// <summary>
        /// [numpy API] Computes the cumulative sum for all elements of a flattened version of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <returns>Array of the same shape as <see cref="flatten(BaseArray{bool}, StorageOrders)"/> with cumulative sums of all elements..</returns>
        /// <remarks><para>Empty arrays <paramref name="A"/> produce empty arrays of the same shape.</para>
        /// </remarks>
        /// <seealso cref="cumsum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, IndT)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<short> cumsum(this ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> A) {
            return numpyInternal.cumsum<long>(A, null);
        }
       

        /// <summary>
        /// [numpy API] Computes the cumulative sum of elements of <paramref name="A"/> along dimension <paramref name="axis"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axis"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">Dimension of <paramref name="A"/> to compute the cumulative sum along. A negative value counts from the last dimension.</param>
        /// <returns>Array of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para><paramref name="axis"/> must lay in the range of existing dimension indices.</para>
        /// <para>Elements of <paramref name="axis"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce empty arrays of the same shape.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axis"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="cumsum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}})"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<byte> cumsum<IndT>(this ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> A,
            IndT axis) where IndT : struct, IConvertible {
            return numpyInternal.cumsum<IndT>(A, axis);
        }

        /// <summary>
        /// [numpy API] Computes the cumulative sum for all elements of a flattened version of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <returns>Array of the same shape as <see cref="flatten(BaseArray{bool}, StorageOrders)"/> with cumulative sums of all elements..</returns>
        /// <remarks><para>Empty arrays <paramref name="A"/> produce empty arrays of the same shape.</para>
        /// </remarks>
        /// <seealso cref="cumsum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, IndT)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<byte> cumsum(this ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> A) {
            return numpyInternal.cumsum<long>(A, null);
        }
       

        /// <summary>
        /// [numpy API] Computes the cumulative sum of elements of <paramref name="A"/> along dimension <paramref name="axis"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axis"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">Dimension of <paramref name="A"/> to compute the cumulative sum along. A negative value counts from the last dimension.</param>
        /// <returns>Array of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para><paramref name="axis"/> must lay in the range of existing dimension indices.</para>
        /// <para>Elements of <paramref name="axis"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce empty arrays of the same shape.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axis"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="cumsum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}})"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<sbyte> cumsum<IndT>(this ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> A,
            IndT axis) where IndT : struct, IConvertible {
            return numpyInternal.cumsum<IndT>(A, axis);
        }

        /// <summary>
        /// [numpy API] Computes the cumulative sum for all elements of a flattened version of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <returns>Array of the same shape as <see cref="flatten(BaseArray{bool}, StorageOrders)"/> with cumulative sums of all elements..</returns>
        /// <remarks><para>Empty arrays <paramref name="A"/> produce empty arrays of the same shape.</para>
        /// </remarks>
        /// <seealso cref="cumsum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, IndT)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<sbyte> cumsum(this ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> A) {
            return numpyInternal.cumsum<long>(A, null);
        }
       

        /// <summary>
        /// [numpy API] Computes the cumulative sum of elements of <paramref name="A"/> along dimension <paramref name="axis"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axis"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">Dimension of <paramref name="A"/> to compute the cumulative sum along. A negative value counts from the last dimension.</param>
        /// <returns>Array of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para><paramref name="axis"/> must lay in the range of existing dimension indices.</para>
        /// <para>Elements of <paramref name="axis"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce empty arrays of the same shape.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axis"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="cumsum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}})"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<fcomplex> cumsum<IndT>(this ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A,
            IndT axis) where IndT : struct, IConvertible {
            return numpyInternal.cumsum<IndT>(A, axis);
        }

        /// <summary>
        /// [numpy API] Computes the cumulative sum for all elements of a flattened version of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <returns>Array of the same shape as <see cref="flatten(BaseArray{bool}, StorageOrders)"/> with cumulative sums of all elements..</returns>
        /// <remarks><para>Empty arrays <paramref name="A"/> produce empty arrays of the same shape.</para>
        /// </remarks>
        /// <seealso cref="cumsum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, IndT)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<fcomplex> cumsum(this ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A) {
            return numpyInternal.cumsum<long>(A, null);
        }
       

        /// <summary>
        /// [numpy API] Computes the cumulative sum of elements of <paramref name="A"/> along dimension <paramref name="axis"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axis"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">Dimension of <paramref name="A"/> to compute the cumulative sum along. A negative value counts from the last dimension.</param>
        /// <returns>Array of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para><paramref name="axis"/> must lay in the range of existing dimension indices.</para>
        /// <para>Elements of <paramref name="axis"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce empty arrays of the same shape.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axis"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="cumsum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}})"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<complex> cumsum<IndT>(this ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A,
            IndT axis) where IndT : struct, IConvertible {
            return numpyInternal.cumsum<IndT>(A, axis);
        }

        /// <summary>
        /// [numpy API] Computes the cumulative sum for all elements of a flattened version of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <returns>Array of the same shape as <see cref="flatten(BaseArray{bool}, StorageOrders)"/> with cumulative sums of all elements..</returns>
        /// <remarks><para>Empty arrays <paramref name="A"/> produce empty arrays of the same shape.</para>
        /// </remarks>
        /// <seealso cref="cumsum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, IndT)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<complex> cumsum(this ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A) {
            return numpyInternal.cumsum<long>(A, null);
        }
       

        /// <summary>
        /// [numpy API] Computes the cumulative sum of elements of <paramref name="A"/> along dimension <paramref name="axis"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axis"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">Dimension of <paramref name="A"/> to compute the cumulative sum along. A negative value counts from the last dimension.</param>
        /// <returns>Array of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para><paramref name="axis"/> must lay in the range of existing dimension indices.</para>
        /// <para>Elements of <paramref name="axis"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce empty arrays of the same shape.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axis"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="cumsum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}})"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<float> cumsum<IndT>(this ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A,
            IndT axis) where IndT : struct, IConvertible {
            return numpyInternal.cumsum<IndT>(A, axis);
        }

        /// <summary>
        /// [numpy API] Computes the cumulative sum for all elements of a flattened version of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <returns>Array of the same shape as <see cref="flatten(BaseArray{bool}, StorageOrders)"/> with cumulative sums of all elements..</returns>
        /// <remarks><para>Empty arrays <paramref name="A"/> produce empty arrays of the same shape.</para>
        /// </remarks>
        /// <seealso cref="cumsum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, IndT)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<float> cumsum(this ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A) {
            return numpyInternal.cumsum<long>(A, null);
        }

#endregion HYCALPER AUTO GENERATED CODE

        #endregion

        #region prod

        #region HYCALPER LOOPSTART 
        /*!HC:TYPELIST:
        <hycalper>
            <type>
            <source locate="here">
                double
            </source>
                <destination>float</destination>
                <destination>complex</destination>
                <destination>fcomplex</destination>
                <destination>sbyte</destination>
                <destination>byte</destination>
                <destination>short</destination>
                <destination>ushort</destination>
                <destination>int</destination>
                <destination>uint</destination>
                <destination>long</destination>
                <destination>ulong</destination>
            </type>
        </hycalper>
        */

        /// <summary>
        /// [numpy API] Creates an array based on the products of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build the products along. Default: (null) factorize all elements.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of factorizing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Factorizing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="prod(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int, bool)"/>
        public static unsafe Array<double> prod<IndT>(this ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.prod(A, axes, keepdims);
        }

        /// <summary>
        /// [numpy API] Creates a scalar array with the product of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the product of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="prod(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int, bool)"/>
        public static unsafe Array<double> prod(this ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A,
            bool keepdims = false) {
            return numpyInternal.prod<long>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Creates an array with the products of elements of <paramref name="A"/> along a specific dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to prod elements of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="prod(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int, bool)"/>
        public static unsafe Array<double> prod(this ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.prod<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        /// <summary>
        /// [numpy API] Creates an array based on the products of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build the products along. Default: (null) factorize all elements.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of factorizing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Factorizing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="prod(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int, bool)"/>
        public static unsafe Array<ulong> prod<IndT>(this ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.prod(A, axes, keepdims);
        }

        /// <summary>
        /// [numpy API] Creates a scalar array with the product of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the product of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="prod(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int, bool)"/>
        public static unsafe Array<ulong> prod(this ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> A,
            bool keepdims = false) {
            return numpyInternal.prod<long>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Creates an array with the products of elements of <paramref name="A"/> along a specific dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to prod elements of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="prod(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int, bool)"/>
        public static unsafe Array<ulong> prod(this ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.prod<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Creates an array based on the products of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build the products along. Default: (null) factorize all elements.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of factorizing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Factorizing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="prod(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int, bool)"/>
        public static unsafe Array<long> prod<IndT>(this ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.prod(A, axes, keepdims);
        }

        /// <summary>
        /// [numpy API] Creates a scalar array with the product of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the product of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="prod(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int, bool)"/>
        public static unsafe Array<long> prod(this ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> A,
            bool keepdims = false) {
            return numpyInternal.prod<long>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Creates an array with the products of elements of <paramref name="A"/> along a specific dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to prod elements of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="prod(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int, bool)"/>
        public static unsafe Array<long> prod(this ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.prod<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Creates an array based on the products of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build the products along. Default: (null) factorize all elements.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of factorizing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Factorizing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="prod(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int, bool)"/>
        public static unsafe Array<uint> prod<IndT>(this ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.prod(A, axes, keepdims);
        }

        /// <summary>
        /// [numpy API] Creates a scalar array with the product of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the product of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="prod(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int, bool)"/>
        public static unsafe Array<uint> prod(this ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> A,
            bool keepdims = false) {
            return numpyInternal.prod<long>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Creates an array with the products of elements of <paramref name="A"/> along a specific dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to prod elements of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="prod(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int, bool)"/>
        public static unsafe Array<uint> prod(this ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.prod<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Creates an array based on the products of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build the products along. Default: (null) factorize all elements.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of factorizing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Factorizing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="prod(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int, bool)"/>
        public static unsafe Array<int> prod<IndT>(this ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.prod(A, axes, keepdims);
        }

        /// <summary>
        /// [numpy API] Creates a scalar array with the product of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the product of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="prod(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int, bool)"/>
        public static unsafe Array<int> prod(this ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> A,
            bool keepdims = false) {
            return numpyInternal.prod<long>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Creates an array with the products of elements of <paramref name="A"/> along a specific dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to prod elements of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="prod(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int, bool)"/>
        public static unsafe Array<int> prod(this ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.prod<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Creates an array based on the products of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build the products along. Default: (null) factorize all elements.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of factorizing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Factorizing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="prod(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int, bool)"/>
        public static unsafe Array<ushort> prod<IndT>(this ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.prod(A, axes, keepdims);
        }

        /// <summary>
        /// [numpy API] Creates a scalar array with the product of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the product of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="prod(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int, bool)"/>
        public static unsafe Array<ushort> prod(this ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> A,
            bool keepdims = false) {
            return numpyInternal.prod<long>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Creates an array with the products of elements of <paramref name="A"/> along a specific dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to prod elements of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="prod(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int, bool)"/>
        public static unsafe Array<ushort> prod(this ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.prod<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Creates an array based on the products of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build the products along. Default: (null) factorize all elements.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of factorizing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Factorizing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="prod(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int, bool)"/>
        public static unsafe Array<short> prod<IndT>(this ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.prod(A, axes, keepdims);
        }

        /// <summary>
        /// [numpy API] Creates a scalar array with the product of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the product of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="prod(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int, bool)"/>
        public static unsafe Array<short> prod(this ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> A,
            bool keepdims = false) {
            return numpyInternal.prod<long>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Creates an array with the products of elements of <paramref name="A"/> along a specific dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to prod elements of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="prod(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int, bool)"/>
        public static unsafe Array<short> prod(this ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.prod<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Creates an array based on the products of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build the products along. Default: (null) factorize all elements.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of factorizing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Factorizing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="prod(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int, bool)"/>
        public static unsafe Array<byte> prod<IndT>(this ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.prod(A, axes, keepdims);
        }

        /// <summary>
        /// [numpy API] Creates a scalar array with the product of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the product of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="prod(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int, bool)"/>
        public static unsafe Array<byte> prod(this ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> A,
            bool keepdims = false) {
            return numpyInternal.prod<long>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Creates an array with the products of elements of <paramref name="A"/> along a specific dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to prod elements of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="prod(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int, bool)"/>
        public static unsafe Array<byte> prod(this ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.prod<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Creates an array based on the products of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build the products along. Default: (null) factorize all elements.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of factorizing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Factorizing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="prod(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int, bool)"/>
        public static unsafe Array<sbyte> prod<IndT>(this ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.prod(A, axes, keepdims);
        }

        /// <summary>
        /// [numpy API] Creates a scalar array with the product of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the product of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="prod(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int, bool)"/>
        public static unsafe Array<sbyte> prod(this ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> A,
            bool keepdims = false) {
            return numpyInternal.prod<long>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Creates an array with the products of elements of <paramref name="A"/> along a specific dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to prod elements of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="prod(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int, bool)"/>
        public static unsafe Array<sbyte> prod(this ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.prod<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Creates an array based on the products of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build the products along. Default: (null) factorize all elements.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of factorizing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Factorizing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="prod(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int, bool)"/>
        public static unsafe Array<fcomplex> prod<IndT>(this ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.prod(A, axes, keepdims);
        }

        /// <summary>
        /// [numpy API] Creates a scalar array with the product of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the product of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="prod(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int, bool)"/>
        public static unsafe Array<fcomplex> prod(this ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A,
            bool keepdims = false) {
            return numpyInternal.prod<long>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Creates an array with the products of elements of <paramref name="A"/> along a specific dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to prod elements of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="prod(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int, bool)"/>
        public static unsafe Array<fcomplex> prod(this ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.prod<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Creates an array based on the products of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build the products along. Default: (null) factorize all elements.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of factorizing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Factorizing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="prod(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int, bool)"/>
        public static unsafe Array<complex> prod<IndT>(this ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.prod(A, axes, keepdims);
        }

        /// <summary>
        /// [numpy API] Creates a scalar array with the product of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the product of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="prod(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int, bool)"/>
        public static unsafe Array<complex> prod(this ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A,
            bool keepdims = false) {
            return numpyInternal.prod<long>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Creates an array with the products of elements of <paramref name="A"/> along a specific dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to prod elements of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="prod(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int, bool)"/>
        public static unsafe Array<complex> prod(this ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.prod<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Creates an array based on the products of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build the products along. Default: (null) factorize all elements.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of factorizing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Factorizing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="prod(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int, bool)"/>
        public static unsafe Array<float> prod<IndT>(this ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.prod(A, axes, keepdims);
        }

        /// <summary>
        /// [numpy API] Creates a scalar array with the product of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the product of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="prod(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int, bool)"/>
        public static unsafe Array<float> prod(this ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A,
            bool keepdims = false) {
            return numpyInternal.prod<long>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Creates an array with the products of elements of <paramref name="A"/> along a specific dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to prod elements of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="prod(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int, bool)"/>
        public static unsafe Array<float> prod(this ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.prod<int>(A, MathInternal.vector<int>(axis), keepdims);
        }

#endregion HYCALPER AUTO GENERATED CODE
        #endregion

        #region cumprod
        #region HYCALPER LOOPSTART 
        /*!HC:TYPELIST:
        <hycalper>
            <type>
            <source locate="here">
                double
            </source>
                <destination>float</destination>
                <destination>complex</destination>
                <destination>fcomplex</destination>
                <destination>sbyte</destination>
                <destination>byte</destination>
                <destination>short</destination>
                <destination>ushort</destination>
                <destination>int</destination>
                <destination>uint</destination>
                <destination>long</destination>
                <destination>ulong</destination>
            </type>
        </hycalper>
        */

        /// <summary>
        /// [numpy API] Computes the cumulative product of elements of <paramref name="A"/> along dimension <paramref name="axis"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axis"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">Dimension of <paramref name="A"/> to compute the cumulative product along. A negative value counts from the last dimension.</param>
        /// <returns>Array of the same shape as A.</returns>
        /// <remarks><para><paramref name="axis"/> must lay in the range of existing dimension indices.</para>
        /// <para><paramref name="axis"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axis"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="cumsum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}})"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<double> cumprod<IndT>(this ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A,
            IndT axis) where IndT : struct, IConvertible {
            return numpyInternal.cumprod<IndT>(A, axis);
        }

        /// <summary>
        /// [numpy API] Computes the cumulative product for all elements of a flattened version of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <returns>Array of the same shape as <see cref="flatten(BaseArray{bool}, StorageOrders)"/> with cumulative products of all elements.</returns>
        /// <seealso cref="cumsum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, IndT)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<double> cumprod(this ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A) {
            return numpyInternal.cumprod<long>(A, null);
        }
        #endregion HYCALPER LOOPEND 
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        /// <summary>
        /// [numpy API] Computes the cumulative product of elements of <paramref name="A"/> along dimension <paramref name="axis"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axis"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">Dimension of <paramref name="A"/> to compute the cumulative product along. A negative value counts from the last dimension.</param>
        /// <returns>Array of the same shape as A.</returns>
        /// <remarks><para><paramref name="axis"/> must lay in the range of existing dimension indices.</para>
        /// <para><paramref name="axis"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axis"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="cumsum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}})"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<ulong> cumprod<IndT>(this ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> A,
            IndT axis) where IndT : struct, IConvertible {
            return numpyInternal.cumprod<IndT>(A, axis);
        }

        /// <summary>
        /// [numpy API] Computes the cumulative product for all elements of a flattened version of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <returns>Array of the same shape as <see cref="flatten(BaseArray{bool}, StorageOrders)"/> with cumulative products of all elements.</returns>
        /// <seealso cref="cumsum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, IndT)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<ulong> cumprod(this ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> A) {
            return numpyInternal.cumprod<long>(A, null);
        }
       

        /// <summary>
        /// [numpy API] Computes the cumulative product of elements of <paramref name="A"/> along dimension <paramref name="axis"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axis"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">Dimension of <paramref name="A"/> to compute the cumulative product along. A negative value counts from the last dimension.</param>
        /// <returns>Array of the same shape as A.</returns>
        /// <remarks><para><paramref name="axis"/> must lay in the range of existing dimension indices.</para>
        /// <para><paramref name="axis"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axis"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="cumsum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}})"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<long> cumprod<IndT>(this ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> A,
            IndT axis) where IndT : struct, IConvertible {
            return numpyInternal.cumprod<IndT>(A, axis);
        }

        /// <summary>
        /// [numpy API] Computes the cumulative product for all elements of a flattened version of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <returns>Array of the same shape as <see cref="flatten(BaseArray{bool}, StorageOrders)"/> with cumulative products of all elements.</returns>
        /// <seealso cref="cumsum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, IndT)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<long> cumprod(this ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> A) {
            return numpyInternal.cumprod<long>(A, null);
        }
       

        /// <summary>
        /// [numpy API] Computes the cumulative product of elements of <paramref name="A"/> along dimension <paramref name="axis"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axis"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">Dimension of <paramref name="A"/> to compute the cumulative product along. A negative value counts from the last dimension.</param>
        /// <returns>Array of the same shape as A.</returns>
        /// <remarks><para><paramref name="axis"/> must lay in the range of existing dimension indices.</para>
        /// <para><paramref name="axis"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axis"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="cumsum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}})"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<uint> cumprod<IndT>(this ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> A,
            IndT axis) where IndT : struct, IConvertible {
            return numpyInternal.cumprod<IndT>(A, axis);
        }

        /// <summary>
        /// [numpy API] Computes the cumulative product for all elements of a flattened version of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <returns>Array of the same shape as <see cref="flatten(BaseArray{bool}, StorageOrders)"/> with cumulative products of all elements.</returns>
        /// <seealso cref="cumsum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, IndT)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<uint> cumprod(this ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> A) {
            return numpyInternal.cumprod<long>(A, null);
        }
       

        /// <summary>
        /// [numpy API] Computes the cumulative product of elements of <paramref name="A"/> along dimension <paramref name="axis"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axis"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">Dimension of <paramref name="A"/> to compute the cumulative product along. A negative value counts from the last dimension.</param>
        /// <returns>Array of the same shape as A.</returns>
        /// <remarks><para><paramref name="axis"/> must lay in the range of existing dimension indices.</para>
        /// <para><paramref name="axis"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axis"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="cumsum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}})"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<int> cumprod<IndT>(this ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> A,
            IndT axis) where IndT : struct, IConvertible {
            return numpyInternal.cumprod<IndT>(A, axis);
        }

        /// <summary>
        /// [numpy API] Computes the cumulative product for all elements of a flattened version of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <returns>Array of the same shape as <see cref="flatten(BaseArray{bool}, StorageOrders)"/> with cumulative products of all elements.</returns>
        /// <seealso cref="cumsum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, IndT)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<int> cumprod(this ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> A) {
            return numpyInternal.cumprod<long>(A, null);
        }
       

        /// <summary>
        /// [numpy API] Computes the cumulative product of elements of <paramref name="A"/> along dimension <paramref name="axis"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axis"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">Dimension of <paramref name="A"/> to compute the cumulative product along. A negative value counts from the last dimension.</param>
        /// <returns>Array of the same shape as A.</returns>
        /// <remarks><para><paramref name="axis"/> must lay in the range of existing dimension indices.</para>
        /// <para><paramref name="axis"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axis"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="cumsum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}})"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<ushort> cumprod<IndT>(this ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> A,
            IndT axis) where IndT : struct, IConvertible {
            return numpyInternal.cumprod<IndT>(A, axis);
        }

        /// <summary>
        /// [numpy API] Computes the cumulative product for all elements of a flattened version of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <returns>Array of the same shape as <see cref="flatten(BaseArray{bool}, StorageOrders)"/> with cumulative products of all elements.</returns>
        /// <seealso cref="cumsum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, IndT)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<ushort> cumprod(this ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> A) {
            return numpyInternal.cumprod<long>(A, null);
        }
       

        /// <summary>
        /// [numpy API] Computes the cumulative product of elements of <paramref name="A"/> along dimension <paramref name="axis"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axis"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">Dimension of <paramref name="A"/> to compute the cumulative product along. A negative value counts from the last dimension.</param>
        /// <returns>Array of the same shape as A.</returns>
        /// <remarks><para><paramref name="axis"/> must lay in the range of existing dimension indices.</para>
        /// <para><paramref name="axis"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axis"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="cumsum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}})"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<short> cumprod<IndT>(this ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> A,
            IndT axis) where IndT : struct, IConvertible {
            return numpyInternal.cumprod<IndT>(A, axis);
        }

        /// <summary>
        /// [numpy API] Computes the cumulative product for all elements of a flattened version of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <returns>Array of the same shape as <see cref="flatten(BaseArray{bool}, StorageOrders)"/> with cumulative products of all elements.</returns>
        /// <seealso cref="cumsum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, IndT)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<short> cumprod(this ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> A) {
            return numpyInternal.cumprod<long>(A, null);
        }
       

        /// <summary>
        /// [numpy API] Computes the cumulative product of elements of <paramref name="A"/> along dimension <paramref name="axis"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axis"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">Dimension of <paramref name="A"/> to compute the cumulative product along. A negative value counts from the last dimension.</param>
        /// <returns>Array of the same shape as A.</returns>
        /// <remarks><para><paramref name="axis"/> must lay in the range of existing dimension indices.</para>
        /// <para><paramref name="axis"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axis"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="cumsum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}})"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<byte> cumprod<IndT>(this ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> A,
            IndT axis) where IndT : struct, IConvertible {
            return numpyInternal.cumprod<IndT>(A, axis);
        }

        /// <summary>
        /// [numpy API] Computes the cumulative product for all elements of a flattened version of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <returns>Array of the same shape as <see cref="flatten(BaseArray{bool}, StorageOrders)"/> with cumulative products of all elements.</returns>
        /// <seealso cref="cumsum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, IndT)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<byte> cumprod(this ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> A) {
            return numpyInternal.cumprod<long>(A, null);
        }
       

        /// <summary>
        /// [numpy API] Computes the cumulative product of elements of <paramref name="A"/> along dimension <paramref name="axis"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axis"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">Dimension of <paramref name="A"/> to compute the cumulative product along. A negative value counts from the last dimension.</param>
        /// <returns>Array of the same shape as A.</returns>
        /// <remarks><para><paramref name="axis"/> must lay in the range of existing dimension indices.</para>
        /// <para><paramref name="axis"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axis"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="cumsum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}})"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<sbyte> cumprod<IndT>(this ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> A,
            IndT axis) where IndT : struct, IConvertible {
            return numpyInternal.cumprod<IndT>(A, axis);
        }

        /// <summary>
        /// [numpy API] Computes the cumulative product for all elements of a flattened version of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <returns>Array of the same shape as <see cref="flatten(BaseArray{bool}, StorageOrders)"/> with cumulative products of all elements.</returns>
        /// <seealso cref="cumsum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, IndT)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<sbyte> cumprod(this ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> A) {
            return numpyInternal.cumprod<long>(A, null);
        }
       

        /// <summary>
        /// [numpy API] Computes the cumulative product of elements of <paramref name="A"/> along dimension <paramref name="axis"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axis"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">Dimension of <paramref name="A"/> to compute the cumulative product along. A negative value counts from the last dimension.</param>
        /// <returns>Array of the same shape as A.</returns>
        /// <remarks><para><paramref name="axis"/> must lay in the range of existing dimension indices.</para>
        /// <para><paramref name="axis"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axis"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="cumsum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}})"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<fcomplex> cumprod<IndT>(this ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A,
            IndT axis) where IndT : struct, IConvertible {
            return numpyInternal.cumprod<IndT>(A, axis);
        }

        /// <summary>
        /// [numpy API] Computes the cumulative product for all elements of a flattened version of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <returns>Array of the same shape as <see cref="flatten(BaseArray{bool}, StorageOrders)"/> with cumulative products of all elements.</returns>
        /// <seealso cref="cumsum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, IndT)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<fcomplex> cumprod(this ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A) {
            return numpyInternal.cumprod<long>(A, null);
        }
       

        /// <summary>
        /// [numpy API] Computes the cumulative product of elements of <paramref name="A"/> along dimension <paramref name="axis"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axis"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">Dimension of <paramref name="A"/> to compute the cumulative product along. A negative value counts from the last dimension.</param>
        /// <returns>Array of the same shape as A.</returns>
        /// <remarks><para><paramref name="axis"/> must lay in the range of existing dimension indices.</para>
        /// <para><paramref name="axis"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axis"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="cumsum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}})"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<complex> cumprod<IndT>(this ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A,
            IndT axis) where IndT : struct, IConvertible {
            return numpyInternal.cumprod<IndT>(A, axis);
        }

        /// <summary>
        /// [numpy API] Computes the cumulative product for all elements of a flattened version of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <returns>Array of the same shape as <see cref="flatten(BaseArray{bool}, StorageOrders)"/> with cumulative products of all elements.</returns>
        /// <seealso cref="cumsum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, IndT)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<complex> cumprod(this ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A) {
            return numpyInternal.cumprod<long>(A, null);
        }
       

        /// <summary>
        /// [numpy API] Computes the cumulative product of elements of <paramref name="A"/> along dimension <paramref name="axis"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axis"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">Dimension of <paramref name="A"/> to compute the cumulative product along. A negative value counts from the last dimension.</param>
        /// <returns>Array of the same shape as A.</returns>
        /// <remarks><para><paramref name="axis"/> must lay in the range of existing dimension indices.</para>
        /// <para><paramref name="axis"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axis"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="cumsum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}})"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<float> cumprod<IndT>(this ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A,
            IndT axis) where IndT : struct, IConvertible {
            return numpyInternal.cumprod<IndT>(A, axis);
        }

        /// <summary>
        /// [numpy API] Computes the cumulative product for all elements of a flattened version of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <returns>Array of the same shape as <see cref="flatten(BaseArray{bool}, StorageOrders)"/> with cumulative products of all elements.</returns>
        /// <seealso cref="cumsum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, IndT)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum{IndT}(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, BaseArray{IndT}, bool)"/>
        public static unsafe Array<float> cumprod(this ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A) {
            return numpyInternal.cumprod<long>(A, null);
        }

#endregion HYCALPER AUTO GENERATED CODE

        #endregion

        #region mean
        #region HYCALPER LOOPSTART 
        /*!HC:TYPELIST:
        <hycalper>
            <type>
            <source locate="here">
                double
            </source>
                <destination>float</destination>
                <destination>complex</destination>
                <destination>fcomplex</destination>
                <destination>sbyte</destination>
                <destination>byte</destination>
                <destination>short</destination>
                <destination>ushort</destination>
                <destination>int</destination>
                <destination>uint</destination>
                <destination>long</destination>
                <destination>ulong</destination>
            </type>
            <type>
            <source locate="here">
                Double
            </source>
                <destination>Single</destination>
                <destination>complex</destination>
                <destination>fcomplex</destination>
                <destination>double</destination>
                <destination>double</destination>
                <destination>double</destination>
                <destination>double</destination>
                <destination>double</destination>
                <destination>double</destination>
                <destination>double</destination>
                <destination>double</destination>
            </type>
        </hycalper>
        */

        /// <summary>
        /// [numpy API] Create an array based on the mean of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build mean along. Default: (null) compute from flattened array.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of summing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Reducing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="mean(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, bool)"/>
        public static unsafe Array<Double> mean<IndT>(this ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.mean<IndT>(A, axes, keepdims);
        }
        /// <summary>
        /// [numpy API] Create a scalar array with the mean of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the mean of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="mean(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, bool)"/>
        public static unsafe Array<Double> mean(this ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A,
            bool keepdims = false) {
            return numpyInternal.mean<long>(A, null, keepdims);
        }
        /// <summary>
        /// [numpy API] Create an array with the mean of elements of <paramref name="A"/> along a single dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to compute the mean of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="mean(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, bool)"/>
        public static unsafe Array<Double> mean(this ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.mean<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        /// <summary>
        /// [numpy API] Create an array based on the mean of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build mean along. Default: (null) compute from flattened array.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of summing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Reducing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="mean(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, bool)"/>
        public static unsafe Array<double> mean<IndT>(this ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.mean<IndT>(A, axes, keepdims);
        }
        /// <summary>
        /// [numpy API] Create a scalar array with the mean of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the mean of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="mean(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, bool)"/>
        public static unsafe Array<double> mean(this ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> A,
            bool keepdims = false) {
            return numpyInternal.mean<long>(A, null, keepdims);
        }
        /// <summary>
        /// [numpy API] Create an array with the mean of elements of <paramref name="A"/> along a single dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to compute the mean of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="mean(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, bool)"/>
        public static unsafe Array<double> mean(this ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.mean<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Create an array based on the mean of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build mean along. Default: (null) compute from flattened array.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of summing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Reducing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="mean(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, bool)"/>
        public static unsafe Array<double> mean<IndT>(this ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.mean<IndT>(A, axes, keepdims);
        }
        /// <summary>
        /// [numpy API] Create a scalar array with the mean of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the mean of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="mean(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, bool)"/>
        public static unsafe Array<double> mean(this ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> A,
            bool keepdims = false) {
            return numpyInternal.mean<long>(A, null, keepdims);
        }
        /// <summary>
        /// [numpy API] Create an array with the mean of elements of <paramref name="A"/> along a single dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to compute the mean of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="mean(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, bool)"/>
        public static unsafe Array<double> mean(this ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.mean<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Create an array based on the mean of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build mean along. Default: (null) compute from flattened array.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of summing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Reducing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="mean(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, bool)"/>
        public static unsafe Array<double> mean<IndT>(this ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.mean<IndT>(A, axes, keepdims);
        }
        /// <summary>
        /// [numpy API] Create a scalar array with the mean of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the mean of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="mean(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, bool)"/>
        public static unsafe Array<double> mean(this ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> A,
            bool keepdims = false) {
            return numpyInternal.mean<long>(A, null, keepdims);
        }
        /// <summary>
        /// [numpy API] Create an array with the mean of elements of <paramref name="A"/> along a single dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to compute the mean of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="mean(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, bool)"/>
        public static unsafe Array<double> mean(this ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.mean<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Create an array based on the mean of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build mean along. Default: (null) compute from flattened array.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of summing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Reducing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="mean(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, bool)"/>
        public static unsafe Array<double> mean<IndT>(this ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.mean<IndT>(A, axes, keepdims);
        }
        /// <summary>
        /// [numpy API] Create a scalar array with the mean of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the mean of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="mean(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, bool)"/>
        public static unsafe Array<double> mean(this ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> A,
            bool keepdims = false) {
            return numpyInternal.mean<long>(A, null, keepdims);
        }
        /// <summary>
        /// [numpy API] Create an array with the mean of elements of <paramref name="A"/> along a single dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to compute the mean of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="mean(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, bool)"/>
        public static unsafe Array<double> mean(this ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.mean<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Create an array based on the mean of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build mean along. Default: (null) compute from flattened array.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of summing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Reducing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="mean(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, bool)"/>
        public static unsafe Array<double> mean<IndT>(this ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.mean<IndT>(A, axes, keepdims);
        }
        /// <summary>
        /// [numpy API] Create a scalar array with the mean of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the mean of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="mean(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, bool)"/>
        public static unsafe Array<double> mean(this ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> A,
            bool keepdims = false) {
            return numpyInternal.mean<long>(A, null, keepdims);
        }
        /// <summary>
        /// [numpy API] Create an array with the mean of elements of <paramref name="A"/> along a single dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to compute the mean of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="mean(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, bool)"/>
        public static unsafe Array<double> mean(this ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.mean<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Create an array based on the mean of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build mean along. Default: (null) compute from flattened array.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of summing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Reducing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="mean(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, bool)"/>
        public static unsafe Array<double> mean<IndT>(this ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.mean<IndT>(A, axes, keepdims);
        }
        /// <summary>
        /// [numpy API] Create a scalar array with the mean of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the mean of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="mean(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, bool)"/>
        public static unsafe Array<double> mean(this ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> A,
            bool keepdims = false) {
            return numpyInternal.mean<long>(A, null, keepdims);
        }
        /// <summary>
        /// [numpy API] Create an array with the mean of elements of <paramref name="A"/> along a single dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to compute the mean of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="mean(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, bool)"/>
        public static unsafe Array<double> mean(this ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.mean<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Create an array based on the mean of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build mean along. Default: (null) compute from flattened array.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of summing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Reducing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="mean(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, bool)"/>
        public static unsafe Array<double> mean<IndT>(this ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.mean<IndT>(A, axes, keepdims);
        }
        /// <summary>
        /// [numpy API] Create a scalar array with the mean of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the mean of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="mean(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, bool)"/>
        public static unsafe Array<double> mean(this ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> A,
            bool keepdims = false) {
            return numpyInternal.mean<long>(A, null, keepdims);
        }
        /// <summary>
        /// [numpy API] Create an array with the mean of elements of <paramref name="A"/> along a single dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to compute the mean of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="mean(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, bool)"/>
        public static unsafe Array<double> mean(this ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.mean<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Create an array based on the mean of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build mean along. Default: (null) compute from flattened array.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of summing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Reducing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="mean(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, bool)"/>
        public static unsafe Array<double> mean<IndT>(this ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.mean<IndT>(A, axes, keepdims);
        }
        /// <summary>
        /// [numpy API] Create a scalar array with the mean of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the mean of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="mean(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, bool)"/>
        public static unsafe Array<double> mean(this ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> A,
            bool keepdims = false) {
            return numpyInternal.mean<long>(A, null, keepdims);
        }
        /// <summary>
        /// [numpy API] Create an array with the mean of elements of <paramref name="A"/> along a single dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to compute the mean of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="mean(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, bool)"/>
        public static unsafe Array<double> mean(this ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.mean<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Create an array based on the mean of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build mean along. Default: (null) compute from flattened array.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of summing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Reducing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="mean(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, bool)"/>
        public static unsafe Array<fcomplex> mean<IndT>(this ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.mean<IndT>(A, axes, keepdims);
        }
        /// <summary>
        /// [numpy API] Create a scalar array with the mean of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the mean of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="mean(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, bool)"/>
        public static unsafe Array<fcomplex> mean(this ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A,
            bool keepdims = false) {
            return numpyInternal.mean<long>(A, null, keepdims);
        }
        /// <summary>
        /// [numpy API] Create an array with the mean of elements of <paramref name="A"/> along a single dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to compute the mean of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="mean(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, bool)"/>
        public static unsafe Array<fcomplex> mean(this ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.mean<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Create an array based on the mean of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build mean along. Default: (null) compute from flattened array.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of summing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Reducing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="mean(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        public static unsafe Array<complex> mean<IndT>(this ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.mean<IndT>(A, axes, keepdims);
        }
        /// <summary>
        /// [numpy API] Create a scalar array with the mean of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the mean of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="mean(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        public static unsafe Array<complex> mean(this ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A,
            bool keepdims = false) {
            return numpyInternal.mean<long>(A, null, keepdims);
        }
        /// <summary>
        /// [numpy API] Create an array with the mean of elements of <paramref name="A"/> along a single dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to compute the mean of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="mean(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        public static unsafe Array<complex> mean(this ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.mean<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Create an array based on the mean of elements of <paramref name="A"/>. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to build mean along. Default: (null) compute from flattened array.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Array with the result of summing elements of <paramref name="A"/> accordingly.</returns>
        /// <remarks><para>Reducing over virtual dimensions is allowed. The result corresponds to <paramref name="A"/> and has the same shape and the same elements.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions;</exception>
        /// <seealso cref="mean(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        public static unsafe Array<Single> mean<IndT>(this ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.mean<IndT>(A, axes, keepdims);
        }
        /// <summary>
        /// [numpy API] Create a scalar array with the mean of all elements of <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A scalar array with the mean of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="mean(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        public static unsafe Array<Single> mean(this ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A,
            bool keepdims = false) {
            return numpyInternal.mean<long>(A, null, keepdims);
        }
        /// <summary>
        /// [numpy API] Create an array with the mean of elements of <paramref name="A"/> along a single dimension. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension to compute the mean of <paramref name="A"/> along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>An array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="mean(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int, bool)"/>
        /// <seealso cref="mean(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        public static unsafe Array<Single> mean(this ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A,
            int axis, bool keepdims = false) {
            return numpyInternal.mean<int>(A, MathInternal.vector<int>(axis), keepdims);
        }

#endregion HYCALPER AUTO GENERATED CODE
        #endregion

        #region all
        #region HYCALPER LOOPSTART 
        /*!HC:TYPELIST:
        <hycalper>
            <type>
            <source locate="here">
                double
            </source>
                <destination>float</destination>
                <destination>complex</destination>
                <destination>fcomplex</destination>
                <destination>sbyte</destination>
                <destination>byte</destination>
                <destination>short</destination>
                <destination>ushort</destination>
                <destination>int</destination>
                <destination>uint</destination>
                <destination>long</destination>
                <destination>ulong</destination>
            </type>
        </hycalper>
        */

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> along specific dimensions being non-zero. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to work along. Default: (null) considers all elements of <paramref name="A"/>, reducing to a scalar.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A logical array with the same shape of <paramref name="A"/>, except dimensions listed in '<paramref name="axes"/>' which are reduced / expanded to length 1.</returns>
        /// <remarks><para>All dimension indices in <paramref name="axes"/> must be a valid, non-virtual dimension index from <paramref name="A"/>.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="any(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int, bool)"/>
        public static unsafe Logical all<IndT>(this ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.all(A, axes, keepdims); 
        }

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Logical scalar array with the result of testing for non-zero values.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// </remarks>
        /// <seealso cref="any(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int, bool)"/>
        public static unsafe Logical all(this ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A,
            bool keepdims = false) {
            return numpyInternal.all<int>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> along the dimension '<paramref name="axis"/>' being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension of <paramref name="A"/> to work along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>Logical array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="axis"/> is smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="any(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int, bool)"/>
        public static unsafe Logical all(this ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A,
           int axis, bool keepdims = false) {
            return numpyInternal.all<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> along specific dimensions being non-zero. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to work along. Default: (null) considers all elements of <paramref name="A"/>, reducing to a scalar.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A logical array with the same shape of <paramref name="A"/>, except dimensions listed in '<paramref name="axes"/>' which are reduced / expanded to length 1.</returns>
        /// <remarks><para>All dimension indices in <paramref name="axes"/> must be a valid, non-virtual dimension index from <paramref name="A"/>.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="any(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int, bool)"/>
        public static unsafe Logical all<IndT>(this ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.all(A, axes, keepdims); 
        }

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Logical scalar array with the result of testing for non-zero values.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// </remarks>
        /// <seealso cref="any(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int, bool)"/>
        public static unsafe Logical all(this ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> A,
            bool keepdims = false) {
            return numpyInternal.all<int>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> along the dimension '<paramref name="axis"/>' being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension of <paramref name="A"/> to work along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>Logical array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="axis"/> is smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="any(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int, bool)"/>
        public static unsafe Logical all(this ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> A,
           int axis, bool keepdims = false) {
            return numpyInternal.all<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> along specific dimensions being non-zero. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to work along. Default: (null) considers all elements of <paramref name="A"/>, reducing to a scalar.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A logical array with the same shape of <paramref name="A"/>, except dimensions listed in '<paramref name="axes"/>' which are reduced / expanded to length 1.</returns>
        /// <remarks><para>All dimension indices in <paramref name="axes"/> must be a valid, non-virtual dimension index from <paramref name="A"/>.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="any(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int, bool)"/>
        public static unsafe Logical all<IndT>(this ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.all(A, axes, keepdims); 
        }

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Logical scalar array with the result of testing for non-zero values.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// </remarks>
        /// <seealso cref="any(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int, bool)"/>
        public static unsafe Logical all(this ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> A,
            bool keepdims = false) {
            return numpyInternal.all<int>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> along the dimension '<paramref name="axis"/>' being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension of <paramref name="A"/> to work along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>Logical array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="axis"/> is smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="any(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int, bool)"/>
        public static unsafe Logical all(this ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> A,
           int axis, bool keepdims = false) {
            return numpyInternal.all<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> along specific dimensions being non-zero. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to work along. Default: (null) considers all elements of <paramref name="A"/>, reducing to a scalar.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A logical array with the same shape of <paramref name="A"/>, except dimensions listed in '<paramref name="axes"/>' which are reduced / expanded to length 1.</returns>
        /// <remarks><para>All dimension indices in <paramref name="axes"/> must be a valid, non-virtual dimension index from <paramref name="A"/>.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="any(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int, bool)"/>
        public static unsafe Logical all<IndT>(this ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.all(A, axes, keepdims); 
        }

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Logical scalar array with the result of testing for non-zero values.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// </remarks>
        /// <seealso cref="any(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int, bool)"/>
        public static unsafe Logical all(this ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> A,
            bool keepdims = false) {
            return numpyInternal.all<int>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> along the dimension '<paramref name="axis"/>' being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension of <paramref name="A"/> to work along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>Logical array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="axis"/> is smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="any(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int, bool)"/>
        public static unsafe Logical all(this ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> A,
           int axis, bool keepdims = false) {
            return numpyInternal.all<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> along specific dimensions being non-zero. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to work along. Default: (null) considers all elements of <paramref name="A"/>, reducing to a scalar.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A logical array with the same shape of <paramref name="A"/>, except dimensions listed in '<paramref name="axes"/>' which are reduced / expanded to length 1.</returns>
        /// <remarks><para>All dimension indices in <paramref name="axes"/> must be a valid, non-virtual dimension index from <paramref name="A"/>.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="any(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int, bool)"/>
        public static unsafe Logical all<IndT>(this ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.all(A, axes, keepdims); 
        }

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Logical scalar array with the result of testing for non-zero values.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// </remarks>
        /// <seealso cref="any(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int, bool)"/>
        public static unsafe Logical all(this ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> A,
            bool keepdims = false) {
            return numpyInternal.all<int>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> along the dimension '<paramref name="axis"/>' being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension of <paramref name="A"/> to work along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>Logical array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="axis"/> is smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="any(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int, bool)"/>
        public static unsafe Logical all(this ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> A,
           int axis, bool keepdims = false) {
            return numpyInternal.all<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> along specific dimensions being non-zero. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to work along. Default: (null) considers all elements of <paramref name="A"/>, reducing to a scalar.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A logical array with the same shape of <paramref name="A"/>, except dimensions listed in '<paramref name="axes"/>' which are reduced / expanded to length 1.</returns>
        /// <remarks><para>All dimension indices in <paramref name="axes"/> must be a valid, non-virtual dimension index from <paramref name="A"/>.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="any(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int, bool)"/>
        public static unsafe Logical all<IndT>(this ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.all(A, axes, keepdims); 
        }

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Logical scalar array with the result of testing for non-zero values.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// </remarks>
        /// <seealso cref="any(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int, bool)"/>
        public static unsafe Logical all(this ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> A,
            bool keepdims = false) {
            return numpyInternal.all<int>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> along the dimension '<paramref name="axis"/>' being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension of <paramref name="A"/> to work along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>Logical array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="axis"/> is smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="any(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int, bool)"/>
        public static unsafe Logical all(this ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> A,
           int axis, bool keepdims = false) {
            return numpyInternal.all<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> along specific dimensions being non-zero. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to work along. Default: (null) considers all elements of <paramref name="A"/>, reducing to a scalar.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A logical array with the same shape of <paramref name="A"/>, except dimensions listed in '<paramref name="axes"/>' which are reduced / expanded to length 1.</returns>
        /// <remarks><para>All dimension indices in <paramref name="axes"/> must be a valid, non-virtual dimension index from <paramref name="A"/>.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="any(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int, bool)"/>
        public static unsafe Logical all<IndT>(this ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.all(A, axes, keepdims); 
        }

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Logical scalar array with the result of testing for non-zero values.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// </remarks>
        /// <seealso cref="any(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int, bool)"/>
        public static unsafe Logical all(this ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> A,
            bool keepdims = false) {
            return numpyInternal.all<int>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> along the dimension '<paramref name="axis"/>' being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension of <paramref name="A"/> to work along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>Logical array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="axis"/> is smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="any(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int, bool)"/>
        public static unsafe Logical all(this ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> A,
           int axis, bool keepdims = false) {
            return numpyInternal.all<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> along specific dimensions being non-zero. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to work along. Default: (null) considers all elements of <paramref name="A"/>, reducing to a scalar.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A logical array with the same shape of <paramref name="A"/>, except dimensions listed in '<paramref name="axes"/>' which are reduced / expanded to length 1.</returns>
        /// <remarks><para>All dimension indices in <paramref name="axes"/> must be a valid, non-virtual dimension index from <paramref name="A"/>.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="any(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int, bool)"/>
        public static unsafe Logical all<IndT>(this ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.all(A, axes, keepdims); 
        }

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Logical scalar array with the result of testing for non-zero values.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// </remarks>
        /// <seealso cref="any(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int, bool)"/>
        public static unsafe Logical all(this ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> A,
            bool keepdims = false) {
            return numpyInternal.all<int>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> along the dimension '<paramref name="axis"/>' being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension of <paramref name="A"/> to work along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>Logical array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="axis"/> is smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="any(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int, bool)"/>
        public static unsafe Logical all(this ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> A,
           int axis, bool keepdims = false) {
            return numpyInternal.all<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> along specific dimensions being non-zero. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to work along. Default: (null) considers all elements of <paramref name="A"/>, reducing to a scalar.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A logical array with the same shape of <paramref name="A"/>, except dimensions listed in '<paramref name="axes"/>' which are reduced / expanded to length 1.</returns>
        /// <remarks><para>All dimension indices in <paramref name="axes"/> must be a valid, non-virtual dimension index from <paramref name="A"/>.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="any(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int, bool)"/>
        public static unsafe Logical all<IndT>(this ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.all(A, axes, keepdims); 
        }

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Logical scalar array with the result of testing for non-zero values.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// </remarks>
        /// <seealso cref="any(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int, bool)"/>
        public static unsafe Logical all(this ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> A,
            bool keepdims = false) {
            return numpyInternal.all<int>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> along the dimension '<paramref name="axis"/>' being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension of <paramref name="A"/> to work along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>Logical array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="axis"/> is smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="any(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int, bool)"/>
        public static unsafe Logical all(this ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> A,
           int axis, bool keepdims = false) {
            return numpyInternal.all<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> along specific dimensions being non-zero. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to work along. Default: (null) considers all elements of <paramref name="A"/>, reducing to a scalar.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A logical array with the same shape of <paramref name="A"/>, except dimensions listed in '<paramref name="axes"/>' which are reduced / expanded to length 1.</returns>
        /// <remarks><para>All dimension indices in <paramref name="axes"/> must be a valid, non-virtual dimension index from <paramref name="A"/>.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="any(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int, bool)"/>
        public static unsafe Logical all<IndT>(this ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.all(A, axes, keepdims); 
        }

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Logical scalar array with the result of testing for non-zero values.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// </remarks>
        /// <seealso cref="any(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int, bool)"/>
        public static unsafe Logical all(this ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A,
            bool keepdims = false) {
            return numpyInternal.all<int>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> along the dimension '<paramref name="axis"/>' being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension of <paramref name="A"/> to work along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>Logical array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="axis"/> is smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="any(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int, bool)"/>
        public static unsafe Logical all(this ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A,
           int axis, bool keepdims = false) {
            return numpyInternal.all<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> along specific dimensions being non-zero. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to work along. Default: (null) considers all elements of <paramref name="A"/>, reducing to a scalar.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A logical array with the same shape of <paramref name="A"/>, except dimensions listed in '<paramref name="axes"/>' which are reduced / expanded to length 1.</returns>
        /// <remarks><para>All dimension indices in <paramref name="axes"/> must be a valid, non-virtual dimension index from <paramref name="A"/>.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="any(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int, bool)"/>
        public static unsafe Logical all<IndT>(this ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.all(A, axes, keepdims); 
        }

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Logical scalar array with the result of testing for non-zero values.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// </remarks>
        /// <seealso cref="any(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int, bool)"/>
        public static unsafe Logical all(this ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A,
            bool keepdims = false) {
            return numpyInternal.all<int>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> along the dimension '<paramref name="axis"/>' being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension of <paramref name="A"/> to work along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>Logical array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="axis"/> is smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="any(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int, bool)"/>
        public static unsafe Logical all(this ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A,
           int axis, bool keepdims = false) {
            return numpyInternal.all<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> along specific dimensions being non-zero. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to work along. Default: (null) considers all elements of <paramref name="A"/>, reducing to a scalar.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A logical array with the same shape of <paramref name="A"/>, except dimensions listed in '<paramref name="axes"/>' which are reduced / expanded to length 1.</returns>
        /// <remarks><para>All dimension indices in <paramref name="axes"/> must be a valid, non-virtual dimension index from <paramref name="A"/>.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="any(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int, bool)"/>
        public static unsafe Logical all<IndT>(this ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.all(A, axes, keepdims); 
        }

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Logical scalar array with the result of testing for non-zero values.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// </remarks>
        /// <seealso cref="any(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int, bool)"/>
        public static unsafe Logical all(this ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A,
            bool keepdims = false) {
            return numpyInternal.all<int>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for all elements of <paramref name="A"/> along the dimension '<paramref name="axis"/>' being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension of <paramref name="A"/> to work along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>Logical array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="axis"/> is smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="any(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int, bool)"/>
        public static unsafe Logical all(this ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A,
           int axis, bool keepdims = false) {
            return numpyInternal.all<int>(A, MathInternal.vector<int>(axis), keepdims);
        }

#endregion HYCALPER AUTO GENERATED CODE
        #endregion

        #region any
        #region HYCALPER LOOPSTART 
        /*!HC:TYPELIST:
        <hycalper>
            <type>
            <source locate="here">
                double
            </source>
                <destination>float</destination>
                <destination>complex</destination>
                <destination>fcomplex</destination>
                <destination>sbyte</destination>
                <destination>byte</destination>
                <destination>short</destination>
                <destination>ushort</destination>
                <destination>int</destination>
                <destination>uint</destination>
                <destination>long</destination>
                <destination>ulong</destination>
            </type>
        </hycalper>
        */

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> along specified dimensions '<paramref name="axes"/>' being non-zero. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to work along. Default: (null) considers all elements of <paramref name="A"/>, reducing to a scalar.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A logical array with the same shape of <paramref name="A"/>, except dimensions listed in '<paramref name="axes"/>' which are reduced / expanded to length 1.</returns>
        /// <remarks><para>All dimension indices in <paramref name="axes"/> must be a valid, non-virtual dimension index from <paramref name="A"/>.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="all(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int, bool)"/>
        public static unsafe Logical any<IndT>(this ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.any(A, axes, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Logical scalar array with the result of testing for non-zero values.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="all(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int, bool)"/>
        public static unsafe Logical any(this ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A,
            bool keepdims = false) {
            return numpyInternal.any<int>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension of <paramref name="A"/> to work along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>Logical array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="axis"/> is smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="all(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{double, Array{double}, InArray{double}, OutArray{double}, Array{double}, Storage{double}}, int, bool)"/>
        public static unsafe Logical any(this ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A,
           int axis, bool keepdims = false) {
            return numpyInternal.any<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> along specified dimensions '<paramref name="axes"/>' being non-zero. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to work along. Default: (null) considers all elements of <paramref name="A"/>, reducing to a scalar.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A logical array with the same shape of <paramref name="A"/>, except dimensions listed in '<paramref name="axes"/>' which are reduced / expanded to length 1.</returns>
        /// <remarks><para>All dimension indices in <paramref name="axes"/> must be a valid, non-virtual dimension index from <paramref name="A"/>.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="all(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int, bool)"/>
        public static unsafe Logical any<IndT>(this ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.any(A, axes, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Logical scalar array with the result of testing for non-zero values.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="all(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int, bool)"/>
        public static unsafe Logical any(this ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> A,
            bool keepdims = false) {
            return numpyInternal.any<int>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension of <paramref name="A"/> to work along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>Logical array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="axis"/> is smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="all(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{ulong, Array{ulong}, InArray{ulong}, OutArray{ulong}, Array{ulong}, Storage{ulong}}, int, bool)"/>
        public static unsafe Logical any(this ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> A,
           int axis, bool keepdims = false) {
            return numpyInternal.any<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> along specified dimensions '<paramref name="axes"/>' being non-zero. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to work along. Default: (null) considers all elements of <paramref name="A"/>, reducing to a scalar.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A logical array with the same shape of <paramref name="A"/>, except dimensions listed in '<paramref name="axes"/>' which are reduced / expanded to length 1.</returns>
        /// <remarks><para>All dimension indices in <paramref name="axes"/> must be a valid, non-virtual dimension index from <paramref name="A"/>.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="all(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int, bool)"/>
        public static unsafe Logical any<IndT>(this ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.any(A, axes, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Logical scalar array with the result of testing for non-zero values.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="all(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int, bool)"/>
        public static unsafe Logical any(this ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> A,
            bool keepdims = false) {
            return numpyInternal.any<int>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension of <paramref name="A"/> to work along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>Logical array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="axis"/> is smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="all(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{long, Array{long}, InArray{long}, OutArray{long}, Array{long}, Storage{long}}, int, bool)"/>
        public static unsafe Logical any(this ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> A,
           int axis, bool keepdims = false) {
            return numpyInternal.any<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> along specified dimensions '<paramref name="axes"/>' being non-zero. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to work along. Default: (null) considers all elements of <paramref name="A"/>, reducing to a scalar.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A logical array with the same shape of <paramref name="A"/>, except dimensions listed in '<paramref name="axes"/>' which are reduced / expanded to length 1.</returns>
        /// <remarks><para>All dimension indices in <paramref name="axes"/> must be a valid, non-virtual dimension index from <paramref name="A"/>.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="all(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int, bool)"/>
        public static unsafe Logical any<IndT>(this ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.any(A, axes, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Logical scalar array with the result of testing for non-zero values.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="all(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int, bool)"/>
        public static unsafe Logical any(this ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> A,
            bool keepdims = false) {
            return numpyInternal.any<int>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension of <paramref name="A"/> to work along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>Logical array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="axis"/> is smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="all(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{uint, Array{uint}, InArray{uint}, OutArray{uint}, Array{uint}, Storage{uint}}, int, bool)"/>
        public static unsafe Logical any(this ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> A,
           int axis, bool keepdims = false) {
            return numpyInternal.any<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> along specified dimensions '<paramref name="axes"/>' being non-zero. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to work along. Default: (null) considers all elements of <paramref name="A"/>, reducing to a scalar.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A logical array with the same shape of <paramref name="A"/>, except dimensions listed in '<paramref name="axes"/>' which are reduced / expanded to length 1.</returns>
        /// <remarks><para>All dimension indices in <paramref name="axes"/> must be a valid, non-virtual dimension index from <paramref name="A"/>.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="all(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int, bool)"/>
        public static unsafe Logical any<IndT>(this ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.any(A, axes, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Logical scalar array with the result of testing for non-zero values.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="all(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int, bool)"/>
        public static unsafe Logical any(this ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> A,
            bool keepdims = false) {
            return numpyInternal.any<int>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension of <paramref name="A"/> to work along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>Logical array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="axis"/> is smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="all(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{int, Array{int}, InArray{int}, OutArray{int}, Array{int}, Storage{int}}, int, bool)"/>
        public static unsafe Logical any(this ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> A,
           int axis, bool keepdims = false) {
            return numpyInternal.any<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> along specified dimensions '<paramref name="axes"/>' being non-zero. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to work along. Default: (null) considers all elements of <paramref name="A"/>, reducing to a scalar.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A logical array with the same shape of <paramref name="A"/>, except dimensions listed in '<paramref name="axes"/>' which are reduced / expanded to length 1.</returns>
        /// <remarks><para>All dimension indices in <paramref name="axes"/> must be a valid, non-virtual dimension index from <paramref name="A"/>.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="all(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int, bool)"/>
        public static unsafe Logical any<IndT>(this ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.any(A, axes, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Logical scalar array with the result of testing for non-zero values.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="all(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int, bool)"/>
        public static unsafe Logical any(this ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> A,
            bool keepdims = false) {
            return numpyInternal.any<int>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension of <paramref name="A"/> to work along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>Logical array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="axis"/> is smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="all(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{ushort, Array{ushort}, InArray{ushort}, OutArray{ushort}, Array{ushort}, Storage{ushort}}, int, bool)"/>
        public static unsafe Logical any(this ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> A,
           int axis, bool keepdims = false) {
            return numpyInternal.any<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> along specified dimensions '<paramref name="axes"/>' being non-zero. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to work along. Default: (null) considers all elements of <paramref name="A"/>, reducing to a scalar.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A logical array with the same shape of <paramref name="A"/>, except dimensions listed in '<paramref name="axes"/>' which are reduced / expanded to length 1.</returns>
        /// <remarks><para>All dimension indices in <paramref name="axes"/> must be a valid, non-virtual dimension index from <paramref name="A"/>.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="all(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int, bool)"/>
        public static unsafe Logical any<IndT>(this ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.any(A, axes, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Logical scalar array with the result of testing for non-zero values.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="all(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int, bool)"/>
        public static unsafe Logical any(this ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> A,
            bool keepdims = false) {
            return numpyInternal.any<int>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension of <paramref name="A"/> to work along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>Logical array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="axis"/> is smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="all(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{short, Array{short}, InArray{short}, OutArray{short}, Array{short}, Storage{short}}, int, bool)"/>
        public static unsafe Logical any(this ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> A,
           int axis, bool keepdims = false) {
            return numpyInternal.any<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> along specified dimensions '<paramref name="axes"/>' being non-zero. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to work along. Default: (null) considers all elements of <paramref name="A"/>, reducing to a scalar.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A logical array with the same shape of <paramref name="A"/>, except dimensions listed in '<paramref name="axes"/>' which are reduced / expanded to length 1.</returns>
        /// <remarks><para>All dimension indices in <paramref name="axes"/> must be a valid, non-virtual dimension index from <paramref name="A"/>.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="all(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int, bool)"/>
        public static unsafe Logical any<IndT>(this ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.any(A, axes, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Logical scalar array with the result of testing for non-zero values.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="all(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int, bool)"/>
        public static unsafe Logical any(this ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> A,
            bool keepdims = false) {
            return numpyInternal.any<int>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension of <paramref name="A"/> to work along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>Logical array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="axis"/> is smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="all(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{byte, Array{byte}, InArray{byte}, OutArray{byte}, Array{byte}, Storage{byte}}, int, bool)"/>
        public static unsafe Logical any(this ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> A,
           int axis, bool keepdims = false) {
            return numpyInternal.any<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> along specified dimensions '<paramref name="axes"/>' being non-zero. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to work along. Default: (null) considers all elements of <paramref name="A"/>, reducing to a scalar.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A logical array with the same shape of <paramref name="A"/>, except dimensions listed in '<paramref name="axes"/>' which are reduced / expanded to length 1.</returns>
        /// <remarks><para>All dimension indices in <paramref name="axes"/> must be a valid, non-virtual dimension index from <paramref name="A"/>.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="all(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int, bool)"/>
        public static unsafe Logical any<IndT>(this ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.any(A, axes, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Logical scalar array with the result of testing for non-zero values.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="all(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int, bool)"/>
        public static unsafe Logical any(this ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> A,
            bool keepdims = false) {
            return numpyInternal.any<int>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension of <paramref name="A"/> to work along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>Logical array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="axis"/> is smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="all(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{sbyte, Array{sbyte}, InArray{sbyte}, OutArray{sbyte}, Array{sbyte}, Storage{sbyte}}, int, bool)"/>
        public static unsafe Logical any(this ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> A,
           int axis, bool keepdims = false) {
            return numpyInternal.any<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> along specified dimensions '<paramref name="axes"/>' being non-zero. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to work along. Default: (null) considers all elements of <paramref name="A"/>, reducing to a scalar.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A logical array with the same shape of <paramref name="A"/>, except dimensions listed in '<paramref name="axes"/>' which are reduced / expanded to length 1.</returns>
        /// <remarks><para>All dimension indices in <paramref name="axes"/> must be a valid, non-virtual dimension index from <paramref name="A"/>.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="all(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int, bool)"/>
        public static unsafe Logical any<IndT>(this ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.any(A, axes, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Logical scalar array with the result of testing for non-zero values.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="all(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int, bool)"/>
        public static unsafe Logical any(this ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A,
            bool keepdims = false) {
            return numpyInternal.any<int>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension of <paramref name="A"/> to work along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>Logical array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="axis"/> is smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="all(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{fcomplex, Array{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, Array{fcomplex}, Storage{fcomplex}}, int, bool)"/>
        public static unsafe Logical any(this ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A,
           int axis, bool keepdims = false) {
            return numpyInternal.any<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> along specified dimensions '<paramref name="axes"/>' being non-zero. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to work along. Default: (null) considers all elements of <paramref name="A"/>, reducing to a scalar.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A logical array with the same shape of <paramref name="A"/>, except dimensions listed in '<paramref name="axes"/>' which are reduced / expanded to length 1.</returns>
        /// <remarks><para>All dimension indices in <paramref name="axes"/> must be a valid, non-virtual dimension index from <paramref name="A"/>.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="all(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int, bool)"/>
        public static unsafe Logical any<IndT>(this ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.any(A, axes, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Logical scalar array with the result of testing for non-zero values.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="all(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int, bool)"/>
        public static unsafe Logical any(this ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A,
            bool keepdims = false) {
            return numpyInternal.any<int>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension of <paramref name="A"/> to work along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>Logical array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="axis"/> is smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="all(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{complex, Array{complex}, InArray{complex}, OutArray{complex}, Array{complex}, Storage{complex}}, int, bool)"/>
        public static unsafe Logical any(this ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A,
           int axis, bool keepdims = false) {
            return numpyInternal.any<int>(A, MathInternal.vector<int>(axis), keepdims);
        }
       

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> along specified dimensions '<paramref name="axes"/>' being non-zero. 
        /// </summary>
        /// <typeparam name="IndT">Element type for <paramref name="axes"/> parameter. Must be numeric.</typeparam>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axes">[Optional] Dimensions of <paramref name="A"/> to work along. Default: (null) considers all elements of <paramref name="A"/>, reducing to a scalar.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>A logical array with the same shape of <paramref name="A"/>, except dimensions listed in '<paramref name="axes"/>' which are reduced / expanded to length 1.</returns>
        /// <remarks><para>All dimension indices in <paramref name="axes"/> must be a valid, non-virtual dimension index from <paramref name="A"/>.</para>
        /// <para>Elements of <paramref name="axes"/> may be negative. Corresponding dimension indices are considered as counting from the end of the range of existing dimensions in <paramref name="A"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type (false).</para>
        /// <para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if elements of <paramref name="axes"/> are smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="all(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int, bool)"/>
        public static unsafe Logical any<IndT>(this ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A,
            BaseArray<IndT> axes = null, bool keepdims = false) where IndT : struct, IConvertible {
            return numpyInternal.any(A, axes, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="keepdims">[Optional] accumulated dimensions remain in the resulting array. Default: (false) accumulated singleton dimensions are removed.</param>
        /// <returns>Logical scalar array with the result of testing for non-zero values.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <seealso cref="all(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int, bool)"/>
        public static unsafe Logical any(this ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A,
            bool keepdims = false) {
            return numpyInternal.any<int>(A, null, keepdims);
        }

        /// <summary>
        /// [numpy API] Tests for any elements of <paramref name="A"/> being non-zero. 
        /// </summary>
        /// <param name="A">The source array. This will not be altered.</param>
        /// <param name="axis">The dimension of <paramref name="A"/> to work along.</param>
        /// <param name="keepdims">[Optional] the reduced dimension <paramref name="axis"/> remains in the resulting array. Default: (false) the dimension <paramref name="axis"/> is removed from the result.</param>
        /// <returns>Logical array with the same shape of <paramref name="A"/>, except dimension '<paramref name="axis"/>' which is reduced / expanded to length 1.</returns>
        /// <remarks><para>Depending on the value of <paramref name="keepdims"/> the array returned will have the same number of dimensions as <paramref name="A"/> 
        /// (<paramref name="keepdims"/> = true) or with a number of dimensions according to <see cref="Settings.MinNumberOfArrayDimensions"/> (<paramref name="keepdims"/> = false).</para>
        /// <para>Empty arrays <paramref name="A"/> produce a scalar array with the default element value for the element data type.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="axis"/> is smaller than -A.S.NumberOfDimensions or larger or equal to A.S.NumberOfDimensions.</exception>
        /// <seealso cref="all(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int, bool)"/>
        /// <seealso cref="prod(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, bool)"/>
        /// <seealso cref="sum(ConcreteArray{float, Array{float}, InArray{float}, OutArray{float}, Array{float}, Storage{float}}, int, bool)"/>
        public static unsafe Logical any(this ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A,
           int axis, bool keepdims = false) {
            return numpyInternal.any<int>(A, MathInternal.vector<int>(axis), keepdims);
        }

#endregion HYCALPER AUTO GENERATED CODE

        #endregion

        #region argminmax helper
        /// <summary>
        /// [numpy API] Helper function for [nan]arg[min|max] invokations.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="A"></param>
        /// <param name="axis"></param>
        /// <param name="values"></param>
        /// <param name="order"></param>
        /// <param name="ignoreNaNs"></param>
        /// <param name="func_all">Function delegate for accumulating all elements.</param>
        /// <param name="func_axis">Function delegate for accumulating axis-wise.</param>
        /// <returns></returns>
        private static unsafe Array<long> performARGMINMAXInternal<T>(
        ConcreteArray<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>> A,
        ref int? axis, OutArray<T> values, StorageOrders order, bool ignoreNaNs,
        Func<ConcreteArray<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>>, OutArray<long>, StorageOrders, bool, Array<T>> func_all,
        Func<ConcreteArray<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>>, OutArray<long>, int, bool, Array<T>> func_axis) {
            using (Scope.Enter()) {

                using var _1 = ReaderLock.Create(A, out var storage);

                Array<long> maxIdx = 0;
                Array<T> maxValues = null;
                if (!axis.HasValue || storage.S.NumberOfDimensions == 0) {
                    maxValues = func_all(storage.AsRetArray(), maxIdx, order, ignoreNaNs);  // may releases A ! 
                } else {
                    while (axis < 0) {
                        axis += (int)storage.S.NumberOfDimensions;
                    }
                    maxValues = func_axis(storage.AsRetArray(), maxIdx, axis.GetValueOrDefault(), false);  // may releases A ! 
                }
                if (!Equals(values, null)) {
                    values.a = maxValues;
                }
                return maxIdx;
            }
        }
        #endregion

        #region minmax helper
        /// <summary>
        /// [numpy API] Helper function for [nan][min|max] invokations.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="A"></param>
        /// <param name="axis"></param>
        /// <param name="indices"></param>
        /// <param name="order"></param>
        /// <param name="ignoreNaNs"></param>
        /// <param name="func_all">Function delegate for accumulating all elements.</param>
        /// <param name="func_axis">Function delegate for accumulating axis-wise.</param>
        /// <returns></returns>
        private static unsafe Array<T> performMINMAXInternal<T>(
            ConcreteArray<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>> A,
            ref int? axis, OutArray<long> indices, StorageOrders order, bool ignoreNaNs,
            Func<ConcreteArray<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>>, OutArray<long>, StorageOrders, bool, Array<T>> func_all,
            Func<ConcreteArray<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>>, OutArray<long>, int, bool, Array<T>> func_axis) {
            using (Scope.Enter()) {

                using var _1 = ReaderLock.Create(A, out var storage);

                Array<long> maxIdx = (Equals(indices, null) ? (Array<long>)null : 0);
                Array<T> maxValues = null;
                if (!axis.HasValue || storage.S.NumberOfDimensions == 0) {
                    maxValues = func_all(storage.AsRetArray(), maxIdx, order, ignoreNaNs);
                } else {
                    while (axis < 0) {
                        axis += (int)storage.S.NumberOfDimensions;
                    }
                    maxValues = func_axis(storage.AsRetArray(), maxIdx, axis.GetValueOrDefault(), false);
                }
                if (!Equals(indices, null)) {
                    indices.a = maxIdx;
                }
                return maxValues;

            }
        }
        #endregion

    }
}
