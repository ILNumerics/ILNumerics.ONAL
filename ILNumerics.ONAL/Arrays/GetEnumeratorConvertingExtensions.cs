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
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;

namespace ILNumerics {

    /// <summary>
    /// This class implements extension methods on the main array classes.
    /// </summary>
    public static partial class ExtensionMethods {

        #region HYCALPER LOOPSTART
        /*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="here">
        ContinuousIteratorDouble
    </source>
    <destination>ContinuousIteratorSingle</destination>
    <destination>ContinuousIteratorComplex</destination>
    <destination>ContinuousIteratorfComplex</destination>
    <destination>ContinuousIteratorULong</destination>
    <destination>ContinuousIteratorLong</destination>
    <destination>ContinuousIteratorUInt</destination>
    <destination>ContinuousIteratorInt</destination>
    <destination>ContinuousIteratorUShort</destination>
    <destination>ContinuousIteratorShort</destination>
    <destination>ContinuousIteratorByte</destination>
    <destination>ContinuousIteratorSByte</destination>
    <destination>ContinuousIteratorBool</destination>
</type>
<type>
    <source locate="here">
        double
    </source>
    <destination>float</destination>
    <destination>complex</destination>
    <destination>fcomplex</destination>
    <destination>ulong</destination>
    <destination>long</destination>
    <destination>UInt32</destination>
    <destination>int</destination>
    <destination>ushort</destination>
    <destination>short</destination>
    <destination>byte</destination>
    <destination>sbyte</destination>
    <destination>bool</destination>
</type>
<type>
    <source locate="here">
        StridedIteratorDouble32
    </source>
    <destination>StridedIteratorSingle32</destination>
    <destination>StridedIteratorComplex32</destination>
    <destination>StridedIteratorFComplex32</destination>
    <destination>StridedIteratorULong32</destination>
    <destination>StridedIteratorLong32</destination>
    <destination>StridedIteratorUInt32</destination>
    <destination>StridedIteratorInt3232</destination>
    <destination>StridedIteratorUShort32</destination>
    <destination>StridedIteratorShort32</destination>
    <destination>StridedIteratorByte32</destination>
    <destination>StridedIteratorSByte32</destination>
    <destination>StridedIteratorBool32</destination>
</type>
<type>
    <source locate="here">
        StridedIteratorDouble64
    </source>
    <destination>StridedIteratorSingle64</destination>
    <destination>StridedIteratorComplex64</destination>
    <destination>StridedIteratorFComplex64</destination>
    <destination>StridedIteratorULong64</destination>
    <destination>StridedIteratorLong64</destination>
    <destination>StridedIteratorUInt64</destination>
    <destination>StridedIteratorInt3264</destination>
    <destination>StridedIteratorUShort64</destination>
    <destination>StridedIteratorShort64</destination>
    <destination>StridedIteratorByte64</destination>
    <destination>StridedIteratorSByte64</destination>
    <destination>StridedIteratorBool64</destination>
</type>
</hycalper>
*/

        /// <summary>
        /// Efficient iterator over the array for use in foreach loops. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="converter">Converter function transforming the element type into the required output type <typeparamref name="Tout"/>.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <returns>Slim, self destructing iterator instance, iterating over all elements of this array.</returns>
        public unsafe static IEnumerable<Tout> Iterator<Tout>(this BaseArray<double> A,  Func<double,Tout> converter, StorageOrders? order = null) where Tout : struct {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'."); 
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            var storage = (A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>).Storage; 
            double* start = (double*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if (storage.Size.IsContinuous && (storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2)) {
                return new Iterators.ContinuousIteratorDouble<Tout>(start, start + storage.Size.NumberOfElements - 1, null, converter);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorDouble32<Tout>(start, storage.Size.GetBSD(write: false), order, null, converter);
            } else {
                return new Iterators.StridedIteratorDouble64<Tout>(start, storage.Size.GetBSD(write: false), order, null, converter);
            }
        }

        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        /// <summary>
        /// Efficient iterator over the array for use in foreach loops. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="converter">Converter function transforming the element type into the required output type <typeparamref name="Tout"/>.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <returns>Slim, self destructing iterator instance, iterating over all elements of this array.</returns>
        public unsafe static IEnumerable<Tout> Iterator<Tout>(this BaseArray<bool> A,  Func<bool,Tout> converter, StorageOrders? order = null) where Tout : struct {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'."); 
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            var storage = (A as ConcreteArray<bool, Array<bool>, InArray<bool>, OutArray<bool>, Array<bool>, Storage<bool>>).Storage; 
            bool* start = (bool*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if (storage.Size.IsContinuous && (storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2)) {
                return new Iterators.ContinuousIteratorBool<Tout>(start, start + storage.Size.NumberOfElements - 1, null, converter);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorBool32<Tout>(start, storage.Size.GetBSD(write: false), order, null, converter);
            } else {
                return new Iterators.StridedIteratorBool64<Tout>(start, storage.Size.GetBSD(write: false), order, null, converter);
            }
        }

       

        /// <summary>
        /// Efficient iterator over the array for use in foreach loops. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="converter">Converter function transforming the element type into the required output type <typeparamref name="Tout"/>.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <returns>Slim, self destructing iterator instance, iterating over all elements of this array.</returns>
        public unsafe static IEnumerable<Tout> Iterator<Tout>(this BaseArray<sbyte> A,  Func<sbyte,Tout> converter, StorageOrders? order = null) where Tout : struct {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'."); 
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            var storage = (A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>).Storage; 
            sbyte* start = (sbyte*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if (storage.Size.IsContinuous && (storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2)) {
                return new Iterators.ContinuousIteratorSByte<Tout>(start, start + storage.Size.NumberOfElements - 1, null, converter);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorSByte32<Tout>(start, storage.Size.GetBSD(write: false), order, null, converter);
            } else {
                return new Iterators.StridedIteratorSByte64<Tout>(start, storage.Size.GetBSD(write: false), order, null, converter);
            }
        }

       

        /// <summary>
        /// Efficient iterator over the array for use in foreach loops. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="converter">Converter function transforming the element type into the required output type <typeparamref name="Tout"/>.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <returns>Slim, self destructing iterator instance, iterating over all elements of this array.</returns>
        public unsafe static IEnumerable<Tout> Iterator<Tout>(this BaseArray<byte> A,  Func<byte,Tout> converter, StorageOrders? order = null) where Tout : struct {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'."); 
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            var storage = (A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>).Storage; 
            byte* start = (byte*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if (storage.Size.IsContinuous && (storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2)) {
                return new Iterators.ContinuousIteratorByte<Tout>(start, start + storage.Size.NumberOfElements - 1, null, converter);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorByte32<Tout>(start, storage.Size.GetBSD(write: false), order, null, converter);
            } else {
                return new Iterators.StridedIteratorByte64<Tout>(start, storage.Size.GetBSD(write: false), order, null, converter);
            }
        }

       

        /// <summary>
        /// Efficient iterator over the array for use in foreach loops. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="converter">Converter function transforming the element type into the required output type <typeparamref name="Tout"/>.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <returns>Slim, self destructing iterator instance, iterating over all elements of this array.</returns>
        public unsafe static IEnumerable<Tout> Iterator<Tout>(this BaseArray<short> A,  Func<short,Tout> converter, StorageOrders? order = null) where Tout : struct {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'."); 
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            var storage = (A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>).Storage; 
            short* start = (short*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if (storage.Size.IsContinuous && (storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2)) {
                return new Iterators.ContinuousIteratorShort<Tout>(start, start + storage.Size.NumberOfElements - 1, null, converter);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorShort32<Tout>(start, storage.Size.GetBSD(write: false), order, null, converter);
            } else {
                return new Iterators.StridedIteratorShort64<Tout>(start, storage.Size.GetBSD(write: false), order, null, converter);
            }
        }

       

        /// <summary>
        /// Efficient iterator over the array for use in foreach loops. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="converter">Converter function transforming the element type into the required output type <typeparamref name="Tout"/>.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <returns>Slim, self destructing iterator instance, iterating over all elements of this array.</returns>
        public unsafe static IEnumerable<Tout> Iterator<Tout>(this BaseArray<ushort> A,  Func<ushort,Tout> converter, StorageOrders? order = null) where Tout : struct {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'."); 
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            var storage = (A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>).Storage; 
            ushort* start = (ushort*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if (storage.Size.IsContinuous && (storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2)) {
                return new Iterators.ContinuousIteratorUShort<Tout>(start, start + storage.Size.NumberOfElements - 1, null, converter);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorUShort32<Tout>(start, storage.Size.GetBSD(write: false), order, null, converter);
            } else {
                return new Iterators.StridedIteratorUShort64<Tout>(start, storage.Size.GetBSD(write: false), order, null, converter);
            }
        }

       

        /// <summary>
        /// Efficient iterator over the array for use in foreach loops. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="converter">Converter function transforming the element type into the required output type <typeparamref name="Tout"/>.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <returns>Slim, self destructing iterator instance, iterating over all elements of this array.</returns>
        public unsafe static IEnumerable<Tout> Iterator<Tout>(this BaseArray<int> A,  Func<int,Tout> converter, StorageOrders? order = null) where Tout : struct {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'."); 
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            var storage = (A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>).Storage; 
            int* start = (int*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if (storage.Size.IsContinuous && (storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2)) {
                return new Iterators.ContinuousIteratorInt<Tout>(start, start + storage.Size.NumberOfElements - 1, null, converter);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorInt3232<Tout>(start, storage.Size.GetBSD(write: false), order, null, converter);
            } else {
                return new Iterators.StridedIteratorInt3264<Tout>(start, storage.Size.GetBSD(write: false), order, null, converter);
            }
        }

       

        /// <summary>
        /// Efficient iterator over the array for use in foreach loops. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="converter">Converter function transforming the element type into the required output type <typeparamref name="Tout"/>.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <returns>Slim, self destructing iterator instance, iterating over all elements of this array.</returns>
        public unsafe static IEnumerable<Tout> Iterator<Tout>(this BaseArray<UInt32> A,  Func<UInt32,Tout> converter, StorageOrders? order = null) where Tout : struct {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'."); 
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            var storage = (A as ConcreteArray<UInt32, Array<UInt32>, InArray<UInt32>, OutArray<UInt32>, Array<UInt32>, Storage<UInt32>>).Storage; 
            UInt32* start = (UInt32*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if (storage.Size.IsContinuous && (storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2)) {
                return new Iterators.ContinuousIteratorUInt<Tout>(start, start + storage.Size.NumberOfElements - 1, null, converter);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorUInt32<Tout>(start, storage.Size.GetBSD(write: false), order, null, converter);
            } else {
                return new Iterators.StridedIteratorUInt64<Tout>(start, storage.Size.GetBSD(write: false), order, null, converter);
            }
        }

       

        /// <summary>
        /// Efficient iterator over the array for use in foreach loops. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="converter">Converter function transforming the element type into the required output type <typeparamref name="Tout"/>.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <returns>Slim, self destructing iterator instance, iterating over all elements of this array.</returns>
        public unsafe static IEnumerable<Tout> Iterator<Tout>(this BaseArray<long> A,  Func<long,Tout> converter, StorageOrders? order = null) where Tout : struct {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'."); 
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            var storage = (A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>).Storage; 
            long* start = (long*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if (storage.Size.IsContinuous && (storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2)) {
                return new Iterators.ContinuousIteratorLong<Tout>(start, start + storage.Size.NumberOfElements - 1, null, converter);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorLong32<Tout>(start, storage.Size.GetBSD(write: false), order, null, converter);
            } else {
                return new Iterators.StridedIteratorLong64<Tout>(start, storage.Size.GetBSD(write: false), order, null, converter);
            }
        }

       

        /// <summary>
        /// Efficient iterator over the array for use in foreach loops. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="converter">Converter function transforming the element type into the required output type <typeparamref name="Tout"/>.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <returns>Slim, self destructing iterator instance, iterating over all elements of this array.</returns>
        public unsafe static IEnumerable<Tout> Iterator<Tout>(this BaseArray<ulong> A,  Func<ulong,Tout> converter, StorageOrders? order = null) where Tout : struct {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'."); 
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            var storage = (A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>).Storage; 
            ulong* start = (ulong*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if (storage.Size.IsContinuous && (storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2)) {
                return new Iterators.ContinuousIteratorULong<Tout>(start, start + storage.Size.NumberOfElements - 1, null, converter);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorULong32<Tout>(start, storage.Size.GetBSD(write: false), order, null, converter);
            } else {
                return new Iterators.StridedIteratorULong64<Tout>(start, storage.Size.GetBSD(write: false), order, null, converter);
            }
        }

       

        /// <summary>
        /// Efficient iterator over the array for use in foreach loops. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="converter">Converter function transforming the element type into the required output type <typeparamref name="Tout"/>.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <returns>Slim, self destructing iterator instance, iterating over all elements of this array.</returns>
        public unsafe static IEnumerable<Tout> Iterator<Tout>(this BaseArray<fcomplex> A,  Func<fcomplex,Tout> converter, StorageOrders? order = null) where Tout : struct {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'."); 
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            var storage = (A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>).Storage; 
            fcomplex* start = (fcomplex*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if (storage.Size.IsContinuous && (storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2)) {
                return new Iterators.ContinuousIteratorfComplex<Tout>(start, start + storage.Size.NumberOfElements - 1, null, converter);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorFComplex32<Tout>(start, storage.Size.GetBSD(write: false), order, null, converter);
            } else {
                return new Iterators.StridedIteratorFComplex64<Tout>(start, storage.Size.GetBSD(write: false), order, null, converter);
            }
        }

       

        /// <summary>
        /// Efficient iterator over the array for use in foreach loops. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="converter">Converter function transforming the element type into the required output type <typeparamref name="Tout"/>.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <returns>Slim, self destructing iterator instance, iterating over all elements of this array.</returns>
        public unsafe static IEnumerable<Tout> Iterator<Tout>(this BaseArray<complex> A,  Func<complex,Tout> converter, StorageOrders? order = null) where Tout : struct {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'."); 
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            var storage = (A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>).Storage; 
            complex* start = (complex*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if (storage.Size.IsContinuous && (storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2)) {
                return new Iterators.ContinuousIteratorComplex<Tout>(start, start + storage.Size.NumberOfElements - 1, null, converter);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorComplex32<Tout>(start, storage.Size.GetBSD(write: false), order, null, converter);
            } else {
                return new Iterators.StridedIteratorComplex64<Tout>(start, storage.Size.GetBSD(write: false), order, null, converter);
            }
        }

       

        /// <summary>
        /// Efficient iterator over the array for use in foreach loops. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="converter">Converter function transforming the element type into the required output type <typeparamref name="Tout"/>.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <returns>Slim, self destructing iterator instance, iterating over all elements of this array.</returns>
        public unsafe static IEnumerable<Tout> Iterator<Tout>(this BaseArray<float> A,  Func<float,Tout> converter, StorageOrders? order = null) where Tout : struct {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'."); 
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            var storage = (A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>).Storage; 
            float* start = (float*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if (storage.Size.IsContinuous && (storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2)) {
                return new Iterators.ContinuousIteratorSingle<Tout>(start, start + storage.Size.NumberOfElements - 1, null, converter);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorSingle32<Tout>(start, storage.Size.GetBSD(write: false), order, null, converter);
            } else {
                return new Iterators.StridedIteratorSingle64<Tout>(start, storage.Size.GetBSD(write: false), order, null, converter);
            }
        }


#endregion HYCALPER AUTO GENERATED CODE
    }
}
