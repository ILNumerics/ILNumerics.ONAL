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
using System;
using System.Collections.Generic;
using System.Text;
using ILNumerics;
using static ILNumerics.Globals;
using System.Security;
using ILNumerics.Core.StorageLayer;
using ILNumerics.Core.Arrays;
using System.Runtime.InteropServices;
using ILNumerics.Core.Global;

namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal {

        /// <summary>
        /// Reinterpret the elements of A as elements of type <typeparamref name="Tout" />. Expand or shrink the array size accordingly.
        /// </summary>
        /// <typeparam name="Tin">Element type of A.</typeparam>
        /// <typeparam name="Tout">Element type of result.</typeparam>
        /// <param name="A">Input array. Not altered.</param>
        /// <returns>A new array with the element type <typeparamref name="Tout" />, referencing the same memory as A.</returns>
        /// <remarks>The cast is performed regardless of the way the individual elements of A are currently stored in memory. When using this method 
        /// one must consider the endianness of the system, which can vary from machine to machine! 
        /// <para>If <typeparamref name="Tin"/> is of equal size as <typeparamref name="Tout"/> the array returned will have the same size and striding as A.</para>
        /// <para>If the size of <typeparamref name="Tin"/> is smaller than the size of <typeparamref name="Tout"/> multiple elements of A are collapsed into a corresponding output element. Both sizes must match so that multiple element instances of <typeparamref name="Tin"/> fit exactly into a single <typeparamref name="Tout"/> element. The dimension where elements in A are stored continously in memory is shrinked by the fraction of the element size difference.</para>
        /// <para>Otherwise, if the input elements size is larger than the size of <typeparamref name="Tout"/>a new trailing dimension is added, containing the newly extracted component from the larger input elements. In this case, the array returned has the same size than A, padded with a trailing dimension and the new dimension's length equals the number of <typeparamref name="Tout"/> fitting exactly into <typeparamref name="Tin"/>.</para>
        /// </remarks>
        /// <exception cref="T:System.ArgumentException">if the sizes of the types requested do not match or if the storage layout of A is not appropriate.</exception>
        
        internal unsafe static Array<Tout> reinterpret_cast<Tin,Tout>(ConcreteArray<Tin, Array<Tin>, InArray<Tin>, OutArray<Tin>, Array<Tin>,Storage<Tin>> A) 
            where Tin: unmanaged where Tout : unmanaged {
            // TODO: ReaderLock 
            using var _1 = ReaderLock.Create(A, out var storageA); 

            var inSize = Marshal.SizeOf(typeof(Tin));
            var outSize = Marshal.SizeOf(typeof(Tout));

            void checkElementTypeSizesMatch(double smallSize, int largeSize) {
                var frac = largeSize / smallSize;
                if (frac - Math.Round(frac) != 0) {
                    throw new ArgumentException("The sizes of both types must be a multiple of each other. Check input / output types!");
                }
            }

            var absd = storageA.S.GetBSD();
            if (outSize > inSize) {

                checkElementTypeSizesMatch(inSize, outSize); 
                // must have one dime with continous storage of appropriate length. It must have a matching size and will be collapsed accordingly. 
                int comprDim = -1;
                int i = 0; 
                for (i = 0; i < absd[0]; i++) {
                    if (absd[3 + absd[0] + i] == 1) {
                        comprDim = i;
                        break; 
                    }
                }
                if (comprDim < 0) {
                    throw new ArgumentException($"To reinterprete an array of element type '{typeof(Tin).Name}' as element type '{typeof(Tout).Name}' one dimension of the array must be continously layed out. No continous dimension was found in A.");
                }
                var fracI = outSize / inSize; 
                if (absd[3 + comprDim] % fracI != 0) {
                    throw new ArgumentException($"The number of elements in A's dimension #{comprDim} must be a multiple of {fracI} (fraction of element type sizes). Length found: {absd[3 + comprDim]}"); 
                }
                var ret = Storage<Tout>.Create(storageA.m_handles);
                var bbsd = ret.S.GetBSD(write: true);

                bbsd[0] = absd[0];
                
                bbsd[0] = absd[0];
                bbsd[1] = absd[1] / fracI;
                bbsd[2] = absd[2] / fracI; 
                for (i = 0; i < absd[0]; i++) {
                    if (i == comprDim) {
                        var len = absd[3 + i] / fracI; 
                        bbsd[3 + i] = len;
                        bbsd[3 + i + bbsd[0]] = len == 1 ? 0 : 1; 
                    } else {
                        bbsd[3 + i] = absd[3 + i]; 
                        bbsd[3 + i + bbsd[0]] = (absd[3 + i] == 1 ? 0 : absd[3 + i + absd[0]] / fracI); 
                    }
                }
                return ret.RetArray; 

            } else if (outSize == inSize) {
                var ret = Storage<Tout>.Create(storageA.m_handles);
                ret.S.SetAll(storageA.S);
                return ret.RetArray; 

            } else {
                //outSize < inSize: expand along new trailing dimension
                if (storageA.S.NumberOfDimensions >= Size.MaxNumberOfDimensions) {
                    throw new ArgumentException("A has too many dimensions (" + Size.MaxNumberOfDimensions + ")."); 
                }
                checkElementTypeSizesMatch(outSize, inSize);

                var ret = Storage<Tout>.Create(storageA.m_handles);
                var bbsd = ret.S.GetBSD(write: true); 
                var fracI = inSize / outSize;

                bbsd[0] = absd[0] + 1;
                bbsd[1] = absd[1] * fracI;
                bbsd[2] = absd[2] * fracI;

                int i = 0; 
                for (; i < absd[0]; i++) {
                    bbsd[3 + i] = absd[3 + i]; 
                    bbsd[3 + i + bbsd[0]] = absd[3 + i + absd[0]] * fracI;  // fracI always > 0: no need for singleton dim striding checks!
                }
                bbsd[3 + i] = fracI; bbsd[3 + i + bbsd[0]] = 1; 
                return ret.RetArray;

            }
        }
    }
}
