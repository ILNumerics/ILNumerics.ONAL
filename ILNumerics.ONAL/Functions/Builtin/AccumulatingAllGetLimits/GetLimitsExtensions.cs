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
using ILNumerics.Core;
using ILNumerics.Core.StorageLayer;
using System;
using System.Security;
using System.Threading;
using ILNumerics.Core.Global;

namespace ILNumerics {

    public static partial class ExtensionMethods {

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
            <source locate="comment">
                summary
            </source>
            <destination>float</destination>
            <destination>complex</destination>
            <destination>fcomplex</destination>
        </type>
        </hycalper>
        */

        /// <summary>
        /// Finds the minimum and maximum values among all elements of <paramref name="A"/>, optionally ignoring special floating point values.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="min">[Output] The minimum value in <paramref name="A"/>.</param>
        /// <param name="max">[Output] The maximum value in <paramref name="A"/>.</param>
        /// <param name="ignoreInfinity">[Optional] When true only non-infinity and non-NaN values will be considered as regular values. Default: false.</param>
        /// <returns>True, if the minimum and maximum values could be computed. False otherwise.</returns>
        /// <remarks><para>For complex values the real and imaginary parts of all elements are considered independently 
        /// and the limits are returned separated in the real and imaginary parts of <paramref name="min"/> and <paramref name="max"/>. </para>
        /// <para>The operation is efficiently performed on all elements of the input 
        /// <paramref name="A"/> in a single run.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings 
        /// of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input <paramref name="A"/> is not altered.</para>
        /// <para>The function always succeeds and returns <c>true</c> for non-empty arrays of integer 
        /// (non-floating point) element types. Overloads for floating point element types:
        /// the function succeeds on non-empty arrays which have at least one element other than NaN and / or 'Infinity' -
        ///  depending on the setting of <paramref name="ignoreInfinity"/>.</para>
        /// <para>The optional parameter <paramref name="ignoreInfinity"/> is useful, for example, to determine the 
        /// minimum and maximum values stored inside this array in order to find scaling factors and for 
        /// normalization purposes. Here, <see cref="Double.NegativeInfinity"/> and <see cref="Double.PositiveInfinity"/>
        /// can be ignored since they would be not useful as scaling factors. For the default value ('false')
        /// infinity values are considered regular values and contribute to the result.</para>
        /// <para>For arrays of real element types (<see cref="Double"/> and <see cref="Single"/>) the value 
        /// of the output parameters <paramref name="min"/> and <paramref name="max"/> is undefined if the function
        /// returns <see langword="false"/>. For <b>complex</b> element types the individual parts of <paramref name="min"/>
        /// and <paramref name="max"/> determine the limits found among the real parts and among the imaginary parts. If 
        /// limits could be computed for the real parts of the elements these limits will be stored into the real parts 
        /// of <paramref name="min"/> and <paramref name="max"/>. Similarly, the limits among the imaginary 
        /// elements are stored into the imaginary parts of <paramref name="min"/> and <paramref name="max"/>. If no limit
        /// could be computed (because all real or all imaginary element parts are all either NaN or Infinity (and 
        /// <paramref name="ignoreInfinity"/> was <see langword="true"/>) the corresponding part of <paramref name="min"/> 
        /// and <paramref name="max"/> will be NaN.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <paramref name="A"/> is empty.</exception>
        public unsafe static bool GetLimits(this BaseArray<double> A, out double min, out double max, bool ignoreInfinity = false) {
            using var _1 = ReaderLock.Create(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, out var storage);
            var ret = storage.GetLimits(out min, out max, ignoreInfinity);
            return ret;
        }
        #endregion HYCALPER LOOPEND 
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        /// <summary>fcomplex</summary>
        /// <param name="A">Input array.</param>
        /// <param name="min">[Output] The minimum value in <paramref name="A"/>.</param>
        /// <param name="max">[Output] The maximum value in <paramref name="A"/>.</param>
        /// <param name="ignoreInfinity">[Optional] When true only non-infinity and non-NaN values will be considered as regular values. Default: false.</param>
        /// <returns>True, if the minimum and maximum values could be computed. False otherwise.</returns>
        /// <remarks><para>For complex values the real and imaginary parts of all elements are considered independently 
        /// and the limits are returned separated in the real and imaginary parts of <paramref name="min"/> and <paramref name="max"/>. </para>
        /// <para>The operation is efficiently performed on all elements of the input 
        /// <paramref name="A"/> in a single run.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings 
        /// of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input <paramref name="A"/> is not altered.</para>
        /// <para>The function always succeeds and returns <c>true</c> for non-empty arrays of integer 
        /// (non-floating point) element types. Overloads for floating point element types:
        /// the function succeeds on non-empty arrays which have at least one element other than NaN and / or 'Infinity' -
        ///  depending on the setting of <paramref name="ignoreInfinity"/>.</para>
        /// <para>The optional parameter <paramref name="ignoreInfinity"/> is useful, for example, to determine the 
        /// minimum and maximum values stored inside this array in order to find scaling factors and for 
        /// normalization purposes. Here, <see cref="Double.NegativeInfinity"/> and <see cref="Double.PositiveInfinity"/>
        /// can be ignored since they would be not useful as scaling factors. For the default value ('false')
        /// infinity values are considered regular values and contribute to the result.</para>
        /// <para>For arrays of real element types (<see cref="Double"/> and <see cref="Single"/>) the value 
        /// of the output parameters <paramref name="min"/> and <paramref name="max"/> is undefined if the function
        /// returns <see langword="false"/>. For <b>complex</b> element types the individual parts of <paramref name="min"/>
        /// and <paramref name="max"/> determine the limits found among the real parts and among the imaginary parts. If 
        /// limits could be computed for the real parts of the elements these limits will be stored into the real parts 
        /// of <paramref name="min"/> and <paramref name="max"/>. Similarly, the limits among the imaginary 
        /// elements are stored into the imaginary parts of <paramref name="min"/> and <paramref name="max"/>. If no limit
        /// could be computed (because all real or all imaginary element parts are all either NaN or Infinity (and 
        /// <paramref name="ignoreInfinity"/> was <see langword="true"/>) the corresponding part of <paramref name="min"/> 
        /// and <paramref name="max"/> will be NaN.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <paramref name="A"/> is empty.</exception>
        public unsafe static bool GetLimits(this BaseArray<fcomplex> A, out fcomplex min, out fcomplex max, bool ignoreInfinity = false) {
            using var _1 = ReaderLock.Create(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, out var storage);
            var ret = storage.GetLimits(out min, out max, ignoreInfinity);
            return ret;
        }
       

        /// <summary>complex</summary>
        /// <param name="A">Input array.</param>
        /// <param name="min">[Output] The minimum value in <paramref name="A"/>.</param>
        /// <param name="max">[Output] The maximum value in <paramref name="A"/>.</param>
        /// <param name="ignoreInfinity">[Optional] When true only non-infinity and non-NaN values will be considered as regular values. Default: false.</param>
        /// <returns>True, if the minimum and maximum values could be computed. False otherwise.</returns>
        /// <remarks><para>For complex values the real and imaginary parts of all elements are considered independently 
        /// and the limits are returned separated in the real and imaginary parts of <paramref name="min"/> and <paramref name="max"/>. </para>
        /// <para>The operation is efficiently performed on all elements of the input 
        /// <paramref name="A"/> in a single run.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings 
        /// of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input <paramref name="A"/> is not altered.</para>
        /// <para>The function always succeeds and returns <c>true</c> for non-empty arrays of integer 
        /// (non-floating point) element types. Overloads for floating point element types:
        /// the function succeeds on non-empty arrays which have at least one element other than NaN and / or 'Infinity' -
        ///  depending on the setting of <paramref name="ignoreInfinity"/>.</para>
        /// <para>The optional parameter <paramref name="ignoreInfinity"/> is useful, for example, to determine the 
        /// minimum and maximum values stored inside this array in order to find scaling factors and for 
        /// normalization purposes. Here, <see cref="Double.NegativeInfinity"/> and <see cref="Double.PositiveInfinity"/>
        /// can be ignored since they would be not useful as scaling factors. For the default value ('false')
        /// infinity values are considered regular values and contribute to the result.</para>
        /// <para>For arrays of real element types (<see cref="Double"/> and <see cref="Single"/>) the value 
        /// of the output parameters <paramref name="min"/> and <paramref name="max"/> is undefined if the function
        /// returns <see langword="false"/>. For <b>complex</b> element types the individual parts of <paramref name="min"/>
        /// and <paramref name="max"/> determine the limits found among the real parts and among the imaginary parts. If 
        /// limits could be computed for the real parts of the elements these limits will be stored into the real parts 
        /// of <paramref name="min"/> and <paramref name="max"/>. Similarly, the limits among the imaginary 
        /// elements are stored into the imaginary parts of <paramref name="min"/> and <paramref name="max"/>. If no limit
        /// could be computed (because all real or all imaginary element parts are all either NaN or Infinity (and 
        /// <paramref name="ignoreInfinity"/> was <see langword="true"/>) the corresponding part of <paramref name="min"/> 
        /// and <paramref name="max"/> will be NaN.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <paramref name="A"/> is empty.</exception>
        public unsafe static bool GetLimits(this BaseArray<complex> A, out complex min, out complex max, bool ignoreInfinity = false) {
            using var _1 = ReaderLock.Create(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, out var storage);
            var ret = storage.GetLimits(out min, out max, ignoreInfinity);
            return ret;
        }
       

        /// <summary>float</summary>
        /// <param name="A">Input array.</param>
        /// <param name="min">[Output] The minimum value in <paramref name="A"/>.</param>
        /// <param name="max">[Output] The maximum value in <paramref name="A"/>.</param>
        /// <param name="ignoreInfinity">[Optional] When true only non-infinity and non-NaN values will be considered as regular values. Default: false.</param>
        /// <returns>True, if the minimum and maximum values could be computed. False otherwise.</returns>
        /// <remarks><para>For complex values the real and imaginary parts of all elements are considered independently 
        /// and the limits are returned separated in the real and imaginary parts of <paramref name="min"/> and <paramref name="max"/>. </para>
        /// <para>The operation is efficiently performed on all elements of the input 
        /// <paramref name="A"/> in a single run.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings 
        /// of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input <paramref name="A"/> is not altered.</para>
        /// <para>The function always succeeds and returns <c>true</c> for non-empty arrays of integer 
        /// (non-floating point) element types. Overloads for floating point element types:
        /// the function succeeds on non-empty arrays which have at least one element other than NaN and / or 'Infinity' -
        ///  depending on the setting of <paramref name="ignoreInfinity"/>.</para>
        /// <para>The optional parameter <paramref name="ignoreInfinity"/> is useful, for example, to determine the 
        /// minimum and maximum values stored inside this array in order to find scaling factors and for 
        /// normalization purposes. Here, <see cref="Double.NegativeInfinity"/> and <see cref="Double.PositiveInfinity"/>
        /// can be ignored since they would be not useful as scaling factors. For the default value ('false')
        /// infinity values are considered regular values and contribute to the result.</para>
        /// <para>For arrays of real element types (<see cref="Double"/> and <see cref="Single"/>) the value 
        /// of the output parameters <paramref name="min"/> and <paramref name="max"/> is undefined if the function
        /// returns <see langword="false"/>. For <b>complex</b> element types the individual parts of <paramref name="min"/>
        /// and <paramref name="max"/> determine the limits found among the real parts and among the imaginary parts. If 
        /// limits could be computed for the real parts of the elements these limits will be stored into the real parts 
        /// of <paramref name="min"/> and <paramref name="max"/>. Similarly, the limits among the imaginary 
        /// elements are stored into the imaginary parts of <paramref name="min"/> and <paramref name="max"/>. If no limit
        /// could be computed (because all real or all imaginary element parts are all either NaN or Infinity (and 
        /// <paramref name="ignoreInfinity"/> was <see langword="true"/>) the corresponding part of <paramref name="min"/> 
        /// and <paramref name="max"/> will be NaN.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <paramref name="A"/> is empty.</exception>
        public unsafe static bool GetLimits(this BaseArray<float> A, out float min, out float max, bool ignoreInfinity = false) {
            using var _1 = ReaderLock.Create(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, out var storage);
            var ret = storage.GetLimits(out min, out max, ignoreInfinity);
            return ret;
        }

#endregion HYCALPER AUTO GENERATED CODE


        #region HYCALPER LOOPSTART
        /*!HC:TYPELIST:
        <hycalper>
        <type>
            <source locate="here">
                sbyte
            </source>
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
        /// Finds the minimum and maximum values among all elements of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="min">[Output] The minimum value in <paramref name="A"/>.</param>
        /// <param name="max">[Output] The maximum value in <paramref name="A"/>.</param>
        /// <returns>True, if the minimum and maximum values could be computed. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/> in a single run.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>The function always succeeds and returns <c>true</c> for non-empty arrays of integer 
        /// (non-floating point) element types. Overloads for floating point element types
        /// the function succeeds on non-empty arrays which have at least one element other than NaN.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <paramref name="A"/> is empty.</exception>
        public unsafe static bool GetLimits(this BaseArray<sbyte> A, out sbyte min, out sbyte max) {
            using var _1 = ReaderLock.Create(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, out var storage);
            var ret = storage.GetLimits(out min, out max); 
            return ret; 
        }

        #endregion HYCALPER LOOPEND 
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       


        /// <summary>
        /// Finds the minimum and maximum values among all elements of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="min">[Output] The minimum value in <paramref name="A"/>.</param>
        /// <param name="max">[Output] The maximum value in <paramref name="A"/>.</param>
        /// <returns>True, if the minimum and maximum values could be computed. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/> in a single run.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>The function always succeeds and returns <c>true</c> for non-empty arrays of integer 
        /// (non-floating point) element types. Overloads for floating point element types
        /// the function succeeds on non-empty arrays which have at least one element other than NaN.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <paramref name="A"/> is empty.</exception>
        public unsafe static bool GetLimits(this BaseArray<ulong> A, out ulong min, out ulong max) {
            using var _1 = ReaderLock.Create(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, out var storage);
            var ret = storage.GetLimits(out min, out max); 
            return ret; 
        }

       


        /// <summary>
        /// Finds the minimum and maximum values among all elements of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="min">[Output] The minimum value in <paramref name="A"/>.</param>
        /// <param name="max">[Output] The maximum value in <paramref name="A"/>.</param>
        /// <returns>True, if the minimum and maximum values could be computed. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/> in a single run.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>The function always succeeds and returns <c>true</c> for non-empty arrays of integer 
        /// (non-floating point) element types. Overloads for floating point element types
        /// the function succeeds on non-empty arrays which have at least one element other than NaN.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <paramref name="A"/> is empty.</exception>
        public unsafe static bool GetLimits(this BaseArray<long> A, out long min, out long max) {
            using var _1 = ReaderLock.Create(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, out var storage);
            var ret = storage.GetLimits(out min, out max); 
            return ret; 
        }

       


        /// <summary>
        /// Finds the minimum and maximum values among all elements of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="min">[Output] The minimum value in <paramref name="A"/>.</param>
        /// <param name="max">[Output] The maximum value in <paramref name="A"/>.</param>
        /// <returns>True, if the minimum and maximum values could be computed. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/> in a single run.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>The function always succeeds and returns <c>true</c> for non-empty arrays of integer 
        /// (non-floating point) element types. Overloads for floating point element types
        /// the function succeeds on non-empty arrays which have at least one element other than NaN.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <paramref name="A"/> is empty.</exception>
        public unsafe static bool GetLimits(this BaseArray<uint> A, out uint min, out uint max) {
            using var _1 = ReaderLock.Create(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, out var storage);
            var ret = storage.GetLimits(out min, out max); 
            return ret; 
        }

       


        /// <summary>
        /// Finds the minimum and maximum values among all elements of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="min">[Output] The minimum value in <paramref name="A"/>.</param>
        /// <param name="max">[Output] The maximum value in <paramref name="A"/>.</param>
        /// <returns>True, if the minimum and maximum values could be computed. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/> in a single run.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>The function always succeeds and returns <c>true</c> for non-empty arrays of integer 
        /// (non-floating point) element types. Overloads for floating point element types
        /// the function succeeds on non-empty arrays which have at least one element other than NaN.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <paramref name="A"/> is empty.</exception>
        public unsafe static bool GetLimits(this BaseArray<int> A, out int min, out int max) {
            using var _1 = ReaderLock.Create(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, out var storage);
            var ret = storage.GetLimits(out min, out max); 
            return ret; 
        }

       


        /// <summary>
        /// Finds the minimum and maximum values among all elements of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="min">[Output] The minimum value in <paramref name="A"/>.</param>
        /// <param name="max">[Output] The maximum value in <paramref name="A"/>.</param>
        /// <returns>True, if the minimum and maximum values could be computed. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/> in a single run.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>The function always succeeds and returns <c>true</c> for non-empty arrays of integer 
        /// (non-floating point) element types. Overloads for floating point element types
        /// the function succeeds on non-empty arrays which have at least one element other than NaN.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <paramref name="A"/> is empty.</exception>
        public unsafe static bool GetLimits(this BaseArray<ushort> A, out ushort min, out ushort max) {
            using var _1 = ReaderLock.Create(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, out var storage);
            var ret = storage.GetLimits(out min, out max); 
            return ret; 
        }

       


        /// <summary>
        /// Finds the minimum and maximum values among all elements of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="min">[Output] The minimum value in <paramref name="A"/>.</param>
        /// <param name="max">[Output] The maximum value in <paramref name="A"/>.</param>
        /// <returns>True, if the minimum and maximum values could be computed. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/> in a single run.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>The function always succeeds and returns <c>true</c> for non-empty arrays of integer 
        /// (non-floating point) element types. Overloads for floating point element types
        /// the function succeeds on non-empty arrays which have at least one element other than NaN.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <paramref name="A"/> is empty.</exception>
        public unsafe static bool GetLimits(this BaseArray<short> A, out short min, out short max) {
            using var _1 = ReaderLock.Create(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, out var storage);
            var ret = storage.GetLimits(out min, out max); 
            return ret; 
        }

       


        /// <summary>
        /// Finds the minimum and maximum values among all elements of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="min">[Output] The minimum value in <paramref name="A"/>.</param>
        /// <param name="max">[Output] The maximum value in <paramref name="A"/>.</param>
        /// <returns>True, if the minimum and maximum values could be computed. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/> in a single run.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>The function always succeeds and returns <c>true</c> for non-empty arrays of integer 
        /// (non-floating point) element types. Overloads for floating point element types
        /// the function succeeds on non-empty arrays which have at least one element other than NaN.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <paramref name="A"/> is empty.</exception>
        public unsafe static bool GetLimits(this BaseArray<byte> A, out byte min, out byte max) {
            using var _1 = ReaderLock.Create(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, out var storage);
            var ret = storage.GetLimits(out min, out max); 
            return ret; 
        }


#endregion HYCALPER AUTO GENERATED CODE

    }
}


