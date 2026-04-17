using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal {

        /// <summary>
        /// Transforms polar/ cylindrical coordinates into scalar coordinates.
        /// </summary>
        /// <param name="theta">Angle values to x axis.</param>
        /// <param name="radius">Radius values from z axis.</param>
        /// <param name="Z">Height coordinates.</param>
        /// <param name="Y">[Optional] Output: If not null on entry the Y components are returned here.</param>
        /// <param name="outZ">[Optional] Output: If not null on entry, the Z components are returned here.</param>
        /// <returns>X component values. <paramref name="Y"/> and <paramref name="Z"/> are returned as output parameter if requested.</returns>
        /// <remarks><paramref name="theta"/>, <paramref name="radius"/> and <paramref name="Z"/> must be of 
        /// the same size or broadcastable to each other. Output arrays returned are of the same size then the broadcasted 
        /// size of the input arrays.
        /// <para><paramref name="outZ"/> corresponds to <paramref name="Z"/> but has been broadcasted to the output size.</para>
        /// </remarks>
        internal static Array<double> pol2cart(InArray<double> theta, InArray<double> radius, InArray<double> Z
                                                   , OutArray<double> Y = null, OutArray<double> outZ = null) {
            using (Scope.Enter()) {
                Array<double> _theta = theta, _radius = radius, _Z = Z; 
                if (!object.Equals(Y, null)) {
                    lock (Y.SynchObj)
                        Y.a = _radius * sin(_theta);
                }
                if (!object.Equals(_Z, null)) {
                    lock (outZ.SynchObj)
                        if (!_theta.S.IsSameShape(_Z.S) || !_radius.S.IsSameShape(_Z.S)) {
                            outZ.a = _Z + zeros<double>(_theta.S) + zeros<double>(_radius.S);
                        } else {
                            outZ.a = _Z;
                        }
                }
                return _radius * cos(_theta);
            }
        }

        /// <summary>
        /// Transforms polar/ cylindrical coordinates into scalar coordinates.
        /// </summary>
        /// <param name="theta">Angle values to x axis.</param>
        /// <param name="radius">Radius values from z axis.</param>
        /// <param name="Z">Height coordinates.</param>
        /// <param name="Y">[Optional] Output: If not null on entry the Y components are returned here.</param>
        /// <param name="outZ">[Optional] Output: If not null on entry, the Z components are returned here.</param>
        /// <returns>X component values. <paramref name="Y"/> and <paramref name="Z"/> are returned as output parameter if requested.</returns>
        /// <remarks><paramref name="theta"/>, <paramref name="radius"/> and <paramref name="Z"/> must be of 
        /// the same size or broadcastable to each other. Output arrays returned are of the same size then the broadcasted 
        /// size of the input arrays.
        /// <para><paramref name="outZ"/> corresponds to <paramref name="Z"/> but has been broadcasted to the output size.</para>
        /// </remarks>
        internal static Array<float> pol2cart(InArray<float> theta, InArray<float> radius, InArray<float> Z
                                                   , OutArray<float> Y = null, OutArray<float> outZ = null) {
            using (Scope.Enter()) {
                Array<float> _theta = theta, _radius = radius, _Z = Z;
                if (!object.Equals(Y, null)) {
                    lock (Y.SynchObj)
                        Y.a = _radius * sin(_theta);
                }
                if (!object.Equals(_Z, null)) {
                    lock (outZ.SynchObj)
                        if (!_theta.S.IsSameShape(_Z.S) || !_radius.S.IsSameShape(_Z.S)) {
                            outZ.a = _Z + zeros<float>(_theta.S) + zeros<float>(_radius.S);
                        } else {
                            outZ.a = _Z;
                        }
                }
                return _radius * cos(_theta);
            }
        }


    }
}
