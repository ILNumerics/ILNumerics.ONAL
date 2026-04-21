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
//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;
using ILNumerics;
using static ILNumerics.Globals;

/*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="here">
        double
    </source>
    <destination>complex</destination>
    <destination>float</destination>
    <destination>fcomplex</destination>
</type>
</hycalper>
*/

namespace ILNumerics.Core.Functions.Builtin {
    internal partial class MathInternal {

        #region HYCALPER LOOPSTART
        /// <summary>
        /// Create two matrices for evaluation and visualization of 2 dimensional functions over a 2d grid.
        /// </summary>
        /// <param name="X">Vector of x values.</param>
        /// <param name="Y">Vector of y values.</param>
        /// <param name="outY">[Output, Optional]: if on entry <paramref name="outY"/> is not null it holds the values for the Y dimension on return.</param>
        /// <returns>X values matrix along the values of <paramref name="X"/>. Corresponding values for Y are returned in <paramref name="outY"/>.</returns>
        /// <remarks><para>The matrices returned can conveniently be used to evaluate and visualize functions of 2 variables over a grid of (X Y) positions.</para>
        /// <para>Note that the X dimension goes along the <i>rows</i>, while the second dimension is considered the columns! This deviates from the 
        /// common intuition and the actual storage order of matrix elements (i.e.: first dimension along the columns and 2nd dimension along the rows). 
        /// The reason for it is the convenience this order gives for plotting purposes: In most plots the X axis is expected to run horizontaly.</para></remarks>
        /// <exception cref="ArgumentNullException">If <paramref name="X"/> or <paramref name="Y"/> is null.</exception>
        /// <seealso cref="meshgrid(InArray{double}, InArray{double}, InArray{double}, OutArray{double}, OutArray{double})"/>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Array<double > meshgrid(InArray<double > X, InArray<double > Y
                                                                  , OutArray<double > outY = null)
        {
            using (Scope.Enter()) {
                if (isnull(X) || isnull(Y)) {
                    throw new ArgumentNullException("X and Y must not be null.");
                }
                Array<double> XX = reshape(X, 1, X.S.NumberOfElements);
                Array<double> YY = reshape(Y, Y.S.NumberOfElements, 1);

                Array<double> ret = repmat(XX, size(YY.Length, 1));
                if (!isnull(outY)) lock(outY.SynchObj) outY.a = repmat(YY, size(1, XX.Length));
                return ret;
            }
        }

        /// <summary>
        /// Create 3d arrays for evaluation and visualization of 3 dimensional functions.
        /// </summary>
        /// <param name="X">Vector of x values.</param>
        /// <param name="Y">Vector of y values.</param>
        /// <param name="Z">Vector of z values.</param>
        /// <param name="outY">[Output, Optional] If on entry <paramref name="outY"/> is not null it holds the values for the y dimension on return.</param>
        /// <param name="outZ">[Output, Optional] If on entry <paramref name="outZ"/> is not null it holds the values for the z dimension on return.</param>
        /// <returns>X value array along the values of <paramref name="X"/>, arrays for y and z dimensions are returned in <paramref name="outY"/> and <paramref name="outZ"/> respectively.</returns>
        /// <remarks><para>The arrays returned can conveniently be used to evaluate and visualize functions of 3 variables X, Y and Z. Coordinates build 
        /// a 3 dimensional grid with edges at all permutations of <paramref name="X"/>, <paramref name="Y"/> and <paramref name="Z"/>.</para>
        /// <para>Note that the X dimension goes along the <i>rows</i>, while the second dimension is considered the columns! This deviates from the 
        /// common intuition and the actual storage order of matrix elements (i.e.: first dimension along the columns and 2nd dimension along the rows). 
        /// The reason for it is the convenience this order gives for plotting purposes: In most plots the X axis is expected to run horizontaly.</para></remarks>
        /// <exception cref="ArgumentNullException">If <paramref name="X"/> or <paramref name="Y"/> is null.</exception>
        /// <seealso cref="meshgrid(InArray{double}, InArray{double}, InArray{double}, OutArray{double}, OutArray{double})"/>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Array<double > meshgrid(InArray<double > X, InArray<double > Y, InArray<double > Z
                                                                ,OutArray<double > outY = null, OutArray<double > outZ = null)
        {
            using (Scope.Enter()) {
                if (isnull(X) || isnull(Y) || isnull(Z)) {
                    throw new ArgumentNullException("All of the input parameters X, Y and Z are obligatory and may not be null.");
                }
                Array<double > XX = reshape(X, 1, X.S.NumberOfElements); 
                Array<double > YY = reshape(Y, Y.S.NumberOfElements, 1); 
                Array<double > ZZ = reshape(Z, 1, 1, Z.S.NumberOfElements);
                                              
                Array<double > ret = repmat(XX,size(YY.S.NumberOfElements,1,ZZ.S.NumberOfElements));
                if (!isnull(outY)) lock (outY.SynchObj) outY.a = repmat(YY,size(1,XX.S.NumberOfElements, ZZ.S.NumberOfElements));
                if (!isnull(outZ)) lock (outZ.SynchObj) outZ.a = repmat(ZZ, size(YY.S.NumberOfElements, XX.S.NumberOfElements, 1));
                return ret;
            }
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
        /// <summary>
        /// Create two matrices for evaluation and visualization of 2 dimensional functions over a 2d grid.
        /// </summary>
        /// <param name="X">Vector of x values.</param>
        /// <param name="Y">Vector of y values.</param>
        /// <param name="outY">[Output, Optional]: if on entry <paramref name="outY"/> is not null it holds the values for the Y dimension on return.</param>
        /// <returns>X values matrix along the values of <paramref name="X"/>. Corresponding values for Y are returned in <paramref name="outY"/>.</returns>
        /// <remarks><para>The matrices returned can conveniently be used to evaluate and visualize functions of 2 variables over a grid of (X Y) positions.</para>
        /// <para>Note that the X dimension goes along the <i>rows</i>, while the second dimension is considered the columns! This deviates from the 
        /// common intuition and the actual storage order of matrix elements (i.e.: first dimension along the columns and 2nd dimension along the rows). 
        /// The reason for it is the convenience this order gives for plotting purposes: In most plots the X axis is expected to run horizontaly.</para></remarks>
        /// <exception cref="ArgumentNullException">If <paramref name="X"/> or <paramref name="Y"/> is null.</exception>
        /// <seealso cref="meshgrid(InArray{fcomplex}, InArray{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, OutArray{fcomplex})"/>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Array<fcomplex > meshgrid(InArray<fcomplex > X, InArray<fcomplex > Y
                                                                  , OutArray<fcomplex > outY = null)
        {
            using (Scope.Enter()) {
                if (isnull(X) || isnull(Y)) {
                    throw new ArgumentNullException("X and Y must not be null.");
                }
                Array<fcomplex> XX = reshape(X, 1, X.S.NumberOfElements);
                Array<fcomplex> YY = reshape(Y, Y.S.NumberOfElements, 1);

                Array<fcomplex> ret = repmat(XX, size(YY.Length, 1));
                if (!isnull(outY)) lock(outY.SynchObj) outY.a = repmat(YY, size(1, XX.Length));
                return ret;
            }
        }

        /// <summary>
        /// Create 3d arrays for evaluation and visualization of 3 dimensional functions.
        /// </summary>
        /// <param name="X">Vector of x values.</param>
        /// <param name="Y">Vector of y values.</param>
        /// <param name="Z">Vector of z values.</param>
        /// <param name="outY">[Output, Optional] If on entry <paramref name="outY"/> is not null it holds the values for the y dimension on return.</param>
        /// <param name="outZ">[Output, Optional] If on entry <paramref name="outZ"/> is not null it holds the values for the z dimension on return.</param>
        /// <returns>X value array along the values of <paramref name="X"/>, arrays for y and z dimensions are returned in <paramref name="outY"/> and <paramref name="outZ"/> respectively.</returns>
        /// <remarks><para>The arrays returned can conveniently be used to evaluate and visualize functions of 3 variables X, Y and Z. Coordinates build 
        /// a 3 dimensional grid with edges at all permutations of <paramref name="X"/>, <paramref name="Y"/> and <paramref name="Z"/>.</para>
        /// <para>Note that the X dimension goes along the <i>rows</i>, while the second dimension is considered the columns! This deviates from the 
        /// common intuition and the actual storage order of matrix elements (i.e.: first dimension along the columns and 2nd dimension along the rows). 
        /// The reason for it is the convenience this order gives for plotting purposes: In most plots the X axis is expected to run horizontaly.</para></remarks>
        /// <exception cref="ArgumentNullException">If <paramref name="X"/> or <paramref name="Y"/> is null.</exception>
        /// <seealso cref="meshgrid(InArray{fcomplex}, InArray{fcomplex}, InArray{fcomplex}, OutArray{fcomplex}, OutArray{fcomplex})"/>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Array<fcomplex > meshgrid(InArray<fcomplex > X, InArray<fcomplex > Y, InArray<fcomplex > Z
                                                                ,OutArray<fcomplex > outY = null, OutArray<fcomplex > outZ = null)
        {
            using (Scope.Enter()) {
                if (isnull(X) || isnull(Y) || isnull(Z)) {
                    throw new ArgumentNullException("All of the input parameters X, Y and Z are obligatory and may not be null.");
                }
                Array<fcomplex > XX = reshape(X, 1, X.S.NumberOfElements); 
                Array<fcomplex > YY = reshape(Y, Y.S.NumberOfElements, 1); 
                Array<fcomplex > ZZ = reshape(Z, 1, 1, Z.S.NumberOfElements);
                                              
                Array<fcomplex > ret = repmat(XX,size(YY.S.NumberOfElements,1,ZZ.S.NumberOfElements));
                if (!isnull(outY)) lock (outY.SynchObj) outY.a = repmat(YY,size(1,XX.S.NumberOfElements, ZZ.S.NumberOfElements));
                if (!isnull(outZ)) lock (outZ.SynchObj) outZ.a = repmat(ZZ, size(YY.S.NumberOfElements, XX.S.NumberOfElements, 1));
                return ret;
            }
        }
        /// <summary>
        /// Create two matrices for evaluation and visualization of 2 dimensional functions over a 2d grid.
        /// </summary>
        /// <param name="X">Vector of x values.</param>
        /// <param name="Y">Vector of y values.</param>
        /// <param name="outY">[Output, Optional]: if on entry <paramref name="outY"/> is not null it holds the values for the Y dimension on return.</param>
        /// <returns>X values matrix along the values of <paramref name="X"/>. Corresponding values for Y are returned in <paramref name="outY"/>.</returns>
        /// <remarks><para>The matrices returned can conveniently be used to evaluate and visualize functions of 2 variables over a grid of (X Y) positions.</para>
        /// <para>Note that the X dimension goes along the <i>rows</i>, while the second dimension is considered the columns! This deviates from the 
        /// common intuition and the actual storage order of matrix elements (i.e.: first dimension along the columns and 2nd dimension along the rows). 
        /// The reason for it is the convenience this order gives for plotting purposes: In most plots the X axis is expected to run horizontaly.</para></remarks>
        /// <exception cref="ArgumentNullException">If <paramref name="X"/> or <paramref name="Y"/> is null.</exception>
        /// <seealso cref="meshgrid(InArray{float}, InArray{float}, InArray{float}, OutArray{float}, OutArray{float})"/>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Array<float > meshgrid(InArray<float > X, InArray<float > Y
                                                                  , OutArray<float > outY = null)
        {
            using (Scope.Enter()) {
                if (isnull(X) || isnull(Y)) {
                    throw new ArgumentNullException("X and Y must not be null.");
                }
                Array<float> XX = reshape(X, 1, X.S.NumberOfElements);
                Array<float> YY = reshape(Y, Y.S.NumberOfElements, 1);

                Array<float> ret = repmat(XX, size(YY.Length, 1));
                if (!isnull(outY)) lock(outY.SynchObj) outY.a = repmat(YY, size(1, XX.Length));
                return ret;
            }
        }

        /// <summary>
        /// Create 3d arrays for evaluation and visualization of 3 dimensional functions.
        /// </summary>
        /// <param name="X">Vector of x values.</param>
        /// <param name="Y">Vector of y values.</param>
        /// <param name="Z">Vector of z values.</param>
        /// <param name="outY">[Output, Optional] If on entry <paramref name="outY"/> is not null it holds the values for the y dimension on return.</param>
        /// <param name="outZ">[Output, Optional] If on entry <paramref name="outZ"/> is not null it holds the values for the z dimension on return.</param>
        /// <returns>X value array along the values of <paramref name="X"/>, arrays for y and z dimensions are returned in <paramref name="outY"/> and <paramref name="outZ"/> respectively.</returns>
        /// <remarks><para>The arrays returned can conveniently be used to evaluate and visualize functions of 3 variables X, Y and Z. Coordinates build 
        /// a 3 dimensional grid with edges at all permutations of <paramref name="X"/>, <paramref name="Y"/> and <paramref name="Z"/>.</para>
        /// <para>Note that the X dimension goes along the <i>rows</i>, while the second dimension is considered the columns! This deviates from the 
        /// common intuition and the actual storage order of matrix elements (i.e.: first dimension along the columns and 2nd dimension along the rows). 
        /// The reason for it is the convenience this order gives for plotting purposes: In most plots the X axis is expected to run horizontaly.</para></remarks>
        /// <exception cref="ArgumentNullException">If <paramref name="X"/> or <paramref name="Y"/> is null.</exception>
        /// <seealso cref="meshgrid(InArray{float}, InArray{float}, InArray{float}, OutArray{float}, OutArray{float})"/>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Array<float > meshgrid(InArray<float > X, InArray<float > Y, InArray<float > Z
                                                                ,OutArray<float > outY = null, OutArray<float > outZ = null)
        {
            using (Scope.Enter()) {
                if (isnull(X) || isnull(Y) || isnull(Z)) {
                    throw new ArgumentNullException("All of the input parameters X, Y and Z are obligatory and may not be null.");
                }
                Array<float > XX = reshape(X, 1, X.S.NumberOfElements); 
                Array<float > YY = reshape(Y, Y.S.NumberOfElements, 1); 
                Array<float > ZZ = reshape(Z, 1, 1, Z.S.NumberOfElements);
                                              
                Array<float > ret = repmat(XX,size(YY.S.NumberOfElements,1,ZZ.S.NumberOfElements));
                if (!isnull(outY)) lock (outY.SynchObj) outY.a = repmat(YY,size(1,XX.S.NumberOfElements, ZZ.S.NumberOfElements));
                if (!isnull(outZ)) lock (outZ.SynchObj) outZ.a = repmat(ZZ, size(YY.S.NumberOfElements, XX.S.NumberOfElements, 1));
                return ret;
            }
        }
        /// <summary>
        /// Create two matrices for evaluation and visualization of 2 dimensional functions over a 2d grid.
        /// </summary>
        /// <param name="X">Vector of x values.</param>
        /// <param name="Y">Vector of y values.</param>
        /// <param name="outY">[Output, Optional]: if on entry <paramref name="outY"/> is not null it holds the values for the Y dimension on return.</param>
        /// <returns>X values matrix along the values of <paramref name="X"/>. Corresponding values for Y are returned in <paramref name="outY"/>.</returns>
        /// <remarks><para>The matrices returned can conveniently be used to evaluate and visualize functions of 2 variables over a grid of (X Y) positions.</para>
        /// <para>Note that the X dimension goes along the <i>rows</i>, while the second dimension is considered the columns! This deviates from the 
        /// common intuition and the actual storage order of matrix elements (i.e.: first dimension along the columns and 2nd dimension along the rows). 
        /// The reason for it is the convenience this order gives for plotting purposes: In most plots the X axis is expected to run horizontaly.</para></remarks>
        /// <exception cref="ArgumentNullException">If <paramref name="X"/> or <paramref name="Y"/> is null.</exception>
        /// <seealso cref="meshgrid(InArray{complex}, InArray{complex}, InArray{complex}, OutArray{complex}, OutArray{complex})"/>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Array<complex > meshgrid(InArray<complex > X, InArray<complex > Y
                                                                  , OutArray<complex > outY = null)
        {
            using (Scope.Enter()) {
                if (isnull(X) || isnull(Y)) {
                    throw new ArgumentNullException("X and Y must not be null.");
                }
                Array<complex> XX = reshape(X, 1, X.S.NumberOfElements);
                Array<complex> YY = reshape(Y, Y.S.NumberOfElements, 1);

                Array<complex> ret = repmat(XX, size(YY.Length, 1));
                if (!isnull(outY)) lock(outY.SynchObj) outY.a = repmat(YY, size(1, XX.Length));
                return ret;
            }
        }

        /// <summary>
        /// Create 3d arrays for evaluation and visualization of 3 dimensional functions.
        /// </summary>
        /// <param name="X">Vector of x values.</param>
        /// <param name="Y">Vector of y values.</param>
        /// <param name="Z">Vector of z values.</param>
        /// <param name="outY">[Output, Optional] If on entry <paramref name="outY"/> is not null it holds the values for the y dimension on return.</param>
        /// <param name="outZ">[Output, Optional] If on entry <paramref name="outZ"/> is not null it holds the values for the z dimension on return.</param>
        /// <returns>X value array along the values of <paramref name="X"/>, arrays for y and z dimensions are returned in <paramref name="outY"/> and <paramref name="outZ"/> respectively.</returns>
        /// <remarks><para>The arrays returned can conveniently be used to evaluate and visualize functions of 3 variables X, Y and Z. Coordinates build 
        /// a 3 dimensional grid with edges at all permutations of <paramref name="X"/>, <paramref name="Y"/> and <paramref name="Z"/>.</para>
        /// <para>Note that the X dimension goes along the <i>rows</i>, while the second dimension is considered the columns! This deviates from the 
        /// common intuition and the actual storage order of matrix elements (i.e.: first dimension along the columns and 2nd dimension along the rows). 
        /// The reason for it is the convenience this order gives for plotting purposes: In most plots the X axis is expected to run horizontaly.</para></remarks>
        /// <exception cref="ArgumentNullException">If <paramref name="X"/> or <paramref name="Y"/> is null.</exception>
        /// <seealso cref="meshgrid(InArray{complex}, InArray{complex}, InArray{complex}, OutArray{complex}, OutArray{complex})"/>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Array<complex > meshgrid(InArray<complex > X, InArray<complex > Y, InArray<complex > Z
                                                                ,OutArray<complex > outY = null, OutArray<complex > outZ = null)
        {
            using (Scope.Enter()) {
                if (isnull(X) || isnull(Y) || isnull(Z)) {
                    throw new ArgumentNullException("All of the input parameters X, Y and Z are obligatory and may not be null.");
                }
                Array<complex > XX = reshape(X, 1, X.S.NumberOfElements); 
                Array<complex > YY = reshape(Y, Y.S.NumberOfElements, 1); 
                Array<complex > ZZ = reshape(Z, 1, 1, Z.S.NumberOfElements);
                                              
                Array<complex > ret = repmat(XX,size(YY.S.NumberOfElements,1,ZZ.S.NumberOfElements));
                if (!isnull(outY)) lock (outY.SynchObj) outY.a = repmat(YY,size(1,XX.S.NumberOfElements, ZZ.S.NumberOfElements));
                if (!isnull(outZ)) lock (outZ.SynchObj) outZ.a = repmat(ZZ, size(YY.S.NumberOfElements, XX.S.NumberOfElements, 1));
                return ret;
            }
        }

#endregion HYCALPER AUTO GENERATED CODE
   }
}
