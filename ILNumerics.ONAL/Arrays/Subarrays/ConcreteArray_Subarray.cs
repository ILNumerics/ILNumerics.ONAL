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
using ILNumerics.Core.Global;
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.Arrays {

    public abstract partial class ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT>   // T1 must not read "T" to prevent from conflicts with the .T (transpose) property.
        : BaseArray<T1>, IEnumerable<T1>
        where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {

        #region Subarray, DimSpec arguments
        /// <summary>
        /// Extract part from this array. Matlab® and numpy indexing.
        /// </summary>
        /// <param name="d0">Range specification along the first dimension.</param>
        /// <returns>New array as vector with parts of this array elements as specified by <paramref name="d0"/>.</returns>
        /// <remarks><para>This method considers a single dimension specifier only. For multi-dimensional arrays 
        /// a flattened version is considered and the range <paramref name="d0"/> is extracted from the 
        /// flattened version. The term 'flatten' here means: reshaping the array to a 1-dimensional array by lining-up all 
        /// elements of the array in column major order.</para>
        /// <para>The setting <see cref="Settings.MinNumberOfArrayDimensions"/> is respected for the array returned.</para>
        /// <para>The setting <see cref="Settings.ArrayStyle"/> is considered. In <see cref="ArrayStyles.ILNumericsV4"/> 
        /// array style (default) it allows <paramref name="d0"/> to refer to any element inside <i>all</i> dimensions of this array (and 
        /// not only inside this dimension) in the same manner as the 'linear indexing' feature known from 
        /// Matlab® et al.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if the range provided in <paramref name="d0"/> addresses 
        /// any elements outside the range of this array. I.e.: <paramref name="d0"/> 
        /// refers to a non-existing element. See <see cref="Settings.ArrayStyle"/>.</exception>
        /// <exception cref="NotSupportedException"> if <paramref name="d0"/> attempts to use an indexing feature which 
        /// is not supported in the current <see cref="Settings.ArrayStyle"/> mode. For example, if <paramref name="d0"/>
        /// was provided as <see cref="Globals.newaxis"/> while in <see cref="ArrayStyles.ILNumericsV4"/> mode.</exception>
        /// <seealso cref="Subarray(DimSpec,DimSpec)"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray)"/>
        /// <seealso cref="Subarray(BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{BaseArray}, long, long)"/>
        /// <seealso cref="GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="GetItem(long)"/>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        public unsafe RetT Subarray(DimSpec d0) {

            using var _1 = ReaderLock.Create(this, out var storage);
            var ret = (Settings.ArrayStyle == ArrayStyles.ILNumericsV4 ? 
                storage.GetRange_ML(d0, false) : 
                storage.GetRange_np(d0)).RetArray; 
            d0?.Dispose(); 
            return ret;

        }
        /// <summary>
        /// Extract part of this array. Matlab® and numpy indexing.
        /// </summary>
        /// <param name="d0">Range specification along dimension #0.</param>
        /// <param name="d1">Range specification along dimension #1.</param>
        /// <returns>New array with parts of this array as specified by <paramref name="d0"/> and <paramref name="d1"/>.</returns>
        /// <remarks>
        /// <para>This method considers the given dimensional range specifiers to extract a subarray from this array. 
        /// Both indexing styles: <see cref="ArrayStyles.numpy"/> and Matlab® indexing (<see cref="ArrayStyles.ILNumericsV4"/>)
        /// are supported. For such indexing features specific to one or the other style the current setting of 
        /// <see cref="Settings.ArrayStyle"/> is considered. In <see cref="ArrayStyles.ILNumericsV4"/> 
        /// array style (default) the last given dimension specifier may refer to elements inside <i>merged, trailing, 
        /// unspecified</i> dimensions of this array (and not only inside this dimension) in the same manner as the 'linear indexing' 
        /// feature known from Matlab®, Octave, <see cref="ArrayStyles.ILNumericsV4">ILNumerics version 4</see> et al.</para>
        /// <para>Due to the simplicity of the specifiers <paramref name="d0"/>, ... the subarray creation is optimized 
        /// to be fast and efficient, often without requiring new memory for copying elements. Note, that the array 
        /// returned may use the same element storage as this array. Only when trying to modify either one of this array 
        /// or the subarray a copy will be made.</para>
        /// <para>The setting <see cref="Settings.MinNumberOfArrayDimensions"/> is respected for the array returned.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if the range provided in a specifier <paramref name="d0"/>,... addresses 
        /// a non existing element.</exception>
        /// <exception cref="NotSupportedException"> if any of <paramref name="d0"/>,... attempts to use an indexing feature which 
        /// is not supported in the current <see cref="Settings.ArrayStyle"/> mode. For example, if <paramref name="d0"/>
        /// was provided as <see cref="Globals.newaxis"/> while being in <see cref="ArrayStyles.ILNumericsV4"/> mode.</exception>
        /// <seealso cref="Subarray(DimSpec,DimSpec)"/>
        /// <seealso cref="Subarray(BaseArray, BaseArray, BaseArray)"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{BaseArray}, long, long)"/>
        /// <seealso cref="GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="GetItem(long)"/>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        public unsafe RetT Subarray(DimSpec d0, DimSpec d1) {

            using var _1 = ReaderLock.Create(this, out var storage);
            var ret = (Settings.ArrayStyle == ArrayStyles.ILNumericsV4 ?
                storage.GetRange_ML(d0, d1, false) :
                storage.GetRange_np(d0, d1)).RetArray;
            d0?.Dispose(); d1?.Dispose();
            return ret;
        }

        /// <summary>
        /// Extract part of this array. Matlab® and numpy indexing.
        /// </summary>
        /// <param name="d0">Range specification along dimension #0.</param>
        /// <param name="d1">Range specification along dimension #1.</param>
        /// <param name="d2">Range specification along dimension #2.</param>
        /// <returns>New array with parts of this array as specified by <paramref name="d0"/> ... <paramref name="d2"/>.</returns>
        /// <remarks>
        /// <para>This method considers the given dimensional range specifiers to extract a subarray from this array. 
        /// Both indexing styles: <see cref="ArrayStyles.numpy"/> and Matlab® indexing (<see cref="ArrayStyles.ILNumericsV4"/>)
        /// are supported. For such indexing features specific to one or the other style the current setting of 
        /// <see cref="Settings.ArrayStyle"/> is considered. In <see cref="ArrayStyles.ILNumericsV4"/> 
        /// array style (default) the last given dimension specifier may refer to elements inside <i>merged, trailing, 
        /// unspecified</i> dimensions of this array (and not only inside this dimension) in the same manner as the 'linear indexing' 
        /// feature known from Matlab®, Octave, <see cref="ArrayStyles.ILNumericsV4">ILNumerics version 4</see> et al.</para>
        /// <para>Due to the simplicity of the specifiers <paramref name="d0"/>, ... the subarray creation is optimized 
        /// to be fast and efficient, often without requiring new memory for copying elements. Note, that the array 
        /// returned may use the same element storage as this array. Only when trying to modify either one of this array 
        /// or the subarray a copy will be made.</para>
        /// <para>The setting <see cref="Settings.MinNumberOfArrayDimensions"/> is respected for the array returned.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if the range provided in a specifier <paramref name="d0"/>,... addresses 
        /// a non existing element.</exception>
        /// <exception cref="NotSupportedException"> if any of <paramref name="d0"/>,... attempts to use an indexing feature which 
        /// is not supported in the current <see cref="Settings.ArrayStyle"/> mode. For example, if <paramref name="d0"/>
        /// was provided as <see cref="Globals.newaxis"/> while being in <see cref="ArrayStyles.ILNumericsV4"/> mode.</exception>
        /// <seealso cref="Subarray(DimSpec,DimSpec)"/>
        /// <seealso cref="Subarray(BaseArray, BaseArray, BaseArray)"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{BaseArray}, long, long)"/>
        /// <seealso cref="GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="GetItem(long)"/>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        public unsafe RetT Subarray(DimSpec d0, DimSpec d1, DimSpec d2) {

            using var _1 = ReaderLock.Create(this, out var storage);
            var ret = (Settings.ArrayStyle == ArrayStyles.ILNumericsV4 ?
                storage.GetRange_ML(d0, d1, d2, false) :
                storage.GetRange_np(d0, d1, d2)).RetArray;
            d0?.Dispose(); d1?.Dispose(); d2?.Dispose();
            return ret;
        }
        /// <summary>
        /// Extract part of this array. Matlab® and numpy indexing.
        /// </summary>
        /// <param name="d0">Range specification along dimension #0.</param>
        /// <param name="d1">Range specification along dimension #1.</param>
        /// <param name="d2">Range specification along dimension #2.</param>
        /// <param name="d3">Range specification along dimension #3.</param>
        /// <returns>New array with parts of this array as specified by <paramref name="d0"/> ... <paramref name="d3"/>.</returns>
        /// <remarks>
        /// <para>This method considers the given dimensional range specifiers to extract a subarray from this array. 
        /// Both indexing styles: <see cref="ArrayStyles.numpy"/> and Matlab® indexing (<see cref="ArrayStyles.ILNumericsV4"/>)
        /// are supported. For such indexing features specific to one or the other style the current setting of 
        /// <see cref="Settings.ArrayStyle"/> is considered. In <see cref="ArrayStyles.ILNumericsV4"/> 
        /// array style (default) the last given dimension specifier may refer to elements inside <i>merged, trailing, 
        /// unspecified</i> dimensions of this array (and not only inside this dimension) in the same manner as the 'linear indexing' 
        /// feature known from Matlab®, Octave, <see cref="ArrayStyles.ILNumericsV4">ILNumerics version 4</see> et al.</para>
        /// <para>Due to the simplicity of the specifiers <paramref name="d0"/>, ... the subarray creation is optimized 
        /// to be fast and efficient, often without requiring new memory for copying elements. Note, that the array 
        /// returned may use the same element storage as this array. Only when trying to modify either one of this array 
        /// or the subarray a copy will be made.</para>
        /// <para>The setting <see cref="Settings.MinNumberOfArrayDimensions"/> is respected for the array returned.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if the range provided in a specifier <paramref name="d0"/>,... addresses 
        /// a non existing element.</exception>
        /// <exception cref="NotSupportedException"> if any of <paramref name="d0"/>,... attempts to use an indexing feature which 
        /// is not supported in the current <see cref="Settings.ArrayStyle"/> mode. For example, if <paramref name="d0"/>
        /// was provided as <see cref="Globals.newaxis"/> while being in <see cref="ArrayStyles.ILNumericsV4"/> mode.</exception>
        /// <seealso cref="Subarray(DimSpec,DimSpec)"/>
        /// <seealso cref="Subarray(BaseArray, BaseArray, BaseArray)"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{BaseArray}, long, long)"/>
        /// <seealso cref="GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="GetItem(long)"/>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        public unsafe RetT Subarray(DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3) {

            using var _1 = ReaderLock.Create(this, out var storage);
            var ret = (Settings.ArrayStyle == ArrayStyles.ILNumericsV4 ?
                storage.GetRange_ML(d0, d1, d2, d3, false) :
                storage.GetRange_np(d0, d1, d2, d3)).RetArray;
            d0?.Dispose(); d1?.Dispose(); d2?.Dispose(); d3?.Dispose();
            return ret;
        }
        /// <summary>
        /// Extract part of this array. Matlab® and numpy indexing.
        /// </summary>
        /// <param name="d0">Range specification along dimension #0.</param>
        /// <param name="d1">Range specification along dimension #1.</param>
        /// <param name="d2">Range specification along dimension #2.</param>
        /// <param name="d3">Range specification along dimension #3.</param>
        /// <param name="d4">Range specification along dimension #4.</param>
        /// <returns>New array with parts of this array as specified by <paramref name="d0"/> ... <paramref name="d4"/>.</returns>
        /// <remarks>
        /// <para>This method considers the given dimensional range specifiers to extract a subarray from this array. 
        /// Both indexing styles: <see cref="ArrayStyles.numpy"/> and Matlab® indexing (<see cref="ArrayStyles.ILNumericsV4"/>)
        /// are supported. For such indexing features specific to one or the other style the current setting of 
        /// <see cref="Settings.ArrayStyle"/> is considered. In <see cref="ArrayStyles.ILNumericsV4"/> 
        /// array style (default) the last given dimension specifier may refer to elements inside <i>merged, trailing, 
        /// unspecified</i> dimensions of this array (and not only inside this dimension) in the same manner as the 'linear indexing' 
        /// feature known from Matlab®, Octave, <see cref="ArrayStyles.ILNumericsV4">ILNumerics version 4</see> et al.</para>
        /// <para>Due to the simplicity of the specifiers <paramref name="d0"/>, ... the subarray creation is optimized 
        /// to be fast and efficient, often without requiring new memory for copying elements. Note, that the array 
        /// returned may use the same element storage as this array. Only when trying to modify either one of this array 
        /// or the subarray a copy will be made.</para>
        /// <para>The setting <see cref="Settings.MinNumberOfArrayDimensions"/> is respected for the array returned.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if the range provided in a specifier <paramref name="d0"/>,... addresses 
        /// a non existing element.</exception>
        /// <exception cref="NotSupportedException"> if any of <paramref name="d0"/>,... attempts to use an indexing feature which 
        /// is not supported in the current <see cref="Settings.ArrayStyle"/> mode. For example, if <paramref name="d0"/>
        /// was provided as <see cref="Globals.newaxis"/> while being in <see cref="ArrayStyles.ILNumericsV4"/> mode.</exception>
        /// <seealso cref="Subarray(DimSpec,DimSpec)"/>
        /// <seealso cref="Subarray(BaseArray, BaseArray, BaseArray)"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{BaseArray}, long, long)"/>
        /// <seealso cref="GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="GetItem(long)"/>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        public unsafe RetT Subarray(DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4) {

            using var _1 = ReaderLock.Create(this, out var storage);
            var ret = (Settings.ArrayStyle == ArrayStyles.ILNumericsV4 ?
                storage.GetRange_ML(d0, d1, d2, d3, d4, false) :
                storage.GetRange_np(d0, d1, d2, d3, d4)).RetArray;
            d0?.Dispose(); d1?.Dispose(); d2?.Dispose(); d3?.Dispose(); d4?.Dispose();
            return ret;
        }
        /// <summary>
        /// Extract part of this array. Matlab® and numpy indexing.
        /// </summary>
        /// <param name="d0">Range specification along dimension #0.</param>
        /// <param name="d1">Range specification along dimension #1.</param>
        /// <param name="d2">Range specification along dimension #2.</param>
        /// <param name="d3">Range specification along dimension #3.</param>
        /// <param name="d4">Range specification along dimension #4.</param>
        /// <param name="d5">Range specification along dimension #5.</param>
        /// <returns>New array with parts of this array as specified by <paramref name="d0"/> ... <paramref name="d5"/>.</returns>
        /// <remarks>
        /// <para>This method considers the given dimensional range specifiers to extract a subarray from this array. 
        /// Both indexing styles: <see cref="ArrayStyles.numpy"/> and Matlab® indexing (<see cref="ArrayStyles.ILNumericsV4"/>)
        /// are supported. For such indexing features specific to one or the other style the current setting of 
        /// <see cref="Settings.ArrayStyle"/> is considered. In <see cref="ArrayStyles.ILNumericsV4"/> 
        /// array style (default) the last given dimension specifier may refer to elements inside <i>merged, trailing, 
        /// unspecified</i> dimensions of this array (and not only inside this dimension) in the same manner as the 'linear indexing' 
        /// feature known from Matlab®, Octave, <see cref="ArrayStyles.ILNumericsV4">ILNumerics version 4</see> et al.</para>
        /// <para>Due to the simplicity of the specifiers <paramref name="d0"/>, ... the subarray creation is optimized 
        /// to be fast and efficient, often without requiring new memory for copying elements. Note, that the array 
        /// returned may use the same element storage as this array. Only when trying to modify either one of this array 
        /// or the subarray a copy will be made.</para>
        /// <para>The setting <see cref="Settings.MinNumberOfArrayDimensions"/> is respected for the array returned.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if the range provided in a specifier <paramref name="d0"/>,... addresses 
        /// a non existing element.</exception>
        /// <exception cref="NotSupportedException"> if any of <paramref name="d0"/>,... attempts to use an indexing feature which 
        /// is not supported in the current <see cref="Settings.ArrayStyle"/> mode. For example, if <paramref name="d0"/>
        /// was provided as <see cref="Globals.newaxis"/> while being in <see cref="ArrayStyles.ILNumericsV4"/> mode.</exception>
        /// <seealso cref="Subarray(DimSpec,DimSpec)"/>
        /// <seealso cref="Subarray(BaseArray, BaseArray, BaseArray)"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{BaseArray}, long, long)"/>
        /// <seealso cref="GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="GetItem(long)"/>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        public unsafe RetT Subarray(DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4, DimSpec d5) {

            using var _1 = ReaderLock.Create(this, out var storage);
            var ret = (Settings.ArrayStyle == ArrayStyles.ILNumericsV4 ?
                storage.GetRange_ML(d0, d1, d2, d3, d4, d5, false) :
                storage.GetRange_np(d0, d1, d2, d3, d4, d5)).RetArray;
            d0?.Dispose(); d1?.Dispose(); d2?.Dispose(); d3?.Dispose(); d4?.Dispose(); d5?.Dispose();
            return ret;
        }
        /// <summary>
        /// Extract part of this array. Matlab® and numpy indexing.
        /// </summary>
        /// <param name="d0">Range specification along dimension #0.</param>
        /// <param name="d1">Range specification along dimension #1.</param>
        /// <param name="d2">Range specification along dimension #2.</param>
        /// <param name="d3">Range specification along dimension #3.</param>
        /// <param name="d4">Range specification along dimension #4.</param>
        /// <param name="d5">Range specification along dimension #5.</param>
        /// <param name="d6">Range specification along dimension #6.</param>
        /// <returns>New array with parts of this array as specified by <paramref name="d0"/> ... <paramref name="d6"/>.</returns>
        /// <remarks>
        /// <para>This method considers the given dimensional range specifiers to extract a subarray from this array. 
        /// Both indexing styles: <see cref="ArrayStyles.numpy"/> and Matlab® indexing (<see cref="ArrayStyles.ILNumericsV4"/>)
        /// are supported. For such indexing features specific to one or the other style the current setting of 
        /// <see cref="Settings.ArrayStyle"/> is considered. In <see cref="ArrayStyles.ILNumericsV4"/> 
        /// array style (default) the last given dimension specifier may refer to elements inside <i>merged, trailing, 
        /// unspecified</i> dimensions of this array (and not only inside this dimension) in the same manner as the 'linear indexing' 
        /// feature known from Matlab®, Octave, <see cref="ArrayStyles.ILNumericsV4">ILNumerics version 4</see> et al.</para>
        /// <para>Due to the simplicity of the specifiers <paramref name="d0"/>, ... the subarray creation is optimized 
        /// to be fast and efficient, often without requiring new memory for copying elements. Note, that the array 
        /// returned may use the same element storage as this array. Only when trying to modify either one of this array 
        /// or the subarray a copy will be made.</para>
        /// <para>The setting <see cref="Settings.MinNumberOfArrayDimensions"/> is respected for the array returned.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if the range provided in a specifier <paramref name="d0"/>,... addresses 
        /// a non existing element.</exception>
        /// <exception cref="NotSupportedException"> if any of <paramref name="d0"/>,... attempts to use an indexing feature which 
        /// is not supported in the current <see cref="Settings.ArrayStyle"/> mode. For example, if <paramref name="d0"/>
        /// was provided as <see cref="Globals.newaxis"/> while being in <see cref="ArrayStyles.ILNumericsV4"/> mode.</exception>
        /// <seealso cref="Subarray(DimSpec,DimSpec)"/>
        /// <seealso cref="Subarray(BaseArray, BaseArray, BaseArray)"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{BaseArray}, long, long)"/>
        /// <seealso cref="GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="GetItem(long)"/>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        public RetT Subarray(DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4, DimSpec d5, DimSpec d6) {

            using var _1 = ReaderLock.Create(this, out var storage);
            var ret = (Settings.ArrayStyle == ArrayStyles.ILNumericsV4 ?
                storage.GetRange_ML(d0, d1, d2, d3, d4, d5, d6, false) :
                storage.GetRange_np(d0, d1, d2, d3, d4, d5, d6)).RetArray;
            d0?.Dispose(); d1?.Dispose(); d2?.Dispose(); d3?.Dispose(); d4?.Dispose(); d5?.Dispose(); d6?.Dispose();
            return ret;
        }
        /// <summary>
        /// Extract part of this array. Matlab® and numpy indexing.
        /// </summary>
        /// <param name="dims">Range specification for arbitrary dimensions as vector.</param>
        /// <returns>New array with parts of this array as specified by <paramref name="dims"/>.</returns>
        /// <remarks>
        /// <para>This method considers the given dimensional range specifiers to extract a subarray from this array. 
        /// Both indexing styles: <see cref="ArrayStyles.numpy"/> and Matlab® indexing (<see cref="ArrayStyles.ILNumericsV4"/>)
        /// are supported. For such indexing features specific to one or the other style the current setting of 
        /// <see cref="Settings.ArrayStyle"/> is considered. For example, in <see cref="ArrayStyles.ILNumericsV4"/> 
        /// array style (default) the last given dimension specifier may refer to elements inside <i>merged, trailing, 
        /// unspecified</i> dimensions of this array (and not only inside this dimension) in the same manner as the 'linear indexing' 
        /// feature known from Matlab®, Octave, <see cref="ArrayStyles.ILNumericsV4">ILNumerics version 4</see> et al.</para>
        /// <para>Due to the simplicity of the individual specifiers <paramref name="dims"/> the subarray creation is optimized 
        /// to be fast and efficient, often without requiring new memory for copying elements. Note, that the array 
        /// returned may use the same element storage as this array. Only when trying to modify either one of this array 
        /// or the subarray a copy will be made.</para>
        /// <para>The setting <see cref="Settings.MinNumberOfArrayDimensions"/> (as controlled by <see cref="Settings.ArrayStyle"/>) 
        /// is respected for the array returned.</para>
        /// <para>This overload of Subarray() allows to define range specifiers for individual dimensions as a system array. 
        /// It is convenient when subarrays are to be created without knowing the number of dimensions and the concrete
        /// range parameters in advance. It is not recommended for situations where high performance is crucial. Note, that 
        /// individual <see cref="DimSpec"/> objects for each dimension addressed in <paramref name="dims"/> can be used in 
        /// exactly one call to <see cref="Subarray(DimSpec[])"/> and are released in the function automatically. Thus, when
        /// calling <see cref="Subarray(DimSpec[])"/> in loops make sure to recreate <i>all</i> dimension specifier prior to
        /// each call to <see cref="Subarray(DimSpec[])"/>.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if the range provided in a specifier <paramref name="dims"/> addresses 
        /// a non existing element.</exception>
        /// <exception cref="NotSupportedException"> if any of <paramref name="dims"/> attempts to use an indexing feature which 
        /// is not supported in the current <see cref="Settings.ArrayStyle"/> mode. For example, if <paramref name="dims"/>[0]
        /// was provided as <see cref="Globals.newaxis"/> while being in <see cref="ArrayStyles.ILNumericsV4"/> mode.</exception>
        /// <seealso cref="Subarray(DimSpec,DimSpec)"/>
        /// <seealso cref="Subarray(BaseArray[])"/>
        /// <seealso cref="Subarray(BaseArray, BaseArray, BaseArray)"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{BaseArray}, long, long)"/>
        /// <seealso cref="GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="GetItem(long)"/>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        public RetT Subarray(params DimSpec[] dims) {
            if (dims == null || dims.Length == 0) {
                throw new ArgumentNullException("null and empty arrays are no valid dimension specifiers!"); 
            }
            switch (dims.Length) {
                case 1:
                    return Subarray(dims[0]);
                case 2:
                    return Subarray(dims[0], dims[1]);
                case 3:
                    return Subarray(dims[0], dims[1], dims[2]);
                case 4:
                    return Subarray(dims[0], dims[1], dims[2], dims[3]);
                case 5:
                    return Subarray(dims[0], dims[1], dims[2], dims[3], dims[4]);
                case 6:
                    return Subarray(dims[0], dims[1], dims[2], dims[3], dims[4], dims[5]);
                case 7:
                    return Subarray(dims[0], dims[1], dims[2], dims[3], dims[4], dims[5], dims[6]);
                default: {

                        using var _1 = ReaderLock.Create(this, out var storage);
                        var ret = (Settings.ArrayStyle == ArrayStyles.ILNumericsV4 ?
                            storage.GetRange_ML(dims, false) :
                            storage.GetRange_np(dims)).RetArray;
                        foreach(var d in dims) d?.Dispose();
                        return ret;
                    }
            }
        }
        #endregion

        #region Subarray, BaseArray arguments
        /// <summary>
        /// Extract part of this array. Matlab® and numpy indexing.
        /// </summary>
        /// <param name="d0">Range specification for dimension #0.</param>
        /// <returns>New array with parts of this array as specified by <paramref name="d0"/>.</returns>
        /// <remarks>
        /// <para>This method considers the given dimensional range specifiers to extract a subarray from this array. 
        /// Both indexing styles: <see cref="ArrayStyles.numpy"/> and Matlab® indexing (<see cref="ArrayStyles.ILNumericsV4"/>)
        /// are supported. For such indexing features specific to one or the other style the current setting of 
        /// <see cref="Settings.ArrayStyle"/> is considered. For example, in <see cref="ArrayStyles.ILNumericsV4"/> 
        /// array style (default) the last given dimension specifier may refer to elements inside <i>merged, trailing, 
        /// unspecified</i> dimensions of this array (and not only inside this dimension) in the same manner as the 'linear indexing' 
        /// feature known from Matlab®, Octave, <see cref="ArrayStyles.ILNumericsV4">ILNumerics version 4</see> et al.</para>
        /// <para>For simple dimension specifiers <paramref name="d0"/>,... the subarray creation is often optimized 
        /// to be fast and efficient, often without requiring new memory for copying elements. Note, that the array 
        /// returned may use the same element storage as this array in this case. Only when trying to modify either 
        /// one of this array or the subarray a copy will be made. More complex subarray specifications may require 
        /// iterating and copying addressed elements into new memory. </para>
        /// <para>The setting <see cref="Settings.MinNumberOfArrayDimensions"/> (as controlled by <see cref="Settings.ArrayStyle"/>) 
        /// is respected for the array returned.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if the range provided in a specifier <paramref name="d0"/>,... 
        /// addresses a non existing element.</exception>
        /// <exception cref="NotSupportedException"> if any of <paramref name="d0"/>,... attempts to use an indexing feature which 
        /// is not supported in the current <see cref="Settings.ArrayStyle"/> mode. For example, if <paramref name="d0"/>
        /// was provided as <see cref="Globals.newaxis"/> while being in <see cref="ArrayStyles.ILNumericsV4"/> mode.</exception>
        /// <seealso cref="Subarray(BaseArray,BaseArray)"/>
        /// <seealso cref="Subarray(DimSpec[])"/>
        /// <seealso cref="Subarray(DimSpec, DimSpec, DimSpec)"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, long, long)"/>
        /// <seealso cref="GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="GetItem(long)"/>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        public unsafe RetT Subarray(BaseArray d0) {

            using var _1 = ReaderLock.Create(this, out var storage);
            // TODO: according to the spec we must keep d0 alive during processing of Subarray(). 
            // Option 1 would be to use a ReaderLock as for this storage. But it would force us 
            // use the locked storage throughout Subarray - which requires greater adoptions in the code. 
            // Thus, we leave this for future enhancements ... :| 
            var ret = (Settings.ArrayStyle == ArrayStyles.ILNumericsV4 ?
                storage.GetRange_ML(d0, false) :
                storage.GetRange_np(d0)).RetArray;
            return ret;
        }
        /// <summary>
        /// Extract part of this array. Matlab® and numpy indexing.
        /// </summary>
        /// <param name="d0">Range specification for dimension #0.</param>
        /// <param name="d1">Range specification for dimension #1.</param>
        /// <returns>New array with parts of this array as specified by <paramref name="d0"/>...<paramref name="d1"/>.</returns>
        /// <remarks>
        /// <para>This method considers the given dimensional range specifiers to extract a subarray from this array. 
        /// Both indexing styles: <see cref="ArrayStyles.numpy"/> and Matlab® indexing (<see cref="ArrayStyles.ILNumericsV4"/>)
        /// are supported. For such indexing features specific to one or the other style the current setting of 
        /// <see cref="Settings.ArrayStyle"/> is considered. For example, in <see cref="ArrayStyles.ILNumericsV4"/> 
        /// array style (default) the last given dimension specifier may refer to elements inside <i>merged, trailing, 
        /// unspecified</i> dimensions of this array (and not only inside this dimension) in the same manner as the 'linear indexing' 
        /// feature known from Matlab®, Octave, <see cref="ArrayStyles.ILNumericsV4">ILNumerics version 4</see> et al.</para>
        /// <para>For simple dimension specifiers <paramref name="d0"/>,... the subarray creation is often optimized 
        /// to be fast and efficient, often without requiring new memory for copying elements. Note, that the array 
        /// returned may use the same element storage as this array in this case. Only when trying to modify either 
        /// one of this array or the subarray a copy will be made. More complex subarray specifications may require 
        /// iterating and copying addressed elements into new memory. </para>
        /// <para>The setting <see cref="Settings.MinNumberOfArrayDimensions"/> (as controlled by <see cref="Settings.ArrayStyle"/>) 
        /// is respected for the array returned.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if the range provided in a specifier <paramref name="d0"/>,... 
        /// addresses a non existing element.</exception>
        /// <exception cref="NotSupportedException"> if any of <paramref name="d0"/>,... attempts to use an indexing feature which 
        /// is not supported in the current <see cref="Settings.ArrayStyle"/> mode. For example, if <paramref name="d0"/>
        /// was provided as <see cref="Globals.newaxis"/> while being in <see cref="ArrayStyles.ILNumericsV4"/> mode.</exception>
        /// <seealso cref="Subarray(BaseArray,BaseArray)"/>
        /// <seealso cref="Subarray(DimSpec[])"/>
        /// <seealso cref="Subarray(DimSpec, DimSpec, DimSpec)"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, long, long)"/>
        /// <seealso cref="GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="GetItem(long)"/>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        public unsafe RetT Subarray(BaseArray d0, BaseArray d1) {

            using var _1 = ReaderLock.Create(this, out var storage);
            var ret = (Settings.ArrayStyle == ArrayStyles.ILNumericsV4 ?
                storage.GetRange_ML(d0, d1, false) :
                storage.GetRange_np(d0, d1)).RetArray;
            return ret;
        }
        /// <summary>
        /// Extract part of this array. Matlab® and numpy indexing.
        /// </summary>
        /// <param name="d0">Range specification for dimension #0.</param>
        /// <param name="d1">Range specification for dimension #1.</param>
        /// <param name="d2">Range specification for dimension #2.</param>
        /// <returns>New array with parts of this array as specified by <paramref name="d0"/>...<paramref name="d2"/>.</returns>
        /// <remarks>
        /// <para>This method considers the given dimensional range specifiers to extract a subarray from this array. 
        /// Both indexing styles: <see cref="ArrayStyles.numpy"/> and Matlab® indexing (<see cref="ArrayStyles.ILNumericsV4"/>)
        /// are supported. For such indexing features specific to one or the other style the current setting of 
        /// <see cref="Settings.ArrayStyle"/> is considered. For example, in <see cref="ArrayStyles.ILNumericsV4"/> 
        /// array style (default) the last given dimension specifier may refer to elements inside <i>merged, trailing, 
        /// unspecified</i> dimensions of this array (and not only inside this dimension) in the same manner as the 'linear indexing' 
        /// feature known from Matlab®, Octave, <see cref="ArrayStyles.ILNumericsV4">ILNumerics version 4</see> et al.</para>
        /// <para>For simple dimension specifiers <paramref name="d0"/>,... the subarray creation is often optimized 
        /// to be fast and efficient, often without requiring new memory for copying elements. Note, that the array 
        /// returned may use the same element storage as this array in this case. Only when trying to modify either 
        /// one of this array or the subarray a copy will be made. More complex subarray specifications may require 
        /// iterating and copying addressed elements into new memory. </para>
        /// <para>The setting <see cref="Settings.MinNumberOfArrayDimensions"/> (as controlled by <see cref="Settings.ArrayStyle"/>) 
        /// is respected for the array returned.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if the range provided in a specifier <paramref name="d0"/>,... 
        /// addresses a non existing element.</exception>
        /// <exception cref="NotSupportedException"> if any of <paramref name="d0"/>,... attempts to use an indexing feature which 
        /// is not supported in the current <see cref="Settings.ArrayStyle"/> mode. For example, if <paramref name="d0"/>
        /// was provided as <see cref="Globals.newaxis"/> while being in <see cref="ArrayStyles.ILNumericsV4"/> mode.</exception>
        /// <seealso cref="Subarray(BaseArray,BaseArray)"/>
        /// <seealso cref="Subarray(DimSpec[])"/>
        /// <seealso cref="Subarray(DimSpec, DimSpec, DimSpec)"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, long, long)"/>
        /// <seealso cref="GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="GetItem(long)"/>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        public unsafe RetT Subarray(BaseArray d0, BaseArray d1, BaseArray d2) {

            using var _1 = ReaderLock.Create(this, out var storage);
            var ret = (Settings.ArrayStyle == ArrayStyles.ILNumericsV4 ?
                storage.GetRange_ML(d0, d1, d2, false) :
                storage.GetRange_np(d0, d1, d2)).RetArray;
            return ret;
        }
        /// <summary>
        /// Extract part of this array. Matlab® and numpy indexing.
        /// </summary>
        /// <param name="d0">Range specification for dimension #0.</param>
        /// <param name="d1">Range specification for dimension #1.</param>
        /// <param name="d2">Range specification for dimension #2.</param>
        /// <param name="d3">Range specification for dimension #3.</param>
        /// <returns>New array with parts of this array as specified by <paramref name="d0"/>...<paramref name="d3"/>.</returns>
        /// <remarks>
        /// <para>This method considers the given dimensional range specifiers to extract a subarray from this array. 
        /// Both indexing styles: <see cref="ArrayStyles.numpy"/> and Matlab® indexing (<see cref="ArrayStyles.ILNumericsV4"/>)
        /// are supported. For such indexing features specific to one or the other style the current setting of 
        /// <see cref="Settings.ArrayStyle"/> is considered. For example, in <see cref="ArrayStyles.ILNumericsV4"/> 
        /// array style (default) the last given dimension specifier may refer to elements inside <i>merged, trailing, 
        /// unspecified</i> dimensions of this array (and not only inside this dimension) in the same manner as the 'linear indexing' 
        /// feature known from Matlab®, Octave, <see cref="ArrayStyles.ILNumericsV4">ILNumerics version 4</see> et al.</para>
        /// <para>For simple dimension specifiers <paramref name="d0"/>,... the subarray creation is often optimized 
        /// to be fast and efficient, often without requiring new memory for copying elements. Note, that the array 
        /// returned may use the same element storage as this array in this case. Only when trying to modify either 
        /// one of this array or the subarray a copy will be made. More complex subarray specifications may require 
        /// iterating and copying addressed elements into new memory. </para>
        /// <para>The setting <see cref="Settings.MinNumberOfArrayDimensions"/> (as controlled by <see cref="Settings.ArrayStyle"/>) 
        /// is respected for the array returned.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if the range provided in a specifier <paramref name="d0"/>,... 
        /// addresses a non existing element.</exception>
        /// <exception cref="NotSupportedException"> if any of <paramref name="d0"/>,... attempts to use an indexing feature which 
        /// is not supported in the current <see cref="Settings.ArrayStyle"/> mode. For example, if <paramref name="d0"/>
        /// was provided as <see cref="Globals.newaxis"/> while being in <see cref="ArrayStyles.ILNumericsV4"/> mode.</exception>
        /// <seealso cref="Subarray(BaseArray,BaseArray)"/>
        /// <seealso cref="Subarray(DimSpec[])"/>
        /// <seealso cref="Subarray(DimSpec, DimSpec, DimSpec)"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, long, long)"/>
        /// <seealso cref="GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="GetItem(long)"/>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        public unsafe RetT Subarray(BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3) {

            using var _1 = ReaderLock.Create(this, out var storage);
            var ret = (Settings.ArrayStyle == ArrayStyles.ILNumericsV4 ?
                storage.GetRange_ML(d0, d1, d2, d3, false) :
                storage.GetRange_np(d0, d1, d2, d3)).RetArray;
            return ret;
        }
        /// <summary>
        /// Extract part of this array. Matlab® and numpy indexing.
        /// </summary>
        /// <param name="d0">Range specification for dimension #0.</param>
        /// <param name="d1">Range specification for dimension #1.</param>
        /// <param name="d2">Range specification for dimension #2.</param>
        /// <param name="d3">Range specification for dimension #3.</param>
        /// <param name="d4">Range specification for dimension #4.</param>
        /// <returns>New array with parts of this array as specified by <paramref name="d0"/>...<paramref name="d4"/>.</returns>
        /// <remarks>
        /// <para>This method considers the given dimensional range specifiers to extract a subarray from this array. 
        /// Both indexing styles: <see cref="ArrayStyles.numpy"/> and Matlab® indexing (<see cref="ArrayStyles.ILNumericsV4"/>)
        /// are supported. For such indexing features specific to one or the other style the current setting of 
        /// <see cref="Settings.ArrayStyle"/> is considered. For example, in <see cref="ArrayStyles.ILNumericsV4"/> 
        /// array style (default) the last given dimension specifier may refer to elements inside <i>merged, trailing, 
        /// unspecified</i> dimensions of this array (and not only inside this dimension) in the same manner as the 'linear indexing' 
        /// feature known from Matlab®, Octave, <see cref="ArrayStyles.ILNumericsV4">ILNumerics version 4</see> et al.</para>
        /// <para>For simple dimension specifiers <paramref name="d0"/>,... the subarray creation is often optimized 
        /// to be fast and efficient, often without requiring new memory for copying elements. Note, that the array 
        /// returned may use the same element storage as this array in this case. Only when trying to modify either 
        /// one of this array or the subarray a copy will be made. More complex subarray specifications may require 
        /// iterating and copying addressed elements into new memory. </para>
        /// <para>The setting <see cref="Settings.MinNumberOfArrayDimensions"/> (as controlled by <see cref="Settings.ArrayStyle"/>) 
        /// is respected for the array returned.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if the range provided in a specifier <paramref name="d0"/>,... 
        /// addresses a non existing element.</exception>
        /// <exception cref="NotSupportedException"> if any of <paramref name="d0"/>,... attempts to use an indexing feature which 
        /// is not supported in the current <see cref="Settings.ArrayStyle"/> mode. For example, if <paramref name="d0"/>
        /// was provided as <see cref="Globals.newaxis"/> while being in <see cref="ArrayStyles.ILNumericsV4"/> mode.</exception>
        /// <seealso cref="Subarray(BaseArray,BaseArray)"/>
        /// <seealso cref="Subarray(DimSpec[])"/>
        /// <seealso cref="Subarray(DimSpec, DimSpec, DimSpec)"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, long, long)"/>
        /// <seealso cref="GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="GetItem(long)"/>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        public unsafe RetT Subarray(BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3, BaseArray d4) {

            using var _1 = ReaderLock.Create(this, out var storage);
            var ret = (Settings.ArrayStyle == ArrayStyles.ILNumericsV4 ?
                storage.GetRange_ML(d0, d1, d2, d3, d4, false) :
                storage.GetRange_np(d0, d1, d2, d3, d4)).RetArray;
            return ret;
        }
        /// <summary>
        /// Extract part of this array. Matlab® and numpy indexing.
        /// </summary>
        /// <param name="d0">Range specification for dimension #0.</param>
        /// <param name="d1">Range specification for dimension #1.</param>
        /// <param name="d2">Range specification for dimension #2.</param>
        /// <param name="d3">Range specification for dimension #3.</param>
        /// <param name="d4">Range specification for dimension #4.</param>
        /// <param name="d5">Range specification for dimension #5.</param>
        /// <returns>New array with parts of this array as specified by <paramref name="d0"/>...<paramref name="d5"/>.</returns>
        /// <remarks>
        /// <para>This method considers the given dimensional range specifiers to extract a subarray from this array. 
        /// Both indexing styles: <see cref="ArrayStyles.numpy"/> and Matlab® indexing (<see cref="ArrayStyles.ILNumericsV4"/>)
        /// are supported. For such indexing features specific to one or the other style the current setting of 
        /// <see cref="Settings.ArrayStyle"/> is considered. For example, in <see cref="ArrayStyles.ILNumericsV4"/> 
        /// array style (default) the last given dimension specifier may refer to elements inside <i>merged, trailing, 
        /// unspecified</i> dimensions of this array (and not only inside this dimension) in the same manner as the 'linear indexing' 
        /// feature known from Matlab®, Octave, <see cref="ArrayStyles.ILNumericsV4">ILNumerics version 4</see> et al.</para>
        /// <para>For simple dimension specifiers <paramref name="d0"/>,... the subarray creation is often optimized 
        /// to be fast and efficient, often without requiring new memory for copying elements. Note, that the array 
        /// returned may use the same element storage as this array in this case. Only when trying to modify either 
        /// one of this array or the subarray a copy will be made. More complex subarray specifications may require 
        /// iterating and copying addressed elements into new memory. </para>
        /// <para>The setting <see cref="Settings.MinNumberOfArrayDimensions"/> (as controlled by <see cref="Settings.ArrayStyle"/>) 
        /// is respected for the array returned.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if the range provided in a specifier <paramref name="d0"/>,... 
        /// addresses a non existing element.</exception>
        /// <exception cref="NotSupportedException"> if any of <paramref name="d0"/>,... attempts to use an indexing feature which 
        /// is not supported in the current <see cref="Settings.ArrayStyle"/> mode. For example, if <paramref name="d0"/>
        /// was provided as <see cref="Globals.newaxis"/> while being in <see cref="ArrayStyles.ILNumericsV4"/> mode.</exception>
        /// <seealso cref="Subarray(BaseArray,BaseArray)"/>
        /// <seealso cref="Subarray(DimSpec[])"/>
        /// <seealso cref="Subarray(DimSpec, DimSpec, DimSpec)"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, long, long)"/>
        /// <seealso cref="GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="GetItem(long)"/>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        public unsafe RetT Subarray(BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3, BaseArray d4, BaseArray d5) {

            using var _1 = ReaderLock.Create(this, out var storage);
            var ret = (Settings.ArrayStyle == ArrayStyles.ILNumericsV4 ?
                storage.GetRange_ML(d0, d1, d2, d3, d4, d5, false) :
                storage.GetRange_np(d0, d1, d2, d3, d4, d5)).RetArray;
            return ret;
        }
        /// <summary>
        /// Extract part of this array. Matlab® and numpy indexing.
        /// </summary>
        /// <param name="d0">Range specification for dimension #0.</param>
        /// <param name="d1">Range specification for dimension #1.</param>
        /// <param name="d2">Range specification for dimension #2.</param>
        /// <param name="d3">Range specification for dimension #3.</param>
        /// <param name="d4">Range specification for dimension #4.</param>
        /// <param name="d5">Range specification for dimension #5.</param>
        /// <param name="d6">Range specification for dimension #6.</param>
        /// <returns>New array with parts of this array as specified by <paramref name="d0"/>...<paramref name="d6"/>.</returns>
        /// <remarks>
        /// <para>This method considers the given dimensional range specifiers to extract a subarray from this array. 
        /// Both indexing styles: <see cref="ArrayStyles.numpy"/> and Matlab® indexing (<see cref="ArrayStyles.ILNumericsV4"/>)
        /// are supported. For such indexing features specific to one or the other style the current setting of 
        /// <see cref="Settings.ArrayStyle"/> is considered. For example, in <see cref="ArrayStyles.ILNumericsV4"/> 
        /// array style (default) the last given dimension specifier may refer to elements inside <i>merged, trailing, 
        /// unspecified</i> dimensions of this array (and not only inside this dimension) in the same manner as the 'linear indexing' 
        /// feature known from Matlab®, Octave, <see cref="ArrayStyles.ILNumericsV4">ILNumerics version 4</see> et al.</para>
        /// <para>For simple dimension specifiers <paramref name="d0"/>,... the subarray creation is often optimized 
        /// to be fast and efficient, often without requiring new memory for copying elements. Note, that the array 
        /// returned may use the same element storage as this array in this case. Only when trying to modify either 
        /// one of this array or the subarray a copy will be made. More complex subarray specifications may require 
        /// iterating and copying addressed elements into new memory. </para>
        /// <para>The setting <see cref="Settings.MinNumberOfArrayDimensions"/> (as controlled by <see cref="Settings.ArrayStyle"/>) 
        /// is respected for the array returned.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if the range provided in a specifier <paramref name="d0"/>,... 
        /// addresses a non existing element.</exception>
        /// <exception cref="NotSupportedException"> if any of <paramref name="d0"/>,... attempts to use an indexing feature which 
        /// is not supported in the current <see cref="Settings.ArrayStyle"/> mode. For example, if <paramref name="d0"/>
        /// was provided as <see cref="Globals.newaxis"/> while being in <see cref="ArrayStyles.ILNumericsV4"/> mode.</exception>
        /// <seealso cref="Subarray(BaseArray,BaseArray)"/>
        /// <seealso cref="Subarray(DimSpec[])"/>
        /// <seealso cref="Subarray(DimSpec, DimSpec, DimSpec)"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, long, long)"/>
        /// <seealso cref="GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="GetItem(long)"/>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        public unsafe RetT Subarray(BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3, BaseArray d4, BaseArray d5, BaseArray d6) {

            using var _1 = ReaderLock.Create(this, out var storage);
            var ret = (Settings.ArrayStyle == ArrayStyles.ILNumericsV4 ?
                storage.GetRange_ML(d0, d1, d2, d3, d4, d5, d6, false) :
                storage.GetRange_np(d0, d1, d2, d3, d4, d5, d6)).RetArray;
            return ret;
        }
        /// <summary>
        /// Extract part of this array. Matlab® and numpy indexing.
        /// </summary>
        /// <param name="dims">Range specification for arbitrary dimensions as vector of <see cref="BaseArray"/>.</param>
        /// <returns>New array with parts of this array as specified by <paramref name="dims"/>.</returns>
        /// <remarks>
        /// <para>This method considers the given dimensional range specifiers to extract a subarray from this array. 
        /// Both indexing styles: <see cref="ArrayStyles.numpy"/> and Matlab® indexing (<see cref="ArrayStyles.ILNumericsV4"/>)
        /// are supported. For such indexing features specific to one or the other style the current setting of 
        /// <see cref="Settings.ArrayStyle"/> is considered. For example, in <see cref="ArrayStyles.ILNumericsV4"/> 
        /// array style (default) the last given dimension specifier may refer to elements inside <i>merged, trailing, 
        /// unspecified</i> dimensions of this array (and not only inside this dimension) in the same manner as the 'linear indexing' 
        /// feature known from Matlab®, Octave, <see cref="ArrayStyles.ILNumericsV4">ILNumerics version 4</see> et al.</para>
        /// <para>For simple dimension specifiers <paramref name="dims"/> the subarray creation is often optimized 
        /// to be fast and efficient, often without requiring new memory for copying elements. Note, that the array 
        /// returned may use the same element storage as this array in this case. Only when trying to modify either 
        /// one of this array or the subarray a copy will be made. More complex subarray specifications may require 
        /// iterating and copying addressed elements into new memory. </para>
        /// <para>The setting <see cref="Settings.MinNumberOfArrayDimensions"/> (as controlled by <see cref="Settings.ArrayStyle"/>) 
        /// is respected for the array returned.</para>
        /// <para>This overload of Subarray() allows to define range specifiers for individual dimensions as a system array. 
        /// It is convenient when subarrays are to be created without knowing the number of dimensions and the concrete
        /// range parameters in advance. It is not recommended for situations where high performance is crucial. Note, that 
        /// individual <see cref="BaseArray"/> objects for each dimension addressed in <paramref name="dims"/> can be used in 
        /// exactly one call to <see cref="Subarray(BaseArray[])"/> and are released in the function automatically. Thus, when
        /// calling <see cref="Subarray(BaseArray[])"/> in loops make sure to recreate <i>all</i> dimension specifier prior to
        /// every call to <see cref="Subarray(BaseArray[])"/>.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if the range provided in a specifier <paramref name="dims"/> addresses 
        /// a non existing element.</exception>
        /// <exception cref="NotSupportedException"> if any of <paramref name="dims"/> attempts to use an indexing feature which 
        /// is not supported in the current <see cref="Settings.ArrayStyle"/> mode. For example, if <paramref name="dims"/>[0]
        /// was provided as <see cref="Globals.newaxis"/> while being in <see cref="ArrayStyles.ILNumericsV4"/> mode.</exception>
        /// <seealso cref="Subarray(BaseArray,BaseArray)"/>
        /// <seealso cref="Subarray(DimSpec[])"/>
        /// <seealso cref="Subarray(DimSpec, DimSpec, DimSpec)"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Immutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[long]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[DimSpec]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.this[BaseArray]"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, long, long)"/>
        /// <seealso cref="GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="GetItem(long)"/>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        public RetT Subarray(params BaseArray[] dims) {

            using var _1 = ReaderLock.Create(this, out var storage);
            var ret = (Settings.ArrayStyle == ArrayStyles.ILNumericsV4 ?
                storage.GetRange_ML(dims, false) :
                storage.GetRange_np(dims)).RetArray;
            return ret;
        }
        #endregion

    }
}
