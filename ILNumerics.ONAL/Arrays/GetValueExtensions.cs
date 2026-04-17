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
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics {

    
    public static partial class ExtensionMethods {

        /*  !!! CAUTION! BEFORE TRYING TO SIMPLIFY THISS MASSIVE OVERLOADING: IT HAS BEEN ATTEMPTED BEFORE! -> READ BELOW !!! */

        /* GetValue extensions. We have ... 
         * 1) a generic (non-constraint) implementation for all unspecified (non-common, user defined) element types. 
         * 2) specific value / primitive / unmanaged element type overloads. This uses hycalper to derive specific numeric 
         *    primitive types from the Array<double> implementation. We could use a single implementation based on 
         *    a generic implementation, constraint to 'unmanaged' element types T, though. However, we decided to not 
         *    go this way, because it would prevent us from having 1! Overload resolution does not work solely based on type constraints, unfortunately...). 
         * 3) Specific overloads for Logical array types. 
         * 4) Specific overloads for Cell array types. 
         */

        #region HYCALPER LOOPSTART
        /*!HC:TYPELIST:
        <hycalper>
        <type>
            <source locate="here">
                double
            </source>
            <destination>float</destination>
            <destination>int</destination>
            <destination>uint</destination>
            <destination>long</destination>
            <destination>ulong</destination>
            <destination>complex</destination>
            <destination>fcomplex</destination>
            <destination>short</destination>
            <destination>ushort</destination>
            <destination>byte</destination>
            <destination>sbyte</destination>
        </type>
        </hycalper>
        */

        #region long indices
        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension or sequential index into the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element at <b>sequential</b> index <paramref name="d0"/>. Note that 
        /// the index provided in <paramref name="d0"/> not necessarily corresponds with the storage position of the element to retrieve.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{double}, long, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static double GetValue(this BaseArray<double> A, long d0) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, out var storage);
            return ((double*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> and <paramref name="d1"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first two dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{double}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static double GetValue(this BaseArray<double> A, long d0, long d1) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, out var storage);
            var ret = ((double*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1)];
            return ret;

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d2"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first three dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{double}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension, or the value of <paramref name="d1"/> exceeds the number of elements in the 2nd dimension, 
        /// or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static double GetValue(this BaseArray<double> A, long d0, long d1, long d2) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, out var storage);
            return ((double*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d3"/>.  
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first four dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{double}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static double GetValue(this BaseArray<double> A, long d0, long d1, long d2, long d3) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, out var storage);
            return ((double*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d4"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first five dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{double}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static double GetValue(this BaseArray<double> A, long d0, long d1, long d2, long d3, long d4) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, out var storage);
            return ((double*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d5"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first six dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{double}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static double GetValue(this BaseArray<double> A, long d0, long d1, long d2, long d3, long d4, long d5) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, out var storage);
            return ((double*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d6"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension. </param>
        /// <param name="d6">Index into the sevenths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the seven dimensions. </para>
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{double}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/>, <paramref name="d5"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static double GetValue(this BaseArray<double> A, long d0, long d1, long d2, long d3, long d4, long d5, long d6) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, out var storage);
            return ((double*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6)];

        }
        #endregion 

        #region uint indices
        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension or sequential index into the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element at <b>sequential</b> index <paramref name="d0"/>. Note that 
        /// the index provided in <paramref name="d0"/> not necessarily corresponds with the storage position of the element to retrieve.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{double}, uint, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static double GetValue(this BaseArray<double> A, uint d0) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, out var storage);
            return ((double*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> and <paramref name="d1"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first two dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{double}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static double GetValue(this BaseArray<double> A, uint d0, uint d1) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, out var storage);
            var ret = ((double*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1)];
            return ret;

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d2"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first three dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{double}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension, or the value of <paramref name="d1"/> exceeds the number of elements in the 2nd dimension, 
        /// or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static double GetValue(this BaseArray<double> A, uint d0, uint d1, uint d2) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, out var storage);
            return ((double*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d3"/>.  
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first four dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{double}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static double GetValue(this BaseArray<double> A, uint d0, uint d1, uint d2, uint d3) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, out var storage);
            return ((double*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d4"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first five dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{double}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static double GetValue(this BaseArray<double> A, uint d0, uint d1, uint d2, uint d3, uint d4) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, out var storage);
            return ((double*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d5"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first six dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{double}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static double GetValue(this BaseArray<double> A, uint d0, uint d1, uint d2, uint d3, uint d4, uint d5) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, out var storage);
            return ((double*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d6"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension. </param>
        /// <param name="d6">Index into the sevenths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the seven dimensions. </para>
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{double}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/>, <paramref name="d5"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static double GetValue(this BaseArray<double> A, uint d0, uint d1, uint d2, uint d3, uint d4, uint d5, uint d6) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, out var storage);
            return ((double*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6)];

        }
        #endregion 

        /// <summary>
        /// Retrieves the value at the element addressed by indices provided as an index array.
        /// </summary>
        /// <param name="indices">Index array with dimensional indices addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        /// <exception cref="IndexOutOfRangeException"> when the element addressed does not exist.</exception>
        public unsafe static double GetValue(this BaseArray<double> A, params uint[] indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, out var storage);
            return ((double*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }
        /// <summary>
        /// Retrieves the value at the element addressed by indices stored as an index array. 
        /// </summary>
        /// <param name="indices">Index array addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        public unsafe static double GetValue(this BaseArray<double> A, params long[] indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, out var storage);
            return ((double*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }
        /// <summary>
        /// Retrieves the value at the element addressed by indices stored in an index array.
        /// </summary>
        /// <param name="indices">Index array addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        public unsafe static double GetValue(this BaseArray<double> A, InArray<long> indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, out var storage);
            return ((double*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }

        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        #region long indices
        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension or sequential index into the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element at <b>sequential</b> index <paramref name="d0"/>. Note that 
        /// the index provided in <paramref name="d0"/> not necessarily corresponds with the storage position of the element to retrieve.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{sbyte}, long, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static sbyte GetValue(this BaseArray<sbyte> A, long d0) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, out var storage);
            return ((sbyte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> and <paramref name="d1"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first two dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{sbyte}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static sbyte GetValue(this BaseArray<sbyte> A, long d0, long d1) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, out var storage);
            var ret = ((sbyte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1)];
            return ret;

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d2"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first three dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{sbyte}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension, or the value of <paramref name="d1"/> exceeds the number of elements in the 2nd dimension, 
        /// or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static sbyte GetValue(this BaseArray<sbyte> A, long d0, long d1, long d2) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, out var storage);
            return ((sbyte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d3"/>.  
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first four dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{sbyte}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static sbyte GetValue(this BaseArray<sbyte> A, long d0, long d1, long d2, long d3) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, out var storage);
            return ((sbyte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d4"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first five dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{sbyte}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static sbyte GetValue(this BaseArray<sbyte> A, long d0, long d1, long d2, long d3, long d4) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, out var storage);
            return ((sbyte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d5"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first six dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{sbyte}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static sbyte GetValue(this BaseArray<sbyte> A, long d0, long d1, long d2, long d3, long d4, long d5) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, out var storage);
            return ((sbyte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d6"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension. </param>
        /// <param name="d6">Index into the sevenths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the seven dimensions. </para>
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{sbyte}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/>, <paramref name="d5"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static sbyte GetValue(this BaseArray<sbyte> A, long d0, long d1, long d2, long d3, long d4, long d5, long d6) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, out var storage);
            return ((sbyte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6)];

        }
        #endregion 

        #region uint indices
        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension or sequential index into the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element at <b>sequential</b> index <paramref name="d0"/>. Note that 
        /// the index provided in <paramref name="d0"/> not necessarily corresponds with the storage position of the element to retrieve.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{sbyte}, uint, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static sbyte GetValue(this BaseArray<sbyte> A, uint d0) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, out var storage);
            return ((sbyte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> and <paramref name="d1"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first two dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{sbyte}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static sbyte GetValue(this BaseArray<sbyte> A, uint d0, uint d1) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, out var storage);
            var ret = ((sbyte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1)];
            return ret;

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d2"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first three dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{sbyte}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension, or the value of <paramref name="d1"/> exceeds the number of elements in the 2nd dimension, 
        /// or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static sbyte GetValue(this BaseArray<sbyte> A, uint d0, uint d1, uint d2) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, out var storage);
            return ((sbyte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d3"/>.  
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first four dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{sbyte}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static sbyte GetValue(this BaseArray<sbyte> A, uint d0, uint d1, uint d2, uint d3) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, out var storage);
            return ((sbyte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d4"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first five dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{sbyte}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static sbyte GetValue(this BaseArray<sbyte> A, uint d0, uint d1, uint d2, uint d3, uint d4) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, out var storage);
            return ((sbyte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d5"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first six dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{sbyte}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static sbyte GetValue(this BaseArray<sbyte> A, uint d0, uint d1, uint d2, uint d3, uint d4, uint d5) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, out var storage);
            return ((sbyte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d6"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension. </param>
        /// <param name="d6">Index into the sevenths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the seven dimensions. </para>
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{sbyte}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/>, <paramref name="d5"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static sbyte GetValue(this BaseArray<sbyte> A, uint d0, uint d1, uint d2, uint d3, uint d4, uint d5, uint d6) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, out var storage);
            return ((sbyte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6)];

        }
        #endregion 

        /// <summary>
        /// Retrieves the value at the element addressed by indices provided as an index array.
        /// </summary>
        /// <param name="indices">Index array with dimensional indices addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        /// <exception cref="IndexOutOfRangeException"> when the element addressed does not exist.</exception>
        public unsafe static sbyte GetValue(this BaseArray<sbyte> A, params uint[] indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, out var storage);
            return ((sbyte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }
        /// <summary>
        /// Retrieves the value at the element addressed by indices stored as an index array. 
        /// </summary>
        /// <param name="indices">Index array addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        public unsafe static sbyte GetValue(this BaseArray<sbyte> A, params long[] indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, out var storage);
            return ((sbyte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }
        /// <summary>
        /// Retrieves the value at the element addressed by indices stored in an index array.
        /// </summary>
        /// <param name="indices">Index array addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        public unsafe static sbyte GetValue(this BaseArray<sbyte> A, InArray<long> indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, out var storage);
            return ((sbyte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }

       

        #region long indices
        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension or sequential index into the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element at <b>sequential</b> index <paramref name="d0"/>. Note that 
        /// the index provided in <paramref name="d0"/> not necessarily corresponds with the storage position of the element to retrieve.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{byte}, long, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static byte GetValue(this BaseArray<byte> A, long d0) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, out var storage);
            return ((byte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> and <paramref name="d1"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first two dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{byte}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static byte GetValue(this BaseArray<byte> A, long d0, long d1) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, out var storage);
            var ret = ((byte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1)];
            return ret;

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d2"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first three dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{byte}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension, or the value of <paramref name="d1"/> exceeds the number of elements in the 2nd dimension, 
        /// or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static byte GetValue(this BaseArray<byte> A, long d0, long d1, long d2) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, out var storage);
            return ((byte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d3"/>.  
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first four dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{byte}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static byte GetValue(this BaseArray<byte> A, long d0, long d1, long d2, long d3) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, out var storage);
            return ((byte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d4"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first five dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{byte}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static byte GetValue(this BaseArray<byte> A, long d0, long d1, long d2, long d3, long d4) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, out var storage);
            return ((byte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d5"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first six dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{byte}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static byte GetValue(this BaseArray<byte> A, long d0, long d1, long d2, long d3, long d4, long d5) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, out var storage);
            return ((byte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d6"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension. </param>
        /// <param name="d6">Index into the sevenths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the seven dimensions. </para>
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{byte}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/>, <paramref name="d5"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static byte GetValue(this BaseArray<byte> A, long d0, long d1, long d2, long d3, long d4, long d5, long d6) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, out var storage);
            return ((byte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6)];

        }
        #endregion 

        #region uint indices
        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension or sequential index into the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element at <b>sequential</b> index <paramref name="d0"/>. Note that 
        /// the index provided in <paramref name="d0"/> not necessarily corresponds with the storage position of the element to retrieve.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{byte}, uint, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static byte GetValue(this BaseArray<byte> A, uint d0) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, out var storage);
            return ((byte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> and <paramref name="d1"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first two dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{byte}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static byte GetValue(this BaseArray<byte> A, uint d0, uint d1) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, out var storage);
            var ret = ((byte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1)];
            return ret;

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d2"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first three dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{byte}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension, or the value of <paramref name="d1"/> exceeds the number of elements in the 2nd dimension, 
        /// or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static byte GetValue(this BaseArray<byte> A, uint d0, uint d1, uint d2) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, out var storage);
            return ((byte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d3"/>.  
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first four dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{byte}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static byte GetValue(this BaseArray<byte> A, uint d0, uint d1, uint d2, uint d3) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, out var storage);
            return ((byte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d4"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first five dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{byte}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static byte GetValue(this BaseArray<byte> A, uint d0, uint d1, uint d2, uint d3, uint d4) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, out var storage);
            return ((byte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d5"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first six dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{byte}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static byte GetValue(this BaseArray<byte> A, uint d0, uint d1, uint d2, uint d3, uint d4, uint d5) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, out var storage);
            return ((byte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d6"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension. </param>
        /// <param name="d6">Index into the sevenths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the seven dimensions. </para>
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{byte}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/>, <paramref name="d5"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static byte GetValue(this BaseArray<byte> A, uint d0, uint d1, uint d2, uint d3, uint d4, uint d5, uint d6) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, out var storage);
            return ((byte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6)];

        }
        #endregion 

        /// <summary>
        /// Retrieves the value at the element addressed by indices provided as an index array.
        /// </summary>
        /// <param name="indices">Index array with dimensional indices addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        /// <exception cref="IndexOutOfRangeException"> when the element addressed does not exist.</exception>
        public unsafe static byte GetValue(this BaseArray<byte> A, params uint[] indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, out var storage);
            return ((byte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }
        /// <summary>
        /// Retrieves the value at the element addressed by indices stored as an index array. 
        /// </summary>
        /// <param name="indices">Index array addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        public unsafe static byte GetValue(this BaseArray<byte> A, params long[] indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, out var storage);
            return ((byte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }
        /// <summary>
        /// Retrieves the value at the element addressed by indices stored in an index array.
        /// </summary>
        /// <param name="indices">Index array addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        public unsafe static byte GetValue(this BaseArray<byte> A, InArray<long> indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, out var storage);
            return ((byte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }

       

        #region long indices
        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension or sequential index into the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element at <b>sequential</b> index <paramref name="d0"/>. Note that 
        /// the index provided in <paramref name="d0"/> not necessarily corresponds with the storage position of the element to retrieve.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{ushort}, long, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static ushort GetValue(this BaseArray<ushort> A, long d0) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, out var storage);
            return ((ushort*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> and <paramref name="d1"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first two dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{ushort}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static ushort GetValue(this BaseArray<ushort> A, long d0, long d1) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, out var storage);
            var ret = ((ushort*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1)];
            return ret;

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d2"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first three dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{ushort}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension, or the value of <paramref name="d1"/> exceeds the number of elements in the 2nd dimension, 
        /// or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static ushort GetValue(this BaseArray<ushort> A, long d0, long d1, long d2) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, out var storage);
            return ((ushort*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d3"/>.  
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first four dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{ushort}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static ushort GetValue(this BaseArray<ushort> A, long d0, long d1, long d2, long d3) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, out var storage);
            return ((ushort*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d4"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first five dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{ushort}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static ushort GetValue(this BaseArray<ushort> A, long d0, long d1, long d2, long d3, long d4) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, out var storage);
            return ((ushort*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d5"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first six dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{ushort}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static ushort GetValue(this BaseArray<ushort> A, long d0, long d1, long d2, long d3, long d4, long d5) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, out var storage);
            return ((ushort*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d6"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension. </param>
        /// <param name="d6">Index into the sevenths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the seven dimensions. </para>
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{ushort}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/>, <paramref name="d5"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static ushort GetValue(this BaseArray<ushort> A, long d0, long d1, long d2, long d3, long d4, long d5, long d6) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, out var storage);
            return ((ushort*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6)];

        }
        #endregion 

        #region uint indices
        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension or sequential index into the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element at <b>sequential</b> index <paramref name="d0"/>. Note that 
        /// the index provided in <paramref name="d0"/> not necessarily corresponds with the storage position of the element to retrieve.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{ushort}, uint, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static ushort GetValue(this BaseArray<ushort> A, uint d0) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, out var storage);
            return ((ushort*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> and <paramref name="d1"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first two dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{ushort}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static ushort GetValue(this BaseArray<ushort> A, uint d0, uint d1) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, out var storage);
            var ret = ((ushort*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1)];
            return ret;

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d2"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first three dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{ushort}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension, or the value of <paramref name="d1"/> exceeds the number of elements in the 2nd dimension, 
        /// or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static ushort GetValue(this BaseArray<ushort> A, uint d0, uint d1, uint d2) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, out var storage);
            return ((ushort*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d3"/>.  
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first four dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{ushort}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static ushort GetValue(this BaseArray<ushort> A, uint d0, uint d1, uint d2, uint d3) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, out var storage);
            return ((ushort*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d4"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first five dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{ushort}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static ushort GetValue(this BaseArray<ushort> A, uint d0, uint d1, uint d2, uint d3, uint d4) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, out var storage);
            return ((ushort*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d5"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first six dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{ushort}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static ushort GetValue(this BaseArray<ushort> A, uint d0, uint d1, uint d2, uint d3, uint d4, uint d5) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, out var storage);
            return ((ushort*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d6"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension. </param>
        /// <param name="d6">Index into the sevenths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the seven dimensions. </para>
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{ushort}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/>, <paramref name="d5"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static ushort GetValue(this BaseArray<ushort> A, uint d0, uint d1, uint d2, uint d3, uint d4, uint d5, uint d6) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, out var storage);
            return ((ushort*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6)];

        }
        #endregion 

        /// <summary>
        /// Retrieves the value at the element addressed by indices provided as an index array.
        /// </summary>
        /// <param name="indices">Index array with dimensional indices addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        /// <exception cref="IndexOutOfRangeException"> when the element addressed does not exist.</exception>
        public unsafe static ushort GetValue(this BaseArray<ushort> A, params uint[] indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, out var storage);
            return ((ushort*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }
        /// <summary>
        /// Retrieves the value at the element addressed by indices stored as an index array. 
        /// </summary>
        /// <param name="indices">Index array addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        public unsafe static ushort GetValue(this BaseArray<ushort> A, params long[] indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, out var storage);
            return ((ushort*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }
        /// <summary>
        /// Retrieves the value at the element addressed by indices stored in an index array.
        /// </summary>
        /// <param name="indices">Index array addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        public unsafe static ushort GetValue(this BaseArray<ushort> A, InArray<long> indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, out var storage);
            return ((ushort*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }

       

        #region long indices
        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension or sequential index into the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element at <b>sequential</b> index <paramref name="d0"/>. Note that 
        /// the index provided in <paramref name="d0"/> not necessarily corresponds with the storage position of the element to retrieve.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{short}, long, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static short GetValue(this BaseArray<short> A, long d0) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, out var storage);
            return ((short*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> and <paramref name="d1"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first two dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{short}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static short GetValue(this BaseArray<short> A, long d0, long d1) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, out var storage);
            var ret = ((short*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1)];
            return ret;

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d2"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first three dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{short}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension, or the value of <paramref name="d1"/> exceeds the number of elements in the 2nd dimension, 
        /// or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static short GetValue(this BaseArray<short> A, long d0, long d1, long d2) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, out var storage);
            return ((short*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d3"/>.  
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first four dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{short}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static short GetValue(this BaseArray<short> A, long d0, long d1, long d2, long d3) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, out var storage);
            return ((short*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d4"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first five dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{short}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static short GetValue(this BaseArray<short> A, long d0, long d1, long d2, long d3, long d4) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, out var storage);
            return ((short*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d5"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first six dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{short}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static short GetValue(this BaseArray<short> A, long d0, long d1, long d2, long d3, long d4, long d5) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, out var storage);
            return ((short*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d6"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension. </param>
        /// <param name="d6">Index into the sevenths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the seven dimensions. </para>
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{short}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/>, <paramref name="d5"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static short GetValue(this BaseArray<short> A, long d0, long d1, long d2, long d3, long d4, long d5, long d6) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, out var storage);
            return ((short*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6)];

        }
        #endregion 

        #region uint indices
        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension or sequential index into the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element at <b>sequential</b> index <paramref name="d0"/>. Note that 
        /// the index provided in <paramref name="d0"/> not necessarily corresponds with the storage position of the element to retrieve.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{short}, uint, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static short GetValue(this BaseArray<short> A, uint d0) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, out var storage);
            return ((short*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> and <paramref name="d1"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first two dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{short}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static short GetValue(this BaseArray<short> A, uint d0, uint d1) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, out var storage);
            var ret = ((short*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1)];
            return ret;

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d2"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first three dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{short}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension, or the value of <paramref name="d1"/> exceeds the number of elements in the 2nd dimension, 
        /// or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static short GetValue(this BaseArray<short> A, uint d0, uint d1, uint d2) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, out var storage);
            return ((short*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d3"/>.  
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first four dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{short}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static short GetValue(this BaseArray<short> A, uint d0, uint d1, uint d2, uint d3) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, out var storage);
            return ((short*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d4"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first five dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{short}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static short GetValue(this BaseArray<short> A, uint d0, uint d1, uint d2, uint d3, uint d4) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, out var storage);
            return ((short*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d5"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first six dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{short}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static short GetValue(this BaseArray<short> A, uint d0, uint d1, uint d2, uint d3, uint d4, uint d5) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, out var storage);
            return ((short*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d6"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension. </param>
        /// <param name="d6">Index into the sevenths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the seven dimensions. </para>
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{short}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/>, <paramref name="d5"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static short GetValue(this BaseArray<short> A, uint d0, uint d1, uint d2, uint d3, uint d4, uint d5, uint d6) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, out var storage);
            return ((short*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6)];

        }
        #endregion 

        /// <summary>
        /// Retrieves the value at the element addressed by indices provided as an index array.
        /// </summary>
        /// <param name="indices">Index array with dimensional indices addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        /// <exception cref="IndexOutOfRangeException"> when the element addressed does not exist.</exception>
        public unsafe static short GetValue(this BaseArray<short> A, params uint[] indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, out var storage);
            return ((short*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }
        /// <summary>
        /// Retrieves the value at the element addressed by indices stored as an index array. 
        /// </summary>
        /// <param name="indices">Index array addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        public unsafe static short GetValue(this BaseArray<short> A, params long[] indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, out var storage);
            return ((short*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }
        /// <summary>
        /// Retrieves the value at the element addressed by indices stored in an index array.
        /// </summary>
        /// <param name="indices">Index array addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        public unsafe static short GetValue(this BaseArray<short> A, InArray<long> indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, out var storage);
            return ((short*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }

       

        #region long indices
        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension or sequential index into the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element at <b>sequential</b> index <paramref name="d0"/>. Note that 
        /// the index provided in <paramref name="d0"/> not necessarily corresponds with the storage position of the element to retrieve.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{fcomplex}, long, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static fcomplex GetValue(this BaseArray<fcomplex> A, long d0) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, out var storage);
            return ((fcomplex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> and <paramref name="d1"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first two dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{fcomplex}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static fcomplex GetValue(this BaseArray<fcomplex> A, long d0, long d1) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, out var storage);
            var ret = ((fcomplex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1)];
            return ret;

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d2"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first three dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{fcomplex}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension, or the value of <paramref name="d1"/> exceeds the number of elements in the 2nd dimension, 
        /// or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static fcomplex GetValue(this BaseArray<fcomplex> A, long d0, long d1, long d2) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, out var storage);
            return ((fcomplex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d3"/>.  
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first four dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{fcomplex}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static fcomplex GetValue(this BaseArray<fcomplex> A, long d0, long d1, long d2, long d3) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, out var storage);
            return ((fcomplex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d4"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first five dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{fcomplex}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static fcomplex GetValue(this BaseArray<fcomplex> A, long d0, long d1, long d2, long d3, long d4) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, out var storage);
            return ((fcomplex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d5"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first six dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{fcomplex}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static fcomplex GetValue(this BaseArray<fcomplex> A, long d0, long d1, long d2, long d3, long d4, long d5) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, out var storage);
            return ((fcomplex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d6"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension. </param>
        /// <param name="d6">Index into the sevenths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the seven dimensions. </para>
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{fcomplex}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/>, <paramref name="d5"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static fcomplex GetValue(this BaseArray<fcomplex> A, long d0, long d1, long d2, long d3, long d4, long d5, long d6) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, out var storage);
            return ((fcomplex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6)];

        }
        #endregion 

        #region uint indices
        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension or sequential index into the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element at <b>sequential</b> index <paramref name="d0"/>. Note that 
        /// the index provided in <paramref name="d0"/> not necessarily corresponds with the storage position of the element to retrieve.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{fcomplex}, uint, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static fcomplex GetValue(this BaseArray<fcomplex> A, uint d0) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, out var storage);
            return ((fcomplex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> and <paramref name="d1"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first two dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{fcomplex}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static fcomplex GetValue(this BaseArray<fcomplex> A, uint d0, uint d1) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, out var storage);
            var ret = ((fcomplex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1)];
            return ret;

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d2"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first three dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{fcomplex}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension, or the value of <paramref name="d1"/> exceeds the number of elements in the 2nd dimension, 
        /// or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static fcomplex GetValue(this BaseArray<fcomplex> A, uint d0, uint d1, uint d2) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, out var storage);
            return ((fcomplex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d3"/>.  
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first four dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{fcomplex}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static fcomplex GetValue(this BaseArray<fcomplex> A, uint d0, uint d1, uint d2, uint d3) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, out var storage);
            return ((fcomplex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d4"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first five dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{fcomplex}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static fcomplex GetValue(this BaseArray<fcomplex> A, uint d0, uint d1, uint d2, uint d3, uint d4) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, out var storage);
            return ((fcomplex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d5"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first six dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{fcomplex}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static fcomplex GetValue(this BaseArray<fcomplex> A, uint d0, uint d1, uint d2, uint d3, uint d4, uint d5) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, out var storage);
            return ((fcomplex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d6"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension. </param>
        /// <param name="d6">Index into the sevenths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the seven dimensions. </para>
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{fcomplex}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/>, <paramref name="d5"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static fcomplex GetValue(this BaseArray<fcomplex> A, uint d0, uint d1, uint d2, uint d3, uint d4, uint d5, uint d6) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, out var storage);
            return ((fcomplex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6)];

        }
        #endregion 

        /// <summary>
        /// Retrieves the value at the element addressed by indices provided as an index array.
        /// </summary>
        /// <param name="indices">Index array with dimensional indices addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        /// <exception cref="IndexOutOfRangeException"> when the element addressed does not exist.</exception>
        public unsafe static fcomplex GetValue(this BaseArray<fcomplex> A, params uint[] indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, out var storage);
            return ((fcomplex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }
        /// <summary>
        /// Retrieves the value at the element addressed by indices stored as an index array. 
        /// </summary>
        /// <param name="indices">Index array addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        public unsafe static fcomplex GetValue(this BaseArray<fcomplex> A, params long[] indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, out var storage);
            return ((fcomplex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }
        /// <summary>
        /// Retrieves the value at the element addressed by indices stored in an index array.
        /// </summary>
        /// <param name="indices">Index array addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        public unsafe static fcomplex GetValue(this BaseArray<fcomplex> A, InArray<long> indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, out var storage);
            return ((fcomplex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }

       

        #region long indices
        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension or sequential index into the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element at <b>sequential</b> index <paramref name="d0"/>. Note that 
        /// the index provided in <paramref name="d0"/> not necessarily corresponds with the storage position of the element to retrieve.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{complex}, long, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static complex GetValue(this BaseArray<complex> A, long d0) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, out var storage);
            return ((complex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> and <paramref name="d1"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first two dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{complex}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static complex GetValue(this BaseArray<complex> A, long d0, long d1) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, out var storage);
            var ret = ((complex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1)];
            return ret;

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d2"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first three dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{complex}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension, or the value of <paramref name="d1"/> exceeds the number of elements in the 2nd dimension, 
        /// or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static complex GetValue(this BaseArray<complex> A, long d0, long d1, long d2) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, out var storage);
            return ((complex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d3"/>.  
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first four dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{complex}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static complex GetValue(this BaseArray<complex> A, long d0, long d1, long d2, long d3) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, out var storage);
            return ((complex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d4"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first five dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{complex}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static complex GetValue(this BaseArray<complex> A, long d0, long d1, long d2, long d3, long d4) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, out var storage);
            return ((complex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d5"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first six dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{complex}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static complex GetValue(this BaseArray<complex> A, long d0, long d1, long d2, long d3, long d4, long d5) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, out var storage);
            return ((complex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d6"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension. </param>
        /// <param name="d6">Index into the sevenths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the seven dimensions. </para>
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{complex}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/>, <paramref name="d5"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static complex GetValue(this BaseArray<complex> A, long d0, long d1, long d2, long d3, long d4, long d5, long d6) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, out var storage);
            return ((complex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6)];

        }
        #endregion 

        #region uint indices
        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension or sequential index into the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element at <b>sequential</b> index <paramref name="d0"/>. Note that 
        /// the index provided in <paramref name="d0"/> not necessarily corresponds with the storage position of the element to retrieve.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{complex}, uint, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static complex GetValue(this BaseArray<complex> A, uint d0) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, out var storage);
            return ((complex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> and <paramref name="d1"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first two dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{complex}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static complex GetValue(this BaseArray<complex> A, uint d0, uint d1) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, out var storage);
            var ret = ((complex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1)];
            return ret;

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d2"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first three dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{complex}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension, or the value of <paramref name="d1"/> exceeds the number of elements in the 2nd dimension, 
        /// or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static complex GetValue(this BaseArray<complex> A, uint d0, uint d1, uint d2) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, out var storage);
            return ((complex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d3"/>.  
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first four dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{complex}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static complex GetValue(this BaseArray<complex> A, uint d0, uint d1, uint d2, uint d3) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, out var storage);
            return ((complex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d4"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first five dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{complex}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static complex GetValue(this BaseArray<complex> A, uint d0, uint d1, uint d2, uint d3, uint d4) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, out var storage);
            return ((complex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d5"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first six dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{complex}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static complex GetValue(this BaseArray<complex> A, uint d0, uint d1, uint d2, uint d3, uint d4, uint d5) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, out var storage);
            return ((complex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d6"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension. </param>
        /// <param name="d6">Index into the sevenths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the seven dimensions. </para>
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{complex}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/>, <paramref name="d5"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static complex GetValue(this BaseArray<complex> A, uint d0, uint d1, uint d2, uint d3, uint d4, uint d5, uint d6) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, out var storage);
            return ((complex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6)];

        }
        #endregion 

        /// <summary>
        /// Retrieves the value at the element addressed by indices provided as an index array.
        /// </summary>
        /// <param name="indices">Index array with dimensional indices addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        /// <exception cref="IndexOutOfRangeException"> when the element addressed does not exist.</exception>
        public unsafe static complex GetValue(this BaseArray<complex> A, params uint[] indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, out var storage);
            return ((complex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }
        /// <summary>
        /// Retrieves the value at the element addressed by indices stored as an index array. 
        /// </summary>
        /// <param name="indices">Index array addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        public unsafe static complex GetValue(this BaseArray<complex> A, params long[] indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, out var storage);
            return ((complex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }
        /// <summary>
        /// Retrieves the value at the element addressed by indices stored in an index array.
        /// </summary>
        /// <param name="indices">Index array addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        public unsafe static complex GetValue(this BaseArray<complex> A, InArray<long> indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, out var storage);
            return ((complex*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }

       

        #region long indices
        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension or sequential index into the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element at <b>sequential</b> index <paramref name="d0"/>. Note that 
        /// the index provided in <paramref name="d0"/> not necessarily corresponds with the storage position of the element to retrieve.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{ulong}, long, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static ulong GetValue(this BaseArray<ulong> A, long d0) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, out var storage);
            return ((ulong*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> and <paramref name="d1"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first two dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{ulong}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static ulong GetValue(this BaseArray<ulong> A, long d0, long d1) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, out var storage);
            var ret = ((ulong*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1)];
            return ret;

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d2"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first three dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{ulong}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension, or the value of <paramref name="d1"/> exceeds the number of elements in the 2nd dimension, 
        /// or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static ulong GetValue(this BaseArray<ulong> A, long d0, long d1, long d2) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, out var storage);
            return ((ulong*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d3"/>.  
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first four dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{ulong}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static ulong GetValue(this BaseArray<ulong> A, long d0, long d1, long d2, long d3) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, out var storage);
            return ((ulong*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d4"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first five dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{ulong}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static ulong GetValue(this BaseArray<ulong> A, long d0, long d1, long d2, long d3, long d4) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, out var storage);
            return ((ulong*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d5"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first six dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{ulong}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static ulong GetValue(this BaseArray<ulong> A, long d0, long d1, long d2, long d3, long d4, long d5) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, out var storage);
            return ((ulong*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d6"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension. </param>
        /// <param name="d6">Index into the sevenths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the seven dimensions. </para>
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{ulong}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/>, <paramref name="d5"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static ulong GetValue(this BaseArray<ulong> A, long d0, long d1, long d2, long d3, long d4, long d5, long d6) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, out var storage);
            return ((ulong*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6)];

        }
        #endregion 

        #region uint indices
        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension or sequential index into the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element at <b>sequential</b> index <paramref name="d0"/>. Note that 
        /// the index provided in <paramref name="d0"/> not necessarily corresponds with the storage position of the element to retrieve.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{ulong}, uint, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static ulong GetValue(this BaseArray<ulong> A, uint d0) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, out var storage);
            return ((ulong*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> and <paramref name="d1"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first two dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{ulong}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static ulong GetValue(this BaseArray<ulong> A, uint d0, uint d1) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, out var storage);
            var ret = ((ulong*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1)];
            return ret;

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d2"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first three dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{ulong}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension, or the value of <paramref name="d1"/> exceeds the number of elements in the 2nd dimension, 
        /// or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static ulong GetValue(this BaseArray<ulong> A, uint d0, uint d1, uint d2) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, out var storage);
            return ((ulong*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d3"/>.  
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first four dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{ulong}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static ulong GetValue(this BaseArray<ulong> A, uint d0, uint d1, uint d2, uint d3) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, out var storage);
            return ((ulong*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d4"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first five dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{ulong}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static ulong GetValue(this BaseArray<ulong> A, uint d0, uint d1, uint d2, uint d3, uint d4) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, out var storage);
            return ((ulong*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d5"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first six dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{ulong}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static ulong GetValue(this BaseArray<ulong> A, uint d0, uint d1, uint d2, uint d3, uint d4, uint d5) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, out var storage);
            return ((ulong*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d6"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension. </param>
        /// <param name="d6">Index into the sevenths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the seven dimensions. </para>
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{ulong}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/>, <paramref name="d5"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static ulong GetValue(this BaseArray<ulong> A, uint d0, uint d1, uint d2, uint d3, uint d4, uint d5, uint d6) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, out var storage);
            return ((ulong*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6)];

        }
        #endregion 

        /// <summary>
        /// Retrieves the value at the element addressed by indices provided as an index array.
        /// </summary>
        /// <param name="indices">Index array with dimensional indices addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        /// <exception cref="IndexOutOfRangeException"> when the element addressed does not exist.</exception>
        public unsafe static ulong GetValue(this BaseArray<ulong> A, params uint[] indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, out var storage);
            return ((ulong*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }
        /// <summary>
        /// Retrieves the value at the element addressed by indices stored as an index array. 
        /// </summary>
        /// <param name="indices">Index array addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        public unsafe static ulong GetValue(this BaseArray<ulong> A, params long[] indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, out var storage);
            return ((ulong*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }
        /// <summary>
        /// Retrieves the value at the element addressed by indices stored in an index array.
        /// </summary>
        /// <param name="indices">Index array addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        public unsafe static ulong GetValue(this BaseArray<ulong> A, InArray<long> indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, out var storage);
            return ((ulong*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }

       

        #region long indices
        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension or sequential index into the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element at <b>sequential</b> index <paramref name="d0"/>. Note that 
        /// the index provided in <paramref name="d0"/> not necessarily corresponds with the storage position of the element to retrieve.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{long}, long, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static long GetValue(this BaseArray<long> A, long d0) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, out var storage);
            return ((long*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> and <paramref name="d1"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first two dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{long}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static long GetValue(this BaseArray<long> A, long d0, long d1) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, out var storage);
            var ret = ((long*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1)];
            return ret;

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d2"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first three dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{long}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension, or the value of <paramref name="d1"/> exceeds the number of elements in the 2nd dimension, 
        /// or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static long GetValue(this BaseArray<long> A, long d0, long d1, long d2) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, out var storage);
            return ((long*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d3"/>.  
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first four dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{long}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static long GetValue(this BaseArray<long> A, long d0, long d1, long d2, long d3) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, out var storage);
            return ((long*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d4"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first five dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{long}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static long GetValue(this BaseArray<long> A, long d0, long d1, long d2, long d3, long d4) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, out var storage);
            return ((long*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d5"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first six dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{long}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static long GetValue(this BaseArray<long> A, long d0, long d1, long d2, long d3, long d4, long d5) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, out var storage);
            return ((long*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d6"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension. </param>
        /// <param name="d6">Index into the sevenths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the seven dimensions. </para>
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{long}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/>, <paramref name="d5"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static long GetValue(this BaseArray<long> A, long d0, long d1, long d2, long d3, long d4, long d5, long d6) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, out var storage);
            return ((long*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6)];

        }
        #endregion 

        #region uint indices
        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension or sequential index into the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element at <b>sequential</b> index <paramref name="d0"/>. Note that 
        /// the index provided in <paramref name="d0"/> not necessarily corresponds with the storage position of the element to retrieve.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{long}, uint, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static long GetValue(this BaseArray<long> A, uint d0) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, out var storage);
            return ((long*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> and <paramref name="d1"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first two dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{long}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static long GetValue(this BaseArray<long> A, uint d0, uint d1) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, out var storage);
            var ret = ((long*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1)];
            return ret;

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d2"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first three dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{long}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension, or the value of <paramref name="d1"/> exceeds the number of elements in the 2nd dimension, 
        /// or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static long GetValue(this BaseArray<long> A, uint d0, uint d1, uint d2) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, out var storage);
            return ((long*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d3"/>.  
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first four dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{long}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static long GetValue(this BaseArray<long> A, uint d0, uint d1, uint d2, uint d3) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, out var storage);
            return ((long*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d4"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first five dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{long}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static long GetValue(this BaseArray<long> A, uint d0, uint d1, uint d2, uint d3, uint d4) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, out var storage);
            return ((long*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d5"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first six dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{long}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static long GetValue(this BaseArray<long> A, uint d0, uint d1, uint d2, uint d3, uint d4, uint d5) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, out var storage);
            return ((long*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d6"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension. </param>
        /// <param name="d6">Index into the sevenths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the seven dimensions. </para>
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{long}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/>, <paramref name="d5"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static long GetValue(this BaseArray<long> A, uint d0, uint d1, uint d2, uint d3, uint d4, uint d5, uint d6) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, out var storage);
            return ((long*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6)];

        }
        #endregion 

        /// <summary>
        /// Retrieves the value at the element addressed by indices provided as an index array.
        /// </summary>
        /// <param name="indices">Index array with dimensional indices addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        /// <exception cref="IndexOutOfRangeException"> when the element addressed does not exist.</exception>
        public unsafe static long GetValue(this BaseArray<long> A, params uint[] indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, out var storage);
            return ((long*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }
        /// <summary>
        /// Retrieves the value at the element addressed by indices stored as an index array. 
        /// </summary>
        /// <param name="indices">Index array addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        public unsafe static long GetValue(this BaseArray<long> A, params long[] indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, out var storage);
            return ((long*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }
        /// <summary>
        /// Retrieves the value at the element addressed by indices stored in an index array.
        /// </summary>
        /// <param name="indices">Index array addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        public unsafe static long GetValue(this BaseArray<long> A, InArray<long> indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, out var storage);
            return ((long*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }

       

        #region long indices
        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension or sequential index into the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element at <b>sequential</b> index <paramref name="d0"/>. Note that 
        /// the index provided in <paramref name="d0"/> not necessarily corresponds with the storage position of the element to retrieve.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{uint}, long, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static uint GetValue(this BaseArray<uint> A, long d0) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, out var storage);
            return ((uint*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> and <paramref name="d1"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first two dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{uint}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static uint GetValue(this BaseArray<uint> A, long d0, long d1) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, out var storage);
            var ret = ((uint*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1)];
            return ret;

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d2"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first three dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{uint}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension, or the value of <paramref name="d1"/> exceeds the number of elements in the 2nd dimension, 
        /// or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static uint GetValue(this BaseArray<uint> A, long d0, long d1, long d2) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, out var storage);
            return ((uint*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d3"/>.  
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first four dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{uint}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static uint GetValue(this BaseArray<uint> A, long d0, long d1, long d2, long d3) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, out var storage);
            return ((uint*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d4"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first five dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{uint}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static uint GetValue(this BaseArray<uint> A, long d0, long d1, long d2, long d3, long d4) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, out var storage);
            return ((uint*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d5"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first six dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{uint}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static uint GetValue(this BaseArray<uint> A, long d0, long d1, long d2, long d3, long d4, long d5) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, out var storage);
            return ((uint*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d6"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension. </param>
        /// <param name="d6">Index into the sevenths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the seven dimensions. </para>
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{uint}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/>, <paramref name="d5"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static uint GetValue(this BaseArray<uint> A, long d0, long d1, long d2, long d3, long d4, long d5, long d6) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, out var storage);
            return ((uint*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6)];

        }
        #endregion 

        #region uint indices
        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension or sequential index into the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element at <b>sequential</b> index <paramref name="d0"/>. Note that 
        /// the index provided in <paramref name="d0"/> not necessarily corresponds with the storage position of the element to retrieve.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{uint}, uint, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static uint GetValue(this BaseArray<uint> A, uint d0) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, out var storage);
            return ((uint*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> and <paramref name="d1"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first two dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{uint}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static uint GetValue(this BaseArray<uint> A, uint d0, uint d1) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, out var storage);
            var ret = ((uint*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1)];
            return ret;

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d2"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first three dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{uint}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension, or the value of <paramref name="d1"/> exceeds the number of elements in the 2nd dimension, 
        /// or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static uint GetValue(this BaseArray<uint> A, uint d0, uint d1, uint d2) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, out var storage);
            return ((uint*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d3"/>.  
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first four dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{uint}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static uint GetValue(this BaseArray<uint> A, uint d0, uint d1, uint d2, uint d3) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, out var storage);
            return ((uint*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d4"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first five dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{uint}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static uint GetValue(this BaseArray<uint> A, uint d0, uint d1, uint d2, uint d3, uint d4) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, out var storage);
            return ((uint*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d5"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first six dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{uint}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static uint GetValue(this BaseArray<uint> A, uint d0, uint d1, uint d2, uint d3, uint d4, uint d5) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, out var storage);
            return ((uint*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d6"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension. </param>
        /// <param name="d6">Index into the sevenths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the seven dimensions. </para>
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{uint}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/>, <paramref name="d5"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static uint GetValue(this BaseArray<uint> A, uint d0, uint d1, uint d2, uint d3, uint d4, uint d5, uint d6) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, out var storage);
            return ((uint*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6)];

        }
        #endregion 

        /// <summary>
        /// Retrieves the value at the element addressed by indices provided as an index array.
        /// </summary>
        /// <param name="indices">Index array with dimensional indices addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        /// <exception cref="IndexOutOfRangeException"> when the element addressed does not exist.</exception>
        public unsafe static uint GetValue(this BaseArray<uint> A, params uint[] indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, out var storage);
            return ((uint*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }
        /// <summary>
        /// Retrieves the value at the element addressed by indices stored as an index array. 
        /// </summary>
        /// <param name="indices">Index array addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        public unsafe static uint GetValue(this BaseArray<uint> A, params long[] indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, out var storage);
            return ((uint*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }
        /// <summary>
        /// Retrieves the value at the element addressed by indices stored in an index array.
        /// </summary>
        /// <param name="indices">Index array addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        public unsafe static uint GetValue(this BaseArray<uint> A, InArray<long> indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, out var storage);
            return ((uint*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }

       

        #region long indices
        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension or sequential index into the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element at <b>sequential</b> index <paramref name="d0"/>. Note that 
        /// the index provided in <paramref name="d0"/> not necessarily corresponds with the storage position of the element to retrieve.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{int}, long, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static int GetValue(this BaseArray<int> A, long d0) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, out var storage);
            return ((int*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> and <paramref name="d1"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first two dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{int}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static int GetValue(this BaseArray<int> A, long d0, long d1) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, out var storage);
            var ret = ((int*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1)];
            return ret;

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d2"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first three dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{int}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension, or the value of <paramref name="d1"/> exceeds the number of elements in the 2nd dimension, 
        /// or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static int GetValue(this BaseArray<int> A, long d0, long d1, long d2) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, out var storage);
            return ((int*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d3"/>.  
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first four dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{int}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static int GetValue(this BaseArray<int> A, long d0, long d1, long d2, long d3) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, out var storage);
            return ((int*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d4"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first five dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{int}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static int GetValue(this BaseArray<int> A, long d0, long d1, long d2, long d3, long d4) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, out var storage);
            return ((int*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d5"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first six dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{int}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static int GetValue(this BaseArray<int> A, long d0, long d1, long d2, long d3, long d4, long d5) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, out var storage);
            return ((int*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d6"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension. </param>
        /// <param name="d6">Index into the sevenths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the seven dimensions. </para>
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{int}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/>, <paramref name="d5"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static int GetValue(this BaseArray<int> A, long d0, long d1, long d2, long d3, long d4, long d5, long d6) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, out var storage);
            return ((int*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6)];

        }
        #endregion 

        #region uint indices
        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension or sequential index into the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element at <b>sequential</b> index <paramref name="d0"/>. Note that 
        /// the index provided in <paramref name="d0"/> not necessarily corresponds with the storage position of the element to retrieve.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{int}, uint, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static int GetValue(this BaseArray<int> A, uint d0) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, out var storage);
            return ((int*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> and <paramref name="d1"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first two dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{int}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static int GetValue(this BaseArray<int> A, uint d0, uint d1) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, out var storage);
            var ret = ((int*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1)];
            return ret;

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d2"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first three dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{int}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension, or the value of <paramref name="d1"/> exceeds the number of elements in the 2nd dimension, 
        /// or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static int GetValue(this BaseArray<int> A, uint d0, uint d1, uint d2) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, out var storage);
            return ((int*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d3"/>.  
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first four dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{int}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static int GetValue(this BaseArray<int> A, uint d0, uint d1, uint d2, uint d3) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, out var storage);
            return ((int*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d4"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first five dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{int}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static int GetValue(this BaseArray<int> A, uint d0, uint d1, uint d2, uint d3, uint d4) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, out var storage);
            return ((int*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d5"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first six dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{int}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static int GetValue(this BaseArray<int> A, uint d0, uint d1, uint d2, uint d3, uint d4, uint d5) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, out var storage);
            return ((int*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d6"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension. </param>
        /// <param name="d6">Index into the sevenths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the seven dimensions. </para>
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{int}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/>, <paramref name="d5"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static int GetValue(this BaseArray<int> A, uint d0, uint d1, uint d2, uint d3, uint d4, uint d5, uint d6) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, out var storage);
            return ((int*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6)];

        }
        #endregion 

        /// <summary>
        /// Retrieves the value at the element addressed by indices provided as an index array.
        /// </summary>
        /// <param name="indices">Index array with dimensional indices addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        /// <exception cref="IndexOutOfRangeException"> when the element addressed does not exist.</exception>
        public unsafe static int GetValue(this BaseArray<int> A, params uint[] indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, out var storage);
            return ((int*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }
        /// <summary>
        /// Retrieves the value at the element addressed by indices stored as an index array. 
        /// </summary>
        /// <param name="indices">Index array addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        public unsafe static int GetValue(this BaseArray<int> A, params long[] indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, out var storage);
            return ((int*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }
        /// <summary>
        /// Retrieves the value at the element addressed by indices stored in an index array.
        /// </summary>
        /// <param name="indices">Index array addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        public unsafe static int GetValue(this BaseArray<int> A, InArray<long> indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, out var storage);
            return ((int*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }

       

        #region long indices
        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension or sequential index into the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element at <b>sequential</b> index <paramref name="d0"/>. Note that 
        /// the index provided in <paramref name="d0"/> not necessarily corresponds with the storage position of the element to retrieve.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{float}, long, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static float GetValue(this BaseArray<float> A, long d0) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, out var storage);
            return ((float*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> and <paramref name="d1"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first two dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{float}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static float GetValue(this BaseArray<float> A, long d0, long d1) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, out var storage);
            var ret = ((float*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1)];
            return ret;

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d2"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first three dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{float}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension, or the value of <paramref name="d1"/> exceeds the number of elements in the 2nd dimension, 
        /// or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static float GetValue(this BaseArray<float> A, long d0, long d1, long d2) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, out var storage);
            return ((float*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d3"/>.  
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first four dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{float}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static float GetValue(this BaseArray<float> A, long d0, long d1, long d2, long d3) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, out var storage);
            return ((float*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d4"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first five dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{float}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static float GetValue(this BaseArray<float> A, long d0, long d1, long d2, long d3, long d4) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, out var storage);
            return ((float*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d5"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first six dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{float}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static float GetValue(this BaseArray<float> A, long d0, long d1, long d2, long d3, long d4, long d5) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, out var storage);
            return ((float*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d6"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension. </param>
        /// <param name="d6">Index into the sevenths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the seven dimensions. </para>
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{float}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/>, <paramref name="d5"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static float GetValue(this BaseArray<float> A, long d0, long d1, long d2, long d3, long d4, long d5, long d6) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, out var storage);
            return ((float*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6)];

        }
        #endregion 

        #region uint indices
        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension or sequential index into the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element at <b>sequential</b> index <paramref name="d0"/>. Note that 
        /// the index provided in <paramref name="d0"/> not necessarily corresponds with the storage position of the element to retrieve.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{float}, uint, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static float GetValue(this BaseArray<float> A, uint d0) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, out var storage);
            return ((float*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> and <paramref name="d1"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first two dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{float}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static float GetValue(this BaseArray<float> A, uint d0, uint d1) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, out var storage);
            var ret = ((float*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1)];
            return ret;

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d2"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first three dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{float}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension, or the value of <paramref name="d1"/> exceeds the number of elements in the 2nd dimension, 
        /// or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static float GetValue(this BaseArray<float> A, uint d0, uint d1, uint d2) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, out var storage);
            return ((float*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d3"/>.  
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first four dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{float}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static float GetValue(this BaseArray<float> A, uint d0, uint d1, uint d2, uint d3) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, out var storage);
            return ((float*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d4"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first five dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{float}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static float GetValue(this BaseArray<float> A, uint d0, uint d1, uint d2, uint d3, uint d4) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, out var storage);
            return ((float*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d5"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first six dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if such exist (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{float}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static float GetValue(this BaseArray<float> A, uint d0, uint d1, uint d2, uint d3, uint d4, uint d5) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, out var storage);
            return ((float*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5)];

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d6"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension. </param>
        /// <param name="d6">Index into the sevenths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the seven dimensions. </para>
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{float}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/>, <paramref name="d5"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static float GetValue(this BaseArray<float> A, uint d0, uint d1, uint d2, uint d3, uint d4, uint d5, uint d6) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, out var storage);
            return ((float*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6)];

        }
        #endregion 

        /// <summary>
        /// Retrieves the value at the element addressed by indices provided as an index array.
        /// </summary>
        /// <param name="indices">Index array with dimensional indices addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        /// <exception cref="IndexOutOfRangeException"> when the element addressed does not exist.</exception>
        public unsafe static float GetValue(this BaseArray<float> A, params uint[] indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, out var storage);
            return ((float*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }
        /// <summary>
        /// Retrieves the value at the element addressed by indices stored as an index array. 
        /// </summary>
        /// <param name="indices">Index array addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        public unsafe static float GetValue(this BaseArray<float> A, params long[] indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, out var storage);
            return ((float*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }
        /// <summary>
        /// Retrieves the value at the element addressed by indices stored in an index array.
        /// </summary>
        /// <param name="indices">Index array addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        public unsafe static float GetValue(this BaseArray<float> A, InArray<long> indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, out var storage);
            return ((float*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)];

        }


#endregion HYCALPER AUTO GENERATED CODE

        #region bool overloads

        /// <summary>
        /// Retrieves the value of the element at the position as specified by <paramref name="d0"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension or sequential index into the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element at <b>sequential</b> index <paramref name="d0"/>. Note that 
        /// the index provided in <paramref name="d0"/> does not correspond with the storage position of the element to retrieve. This 
        /// would only be true for special cases of storage layout and dimension numbers. For arbitrary storage 
        /// layouts both are likely to differ.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{double}, long, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static bool GetValue(this BaseArray<bool> A, long d0) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>, out var storage);
            return ((byte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0)] != 0;

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> and <paramref name="d1"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first two dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions (<see cref="ArrayStyles.ILNumericsV4"/>).</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{double}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static bool GetValue(this BaseArray<bool> A, long d0, long d1) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>, out var storage);
            return ((byte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1)] != 0;

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d2"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first three dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions, if possible (<see cref="ArrayStyles.ILNumericsV4"/>). </para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{double}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension, or the value of <paramref name="d1"/> exceeds the number of elements in the 2nd dimension, 
        /// or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static bool GetValue(this BaseArray<bool> A, long d0, long d1, long d2) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>, out var storage);
            return ((byte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2)] != 0;

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d3"/>.  
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first four dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{double}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static bool GetValue(this BaseArray<bool> A, long d0, long d1, long d2, long d3) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>, out var storage);
            return ((byte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3)] != 0;

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d4"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first five dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{double}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static bool GetValue(this BaseArray<bool> A, long d0, long d1, long d2, long d3, long d4) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>, out var storage);
            return ((byte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4)] != 0;

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d5"/>. 
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first six dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{double}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static bool GetValue(this BaseArray<bool> A, long d0, long d1, long d2, long d3, long d4, long d5) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>, out var storage);
            return ((byte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5)] != 0;

        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d6"/>.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension. </param>
        /// <param name="d6">Index into the sevenths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the seven dimensions. </para>
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{double}, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/>, <paramref name="d5"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static bool GetValue(this BaseArray<bool> A, long d0, long d1, long d2, long d3, long d4, long d5, long d6) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>, out var storage);
            return ((byte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6)] != 0;

        }

        /// <summary>
        /// Retrieve the value at the element addressed by indices stored in an index array.
        /// </summary>
        /// <param name="indices">Index array addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        public unsafe static bool GetValue(this BaseArray<bool> A, params uint[] indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>, out var storage);
            return ((byte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)] != 0;

        }
        /// <summary>
        /// Retrieve the value at the element addressed by indices stored in an index array. <see cref="Int64"/> indexing.
        /// </summary>
        /// <param name="indices">Index array addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        public unsafe static bool GetValue(this BaseArray<bool> A, params long[] indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>, out var storage);
            return ((byte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)] != 0;

        }
        /// <summary>
        /// Retrieve the value at the element addressed by indices stored in an index array. <see cref="Int64"/> indexing.
        /// </summary>
        /// <param name="indices">Index array addressing the element to be retrieved.</param>
        /// <param name="A">The array instance.</param>
        /// <returns>The element value at the position as specified by <paramref name="indices"/>.</returns>
        public unsafe static bool GetValue(this BaseArray<bool> A, InArray<long> indices) {

            using var _1 = ReaderLock.Create(A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>, out var storage);
            return ((byte*)storage.Handles[0].Pointer)[storage.Size.GetSeqIndex(indices)] != 0;

        }
        #endregion

        #region cell overloads GetValue(...)
        /// <summary>
        /// Retrieves a clone of the value of a scalar (0-dim) cell array as an array. Supports deep indexing.
        /// </summary>
        /// <param name="A">The cell instance.</param>
        /// <returns>Copy/clone of the element value. Scalar arrays are wrapped into a scalar array.</returns>
        /// <remarks><para>This functions retrieves the value of the only element of a 0-dim scalar cell array.</para> 
        /// <para>The type of the array returned corresponds to the type of the element stored. Use 
        /// <see cref="ExtensionMethods.GetValue{T}(BaseArray{BaseArray})"/> in order to retrieve the element as its natural type. 
        /// <see cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/> can be used to inspect 
        /// the element type.</para>
        /// <para>Note, that the value addressed is not wrapped into a scalar cell before returning as would be the case for indexers on cells, 
        /// which always return cells. Thus the return value might be a cell or an ILNumerics array of any element type.</para>
        /// </remarks>
        /// <seealso cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/>
        /// <seealso cref="ToArray{T}(BaseArray)"/>
        /// <seealso cref="GetValue{T}(BaseArray{BaseArray}, long, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if <paramref name="A"/> is not a 0-dimensional (numpy) scalar.</exception>
        public unsafe static BaseArray GetValue(this BaseArray<BaseArray> A) {
            var storage = (A as ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage>).Storage;
            var ret = storage.GetStorage_DeepIndex(len: 0)?.GetBaseArrayClone();
            return ret;
        }

        /// <summary>
        /// Retrieves a clone of the value at the position <paramref name="d0"/> as an array.
        /// </summary>
        /// <param name="A">The cell instance.</param>
        /// <param name="d0">Index into the first dimension or sequential index into the array.</param>
        /// <returns>Copy/clone of the elements value. Scalar values are wrapped into a scalar array.</returns>
        /// <remarks><para>This functions retrieves the value of the element at <b>sequential</b> index <paramref name="d0"/>. Note that 
        /// the index provided in <paramref name="d0"/> does not correspond to the storage position of the element to retrieve. This 
        /// would only be true for special cases of storage layout and dimension numbers. For arbitrary storage layouts both are likely to differ.</para> 
        /// <para>The type of the array returned corresponds to the type of the element addressed by <paramref name="d0"/>. Use 
        /// <see cref="ExtensionMethods.GetValue{T}(BaseArray{BaseArray}, long)"/> in order to retrieve the element as its natural type. 
        /// <see cref="IsTypeOf{ArrayT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/> can be used to inspect 
        /// the element type.</para>
        /// <para>Note, that the value addressed is not wrapped into a scalar cell before returning as would be the case for indexers on cells, 
        /// which always return cells. Thus the return value might be a cell or an ILNumerics array of any element type.</para>
        /// </remarks>
        /// <seealso cref="IsTypeOf{ArrayT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/>
        /// <seealso cref="ToArray{T}(BaseArray)"/>
        /// <seealso cref="GetValue{T}(BaseArray{BaseArray}, long, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static BaseArray GetValue(this BaseArray<BaseArray> A, long d0) {
            var storage = (A as ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage>).Storage;
            var ret = storage.GetStorage_DeepIndex(d0,len: 1)?.GetBaseArrayClone();
            return ret; 
        }

        /// <summary>
        /// Retrieves a clone of the value at the position specified by <paramref name="d0"/> and <paramref name="d1"/> as an array. Supports deep indexing.
        /// </summary>
        /// <param name="A">The cell instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Copy/clone of the elements value. Scalar values are wrapped into a scalar array.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first two dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions.</para> 
        /// <para>The type of the array returned corresponds to the type of the element addressed by <paramref name="d0"/> and <paramref name="d1"/>. Use 
        /// <see cref="ExtensionMethods.GetValue{T}(BaseArray{BaseArray}, long)"/> in order to retrieve the element as its natural type. 
        /// <see cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/> can be used to inspect 
        /// the element type.</para>
        /// </remarks>
        /// <seealso cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/>
        /// <seealso cref="ToArray{T}(BaseArray)"/>
        /// <seealso cref="GetValue{T}(BaseArray{BaseArray}, long, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static BaseArray GetValue(this BaseArray<BaseArray> A, long d0, long d1) {
            var storage = (A as ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage>).Storage;
            var ret = storage.GetStorage_DeepIndex(d0, d1, len: 2)?.GetBaseArrayClone();
            return ret;
        }

        /// <summary>
        /// Retrieves a clone of the value at the position specified by <paramref name="d0"/> ... <paramref name="d2"/> as an array. Supports deep indexing.
        /// </summary>
        /// <param name="A">The cell instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Copy/clone of the elements value. Scalar values are wrapped into a scalar array.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by <paramref name="d0"/>...<paramref name="d2"/>. If while 
        /// searching for the element a cell is found the function steps down into the cell and continues to retrieve the value from the 
        /// inner cell using subsequent trailing indices left over (deep indexing).</para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions of this inner most array element.</para> 
        /// <para>The type of the array returned corresponds to the type of the element addressed by <paramref name="d0"/>...<paramref name="d2"/>. Use 
        /// <see cref="ExtensionMethods.GetValue{T}(BaseArray{BaseArray}, long)"/> in order to retrieve the element as its natural type. 
        /// <see cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/> can be used to inspect 
        /// the element type.</para>
        /// </remarks>
        /// <seealso cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/>
        /// <seealso cref="ToArray{T}(BaseArray)"/>
        /// <seealso cref="GetValue{T}(BaseArray{BaseArray}, long, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension, or the value of <paramref name="d1"/> exceeds the number of elements in the 2nd dimension, 
        /// or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static BaseArray GetValue(this BaseArray<BaseArray> A, long d0, long d1, long d2) {
            var storage = (A as ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage>).Storage;
            var ret = storage.GetStorage_DeepIndex(d0, d1, d2, len: 3)?.GetBaseArrayClone();
            return ret;
        }

        /// <summary>
        /// Retrieves a clone of the value at the position specified by <paramref name="d0"/> ... <paramref name="d3"/> as an array. Supports deep indexing.
        /// </summary>
        /// <param name="A">The cell instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Copy/clone of the elements value. Scalar values are wrapped into a scalar array.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by <paramref name="d0"/>...<paramref name="d3"/>. If while 
        /// searching for the element a cell is found the function steps down into the cell and continues to retrieve the value from the 
        /// inner cell using subsequent trailing indices left over (deep indexing).</para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions of this inner most array element.</para> 
        /// <para>The type of the array returned corresponds to the type of the element addressed by <paramref name="d0"/>...<paramref name="d3"/>. Use 
        /// <see cref="ExtensionMethods.GetValue{T}(BaseArray{BaseArray}, long)"/> in order to retrieve the element as its natural type. 
        /// <see cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/> can be used to inspect 
        /// the element type.</para>
        /// </remarks>
        /// <seealso cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/>
        /// <seealso cref="ToArray{T}(BaseArray)"/>
        /// <seealso cref="GetValue{T}(BaseArray{BaseArray}, long, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static BaseArray GetValue(this BaseArray<BaseArray> A, long d0, long d1, long d2, long d3) {
            var storage = (A as ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage>).Storage;
            var ret = storage.GetStorage_DeepIndex(d0, d1, d2, d3, len: 4)?.GetBaseArrayClone();
            return ret;
        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d4"/> as an array. Supports deep indexing.
        /// </summary>
        /// <param name="A">The cell instance.</param>
        /// <param name="d0">Index #0.</param>
        /// <param name="d1">Index #1.</param>
        /// <param name="d2">Index #2.</param>
        /// <param name="d3">Index #3.</param>
        /// <param name="d4">Index #4 into the last dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Copy/clone of the elements value. Scalar values are wrapped into a scalar array.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by <paramref name="d0"/>...<paramref name="d4"/>. If while 
        /// searching for the element a cell is found the function steps down into the cell and continues to retrieve the value from the 
        /// inner cell using subsequent trailing indices left over (deep indexing).</para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions of this inner most array element.</para> 
        /// <para>The type of the array returned corresponds to the type of the element addressed by <paramref name="d0"/>...<paramref name="d3"/>. Use 
        /// <see cref="ExtensionMethods.GetValue{T}(BaseArray{BaseArray}, long)"/> in order to retrieve the element as its natural type. 
        /// <see cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/> can be used to inspect 
        /// the element type.</para>
        /// </remarks>
        /// <seealso cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/>
        /// <seealso cref="ToArray{T}(BaseArray)"/>
        /// <seealso cref="GetValue{T}(BaseArray{BaseArray}, long, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static BaseArray GetValue(this BaseArray<BaseArray> A, long d0, long d1, long d2, long d3, long d4) {
            var storage = (A as ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage>).Storage;
            var ret = storage.GetStorage_DeepIndex(d0, d1, d2, d3, d4, len: 5)?.GetBaseArrayClone();
            return ret;
        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d5"/> as an array. Supports deep indexing.
        /// </summary>
        /// <param name="A">The cell instance.</param>
        /// <param name="d0">Index #0.</param>
        /// <param name="d1">Index #1.</param>
        /// <param name="d2">Index #2.</param>
        /// <param name="d3">Index #3.</param>
        /// <param name="d4">Index #4.</param>
        /// <param name="d5">Index #5 into the last dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Copy/clone of the elements value. Scalar values are wrapped into a scalar array.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by <paramref name="d0"/>...<paramref name="d5"/>. If while 
        /// searching for the element a cell is found the function steps down into the cell and continues to retrieve the value from the 
        /// inner cell using subsequent trailing indices left over (deep indexing).</para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions of this inner most array element.</para> 
        /// <para>The type of the array returned corresponds to the type of the element addressed by <paramref name="d0"/>...<paramref name="d5"/>. Use 
        /// <see cref="ExtensionMethods.GetValue{T}(BaseArray{BaseArray}, long)"/> in order to retrieve the element as its natural type. 
        /// <see cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/> can be used to inspect 
        /// the element type.</para>
        /// </remarks>
        /// <seealso cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/>
        /// <seealso cref="ToArray{T}(BaseArray)"/>
        /// <seealso cref="GetValue{T}(BaseArray{BaseArray}, long, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static BaseArray GetValue(this BaseArray<BaseArray> A, long d0, long d1, long d2, long d3, long d4, long d5) {
            var storage = (A as ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage>).Storage;
            var ret = storage.GetStorage_DeepIndex(d0, d1, d2, d3, d4, d5, len: 6)?.GetBaseArrayClone();
            return ret;
        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d6"/> as as array. Supports deep indexing.
        /// </summary>
        /// <param name="A">The cell instance.</param>
        /// <param name="d0">Index into dimension #0.</param>
        /// <param name="d1">Index into dimension #1. </param>
        /// <param name="d2">Index into dimension #2.</param>
        /// <param name="d3">Index into dimension #3. </param>
        /// <param name="d4">Index into dimension #4. </param>
        /// <param name="d5">Index into dimension #5. </param>
        /// <param name="d6">Index into thge last dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Copy/clone of the elements value. Scalar values are wrapped into a scalar array.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by <paramref name="d0"/>...<paramref name="d6"/>. If while 
        /// searching for the element a cell is found the function steps down into the cell and continues to retrieve the value from the 
        /// inner cell using subsequent trailing indices left over (deep indexing).</para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions of this inner most array element.</para> 
        /// <para>The type of the array returned corresponds to the type of the element addressed by <paramref name="d0"/>...<paramref name="d5"/>. Use 
        /// <see cref="ExtensionMethods.GetValue{T}(BaseArray{BaseArray}, long)"/> in order to retrieve the element as its natural type. 
        /// <see cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/> can be used to inspect 
        /// the element type.</para>
        /// </remarks>
        /// <seealso cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/>
        /// <seealso cref="ToArray{T}(BaseArray)"/>
        /// <seealso cref="GetValue{T}(BaseArray{BaseArray}, long, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="GetValue(BaseArray{double}, uint)"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/>, <paramref name="d5"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static BaseArray GetValue(this BaseArray<BaseArray> A, long d0, long d1, long d2, long d3, long d4, long d5, long d6) {
            var storage = (A as ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage>).Storage;
            var ret = storage.GetStorage_DeepIndex(d0, d1, d2, d3, d4, d5, d6, len: 7)?.GetBaseArrayClone();
            return ret;
        }

        /// <summary>
        /// Retrieves the value of the element addressed by the index array <paramref name="indices"/> as an array. Supports deep indexing.
        /// </summary>
        /// <param name="indices">Index array addressing the element to be retrieved.</param>
        /// <param name="A">The cell instance.</param>
        /// <returns>Copy/clone of the elements value. Scalar values are wrapped into a scalar array.</returns>
        /// <exception cref="ArgumentException">If <paramref name="indices"/> is null or have 0 or more than 7 elements.</exception>
        /// <seealso cref="GetValue(BaseArray{BaseArray}, long)"/>
        /// <seealso cref="GetValue{T}(BaseArray{BaseArray}, long)"/>
        public unsafe static BaseArray GetValue(this BaseArray<BaseArray> A, InArray<long> indices) {
            using (Scope.Enter(indices)) {
                if (Equals(indices,null) || indices.S.NumberOfElements == 0 || indices.S.NumberOfElements > 7) {
                    throw new ArgumentException($"The 'indices' argument is not valid. It must be a vector with 1...7 elements."); 
                }
                switch (indices.S.NumberOfElements) {
                    case 1:
                        return A.GetValue(indices.GetValue(0));
                    case 2:
                        return A.GetValue(indices.GetValue(0), indices.GetValue(1));
                    case 3:
                        return A.GetValue(indices.GetValue(0), indices.GetValue(1), indices.GetValue(2));
                    case 4:
                        return A.GetValue(indices.GetValue(0), indices.GetValue(1), indices.GetValue(2), indices.GetValue(3));
                    case 5:
                        return A.GetValue(indices.GetValue(0), indices.GetValue(1), indices.GetValue(2), indices.GetValue(3), indices.GetValue(4));
                    case 6:
                        return A.GetValue(indices.GetValue(0), indices.GetValue(1), indices.GetValue(2), indices.GetValue(3), indices.GetValue(4), indices.GetValue(5));
                    case 7:
                        return A.GetValue(indices.GetValue(0), indices.GetValue(1), indices.GetValue(2), indices.GetValue(3), indices.GetValue(4), indices.GetValue(5), indices.GetValue(6));
                }
                throw new NotSupportedException($"The maximum number of dimensions / indices supported for/on cell arrays is 7. Found: {indices.S.NumberOfElements} indices."); 
            }
        }
        #endregion

        #region cell overloads GetValue<T>(...) (deep indexing)
        /// <summary>
        /// Retrieves a clone of the value of a scalar (0-dim) cell array as an array. Supports deep indexing.
        /// </summary>
        /// <param name="A">The cell instance.</param>
        /// <returns>Copy/clone of the element value. Scalar values are not wrapped and returned as their natural type <typeparamref name="T"/>.</returns>
        /// <remarks><para>This functions retrieves the value of the only element of a 0-dim scalar cell array.</para> 
        /// <para>The type of the array returned corresponds to the type of the element stored. Use 
        /// <see cref="ExtensionMethods.GetValue{T}(BaseArray{BaseArray})"/> in order to retrieve the element as its natural type. 
        /// <see cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/> can be used to inspect 
        /// the element type.</para>
        /// <para>Note, that the value addressed is not wrapped into a scalar cell before returning as would be the case for indexers on cells, 
        /// which always return cells. Thus the return value might be a cell or an ILNumerics array of any element type.</para>
        /// </remarks>
        /// <seealso cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/>
        /// <seealso cref="ToArray{T}(BaseArray)"/>
        /// <seealso cref="GetValue{T}(BaseArray{BaseArray}, long, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if <paramref name="A"/> is not a 0-dimensional (numpy) scalar.</exception>
        public unsafe static T GetValue<T>(this BaseArray<BaseArray> A) {
            var storage = (A as ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage>).Storage;
            var ret = storage.GetStorage_DeepIndex(len: 0).GetBaseArray();
            T r = default(T);
            if (ret is Array<T>) {
                r = ret.ToArray<T>().GetValue(0);
            } else if (ret is Logical && r is bool) {
                r = (T)(object)ret.ToLogical().GetValue(0);
            } else {
                throw new ArgumentException($"The type T:{typeof(T).Name} given is not valid or does not match the type of the element found: {(Equals(ret, null) ? "<null>" : ret.GetType().Name)}.");
            }
            return r;
        }

        /// <summary>
        /// Retrieves a clone of the value at the position <paramref name="d0"/>. Supports deep indexing.
        /// </summary>
        /// <param name="A">The cell instance.</param>
        /// <param name="d0">Index into the first dimension or sequential index into the array.</param>
        /// <returns>Copy/clone of the elements value. Scalar values are not wrapped and returned as their natural type <typeparamref name="T"/>.</returns>
        /// <remarks><para>This functions retrieves the value of the element at <b>sequential</b> index <paramref name="d0"/>. Note that 
        /// the index provided in <paramref name="d0"/> does not correspond to the storage position of the element to retrieve. This 
        /// would only be true for special cases of storage layout and dimension numbers. For arbitrary storage layouts both are likely to differ.</para> 
        /// <para>The type of the array returned corresponds to the type of the element addressed by <paramref name="d0"/>. Use 
        /// <see cref="ExtensionMethods.GetValue{T}(BaseArray{BaseArray}, long)"/> in order to retrieve the element as its natural type. 
        /// <see cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/> can be used to inspect 
        /// the element type.</para>
        /// <para>Note, that the value addressed is not wrapped into a scalar cell before returning as would be the case for indexers on cells, 
        /// which always return cells. Thus the return value might be a cell or an ILNumerics array of any element type.</para>
        /// </remarks>
        /// <seealso cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/>
        /// <seealso cref="ToArray{T}(BaseArray)"/>
        /// <seealso cref="GetValue{T}(BaseArray{BaseArray}, long, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static T GetValue<T>(this BaseArray<BaseArray> A, long d0) {
            var storage = (A as ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage>).Storage;
            var ret = storage.GetStorage_DeepIndex(d0, len: 1)?.GetBaseArray();
            T r = default(T);
            if (ret is Array<T>) {
                r = ret.ToArray<T>().GetValue(0);
            } else if (ret is Logical && r is bool) {
                r = (T)(object)ret.ToLogical().GetValue(0); 
            } else {
                throw new ArgumentException($"The type T:{typeof(T).Name} given is not valid or does not match the type of the element found: { (Equals(ret, null) ? "<null>" : ret.GetType().Name) }."); 
            }
            return r;
        }

        /// <summary>
        /// Retrieves a clone of the value at the position specified by <paramref name="d0"/> and <paramref name="d1"/> as an array. Supports deep indexing.
        /// </summary>
        /// <param name="A">The cell instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Copy/clone of the elements value. Scalar values are wrapped into a scalar array.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first two dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions.</para> 
        /// <para>The type of the array returned corresponds to the type of the element addressed by <paramref name="d0"/> and <paramref name="d1"/>. Use 
        /// <see cref="ExtensionMethods.GetValue{T}(BaseArray{BaseArray}, long)"/> in order to retrieve the element as its natural type. 
        /// <see cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/> can be used to inspect 
        /// the element type.</para>
        /// </remarks>
        /// <seealso cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/>
        /// <seealso cref="ToArray{T}(BaseArray)"/>
        /// <seealso cref="GetValue{T}(BaseArray{BaseArray}, long, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static T GetValue<T>(this BaseArray<BaseArray> A, long d0, long d1) {
            var storage = (A as ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage>).Storage;
            var ret = storage.GetStorage_DeepIndex(d0, d1, len: 2)?.GetBaseArray();
            T r = default(T);
            if (ret is Array<T>) {
                r = ret.ToArray<T>().GetValue(0);
            } else if (ret is Logical && r is bool) {
                r = (T)(object)ret.ToLogical().GetValue(0);
            } else {
                throw new ArgumentException($"The type T given is not valid or does not match the type of the element found: { (Equals(ret, null) ? "<null>" : ret.GetType().Name) }.");
            }
            return r;
        }

        /// <summary>
        /// Retrieves a clone of the value at the position specified by <paramref name="d0"/> ... <paramref name="d2"/> as an array. Supports deep indexing.
        /// </summary>
        /// <param name="A">The cell instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Copy/clone of the elements value. Scalar values are wrapped into a scalar array.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by <paramref name="d0"/>...<paramref name="d2"/>. If while 
        /// searching for the element a cell is found the function steps down into the cell and continues to retrieve the value from the 
        /// inner cell using subsequent trailing indices left over (deep indexing).</para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions of this inner most array element.</para> 
        /// <para>The type of the array returned corresponds to the type of the element addressed by <paramref name="d0"/>...<paramref name="d2"/>. Use 
        /// <see cref="ExtensionMethods.GetValue{T}(BaseArray{BaseArray}, long)"/> in order to retrieve the element as its natural type. 
        /// <see cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/> can be used to inspect 
        /// the element type.</para>
        /// </remarks>
        /// <seealso cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/>
        /// <seealso cref="ToArray{T}(BaseArray)"/>
        /// <seealso cref="GetValue{T}(BaseArray{BaseArray}, long, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension, or the value of <paramref name="d1"/> exceeds the number of elements in the 2nd dimension, 
        /// or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static T GetValue<T>(this BaseArray<BaseArray> A, long d0, long d1, long d2) {
            var storage = (A as ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage>).Storage;
            var ret = storage.GetStorage_DeepIndex(d0, d1, d2, len: 3)?.GetBaseArray();
            T r = default(T);
            if (ret is Array<T>) {
                r = ret.ToArray<T>().GetValue(0);
            } else if (ret is Logical && r is bool) {
                r = (T)(object)ret.ToLogical().GetValue(0);
            } else {
                throw new ArgumentException($"The type T given is not valid or does not match the type of the element found: { (Equals(ret,null) ? "<null>" : ret.GetType().Name) }.");
            }
            return r;
        }

        /// <summary>
        /// Retrieves a clone of the value at the position specified by <paramref name="d0"/> ... <paramref name="d3"/> as an array. Supports deep indexing.
        /// </summary>
        /// <param name="A">The cell instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Copy/clone of the elements value. Scalar values are wrapped into a scalar array.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by <paramref name="d0"/>...<paramref name="d3"/>. If while 
        /// searching for the element a cell is found the function steps down into the cell and continues to retrieve the value from the 
        /// inner cell using subsequent trailing indices left over (deep indexing).</para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions of this inner most array element.</para> 
        /// <para>The type of the array returned corresponds to the type of the element addressed by <paramref name="d0"/>...<paramref name="d3"/>. Use 
        /// <see cref="ExtensionMethods.GetValue{T}(BaseArray{BaseArray}, long)"/> in order to retrieve the element as its natural type. 
        /// <see cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/> can be used to inspect 
        /// the element type.</para>
        /// </remarks>
        /// <seealso cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/>
        /// <seealso cref="ToArray{T}(BaseArray)"/>
        /// <seealso cref="GetValue{T}(BaseArray{BaseArray}, long, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static T GetValue<T>(this BaseArray<BaseArray> A, long d0, long d1, long d2, long d3) {
            var storage = (A as ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage>).Storage;
            var ret = storage.GetStorage_DeepIndex(d0, d1, d2, d3, len: 4)?.GetBaseArray();
            T r = default(T);
            if (ret is Array<T>) {
                r = ret.ToArray<T>().GetValue(0);
            } else if (ret is Logical && r is bool) {
                r = (T)(object)ret.ToLogical().GetValue(0);
            } else {
                throw new ArgumentException($"The type T given is not valid or does not match the type of the element found: { (Equals(ret, null) ? "<null>" : ret.GetType().Name) }.");
            }
            return r;
        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d4"/> as an array. Supports deep indexing.
        /// </summary>
        /// <param name="A">The cell instance.</param>
        /// <param name="d0">Index #0.</param>
        /// <param name="d1">Index #1.</param>
        /// <param name="d2">Index #2.</param>
        /// <param name="d3">Index #3.</param>
        /// <param name="d4">Index #4 into the last dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Copy/clone of the elements value. Scalar values are wrapped into a scalar array.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by <paramref name="d0"/>...<paramref name="d4"/>. If while 
        /// searching for the element a cell is found the function steps down into the cell and continues to retrieve the value from the 
        /// inner cell using subsequent trailing indices left over (deep indexing).</para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions of this inner most array element.</para> 
        /// <para>The type of the array returned corresponds to the type of the element addressed by <paramref name="d0"/>...<paramref name="d3"/>. Use 
        /// <see cref="ExtensionMethods.GetValue{T}(BaseArray{BaseArray}, long)"/> in order to retrieve the element as its natural type. 
        /// <see cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/> can be used to inspect 
        /// the element type.</para>
        /// </remarks>
        /// <seealso cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/>
        /// <seealso cref="ToArray{T}(BaseArray)"/>
        /// <seealso cref="GetValue{T}(BaseArray{BaseArray}, long, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static T GetValue<T>(this BaseArray<BaseArray> A, long d0, long d1, long d2, long d3, long d4) {
            var storage = (A as ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage>).Storage;
            var ret = storage.GetStorage_DeepIndex(d0, d1, d2, d3, d4, len: 5)?.GetBaseArray();
            T r = default(T);
            if (ret is Array<T>) {
                r = ret.ToArray<T>().GetValue(0);
            } else if (ret is Logical && r is bool) {
                r = (T)(object)ret.ToLogical().GetValue(0);
            } else {
                throw new ArgumentException($"The given type <{typeof(T).Name}> is not valid or does not match the type of the element found: { (Equals(ret, null) ? "<null>" : ret.GetType().Name) }.");
            }
            return r;
        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d5"/> as an array. Supports deep indexing.
        /// </summary>
        /// <param name="A">The cell instance.</param>
        /// <param name="d0">Index #0.</param>
        /// <param name="d1">Index #1.</param>
        /// <param name="d2">Index #2.</param>
        /// <param name="d3">Index #3.</param>
        /// <param name="d4">Index #4.</param>
        /// <param name="d5">Index #5 into the last dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Copy/clone of the elements value. Scalar values are wrapped into a scalar array.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by <paramref name="d0"/>...<paramref name="d5"/>. If while 
        /// searching for the element a cell is found the function steps down into the cell and continues to retrieve the value from the 
        /// inner cell using subsequent trailing indices left over (deep indexing).</para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions of this inner most array element.</para> 
        /// <para>The type of the array returned corresponds to the type of the element addressed by <paramref name="d0"/>...<paramref name="d5"/>. Use 
        /// <see cref="ExtensionMethods.GetValue{T}(BaseArray{BaseArray}, long)"/> in order to retrieve the element as its natural type. 
        /// <see cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/> can be used to inspect 
        /// the element type.</para>
        /// </remarks>
        /// <seealso cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/>
        /// <seealso cref="ToArray{T}(BaseArray)"/>
        /// <seealso cref="GetValue{T}(BaseArray{BaseArray}, long, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static T GetValue<T>(this BaseArray<BaseArray> A, long d0, long d1, long d2, long d3, long d4, long d5) {
            var storage = (A as ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage>).Storage;
            var ret = storage.GetStorage_DeepIndex(d0, d1, d2, d3, d4, d5, len: 6)?.GetBaseArray();
            T r = default(T);
            if (ret is Array<T>) {
                r = ret.ToArray<T>().GetValue(0);
            } else if (ret is Logical && r is bool) {
                r = (T)(object)ret.ToLogical().GetValue(0);
            } else {
                throw new ArgumentException($"The type T given is not valid or does not match the type of the element found: { (Equals(ret, null) ? "<null>" : ret.GetType().Name) }.");
            }
            return r;
        }

        /// <summary>
        /// Retrieves the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d6"/> as as array. Supports deep indexing.
        /// </summary>
        /// <param name="A">The cell instance.</param>
        /// <param name="d0">Index into dimension #0.</param>
        /// <param name="d1">Index into dimension #1. </param>
        /// <param name="d2">Index into dimension #2.</param>
        /// <param name="d3">Index into dimension #3. </param>
        /// <param name="d4">Index into dimension #4. </param>
        /// <param name="d5">Index into dimension #5. </param>
        /// <param name="d6">Index into thge last dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Copy/clone of the elements value. Scalar values are wrapped into a scalar array.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by <paramref name="d0"/>...<paramref name="d6"/>. If while 
        /// searching for the element a cell is found the function steps down into the cell and continues to retrieve the value from the 
        /// inner cell using subsequent trailing indices left over (deep indexing).</para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions of this inner most array element.</para> 
        /// <para>The type of the array returned corresponds to the type of the element addressed by <paramref name="d0"/>...<paramref name="d5"/>. Use 
        /// <see cref="ExtensionMethods.GetValue{T}(BaseArray{BaseArray}, long)"/> in order to retrieve the element as its natural type. 
        /// <see cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/> can be used to inspect 
        /// the element type.</para>
        /// </remarks>
        /// <seealso cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/>
        /// <seealso cref="ToArray{T}(BaseArray)"/>
        /// <seealso cref="GetValue{T}(BaseArray{BaseArray}, long, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="GetValue(BaseArray{double}, uint)"/>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/>, <paramref name="d5"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        public unsafe static T GetValue<T>(this BaseArray<BaseArray> A, long d0, long d1, long d2, long d3, long d4, long d5, long d6) {
            var storage = (A as ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage>).Storage;
            var ret = storage.GetStorage_DeepIndex(d0, d1, d2, d3, d4, d5, d6, len: 7)?.GetBaseArray();
            T r = default(T);
            if (ret is Array<T>) {
                r = ret.ToArray<T>().GetValue(0);
            } else if (ret is Logical && r is bool) {
                r = (T)(object)ret.ToLogical().GetValue(0);
            } else {
                throw new ArgumentException($"The type T given is not valid or does not match the type of the element found: { (Equals(ret, null) ? "<null>" : ret.GetType().Name) }.");
            }
            return r;
        }

        /// <summary>
        /// Retrieves the value of the element addressed by the index array <paramref name="indices"/>. Supports deep indexing.
        /// </summary>
        /// <param name="indices">Index array addressing the element to be retrieved.</param>
        /// <param name="A">The cell instance.</param>
        /// <returns>Copy/clone of the elements value. Scalar values are wrapped into a scalar array.</returns>
        /// <exception cref="ArgumentException">If <paramref name="indices"/> is null or have 0 or more than 7 elements.</exception>
        /// <seealso cref="GetValue(BaseArray{BaseArray}, long)"/>
        /// <seealso cref="GetValue{T}(BaseArray{BaseArray}, long)"/>
        public unsafe static T GetValue<T>(this BaseArray<BaseArray> A, InArray<long> indices) {
            using (Scope.Enter(indices)) {
                if (Equals(indices, null) || indices.S.NumberOfElements == 0 || indices.S.NumberOfElements > 7) {
                    throw new ArgumentException($"The 'indices' argument is not valid. It must be a vector with 1...7 elements.");
                }
                switch (indices.S.NumberOfElements) {
                    case 1:
                        return A.GetValue<T>(indices.GetValue(0));
                    case 2:
                        return A.GetValue<T>(indices.GetValue(0), indices.GetValue(1));
                    case 3:
                        return A.GetValue<T>(indices.GetValue(0), indices.GetValue(1), indices.GetValue(2));
                    case 4:
                        return A.GetValue<T>(indices.GetValue(0), indices.GetValue(1), indices.GetValue(2), indices.GetValue(3));
                    case 5:
                        return A.GetValue<T>(indices.GetValue(0), indices.GetValue(1), indices.GetValue(2), indices.GetValue(3), indices.GetValue(4));
                    case 6:
                        return A.GetValue<T>(indices.GetValue(0), indices.GetValue(1), indices.GetValue(2), indices.GetValue(3), indices.GetValue(4), indices.GetValue(5));
                    case 7:
                        return A.GetValue<T>(indices.GetValue(0), indices.GetValue(1), indices.GetValue(2), indices.GetValue(3), indices.GetValue(4), indices.GetValue(5), indices.GetValue(6));
                }
                return default(T); // will not happen
            }
        }
        #endregion

        #region generic GetValue() : class/struct types
        /// <summary>
        /// Retrieves the value of the element at the position as specified by <paramref name="d0"/>. <see cref="Int64"/> indexing.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension or sequential index into the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element at <b>sequential</b> index <paramref name="d0"/>. Note that 
        /// the index provided in <paramref name="d0"/> does not correspond with the storage position of the element to retrieve. This 
        /// would only be true for special cases of storage layout and dimension numbers. For arbitrary storage 
        /// layouts both are likely to differ.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{double}, uint, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        
        public unsafe static T GetValue<T>(this BaseArray<T> A, long d0) {
            if (default(T) is ValueType) {
                // bool is ruled out: more concrete extension overload is picked by the compiler
                var storage = (A as ConcreteArray<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>>).Storage; 
                byte* p = (byte*)storage.Handles[0].Pointer;
                var ret = Unsafe.Read<T>((void*)(p + storage.Size.GetSeqIndex(d0) * Storage<T>.SizeOfT));
                
                return ret; 
            } else {
                // arbitrary reference type array (Cell is handled separately below, similar to bool)
                var storage = (A as ConcreteArray<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>>).Storage;
                var val = (storage.Handles[0] as ManagedHostHandle<T>).HostArray[storage.S.GetSeqIndex(d0)];
                 
                return val;
            }
        }

        /// <summary>
        /// Retrieve the value of the element at the position specified by <paramref name="d0"/> and <paramref name="d1"/>. <see cref="Int64"/> indexing.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first two dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{double}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        
        public unsafe static T GetValue<T>(this BaseArray<T> A, long d0, long d1) {
            if (default(T) is ValueType) {
                // bool is ruled out: more concrete extension overload is picked by the compiler
                var storage = (A as ConcreteArray<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>>).Storage;
                byte* p = (byte*)storage.Handles[0].Pointer;
                var ret = Unsafe.Read<T>((void*)(p + storage.Size.GetSeqIndex(d0, d1) * Storage<T>.SizeOfT));
                
                return ret;
            } else {
                // arbitrary reference type array (Cell is handled separately below, similar to bool)
                var storage = (A as ConcreteArray<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>>).Storage;
                var val = (storage.Handles[0] as ManagedHostHandle<T>).HostArray[storage.S.GetSeqIndex(d0, d1)];
                
                return val;
            }
        }

        /// <summary>
        /// Retrieve the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d2"/>. <see cref="Int64"/> indexing.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first three dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{double}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension, or the value of <paramref name="d1"/> exceeds the number of elements in the 2nd dimension, 
        /// or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        
        public unsafe static T GetValue<T>(this BaseArray<T> A, long d0, long d1, long d2) {
            if (default(T) is ValueType) {
                // bool is ruled out: more concrete extension overload is picked by the compiler
                var storage = (A as ConcreteArray<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>>).Storage;
                byte* p = (byte*)storage.Handles[0].Pointer;
                var ret = Unsafe.Read<T>((void*)(p + storage.Size.GetSeqIndex(d0, d1, d2) * Storage<T>.SizeOfT));
                
                return ret;
            } else {
                // arbitrary reference type array (Cell is handled separately below, similar to bool)
                var storage = (A as ConcreteArray<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>>).Storage;
                var val = (storage.Handles[0] as ManagedHostHandle<T>).HostArray[storage.S.GetSeqIndex(d0, d1, d2)];
                
                return val;
            }
        }

        /// <summary>
        /// Retrieve the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d3"/>. <see cref="Int64"/> indexing.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first four dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{double}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        
        public unsafe static T GetValue<T>(this BaseArray<T> A, long d0, long d1, long d2, long d3) {
            if (default(T) is ValueType) {
                // bool is ruled out: more concrete extension overload is picked by the compiler
                var storage = (A as ConcreteArray<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>>).Storage;
                byte* p = (byte*)storage.Handles[0].Pointer;
                var ret = Unsafe.Read<T>((void*)(p + storage.Size.GetSeqIndex(d0, d1, d2, d3) * Storage<T>.SizeOfT));
                
                return ret;
            } else {
                // arbitrary reference type array (Cell is handled separately below, similar to bool)
                var storage = (A as ConcreteArray<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>>).Storage;
                var val = (storage.Handles[0] as ManagedHostHandle<T>).HostArray[storage.S.GetSeqIndex(d0, d1, d2, d3)];
                
                return val;
            }
        }

        /// <summary>
        /// Retrieve the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d4"/>. <see cref="Int64"/> indexing.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first five dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{double}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        
        public unsafe static T GetValue<T>(this BaseArray<T> A, long d0, long d1, long d2, long d3, long d4) {
            if (default(T) is ValueType) {
                // bool is ruled out: more concrete extension overload is picked by the compiler
                var storage = (A as ConcreteArray<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>>).Storage;
                byte* p = (byte*)storage.Handles[0].Pointer;
                var ret = Unsafe.Read<T>((void*)(p + storage.Size.GetSeqIndex(d0, d1, d2, d3,d4) * Storage<T>.SizeOfT));
                
                return ret;
            } else {
                // arbitrary reference type array (Cell is handled separately below, similar to bool)
                var storage = (A as ConcreteArray<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>>).Storage;
                var val = (storage.Handles[0] as ManagedHostHandle<T>).HostArray[storage.S.GetSeqIndex(d0, d1, d2,d3,d4)];
                
                return val;
            }
        }

        /// <summary>
        /// Retrieve the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d5"/>. <see cref="Int64"/> indexing.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first six dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{double}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        
        public unsafe static T GetValue<T>(this BaseArray<T> A, long d0, long d1, long d2, long d3, long d4, long d5) {
            if (default(T) is ValueType) {
                // bool is ruled out: more concrete extension overload is picked by the compiler
                var storage = (A as ConcreteArray<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>>).Storage;
                byte* p = (byte*)storage.Handles[0].Pointer;
                var ret = Unsafe.Read<T>((void*)(p + storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5) * Storage<T>.SizeOfT));
                
                return ret;
            } else {
                // arbitrary reference type array (Cell is handled separately below, similar to bool)
                var storage = (A as ConcreteArray<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>>).Storage;
                var val = (storage.Handles[0] as ManagedHostHandle<T>).HostArray[storage.S.GetSeqIndex(d0, d1, d2, d3, d4, d5)];
                
                return val;
            }
        }

        /// <summary>
        /// Retrieve the value of the element at the position specified by <paramref name="d0"/> ... <paramref name="d6"/>. <see cref="Int64"/> indexing.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension. </param>
        /// <param name="d6">Index into the sevenths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the seven dimensions. </para>
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{double}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/>, <paramref name="d5"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        
        public unsafe static T GetValue<T>(this BaseArray<T> A, long d0, long d1, long d2, long d3, long d4, long d5, long d6) {
            if (default(T) is ValueType) {
                // bool is ruled out: more concrete extension overload is picked by the compiler
                var storage = (A as ConcreteArray<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>>).Storage;
                byte* p = (byte*)storage.Handles[0].Pointer;
                var ret = Unsafe.Read<T>((void*)(p + storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6) * Storage<T>.SizeOfT));
                
                return ret;
            } else {
                // arbitrary reference type array (Cell is handled separately below, similar to bool)
                var storage = (A as ConcreteArray<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>>).Storage;
                var val = (storage.Handles[0] as ManagedHostHandle<T>).HostArray[storage.S.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6)];
                
                return val;
            }
        }

        #endregion

#if OBSOLETE

        // This is not valid! It would bring nasty API (Intellisense-wise) and ambiquities with concrete <double>,... implementations!

        /// <summary>
        /// Retrieve the value of the element at the position specified by <paramref name="d0"/> and <paramref name="d1"/>. <see cref="Int64"/> indexing.
        /// </summary>
        /// <param name="A">The array instance.</param>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first two dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions.</para> 
        /// </remarks>
        /// <seealso cref="GetValue(BaseArray{double}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        
        public unsafe static T GetValue<T,LocalT,InT,OutT,RetT,StorageT>(
            this ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A, long d0, long d1)
                    where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
                    where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                    where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
                    where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                    where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            {
                return A.Storage.GetValueSeq(A.S.GetSeqIndex(d0, d1)); 
        }

#endif
    }
}
