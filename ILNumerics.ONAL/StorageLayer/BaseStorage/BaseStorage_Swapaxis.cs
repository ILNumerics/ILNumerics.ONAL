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
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.StorageLayer {

    public abstract partial class BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> : IDisposable, IStorage   // T1 must not read "T" to prevent from conflicts with the .T (transpose) property.
        where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

        /// <summary>
        /// Swaps 2 axes. 
        /// </summary>
        /// <param name="axis1">The first axis.</param>
        /// <param name="axis2">the second axis.</param>
        /// <param name="inplace">true: performs the swapping on this instance. False: create, modify and return a new instance.</param>
        /// <returns>A new storage having the axes 1 and 2 swapped.</returns>
        /// <remarks>A new storage is created as clone of this storage and the axes are swapped on the new storage. When <paramref name="inplace"/> 
        /// is true the new storage is assigned to this instance, otherwise this instance is not altered.</remarks>
        
        internal unsafe StorageT Swapaxis(uint axis1, uint axis2, bool inplace) {

            if (S.NumberOfDimensions == 0) {
                throw new ArgumentException("Unable to swap non-existing dimensions.");
            }
            if (axis1 >= Size.NumberOfDimensions || axis2 >= Size.NumberOfDimensions) {
                throw new ArgumentException($"The axis1 and axis2 arguments for this array must lay in the range [0...{Size.NumberOfDimensions - 1}]. Provided: axis1={axis1}, axis2={axis2}.");
            }
            if (axis1 == axis2) {
                var r = Create(Handles, S);
                if (inplace) {
                    Assign(r, true);
                }
                return r; 
            }

            if (inplace) {
                // thread safe: 
                var swapped = Swapaxis(axis1, axis2, false);  // creates a new storage + performs swap on it (see below)
                Assign(swapped, true);  // utilizes a lock on this array's synch object
                return swapped; 
            }

            var ret = Create(Handles, S);
            var bsd = ret.Size.GetBSD(true);
            System.Diagnostics.Debug.Assert(ret.ReferenceCount == 0 || ReferenceEquals(ret, this));
            System.Diagnostics.Debug.Assert(axis1 <= bsd[0]);
            System.Diagnostics.Debug.Assert(axis2 <= bsd[0]);
            // swap axes in bsd only
            long tmp = bsd[3 + axis1];
            bsd[3 + axis1] = bsd[3 + axis2];
            bsd[3 + axis2] = tmp;
            tmp = bsd[3 + bsd[0] + axis1];
            bsd[3 + bsd[0] + axis1] = bsd[3 + bsd[0] + axis2];
            bsd[3 + bsd[0] + axis2] = tmp;
            return ret as StorageT; 
        }
    }
}
