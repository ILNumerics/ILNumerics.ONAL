using System;
using System.Collections.Generic;
using System.Text;
using ILNumerics;
using static ILNumerics.Globals;
using static ILNumerics.Core.Functions.Builtin.MathInternal;


namespace ILNumerics {
    /// <summary>
    /// A helper class that can be used to generate various simple but non-trivial test data sets.
    /// </summary>
    public static class SpecialData {
        /// <summary>
        /// Get example terrain data, 401 x 401 short matrix with heights in meters
        /// </summary>
        public static Array<short> terrain {
            get {
                using (var s = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ILNumerics.Core.Resources.terrain.bin")) {
                    Array<short> ret = loadBinary<short>(s,401,401,401); 
                    return ret; 
                }
            }
        }

        /// <summary>
        /// Generate sinc function in 2D, useful for plotting examples
        /// </summary>
        /// <param name="rows">Number of rows</param>
        /// <param name="cols">Number of columns</param>
        /// <param name="periods">Influences the number of periods to be drawn in both directions. 1 will result in 4 zero crossings, higher values result in more, lower values in less zero crossings.</param>
        /// <returns>Matrix with sinc data in 2 dimensions</returns>
        public static Array<double> sinc (int rows, int cols, float periods) {
            using (Scope.Enter()) {
                Array<double> X = repmat(arange<double,double>(-cols,2.0,cols-1).T,rows,1) / cols*pi*2*periods;  
                Array<double> Y = repmat(arange<double,double>(-rows,2.0,rows-1),1,cols) / rows*pi*2*periods; 
                Array<double> ret = sqrt(X * X + Y * Y);
                ret[ret == 0.0] = eps;
                ret.a = sin(ret)/ret; 
                return ret; 
            }
        }
        /// <summary>
        /// Generate sinc function in 2D, useful for plotting examples
        /// </summary>
        /// <param name="rows">Number of rows</param>
        /// <param name="cols">Number of columns</param>
        /// <returns>Matrix with sinc data in 2 dimensions</returns>
        /// <remarks>The function generates 4 zero crossings in each direction</remarks>
        public static Array<double> sinc(int rows, int cols) {
            return sinc(rows, cols, 1.0f);
        }
        /// <summary>
        /// Generate sinc function in 2D, single precision, useful for plotting examples
        /// </summary>
        /// <param name="rows">Number of rows</param>
        /// <param name="cols">Number of columns</param>
        /// <returns>Matrix with sinc data in 2 dimensions</returns>
        /// <remarks>The function generates 4 zero crossings in each direction</remarks>
        public static Array<float> sincf(int rows, int cols) {
            return tosingle(sinc(rows, cols, 1.0f));
        }
        /// <summary>
        /// Generate sinc function in 2D, useful for plotting examples
        /// </summary>
        /// <param name="rows">Number of rows</param>
        /// <param name="cols">Number of columns</param>
        /// <param name="periods">Influences the number of periods to be drawn in both directions. 1 will result in 4 zero crossings, higher values result in more, lower values in less zero crossings.</param>
        /// <returns>Matrix with sinc data in 2 dimensions</returns>
        public static Array<float> sincf(int rows, int cols, float periods) {
            return tosingle(sinc(rows,cols,periods)); 
        }

        /// <summary>
        /// Create specified periods of sine and cosine data
        /// </summary>
        /// <param name="numSamples">Number of samples</param>
        /// <param name="periods">Number of (full) periods to be generated, must be &gt; 0</param>
        /// <returns>Matrix with sine data in first column, cosine data in second column</returns>
        public static Array<double> sincos1D(int numSamples, double periods) {
            using (Scope.Enter()) {
                Array<double> t = linspace<double>(0.0, 2 * pi * periods, numSamples);
                return horzcat(sin(t).T, cos(t).T);
            }
        }
        /// <summary>
        /// Create specified periods of sine and cosine data, single precision
        /// </summary>
        /// <param name="numSamples">Number of samples</param>
        /// <param name="periods">Number of (full) periods to be generated, must be &gt; 0</param>
        /// <returns>Matrix with sine data in first column, cosine data in second column</returns>
        public static Array<float> sincos1Df(int numSamples, double periods) {
            using (Scope.Enter()) {
                Array<float> t = linspace<float>(0, 2f * pif * (float)periods, numSamples);
                return horzcat(sin(t).T, cos(t).T);
            }
        }
        /// <summary>
        /// Create demo data for surface plots with the appearance of a waterfall.
        /// </summary>
        /// <param name="rows">Number of rows</param>
        /// <param name="cols">Number of columns</param>
        /// <returns>Matrix with data showing a waterfall terrain. </returns>
        public static Array<double> waterfall(int rows, int cols) {
            using (Scope.Enter()) {
                Array<double> a = rand(rows, cols);
                Array<double> bord = rand(1, cols) * 3 + (rows / 2);
                for (int c = 0; c < cols; c++) {
                    int b = (int)(bord[c] - sin(c / cols * pi) * cols / 5);
                    a[arange(0.0, b), c] = a[arange(0.0, b), c] + 2.0;
                }
                return a;
            }
        }

        /// <summary>
        /// Create surface data of a sphere
        /// </summary>
        /// <param name="n">Number of facettes per angle</param>
        /// <param name="X">[Output] X coords</param>
        /// <param name="Y">[Output] Y coords</param>
        /// <param name="Z">[Output] Z coords</param>
        public static void sphere(int n, OutArray<double> X, OutArray<double> Y, OutArray<double> Z) {
            using (Scope.Enter()) {
                Array<double> phi = repmat(linspace(-pi, pi, n).T, 1, n);
                Array<double> rho = repmat(linspace(0, pi, n), n, 1);
                Y.a = sin(phi) * sin(rho);
                X.a = cos(phi) * sin(rho);
                Z.a = cos(rho);
            }
        }
        /// <summary>
        /// Create surface data for a Möbius strip 
        /// </summary>
        /// <param name="n">Granularity (number of facettes)</param>
        /// <param name="w">Width</param>
        /// <param name="R">Radius</param>
        /// <param name="X">[Output] X coords</param>
        /// <param name="Y">[Output] Y coords</param>
        /// <param name="Z">[Output] Z coords</param>
        /// <remarks>Möbius strip is a surfcae, crated by cutting a regular strip, twisting one end by 180 deg and glueing 
        /// both ends together again.</remarks>
        public static void moebius(int n, double w, double R, OutArray<double> X, OutArray<double> Y, OutArray<double> Z) {
            using (Scope.Enter()) {
                Array<double> s = repmat(linspace(-w, w, n), n, 1);
                Array<double> t = repmat(linspace(0, 2 * pi, n).T, 1, n);
                X.a = (R + s * cos(0.5 * t)) * cos(t);
                Y.a = (R + s * cos(0.5 * t)) * sin(t);
                Z.a = s * sin(0.5 * t);
            }
        }
        /// <summary>
        /// Create torus cartesian coordinates, to be used for surface plotting
        /// </summary>
        /// <param name="outerRadius">[optional] the outer radius of the torus ring, default: 0.75</param>
        /// <param name="innerRadius">[optional] the inner radius of the torus ring, default: 0.25</param>
        /// <param name="stepsPoloidal">[optional] number of grid points in poloidal direction, default: 100</param>
        /// <param name="stepsToroidal">[optional] number of grid points in toroidal direction, default: 100</param>
        /// <returns>Data array with cartesian coordinates of the torus gris points. </returns>
        /// <remarks></remarks>
        public static Array<float> torus(float outerRadius = 0.75f, float innerRadius = 0.25f, int stepsPoloidal = 100, int stepsToroidal = 100) {
            using (Scope.Enter()) {
                Array<float> ret = zeros<float>(stepsPoloidal, stepsToroidal, 2); 
                Array<float> theta = linspace<float>(-pif, pif, stepsPoloidal);
                Array<float> phi = linspace<float>(0, 2f * pif, stepsToroidal);
                Array<float> p = 1;
                Array<float> t = meshgrid(phi, theta, p); 
                ret[full,full,1] = (outerRadius + innerRadius * cos(p)) * cos(t);
                ret[full,full,2] = (outerRadius + innerRadius * cos(p)) * sin(t);
                ret[full,full,0] = innerRadius * sin(p);
                return ret; 
            }
        }
    }
}
