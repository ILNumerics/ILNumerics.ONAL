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

#pragma warning disable 162 
#define ROBUST_COMPLEX_DIVISION
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Numerics;

/*!HC:TYPELIST:
<hycalper>
</hycalper>
 */

namespace ILNumerics {
    /// <summary>
    /// Floating point complex value data type of float (single) precision
    /// </summary>
    /// <remarks>This class extends the system value types for real numbers to complex float 
    /// values. Besides the publicly available members 'real' and 'imag' it provides all the 
    /// basis functionality the floating point System.double brings (abs, log, sqrt, tan etc.) for 
    /// float precision complex,
    /// as well as it overrides the basic unary and binary operators for all common system value 
    /// types including rarely used types (e.g. UInt16). This includes the basic numerical operations 
    /// like '+','-','/','*' and the relational operators: '==','>','>=' etc. Also there are some 
    /// explicit and some implicit casting operators from / to fcomplex values into system 
    /// value types. </remarks>
    [Serializable]    
    [StructLayout(LayoutKind.Sequential)]
    public struct fcomplex : IEquatable<fcomplex> {
        /// <summary>
        /// Real part of this complex number
        /// </summary>
        public float real;
        /// <summary>
        /// Imaginary part of this complex number
        /// </summary>
        public float imag;
        /// <summary>
        /// Imaginary unit 
        /// </summary>
        public static readonly fcomplex i = new fcomplex(0.0f,1.0f); 

        /// <summary>
        /// Construct new float complex number
        /// </summary>
        /// <param name="real">Real part</param>
        /// <param name="imag">Imaginary part</param>
        public fcomplex(float real, float imag) {
            this.real = real;
            this.imag = imag;
        }

        /// <summary>
        /// Returns the complex conjugate of this complex number.
        /// </summary>
        /// <seealso cref="Conjugate()"/>
        public fcomplex conj {
            get{
                return new fcomplex(real,imag * (-1.0f));
            }
        }

        /// <summary>
        /// Positive infinity for real and imaginary part of complex value
        /// </summary>
        [Obsolete("Use fcomplex.PositiveInfinity instead.")]
        public static fcomplex INF {
            get {
                return new fcomplex(
                    float.PositiveInfinity,
                    float.PositiveInfinity
                );
            }
        }
        /// <summary>
        /// Positive infinity for real and imag part of complex value
        /// </summary>
        public static fcomplex PositiveInfinity {
            get {
                return new fcomplex(
                    float.PositiveInfinity,
                    float.PositiveInfinity
                );
            }
        }

        /// <summary>
        /// New fcomplex, real and imaginary parts are zero
        /// </summary>
        public static fcomplex Zero {
            get {
                return new fcomplex(0f,0f);
            }
        }

        /// <summary>
        /// fcomplex quantity, marked as being "not a number"
        /// </summary>
        public static fcomplex NaN {
            get {
                return new fcomplex(float.NaN,float.NaN); 
            }
        }

        /// <summary>
        /// Are obj's real and imaginary part identical to the real and imaginary parts of this fcomplex
        /// </summary>
        /// <param name="obj">fcomplex object to determine the equality for</param>
        /// <returns>true if obj is of fcomplex type and its real and imag part has the same 
        /// values as the real and imaginary part of this array.</returns>
        public override bool Equals(object obj) {
            if (object.Equals(obj, null)) return false;
            if (obj.GetType() == typeof(fcomplex) && ((fcomplex)obj) == this)
                return true; 
            return false; 
        }

        /// <summary>
        /// Check if a fcomplex number equals this fcomplex number
        /// </summary>
        /// <param name="other">other complex number</param>
        /// <returns>true if both, real and imaginary parts of both complex number are (binary) equal, false otherwise</returns>
        public bool Equals(fcomplex other) {
            return real.Equals(other.real) && imag.Equals(other.imag);
        }

        /// <summary>
        /// Give HashCode of this fcomplex number
        /// </summary>
        /// <returns>HashCode of this fcomplex number</returns>
        public override int GetHashCode() {
            return 31 * real.GetHashCode() + imag.GetHashCode();
        }

        /// <summary>
        /// Conjugates this complex number inplace.
        /// </summary>
        /// <seealso cref="conj"/>
        /// <seealso cref="Conjugate(fcomplex)"/>
        public void Conjugate() {
            imag = -imag;
        }

        #region HYCALPER LOOPSTART OPERATORS_complex+complex@Runtime\complex.cs
        /*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="after">
        Tret
    </source>
    <destination>fcomplex</destination>
    <destination>complex</destination>
</type>
<type>
    <source locate="after">
        TinArr1 
    </source>
    <destination>fcomplex</destination>
    <destination>fcomplex</destination>
</type>
<type>
    <source locate="after">
        TinArr2
    </source>
    <destination>fcomplex</destination>
    <destination>complex</destination>
</type>
<type>
    <source locate="after" endmark=" ">
        FCast
    </source>
    <destination>(float)</destination>
    <destination>(double)</destination>
</type>
<type>
    <source locate="after">
        TRret
    </source>
    <destination>float</destination>
    <destination>double</destination>
</type>
</hycalper>
 */
        #endregion HYCALPER LOOPEND OPERATORS_complex+complex@Runtime\complex.cs
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       
        /// <summary>
        /// Add two complex numbers
        /// </summary>
        /// <param name="A">First summand</param>
        /// <param name="B">Second summand</param>
        /// <returns>result</returns>
        public static  complex operator +( fcomplex A,  complex B) {
            complex ret; 
             ret.real =  (double) (A.real + B.real );
             ret.imag =  (double) (A.imag + B.imag );
            return ret;
        }
        /// <summary>
        /// Subtract two complex values
        /// </summary>
        /// <param name="A">Minuend</param>
        /// <param name="B">Subtrahend</param>
        /// <returns>result</returns>
        public static  complex operator -( fcomplex A,  complex B) {
            complex ret; 
            ret.real =  (double) (A.real  - B.real );
            ret.imag =  (double) (A.imag - B.imag );
            return ret;
        }
        /// <summary>
        /// Multiply two complex values
        /// </summary>
        /// <param name="A">First factor</param>
        /// <param name="B">Second factor</param>
        /// <returns>result</returns>
        public static  complex operator *( fcomplex A,  complex B) {
            complex ret;
            ret.real =  (double) ((A.real * B.real ) - (A.imag * B.imag ));
            ret.imag =  (double) ((A.real * B.imag ) + (A.imag * B.real ));
            return ret; 
        }
        /// <summary>
        /// Divide two complex numbers.
        /// </summary>
        /// <param name="A">Divident</param>
        /// <param name="B">Divisor</param>
        /// <returns>Result</returns>
        /// <remarks><para>Unless the operator must handle special inputs (Inf or 0 values), 
        /// the algorithm described in [1] is used for division. This is considered to be 
        /// more robust against floating point overflow than the naive approach of simple 
        /// cartesian division.</para>
        /// <para>References: [1]: Smith, R.L., Algorithm 116: Complex division. Commun.ACM 5,8 (1962),435 <br />
        /// [2]: Stewart, G.W., A note on complex division, ACM trans.on math software, Vol.11, N.3 (1985)</para>
        /// <para>For a more detailed investigation of the robustnes and performance of various complex division algorithms, see: [3]: M.Baudin, R.L.Smith: A Robust Complex Division in Scilab (https://arxiv.org/pdf/1210.4539.pdf)</para>
        /// </remarks>
        public static  complex operator /( fcomplex A,  complex B) {
            if (B.imag == 0) return A / B.real; 
            //return A * (1 / B); 
            if (IsNaN(A) ||  complex .IsNaN(B)) return NaN;
            //if ( complex .IsInfinity(B)) return NaN;            
            //if (A.real == 0 && A.imag == 0) return ( complex )0;
            complex ret;
            if (B.real == 0) {
                ret.imag =  (double) -(A.real / B.imag); 
                ret.real =  (double) (A.imag / B.imag); 
                return ret; 
            }
            // This would be the naive approach. But it comes with small robustness against overflow. This is used in legacy compilers (ifort) when x87 registers are available (80 bit). 
#if !ROBUST_COMPLEX_DIVISION
            //if (norm2 == 0) return PositiveInfinity;    // this may be removed, since 1) B is checked above and division by 0 results in inf anyway ? 
            
            //ret.real =  (double) * B.real) + (A.imag * B.imag)) / norm2);
            //ret.imag =  (double) * B.real) - (A.real * B.imag)) / norm2);
            //return ret; 
            
            // alternative scaling. Better compatible with ifort ?! 
            //A = A / norm2;
            //B = B / norm2; 

            // CAUTION!!! this is (exactly) what ifort does! Don't change without accepting that we would loose byte compatibility!!! 
            // CAUTION 2! Below code is prone to over / underflow, obviously. But this is what a plain, simple complex divide does. See ZLADIV for a more robust version. 
            double norm2 = 1.0 / (B.real * B.real + B.imag * B.imag);
            ret.real =  (double) * B.real) + (A.imag * B.imag)) * norm2;
            ret.imag =  (double) * B.real) - (A.real * B.imag)) * norm2;
            return ret; 
#else
            // Smiths algorithm! There are more accurate algorithms available. See: 
            // https://arxiv.org/pdf/1210.4539.pdf (Scilab rel.)
            // But they come with significantly more compuational cost and more complexity. 
            // System.Numerics.Complex does use Smiths algorithm, too. (Not that this would be an argument here.)
            double a = A.real, b = A.imag;
            double c = B.real, d = B.imag;
            if (Math.Abs(d) <= Math.Abs(c)) {
                double r = d / c;
                double den = c + d * r;
                return new  complex(( double) ((a + b * r) / den), ( double)((b - a * r) / den));
            } else {
                double r = c / d;
                double den = c * r + d;
                return new  complex(( double)((a * r + b) / den), ( double)((b * r - a) / den));
            }
#endif
        }
        /// <summary>
        /// Equality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true, if real and imaginary part are identical</returns>
        public static bool operator ==( fcomplex A,  complex B) {
            return (A.imag  == B.imag ) && (A.real  == B.real );
        }
        /// <summary>
        /// Unequality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real and imaginary parts of A and B are not equal, false otherwise</returns>
        public static bool operator !=( fcomplex A,  complex B) {
            return (A.imag  != B.imag ) || (A.real  != B.real );
        }
        /// <summary>
        /// Greater than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator > ( fcomplex A,  complex B) {
            return (A.real > B.real );
        }
        /// <summary>
        /// Lower than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator < ( fcomplex A,  complex B) {
            return (A.real < B.real );
        }
        /// <summary>
        /// Greater than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator >=( fcomplex A,  complex B) {
            return (A.real >= B.real );
        }
        /// <summary>
        /// Lower than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <=( fcomplex A,  complex B) { 
            return (A.real <= B.real );
        }
       
        /// <summary>
        /// Add two complex numbers
        /// </summary>
        /// <param name="A">First summand</param>
        /// <param name="B">Second summand</param>
        /// <returns>result</returns>
        public static  fcomplex operator +( fcomplex A,  fcomplex B) {
            fcomplex ret; 
             ret.real =  (float) (A.real + B.real );
             ret.imag =  (float) (A.imag + B.imag );
            return ret;
        }
        /// <summary>
        /// Subtract two complex values
        /// </summary>
        /// <param name="A">Minuend</param>
        /// <param name="B">Subtrahend</param>
        /// <returns>result</returns>
        public static  fcomplex operator -( fcomplex A,  fcomplex B) {
            fcomplex ret; 
            ret.real =  (float) (A.real  - B.real );
            ret.imag =  (float) (A.imag - B.imag );
            return ret;
        }
        /// <summary>
        /// Multiply two complex values
        /// </summary>
        /// <param name="A">First factor</param>
        /// <param name="B">Second factor</param>
        /// <returns>result</returns>
        public static  fcomplex operator *( fcomplex A,  fcomplex B) {
            fcomplex ret;
            ret.real =  (float) ((A.real * B.real ) - (A.imag * B.imag ));
            ret.imag =  (float) ((A.real * B.imag ) + (A.imag * B.real ));
            return ret; 
        }
        /// <summary>
        /// Divide two complex numbers.
        /// </summary>
        /// <param name="A">Divident</param>
        /// <param name="B">Divisor</param>
        /// <returns>Result</returns>
        /// <remarks><para>Unless the operator must handle special inputs (Inf or 0 values), 
        /// the algorithm described in [1] is used for division. This is considered to be 
        /// more robust against floating point overflow than the naive approach of simple 
        /// cartesian division.</para>
        /// <para>References: [1]: Smith, R.L., Algorithm 116: Complex division. Commun.ACM 5,8 (1962),435 <br />
        /// [2]: Stewart, G.W., A note on complex division, ACM trans.on math software, Vol.11, N.3 (1985)</para>
        /// <para>For a more detailed investigation of the robustnes and performance of various complex division algorithms, see: [3]: M.Baudin, R.L.Smith: A Robust Complex Division in Scilab (https://arxiv.org/pdf/1210.4539.pdf)</para>
        /// </remarks>
        public static  fcomplex operator /( fcomplex A,  fcomplex B) {
            if (B.imag == 0) return A / B.real; 
            //return A * (1 / B); 
            if (IsNaN(A) ||  fcomplex .IsNaN(B)) return NaN;
            //if ( fcomplex .IsInfinity(B)) return NaN;            
            //if (A.real == 0 && A.imag == 0) return ( fcomplex )0;
            fcomplex ret;
            if (B.real == 0) {
                ret.imag =  (float) -(A.real / B.imag); 
                ret.real =  (float) (A.imag / B.imag); 
                return ret; 
            }
            // This would be the naive approach. But it comes with small robustness against overflow. This is used in legacy compilers (ifort) when x87 registers are available (80 bit). 
#if !ROBUST_COMPLEX_DIVISION
            //if (norm2 == 0) return PositiveInfinity;    // this may be removed, since 1) B is checked above and division by 0 results in inf anyway ? 
            
            //ret.real =  (float) * B.real) + (A.imag * B.imag)) / norm2);
            //ret.imag =  (float) * B.real) - (A.real * B.imag)) / norm2);
            //return ret; 
            
            // alternative scaling. Better compatible with ifort ?! 
            //A = A / norm2;
            //B = B / norm2; 

            // CAUTION!!! this is (exactly) what ifort does! Don't change without accepting that we would loose byte compatibility!!! 
            // CAUTION 2! Below code is prone to over / underflow, obviously. But this is what a plain, simple complex divide does. See ZLADIV for a more robust version. 
            double norm2 = 1.0 / (B.real * B.real + B.imag * B.imag);
            ret.real =  (float) * B.real) + (A.imag * B.imag)) * norm2;
            ret.imag =  (float) * B.real) - (A.real * B.imag)) * norm2;
            return ret; 
#else
            // Smiths algorithm! There are more accurate algorithms available. See: 
            // https://arxiv.org/pdf/1210.4539.pdf (Scilab rel.)
            // But they come with significantly more compuational cost and more complexity. 
            // System.Numerics.Complex does use Smiths algorithm, too. (Not that this would be an argument here.)
            double a = A.real, b = A.imag;
            double c = B.real, d = B.imag;
            if (Math.Abs(d) <= Math.Abs(c)) {
                double r = d / c;
                double den = c + d * r;
                return new  fcomplex(( float) ((a + b * r) / den), ( float)((b - a * r) / den));
            } else {
                double r = c / d;
                double den = c * r + d;
                return new  fcomplex(( float)((a * r + b) / den), ( float)((b * r - a) / den));
            }
#endif
        }
        /// <summary>
        /// Equality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true, if real and imaginary part are identical</returns>
        public static bool operator ==( fcomplex A,  fcomplex B) {
            return (A.imag  == B.imag ) && (A.real  == B.real );
        }
        /// <summary>
        /// Unequality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real and imaginary parts of A and B are not equal, false otherwise</returns>
        public static bool operator !=( fcomplex A,  fcomplex B) {
            return (A.imag  != B.imag ) || (A.real  != B.real );
        }
        /// <summary>
        /// Greater than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator > ( fcomplex A,  fcomplex B) {
            return (A.real > B.real );
        }
        /// <summary>
        /// Lower than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator < ( fcomplex A,  fcomplex B) {
            return (A.real < B.real );
        }
        /// <summary>
        /// Greater than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator >=( fcomplex A,  fcomplex B) {
            return (A.real >= B.real );
        }
        /// <summary>
        /// Lower than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <=( fcomplex A,  fcomplex B) { 
            return (A.real <= B.real );
        }

#endregion HYCALPER AUTO GENERATED CODE

        #region HYCALPER LOOPSTART OPERATORS_complex+noncomplex@Runtime\complex.cs
        /*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="after">
        Tret
    </source>
    <destination>fcomplex</destination>
    <destination>fcomplex</destination>
    <destination>fcomplex</destination>
    <destination>fcomplex</destination>
    <destination>fcomplex</destination>
</type>
<type>
    <source locate="after">
        TinArr1 
    </source>
    <destination>fcomplex</destination>
    <destination>fcomplex</destination>
    <destination>fcomplex</destination>
    <destination>fcomplex</destination>
    <destination>fcomplex</destination>
</type>
<type>
    <source locate="after">
        TinArr2
    </source>
    <destination>double</destination>
    <destination>byte</destination>
    <destination>float</destination>
    <destination>Int32</destination>
    <destination>Int64</destination>
</type>
<type>
    <source locate="after" endmark=" ">
        FCast
    </source>
    <destination>(float)</destination>
    <destination>(float)</destination>
    <destination>(float)</destination>
    <destination>(float)</destination>
    <destination>(float)</destination>
</type>
<type>
    <source locate="after">
        infinity
    </source>
    <destination>fcomplex.INF</destination>
    <destination>fcomplex.INF</destination>
    <destination>fcomplex.INF</destination>
    <destination>fcomplex.INF</destination>
    <destination>fcomplex.INF</destination>
</type>
<type>
    <source locate="nextline">
        test4inf
    </source>
    <destination>if (double.IsInfinity(B))</destination>
    <destination>if (false)</destination>
    <destination>if (float.IsInfinity(B))</destination>
    <destination>if (false)</destination>
    <destination>if (false)</destination>
</type>
<type>
    <source locate="nextline">
        test4NaNin2 
    </source>
    <destination>if (double.IsNaN(B)) return NaN;</destination>
    <destination></destination>
    <destination>if (float.IsNaN(B)) return NaN;</destination>
    <destination></destination>
    <destination></destination>
</type>
</hycalper>
 */
        #endregion HYCALPER LOOPEND OPERATOR_complex+noncomplex@complex.cs
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       
        /// <summary>
        /// Add two complex numbers
        /// </summary>
        /// <param name="A">First summand</param>
        /// <param name="B">Second summand</param>
        /// <returns>Result</returns>
        public static  fcomplex operator +( fcomplex A,  Int64 B) {
            fcomplex ret;
            ret.real =  (float) (A.real + B);
            ret.imag =  (float) A.imag;
            return ret;
        }
        /// <summary>
        /// Subtract two values
        /// </summary>
        /// <param name="A">Minuend</param>
        /// <param name="B">Subtrahend</param>
        /// <returns>result</returns>
        public static  fcomplex operator -( fcomplex A,  Int64 B) {
            fcomplex ret;
            ret.real =  (float) (A.real - B);
            ret.imag =  (float) A.imag;
            return ret;
        }
        /// <summary>
        /// Multiply two values
        /// </summary>
        /// <param name="A">First factor</param>
        /// <param name="B">Second factor</param>
        /// <returns>result</returns>
        public static  fcomplex operator *( fcomplex A,  Int64 B) {
            fcomplex ret;
            ret.real =  (float) (A.real * B);
            ret.imag =  (float) (A.imag * B);
            return ret;
        }
        /// <summary>
        /// Divide two numbers
        /// </summary>
        /// <param name="A">Divident</param>
        /// <param name="B">Divisor</param>
        /// <returns>result</returns>
        public static  fcomplex operator /( fcomplex A,  Int64 B) {
            if (IsNaN(A)) return NaN;
            
            if (A.real == 0 && A.imag == 0) {
                if (B == 0) return NaN; 
                return ( fcomplex )0;
            } else {
                if (false)
                {
                    if (IsInfinity(A)) {
                        return NaN; 
                    } else {
                        return ( fcomplex )0;
                    }
                }
            }
            fcomplex ret;
            if (B == 0) return PositiveInfinity ;
            ret.real =  (float) (A.real / B);
            ret.imag =  (float) (A.imag / B);
            return ret;
        }
        /// <summary>
        /// Equality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>result</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator ==( fcomplex A,  Int64 B) {
            return (A.real == B && A.imag == 0.0);
        }
        /// <summary>
        /// Unequality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real and imaginary parts of A and B are not equal, false otherwise</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator !=( fcomplex A,  Int64 B) {
            return (A.imag != 0.0) || (A.real != B);
        }
        /// <summary>
        /// Freater than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator > ( fcomplex A,  Int64 B) {
            return (A.real > B);
        }
        /// <summary>
        /// Lower than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <(  fcomplex A,  Int64 B) {
            return (A.real < B);
        }
        /// <summary>
        /// Greater than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator >=( fcomplex A,  Int64 B) {
            return (A.real >= B);
        }
        /// <summary>
        /// Lower than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <=( fcomplex A,  Int64 B) {
            return (A.real <= B);
        }
       
        /// <summary>
        /// Add two complex numbers
        /// </summary>
        /// <param name="A">First summand</param>
        /// <param name="B">Second summand</param>
        /// <returns>Result</returns>
        public static  fcomplex operator +( fcomplex A,  Int32 B) {
            fcomplex ret;
            ret.real =  (float) (A.real + B);
            ret.imag =  (float) A.imag;
            return ret;
        }
        /// <summary>
        /// Subtract two values
        /// </summary>
        /// <param name="A">Minuend</param>
        /// <param name="B">Subtrahend</param>
        /// <returns>result</returns>
        public static  fcomplex operator -( fcomplex A,  Int32 B) {
            fcomplex ret;
            ret.real =  (float) (A.real - B);
            ret.imag =  (float) A.imag;
            return ret;
        }
        /// <summary>
        /// Multiply two values
        /// </summary>
        /// <param name="A">First factor</param>
        /// <param name="B">Second factor</param>
        /// <returns>result</returns>
        public static  fcomplex operator *( fcomplex A,  Int32 B) {
            fcomplex ret;
            ret.real =  (float) (A.real * B);
            ret.imag =  (float) (A.imag * B);
            return ret;
        }
        /// <summary>
        /// Divide two numbers
        /// </summary>
        /// <param name="A">Divident</param>
        /// <param name="B">Divisor</param>
        /// <returns>result</returns>
        public static  fcomplex operator /( fcomplex A,  Int32 B) {
            if (IsNaN(A)) return NaN;
            
            if (A.real == 0 && A.imag == 0) {
                if (B == 0) return NaN; 
                return ( fcomplex )0;
            } else {
                if (false)
                {
                    if (IsInfinity(A)) {
                        return NaN; 
                    } else {
                        return ( fcomplex )0;
                    }
                }
            }
            fcomplex ret;
            if (B == 0) return PositiveInfinity ;
            ret.real =  (float) (A.real / B);
            ret.imag =  (float) (A.imag / B);
            return ret;
        }
        /// <summary>
        /// Equality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>result</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator ==( fcomplex A,  Int32 B) {
            return (A.real == B && A.imag == 0.0);
        }
        /// <summary>
        /// Unequality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real and imaginary parts of A and B are not equal, false otherwise</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator !=( fcomplex A,  Int32 B) {
            return (A.imag != 0.0) || (A.real != B);
        }
        /// <summary>
        /// Freater than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator > ( fcomplex A,  Int32 B) {
            return (A.real > B);
        }
        /// <summary>
        /// Lower than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <(  fcomplex A,  Int32 B) {
            return (A.real < B);
        }
        /// <summary>
        /// Greater than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator >=( fcomplex A,  Int32 B) {
            return (A.real >= B);
        }
        /// <summary>
        /// Lower than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <=( fcomplex A,  Int32 B) {
            return (A.real <= B);
        }
       
        /// <summary>
        /// Add two complex numbers
        /// </summary>
        /// <param name="A">First summand</param>
        /// <param name="B">Second summand</param>
        /// <returns>Result</returns>
        public static  fcomplex operator +( fcomplex A,  float B) {
            fcomplex ret;
            ret.real =  (float) (A.real + B);
            ret.imag =  (float) A.imag;
            return ret;
        }
        /// <summary>
        /// Subtract two values
        /// </summary>
        /// <param name="A">Minuend</param>
        /// <param name="B">Subtrahend</param>
        /// <returns>result</returns>
        public static  fcomplex operator -( fcomplex A,  float B) {
            fcomplex ret;
            ret.real =  (float) (A.real - B);
            ret.imag =  (float) A.imag;
            return ret;
        }
        /// <summary>
        /// Multiply two values
        /// </summary>
        /// <param name="A">First factor</param>
        /// <param name="B">Second factor</param>
        /// <returns>result</returns>
        public static  fcomplex operator *( fcomplex A,  float B) {
            fcomplex ret;
            ret.real =  (float) (A.real * B);
            ret.imag =  (float) (A.imag * B);
            return ret;
        }
        /// <summary>
        /// Divide two numbers
        /// </summary>
        /// <param name="A">Divident</param>
        /// <param name="B">Divisor</param>
        /// <returns>result</returns>
        public static  fcomplex operator /( fcomplex A,  float B) {
            if (IsNaN(A)) return NaN;
            if (float.IsNaN(B)) return NaN;
            if (A.real == 0 && A.imag == 0) {
                if (B == 0) return NaN; 
                return ( fcomplex )0;
            } else {
                if (float.IsInfinity(B))
                {
                    if (IsInfinity(A)) {
                        return NaN; 
                    } else {
                        return ( fcomplex )0;
                    }
                }
            }
            fcomplex ret;
            if (B == 0) return PositiveInfinity ;
            ret.real =  (float) (A.real / B);
            ret.imag =  (float) (A.imag / B);
            return ret;
        }
        /// <summary>
        /// Equality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>result</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator ==( fcomplex A,  float B) {
            return (A.real == B && A.imag == 0.0);
        }
        /// <summary>
        /// Unequality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real and imaginary parts of A and B are not equal, false otherwise</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator !=( fcomplex A,  float B) {
            return (A.imag != 0.0) || (A.real != B);
        }
        /// <summary>
        /// Freater than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator > ( fcomplex A,  float B) {
            return (A.real > B);
        }
        /// <summary>
        /// Lower than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <(  fcomplex A,  float B) {
            return (A.real < B);
        }
        /// <summary>
        /// Greater than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator >=( fcomplex A,  float B) {
            return (A.real >= B);
        }
        /// <summary>
        /// Lower than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <=( fcomplex A,  float B) {
            return (A.real <= B);
        }
       
        /// <summary>
        /// Add two complex numbers
        /// </summary>
        /// <param name="A">First summand</param>
        /// <param name="B">Second summand</param>
        /// <returns>Result</returns>
        public static  fcomplex operator +( fcomplex A,  byte B) {
            fcomplex ret;
            ret.real =  (float) (A.real + B);
            ret.imag =  (float) A.imag;
            return ret;
        }
        /// <summary>
        /// Subtract two values
        /// </summary>
        /// <param name="A">Minuend</param>
        /// <param name="B">Subtrahend</param>
        /// <returns>result</returns>
        public static  fcomplex operator -( fcomplex A,  byte B) {
            fcomplex ret;
            ret.real =  (float) (A.real - B);
            ret.imag =  (float) A.imag;
            return ret;
        }
        /// <summary>
        /// Multiply two values
        /// </summary>
        /// <param name="A">First factor</param>
        /// <param name="B">Second factor</param>
        /// <returns>result</returns>
        public static  fcomplex operator *( fcomplex A,  byte B) {
            fcomplex ret;
            ret.real =  (float) (A.real * B);
            ret.imag =  (float) (A.imag * B);
            return ret;
        }
        /// <summary>
        /// Divide two numbers
        /// </summary>
        /// <param name="A">Divident</param>
        /// <param name="B">Divisor</param>
        /// <returns>result</returns>
        public static  fcomplex operator /( fcomplex A,  byte B) {
            if (IsNaN(A)) return NaN;
            
            if (A.real == 0 && A.imag == 0) {
                if (B == 0) return NaN; 
                return ( fcomplex )0;
            } else {
                if (false)
                {
                    if (IsInfinity(A)) {
                        return NaN; 
                    } else {
                        return ( fcomplex )0;
                    }
                }
            }
            fcomplex ret;
            if (B == 0) return PositiveInfinity ;
            ret.real =  (float) (A.real / B);
            ret.imag =  (float) (A.imag / B);
            return ret;
        }
        /// <summary>
        /// Equality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>result</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator ==( fcomplex A,  byte B) {
            return (A.real == B && A.imag == 0.0);
        }
        /// <summary>
        /// Unequality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real and imaginary parts of A and B are not equal, false otherwise</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator !=( fcomplex A,  byte B) {
            return (A.imag != 0.0) || (A.real != B);
        }
        /// <summary>
        /// Freater than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator > ( fcomplex A,  byte B) {
            return (A.real > B);
        }
        /// <summary>
        /// Lower than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <(  fcomplex A,  byte B) {
            return (A.real < B);
        }
        /// <summary>
        /// Greater than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator >=( fcomplex A,  byte B) {
            return (A.real >= B);
        }
        /// <summary>
        /// Lower than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <=( fcomplex A,  byte B) {
            return (A.real <= B);
        }
       
        /// <summary>
        /// Add two complex numbers
        /// </summary>
        /// <param name="A">First summand</param>
        /// <param name="B">Second summand</param>
        /// <returns>Result</returns>
        public static  fcomplex operator +( fcomplex A,  double B) {
            fcomplex ret;
            ret.real =  (float) (A.real + B);
            ret.imag =  (float) A.imag;
            return ret;
        }
        /// <summary>
        /// Subtract two values
        /// </summary>
        /// <param name="A">Minuend</param>
        /// <param name="B">Subtrahend</param>
        /// <returns>result</returns>
        public static  fcomplex operator -( fcomplex A,  double B) {
            fcomplex ret;
            ret.real =  (float) (A.real - B);
            ret.imag =  (float) A.imag;
            return ret;
        }
        /// <summary>
        /// Multiply two values
        /// </summary>
        /// <param name="A">First factor</param>
        /// <param name="B">Second factor</param>
        /// <returns>result</returns>
        public static  fcomplex operator *( fcomplex A,  double B) {
            fcomplex ret;
            ret.real =  (float) (A.real * B);
            ret.imag =  (float) (A.imag * B);
            return ret;
        }
        /// <summary>
        /// Divide two numbers
        /// </summary>
        /// <param name="A">Divident</param>
        /// <param name="B">Divisor</param>
        /// <returns>result</returns>
        public static  fcomplex operator /( fcomplex A,  double B) {
            if (IsNaN(A)) return NaN;
            if (double.IsNaN(B)) return NaN;
            if (A.real == 0 && A.imag == 0) {
                if (B == 0) return NaN; 
                return ( fcomplex )0;
            } else {
                if (double.IsInfinity(B))
                {
                    if (IsInfinity(A)) {
                        return NaN; 
                    } else {
                        return ( fcomplex )0;
                    }
                }
            }
            fcomplex ret;
            if (B == 0) return PositiveInfinity ;
            ret.real =  (float) (A.real / B);
            ret.imag =  (float) (A.imag / B);
            return ret;
        }
        /// <summary>
        /// Equality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>result</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator ==( fcomplex A,  double B) {
            return (A.real == B && A.imag == 0.0);
        }
        /// <summary>
        /// Unequality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real and imaginary parts of A and B are not equal, false otherwise</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator !=( fcomplex A,  double B) {
            return (A.imag != 0.0) || (A.real != B);
        }
        /// <summary>
        /// Freater than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator > ( fcomplex A,  double B) {
            return (A.real > B);
        }
        /// <summary>
        /// Lower than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <(  fcomplex A,  double B) {
            return (A.real < B);
        }
        /// <summary>
        /// Greater than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator >=( fcomplex A,  double B) {
            return (A.real >= B);
        }
        /// <summary>
        /// Lower than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <=( fcomplex A,  double B) {
            return (A.real <= B);
        }

#endregion HYCALPER AUTO GENERATED CODE

        #region HYCALPER LOOPSTART OPERATORS_noncomplex+complex@Runtime\complex.cs
        /*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="after">
        Tret
    </source>
    <destination>fcomplex</destination>
    <destination>fcomplex</destination>
    <destination>fcomplex</destination>
    <destination>fcomplex</destination>
</type>
<type>
    <source locate="after">
        TinArr2 
    </source>
    <destination>fcomplex</destination>
    <destination>fcomplex</destination>
    <destination>fcomplex</destination>
    <destination>fcomplex</destination>
</type>
<type>
    <source locate="after">
        TinArr1
    </source>
    <destination>byte</destination>
    <destination>float</destination>
    <destination>Int32</destination>
    <destination>Int64</destination>
</type>
<type>
    <source locate="after" endmark=" ">
        FCast
    </source>
    <destination>(float)</destination>
    <destination>(float)</destination>
    <destination>(float)</destination>
    <destination>(float)</destination>
</type>
<type>
    <source locate="after">
        infinity
    </source>
    <destination>fcomplex.INF</destination>
    <destination>fcomplex.INF</destination>
    <destination>fcomplex.INF</destination>
    <destination>fcomplex.INF</destination>
</type>
</hycalper>
 */
        #endregion HYCALPER LOOPEND OPERATOR_noncomplex+complex@complex.cs
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       
        /// <summary>
        /// Add two complex values
        /// </summary>
        /// <param name="A">First summand</param>
        /// <param name="B">Second summand</param>
        /// <returns>Result</returns>
        public static  fcomplex operator +( Int64 A,  fcomplex B) {
            fcomplex ret; 
            ret.real =  (float) (A + B.real);
            ret.imag =  (float) B.imag;
            return ret;
        }
        /// <summary>
        /// Subtract two values
        /// </summary>
        /// <param name="A">Minuend</param>
        /// <param name="B">Subtrahend</param>
        /// <returns>Result</returns>
        public static  fcomplex operator -( Int64 A,  fcomplex B) {
            fcomplex ret;
            ret.real =  (float) (A - B.real);
            ret.imag = - (float) B.imag; 
            return ret;
        }
        /// <summary>
        /// Multiply two values
        /// </summary>
        /// <param name="A">First factor</param>
        /// <param name="B">Second factor</param>
        /// <returns>Result</returns>
        public static  fcomplex operator *( Int64 A,  fcomplex B) {
            fcomplex ret;
            ret.real =  (float) (A * B.real);
            ret.imag =  (float) (A * B.imag);
            return ret;
        }
        /// <summary>
        /// Divide two values
        /// </summary>
        /// <param name="A">Divident</param>
        /// <param name="B">Divisor</param>
        /// <returns>Result</returns>
        public static  fcomplex operator /( Int64 A,  fcomplex B) {
            fcomplex ret; 
            if (A == 0) {
                if (IsInfinity(B)) return NaN; 
            } else {
                if (IsInfinity(B)) return ( fcomplex )0; 
            }
            if (B.real == 0 && B.imag == 0) {
                return PositiveInfinity;
            }
            // this algorithm is taken from [1]. The one described in [2] was not taken. Tests 
            // did not show any advantage when using double precision floating point arithmetic.
            double tmp; 
            if (Math.Abs(B.real) >= Math.Abs(B.imag)) {
                tmp =  (float) (B.imag * (1/B.real)); 
                ret.imag =  (float) (B.real + B.imag*tmp); 
                ret.real =  (float) A/ret.imag; 
                ret.imag = -  (float) (A*tmp)/ret.imag; 
            } else {
                tmp =  (float) (B.real * (1/B.imag));
                ret.imag =  (float) (B.imag + B.real*tmp); 
                ret.real =  (float) (A*tmp)/ret.imag; 
                ret.imag = -  (float) A/ret.imag; 
            }
            return ret;
        }
        /// <summary>
        /// Equality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>Result</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator ==( Int64 A,  fcomplex B) {
            return (B.real == A && B.imag == 0.0);
        }
        /// <summary>
        /// Unequality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real and imaginary parts of A and B are not equal, false otherwise</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator !=( Int64 A,  fcomplex B) {
            return (B.imag != 0.0) || (B.real != A);
        }
        /// <summary>
        /// Greater than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator > ( Int64 A,  fcomplex B) {
            return (A > B.real);
        }
        /// <summary>
        /// Lower than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator < ( Int64 A,  fcomplex B) {
            return (A < B.real);
        }
        /// <summary>
        /// Greater than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator >=( Int64 A,  fcomplex B) {
            return (A >= B.real);
        }
        /// <summary>
        /// Lower than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <=( Int64 A,  fcomplex B) {
            return (A <= B.real);
        }
       
        /// <summary>
        /// Add two complex values
        /// </summary>
        /// <param name="A">First summand</param>
        /// <param name="B">Second summand</param>
        /// <returns>Result</returns>
        public static  fcomplex operator +( Int32 A,  fcomplex B) {
            fcomplex ret; 
            ret.real =  (float) (A + B.real);
            ret.imag =  (float) B.imag;
            return ret;
        }
        /// <summary>
        /// Subtract two values
        /// </summary>
        /// <param name="A">Minuend</param>
        /// <param name="B">Subtrahend</param>
        /// <returns>Result</returns>
        public static  fcomplex operator -( Int32 A,  fcomplex B) {
            fcomplex ret;
            ret.real =  (float) (A - B.real);
            ret.imag = - (float) B.imag; 
            return ret;
        }
        /// <summary>
        /// Multiply two values
        /// </summary>
        /// <param name="A">First factor</param>
        /// <param name="B">Second factor</param>
        /// <returns>Result</returns>
        public static  fcomplex operator *( Int32 A,  fcomplex B) {
            fcomplex ret;
            ret.real =  (float) (A * B.real);
            ret.imag =  (float) (A * B.imag);
            return ret;
        }
        /// <summary>
        /// Divide two values
        /// </summary>
        /// <param name="A">Divident</param>
        /// <param name="B">Divisor</param>
        /// <returns>Result</returns>
        public static  fcomplex operator /( Int32 A,  fcomplex B) {
            fcomplex ret; 
            if (A == 0) {
                if (IsInfinity(B)) return NaN; 
            } else {
                if (IsInfinity(B)) return ( fcomplex )0; 
            }
            if (B.real == 0 && B.imag == 0) {
                return PositiveInfinity;
            }
            // this algorithm is taken from [1]. The one described in [2] was not taken. Tests 
            // did not show any advantage when using double precision floating point arithmetic.
            double tmp; 
            if (Math.Abs(B.real) >= Math.Abs(B.imag)) {
                tmp =  (float) (B.imag * (1/B.real)); 
                ret.imag =  (float) (B.real + B.imag*tmp); 
                ret.real =  (float) A/ret.imag; 
                ret.imag = -  (float) (A*tmp)/ret.imag; 
            } else {
                tmp =  (float) (B.real * (1/B.imag));
                ret.imag =  (float) (B.imag + B.real*tmp); 
                ret.real =  (float) (A*tmp)/ret.imag; 
                ret.imag = -  (float) A/ret.imag; 
            }
            return ret;
        }
        /// <summary>
        /// Equality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>Result</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator ==( Int32 A,  fcomplex B) {
            return (B.real == A && B.imag == 0.0);
        }
        /// <summary>
        /// Unequality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real and imaginary parts of A and B are not equal, false otherwise</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator !=( Int32 A,  fcomplex B) {
            return (B.imag != 0.0) || (B.real != A);
        }
        /// <summary>
        /// Greater than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator > ( Int32 A,  fcomplex B) {
            return (A > B.real);
        }
        /// <summary>
        /// Lower than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator < ( Int32 A,  fcomplex B) {
            return (A < B.real);
        }
        /// <summary>
        /// Greater than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator >=( Int32 A,  fcomplex B) {
            return (A >= B.real);
        }
        /// <summary>
        /// Lower than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <=( Int32 A,  fcomplex B) {
            return (A <= B.real);
        }
       
        /// <summary>
        /// Add two complex values
        /// </summary>
        /// <param name="A">First summand</param>
        /// <param name="B">Second summand</param>
        /// <returns>Result</returns>
        public static  fcomplex operator +( float A,  fcomplex B) {
            fcomplex ret; 
            ret.real =  (float) (A + B.real);
            ret.imag =  (float) B.imag;
            return ret;
        }
        /// <summary>
        /// Subtract two values
        /// </summary>
        /// <param name="A">Minuend</param>
        /// <param name="B">Subtrahend</param>
        /// <returns>Result</returns>
        public static  fcomplex operator -( float A,  fcomplex B) {
            fcomplex ret;
            ret.real =  (float) (A - B.real);
            ret.imag = - (float) B.imag; 
            return ret;
        }
        /// <summary>
        /// Multiply two values
        /// </summary>
        /// <param name="A">First factor</param>
        /// <param name="B">Second factor</param>
        /// <returns>Result</returns>
        public static  fcomplex operator *( float A,  fcomplex B) {
            fcomplex ret;
            ret.real =  (float) (A * B.real);
            ret.imag =  (float) (A * B.imag);
            return ret;
        }
        /// <summary>
        /// Divide two values
        /// </summary>
        /// <param name="A">Divident</param>
        /// <param name="B">Divisor</param>
        /// <returns>Result</returns>
        public static  fcomplex operator /( float A,  fcomplex B) {
            fcomplex ret; 
            if (A == 0) {
                if (IsInfinity(B)) return NaN; 
            } else {
                if (IsInfinity(B)) return ( fcomplex )0; 
            }
            if (B.real == 0 && B.imag == 0) {
                return PositiveInfinity;
            }
            // this algorithm is taken from [1]. The one described in [2] was not taken. Tests 
            // did not show any advantage when using double precision floating point arithmetic.
            double tmp; 
            if (Math.Abs(B.real) >= Math.Abs(B.imag)) {
                tmp =  (float) (B.imag * (1/B.real)); 
                ret.imag =  (float) (B.real + B.imag*tmp); 
                ret.real =  (float) A/ret.imag; 
                ret.imag = -  (float) (A*tmp)/ret.imag; 
            } else {
                tmp =  (float) (B.real * (1/B.imag));
                ret.imag =  (float) (B.imag + B.real*tmp); 
                ret.real =  (float) (A*tmp)/ret.imag; 
                ret.imag = -  (float) A/ret.imag; 
            }
            return ret;
        }
        /// <summary>
        /// Equality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>Result</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator ==( float A,  fcomplex B) {
            return (B.real == A && B.imag == 0.0);
        }
        /// <summary>
        /// Unequality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real and imaginary parts of A and B are not equal, false otherwise</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator !=( float A,  fcomplex B) {
            return (B.imag != 0.0) || (B.real != A);
        }
        /// <summary>
        /// Greater than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator > ( float A,  fcomplex B) {
            return (A > B.real);
        }
        /// <summary>
        /// Lower than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator < ( float A,  fcomplex B) {
            return (A < B.real);
        }
        /// <summary>
        /// Greater than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator >=( float A,  fcomplex B) {
            return (A >= B.real);
        }
        /// <summary>
        /// Lower than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <=( float A,  fcomplex B) {
            return (A <= B.real);
        }
       
        /// <summary>
        /// Add two complex values
        /// </summary>
        /// <param name="A">First summand</param>
        /// <param name="B">Second summand</param>
        /// <returns>Result</returns>
        public static  fcomplex operator +( byte A,  fcomplex B) {
            fcomplex ret; 
            ret.real =  (float) (A + B.real);
            ret.imag =  (float) B.imag;
            return ret;
        }
        /// <summary>
        /// Subtract two values
        /// </summary>
        /// <param name="A">Minuend</param>
        /// <param name="B">Subtrahend</param>
        /// <returns>Result</returns>
        public static  fcomplex operator -( byte A,  fcomplex B) {
            fcomplex ret;
            ret.real =  (float) (A - B.real);
            ret.imag = - (float) B.imag; 
            return ret;
        }
        /// <summary>
        /// Multiply two values
        /// </summary>
        /// <param name="A">First factor</param>
        /// <param name="B">Second factor</param>
        /// <returns>Result</returns>
        public static  fcomplex operator *( byte A,  fcomplex B) {
            fcomplex ret;
            ret.real =  (float) (A * B.real);
            ret.imag =  (float) (A * B.imag);
            return ret;
        }
        /// <summary>
        /// Divide two values
        /// </summary>
        /// <param name="A">Divident</param>
        /// <param name="B">Divisor</param>
        /// <returns>Result</returns>
        public static  fcomplex operator /( byte A,  fcomplex B) {
            fcomplex ret; 
            if (A == 0) {
                if (IsInfinity(B)) return NaN; 
            } else {
                if (IsInfinity(B)) return ( fcomplex )0; 
            }
            if (B.real == 0 && B.imag == 0) {
                return PositiveInfinity;
            }
            // this algorithm is taken from [1]. The one described in [2] was not taken. Tests 
            // did not show any advantage when using double precision floating point arithmetic.
            double tmp; 
            if (Math.Abs(B.real) >= Math.Abs(B.imag)) {
                tmp =  (float) (B.imag * (1/B.real)); 
                ret.imag =  (float) (B.real + B.imag*tmp); 
                ret.real =  (float) A/ret.imag; 
                ret.imag = -  (float) (A*tmp)/ret.imag; 
            } else {
                tmp =  (float) (B.real * (1/B.imag));
                ret.imag =  (float) (B.imag + B.real*tmp); 
                ret.real =  (float) (A*tmp)/ret.imag; 
                ret.imag = -  (float) A/ret.imag; 
            }
            return ret;
        }
        /// <summary>
        /// Equality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>Result</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator ==( byte A,  fcomplex B) {
            return (B.real == A && B.imag == 0.0);
        }
        /// <summary>
        /// Unequality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real and imaginary parts of A and B are not equal, false otherwise</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator !=( byte A,  fcomplex B) {
            return (B.imag != 0.0) || (B.real != A);
        }
        /// <summary>
        /// Greater than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator > ( byte A,  fcomplex B) {
            return (A > B.real);
        }
        /// <summary>
        /// Lower than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator < ( byte A,  fcomplex B) {
            return (A < B.real);
        }
        /// <summary>
        /// Greater than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator >=( byte A,  fcomplex B) {
            return (A >= B.real);
        }
        /// <summary>
        /// Lower than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <=( byte A,  fcomplex B) {
            return (A <= B.real);
        }

#endregion HYCALPER AUTO GENERATED CODE

        #region unary minus
        /// <summary>
        /// Unary minus operator
        /// </summary>
        /// <param name="in1">fcomplex input</param>
        /// <returns>fcomplex number similar to in1, having real and imag part negated</returns>
        public static fcomplex operator -( fcomplex in1) {
            fcomplex ret = new fcomplex(); 
            ret.imag = -in1.imag; 
            ret.real = -in1.real; 
            return ret;
        }
        #endregion

        /// <summary>
        /// Magnitude value of float complex number
        /// </summary>
        /// <param name="input">fcomplex number</param>
        /// <returns>Magnitude of input</returns>
        public static float Abs(fcomplex input) {
            return (float)System.Numerics.Complex.Abs(new System.Numerics.Complex(input.real, input.imag));
            //return Complex.Abs(new Complex(input.real, input.imag)); 
            // return (float) Math.Sqrt ( input.real * input.real + input.imag * input.imag );  // using float makes underflow more likely in *
        }
        /// <summary>
        /// Angle of complex number
        /// </summary>
        /// <param name="input">fcomplex number to compute angle of</param>
        /// <returns>Angle of input</returns>
        public static double Angle(fcomplex input) {
            return (float) Math.Atan2 ( input.imag, input.real );
        }
        /// <summary>
        /// Arcus cosinus for float complex number
        /// </summary>
        /// <param name="input">fcomplex input</param>
        /// <returns>Arcus cosinus of input</returns>
        /// <remarks>The arcus cosinus of a complex number is computed by
        /// <para>Log(Sqrt(input^2 - 1) + input) * i </para></remarks>
        public static fcomplex Acos(fcomplex input) {
            fcomplex ret = new fcomplex ( 0, -1 );
            return fcomplex.Log ( fcomplex.Sqrt ( input * input - 1 )
                + input ) * ret;
        }
        /// <summary>
        /// Arcus cosinus of real number
        /// </summary>
        /// <param name="input">float input</param>
        /// <returns>Arcus cosinus of input</returns>
        /// <remarks>For input > 1.0, <see cref="ILNumerics.fcomplex.Acos(fcomplex)"/> will be used. </remarks>
        public static fcomplex Acos(float input) {
            if (Math.Abs(input) <= 1.0)
                return new fcomplex((float)Math.Acos(input), 0.0f);
            else {
                return Acos((fcomplex)input);
            }
        }
        /// <summary>
        /// Arcus sinus of real number
        /// </summary>
        /// <param name="input">float input</param>
        /// <returns>Arcus sinus of input</returns>
        /// <remarks>For input > 1.0, <see cref="ILNumerics.fcomplex.Asin(fcomplex)"/> will be used. </remarks>
        public static fcomplex Asin(float input) {
            if (Math.Abs(input) <= 1.0)
                return new fcomplex((float)Math.Asin(input), 0.0f);
            else {
                return Asin((fcomplex)input);
            }
        }
        /// <summary>
        /// Arcus sinus for complex number
        /// </summary>
        /// <param name="input">fcomplex input</param>
        /// <returns>Arcus sinus of input</returns>
        public static fcomplex Asin(fcomplex input) {
            fcomplex ret = Acos ( input );
            ret.real = (float) (Math.PI / 2 - ret.real);
            return ret; 
        }
        /// <summary>
        /// Power of base e for float complex number
        /// </summary>
        /// <param name="input">fcomplex input</param>
        /// <returns>Result of Exp(input)</returns>
        public static fcomplex Exp(fcomplex input) {
            return fcomplex.FromPol ( (float) Math.Exp ( input.real ), input.imag );
        }
        /// <summary>
        /// fcomplex power real exponent
        /// </summary>
        /// <param name="input">Basis </param>
        /// <param name="exponent">Exponent</param>
        /// <returns>New fcomplex number with result</returns>
        public static fcomplex Pow(fcomplex input, double exponent) {
            fcomplex ret = input.Log ();
            ret.imag *= (float) exponent;
            ret.real *= (float) exponent;
            return ret.Exp ();
        }
        /// <summary>
        /// Complex power - real basis, real exponent
        /// </summary>
        /// <param name="basis">Basis</param>
        /// <param name="exponent">Exponent</param>
        /// <returns>fcomplex number.</returns>
        /// <remarks>The result will be a fcomplex number. For negative basis 
        /// the basis will be converted to a fcomplex number and the power 
        /// will be computed in the fcomplex plane.</remarks>
        public static fcomplex Pow(double basis, double exponent) {
            if (basis < 0) {
                return Pow((fcomplex)basis, exponent);
            } else {
                return (fcomplex)Math.Pow(basis, exponent);
            }
        }
        /// <summary>
        /// Power: complex base, complex exponent
        /// </summary>
        /// <param name="basis">Basis</param>
        /// <param name="exponent">Exponent</param>
        /// <returns>result of basis^exponent</returns>
        public static fcomplex Pow(fcomplex basis, fcomplex exponent) {
            fcomplex ret = ( basis.Log () * exponent );
            return ret.Exp ();
        }
        /// <summary>
        /// Square root of real input
        /// </summary>
        /// <param name="input">float input</param>
        /// <returns>Square root of input</returns>
        public static fcomplex Sqrt(float input) {
            if (input > 0)
                return new fcomplex((float)Math.Sqrt(input), 0.0f);
            else
                return Sqrt((fcomplex)input); 
        }
        /// <summary>
        /// Square root of complex number
        /// </summary>
        /// <param name="input">fcomplex input</param>
        /// <returns>Square root of input</returns>
        public static fcomplex Sqrt(fcomplex input) {
            // Reference : numerical recipes in C: Appendix C
            fcomplex ret = new fcomplex ();
            double x, y, w, r;
            if (input.real == 0.0 && input.imag == 0.0)
                return ret;
            else {
                x = (float) Math.Abs ( input.real );
                y = (float) Math.Abs ( input.imag );
                if (x >= y) {
                    r = y / x;
                    w = Math.Sqrt ( x ) * Math.Sqrt ( 0.5 * ( 1.0 + Math.Sqrt ( 1.0 + r * r ) ) );
                } else {
                    r = x / y;
                    w = Math.Sqrt ( y ) * Math.Sqrt ( 0.5 * ( r + Math.Sqrt ( 1.0 + r * r ) ) );
                }
                if (input.real >= 0.0) {
                    ret.real = (float) w;
                    ret.imag = (float) (input.imag / ( 2.0 * w ));
                } else {
                    ret.imag = (float) (( input.imag >= 0 ) ? w : -w);
                    ret.real = (float) (input.imag / ( 2.0 * ret.imag ));
                }
                return ret;
            }
        }
        /// <summary>
        /// Tangens of float complex number
        /// </summary>
        /// <param name="input">fcomplex input</param>
        /// <returns>Tangens of input</returns>
        public static fcomplex Tan(fcomplex input) {
            fcomplex ci = Cos(input);
            if (ci.real == (float)0.0 && ci.imag == (float)0.0)
                return PositiveInfinity;
            return (Sin(input) / ci);
        }
        /// <summary>
        /// Tangens hyperbolicus of float complex input
        /// </summary>
        /// <param name="input">fcomplex input</param>
        /// <returns>Tangens hyperbolicus</returns>
        public static fcomplex Tanh(fcomplex input) {
            fcomplex si = Sin(input);
            if (si.real == (float)0.0 && si.imag == (float)0.0)
                return PositiveInfinity;
            return (Cos(input) / si);
        }
        /// <summary>
        /// Natural logarithm of complex input
        /// </summary>
        /// <param name="input">fcomplex input</param>
        /// <returns>Natural logarithm of input</returns>
        public static fcomplex Log(fcomplex input) {
            fcomplex ret = new fcomplex ();
            ret.real = (float) Math.Log ( Math.Sqrt ( input.real * input.real + input.imag * input.imag ) );
            ret.imag = (float) Math.Atan2 ( input.imag, input.real );
            return ret;
        }
        /// <summary>
        /// Logarithm to base 10
        /// </summary>
        /// <param name="input">fcomplex input</param>
        /// <returns>Logarithm of input</returns>
        public static fcomplex Log10(fcomplex input) {
            return Log(input) / 2.30258509299405f;
        }
        /// <summary>
        /// Logarithm of base 2
        /// </summary>
        /// <param name="input">fcomplex input</param>
        /// <returns>Logarithm of input</returns>
        public static fcomplex Log2(fcomplex input) {
            return Log(input) / 0.693147180559945f;
        }
        /// <summary>
        /// Logarithm of real input 
        /// </summary>
        /// <param name="input">float input - may be negative</param>
        /// <returns>Complex logarithm</returns>
        public static fcomplex Log(float input) {
            return Log (new fcomplex(input,0.0f)); 
        }
        /// <summary>
        /// Logarithm of base 10 of real input 
        /// </summary>
        /// <param name="input">float input - may be negative</param>
        /// <returns>Complex logarithm of base 10</returns>
        public static fcomplex Log10(float input) {
            return Log(new fcomplex(input,0.0f)) / 2.30258509299405f;
        }
        /// <summary>
        /// Logarithm of base 2
        /// </summary>
        /// <param name="input">float input - may be negative</param>
        /// <returns>Complex logarithm of base 2</returns>
        public static fcomplex Log2(float input) {
            return Log(new fcomplex(input,0.0f)) / 0.693147180559945f;
        }
        /// <summary>
        /// Convert from polar to cartesian form
        /// </summary>
        /// <param name="magnitude">Magnitude</param>
        /// <param name="angle">Angle</param>
        /// <returns>fcomplex number with magnitude <c>magnitude</c> 
        /// and phase <c>angle</c></returns>
        public static fcomplex FromPol(float magnitude, float angle) {
            return new fcomplex (
                (magnitude * (float)Math.Cos ( angle )),
                (magnitude * (float)Math.Sin ( angle ))
            );
        }
        
        /// <summary>
        /// Convert this complex number into a string representation.
        /// </summary>
        /// <returns>String displaying the complex number (full precision).</returns>
        public override String ToString() {
            return ILNumerics.F2NET.Helper.PrettyPrintNumber(1.0, this, 8, 0);
            if (imag >= 0) {
                return $"{real}+{(double.IsInfinity(imag) ? " " : "i")}{imag}";
            } else {
                return $"{real}-{((double.IsInfinity(imag) || double.IsNaN(imag)) ? " " : "i")}{-imag}";
            }
        }

        /// <summary>
        /// Magnitude of this float complex number
        /// </summary>
        /// <returns>Magnitude</returns>
        public float Abs() {
            return (float)(new System.Numerics.Complex(real, imag).Magnitude);
            //return (float)Math.Sqrt(real * real + imag * imag);
        }
        /// <summary>
        /// Phase angle of this float complex number
        /// </summary>
        /// <returns>Phase angle </returns>
        public double Angle() {
            return (float)Math.Atan2(imag, real);
        }
        /// <summary>
        /// Arcus cosinus of this float complex number
        /// </summary>
        /// <returns>Arcus cosinus</returns>
        public fcomplex Acos() {
            fcomplex ret = new fcomplex(0, -1);
            return fcomplex.Log(fcomplex.Sqrt(this * this - 1)
                + this) * ret;
        }
        /// <summary>
        /// Arcus sinus of this float complex number
        /// </summary>
        /// <returns>Arcus sinus</returns>
        public fcomplex Asin() {
            fcomplex ret = Acos(this);
            ret.real = (float)(Math.PI / 2 - ret.real);
            return ret;
        }
        /// <summary>
        /// Arcus tangens of float complex number
        /// </summary>
        /// <param name="input">fcomplex input</param>
        /// <returns>Arcus tangens of input</returns>
        public static fcomplex Atan(fcomplex input) {
            fcomplex ret = new fcomplex(0, (float)0.5);
            return (ret * Log((fcomplex.i + input) / (fcomplex.i - input)));
        }
        /// <summary>
        /// Round towards next greater integer
        /// </summary>
        /// <param name="input">fcomplex input</param>
        /// <returns>Rounded float complex number</returns>
        /// <remarks>Real and imaginary parts are independently rounded 
        /// towards the next integer value towards positive infinity.</remarks>
        public static fcomplex Ceiling (fcomplex input){
            return new fcomplex(
                    (float)Math.Ceiling(input.real),
                    (float)Math.Ceiling(input.imag)
            );
        }
        /// <summary>
        /// Round towards next lower integer
        /// </summary>
        /// <param name="input">fcomplex input</param>
        /// <returns>Rounded float complex number</returns>
        /// <remarks>Real and imaginary parts are independently rounded 
        /// towards the next integer value towards negative infinity.</remarks>
        public static fcomplex Floor (fcomplex input){
            return new fcomplex(
                    (float)Math.Floor(input.real),
                    (float)Math.Floor(input.imag)
            );
        }
        /// <summary>
        /// Rounds towards nearest integer.
        /// </summary>
        /// <param name="a">Input value.</param>
        /// <returns>The value of <paramref name="a"/>, rounded to the nearest integer.</returns>
        /// <remarks><para>Rounding is performed for <see cref="real"/> and <see cref="imag"/> 
        /// separately.</para>
        /// <para>Numbers halfway between two others (0.5, 1.5,...) are rounded towards 
        /// the next even number. See: <see cref="System.MidpointRounding.ToEven"/>.</para></remarks>
        /// <see cref="Math.Round(double)"/>
        /// <see cref="complex.Round(complex)"/>
        /// <see cref="complex.Round(complex,int)"/>
        public static fcomplex Round(fcomplex a) {
            return new fcomplex(
                    (float)Math.Round(a.real),
                    (float)Math.Round(a.imag)
            );
        }
        /// <summary>
        /// Rounds to a specified number of fractional digits.
        /// </summary>
        /// <param name="a">Input value.</param>
        /// <param name="digits">Number of fractional digits to round to.</param>
        /// <returns>The value of <paramref name="a"/>, rounded to the 
        /// specified number of fractional digits.</returns>
        /// <remarks><para>Rounding is performed for <see cref="real"/> and <see cref="imag"/> 
        /// separately.</para>
        /// <para>Numbers halfway between two others (0.5, 1.5,...) are rounded towards 
        /// the next even number. See: <see cref="System.MidpointRounding.ToEven"/>.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">if digits is negative.</exception>
        public static fcomplex Round(fcomplex a, int digits) {
            return new fcomplex(
                    (float)Math.Round(a.real, digits),
                    (float)Math.Round(a.imag, digits)
            );
        }
        /// <summary>
        /// Signum function
        /// </summary>
        /// <param name="input">fcomplex input</param>
        /// <returns> Signum of input</returns>
        /// <remarks>
        /// For numbers a = 0.0 + 0.0i, sign(a)'s real and imag parts are 0.0. 
        /// For all other numbers sign(a) is the projection onto the unit circle.</remarks>
        public static fcomplex Sign(fcomplex input){
            if (input.real == 0.0 && input.imag == 0.0)
                return new fcomplex(); 
            else {
                float mag = (float)Math.Sqrt(input.real * input.real + input.imag * input.imag); 
                return new fcomplex(
                    input.real / mag,
                    input.imag / mag);
            }
        }
        /// <summary>
        /// Truncate a floating point complex value
        /// </summary>
        /// <param name="input">fcomplex input</param>
        /// <returns>Integer part of input</returns>
        /// <remarks>Operates on real and imaginary parts separately.</remarks>
        public static fcomplex Truncate (fcomplex input){
            return new fcomplex(
                    (float)Math.Truncate(input.real),
                    (float)Math.Truncate(input.imag)
            );
        }
        /// <summary>
        /// Creates the complex conjugate of the complex value <paramref name="a"/>.
        /// </summary>
        /// <param name="a">The complex value.</param>
        /// <returns>The conjugate of a.</returns>
        /// <seealso cref="conj"/>
        /// <seealso cref="Conjugate()"/>
        public static fcomplex Conjugate(fcomplex a) {
            return new fcomplex(a.real, -a.imag);
        }

        /// <summary>
        /// Cosinus
        /// </summary>
        /// <param name="input">fcomplex input</param>
        /// <returns>Cosinus of input</returns>
        /// <remarks><para>The cosinus is computed by the trigonometric euler equation: </para>
        /// <para>0.5 * [exp(i input) + exp(-i input)]</para></remarks>
        public static fcomplex Cos(fcomplex input) {
            fcomplex i = new fcomplex(0, 1.0f);
            fcomplex ni = new fcomplex(0, -1.0f);
            return (Exp(i * input) + Exp(ni * input)) / 2.0f;
        }
        /// <summary>
        /// Cosinus hyperbolicus
        /// </summary>
        /// <param name="input">fcomplex input</param>
        /// <returns>Cosinus hyperbolicus of input</returns>
        /// <remarks><para>The cosinus is computed by the trigonometric euler equation: </para>
        /// <para>(Exp(input) + Exp(-1.0 * input)) / 2.0</para></remarks>
        public static fcomplex Cosh(fcomplex input) {
            return (Exp(input) + Exp(-1.0f * input)) / 2.0f;
        }
        /// <summary>
        /// Sinus
        /// </summary>
        /// <param name="input">fcomplex input</param>
        /// <returns>Sinus of input</returns>
        /// <remarks><para>The sinus is computed by the trigonometric euler equation: </para>
        /// <para>(Exp(i * input) - Exp(-1.0 * i * input)) / (2.0 * i)</para></remarks>
        public static fcomplex Sin(fcomplex input) {
            fcomplex i = new fcomplex(0, (float)1.0);
            fcomplex mi = new fcomplex(0, (float)-1.0);
            return (Exp(i * input) - Exp(mi * input)) / (2.0 * i);
        }
        /// <summary>
        /// Sinus hyperbolicus
        /// </summary>
        /// <param name="input">fcomplex input</param>
        /// <returns>Sinus hyperbolicus of input</returns>
        /// <remarks><para>The sinus hyperbolicus is computed by the trigonometric euler equation: </para>
        /// <para>(Exp(input) - Exp(-1.0 * input)) / 2.0</para></remarks>
        public static fcomplex Sinh(fcomplex input) {
            fcomplex ret = new fcomplex(0, 2);
            fcomplex i = new fcomplex(0, (float)1.0);
            fcomplex mi = new fcomplex(0, (float)-1.0);
            return (Exp(input) - Exp(-1.0 * input)) / 2.0;
        }
        /// <summary>
        /// Exponential / power of base e
        /// </summary>
        /// <returns>Power of base e</returns>
        public fcomplex Exp() {
            return fcomplex.FromPol((float)Math.Exp(real), imag);
        }
        /// <summary>
        /// Power of fcomplex number, real exponent
        /// </summary>
        /// <param name="exponent">Exponent</param>
        /// <returns>New fcomplex number with result</returns>
        public fcomplex Pow(double exponent) {
            fcomplex ret = Log();
            ret.imag *= (float)exponent;
            ret.real *= (float)exponent;
            return ret.Exp();
        }
        /// <summary>
        /// Power of fcomplex number, complex exponent
        /// </summary>
        /// <param name="exponent">Exponent</param>
        /// <returns>New fcomplex number with result</returns>
        public fcomplex Pow(fcomplex exponent) {
            fcomplex ret = (Log() * exponent);
            return ret.Exp();
        }
        /// <summary>
        /// Square root of fcomplex number
        /// </summary>
        /// <returns>Square root</returns>
        public fcomplex Sqrt() {
            // Reference : numerical recipes in C: Appendix C
            fcomplex ret = new fcomplex();
            double x, y, w, r;
            if ( real == 0.0 && imag == 0.0)
                return ret;
            else {
                x = (float)Math.Abs(real);
                y = (float)Math.Abs( imag);
                if (x >= y) {
                    r = y / x;
                    w = Math.Sqrt(x) * Math.Sqrt(0.5 * (1.0 + Math.Sqrt(1.0 + r * r)));
                } else {
                    r = x / y;
                    w = Math.Sqrt(y) * Math.Sqrt(0.5 * (r + Math.Sqrt(1.0 + r * r)));
                }
                if ( real >= 0.0) {
                    ret.real = (float)w;
                    ret.imag = (float)( imag / (2.0 * w));
                } else {
                    ret.imag = (float)(( imag >= 0) ? w : -w);
                    ret.real = (float)( imag / (2.0 * ret.imag));
                }
                return ret;
            }
        }
        /// <summary>
        /// Logarithm of fcomplex number
        /// </summary>
        /// <returns>Natural logarithm</returns>
        /// <remarks>The logarithm of a complex number A is defined as follows: <br />
        /// <list type="none"><item>real part: log(abs(A))</item>
        /// <item>imag part: Atan2(imag(A),real(A))</item></list>
        /// </remarks>
        public fcomplex Log() {
            fcomplex ret = new fcomplex();
            ret.real = (float)Math.Log(Math.Sqrt( real *  real +  imag *  imag));
            ret.imag = (float)Math.Atan2( imag,  real);
            return ret;
        }
        /// <summary>
        /// Test if any of real or imaginary parts are NAN's
        /// </summary>
        /// <param name="input">fcomplex input</param>
        /// <returns>true if any of real or imag part is not a number</returns>
        public static bool IsNaN(fcomplex input) {
            if (Single.IsNaN(input.real) || Single.IsNaN(input.imag)) 
                return true; 
            else 
                return false; 
        }
        /// <summary>
        /// Test if any of real or imaginary parts are infinite
        /// </summary>
        /// <param name="input">fcomplex input</param>
        /// <returns>true if any of real or imag part is infinite</returns>
        public static bool IsInfinity(fcomplex input) {  
            if (Single.IsInfinity(input.real) || Single.IsInfinity(input.imag)) 
                return true; 
            else 
                return false; 
        }
        /// <summary>
        /// Test if any of real or imaginary parts are pos. infinite
        /// </summary>
        /// <param name="input">fcomplex input</param>
        /// <returns>true if any of real or imag part is positive infinite</returns>
        public static bool IsPositiveInfinity(fcomplex input) {  
            if (Single.IsPositiveInfinity(input.real) || Single.IsPositiveInfinity(input.imag)) 
                return true; 
            else 
                return false; 
        }
        /// <summary>
        /// Test if any of real or imaginary parts are neg. infinite
        /// </summary>
        /// <param name="input">fcomplex input</param>
        /// <returns>true if any of real or imag part is negative infinite</returns>
        public static bool IsNegativeInfinity(fcomplex input) {  
            if (Single.IsNegativeInfinity(input.real) || Single.IsNegativeInfinity(input.imag)) 
                return true; 
            else 
                return false; 
        }
        /// <summary>
        /// Test if any of real or imaginary parts are finite
        /// </summary>
        /// <param name="input">fcomplex input</param>
        /// <returns>true if any of real and imag part is finite</returns>
        public static bool IsFinite (fcomplex input) {
            if (!fcomplex.IsInfinity(input) && !fcomplex.IsNaN(input))
                return true;
            else
                return false;
        }

        #region CAST_OPERATORS
        /// <summary>
        /// Implicit cast real number into complex number
        /// </summary>
        /// <param name="a">double</param>
        /// <returns>fcomplex number with real part equals a</returns>
        public static implicit operator fcomplex(double a) {
            return new fcomplex((float)a, 0.0F);
        }
        /// <summary>
        /// Implicit cast real number into complex number
        /// </summary>
        /// <param name="a">float</param>
        /// <returns>fcomplex number with real part equals a</returns>
        public static implicit operator fcomplex(float a) {
            return new fcomplex(a, 0.0F);
        }
        /// <summary>
        /// Implicit cast real number into complex number
        /// </summary>
        /// <param name="a">byte</param>
        /// <returns>fcomplex number with real part equals a</returns>
        public static implicit operator fcomplex(byte a) {
            return new fcomplex(a, 0.0F);
        }
        /// <summary>
        /// Implicit cast real number into complex number
        /// </summary>
        /// <param name="a">byte</param>
        /// <returns>fcomplex number with real part equals a</returns>
        public static implicit operator fcomplex(sbyte a) {
            return new fcomplex(a, 0.0F);
        }
        /// <summary>
        /// Implicit cast real number into complex number
        /// </summary>
        /// <param name="a">char</param>
        /// <returns>fcomplex number with real part equals a</returns>
        public static implicit operator fcomplex(char a) {
            return new fcomplex(a, 0.0F);
        }
        /// <summary>
        /// Implicit cast real number into complex number
        /// </summary>
        /// <param name="a">Int16</param>
        /// <returns>fcomplex number with real part equals a</returns>
        public static implicit operator fcomplex(Int16 a) {
            return new fcomplex(a, 0.0F);
        }
        /// <summary>
        /// Implicit cast real number into complex number
        /// </summary>
        /// <param name="a">Int32</param>
        /// <returns>fcomplex number with real part equals a</returns>
        public static implicit operator fcomplex(Int32 a) {
            return new fcomplex((float)a, 0.0F);    
        }
        /// <summary>
        /// Implicit cast real number into complex number
        /// </summary>
        /// <param name="a">Int64</param>
        /// <returns>fcomplex number with real part equals a</returns>
        public static implicit operator fcomplex(Int64 a) {
            return new fcomplex((float)a, 0.0F);
        }
        /// <summary>
        /// Implicit cast real number into complex number
        /// </summary>
        /// <param name="a">UInt16</param>
        /// <returns>fcomplex number with real part equals a</returns>
        public static implicit operator fcomplex(UInt16 a) {
            return new fcomplex((float)a, 0.0F);
        }
        /// <summary>
        /// Implicit cast real number into complex number
        /// </summary>
        /// <param name="a">UInt32</param>
        /// <returns>fcomplex number with real part equals a</returns>
        public static implicit operator fcomplex(UInt32 a) {
            return new fcomplex((float)a, 0.0F);
        }
        /// <summary>
        /// Implicit cast real number into complex number
        /// </summary>
        /// <param name="a">UInt64</param>
        /// <returns>fcomplex number with real part equals a</returns>
        public static implicit operator fcomplex(UInt64 a) {
            return new fcomplex((float)a, 0.0F);
        }

        /// <summary>
        /// Explicit cast complex number into real number
        /// </summary>
        /// <param name="a">fcomplex number</param>
        /// <returns>Real number with real part of a</returns>
        public static explicit operator double(fcomplex a) {
            return a.real; 
        }
        /// <summary>
        /// Explicit cast complex number into real number
        /// </summary>
        /// <param name="a">fcomplex number</param>
        /// <returns>Real number with real part of a</returns>
        public static explicit operator float(fcomplex a) {
            return (float)a.real;
        }
        /// <summary>
        /// Explicit cast complex number into real number
        /// </summary>
        /// <param name="a">fcomplex number</param>
        /// <returns>Real number with real part of a</returns>
        public static explicit operator byte(fcomplex a) {
            return (byte) a.real; 
        }
        /// <summary>
        /// Explicit cast complex number into real number
        /// </summary>
        /// <param name="a">fcomplex number</param>
        /// <returns>Real number with real part of a</returns>
        public static explicit operator sbyte(fcomplex a) {
            return (sbyte) a.real; 
        }
        /// <summary>
        /// Explicit cast complex number into real number
        /// </summary>
        /// <param name="a">fcomplex number</param>
        /// <returns>Real number with real part of a</returns>
        public static explicit operator char(fcomplex a) {
            return (char) a.real; 
        }
        /// <summary>
        /// Explicit cast complex number into real number
        /// </summary>
        /// <param name="a">fcomplex number</param>
        /// <returns>Real number with real part of a</returns>
        public static explicit operator Int16(fcomplex a) {
            return (Int16) a.real; 
        }
        /// <summary>
        /// Explicit cast complex number into real number
        /// </summary>
        /// <param name="a">complex number</param>
        /// <returns>Real number with real part of a</returns>
        public static explicit operator Int32(fcomplex a) {
            return (Int32) a.real; 
        }
        /// <summary>
        /// Explicit cast complex number into real number
        /// </summary>
        /// <param name="a">fcomplex number</param>
        /// <returns>Real number with real part of a</returns>
        public static explicit operator Int64(fcomplex a) {
            return (Int64) a.real; 
        }
        /// <summary>
        /// Explicit cast complex number into real number
        /// </summary>
        /// <param name="a">fcomplex number</param>
        /// <returns>Real number with real part of a</returns>
        public static explicit operator UInt16(fcomplex a) {
            return (UInt16) a.real; 
        }
        /// <summary>
        /// Explicit cast complex number into real number
        /// </summary>
        /// <param name="a">fcomplex number</param>
        /// <returns>Real number with real part of a</returns>
        public static explicit operator UInt32(fcomplex a) {
            return (UInt32) a.real; 
        }
        /// <summary>
        /// Explicit cast complex number into real number
        /// </summary>
        /// <param name="a">fcomplex number</param>
        /// <returns>Real number with real part of a</returns>
        public static explicit operator UInt64(fcomplex a) {
            return (UInt64) a.real; 
        }
        /// <summary>
        /// Test if real and imag part are zero
        /// </summary>
        /// <returns>true if real and imag parts are zero, false else</returns>
        public bool iszero() {
            if (real == 0.0f && imag == 0.0f) 
                return true; 
            else 
                return false; 
        }
        #endregion CAST_OPERATORS

        #region Parse
        /// <summary>
        /// Converts the string <paramref name="text"/> containing a complex number into the complex number.
        /// </summary>
        /// <param name="text">String representation of the complex number.</param>
        /// <param name="style">Number style used for parsing individual parts (real, imag).</param>
        /// <param name="culture">The culture determining separators and floating point formats.</param>
        /// <returns>The complex number.</returns>
        /// <exception cref="FormatException">if the string is in an invalid format and/or cannot be converted.</exception>
        public static fcomplex Parse(string text, NumberStyles style, CultureInfo culture) {
            string real, imag;
            if (!complex.Partition(text, out real, out imag)) {
                throw new FormatException($"The string '{text}' is not a valid complex number.");
            }
            return new fcomplex(float.Parse(real, style, culture), float.Parse(imag, style, culture));
        }
        #endregion

    }

}
