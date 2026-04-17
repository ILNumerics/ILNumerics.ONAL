using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal {


        /// <summary>
        /// Transforms cartesian coordinates into spherical coordinates.
        /// </summary>
        /// <param name="X">X coordinate.</param>
        /// <param name="Y">Y coordinate.</param>
        /// <param name="Z">Z coordinate.</param>
        /// <param name="Theta">[Optional] Output: polar angle. Default: (null) do not compute.</param>
        /// <param name="Phi">[Optional] Output: Azimuthal angle. Default: (null) do not compute.</param>
        /// <returns>Array with the radius values.<paramref name="Theta"/> and <paramref name="Phi"/> are returned on request 
        /// (i.e.: if not null on entry). </returns>
        /// <remarks> The input parameters <paramref name="X"/>, <paramref name="Y"/> and <paramref name="Z"/>
        /// must be of the same size or broadcasting compatible. All arrays returned are of the broadcasted size </remarks>
        internal static Array<double> cart2sphere(InArray<double> X, InArray<double> Y, InArray<double> Z, 
                                                    OutArray<double> Theta = null, OutArray<double> Phi = null) {
            using (Scope.Enter()) {
                Array<double> X_ = X, Y_ = Y, Z_ = Z;
                Array<double> r = sqrt(X_ * X_ + Y_ * Y_ + Z_ * Z_);
                if (!object.Equals(Theta, null)) {
                    // add exception: r = 0
                    r[r == 0] = Globals.eps;
                    lock (Theta.SynchObj) {
                        Theta.a = acos(Z_ / r);
                    }
                }
                if (!object.Equals(Phi, null)) {
                    Array<double> Xc = X_.C;
                    Xc[X_ == 0] = Globals.eps;
                    lock (Phi.SynchObj) {
                        Phi.a = atan(Y_ / Xc);
                    }
                }
                return r;
            }
        }

        /// <summary>
        /// Transforms cartesian coordinates into spherical coordinates.
        /// </summary>
        /// <param name="X">X coordinate.</param>
        /// <param name="Y">Y coordinate.</param>
        /// <param name="Z">Z coordinate.</param>
        /// <param name="Theta">[Optional] Output: polar angle. Default: (null) do not compute.</param>
        /// <param name="Phi">[Optional] Output: Azimuthal angle. Default: (null) do not compute.</param>
        /// <returns>Array with the radius values.<paramref name="Theta"/> and <paramref name="Phi"/> are returned on request 
        /// (i.e.: if not null on entry). </returns>
        /// <remarks> The input parameters <paramref name="X"/>, <paramref name="Y"/> and <paramref name="Z"/>
        /// must be of the same size or broadcasting compatible. All arrays returned are of the broadcasted size </remarks>
        internal static Array<float> cart2sphere(InArray<float> X, InArray<float> Y, InArray<float> Z, 
                                                OutArray<float> Theta = null, OutArray<float> Phi = null) {
            using (Scope.Enter()) {
                Array<float> X_ = X, Y_ = Y, Z_ = Z;
                Array<float> r = sqrt(X_ * X_ + Y_ * Y_ + Z_ * Z_);
                if (!object.Equals(Theta, null)) {
                    // add exception: r = 0
                    r[r == 0] = Globals.epsf;
                    lock (Theta.SynchObj) {
                        Theta.a = acos(Z_ / r);
                    }
                }
                if (!object.Equals(Phi, null)) {
                    Array<float> Xc = X_.C;
                    Xc[X_ == 0] = Globals.epsf;
                    lock (Phi.SynchObj) {
                        Phi.a = atan(Y_ / Xc);
                    }
                }
                return r;
            }
        }






    }
}
