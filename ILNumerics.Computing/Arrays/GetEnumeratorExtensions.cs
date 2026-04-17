//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////
using ILNumerics.Core.Arrays;
using ILNumerics.Core.Global;
using ILNumerics.Core.MemoryLayer;
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

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
    <destination>ContinuousIteratorLong</destination>
    <destination>ContinuousIteratorULong</destination>
    <destination>ContinuousIteratorUInt</destination>
    <destination>ContinuousIteratorInt</destination>
    <destination>ContinuousIteratorUShort</destination>
    <destination>ContinuousIteratorShort</destination>
    <destination>ContinuousIteratorByte</destination>
    <destination>ContinuousIteratorSByte</destination>
</type>
<type>
    <source locate="here">
        double
    </source>
    <destination>float</destination>
    <destination>complex</destination>
    <destination>fcomplex</destination>
    <destination>long</destination>
    <destination>ulong</destination>
    <destination>UInt32</destination>
    <destination>int</destination>
    <destination>ushort</destination>
    <destination>short</destination>
    <destination>byte</destination>
    <destination>sbyte</destination>
</type>
<type>
    <source locate="here">
        StridedIteratorDouble32
    </source>
    <destination>StridedIteratorSingle32</destination>
    <destination>StridedIteratorComplex32</destination>
    <destination>StridedIteratorFComplex32</destination>
    <destination>StridedIteratorLong32</destination>
    <destination>StridedIteratorULong32</destination>
    <destination>StridedIteratorUInt32</destination>
    <destination>StridedIteratorInt3232</destination>
    <destination>StridedIteratorUShort32</destination>
    <destination>StridedIteratorShort32</destination>
    <destination>StridedIteratorByte32</destination>
    <destination>StridedIteratorSByte32</destination>
</type>
<type>
    <source locate="here">
        StridedIteratorDouble64
    </source>
    <destination>StridedIteratorSingle64</destination>
    <destination>StridedIteratorComplex64</destination>
    <destination>StridedIteratorFComplex64</destination>
    <destination>StridedIteratorLong64</destination>
    <destination>StridedIteratorULong64</destination>
    <destination>StridedIteratorUInt64</destination>
    <destination>StridedIteratorInt3264</destination>
    <destination>StridedIteratorUShort64</destination>
    <destination>StridedIteratorShort64</destination>
    <destination>StridedIteratorByte64</destination>
    <destination>StridedIteratorSByte64</destination>
</type>
</hycalper>
*/

        /// <summary>
        /// Efficient iterator over the array for use in foreach loops. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <param name="keepAlive">[Optional] do not release <paramref name="A"/> after iteration. Default: false (RetT typed A will be released).</param>
        /// <returns>Slim, self destructing iterator instance, iterating over all elements of this array.</returns>
        public unsafe static IEnumerable<double> Iterator(
            this BaseArray<double> A, 
            StorageOrders? order = null,
            bool keepAlive = false) {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'."); 
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, out var storage, releaseRetT : false); 

            double* start = (double*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if ((storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2) && storage.Size.IsContinuous) {
                return new Iterators.ContinuousIteratorDouble(start, start + storage.Size.NumberOfElements - 1, keepAlive ? null : null);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorDouble32(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null);
            } else {
                return new Iterators.StridedIteratorDouble64(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null);
            }
        }

        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        /// <summary>
        /// Efficient iterator over the array for use in foreach loops. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <param name="keepAlive">[Optional] do not release <paramref name="A"/> after iteration. Default: false (RetT typed A will be released).</param>
        /// <returns>Slim, self destructing iterator instance, iterating over all elements of this array.</returns>
        public unsafe static IEnumerable<sbyte> Iterator(
            this BaseArray<sbyte> A, 
            StorageOrders? order = null,
            bool keepAlive = false) {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'."); 
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, out var storage, releaseRetT : false); 

            sbyte* start = (sbyte*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if ((storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2) && storage.Size.IsContinuous) {
                return new Iterators.ContinuousIteratorSByte(start, start + storage.Size.NumberOfElements - 1, keepAlive ? null : null);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorSByte32(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null);
            } else {
                return new Iterators.StridedIteratorSByte64(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null);
            }
        }

       

        /// <summary>
        /// Efficient iterator over the array for use in foreach loops. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <param name="keepAlive">[Optional] do not release <paramref name="A"/> after iteration. Default: false (RetT typed A will be released).</param>
        /// <returns>Slim, self destructing iterator instance, iterating over all elements of this array.</returns>
        public unsafe static IEnumerable<byte> Iterator(
            this BaseArray<byte> A, 
            StorageOrders? order = null,
            bool keepAlive = false) {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'."); 
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, out var storage, releaseRetT : false); 

            byte* start = (byte*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if ((storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2) && storage.Size.IsContinuous) {
                return new Iterators.ContinuousIteratorByte(start, start + storage.Size.NumberOfElements - 1, keepAlive ? null : null);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorByte32(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null);
            } else {
                return new Iterators.StridedIteratorByte64(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null);
            }
        }

       

        /// <summary>
        /// Efficient iterator over the array for use in foreach loops. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <param name="keepAlive">[Optional] do not release <paramref name="A"/> after iteration. Default: false (RetT typed A will be released).</param>
        /// <returns>Slim, self destructing iterator instance, iterating over all elements of this array.</returns>
        public unsafe static IEnumerable<short> Iterator(
            this BaseArray<short> A, 
            StorageOrders? order = null,
            bool keepAlive = false) {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'."); 
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, out var storage, releaseRetT : false); 

            short* start = (short*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if ((storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2) && storage.Size.IsContinuous) {
                return new Iterators.ContinuousIteratorShort(start, start + storage.Size.NumberOfElements - 1, keepAlive ? null : null);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorShort32(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null);
            } else {
                return new Iterators.StridedIteratorShort64(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null);
            }
        }

       

        /// <summary>
        /// Efficient iterator over the array for use in foreach loops. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <param name="keepAlive">[Optional] do not release <paramref name="A"/> after iteration. Default: false (RetT typed A will be released).</param>
        /// <returns>Slim, self destructing iterator instance, iterating over all elements of this array.</returns>
        public unsafe static IEnumerable<ushort> Iterator(
            this BaseArray<ushort> A, 
            StorageOrders? order = null,
            bool keepAlive = false) {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'."); 
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, out var storage, releaseRetT : false); 

            ushort* start = (ushort*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if ((storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2) && storage.Size.IsContinuous) {
                return new Iterators.ContinuousIteratorUShort(start, start + storage.Size.NumberOfElements - 1, keepAlive ? null : null);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorUShort32(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null);
            } else {
                return new Iterators.StridedIteratorUShort64(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null);
            }
        }

       

        /// <summary>
        /// Efficient iterator over the array for use in foreach loops. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <param name="keepAlive">[Optional] do not release <paramref name="A"/> after iteration. Default: false (RetT typed A will be released).</param>
        /// <returns>Slim, self destructing iterator instance, iterating over all elements of this array.</returns>
        public unsafe static IEnumerable<int> Iterator(
            this BaseArray<int> A, 
            StorageOrders? order = null,
            bool keepAlive = false) {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'."); 
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, out var storage, releaseRetT : false); 

            int* start = (int*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if ((storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2) && storage.Size.IsContinuous) {
                return new Iterators.ContinuousIteratorInt(start, start + storage.Size.NumberOfElements - 1, keepAlive ? null : null);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorInt3232(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null);
            } else {
                return new Iterators.StridedIteratorInt3264(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null);
            }
        }

       

        /// <summary>
        /// Efficient iterator over the array for use in foreach loops. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <param name="keepAlive">[Optional] do not release <paramref name="A"/> after iteration. Default: false (RetT typed A will be released).</param>
        /// <returns>Slim, self destructing iterator instance, iterating over all elements of this array.</returns>
        public unsafe static IEnumerable<UInt32> Iterator(
            this BaseArray<UInt32> A, 
            StorageOrders? order = null,
            bool keepAlive = false) {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'."); 
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<UInt32, Array<UInt32>, InArray<UInt32>, OutArray<UInt32>, Array<UInt32>, Storage<UInt32>>, out var storage, releaseRetT : false); 

            UInt32* start = (UInt32*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if ((storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2) && storage.Size.IsContinuous) {
                return new Iterators.ContinuousIteratorUInt(start, start + storage.Size.NumberOfElements - 1, keepAlive ? null : null);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorUInt32(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null);
            } else {
                return new Iterators.StridedIteratorUInt64(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null);
            }
        }

       

        /// <summary>
        /// Efficient iterator over the array for use in foreach loops. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <param name="keepAlive">[Optional] do not release <paramref name="A"/> after iteration. Default: false (RetT typed A will be released).</param>
        /// <returns>Slim, self destructing iterator instance, iterating over all elements of this array.</returns>
        public unsafe static IEnumerable<ulong> Iterator(
            this BaseArray<ulong> A, 
            StorageOrders? order = null,
            bool keepAlive = false) {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'."); 
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, out var storage, releaseRetT : false); 

            ulong* start = (ulong*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if ((storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2) && storage.Size.IsContinuous) {
                return new Iterators.ContinuousIteratorULong(start, start + storage.Size.NumberOfElements - 1, keepAlive ? null : null);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorULong32(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null);
            } else {
                return new Iterators.StridedIteratorULong64(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null);
            }
        }

       

        /// <summary>
        /// Efficient iterator over the array for use in foreach loops. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <param name="keepAlive">[Optional] do not release <paramref name="A"/> after iteration. Default: false (RetT typed A will be released).</param>
        /// <returns>Slim, self destructing iterator instance, iterating over all elements of this array.</returns>
        public unsafe static IEnumerable<long> Iterator(
            this BaseArray<long> A, 
            StorageOrders? order = null,
            bool keepAlive = false) {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'."); 
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, out var storage, releaseRetT : false); 

            long* start = (long*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if ((storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2) && storage.Size.IsContinuous) {
                return new Iterators.ContinuousIteratorLong(start, start + storage.Size.NumberOfElements - 1, keepAlive ? null : null);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorLong32(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null);
            } else {
                return new Iterators.StridedIteratorLong64(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null);
            }
        }

       

        /// <summary>
        /// Efficient iterator over the array for use in foreach loops. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <param name="keepAlive">[Optional] do not release <paramref name="A"/> after iteration. Default: false (RetT typed A will be released).</param>
        /// <returns>Slim, self destructing iterator instance, iterating over all elements of this array.</returns>
        public unsafe static IEnumerable<fcomplex> Iterator(
            this BaseArray<fcomplex> A, 
            StorageOrders? order = null,
            bool keepAlive = false) {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'."); 
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, out var storage, releaseRetT : false); 

            fcomplex* start = (fcomplex*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if ((storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2) && storage.Size.IsContinuous) {
                return new Iterators.ContinuousIteratorfComplex(start, start + storage.Size.NumberOfElements - 1, keepAlive ? null : null);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorFComplex32(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null);
            } else {
                return new Iterators.StridedIteratorFComplex64(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null);
            }
        }

       

        /// <summary>
        /// Efficient iterator over the array for use in foreach loops. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <param name="keepAlive">[Optional] do not release <paramref name="A"/> after iteration. Default: false (RetT typed A will be released).</param>
        /// <returns>Slim, self destructing iterator instance, iterating over all elements of this array.</returns>
        public unsafe static IEnumerable<complex> Iterator(
            this BaseArray<complex> A, 
            StorageOrders? order = null,
            bool keepAlive = false) {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'."); 
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, out var storage, releaseRetT : false); 

            complex* start = (complex*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if ((storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2) && storage.Size.IsContinuous) {
                return new Iterators.ContinuousIteratorComplex(start, start + storage.Size.NumberOfElements - 1, keepAlive ? null : null);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorComplex32(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null);
            } else {
                return new Iterators.StridedIteratorComplex64(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null);
            }
        }

       

        /// <summary>
        /// Efficient iterator over the array for use in foreach loops. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <param name="keepAlive">[Optional] do not release <paramref name="A"/> after iteration. Default: false (RetT typed A will be released).</param>
        /// <returns>Slim, self destructing iterator instance, iterating over all elements of this array.</returns>
        public unsafe static IEnumerable<float> Iterator(
            this BaseArray<float> A, 
            StorageOrders? order = null,
            bool keepAlive = false) {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'."); 
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, out var storage, releaseRetT : false); 

            float* start = (float*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if ((storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2) && storage.Size.IsContinuous) {
                return new Iterators.ContinuousIteratorSingle(start, start + storage.Size.NumberOfElements - 1, keepAlive ? null : null);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorSingle32(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null);
            } else {
                return new Iterators.StridedIteratorSingle64(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null);
            }
        }


#endregion HYCALPER AUTO GENERATED CODE

        /// <summary>
        /// Efficient iterator over the array for use in foreach loops. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <param name="keepAlive">[Optional] True: a volatile array type as source array <paramref name="A"/> will not be released after the itator finished enumerating <paramref name="A"/>s values. False: the iterator will release a volatile A (default).</param>
        /// <returns>Slim, self destructing iterator instance, iterating over all elements of this array.</returns>
        public unsafe static IEnumerable<bool> Iterator(
            this BaseArray<bool> A,
            StorageOrders? order = null,
            bool keepAlive = false) {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'.");
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            var storage = (A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>).Storage;
            byte* start = (byte*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if ((storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2) && storage.Size.IsContinuous) {
                return new Iterators.ContinuousIteratorBool(start, start + storage.Size.NumberOfElements - 1, null);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorBool32(start, storage.Size.GetBSD(write: false), order, null);
            } else {
                return new Iterators.StridedIteratorBool64(start, storage.Size.GetBSD(write: false), order, null);
            }
        }

        /// <summary>
        /// Efficient iterator over generic type elements for use in foreach loops. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <param name="keepAlive">[Optional] True: a volatile array type as source array <paramref name="A"/> will not be released after the itator finished enumerating <paramref name="A"/>s values. 
        /// False: the iterator will release volatile A (default).</param>
        /// <returns>Slim, self destructing iterator instance, iterating over all elements of this array.</returns>
        /// <typeparam name="T">Element type.</typeparam>
        public unsafe static IEnumerable<T> Iterator<T>(this BaseArray<T> A, StorageOrders? order = null, bool keepAlive = false) {

            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'.");
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            
            if (A is ConcreteArray<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>>) {

                // all native types, Logical
                var storage = (A as ConcreteArray<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>>).Storage;
                if (storage.Handles[0] is NativeHostHandle) {
                    // forward to existing iterators on struct / value element types
                    if (A is BaseArray<double>) {
                        return (A as BaseArray<double>)?.Iterator(order, keepAlive) as IEnumerable<T>;
                    } else if (A is BaseArray<float>) {
                        return (A as BaseArray<float>)?.Iterator(order, keepAlive) as IEnumerable<T>;
                    } else if (A is BaseArray<uint>) {
                        return (A as BaseArray<uint>)?.Iterator(order, keepAlive) as IEnumerable<T>;
                    } else if (A is BaseArray<int>) {
                        return (A as BaseArray<int>)?.Iterator(order, keepAlive) as IEnumerable<T>;
                    } else if (A is BaseArray<long>) {
                        return (A as BaseArray<long>)?.Iterator(order, keepAlive) as IEnumerable<T>;
                    } else if (A is BaseArray<ulong>) {
                        return (A as BaseArray<ulong>)?.Iterator(order, keepAlive) as IEnumerable<T>;
                    } else if (A is BaseArray<short>) {
                        return (A as BaseArray<short>)?.Iterator(order, keepAlive) as IEnumerable<T>;
                    } else if (A is BaseArray<ushort>) {
                        return (A as BaseArray<ushort>)?.Iterator(order, keepAlive) as IEnumerable<T>;
                    } else if (A is BaseArray<byte>) {
                        return (A as BaseArray<byte>)?.Iterator(order, keepAlive) as IEnumerable<T>;
                    } else if (A is BaseArray<sbyte>) {
                        return (A as BaseArray<sbyte>)?.Iterator(order, keepAlive) as IEnumerable<T>;
                    } else if (A is BaseArray<complex>) {
                        return (A as BaseArray<complex>)?.Iterator(order, keepAlive) as IEnumerable<T>;
                    } else if (A is BaseArray<fcomplex>) {
                        return (A as BaseArray<fcomplex>)?.Iterator(order, keepAlive) as IEnumerable<T>;
                    } else if (A is BaseArray<bool>) {
                        return (A as BaseArray<bool>)?.Iterator(order, keepAlive) as IEnumerable<T>;
                    }
                    throw new NotImplementedException("Use a concrete element type for accessing the element type specific overloads of Iterator<T> instead!");

                } else {
                    // all unknown reference types, including Array<string>: 
                    long start = storage.Size.BaseOffset;
                    T[] values = (storage.Handles[0] as ManagedHostHandle<T>).HostArray;
                    if ((storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2) && storage.Size.IsContinuous) {
                        return new Iterators.ContinuousIterator<T>(values, start, start + (long)storage.Size.NumberOfElements - 1, null);
                    } else if (IntPtr.Size == 4) {
                        return new Iterators.StridedIterator32<T>(values, (uint)start, storage.Size.GetBSD(write: false), order, null);
                    } else {
                        return new Iterators.StridedIterator64<T>(values, start, storage.Size.GetBSD(write: false), order, null);
                    }
                }
            } else if (typeof(T) == typeof(BaseArray)) { // A is Cell || A is InCell || A is OutCell) {
                var storage = (A as ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage>).Storage;
                long start = storage.Size.BaseOffset;
                IStorage[] values = (storage.Handles[0] as ManagedHostHandle<IStorage>).HostArray;
                return new Iterators.StridedCellIterator64(values, start, storage.Size.GetBSD(write: false), order, null) as IEnumerable<T>;
            } else if (A is ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>) {
                return (A as BaseArray<bool>)?.Iterator(order, keepAlive) as IEnumerable<T>;
            } else {
                throw new NotSupportedException($"This iterator does not support arrays of type '{A?.GetType()}'<{A?.GetElementType()}>");
            }
        }

        #region HYCALPER LOOPSTART [Internal] indices iterators over value elements, returning IIndexIterators
        /*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="here">
        Double
    </source>
    <destination>Single</destination>
    <destination>Complex</destination>
    <destination>FComplex</destination>
    <destination>ULong</destination>
    <destination>Long</destination>
    <destination>UInt</destination>
    <destination>Int32</destination>
    <destination>UShort</destination>
    <destination>Short</destination>
    <destination>Byte</destination>
    <destination>SByte</destination>
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
    <destination>uint</destination>
    <destination>int</destination>
    <destination>ushort</destination>
    <destination>short</destination>
    <destination>byte</destination>
    <destination>sbyte</destination>
</type>
</hycalper>
*/

        /// <summary>
        /// [Internal] Efficient iterator over the array for use in subarray evaluations. 
        /// </summary>
        /// <param name="lastDimensionIdx">Index of the last element in the dimension when the iterator is to be used in subarray operations.</param>
        /// <param name="A">The array instance.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <param name="checkLimits">[Optional] Checks limits and throws an IOOR exception. Default: true (do perform checks).</param>
        /// <param name="slim">[Optional] True: do not maintain features required for extended subarray-ing. Saves the determination
        /// <param name="keepAlive">[Optional] True: a volatile array type as source array <paramref name="A"/> will not be released after the itator finished enumerating <paramref name="A"/>s values. False: the iterator will release a volatile A (default).</param>
        /// of out-of-range indices, NaN checks etc. The object returned will not provide all <see cref="IIndexIterator"/> data! Default: false.</param>
        /// <returns>Efficient, self destructing iterator instance, iterating over all elements of this array.</returns>
        /// <remarks><paramref name="slim"/> is used to enable the iterator for scenarios where error checking is performed outside of the 
        /// iterator and focus is on speedy creation of the iterator. In subarray operations this is disabled and the more 'safe' variant 
        /// is used. When enabled (true), no OOR checks are performed and <paramref name="checkLimits"/> is ignored.</remarks>
        internal unsafe static IIndexIterator IndexIterator(
            this BaseArray<double> A,
            long lastDimensionIdx,
            StorageOrders? order = null,
            bool checkLimits = true, 
            bool slim = false,
            bool keepAlive = false) { 
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'.");
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            var storage = (A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>).Storage;

            double min, max;
            long? stepsize;
            if (slim) {
                min = 0; max = 0; stepsize = -1;
            } else {
                if (!storage.GetLimits(out min, out max)) {
                    if (storage.Size.NumberOfElements > 0) {
                        throw new ArgumentException("Invalid index specification. Indices must be or be convertible to integer (uint / long) values.");
                    } else {
                        min = unchecked((double)(-1));
                        max = unchecked((double)(-1));
                    }
                } else if (checkLimits && ((long)min < -lastDimensionIdx - 1 || (long)max > lastDimensionIdx)) {
                    throw new IndexOutOfRangeException($"Indices i must be in range {0} <= i <= {lastDimensionIdx}. Minimal index found: {min}. Maximal index found: {max}.");
                }
                switch (storage.S.NumberOfElements) {
                    case 2:
                        stepsize = (long)max - (long)min;
                        break;
                    case 1:
                    case 0:
                        stepsize = 1;
                        break;
                    default:
                        stepsize = null;
                        break;
                }
                //if (storage.S.NumberOfElements <= 2) {
                //    if (storage.S.NumberOfElements == 2) {
                //        stepsize = (long)max - (long)min;
                //    } else {
                //        stepsize = 1;
                //    }
                //} else {
                //    stepsize = null;
                //}
            }
            double* start = (double*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if ((storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2) && storage.Size.IsContinuous) {
                return new Iterators.ContinuousIteratorDoubleLong(start, start + storage.Size.NumberOfElements - 1, keepAlive ? null : null, lastDimensionIdx, (long)min, (long)max, stepsize);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorDoubleLong32(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null, (uint)lastDimensionIdx, (uint)min, (uint)max, (uint?)stepsize);
            } else {
                return new Iterators.StridedIteratorDoubleLong64(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null, lastDimensionIdx, (long)min, (long)max, stepsize);
            }
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        /// <summary>
        /// [Internal] Efficient iterator over the array for use in subarray evaluations. 
        /// </summary>
        /// <param name="lastDimensionIdx">Index of the last element in the dimension when the iterator is to be used in subarray operations.</param>
        /// <param name="A">The array instance.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <param name="checkLimits">[Optional] Checks limits and throws an IOOR exception. Default: true (do perform checks).</param>
        /// <param name="slim">[Optional] True: do not maintain features required for extended subarray-ing. Saves the determination
        /// <param name="keepAlive">[Optional] True: a volatile array type as source array <paramref name="A"/> will not be released after the itator finished enumerating <paramref name="A"/>s values. False: the iterator will release a volatile A (default).</param>
        /// of out-of-range indices, NaN checks etc. The object returned will not provide all <see cref="IIndexIterator"/> data! Default: false.</param>
        /// <returns>Efficient, self destructing iterator instance, iterating over all elements of this array.</returns>
        /// <remarks><paramref name="slim"/> is used to enable the iterator for scenarios where error checking is performed outside of the 
        /// iterator and focus is on speedy creation of the iterator. In subarray operations this is disabled and the more 'safe' variant 
        /// is used. When enabled (true), no OOR checks are performed and <paramref name="checkLimits"/> is ignored.</remarks>
        internal unsafe static IIndexIterator IndexIterator(
            this BaseArray<sbyte> A,
            long lastDimensionIdx,
            StorageOrders? order = null,
            bool checkLimits = true, 
            bool slim = false,
            bool keepAlive = false) { 
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'.");
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            var storage = (A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>).Storage;

            sbyte min, max;
            long? stepsize;
            if (slim) {
                min = 0; max = 0; stepsize = -1;
            } else {
                if (!storage.GetLimits(out min, out max)) {
                    if (storage.Size.NumberOfElements > 0) {
                        throw new ArgumentException("Invalid index specification. Indices must be or be convertible to integer (uint / long) values.");
                    } else {
                        min = unchecked((sbyte)(-1));
                        max = unchecked((sbyte)(-1));
                    }
                } else if (checkLimits && ((long)min < -lastDimensionIdx - 1 || (long)max > lastDimensionIdx)) {
                    throw new IndexOutOfRangeException($"Indices i must be in range {0} <= i <= {lastDimensionIdx}. Minimal index found: {min}. Maximal index found: {max}.");
                }
                switch (storage.S.NumberOfElements) {
                    case 2:
                        stepsize = (long)max - (long)min;
                        break;
                    case 1:
                    case 0:
                        stepsize = 1;
                        break;
                    default:
                        stepsize = null;
                        break;
                }
                //if (storage.S.NumberOfElements <= 2) {
                //    if (storage.S.NumberOfElements == 2) {
                //        stepsize = (long)max - (long)min;
                //    } else {
                //        stepsize = 1;
                //    }
                //} else {
                //    stepsize = null;
                //}
            }
            sbyte* start = (sbyte*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if ((storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2) && storage.Size.IsContinuous) {
                return new Iterators.ContinuousIteratorSByteLong(start, start + storage.Size.NumberOfElements - 1, keepAlive ? null : null, lastDimensionIdx, (long)min, (long)max, stepsize);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorSByteLong32(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null, (uint)lastDimensionIdx, (uint)min, (uint)max, (uint?)stepsize);
            } else {
                return new Iterators.StridedIteratorSByteLong64(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null, lastDimensionIdx, (long)min, (long)max, stepsize);
            }
        }
       

        /// <summary>
        /// [Internal] Efficient iterator over the array for use in subarray evaluations. 
        /// </summary>
        /// <param name="lastDimensionIdx">Index of the last element in the dimension when the iterator is to be used in subarray operations.</param>
        /// <param name="A">The array instance.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <param name="checkLimits">[Optional] Checks limits and throws an IOOR exception. Default: true (do perform checks).</param>
        /// <param name="slim">[Optional] True: do not maintain features required for extended subarray-ing. Saves the determination
        /// <param name="keepAlive">[Optional] True: a volatile array type as source array <paramref name="A"/> will not be released after the itator finished enumerating <paramref name="A"/>s values. False: the iterator will release a volatile A (default).</param>
        /// of out-of-range indices, NaN checks etc. The object returned will not provide all <see cref="IIndexIterator"/> data! Default: false.</param>
        /// <returns>Efficient, self destructing iterator instance, iterating over all elements of this array.</returns>
        /// <remarks><paramref name="slim"/> is used to enable the iterator for scenarios where error checking is performed outside of the 
        /// iterator and focus is on speedy creation of the iterator. In subarray operations this is disabled and the more 'safe' variant 
        /// is used. When enabled (true), no OOR checks are performed and <paramref name="checkLimits"/> is ignored.</remarks>
        internal unsafe static IIndexIterator IndexIterator(
            this BaseArray<byte> A,
            long lastDimensionIdx,
            StorageOrders? order = null,
            bool checkLimits = true, 
            bool slim = false,
            bool keepAlive = false) { 
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'.");
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            var storage = (A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>).Storage;

            byte min, max;
            long? stepsize;
            if (slim) {
                min = 0; max = 0; stepsize = -1;
            } else {
                if (!storage.GetLimits(out min, out max)) {
                    if (storage.Size.NumberOfElements > 0) {
                        throw new ArgumentException("Invalid index specification. Indices must be or be convertible to integer (uint / long) values.");
                    } else {
                        min = unchecked((byte)(-1));
                        max = unchecked((byte)(-1));
                    }
                } else if (checkLimits && ((long)min < -lastDimensionIdx - 1 || (long)max > lastDimensionIdx)) {
                    throw new IndexOutOfRangeException($"Indices i must be in range {0} <= i <= {lastDimensionIdx}. Minimal index found: {min}. Maximal index found: {max}.");
                }
                switch (storage.S.NumberOfElements) {
                    case 2:
                        stepsize = (long)max - (long)min;
                        break;
                    case 1:
                    case 0:
                        stepsize = 1;
                        break;
                    default:
                        stepsize = null;
                        break;
                }
                //if (storage.S.NumberOfElements <= 2) {
                //    if (storage.S.NumberOfElements == 2) {
                //        stepsize = (long)max - (long)min;
                //    } else {
                //        stepsize = 1;
                //    }
                //} else {
                //    stepsize = null;
                //}
            }
            byte* start = (byte*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if ((storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2) && storage.Size.IsContinuous) {
                return new Iterators.ContinuousIteratorByteLong(start, start + storage.Size.NumberOfElements - 1, keepAlive ? null : null, lastDimensionIdx, (long)min, (long)max, stepsize);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorByteLong32(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null, (uint)lastDimensionIdx, (uint)min, (uint)max, (uint?)stepsize);
            } else {
                return new Iterators.StridedIteratorByteLong64(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null, lastDimensionIdx, (long)min, (long)max, stepsize);
            }
        }
       

        /// <summary>
        /// [Internal] Efficient iterator over the array for use in subarray evaluations. 
        /// </summary>
        /// <param name="lastDimensionIdx">Index of the last element in the dimension when the iterator is to be used in subarray operations.</param>
        /// <param name="A">The array instance.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <param name="checkLimits">[Optional] Checks limits and throws an IOOR exception. Default: true (do perform checks).</param>
        /// <param name="slim">[Optional] True: do not maintain features required for extended subarray-ing. Saves the determination
        /// <param name="keepAlive">[Optional] True: a volatile array type as source array <paramref name="A"/> will not be released after the itator finished enumerating <paramref name="A"/>s values. False: the iterator will release a volatile A (default).</param>
        /// of out-of-range indices, NaN checks etc. The object returned will not provide all <see cref="IIndexIterator"/> data! Default: false.</param>
        /// <returns>Efficient, self destructing iterator instance, iterating over all elements of this array.</returns>
        /// <remarks><paramref name="slim"/> is used to enable the iterator for scenarios where error checking is performed outside of the 
        /// iterator and focus is on speedy creation of the iterator. In subarray operations this is disabled and the more 'safe' variant 
        /// is used. When enabled (true), no OOR checks are performed and <paramref name="checkLimits"/> is ignored.</remarks>
        internal unsafe static IIndexIterator IndexIterator(
            this BaseArray<short> A,
            long lastDimensionIdx,
            StorageOrders? order = null,
            bool checkLimits = true, 
            bool slim = false,
            bool keepAlive = false) { 
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'.");
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            var storage = (A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>).Storage;

            short min, max;
            long? stepsize;
            if (slim) {
                min = 0; max = 0; stepsize = -1;
            } else {
                if (!storage.GetLimits(out min, out max)) {
                    if (storage.Size.NumberOfElements > 0) {
                        throw new ArgumentException("Invalid index specification. Indices must be or be convertible to integer (uint / long) values.");
                    } else {
                        min = unchecked((short)(-1));
                        max = unchecked((short)(-1));
                    }
                } else if (checkLimits && ((long)min < -lastDimensionIdx - 1 || (long)max > lastDimensionIdx)) {
                    throw new IndexOutOfRangeException($"Indices i must be in range {0} <= i <= {lastDimensionIdx}. Minimal index found: {min}. Maximal index found: {max}.");
                }
                switch (storage.S.NumberOfElements) {
                    case 2:
                        stepsize = (long)max - (long)min;
                        break;
                    case 1:
                    case 0:
                        stepsize = 1;
                        break;
                    default:
                        stepsize = null;
                        break;
                }
                //if (storage.S.NumberOfElements <= 2) {
                //    if (storage.S.NumberOfElements == 2) {
                //        stepsize = (long)max - (long)min;
                //    } else {
                //        stepsize = 1;
                //    }
                //} else {
                //    stepsize = null;
                //}
            }
            short* start = (short*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if ((storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2) && storage.Size.IsContinuous) {
                return new Iterators.ContinuousIteratorShortLong(start, start + storage.Size.NumberOfElements - 1, keepAlive ? null : null, lastDimensionIdx, (long)min, (long)max, stepsize);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorShortLong32(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null, (uint)lastDimensionIdx, (uint)min, (uint)max, (uint?)stepsize);
            } else {
                return new Iterators.StridedIteratorShortLong64(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null, lastDimensionIdx, (long)min, (long)max, stepsize);
            }
        }
       

        /// <summary>
        /// [Internal] Efficient iterator over the array for use in subarray evaluations. 
        /// </summary>
        /// <param name="lastDimensionIdx">Index of the last element in the dimension when the iterator is to be used in subarray operations.</param>
        /// <param name="A">The array instance.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <param name="checkLimits">[Optional] Checks limits and throws an IOOR exception. Default: true (do perform checks).</param>
        /// <param name="slim">[Optional] True: do not maintain features required for extended subarray-ing. Saves the determination
        /// <param name="keepAlive">[Optional] True: a volatile array type as source array <paramref name="A"/> will not be released after the itator finished enumerating <paramref name="A"/>s values. False: the iterator will release a volatile A (default).</param>
        /// of out-of-range indices, NaN checks etc. The object returned will not provide all <see cref="IIndexIterator"/> data! Default: false.</param>
        /// <returns>Efficient, self destructing iterator instance, iterating over all elements of this array.</returns>
        /// <remarks><paramref name="slim"/> is used to enable the iterator for scenarios where error checking is performed outside of the 
        /// iterator and focus is on speedy creation of the iterator. In subarray operations this is disabled and the more 'safe' variant 
        /// is used. When enabled (true), no OOR checks are performed and <paramref name="checkLimits"/> is ignored.</remarks>
        internal unsafe static IIndexIterator IndexIterator(
            this BaseArray<ushort> A,
            long lastDimensionIdx,
            StorageOrders? order = null,
            bool checkLimits = true, 
            bool slim = false,
            bool keepAlive = false) { 
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'.");
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            var storage = (A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>).Storage;

            ushort min, max;
            long? stepsize;
            if (slim) {
                min = 0; max = 0; stepsize = -1;
            } else {
                if (!storage.GetLimits(out min, out max)) {
                    if (storage.Size.NumberOfElements > 0) {
                        throw new ArgumentException("Invalid index specification. Indices must be or be convertible to integer (uint / long) values.");
                    } else {
                        min = unchecked((ushort)(-1));
                        max = unchecked((ushort)(-1));
                    }
                } else if (checkLimits && ((long)min < -lastDimensionIdx - 1 || (long)max > lastDimensionIdx)) {
                    throw new IndexOutOfRangeException($"Indices i must be in range {0} <= i <= {lastDimensionIdx}. Minimal index found: {min}. Maximal index found: {max}.");
                }
                switch (storage.S.NumberOfElements) {
                    case 2:
                        stepsize = (long)max - (long)min;
                        break;
                    case 1:
                    case 0:
                        stepsize = 1;
                        break;
                    default:
                        stepsize = null;
                        break;
                }
                //if (storage.S.NumberOfElements <= 2) {
                //    if (storage.S.NumberOfElements == 2) {
                //        stepsize = (long)max - (long)min;
                //    } else {
                //        stepsize = 1;
                //    }
                //} else {
                //    stepsize = null;
                //}
            }
            ushort* start = (ushort*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if ((storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2) && storage.Size.IsContinuous) {
                return new Iterators.ContinuousIteratorUShortLong(start, start + storage.Size.NumberOfElements - 1, keepAlive ? null : null, lastDimensionIdx, (long)min, (long)max, stepsize);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorUShortLong32(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null, (uint)lastDimensionIdx, (uint)min, (uint)max, (uint?)stepsize);
            } else {
                return new Iterators.StridedIteratorUShortLong64(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null, lastDimensionIdx, (long)min, (long)max, stepsize);
            }
        }
       

        /// <summary>
        /// [Internal] Efficient iterator over the array for use in subarray evaluations. 
        /// </summary>
        /// <param name="lastDimensionIdx">Index of the last element in the dimension when the iterator is to be used in subarray operations.</param>
        /// <param name="A">The array instance.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <param name="checkLimits">[Optional] Checks limits and throws an IOOR exception. Default: true (do perform checks).</param>
        /// <param name="slim">[Optional] True: do not maintain features required for extended subarray-ing. Saves the determination
        /// <param name="keepAlive">[Optional] True: a volatile array type as source array <paramref name="A"/> will not be released after the itator finished enumerating <paramref name="A"/>s values. False: the iterator will release a volatile A (default).</param>
        /// of out-of-range indices, NaN checks etc. The object returned will not provide all <see cref="IIndexIterator"/> data! Default: false.</param>
        /// <returns>Efficient, self destructing iterator instance, iterating over all elements of this array.</returns>
        /// <remarks><paramref name="slim"/> is used to enable the iterator for scenarios where error checking is performed outside of the 
        /// iterator and focus is on speedy creation of the iterator. In subarray operations this is disabled and the more 'safe' variant 
        /// is used. When enabled (true), no OOR checks are performed and <paramref name="checkLimits"/> is ignored.</remarks>
        internal unsafe static IIndexIterator IndexIterator(
            this BaseArray<int> A,
            long lastDimensionIdx,
            StorageOrders? order = null,
            bool checkLimits = true, 
            bool slim = false,
            bool keepAlive = false) { 
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'.");
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            var storage = (A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>).Storage;

            int min, max;
            long? stepsize;
            if (slim) {
                min = 0; max = 0; stepsize = -1;
            } else {
                if (!storage.GetLimits(out min, out max)) {
                    if (storage.Size.NumberOfElements > 0) {
                        throw new ArgumentException("Invalid index specification. Indices must be or be convertible to integer (uint / long) values.");
                    } else {
                        min = unchecked((int)(-1));
                        max = unchecked((int)(-1));
                    }
                } else if (checkLimits && ((long)min < -lastDimensionIdx - 1 || (long)max > lastDimensionIdx)) {
                    throw new IndexOutOfRangeException($"Indices i must be in range {0} <= i <= {lastDimensionIdx}. Minimal index found: {min}. Maximal index found: {max}.");
                }
                switch (storage.S.NumberOfElements) {
                    case 2:
                        stepsize = (long)max - (long)min;
                        break;
                    case 1:
                    case 0:
                        stepsize = 1;
                        break;
                    default:
                        stepsize = null;
                        break;
                }
                //if (storage.S.NumberOfElements <= 2) {
                //    if (storage.S.NumberOfElements == 2) {
                //        stepsize = (long)max - (long)min;
                //    } else {
                //        stepsize = 1;
                //    }
                //} else {
                //    stepsize = null;
                //}
            }
            int* start = (int*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if ((storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2) && storage.Size.IsContinuous) {
                return new Iterators.ContinuousIteratorInt32Long(start, start + storage.Size.NumberOfElements - 1, keepAlive ? null : null, lastDimensionIdx, (long)min, (long)max, stepsize);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorInt32Long32(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null, (uint)lastDimensionIdx, (uint)min, (uint)max, (uint?)stepsize);
            } else {
                return new Iterators.StridedIteratorInt32Long64(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null, lastDimensionIdx, (long)min, (long)max, stepsize);
            }
        }
       

        /// <summary>
        /// [Internal] Efficient iterator over the array for use in subarray evaluations. 
        /// </summary>
        /// <param name="lastDimensionIdx">Index of the last element in the dimension when the iterator is to be used in subarray operations.</param>
        /// <param name="A">The array instance.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <param name="checkLimits">[Optional] Checks limits and throws an IOOR exception. Default: true (do perform checks).</param>
        /// <param name="slim">[Optional] True: do not maintain features required for extended subarray-ing. Saves the determination
        /// <param name="keepAlive">[Optional] True: a volatile array type as source array <paramref name="A"/> will not be released after the itator finished enumerating <paramref name="A"/>s values. False: the iterator will release a volatile A (default).</param>
        /// of out-of-range indices, NaN checks etc. The object returned will not provide all <see cref="IIndexIterator"/> data! Default: false.</param>
        /// <returns>Efficient, self destructing iterator instance, iterating over all elements of this array.</returns>
        /// <remarks><paramref name="slim"/> is used to enable the iterator for scenarios where error checking is performed outside of the 
        /// iterator and focus is on speedy creation of the iterator. In subarray operations this is disabled and the more 'safe' variant 
        /// is used. When enabled (true), no OOR checks are performed and <paramref name="checkLimits"/> is ignored.</remarks>
        internal unsafe static IIndexIterator IndexIterator(
            this BaseArray<uint> A,
            long lastDimensionIdx,
            StorageOrders? order = null,
            bool checkLimits = true, 
            bool slim = false,
            bool keepAlive = false) { 
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'.");
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            var storage = (A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>).Storage;

            uint min, max;
            long? stepsize;
            if (slim) {
                min = 0; max = 0; stepsize = -1;
            } else {
                if (!storage.GetLimits(out min, out max)) {
                    if (storage.Size.NumberOfElements > 0) {
                        throw new ArgumentException("Invalid index specification. Indices must be or be convertible to integer (uint / long) values.");
                    } else {
                        min = unchecked((uint)(-1));
                        max = unchecked((uint)(-1));
                    }
                } else if (checkLimits && ((long)min < -lastDimensionIdx - 1 || (long)max > lastDimensionIdx)) {
                    throw new IndexOutOfRangeException($"Indices i must be in range {0} <= i <= {lastDimensionIdx}. Minimal index found: {min}. Maximal index found: {max}.");
                }
                switch (storage.S.NumberOfElements) {
                    case 2:
                        stepsize = (long)max - (long)min;
                        break;
                    case 1:
                    case 0:
                        stepsize = 1;
                        break;
                    default:
                        stepsize = null;
                        break;
                }
                //if (storage.S.NumberOfElements <= 2) {
                //    if (storage.S.NumberOfElements == 2) {
                //        stepsize = (long)max - (long)min;
                //    } else {
                //        stepsize = 1;
                //    }
                //} else {
                //    stepsize = null;
                //}
            }
            uint* start = (uint*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if ((storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2) && storage.Size.IsContinuous) {
                return new Iterators.ContinuousIteratorUIntLong(start, start + storage.Size.NumberOfElements - 1, keepAlive ? null : null, lastDimensionIdx, (long)min, (long)max, stepsize);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorUIntLong32(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null, (uint)lastDimensionIdx, (uint)min, (uint)max, (uint?)stepsize);
            } else {
                return new Iterators.StridedIteratorUIntLong64(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null, lastDimensionIdx, (long)min, (long)max, stepsize);
            }
        }
       

        /// <summary>
        /// [Internal] Efficient iterator over the array for use in subarray evaluations. 
        /// </summary>
        /// <param name="lastDimensionIdx">Index of the last element in the dimension when the iterator is to be used in subarray operations.</param>
        /// <param name="A">The array instance.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <param name="checkLimits">[Optional] Checks limits and throws an IOOR exception. Default: true (do perform checks).</param>
        /// <param name="slim">[Optional] True: do not maintain features required for extended subarray-ing. Saves the determination
        /// <param name="keepAlive">[Optional] True: a volatile array type as source array <paramref name="A"/> will not be released after the itator finished enumerating <paramref name="A"/>s values. False: the iterator will release a volatile A (default).</param>
        /// of out-of-range indices, NaN checks etc. The object returned will not provide all <see cref="IIndexIterator"/> data! Default: false.</param>
        /// <returns>Efficient, self destructing iterator instance, iterating over all elements of this array.</returns>
        /// <remarks><paramref name="slim"/> is used to enable the iterator for scenarios where error checking is performed outside of the 
        /// iterator and focus is on speedy creation of the iterator. In subarray operations this is disabled and the more 'safe' variant 
        /// is used. When enabled (true), no OOR checks are performed and <paramref name="checkLimits"/> is ignored.</remarks>
        internal unsafe static IIndexIterator IndexIterator(
            this BaseArray<long> A,
            long lastDimensionIdx,
            StorageOrders? order = null,
            bool checkLimits = true, 
            bool slim = false,
            bool keepAlive = false) { 
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'.");
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            var storage = (A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>).Storage;

            long min, max;
            long? stepsize;
            if (slim) {
                min = 0; max = 0; stepsize = -1;
            } else {
                if (!storage.GetLimits(out min, out max)) {
                    if (storage.Size.NumberOfElements > 0) {
                        throw new ArgumentException("Invalid index specification. Indices must be or be convertible to integer (uint / long) values.");
                    } else {
                        min = unchecked((long)(-1));
                        max = unchecked((long)(-1));
                    }
                } else if (checkLimits && ((long)min < -lastDimensionIdx - 1 || (long)max > lastDimensionIdx)) {
                    throw new IndexOutOfRangeException($"Indices i must be in range {0} <= i <= {lastDimensionIdx}. Minimal index found: {min}. Maximal index found: {max}.");
                }
                switch (storage.S.NumberOfElements) {
                    case 2:
                        stepsize = (long)max - (long)min;
                        break;
                    case 1:
                    case 0:
                        stepsize = 1;
                        break;
                    default:
                        stepsize = null;
                        break;
                }
                //if (storage.S.NumberOfElements <= 2) {
                //    if (storage.S.NumberOfElements == 2) {
                //        stepsize = (long)max - (long)min;
                //    } else {
                //        stepsize = 1;
                //    }
                //} else {
                //    stepsize = null;
                //}
            }
            long* start = (long*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if ((storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2) && storage.Size.IsContinuous) {
                return new Iterators.ContinuousIteratorLongLong(start, start + storage.Size.NumberOfElements - 1, keepAlive ? null : null, lastDimensionIdx, (long)min, (long)max, stepsize);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorLongLong32(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null, (uint)lastDimensionIdx, (uint)min, (uint)max, (uint?)stepsize);
            } else {
                return new Iterators.StridedIteratorLongLong64(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null, lastDimensionIdx, (long)min, (long)max, stepsize);
            }
        }
       

        /// <summary>
        /// [Internal] Efficient iterator over the array for use in subarray evaluations. 
        /// </summary>
        /// <param name="lastDimensionIdx">Index of the last element in the dimension when the iterator is to be used in subarray operations.</param>
        /// <param name="A">The array instance.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <param name="checkLimits">[Optional] Checks limits and throws an IOOR exception. Default: true (do perform checks).</param>
        /// <param name="slim">[Optional] True: do not maintain features required for extended subarray-ing. Saves the determination
        /// <param name="keepAlive">[Optional] True: a volatile array type as source array <paramref name="A"/> will not be released after the itator finished enumerating <paramref name="A"/>s values. False: the iterator will release a volatile A (default).</param>
        /// of out-of-range indices, NaN checks etc. The object returned will not provide all <see cref="IIndexIterator"/> data! Default: false.</param>
        /// <returns>Efficient, self destructing iterator instance, iterating over all elements of this array.</returns>
        /// <remarks><paramref name="slim"/> is used to enable the iterator for scenarios where error checking is performed outside of the 
        /// iterator and focus is on speedy creation of the iterator. In subarray operations this is disabled and the more 'safe' variant 
        /// is used. When enabled (true), no OOR checks are performed and <paramref name="checkLimits"/> is ignored.</remarks>
        internal unsafe static IIndexIterator IndexIterator(
            this BaseArray<ulong> A,
            long lastDimensionIdx,
            StorageOrders? order = null,
            bool checkLimits = true, 
            bool slim = false,
            bool keepAlive = false) { 
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'.");
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            var storage = (A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>).Storage;

            ulong min, max;
            long? stepsize;
            if (slim) {
                min = 0; max = 0; stepsize = -1;
            } else {
                if (!storage.GetLimits(out min, out max)) {
                    if (storage.Size.NumberOfElements > 0) {
                        throw new ArgumentException("Invalid index specification. Indices must be or be convertible to integer (uint / long) values.");
                    } else {
                        min = unchecked((ulong)(-1));
                        max = unchecked((ulong)(-1));
                    }
                } else if (checkLimits && ((long)min < -lastDimensionIdx - 1 || (long)max > lastDimensionIdx)) {
                    throw new IndexOutOfRangeException($"Indices i must be in range {0} <= i <= {lastDimensionIdx}. Minimal index found: {min}. Maximal index found: {max}.");
                }
                switch (storage.S.NumberOfElements) {
                    case 2:
                        stepsize = (long)max - (long)min;
                        break;
                    case 1:
                    case 0:
                        stepsize = 1;
                        break;
                    default:
                        stepsize = null;
                        break;
                }
                //if (storage.S.NumberOfElements <= 2) {
                //    if (storage.S.NumberOfElements == 2) {
                //        stepsize = (long)max - (long)min;
                //    } else {
                //        stepsize = 1;
                //    }
                //} else {
                //    stepsize = null;
                //}
            }
            ulong* start = (ulong*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if ((storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2) && storage.Size.IsContinuous) {
                return new Iterators.ContinuousIteratorULongLong(start, start + storage.Size.NumberOfElements - 1, keepAlive ? null : null, lastDimensionIdx, (long)min, (long)max, stepsize);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorULongLong32(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null, (uint)lastDimensionIdx, (uint)min, (uint)max, (uint?)stepsize);
            } else {
                return new Iterators.StridedIteratorULongLong64(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null, lastDimensionIdx, (long)min, (long)max, stepsize);
            }
        }
       

        /// <summary>
        /// [Internal] Efficient iterator over the array for use in subarray evaluations. 
        /// </summary>
        /// <param name="lastDimensionIdx">Index of the last element in the dimension when the iterator is to be used in subarray operations.</param>
        /// <param name="A">The array instance.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <param name="checkLimits">[Optional] Checks limits and throws an IOOR exception. Default: true (do perform checks).</param>
        /// <param name="slim">[Optional] True: do not maintain features required for extended subarray-ing. Saves the determination
        /// <param name="keepAlive">[Optional] True: a volatile array type as source array <paramref name="A"/> will not be released after the itator finished enumerating <paramref name="A"/>s values. False: the iterator will release a volatile A (default).</param>
        /// of out-of-range indices, NaN checks etc. The object returned will not provide all <see cref="IIndexIterator"/> data! Default: false.</param>
        /// <returns>Efficient, self destructing iterator instance, iterating over all elements of this array.</returns>
        /// <remarks><paramref name="slim"/> is used to enable the iterator for scenarios where error checking is performed outside of the 
        /// iterator and focus is on speedy creation of the iterator. In subarray operations this is disabled and the more 'safe' variant 
        /// is used. When enabled (true), no OOR checks are performed and <paramref name="checkLimits"/> is ignored.</remarks>
        internal unsafe static IIndexIterator IndexIterator(
            this BaseArray<fcomplex> A,
            long lastDimensionIdx,
            StorageOrders? order = null,
            bool checkLimits = true, 
            bool slim = false,
            bool keepAlive = false) { 
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'.");
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            var storage = (A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>).Storage;

            fcomplex min, max;
            long? stepsize;
            if (slim) {
                min = 0; max = 0; stepsize = -1;
            } else {
                if (!storage.GetLimits(out min, out max)) {
                    if (storage.Size.NumberOfElements > 0) {
                        throw new ArgumentException("Invalid index specification. Indices must be or be convertible to integer (uint / long) values.");
                    } else {
                        min = unchecked((fcomplex)(-1));
                        max = unchecked((fcomplex)(-1));
                    }
                } else if (checkLimits && ((long)min < -lastDimensionIdx - 1 || (long)max > lastDimensionIdx)) {
                    throw new IndexOutOfRangeException($"Indices i must be in range {0} <= i <= {lastDimensionIdx}. Minimal index found: {min}. Maximal index found: {max}.");
                }
                switch (storage.S.NumberOfElements) {
                    case 2:
                        stepsize = (long)max - (long)min;
                        break;
                    case 1:
                    case 0:
                        stepsize = 1;
                        break;
                    default:
                        stepsize = null;
                        break;
                }
                //if (storage.S.NumberOfElements <= 2) {
                //    if (storage.S.NumberOfElements == 2) {
                //        stepsize = (long)max - (long)min;
                //    } else {
                //        stepsize = 1;
                //    }
                //} else {
                //    stepsize = null;
                //}
            }
            fcomplex* start = (fcomplex*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if ((storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2) && storage.Size.IsContinuous) {
                return new Iterators.ContinuousIteratorFComplexLong(start, start + storage.Size.NumberOfElements - 1, keepAlive ? null : null, lastDimensionIdx, (long)min, (long)max, stepsize);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorFComplexLong32(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null, (uint)lastDimensionIdx, (uint)min, (uint)max, (uint?)stepsize);
            } else {
                return new Iterators.StridedIteratorFComplexLong64(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null, lastDimensionIdx, (long)min, (long)max, stepsize);
            }
        }
       

        /// <summary>
        /// [Internal] Efficient iterator over the array for use in subarray evaluations. 
        /// </summary>
        /// <param name="lastDimensionIdx">Index of the last element in the dimension when the iterator is to be used in subarray operations.</param>
        /// <param name="A">The array instance.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <param name="checkLimits">[Optional] Checks limits and throws an IOOR exception. Default: true (do perform checks).</param>
        /// <param name="slim">[Optional] True: do not maintain features required for extended subarray-ing. Saves the determination
        /// <param name="keepAlive">[Optional] True: a volatile array type as source array <paramref name="A"/> will not be released after the itator finished enumerating <paramref name="A"/>s values. False: the iterator will release a volatile A (default).</param>
        /// of out-of-range indices, NaN checks etc. The object returned will not provide all <see cref="IIndexIterator"/> data! Default: false.</param>
        /// <returns>Efficient, self destructing iterator instance, iterating over all elements of this array.</returns>
        /// <remarks><paramref name="slim"/> is used to enable the iterator for scenarios where error checking is performed outside of the 
        /// iterator and focus is on speedy creation of the iterator. In subarray operations this is disabled and the more 'safe' variant 
        /// is used. When enabled (true), no OOR checks are performed and <paramref name="checkLimits"/> is ignored.</remarks>
        internal unsafe static IIndexIterator IndexIterator(
            this BaseArray<complex> A,
            long lastDimensionIdx,
            StorageOrders? order = null,
            bool checkLimits = true, 
            bool slim = false,
            bool keepAlive = false) { 
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'.");
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            var storage = (A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>).Storage;

            complex min, max;
            long? stepsize;
            if (slim) {
                min = 0; max = 0; stepsize = -1;
            } else {
                if (!storage.GetLimits(out min, out max)) {
                    if (storage.Size.NumberOfElements > 0) {
                        throw new ArgumentException("Invalid index specification. Indices must be or be convertible to integer (uint / long) values.");
                    } else {
                        min = unchecked((complex)(-1));
                        max = unchecked((complex)(-1));
                    }
                } else if (checkLimits && ((long)min < -lastDimensionIdx - 1 || (long)max > lastDimensionIdx)) {
                    throw new IndexOutOfRangeException($"Indices i must be in range {0} <= i <= {lastDimensionIdx}. Minimal index found: {min}. Maximal index found: {max}.");
                }
                switch (storage.S.NumberOfElements) {
                    case 2:
                        stepsize = (long)max - (long)min;
                        break;
                    case 1:
                    case 0:
                        stepsize = 1;
                        break;
                    default:
                        stepsize = null;
                        break;
                }
                //if (storage.S.NumberOfElements <= 2) {
                //    if (storage.S.NumberOfElements == 2) {
                //        stepsize = (long)max - (long)min;
                //    } else {
                //        stepsize = 1;
                //    }
                //} else {
                //    stepsize = null;
                //}
            }
            complex* start = (complex*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if ((storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2) && storage.Size.IsContinuous) {
                return new Iterators.ContinuousIteratorComplexLong(start, start + storage.Size.NumberOfElements - 1, keepAlive ? null : null, lastDimensionIdx, (long)min, (long)max, stepsize);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorComplexLong32(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null, (uint)lastDimensionIdx, (uint)min, (uint)max, (uint?)stepsize);
            } else {
                return new Iterators.StridedIteratorComplexLong64(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null, lastDimensionIdx, (long)min, (long)max, stepsize);
            }
        }
       

        /// <summary>
        /// [Internal] Efficient iterator over the array for use in subarray evaluations. 
        /// </summary>
        /// <param name="lastDimensionIdx">Index of the last element in the dimension when the iterator is to be used in subarray operations.</param>
        /// <param name="A">The array instance.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <param name="checkLimits">[Optional] Checks limits and throws an IOOR exception. Default: true (do perform checks).</param>
        /// <param name="slim">[Optional] True: do not maintain features required for extended subarray-ing. Saves the determination
        /// <param name="keepAlive">[Optional] True: a volatile array type as source array <paramref name="A"/> will not be released after the itator finished enumerating <paramref name="A"/>s values. False: the iterator will release a volatile A (default).</param>
        /// of out-of-range indices, NaN checks etc. The object returned will not provide all <see cref="IIndexIterator"/> data! Default: false.</param>
        /// <returns>Efficient, self destructing iterator instance, iterating over all elements of this array.</returns>
        /// <remarks><paramref name="slim"/> is used to enable the iterator for scenarios where error checking is performed outside of the 
        /// iterator and focus is on speedy creation of the iterator. In subarray operations this is disabled and the more 'safe' variant 
        /// is used. When enabled (true), no OOR checks are performed and <paramref name="checkLimits"/> is ignored.</remarks>
        internal unsafe static IIndexIterator IndexIterator(
            this BaseArray<float> A,
            long lastDimensionIdx,
            StorageOrders? order = null,
            bool checkLimits = true, 
            bool slim = false,
            bool keepAlive = false) { 
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'.");
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            var storage = (A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>).Storage;

            float min, max;
            long? stepsize;
            if (slim) {
                min = 0; max = 0; stepsize = -1;
            } else {
                if (!storage.GetLimits(out min, out max)) {
                    if (storage.Size.NumberOfElements > 0) {
                        throw new ArgumentException("Invalid index specification. Indices must be or be convertible to integer (uint / long) values.");
                    } else {
                        min = unchecked((float)(-1));
                        max = unchecked((float)(-1));
                    }
                } else if (checkLimits && ((long)min < -lastDimensionIdx - 1 || (long)max > lastDimensionIdx)) {
                    throw new IndexOutOfRangeException($"Indices i must be in range {0} <= i <= {lastDimensionIdx}. Minimal index found: {min}. Maximal index found: {max}.");
                }
                switch (storage.S.NumberOfElements) {
                    case 2:
                        stepsize = (long)max - (long)min;
                        break;
                    case 1:
                    case 0:
                        stepsize = 1;
                        break;
                    default:
                        stepsize = null;
                        break;
                }
                //if (storage.S.NumberOfElements <= 2) {
                //    if (storage.S.NumberOfElements == 2) {
                //        stepsize = (long)max - (long)min;
                //    } else {
                //        stepsize = 1;
                //    }
                //} else {
                //    stepsize = null;
                //}
            }
            float* start = (float*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            if ((storage.Size.StorageOrder == order || storage.Size.NonSingletonDimensions < 2) && storage.Size.IsContinuous) {
                return new Iterators.ContinuousIteratorSingleLong(start, start + storage.Size.NumberOfElements - 1, keepAlive ? null : null, lastDimensionIdx, (long)min, (long)max, stepsize);
            } else if (IntPtr.Size == 4) {
                return new Iterators.StridedIteratorSingleLong32(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null, (uint)lastDimensionIdx, (uint)min, (uint)max, (uint?)stepsize);
            } else {
                return new Iterators.StridedIteratorSingleLong64(start, storage.Size.GetBSD(write: false), order, keepAlive ? null : null, lastDimensionIdx, (long)min, (long)max, stepsize);
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

        #region generic iterator for elements. 

        /// <summary>
        /// Efficient iterator over generic (but numeric!) type elements for use in indexing. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="lastDimIdx">The value of the highest index addressable by this iterator. This is used to handle negative indices.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <param name="keepAlive">[Optional] True: a volatile array type as source array <paramref name="A"/> will not be released after the itator finished enumerating <paramref name="A"/>s values. False: the iterator will release a volatile A (default).</param>
        /// <returns>Slim, self destructing iterator instance, iterating over all elements of this array and converting values to long indices.</returns>
        /// <typeparam name="T">Element type. Expected to be numeric.</typeparam>
        public unsafe static IEnumerable<long> IndexIterator<T>(this BaseArray<T> A, long lastDimIdx, StorageOrders? order = null, bool keepAlive = false) {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'.");
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            var storage = (A as ConcreteArray<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>>).Storage;
            long start = storage.Size.BaseOffset;
            // forward to existing iterators on struct / value element types
            if (A is BaseArray<double>) {
                return (A as BaseArray<double>)?.IndexIterator(lastDimIdx, order,false, true, keepAlive: keepAlive) as IEnumerable<long>;
            } else if (A is BaseArray<float>) {
                return (A as BaseArray<float>)?.IndexIterator(lastDimIdx, order, false, true, keepAlive: keepAlive) as IEnumerable<long>;
            } else if (A is BaseArray<uint>) {
                return (A as BaseArray<uint>)?.IndexIterator(lastDimIdx, order, false, true, keepAlive: keepAlive) as IEnumerable<long>;
            } else if (A is BaseArray<int>) {
                return (A as BaseArray<int>)?.IndexIterator(lastDimIdx, order, false, true, keepAlive: keepAlive) as IEnumerable<long>;
            } else if (A is BaseArray<long>) {
                return (A as BaseArray<long>)?.IndexIterator(lastDimIdx, order, false, true, keepAlive: keepAlive) as IEnumerable<long>;
            } else if (A is BaseArray<ulong>) {
                return (A as BaseArray<ulong>)?.IndexIterator(lastDimIdx, order, false, true, keepAlive: keepAlive) as IEnumerable<long>;
            } else if (A is BaseArray<short>) {
                return (A as BaseArray<short>)?.IndexIterator(lastDimIdx, order, false, true, keepAlive: keepAlive) as IEnumerable<long>;
            } else if (A is BaseArray<ushort>) {
                return (A as BaseArray<ushort>)?.IndexIterator(lastDimIdx, order, false, true, keepAlive: keepAlive) as IEnumerable<long>;
            } else if (A is BaseArray<byte>) {
                return (A as BaseArray<byte>)?.IndexIterator(lastDimIdx, order, false, true, keepAlive: keepAlive) as IEnumerable<long>;
            } else if (A is BaseArray<sbyte>) {
                return (A as BaseArray<sbyte>)?.IndexIterator(lastDimIdx, order, false, true, keepAlive: keepAlive) as IEnumerable<long>;
            } else if (A is BaseArray<complex>) {
                return (A as BaseArray<complex>)?.IndexIterator(lastDimIdx, order, false, true, keepAlive: keepAlive) as IEnumerable<long>;
            } else if (A is BaseArray<fcomplex>) {
                return (A as BaseArray<fcomplex>)?.IndexIterator(lastDimIdx, order, false, true, keepAlive: keepAlive) as IEnumerable<long>;
            }
            throw new NotImplementedException("This element type is not supported in indexing iterators! Use A.Iterator<T>() instead!"); 
        }
        #endregion

        #region Logical array sequential index iterator
        /// <summary>
        /// [Internal] Efficient iterator over a logical array for use in subarray evaluations. Column-major order iteration!
        /// </summary>
        /// <param name="lastDimensionIdx">Index of the last element in the dimension when the iterator is to be used in subarray operations.</param>
        /// <param name="A">The array instance.</param>
        /// <param name="knownMaxValue">[optional] If provided this value will be cached and transfered to requests for <see cref="IIndexIterator.GetMaximum()"/> later.</param>
        /// <param name="checkLimits">[Optional] Checks limits and throws an IOOR exception. Default: true (do perform checks).</param>
        /// <param name="keepAlive">[Optional] True: a volatile array type as source array <paramref name="A"/> will not be released after the itator finished enumerating <paramref name="A"/>s values. False: the iterator will release a volatile A (default).</param>
        /// <returns>Slim, self destructing iterator instance, iterating over all elements of this array.</returns>
        internal unsafe static IIndexIterator IndexIterator(
            this BaseArray<bool> A,
            long lastDimensionIdx,
            bool checkLimits = true,
            long knownMaxValue = -1,
            bool keepAlive = false) {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'.");
            }
            var storage = (A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>).Storage;
            var size = storage.S; 

            if (checkLimits && ((long)size.NumberOfElements > lastDimensionIdx + 1)) {
                throw new IndexOutOfRangeException($"The number of elements in a logical index array must not exceed the number of elements in the corresponding dimension ({lastDimensionIdx + 1}). Found logical of size: {size.ToString()}");
            }
            byte* start = (byte*)storage.Handles[0].Pointer + storage.Size.BaseOffset;
            var bsd = size.GetBSD(false);
            if (size.NumberOfDimensions == 0) {
                return new Iterators.BoolIndexIterator_ML(start, 1, bsd + 1, bsd + 1, bsd + 3, lastDimensionIdx, keepAlive ? null : null, knownMaxValue, storage.NumberTrues);
            } else if (size.NumberOfElements == 0) {
                return new Iterators.BoolIndexIterator_ML(start, 1, bsd + 1, bsd + 1, null, lastDimensionIdx, keepAlive ? null : null, knownMaxValue, storage.NumberTrues);
            } else {
                return new Iterators.BoolIndexIterator_ML(start, size.NumberOfDimensions, bsd + 3 + size.NumberOfDimensions, bsd + 3, null, lastDimensionIdx, keepAlive ? null : null, knownMaxValue, storage.NumberTrues);
            }
        }

        #endregion


    }
}
