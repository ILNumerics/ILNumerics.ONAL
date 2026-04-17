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
using static ILNumerics.ILMath;

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
<type>
    <source locate="here">
        Double
    </source>
    <destination>double</destination>
    <destination>float</destination>
    <destination>float</destination>
</type>
<type>
    <source locate="nextline">
        cmplxDiag
    </source>
    <destination><![CDATA[return sqrt(sum(real(diag<complex>(multiply(A, conj(A.T))))));]]></destination>
    <destination><![CDATA[return sqrt(sum(diag<float>(multiply(A, A.T))));]]></destination>
    <destination><![CDATA[return sqrt(sum(real(diag<fcomplex>(multiply(A, conj(A.T))))));]]></destination>
</type>
<type>
    <source locate="nextline">
        cmplxFrobVector
    </source>
    <destination><![CDATA[return sqrt(real(sum(A * conj(A))));]]></destination>
    <destination><![CDATA[return sqrt(sum(A * A));]]></destination>
    <destination><![CDATA[return sqrt(real(sum(A * conj(A))));]]></destination>
</type>
</hycalper>
*/

namespace ILNumerics {

    public partial class ILMath {

        #region HYCALPER LOOPSTART
        /// <summary>
        /// Vector or matrix norm.
        /// </summary>
        /// <param name="A">Input matrix or vector.</param>
        /// <param name="degree">[Optional] Degree of the norm. Default: 2.</param>
        /// <returns>Array of same type as input array <paramref name="A"/>.</returns>
        /// <remarks>For vectors, <paramref name="degree"/> must be one of: 
        /// <list type="bullet">
        /// <item>0 : returns sqrt(sum(A * A))</item>
        /// <item>System.double.PositiveInfinity: returns max(abs(A))</item>
        /// <item>System.double.NegativeInfinity: returns min(abs(A))</item>
        /// <item>other double values: returns sum(pow(abs(A),degree))^(1/degree)</item>
        /// </list>
        /// For matrices <paramref name="degree"/> must be one of: 
        /// <list type="bullet">
        /// <item>0: returns Frobenius norm: sqrt(sum(diag(multiply(A, A.T))))</item>
        /// <item>1: returns 1-norm, max(sum(abs(A)))</item>
        /// <item>2: returns the largest singular value of A, max(svd(A))</item>
        /// <item>PositiveInfinity: returns maxall(sum(abs(A), 1)), the largest value of the sums along the rows</item>
        /// </list>
        /// <para>norm(A,0) with A being a vector extends naturally to the frobenius norm for matrices.</para>
        /// <para>For empty arrays A, scalar 0 is returned.</para>
        /// </remarks>
        public static Array<Double> norm(InArray<double> A, Double degree = 2) {
            using (Scope.Enter()) {

                Array<double> _A = A; 

                if (Object.Equals(_A, null) || !_A.IsMatrix)
                    throw new ArgumentException("input array must be matrix or vector.");
                if (_A.IsEmpty)
                    return (Double)0;
                else if (_A.IsVector) {
                    if (degree == Double.PositiveInfinity) {
                        return max(abs(_A));
                    } else if (degree == Double.NegativeInfinity) {
                        return min(abs(_A));
                    } else {
                        if (degree == 0.0)
                            /*!HC:cmplxFrobVector*/
                            return sqrt(sum(_A * _A));
                        return pow(sum(pow(abs(_A), (Double)degree)), (Double)(1.0 / degree));
                    }
                } else {
                    if (degree == 1.0) {
                        return max(sum(abs(_A)));
                    } else if (degree == 2.0) {
                        return max(svd(_A));
                    } else if (degree == Double.PositiveInfinity) {
                        return maxall(sum(abs(_A), 1));
                    } else if (degree == 0.0) {
                        /*!HC:cmplxDiag*/
                        return sqrt(sum(diag<double>(multiply(_A, _A.T))));
                    } else {
                        throw new ArgumentException("invalid argument 'degree' for input matrix _A. Valid options are: 0,1,2,Double.PositiveInfinity");
                    }
                }
            }
        }
        #endregion HYCALPER LOOPEND 
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
        /// <summary>
        /// Vector or matrix norm.
        /// </summary>
        /// <param name="A">Input matrix or vector.</param>
        /// <param name="degree">[Optional] Degree of the norm. Default: 2.</param>
        /// <returns>Array of same type as input array <paramref name="A"/>.</returns>
        /// <remarks>For vectors, <paramref name="degree"/> must be one of: 
        /// <list type="bullet">
        /// <item>0 : returns sqrt(sum(A * A))</item>
        /// <item>System.fcomplex.PositiveInfinity: returns max(abs(A))</item>
        /// <item>System.fcomplex.NegativeInfinity: returns min(abs(A))</item>
        /// <item>other fcomplex values: returns sum(pow(abs(A),degree))^(1/degree)</item>
        /// </list>
        /// For matrices <paramref name="degree"/> must be one of: 
        /// <list type="bullet">
        /// <item>0: returns Frobenius norm: sqrt(sum(diag(multiply(A, A.T))))</item>
        /// <item>1: returns 1-norm, max(sum(abs(A)))</item>
        /// <item>2: returns the largest singular value of A, max(svd(A))</item>
        /// <item>PositiveInfinity: returns maxall(sum(abs(A), 1)), the largest value of the sums along the rows</item>
        /// </list>
        /// <para>norm(A,0) with A being a vector extends naturally to the frobenius norm for matrices.</para>
        /// <para>For empty arrays A, scalar 0 is returned.</para>
        /// </remarks>
        public static Array<float> norm(InArray<fcomplex> A, float degree = 2) {
            using (Scope.Enter()) {

                Array<fcomplex> _A = A; 

                if (Object.Equals(_A, null) || !_A.IsMatrix)
                    throw new ArgumentException("input array must be matrix or vector.");
                if (_A.IsEmpty)
                    return (float)0;
                else if (_A.IsVector) {
                    if (degree == float.PositiveInfinity) {
                        return max(abs(_A));
                    } else if (degree == float.NegativeInfinity) {
                        return min(abs(_A));
                    } else {
                        if (degree == 0.0)
                            return sqrt(real(sum(A * conj(A))));
                        return pow(sum(pow(abs(_A), (float)degree)), (float)(1.0 / degree));
                    }
                } else {
                    if (degree == 1.0) {
                        return max(sum(abs(_A)));
                    } else if (degree == 2.0) {
                        return max(svd(_A));
                    } else if (degree == float.PositiveInfinity) {
                        return maxall(sum(abs(_A), 1));
                    } else if (degree == 0.0) {
                        return sqrt(sum(real(diag<fcomplex>(multiply(A, conj(A.T))))));
                    } else {
                        throw new ArgumentException("invalid argument 'degree' for input matrix _A. Valid options are: 0,1,2,float.PositiveInfinity");
                    }
                }
            }
        }
        /// <summary>
        /// Vector or matrix norm.
        /// </summary>
        /// <param name="A">Input matrix or vector.</param>
        /// <param name="degree">[Optional] Degree of the norm. Default: 2.</param>
        /// <returns>Array of same type as input array <paramref name="A"/>.</returns>
        /// <remarks>For vectors, <paramref name="degree"/> must be one of: 
        /// <list type="bullet">
        /// <item>0 : returns sqrt(sum(A * A))</item>
        /// <item>System.float.PositiveInfinity: returns max(abs(A))</item>
        /// <item>System.float.NegativeInfinity: returns min(abs(A))</item>
        /// <item>other float values: returns sum(pow(abs(A),degree))^(1/degree)</item>
        /// </list>
        /// For matrices <paramref name="degree"/> must be one of: 
        /// <list type="bullet">
        /// <item>0: returns Frobenius norm: sqrt(sum(diag(multiply(A, A.T))))</item>
        /// <item>1: returns 1-norm, max(sum(abs(A)))</item>
        /// <item>2: returns the largest singular value of A, max(svd(A))</item>
        /// <item>PositiveInfinity: returns maxall(sum(abs(A), 1)), the largest value of the sums along the rows</item>
        /// </list>
        /// <para>norm(A,0) with A being a vector extends naturally to the frobenius norm for matrices.</para>
        /// <para>For empty arrays A, scalar 0 is returned.</para>
        /// </remarks>
        public static Array<float> norm(InArray<float> A, float degree = 2) {
            using (Scope.Enter()) {

                Array<float> _A = A; 

                if (Object.Equals(_A, null) || !_A.IsMatrix)
                    throw new ArgumentException("input array must be matrix or vector.");
                if (_A.IsEmpty)
                    return (float)0;
                else if (_A.IsVector) {
                    if (degree == float.PositiveInfinity) {
                        return max(abs(_A));
                    } else if (degree == float.NegativeInfinity) {
                        return min(abs(_A));
                    } else {
                        if (degree == 0.0)
                            return sqrt(sum(A * A));
                        return pow(sum(pow(abs(_A), (float)degree)), (float)(1.0 / degree));
                    }
                } else {
                    if (degree == 1.0) {
                        return max(sum(abs(_A)));
                    } else if (degree == 2.0) {
                        return max(svd(_A));
                    } else if (degree == float.PositiveInfinity) {
                        return maxall(sum(abs(_A), 1));
                    } else if (degree == 0.0) {
                        return sqrt(sum(diag<float>(multiply(A, A.T))));
                    } else {
                        throw new ArgumentException("invalid argument 'degree' for input matrix _A. Valid options are: 0,1,2,float.PositiveInfinity");
                    }
                }
            }
        }
        /// <summary>
        /// Vector or matrix norm.
        /// </summary>
        /// <param name="A">Input matrix or vector.</param>
        /// <param name="degree">[Optional] Degree of the norm. Default: 2.</param>
        /// <returns>Array of same type as input array <paramref name="A"/>.</returns>
        /// <remarks>For vectors, <paramref name="degree"/> must be one of: 
        /// <list type="bullet">
        /// <item>0 : returns sqrt(sum(A * A))</item>
        /// <item>System.complex.PositiveInfinity: returns max(abs(A))</item>
        /// <item>System.complex.NegativeInfinity: returns min(abs(A))</item>
        /// <item>other complex values: returns sum(pow(abs(A),degree))^(1/degree)</item>
        /// </list>
        /// For matrices <paramref name="degree"/> must be one of: 
        /// <list type="bullet">
        /// <item>0: returns Frobenius norm: sqrt(sum(diag(multiply(A, A.T))))</item>
        /// <item>1: returns 1-norm, max(sum(abs(A)))</item>
        /// <item>2: returns the largest singular value of A, max(svd(A))</item>
        /// <item>PositiveInfinity: returns maxall(sum(abs(A), 1)), the largest value of the sums along the rows</item>
        /// </list>
        /// <para>norm(A,0) with A being a vector extends naturally to the frobenius norm for matrices.</para>
        /// <para>For empty arrays A, scalar 0 is returned.</para>
        /// </remarks>
        public static Array<double> norm(InArray<complex> A, double degree = 2) {
            using (Scope.Enter()) {

                Array<complex> _A = A; 

                if (Object.Equals(_A, null) || !_A.IsMatrix)
                    throw new ArgumentException("input array must be matrix or vector.");
                if (_A.IsEmpty)
                    return (double)0;
                else if (_A.IsVector) {
                    if (degree == double.PositiveInfinity) {
                        return max(abs(_A));
                    } else if (degree == double.NegativeInfinity) {
                        return min(abs(_A));
                    } else {
                        if (degree == 0.0)
                            return sqrt(real(sum(A * conj(A))));
                        return pow(sum(pow(abs(_A), (double)degree)), (double)(1.0 / degree));
                    }
                } else {
                    if (degree == 1.0) {
                        return max(sum(abs(_A)));
                    } else if (degree == 2.0) {
                        return max(svd(_A));
                    } else if (degree == double.PositiveInfinity) {
                        return maxall(sum(abs(_A), 1));
                    } else if (degree == 0.0) {
                        return sqrt(sum(real(diag<complex>(multiply(A, conj(A.T))))));
                    } else {
                        throw new ArgumentException("invalid argument 'degree' for input matrix _A. Valid options are: 0,1,2,double.PositiveInfinity");
                    }
                }
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

    }
}
