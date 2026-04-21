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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal { 

        /// <summary>
        /// Transform scalar coordinates into polar (cylindrical) coordinates
        /// </summary>
        /// <param name="X">X coordinates</param>
        /// <param name="Y">Y coordinates</param>
        /// <param name="Z">Z coordinates (height). Can be null if <paramref name="outZ"/> is not requested.</param>
        /// <param name="Radius">[Optional] Output: radius. Default: (null) do not compute.</param>
        /// <param name="outZ">[Optional] Output: Copy of Z. Default: (null) do not return.</param>
        /// <returns>Angles. Radius and Z values are returned as output parameters if requested on entry (i.e.: not null).</returns>
        /// <remarks><paramref name="X"/>, <paramref name="Y"/>, and <paramref name="Z"/> must be the 
        /// same size or broadcastable to each other. Polar coordinate arrays returned are of the same 
        /// size as the broadcasted size of the input arrays.
        /// <para>If <paramref name="outZ"/> is requested it will have the same values as <paramref name="Z"/> and 
        /// the same broadcasted size of the output.</para></remarks>
        internal static Array<double> cart2pol(InArray<double> X, InArray<double> Y, InArray<double> Z, 
                                                OutArray<double> Radius = null, OutArray<double> outZ = null) {
            using (Scope.Enter()) {    
                Array<double> X_ = X, Y_ = Y, Z_ = Z;
                if (!object.Equals(Radius, null))
                    lock (Radius.SynchObj) {
                        Radius.a = sqrt(X_ * X_ + Y_ * Y_);
                    }
                if (!object.Equals(outZ, null) && !object.Equals(Z, null))
                    lock (outZ.SynchObj) {

                        if (!X_.S.IsSameShape(Z.S) || !Y_.S.IsSameShape(Z_.S)) {
                            outZ.a = Z_ + zeros<double>(X_.S) + zeros<double>(Y_.S);
                        } else {
                            outZ.a = Z_;
                        }
                    }
                return atan2(Y_,X_); 
            }
        }

        /// <summary>
        /// Transforms scalar coordinates into polar (cylindrical) coordinates.
        /// </summary>
        /// <param name="X">X coordinates.</param>
        /// <param name="Y">Y coordinates.</param>
        /// <param name="Z">Z coordinates (height).</param>
        /// <param name="Radius">[Optional] Output: radius. Default: (null) do not compute.</param>
        /// <param name="outZ">[Optional] Output: Copy of Z. Default: (null) do not return.</param>
        /// <returns>Angles. Radius and Z values are returned as output parameters if requested on entry (i.e.: not null).</returns>
        /// <remarks><paramref name="X"/>, <paramref name="Y"/>, and <paramref name="Z"/> must be the 
        /// same size or broadcastable to each other. Polar coordinate arrays returned are of the same 
        /// size as the broadcasted size of the input arrays.
        /// <para>If <paramref name="outZ"/> is requested it will have the same values as <paramref name="Z"/> and 
        /// the same broadcasted size of the output.</para></remarks>
        internal static Array<float> cart2pol(InArray<float> X, InArray<float> Y, InArray<float> Z,
                                                    OutArray<float> Radius = null, OutArray<float> outZ = null) {
            using (Scope.Enter()) {
                Array<float> X_ = X, Y_ = Y, Z_ = Z;
                if (!object.Equals(Radius, null))
                    lock (Radius.SynchObj) {
                        Radius.a = sqrt(X_ * X_ + Y_ * Y_);
                    }
                if (!object.Equals(outZ, null) && !object.Equals(Z_, null))
                    lock (outZ.SynchObj) {

                        if (!X_.S.IsSameShape(Z_.S) || !Y_.S.IsSameShape(Z_.S)) {
                            outZ.a = Z_ + zeros<float>(X_.S) + zeros<float>(Y_.S);
                        } else {
                            outZ.a = Z_;
                        }
                    }
                return atan2(Y_, X_);
            }
        }
    }
}
