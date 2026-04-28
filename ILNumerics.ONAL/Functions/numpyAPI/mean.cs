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
using ILNumerics.Core.Internal;
using ILNumerics.Core.StorageLayer;
using System;
using static ILNumerics.Core.Functions.Builtin.MathInternal;

namespace ILNumerics.Core.Functions.Builtin {

    internal static partial class numpyInternal {

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
        /// Gets a new array with the mean of elements of <paramref name="A"/> along dimensions specified by <paramref name="axes"/>. 
        /// </summary>
        /// <typeparam name="AxesT">the (integer) type for the dimension indices.</typeparam>
        /// <param name="A">Input array.</param>
        /// <param name="axes">Dimensions of <paramref name="A"/> to compute the mean for.</param>
        /// <param name="keepdims"></param>
        /// <returns>New array with the dimensions <paramref name="axes"/> reduced to the mean of corresponding elements in <paramref name="A"/>.</returns>
        /// <remarks>Accumulation is internally performed in double precision. On empty array <paramref name="A"/> NaN is returned.</remarks>
        /// <exception cref="ArgumentNullException"> when <paramref name="A"/> was null on entry.</exception>
        /// <exception cref="ArgumentException">when <paramref name="axes"/> contains a value outside of the dimensions of <paramref name="A"/>.</exception>
        internal static unsafe Array<Double> mean<AxesT>(BaseArray<double> A, BaseArray<AxesT> axes = null, bool keepdims = false)
            where AxesT : struct, IConvertible {

            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            using var _1 = Global.ReaderLock.Create(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, out var storage);

            if (Equals(axes,null)) {
                var nelem = storage.Size.NumberOfElements;
                return sum(convert<Double>(storage.AsRetArray()), axes, keepdims) / (nelem > 0 ? nelem : Double.NaN);
            } else {
                using (Scope.Enter()) {
                    Array<int> ax = convert<int>(axes); // releases axes! 
                    long nelem = 1;
                    foreach (var a in ax) {
                        if (a < 0 || a > storage.S.NumberOfDimensions) {
                            throw new ArgumentException($"Invalid dimension for array specified in '{nameof(axes)}': {a}."); 
                        }
                        nelem *= storage.Size[a]; 
                    }
                    return sum(convert<Double>(storage.AsRetArray()), ax, keepdims) / (nelem > 0 ? nelem : Double.NaN);
                }
            }
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       


        /// <summary>
        /// Gets a new array with the mean of elements of <paramref name="A"/> along dimensions specified by <paramref name="axes"/>. 
        /// </summary>
        /// <typeparam name="AxesT">the (integer) type for the dimension indices.</typeparam>
        /// <param name="A">Input array.</param>
        /// <param name="axes">Dimensions of <paramref name="A"/> to compute the mean for.</param>
        /// <param name="keepdims"></param>
        /// <returns>New array with the dimensions <paramref name="axes"/> reduced to the mean of corresponding elements in <paramref name="A"/>.</returns>
        /// <remarks>Accumulation is internally performed in ulong precision. On empty array <paramref name="A"/> NaN is returned.</remarks>
        /// <exception cref="ArgumentNullException"> when <paramref name="A"/> was null on entry.</exception>
        /// <exception cref="ArgumentException">when <paramref name="axes"/> contains a value outside of the dimensions of <paramref name="A"/>.</exception>
        internal static unsafe Array<double> mean<AxesT>(BaseArray<ulong> A, BaseArray<AxesT> axes = null, bool keepdims = false)
            where AxesT : struct, IConvertible {

            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            using var _1 = Global.ReaderLock.Create(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, out var storage);

            if (Equals(axes,null)) {
                var nelem = storage.Size.NumberOfElements;
                return sum(convert<double>(storage.AsRetArray()), axes, keepdims) / (nelem > 0 ? nelem : double.NaN);
            } else {
                using (Scope.Enter()) {
                    Array<int> ax = convert<int>(axes); // releases axes! 
                    long nelem = 1;
                    foreach (var a in ax) {
                        if (a < 0 || a > storage.S.NumberOfDimensions) {
                            throw new ArgumentException($"Invalid dimension for array specified in '{nameof(axes)}': {a}."); 
                        }
                        nelem *= storage.Size[a]; 
                    }
                    return sum(convert<double>(storage.AsRetArray()), ax, keepdims) / (nelem > 0 ? nelem : double.NaN);
                }
            }
        }
       


        /// <summary>
        /// Gets a new array with the mean of elements of <paramref name="A"/> along dimensions specified by <paramref name="axes"/>. 
        /// </summary>
        /// <typeparam name="AxesT">the (integer) type for the dimension indices.</typeparam>
        /// <param name="A">Input array.</param>
        /// <param name="axes">Dimensions of <paramref name="A"/> to compute the mean for.</param>
        /// <param name="keepdims"></param>
        /// <returns>New array with the dimensions <paramref name="axes"/> reduced to the mean of corresponding elements in <paramref name="A"/>.</returns>
        /// <remarks>Accumulation is internally performed in long precision. On empty array <paramref name="A"/> NaN is returned.</remarks>
        /// <exception cref="ArgumentNullException"> when <paramref name="A"/> was null on entry.</exception>
        /// <exception cref="ArgumentException">when <paramref name="axes"/> contains a value outside of the dimensions of <paramref name="A"/>.</exception>
        internal static unsafe Array<double> mean<AxesT>(BaseArray<long> A, BaseArray<AxesT> axes = null, bool keepdims = false)
            where AxesT : struct, IConvertible {

            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            using var _1 = Global.ReaderLock.Create(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, out var storage);

            if (Equals(axes,null)) {
                var nelem = storage.Size.NumberOfElements;
                return sum(convert<double>(storage.AsRetArray()), axes, keepdims) / (nelem > 0 ? nelem : double.NaN);
            } else {
                using (Scope.Enter()) {
                    Array<int> ax = convert<int>(axes); // releases axes! 
                    long nelem = 1;
                    foreach (var a in ax) {
                        if (a < 0 || a > storage.S.NumberOfDimensions) {
                            throw new ArgumentException($"Invalid dimension for array specified in '{nameof(axes)}': {a}."); 
                        }
                        nelem *= storage.Size[a]; 
                    }
                    return sum(convert<double>(storage.AsRetArray()), ax, keepdims) / (nelem > 0 ? nelem : double.NaN);
                }
            }
        }
       


        /// <summary>
        /// Gets a new array with the mean of elements of <paramref name="A"/> along dimensions specified by <paramref name="axes"/>. 
        /// </summary>
        /// <typeparam name="AxesT">the (integer) type for the dimension indices.</typeparam>
        /// <param name="A">Input array.</param>
        /// <param name="axes">Dimensions of <paramref name="A"/> to compute the mean for.</param>
        /// <param name="keepdims"></param>
        /// <returns>New array with the dimensions <paramref name="axes"/> reduced to the mean of corresponding elements in <paramref name="A"/>.</returns>
        /// <remarks>Accumulation is internally performed in uint precision. On empty array <paramref name="A"/> NaN is returned.</remarks>
        /// <exception cref="ArgumentNullException"> when <paramref name="A"/> was null on entry.</exception>
        /// <exception cref="ArgumentException">when <paramref name="axes"/> contains a value outside of the dimensions of <paramref name="A"/>.</exception>
        internal static unsafe Array<double> mean<AxesT>(BaseArray<uint> A, BaseArray<AxesT> axes = null, bool keepdims = false)
            where AxesT : struct, IConvertible {

            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            using var _1 = Global.ReaderLock.Create(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, out var storage);

            if (Equals(axes,null)) {
                var nelem = storage.Size.NumberOfElements;
                return sum(convert<double>(storage.AsRetArray()), axes, keepdims) / (nelem > 0 ? nelem : double.NaN);
            } else {
                using (Scope.Enter()) {
                    Array<int> ax = convert<int>(axes); // releases axes! 
                    long nelem = 1;
                    foreach (var a in ax) {
                        if (a < 0 || a > storage.S.NumberOfDimensions) {
                            throw new ArgumentException($"Invalid dimension for array specified in '{nameof(axes)}': {a}."); 
                        }
                        nelem *= storage.Size[a]; 
                    }
                    return sum(convert<double>(storage.AsRetArray()), ax, keepdims) / (nelem > 0 ? nelem : double.NaN);
                }
            }
        }
       


        /// <summary>
        /// Gets a new array with the mean of elements of <paramref name="A"/> along dimensions specified by <paramref name="axes"/>. 
        /// </summary>
        /// <typeparam name="AxesT">the (integer) type for the dimension indices.</typeparam>
        /// <param name="A">Input array.</param>
        /// <param name="axes">Dimensions of <paramref name="A"/> to compute the mean for.</param>
        /// <param name="keepdims"></param>
        /// <returns>New array with the dimensions <paramref name="axes"/> reduced to the mean of corresponding elements in <paramref name="A"/>.</returns>
        /// <remarks>Accumulation is internally performed in int precision. On empty array <paramref name="A"/> NaN is returned.</remarks>
        /// <exception cref="ArgumentNullException"> when <paramref name="A"/> was null on entry.</exception>
        /// <exception cref="ArgumentException">when <paramref name="axes"/> contains a value outside of the dimensions of <paramref name="A"/>.</exception>
        internal static unsafe Array<double> mean<AxesT>(BaseArray<int> A, BaseArray<AxesT> axes = null, bool keepdims = false)
            where AxesT : struct, IConvertible {

            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            using var _1 = Global.ReaderLock.Create(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, out var storage);

            if (Equals(axes,null)) {
                var nelem = storage.Size.NumberOfElements;
                return sum(convert<double>(storage.AsRetArray()), axes, keepdims) / (nelem > 0 ? nelem : double.NaN);
            } else {
                using (Scope.Enter()) {
                    Array<int> ax = convert<int>(axes); // releases axes! 
                    long nelem = 1;
                    foreach (var a in ax) {
                        if (a < 0 || a > storage.S.NumberOfDimensions) {
                            throw new ArgumentException($"Invalid dimension for array specified in '{nameof(axes)}': {a}."); 
                        }
                        nelem *= storage.Size[a]; 
                    }
                    return sum(convert<double>(storage.AsRetArray()), ax, keepdims) / (nelem > 0 ? nelem : double.NaN);
                }
            }
        }
       


        /// <summary>
        /// Gets a new array with the mean of elements of <paramref name="A"/> along dimensions specified by <paramref name="axes"/>. 
        /// </summary>
        /// <typeparam name="AxesT">the (integer) type for the dimension indices.</typeparam>
        /// <param name="A">Input array.</param>
        /// <param name="axes">Dimensions of <paramref name="A"/> to compute the mean for.</param>
        /// <param name="keepdims"></param>
        /// <returns>New array with the dimensions <paramref name="axes"/> reduced to the mean of corresponding elements in <paramref name="A"/>.</returns>
        /// <remarks>Accumulation is internally performed in ushort precision. On empty array <paramref name="A"/> NaN is returned.</remarks>
        /// <exception cref="ArgumentNullException"> when <paramref name="A"/> was null on entry.</exception>
        /// <exception cref="ArgumentException">when <paramref name="axes"/> contains a value outside of the dimensions of <paramref name="A"/>.</exception>
        internal static unsafe Array<double> mean<AxesT>(BaseArray<ushort> A, BaseArray<AxesT> axes = null, bool keepdims = false)
            where AxesT : struct, IConvertible {

            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            using var _1 = Global.ReaderLock.Create(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, out var storage);

            if (Equals(axes,null)) {
                var nelem = storage.Size.NumberOfElements;
                return sum(convert<double>(storage.AsRetArray()), axes, keepdims) / (nelem > 0 ? nelem : double.NaN);
            } else {
                using (Scope.Enter()) {
                    Array<int> ax = convert<int>(axes); // releases axes! 
                    long nelem = 1;
                    foreach (var a in ax) {
                        if (a < 0 || a > storage.S.NumberOfDimensions) {
                            throw new ArgumentException($"Invalid dimension for array specified in '{nameof(axes)}': {a}."); 
                        }
                        nelem *= storage.Size[a]; 
                    }
                    return sum(convert<double>(storage.AsRetArray()), ax, keepdims) / (nelem > 0 ? nelem : double.NaN);
                }
            }
        }
       


        /// <summary>
        /// Gets a new array with the mean of elements of <paramref name="A"/> along dimensions specified by <paramref name="axes"/>. 
        /// </summary>
        /// <typeparam name="AxesT">the (integer) type for the dimension indices.</typeparam>
        /// <param name="A">Input array.</param>
        /// <param name="axes">Dimensions of <paramref name="A"/> to compute the mean for.</param>
        /// <param name="keepdims"></param>
        /// <returns>New array with the dimensions <paramref name="axes"/> reduced to the mean of corresponding elements in <paramref name="A"/>.</returns>
        /// <remarks>Accumulation is internally performed in short precision. On empty array <paramref name="A"/> NaN is returned.</remarks>
        /// <exception cref="ArgumentNullException"> when <paramref name="A"/> was null on entry.</exception>
        /// <exception cref="ArgumentException">when <paramref name="axes"/> contains a value outside of the dimensions of <paramref name="A"/>.</exception>
        internal static unsafe Array<double> mean<AxesT>(BaseArray<short> A, BaseArray<AxesT> axes = null, bool keepdims = false)
            where AxesT : struct, IConvertible {

            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            using var _1 = Global.ReaderLock.Create(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, out var storage);

            if (Equals(axes,null)) {
                var nelem = storage.Size.NumberOfElements;
                return sum(convert<double>(storage.AsRetArray()), axes, keepdims) / (nelem > 0 ? nelem : double.NaN);
            } else {
                using (Scope.Enter()) {
                    Array<int> ax = convert<int>(axes); // releases axes! 
                    long nelem = 1;
                    foreach (var a in ax) {
                        if (a < 0 || a > storage.S.NumberOfDimensions) {
                            throw new ArgumentException($"Invalid dimension for array specified in '{nameof(axes)}': {a}."); 
                        }
                        nelem *= storage.Size[a]; 
                    }
                    return sum(convert<double>(storage.AsRetArray()), ax, keepdims) / (nelem > 0 ? nelem : double.NaN);
                }
            }
        }
       


        /// <summary>
        /// Gets a new array with the mean of elements of <paramref name="A"/> along dimensions specified by <paramref name="axes"/>. 
        /// </summary>
        /// <typeparam name="AxesT">the (integer) type for the dimension indices.</typeparam>
        /// <param name="A">Input array.</param>
        /// <param name="axes">Dimensions of <paramref name="A"/> to compute the mean for.</param>
        /// <param name="keepdims"></param>
        /// <returns>New array with the dimensions <paramref name="axes"/> reduced to the mean of corresponding elements in <paramref name="A"/>.</returns>
        /// <remarks>Accumulation is internally performed in byte precision. On empty array <paramref name="A"/> NaN is returned.</remarks>
        /// <exception cref="ArgumentNullException"> when <paramref name="A"/> was null on entry.</exception>
        /// <exception cref="ArgumentException">when <paramref name="axes"/> contains a value outside of the dimensions of <paramref name="A"/>.</exception>
        internal static unsafe Array<double> mean<AxesT>(BaseArray<byte> A, BaseArray<AxesT> axes = null, bool keepdims = false)
            where AxesT : struct, IConvertible {

            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            using var _1 = Global.ReaderLock.Create(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, out var storage);

            if (Equals(axes,null)) {
                var nelem = storage.Size.NumberOfElements;
                return sum(convert<double>(storage.AsRetArray()), axes, keepdims) / (nelem > 0 ? nelem : double.NaN);
            } else {
                using (Scope.Enter()) {
                    Array<int> ax = convert<int>(axes); // releases axes! 
                    long nelem = 1;
                    foreach (var a in ax) {
                        if (a < 0 || a > storage.S.NumberOfDimensions) {
                            throw new ArgumentException($"Invalid dimension for array specified in '{nameof(axes)}': {a}."); 
                        }
                        nelem *= storage.Size[a]; 
                    }
                    return sum(convert<double>(storage.AsRetArray()), ax, keepdims) / (nelem > 0 ? nelem : double.NaN);
                }
            }
        }
       


        /// <summary>
        /// Gets a new array with the mean of elements of <paramref name="A"/> along dimensions specified by <paramref name="axes"/>. 
        /// </summary>
        /// <typeparam name="AxesT">the (integer) type for the dimension indices.</typeparam>
        /// <param name="A">Input array.</param>
        /// <param name="axes">Dimensions of <paramref name="A"/> to compute the mean for.</param>
        /// <param name="keepdims"></param>
        /// <returns>New array with the dimensions <paramref name="axes"/> reduced to the mean of corresponding elements in <paramref name="A"/>.</returns>
        /// <remarks>Accumulation is internally performed in sbyte precision. On empty array <paramref name="A"/> NaN is returned.</remarks>
        /// <exception cref="ArgumentNullException"> when <paramref name="A"/> was null on entry.</exception>
        /// <exception cref="ArgumentException">when <paramref name="axes"/> contains a value outside of the dimensions of <paramref name="A"/>.</exception>
        internal static unsafe Array<double> mean<AxesT>(BaseArray<sbyte> A, BaseArray<AxesT> axes = null, bool keepdims = false)
            where AxesT : struct, IConvertible {

            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            using var _1 = Global.ReaderLock.Create(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, out var storage);

            if (Equals(axes,null)) {
                var nelem = storage.Size.NumberOfElements;
                return sum(convert<double>(storage.AsRetArray()), axes, keepdims) / (nelem > 0 ? nelem : double.NaN);
            } else {
                using (Scope.Enter()) {
                    Array<int> ax = convert<int>(axes); // releases axes! 
                    long nelem = 1;
                    foreach (var a in ax) {
                        if (a < 0 || a > storage.S.NumberOfDimensions) {
                            throw new ArgumentException($"Invalid dimension for array specified in '{nameof(axes)}': {a}."); 
                        }
                        nelem *= storage.Size[a]; 
                    }
                    return sum(convert<double>(storage.AsRetArray()), ax, keepdims) / (nelem > 0 ? nelem : double.NaN);
                }
            }
        }
       


        /// <summary>
        /// Gets a new array with the mean of elements of <paramref name="A"/> along dimensions specified by <paramref name="axes"/>. 
        /// </summary>
        /// <typeparam name="AxesT">the (integer) type for the dimension indices.</typeparam>
        /// <param name="A">Input array.</param>
        /// <param name="axes">Dimensions of <paramref name="A"/> to compute the mean for.</param>
        /// <param name="keepdims"></param>
        /// <returns>New array with the dimensions <paramref name="axes"/> reduced to the mean of corresponding elements in <paramref name="A"/>.</returns>
        /// <remarks>Accumulation is internally performed in fcomplex precision. On empty array <paramref name="A"/> NaN is returned.</remarks>
        /// <exception cref="ArgumentNullException"> when <paramref name="A"/> was null on entry.</exception>
        /// <exception cref="ArgumentException">when <paramref name="axes"/> contains a value outside of the dimensions of <paramref name="A"/>.</exception>
        internal static unsafe Array<fcomplex> mean<AxesT>(BaseArray<fcomplex> A, BaseArray<AxesT> axes = null, bool keepdims = false)
            where AxesT : struct, IConvertible {

            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            using var _1 = Global.ReaderLock.Create(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, out var storage);

            if (Equals(axes,null)) {
                var nelem = storage.Size.NumberOfElements;
                return sum(convert<fcomplex>(storage.AsRetArray()), axes, keepdims) / (nelem > 0 ? nelem : fcomplex.NaN);
            } else {
                using (Scope.Enter()) {
                    Array<int> ax = convert<int>(axes); // releases axes! 
                    long nelem = 1;
                    foreach (var a in ax) {
                        if (a < 0 || a > storage.S.NumberOfDimensions) {
                            throw new ArgumentException($"Invalid dimension for array specified in '{nameof(axes)}': {a}."); 
                        }
                        nelem *= storage.Size[a]; 
                    }
                    return sum(convert<fcomplex>(storage.AsRetArray()), ax, keepdims) / (nelem > 0 ? nelem : fcomplex.NaN);
                }
            }
        }
       


        /// <summary>
        /// Gets a new array with the mean of elements of <paramref name="A"/> along dimensions specified by <paramref name="axes"/>. 
        /// </summary>
        /// <typeparam name="AxesT">the (integer) type for the dimension indices.</typeparam>
        /// <param name="A">Input array.</param>
        /// <param name="axes">Dimensions of <paramref name="A"/> to compute the mean for.</param>
        /// <param name="keepdims"></param>
        /// <returns>New array with the dimensions <paramref name="axes"/> reduced to the mean of corresponding elements in <paramref name="A"/>.</returns>
        /// <remarks>Accumulation is internally performed in complex precision. On empty array <paramref name="A"/> NaN is returned.</remarks>
        /// <exception cref="ArgumentNullException"> when <paramref name="A"/> was null on entry.</exception>
        /// <exception cref="ArgumentException">when <paramref name="axes"/> contains a value outside of the dimensions of <paramref name="A"/>.</exception>
        internal static unsafe Array<complex> mean<AxesT>(BaseArray<complex> A, BaseArray<AxesT> axes = null, bool keepdims = false)
            where AxesT : struct, IConvertible {

            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            using var _1 = Global.ReaderLock.Create(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, out var storage);

            if (Equals(axes,null)) {
                var nelem = storage.Size.NumberOfElements;
                return sum(convert<complex>(storage.AsRetArray()), axes, keepdims) / (nelem > 0 ? nelem : complex.NaN);
            } else {
                using (Scope.Enter()) {
                    Array<int> ax = convert<int>(axes); // releases axes! 
                    long nelem = 1;
                    foreach (var a in ax) {
                        if (a < 0 || a > storage.S.NumberOfDimensions) {
                            throw new ArgumentException($"Invalid dimension for array specified in '{nameof(axes)}': {a}."); 
                        }
                        nelem *= storage.Size[a]; 
                    }
                    return sum(convert<complex>(storage.AsRetArray()), ax, keepdims) / (nelem > 0 ? nelem : complex.NaN);
                }
            }
        }
       


        /// <summary>
        /// Gets a new array with the mean of elements of <paramref name="A"/> along dimensions specified by <paramref name="axes"/>. 
        /// </summary>
        /// <typeparam name="AxesT">the (integer) type for the dimension indices.</typeparam>
        /// <param name="A">Input array.</param>
        /// <param name="axes">Dimensions of <paramref name="A"/> to compute the mean for.</param>
        /// <param name="keepdims"></param>
        /// <returns>New array with the dimensions <paramref name="axes"/> reduced to the mean of corresponding elements in <paramref name="A"/>.</returns>
        /// <remarks>Accumulation is internally performed in float precision. On empty array <paramref name="A"/> NaN is returned.</remarks>
        /// <exception cref="ArgumentNullException"> when <paramref name="A"/> was null on entry.</exception>
        /// <exception cref="ArgumentException">when <paramref name="axes"/> contains a value outside of the dimensions of <paramref name="A"/>.</exception>
        internal static unsafe Array<Single> mean<AxesT>(BaseArray<float> A, BaseArray<AxesT> axes = null, bool keepdims = false)
            where AxesT : struct, IConvertible {

            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            using var _1 = Global.ReaderLock.Create(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, out var storage);

            if (Equals(axes,null)) {
                var nelem = storage.Size.NumberOfElements;
                return sum(convert<Single>(storage.AsRetArray()), axes, keepdims) / (nelem > 0 ? nelem : Single.NaN);
            } else {
                using (Scope.Enter()) {
                    Array<int> ax = convert<int>(axes); // releases axes! 
                    long nelem = 1;
                    foreach (var a in ax) {
                        if (a < 0 || a > storage.S.NumberOfDimensions) {
                            throw new ArgumentException($"Invalid dimension for array specified in '{nameof(axes)}': {a}."); 
                        }
                        nelem *= storage.Size[a]; 
                    }
                    return sum(convert<Single>(storage.AsRetArray()), ax, keepdims) / (nelem > 0 ? nelem : Single.NaN);
                }
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

    }
}
