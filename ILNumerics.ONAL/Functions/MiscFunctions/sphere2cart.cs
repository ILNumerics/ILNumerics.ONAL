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
        ///  Transforms spherical coordinates into cartesian coordinates.
        /// </summary>
        /// <param name="radius">Radial distance.</param>
        /// <param name="theta">Polar angle.</param>
        /// <param name="phi">Azimuthal angle.</param>
        /// <param name="Y">[Optional] Output: Y coordinates. Default: (null) do not compute.</param>
        /// <param name="Z">[Optional] Output: Z coordinates. Default: (null) do not compute.</param>
        /// <returns>X coordinates.<paramref name="Y"/> and <paramref name="Z"/> are returned on request.</returns>
        /// <remarks> The input parameters <paramref name="radius"/>, <paramref name="theta"/> and <paramref name="phi"/>
        /// must be of the same size or be broadcastable to each other. All arrays returned are of the broadcasted size.</remarks>
        internal static Array<double> sphere2cart(InArray<double> radius, InArray<double> theta, InArray<double> phi
                                                   ,OutArray<double> Y = null, OutArray<double> Z = null) {
            using (Scope.Enter()) {
                Array<double> _radius = radius, _theta = theta, _phi = phi; 
                if(!object.Equals(Y, null)) {

                    lock (Y.SynchObj)
                    Y.a = radius * sin(theta) * sin(phi);
                }
                if (!object.Equals(Z, null)) {

                    lock (Z.SynchObj) {
                        Z.a = radius * cos(theta);
                        if (!Z.S.IsSameShape(phi.S)) {
                            Z.a = Z + zeros<double>(phi.S);
                        }
                    }
                }
                return radius * sin(theta) * cos(phi);
            }
        }

        /// <summary>
        ///  Transforms spherical coordinates into cartesian coordinates.
        /// </summary>
        /// <param name="radius">Radial distance.</param>
        /// <param name="theta">Polar angle.</param>
        /// <param name="phi">Azimuthal angle.</param>
        /// <param name="Y">[Optional] Output: Y coordinates. Default: (null) do not compute.</param>
        /// <param name="Z">[Optional] Output: Z coordinates. Default: (null) do not compute.</param>
        /// <returns>X coordinates.<paramref name="Y"/> and <paramref name="Z"/> are returned on request.</returns>
        /// <remarks> The input parameters <paramref name="radius"/>, <paramref name="theta"/> and <paramref name="phi"/>
        /// must be of the same size or be broadcastable to each other. All arrays returned are of the broadcasted size.</remarks>
        internal static Array<float> sphere2cart(InArray<float> radius, InArray<float> theta, InArray<float> phi
                                                   , OutArray<float> Y = null, OutArray<float> Z = null) {
            using (Scope.Enter()) {
                Array<float> _radius = radius, _theta = theta, _phi = phi;
                if (!object.Equals(Y, null)) {

                    lock (Y.SynchObj)
                        Y.a = radius * sin(theta) * sin(phi);
                }
                if (!object.Equals(Z, null)) {

                    lock (Z.SynchObj) {
                        Z.a = radius * cos(theta);
                        if (!Z.S.IsSameShape(phi.S)) {
                            Z.a = Z + zeros<float>(phi.S);
                        }
                    }
                }
                return radius * sin(theta) * cos(phi);
            }

        }

    }
}
