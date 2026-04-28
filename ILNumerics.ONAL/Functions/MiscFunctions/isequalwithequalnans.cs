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
using ILNumerics;
using static ILNumerics.Globals;

/*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="here">
        double
    </source>
    <destination>float</destination>
    <destination>complex</destination>
    <destination>fcomplex</destination>
</type>
<type>
    <source locate="here">
        Double
    </source>
    <destination>Single</destination>
    <destination>Complex</destination>
    <destination>FComplex</destination>
</type>
</hycalper>
*/
namespace ILNumerics.Core.Functions.Builtin {

    internal static partial class MathInternal {

        #region HYCALPER LOOPSTART 

        /// <summary>
        /// Checks if all elements of two arrays are equal, comparing NaN and +/- infinity like regular values. 
        /// </summary>
        /// <param name="A">Input array</param>
        /// <param name="B">Input array</param>
        /// <returns>True, if all elements of both arrays are equal; false if any elements or the sizes are not equal.</returns>
        internal static bool isequalwithequalnans(InArray<double> A, 
                                            InArray<double> B) {

            return allall(eqnan(A, B)); 
        }
        #endregion HYCALPER LOOPEND 
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 

        /// <summary>
        /// Checks if all elements of two arrays are equal, comparing NaN and +/- infinity like regular values. 
        /// </summary>
        /// <param name="A">Input array</param>
        /// <param name="B">Input array</param>
        /// <returns>True, if all elements of both arrays are equal; false if any elements or the sizes are not equal.</returns>
        internal static bool isequalwithequalnans(InArray<fcomplex> A, 
                                            InArray<fcomplex> B) {

            return allall(eqnan(A, B)); 
        }

        /// <summary>
        /// Checks if all elements of two arrays are equal, comparing NaN and +/- infinity like regular values. 
        /// </summary>
        /// <param name="A">Input array</param>
        /// <param name="B">Input array</param>
        /// <returns>True, if all elements of both arrays are equal; false if any elements or the sizes are not equal.</returns>
        internal static bool isequalwithequalnans(InArray<complex> A, 
                                            InArray<complex> B) {

            return allall(eqnan(A, B)); 
        }

        /// <summary>
        /// Checks if all elements of two arrays are equal, comparing NaN and +/- infinity like regular values. 
        /// </summary>
        /// <param name="A">Input array</param>
        /// <param name="B">Input array</param>
        /// <returns>True, if all elements of both arrays are equal; false if any elements or the sizes are not equal.</returns>
        internal static bool isequalwithequalnans(InArray<float> A, 
                                            InArray<float> B) {

            return allall(eqnan(A, B)); 
        }

#endregion HYCALPER AUTO GENERATED CODE

    }
}
