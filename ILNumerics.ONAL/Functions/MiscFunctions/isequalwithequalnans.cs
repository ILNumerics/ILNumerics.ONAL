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
