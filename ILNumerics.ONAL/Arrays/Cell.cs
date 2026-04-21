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
using ILNumerics.Core.Arrays;
using ILNumerics.Core.DeviceManagement;
using ILNumerics.Core.Functions.Builtin;
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics {
    /// <summary>
    /// Cell is a multidimensional array container for array elements.
    /// </summary>
    public sealed class Cell

        : Mutable<BaseArray, Cell, InCell, OutCell, Cell, CellStorage>, IDisposable {

        #region construction
        internal Cell(
            CellStorage storage, 
            object synch)
            : base(storage, synch) { }

        #endregion

        #region constructional operators /from RetCell)
        /// <summary>
        /// Convert primitive integer to a scalar cell.
        /// </summary>
        /// <param name="value">Primitive scalar int value</param>
        /// <returns>New scalar temporary cell</returns>

        public static implicit operator Cell(int value) {
            var ret = CellStorage.Create();
            ret.S.SetScalar(0, Settings.MinNumberOfArrayDimensions);
            ret.Handles[0] = DeviceManager.GetDevice(0).New<IStorage>(1, true);
            ret.FromImplicitCast = true;
            ret.SetValueSeq(Storage<int>.Create(value).RetArray, 0);
            return ret.RetArray;
        }
        /// <summary>
        /// Convert primitive integer to a scalar cell
        /// </summary>
        /// <param name="value">Primitive scalar int value</param>
        /// <returns>New scalar temporary cell</returns>

        public static implicit operator Cell(uint value) {
            var ret = CellStorage.Create();
            ret.S.SetScalar(0, Settings.MinNumberOfArrayDimensions);
            ret.Handles[0] = DeviceManager.GetDevice(0).New<IStorage>(1, false);
            ret.FromImplicitCast = true;
            ret.SetValueSeq(Storage<uint>.Create(value).RetArray, 0);
            return ret.RetArray;
        }
        /// <summary>
        /// Convert primitive integer to a scalar cell.
        /// </summary>
        /// <param name="value">Primitive scalar int value</param>
        /// <returns>New scalar temporary cell</returns>

        public static implicit operator Cell(long value) {
            var ret = CellStorage.Create();
            ret.S.SetScalar(0, Settings.MinNumberOfArrayDimensions);
            ret.Handles[0] = DeviceManager.GetDevice(0).New<IStorage>(1, false);
            ret.FromImplicitCast = true;
            ret.SetValueSeq(Storage<long>.Create(value).RetArray, 0);
            return ret.RetArray;
        }
        /// <summary>
        /// Convert primitive integer to a scalar cell.
        /// </summary>
        /// <param name="value">Primitive scalar int value</param>
        /// <returns>New scalar temporary cell</returns>

        public static implicit operator Cell(ulong value) {
            var ret = CellStorage.Create();
            ret.S.SetScalar(0, Settings.MinNumberOfArrayDimensions);
            ret.Handles[0] = DeviceManager.GetDevice(0).New<IStorage>(1, false);
            ret.FromImplicitCast = true;
            ret.SetValueSeq(Storage<ulong>.Create(value).RetArray, 0);
            return ret.RetArray;
        }
        /// <summary>
        /// Convert primitive double to a scalar cell.
        /// </summary>
        /// <param name="value">Primitive scalar double value</param>
        /// <returns>New scalar temporary cell</returns>

        public static implicit operator Cell(double value) {
            var ret = CellStorage.Create();
            ret.S.SetScalar(0, Settings.MinNumberOfArrayDimensions);
            ret.Handles[0] = DeviceManager.GetDevice(0).New<IStorage>(1, false);
            ret.FromImplicitCast = true;
            ret.SetValueSeq(Storage<double>.Create(value).RetArray, 0);
            return ret.RetArray;
        }
        /// <summary>
        /// Convert primitive float to a scalar cell.
        /// </summary>
        /// <param name="value">Primitive scalar float value</param>
        /// <returns>New scalar temporary cell</returns>

        public static implicit operator Cell(float value) {
            var ret = CellStorage.Create();
            ret.S.SetScalar(0, Settings.MinNumberOfArrayDimensions);
            ret.Handles[0] = DeviceManager.GetDevice(0).New<IStorage>(1, false);
            ret.FromImplicitCast = true;
            ret.SetValueSeq(Storage<float>.Create(value).RetArray, 0);
            return ret.RetArray;
        }
        /// <summary>
        /// Convert primitive byte to a scalar cell.
        /// </summary>
        /// <param name="value">Primitive byte int value</param>
        /// <returns>New scalar temporary cell</returns>

        public static implicit operator Cell(byte value) {
            var ret = CellStorage.Create();
            ret.S.SetScalar(0, Settings.MinNumberOfArrayDimensions);
            ret.Handles[0] = DeviceManager.GetDevice(0).New<IStorage>(1, false);
            ret.FromImplicitCast = true;
            ret.SetValueSeq(Storage<byte>.Create(value).RetArray, 0);
            return ret.RetArray;
        }
        /// <summary>
        /// Convert primitive byte to a scalar cell.
        /// </summary>
        /// <param name="value">Primitive byte int value</param>
        /// <returns>New scalar temporary cell</returns>

        public static implicit operator Cell(string value) {
            var ret = CellStorage.Create();
            ret.S.SetScalar(0, Settings.MinNumberOfArrayDimensions);
            ret.Handles[0] = DeviceManager.GetDevice(0).New<IStorage>(1, false);
            ret.FromImplicitCast = true;
            ret.SetValueSeq(Storage<string>.Create(value).RetArray, 0);
            return ret.RetArray;
        }
        /// <summary>
        /// Encapsulates UInt32[] system array into a scalar cell. array. 
        /// </summary>
        /// <param name="value">System array with values for the only cell of the new scalar cell array.</param>
        /// <returns>New scalar temporary cell.</returns>

        public static implicit operator Cell(uint[] value) {
            var ret = CellStorage.Create();
            ret.S.SetScalar(0, Settings.MinNumberOfArrayDimensions);
            ret.Handles[0] = DeviceManager.GetDevice(0).New<IStorage>(1, false);
            ret.FromImplicitCast = true;
            ret.SetValueSeq(MathInternal.vector(value), 0);
            return ret.RetArray;
        }
        /// <summary>
        /// Encapsulates Int64[] system array into a scalar cell. array. 
        /// </summary>
        /// <param name="value">System array with values for the only cell of the new scalar cell array.</param>
        /// <returns>New scalar temporary cell.</returns>

        public static implicit operator Cell(long[] value) {
            var ret = CellStorage.Create();
            ret.S.SetScalar(0, Settings.MinNumberOfArrayDimensions);
            ret.Handles[0] = DeviceManager.GetDevice(0).New<IStorage>(1, false);
            ret.FromImplicitCast = true;
            ret.SetValueSeq(MathInternal.vector(value), 0);
            return ret.RetArray;
        }
        /// <summary>
        /// Encapsulates Int32[] system array into a scalar cell. array. 
        /// </summary>
        /// <param name="value">System array with values for the only cell of the new scalar cell array.</param>
        /// <returns>New scalar temporary cell.</returns>

        public static implicit operator Cell(int[] value) {
            var ret = CellStorage.Create();
            ret.S.SetScalar(0, Settings.MinNumberOfArrayDimensions);
            ret.Handles[0] = DeviceManager.GetDevice(0).New<IStorage>(1, false);
            ret.FromImplicitCast = true;
            ret.SetValueSeq(MathInternal.vector(value), 0);
            return ret.RetArray;
        }
        /// <summary>
        /// Encapsulates Int32[] system array into a scalar cell. array. 
        /// </summary>
        /// <param name="value">System array with values for the only cell of the new scalar cell array.</param>
        /// <returns>New scalar temporary cell.</returns>

        public static implicit operator Cell(double[] value) {
            var ret = CellStorage.Create();
            ret.S.SetScalar(0, Settings.MinNumberOfArrayDimensions);
            ret.Handles[0] = DeviceManager.GetDevice(0).New<IStorage>(1, false);
            ret.FromImplicitCast = true;
            ret.SetValueSeq(MathInternal.vector(value), 0);
            return ret.RetArray;
        }
        /// <summary>
        /// Encapsulate array of strings into a scalar cell.
        /// </summary>
        /// <param name="value">String array - no copy will be made!</param>
        /// <returns>New scalar temporary cell</returns>

        public static implicit operator Cell(string[] value) {
            var ret = CellStorage.Create();
            ret.S.SetScalar(0, Settings.MinNumberOfArrayDimensions);
            ret.Handles[0] = DeviceManager.GetDevice(0).New<IStorage>(1, false);
            ret.FromImplicitCast = true;
            ret.SetValueSeq(MathInternal.vector(value), 0);
            return ret.RetArray;
        }

        #endregion


        #region conversions, cast operators

        #region conversional operators

        /// <summary>
        /// Convert input array (immutable) to local array (mutable).
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <returns>Local array with lifespan corresponding to the current scope.</returns>
        /// <remarks>The new array is created from the source array <paramref name="A"/> and can 
        /// be used multiple times in the function scope and get altered without modifying the source <paramref name="A"/>.
        /// <para>Array conversions in ILNumerics do not require new memory. Elements are not copied unless multiple arrays reference 
        /// the same memory and one of them is about to be changed. Only then a copy of the memory is done and the change is performed 
        /// on the copy (lazy copy on write).</para>
        /// </remarks>
        public static implicit operator Cell(InCell A) {
            if (object.Equals(A, null))
                return null;
            var ret = CellStorage.Create(A.Storage.Handles, A.Storage.Size).GetLocalArray();  // this registers in scope + Retain()
            return ret;
        }

        /// <summary>
        /// Convert output array to a new local array (mutable).
        /// </summary>
        /// <param name="A">Array of OutArray type.</param>
        /// <returns>Local mutable array, detached from the source array <paramref name="A"/>, with lifespan corresponding to the current scope.</returns>
        /// <remarks>The new array is detached from this array. The new array can 
        /// get used multiple times in the function scope and get altered without altering the source array.</remarks>
        public static implicit operator Cell(OutCell A) {
            if (object.Equals(A, null))
                return null;
            var ret = CellStorage.Create(A.Storage.Handles, A.Storage.Size).GetLocalArray();  // this registers in scope + Retain()
            return ret;
        }

        #endregion

        #endregion

        /// <summary>
        /// Release this array after use. Cleans up on <see cref="Array{T1}"/>, <see cref="Cell"/> and <see cref="Logical"/> arrays which are not inside a <see cref="Scope.Enter(BaseArray, ArrayStyles?)"/> block.
        /// </summary>
        /// <remarks><para>This method is used in rare situations where no artificial scope exists around the current function body. Examples of 
        /// such situations are short <see cref="Array{T}"/> snippets in anonymous- or lambda functions, entry methods, or intended manual memory management.</para>
        /// <para>During 'normal' use the user places an artificial scope (<see cref="Scope.Enter(BaseArray, ArrayStyles?)"/>) around the function body. All <see cref="Array{T}"/>, 
        /// <see cref="Cell"/>, and <see cref="Logical"/> arrays created within the scope block are automatically tracked and released once the scope block is left.</para>
        /// <para>Calling this method manually on local arrays as <see cref="Array{T}"/> &amp; Co. can be profitible in situations where no such scope block exists. 
        /// Otherwise, the memory would be reclaimed at a later point in time by the GC. <see cref="Dispose"/> releases the array immediately. If no other 
        /// arrays are sharing the same memory, it will be released to the memory pool for immediate recycling.</para>
        /// <para>Note: failing to call <see cref="Dispose"/> in such situations does not create a memory leak! But the array is only reclaimed 
        /// by the garbage collector and its memory is only freed by the finalization thread during the next GC collection. While this 
        /// is considered regular use, disposing the array manually is recommended in situations where high performance execution or 
        /// low memory consumption is required and no 'common' memory management is possible for some reasons. </para>
        /// <para>Note further, that <see cref="Dispose"/> is <b>not</b> required in 'common' array uses. I.e. when dealing with <see cref="Array{T1}"/> 
        /// inside existing <see cref="Scope.Enter(BaseArray, ArrayStyles?)"/> scope blocks or when a return value is utilized in some way (incl. assignment to <see cref="Array{T1}"/>, 
        /// calling member functions on the return value or giving the return value to other functions as input parameter).</para></remarks>
        /// <seealso cref="ILNumerics.Core.Arrays.ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Dispose()">ILNumerics.Array{T}.Dispose()</seealso>
        public override void Dispose() {
            m_storage.Release();
        }

    }
}
