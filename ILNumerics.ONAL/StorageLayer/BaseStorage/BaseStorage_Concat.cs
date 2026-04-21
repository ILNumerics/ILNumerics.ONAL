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
using System;
using System.Security;

namespace ILNumerics.Core.StorageLayer {

    public abstract partial class BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> : IDisposable, IStorage // T1 must not read "T" to prevent from conflicts with the .T (transpose) property.
        where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

        /// <summary>
        /// Create new storage with the elements of this storage and the <paramref name="other"/> storage concatenated along dimension <paramref name="dim"/>.
        /// </summary>
        /// <param name="other">The other storage. </param>
        /// <param name="dim">0-based dimension index to align both storages along.</param>
        /// <returns>New storage with elements of this and of the <paramref name="other"/> storage.</returns>
        /// <remarks><para>The size of both storages must match. This means that all but the dimension <paramref name="dim"/>
        /// must have the same lengths.</para></remarks>
        /// <exception cref="ArgumentException">If <paramref name="dim"/> is not within the range of valid dimensions of any of both storages.</exception>
        
        internal unsafe virtual StorageT Concat(StorageT other, uint dim) {
            System.Diagnostics.Debug.Assert(!Equals(other, null));

            if (Size.NumberOfDimensions != other.S.NumberOfDimensions) {
                throw new ArgumentException($"Both storages to be concatenated must have the same rank. This size={Size}. Other size={other.S}."); 
            }
            var ndims = other.S.NumberOfDimensions;
            if (dim >= ndims) {
                throw new ArgumentException($"Argument dim must address a non-virtual dimension of this or of the other storage.");
            }
            for (uint i = 0; i < ndims; i++) {
                if (i != dim && Size[i] != other.S[i]) {
                    throw new ArgumentException($"Both storages to be concatenated must have the same shape, except for the concatenation " +
                        $"dimension (#{dim}). Check dimension #{i}! This size={Size}. Other size={other.S}.");
                }
            }

            var ret = Create();
            ret.Handles[0] = DeviceManager.GetDevice(0).New<T>((ulong)(Size.NumberOfElements + other.Size.NumberOfElements));
            var outBSD = ret.S.GetBSD(true);

            var dimspecs = Context.DimSpecArray;
            #region prepare out storage
            // for now: column major storage only
            long stride = 1;
            for (uint i = 0; i < ndims; i++) {
                if (i != dim) {
                    outBSD[3 + i] = Size[i];
                    dimspecs[i] = Globals.full; 
                } else {
                    outBSD[3 + i] = Size[i] + other.S[i];
                    dimspecs[i] = Globals.r(0, Size[i] - 1);
                }
                outBSD[3 + ndims + i] = outBSD[3 + i] == 1 ? 0 : stride;
                stride *= outBSD[3 + i]; 
            }
            outBSD[0] = ndims;
            outBSD[1] = stride; 
            outBSD[2] = 0;
            #endregion
            if (Size.NumberOfElements > 0) {
                var tmp = ret.SetRange_ML(this as StorageT, dimspecs, ndims);
                System.Diagnostics.Debug.Assert(object.ReferenceEquals(tmp, ret) || other.Size.NumberOfElements == 0, "Unexpected out of-place SetRange_ML()! 'ret' was pre-allocated large enough to hold all values. Should not expand nor detach!?");
                if (!object.ReferenceEquals(tmp, ret)) {
                    if (ret.ReferenceCount == 0) {
                        ret.Retain(); ret.Release();
                    }
                    ret = tmp;
                }
            }

            if (other.S.NumberOfElements > 0) {
                // rebuild dimspecs array
                for (uint i = 0; i < ndims; i++) {
                    if (i != dim) {
                        dimspecs[i] = Globals.full;
                    } else {
                        dimspecs[i] = Globals.slice(Size[dim], Size[dim] + other.S[dim]);
                    }
                }
                var tmp = ret.SetRange_ML(other as StorageT, dimspecs, ndims);
                System.Diagnostics.Debug.Assert(object.ReferenceEquals(tmp, ret) || Size.NumberOfElements == 0, "Unexpected out of-place SetRange_ML()! 'ret' was pre-allocated large enough to hold all values. Should not expand nor detach!?");
                if (!object.ReferenceEquals(tmp, ret)) {
                    if (ret.ReferenceCount == 0) {
                        ret.Retain(); ret.Release();
                    }
                    ret = tmp;
                }
            }
            //handle Logicals
            (ret as LogicalStorage)?.SetNumberTrues((this as LogicalStorage).NumberTrues + (other as LogicalStorage).NumberTrues);
            return ret; 
        }
    }
}
