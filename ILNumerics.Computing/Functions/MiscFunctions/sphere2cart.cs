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
