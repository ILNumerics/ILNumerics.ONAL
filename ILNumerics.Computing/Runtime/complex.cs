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
using System.Globalization;
using System.Numerics;
using System.Runtime.InteropServices;    
using System.Text;     
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace ILNumerics {
    /// <summary>
    /// Floating point complex value data type of double precision.
    /// </summary>
    /// <remarks>This class extends the system value types for real numbers to complex double 
    /// values. Besides the publicly available members 'real' and 'imag' it provides all the 
    /// basic functionality floating point type system.double brings (abs, log, sqrt, tan etc.).
    /// Further it overrides basic unary and binary operators for all common system value 
    /// types including rarely used types (e.g. UInt16). This includes the basic numerical operations 
    /// like '+','-','/','*' and the relational operators: '==','>','>=' etc. Also, some 
    /// explicit and some implicit casting operators from / to complex value into system 
    /// value types exist.</remarks>
    [Serializable] 
    [StructLayout(LayoutKind.Sequential)]
    public struct complex : IEquatable<complex>
#if NET8_0_OR_GREATER
        , INumberBase<complex>
        , IAdditionOperators<complex, complex, complex>
        , ISubtractionOperators<complex,complex, complex>
        , IDivisionOperators<complex, complex, complex>
        , IMultiplyOperators<complex, complex, complex>
        , ISignedNumber<complex>
#endif
        {
        /// <summary>
        /// Real part of this complex number
        /// </summary>
        public double real;
        /// <summary>
        /// Imaginary part of this complex number
        /// </summary>
        public double imag;
        /// <summary>
        /// Imaginary unit 
        /// </summary>
        public static readonly complex i = new complex(0.0f,1.0f);


        /// <summary>
        /// Constructor creating a new complex value
        /// </summary>
        /// <param name="real">Real part</param>
        /// <param name="imag">Imaginary part</param>
        public complex(double real, double imag) {
            this.real = real; 
            this.imag = imag; 
        }

        /// <summary>
        /// Returns the complex conjugate of this complex number.
        /// </summary>
        /// <seealso cref="Conjugate(complex)"/>
        /// <seealso cref="Conjugate()"/>
        public complex conj {
            get{
                return new complex(real,-imag);
            }
        }

        /// <summary>
        /// Positive infinity for real and imag part of complex value
        /// </summary>
        [Obsolete("Use complex.PositiveInfinity instead.")]
        public static complex INF {
            get {
                return new complex(
                    double.PositiveInfinity,
                    double.PositiveInfinity
                );
            }
        }
        /// <summary>
        /// Positive infinity for real and imag part of complex value
        /// </summary>
        public static complex PositiveInfinity {
            get {
                return new complex(
                    double.PositiveInfinity,
                    double.PositiveInfinity
                );
            }
        }

        /// <summary>
        /// New complex, real and imaginary parts are zero
        /// </summary>
        public static complex Zero {
            get {
                return new complex(0.0,0.0);
            }
        }

        /// <summary>
        /// New complex number (1.0, 0.0)
        /// </summary>
        public static complex One {
            get {
                return new complex(1.0, 0.0);
            }
        }


        /// <summary>
        /// Complex quantity, marked as being "not a number"
        /// </summary>
        public static complex NaN {
            get {
                return new complex(double.NaN,double.NaN); 
            }
        }

#if NET8_0_OR_GREATER

        static int INumberBase<complex>.Radix => 2;

        static complex IAdditiveIdentity<complex, complex>.AdditiveIdentity => Zero;

        static complex IMultiplicativeIdentity<complex, complex>.MultiplicativeIdentity => One;

        static complex ISignedNumber<complex>.NegativeOne => new complex(-1.0, 0.0);

#endif

        /// <summary>
        /// Are obj's real and imaginary part identical to the real and imaginary parts of this fcomplex
        /// </summary>
        /// <param name="obj">fcomplex object to determine the equality for</param>
        /// <returns>true if obj is of fcomplex type and its real and imag part has the same 
        /// values as the real and imaginary part of this array.</returns>
        public override bool Equals(object obj) {
            if (object.Equals(obj, null)) return false; 
            if (obj.GetType() == typeof(complex) && ((complex)obj) == this)
                return true; 
            return false; 
        }
        /// <summary>
        /// Check if a complex number equals this complex number
        /// </summary>
        /// <param name="other">other complex number</param>
        /// <returns>true if both, real and imaginary parts of both complex number are (binary) equal, false otherwise</returns>
        public bool Equals(complex other) {
            return real.Equals(other.real) && imag.Equals(other.imag); 
        }
        /// <summary>
        /// Hash code of this comples
        /// </summary>
        /// <returns>Hash code of this complex</returns>
        public override int GetHashCode() {
            return 77101 * real.GetHashCode() + imag.GetHashCode();
        }
        /// <summary>
        /// Conjugates this complex number inplace.
        /// </summary>
        /// <seealso cref="conj"/>
        /// <seealso cref="Conjugate(complex)"/>
        public void Conjugate() {
            imag = -imag; 
        }

        #region HYCALPER LOOPSTART OPERATORS_complex+complex
        /*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="after">
        Tret
    </source>
    <destination>complex</destination>
</type>
<type>
    <source locate="after">
        TinArr1 
    </source>
    <destination>complex</destination>
</type>
<type>
    <source locate="after">
        TinArr2
    </source>
    <destination>fcomplex</destination>
</type>
<type>
    <source locate="after" endmark=" ">
        FCast
    </source>
    <destination>(double)</destination>
</type>
<type>
    <source locate="after">
        infinity
    </source>
    <destination>complex.PositiveInfinity</destination>
</type>
<type>
    <source locate="after">
        TRret
    </source>
    <destination>double</destination>
</type>
</hycalper>
 */
        /// <summary>
        /// Add two complex numbers
        /// </summary>
        /// <param name="A">First summand</param>
        /// <param name="B">Second summand</param>
        /// <returns>result</returns>
        public static /*!HC:Tret*/ complex operator +(/*!HC:TinArr1*/ complex A, /*!HC:TinArr2*/ complex B) {
            /*!HC:Tret*/ complex ret; 
             ret.real =  /*!HC:FCast*/ (double) (A.real + B.real );
             ret.imag =  /*!HC:FCast*/ (double) (A.imag + B.imag );
            return ret;
        }
        /// <summary>
        /// Subtract two complex values
        /// </summary>
        /// <param name="A">Minuend</param>
        /// <param name="B">Subtrahend</param>
        /// <returns>result</returns>
        public static /*!HC:Tret*/ complex operator -(/*!HC:TinArr1*/ complex A, /*!HC:TinArr2*/ complex B) {
            /*!HC:Tret*/ complex ret; 
            ret.real = /*!HC:FCast*/ (double) (A.real  - B.real );
            ret.imag = /*!HC:FCast*/ (double) (A.imag - B.imag );
            return ret;
        }
        /// <summary>
        /// Multiply two complex values
        /// </summary>
        /// <param name="A">First factor</param>
        /// <param name="B">Second factor</param>
        /// <returns>result</returns>
        public static /*!HC:Tret*/ complex operator *(/*!HC:TinArr1*/ complex A, /*!HC:TinArr2*/ complex B) {
            /*!HC:Tret*/ complex ret;
            ret.real = /*!HC:FCast*/ (double) ((A.real * B.real ) - (A.imag * B.imag ));
            ret.imag = /*!HC:FCast*/ (double) ((A.real * B.imag ) + (A.imag * B.real ));
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
        public static /*!HC:Tret*/ complex operator /(/*!HC:TinArr1*/ complex A, /*!HC:TinArr2*/ complex B) {
            if (B.imag == 0) return A / B.real; 
            //return A * (1 / B); 
            if (IsNaN(A) || /*!HC:TinArr2*/ complex .IsNaN(B)) return NaN;
            //if (/*!HC:TinArr2*/ complex .IsInfinity(B)) return NaN;            
            //if (A.real == 0 && A.imag == 0) return (/*!HC:Tret*/ complex )0;
            /*!HC:Tret*/ complex ret;
            if (B.real == 0) {
                ret.imag = /*!HC:FCast*/ (double) -(A.real / B.imag); 
                ret.real = /*!HC:FCast*/ (double) (A.imag / B.imag); 
                return ret; 
            }
            // This would be the naive approach. But it comes with small robustness against overflow. This is used in legacy compilers (ifort) when x87 registers are available (80 bit). 
#if !ROBUST_COMPLEX_DIVISION
            //if (norm2 == 0) return PositiveInfinity;    // this may be removed, since 1) B is checked above and division by 0 results in inf anyway ? 
            
            //ret.real = /*!HC:FCast*/ (double)(((A.real * B.real) + (A.imag * B.imag)) / norm2);
            //ret.imag = /*!HC:FCast*/ (double)(((A.imag * B.real) - (A.real * B.imag)) / norm2);
            //return ret; 
            
            // alternative scaling. Better compatible with ifort ?! 
            //A = A / norm2;
            //B = B / norm2; 

            // CAUTION!!! this is (exactly) what ifort does! Don't change without accepting that we would loose byte compatibility!!! 
            // CAUTION 2! Below code is prone to over / underflow, obviously. But this is what a plain, simple complex divide does. See ZLADIV for a more robust version. 
            double norm2 = 1.0 / (B.real * B.real + B.imag * B.imag);
            ret.real = /*!HC:FCast*/ (double)((A.real * B.real) + (A.imag * B.imag)) * norm2;
            ret.imag = /*!HC:FCast*/ (double)((A.imag * B.real) - (A.real * B.imag)) * norm2;
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
                return new /*!HC:Tret*/ complex((/*!HC:TRret*/ double) ((a + b * r) / den), (/*!HC:TRret*/ double)((b - a * r) / den));
            } else {
                double r = c / d;
                double den = c * r + d;
                return new /*!HC:Tret*/ complex((/*!HC:TRret*/ double)((a * r + b) / den), (/*!HC:TRret*/ double)((b * r - a) / den));
            }
#endif
        }
        /// <summary>
        /// Equality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true, if real and imaginary part are identical</returns>
        public static bool operator ==(/*!HC:TinArr1*/ complex A, /*!HC:TinArr2*/ complex B) {
            return (A.imag  == B.imag ) && (A.real  == B.real );
        }
        /// <summary>
        /// Unequality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real and imaginary parts of A and B are not equal, false otherwise</returns>
        public static bool operator !=(/*!HC:TinArr1*/ complex A, /*!HC:TinArr2*/ complex B) {
            return (A.imag  != B.imag ) || (A.real  != B.real );
        }
        /// <summary>
        /// Greater than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator > (/*!HC:TinArr1*/ complex A, /*!HC:TinArr2*/ complex B) {
            return (A.real > B.real );
        }
        /// <summary>
        /// Lower than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator < (/*!HC:TinArr1*/ complex A, /*!HC:TinArr2*/ complex B) {
            return (A.real < B.real );
        }
        /// <summary>
        /// Greater than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator >=(/*!HC:TinArr1*/ complex A, /*!HC:TinArr2*/ complex B) {
            return (A.real >= B.real );
        }
        /// <summary>
        /// Lower than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <=(/*!HC:TinArr1*/ complex A, /*!HC:TinArr2*/ complex B) { 
            return (A.real <= B.real );
        }
#endregion HYCALPER LOOPEND OPERATORS_complex+complex
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       
        /// <summary>
        /// Add two complex numbers
        /// </summary>
        /// <param name="A">First summand</param>
        /// <param name="B">Second summand</param>
        /// <returns>result</returns>
        public static  complex operator +( complex A,  fcomplex B) {
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
        public static  complex operator -( complex A,  fcomplex B) {
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
        public static  complex operator *( complex A,  fcomplex B) {
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
        public static  complex operator /( complex A,  fcomplex B) {
            if (B.imag == 0) return A / B.real; 
            //return A * (1 / B); 
            if (IsNaN(A) ||  fcomplex .IsNaN(B)) return NaN;
            //if ( fcomplex .IsInfinity(B)) return NaN;            
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
        public static bool operator ==( complex A,  fcomplex B) {
            return (A.imag  == B.imag ) && (A.real  == B.real );
        }
        /// <summary>
        /// Unequality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real and imaginary parts of A and B are not equal, false otherwise</returns>
        public static bool operator !=( complex A,  fcomplex B) {
            return (A.imag  != B.imag ) || (A.real  != B.real );
        }
        /// <summary>
        /// Greater than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator > ( complex A,  fcomplex B) {
            return (A.real > B.real );
        }
        /// <summary>
        /// Lower than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator < ( complex A,  fcomplex B) {
            return (A.real < B.real );
        }
        /// <summary>
        /// Greater than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator >=( complex A,  fcomplex B) {
            return (A.real >= B.real );
        }
        /// <summary>
        /// Lower than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <=( complex A,  fcomplex B) { 
            return (A.real <= B.real );
        }

#endregion HYCALPER AUTO GENERATED CODE

#region HYCALPER LOOPSTART OPERATORS_complex+noncomplex
        /*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="after">
        Tret
    </source>
    <destination>complex</destination>
    <destination>complex</destination>
    <destination>complex</destination>
    <destination>complex</destination>
</type>
<type>
    <source locate="after">
        TinArr1 
    </source>
    <destination>complex</destination>
    <destination>complex</destination>
    <destination>complex</destination>
    <destination>complex</destination>
</type>
<type>
    <source locate="after">
        TinArr2
    </source>
    <destination>byte</destination>
    <destination>float</destination>
    <destination>Int32</destination>
    <destination>Int64</destination>
</type>
<type>
    <source locate="after">
        FCast
    </source>
    <destination>(double)</destination>
    <destination>(double)</destination>
    <destination>(double)</destination>
    <destination>(double)</destination>
</type>
<type>
    <source locate="after">
        infinity
    </source>
    <destination>complex.PositiveInfinity</destination>
    <destination>complex.PositiveInfinity</destination>
    <destination>complex.PositiveInfinity</destination>
    <destination>complex.PositiveInfinity</destination>
</type>
<type>
    <source locate="nextline">
        test4inf
    </source>
    <destination>if (false)</destination>
    <destination>if (float.IsInfinity(B))</destination>
    <destination>if (false)</destination>
    <destination>if (false)</destination>
</type>
<type>
    <source locate="nextline">
        test4NaNin2
    </source>
    <destination></destination>
    <destination>if (float.IsNaN(B)) return NaN;</destination>
    <destination></destination>
    <destination></destination>
</type>
</hycalper>
 */
        /// <summary>
        /// Add two complex numbers
        /// </summary>
        /// <param name="A">First summand</param>
        /// <param name="B">Second summand</param>
        /// <returns>Result</returns>
        public static /*!HC:Tret*/ complex operator +(/*!HC:TinArr1*/ complex A, /*!HC:TinArr2*/ double B) {
            /*!HC:Tret*/ complex ret;
            ret.real = /*!HC:FCast*/ (double) (A.real + B);
            ret.imag = /*!HC:FCast*/ (double) A.imag;
            return ret;
        }
        /// <summary>
        /// Subtract two values
        /// </summary>
        /// <param name="A">Minuend</param>
        /// <param name="B">Subtrahend</param>
        /// <returns>result</returns>
        public static /*!HC:Tret*/ complex operator -(/*!HC:TinArr1*/ complex A, /*!HC:TinArr2*/ double B) {
            /*!HC:Tret*/ complex ret;
            ret.real = /*!HC:FCast*/ (double) (A.real - B);
            ret.imag = /*!HC:FCast*/ (double) A.imag;
            return ret;
        }
        /// <summary>
        /// Multiply two values
        /// </summary>
        /// <param name="A">First factor</param>
        /// <param name="B">Second factor</param>
        /// <returns>result</returns>
        public static /*!HC:Tret*/ complex operator *(/*!HC:TinArr1*/ complex A, /*!HC:TinArr2*/ double B) {
            /*!HC:Tret*/ complex ret;
            ret.real = /*!HC:FCast*/ (double) (A.real * B);
            ret.imag = /*!HC:FCast*/ (double) (A.imag * B);
            return ret;
        }
        /// <summary>
        /// Divide two numbers
        /// </summary>
        /// <param name="A">Divident</param>
        /// <param name="B">Divisor</param>
        /// <returns>result</returns>
        public static /*!HC:Tret*/ complex operator /(/*!HC:TinArr1*/ complex A, /*!HC:TinArr2*/ double B) {
            if (IsNaN(A)) return NaN;
            /*!HC:test4NaNin2*/
            if (double.IsNaN(B)) return NaN; 
            if (A.real == 0 && A.imag == 0) {
                if (B == 0) return NaN; 
                return (/*!HC:Tret*/ complex )0;
            } else {
                /*!HC:test4inf*/
                if (double .IsInfinity(B)) 
                {
                    if (IsInfinity(A)) {
                        return NaN; 
                    } else {
                        return (/*!HC:Tret*/ complex )0;
                    }
                }
            }
            /*!HC:Tret*/ complex ret;
            if (B == 0) return PositiveInfinity ;
            ret.real = /*!HC:FCast*/ (double) (A.real / B);
            ret.imag = /*!HC:FCast*/ (double) (A.imag / B);
            return ret;
        }
        /// <summary>
        /// Equality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>result</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator ==(/*!HC:TinArr1*/ complex A, /*!HC:TinArr2*/ double B) {
            return (A.real == B && A.imag == 0.0);
        }
        /// <summary>
        /// Unequality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real and imaginary parts of A and B are not equal, false otherwise</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator !=(/*!HC:TinArr1*/ complex A, /*!HC:TinArr2*/ double B) {
            return (A.imag != 0.0) || (A.real != B);
        }
        /// <summary>
        /// Freater than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator > (/*!HC:TinArr1*/ complex A, /*!HC:TinArr2*/ double B) {
            return (A.real > B);
        }
        /// <summary>
        /// Lower than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <( /*!HC:TinArr1*/ complex A, /*!HC:TinArr2*/ double B) {
            return (A.real < B);
        }
        /// <summary>
        /// Greater than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator >=(/*!HC:TinArr1*/ complex A, /*!HC:TinArr2*/ double B) {
            return (A.real >= B);
        }
        /// <summary>
        /// Lower than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <=(/*!HC:TinArr1*/ complex A, /*!HC:TinArr2*/ double B) {
            return (A.real <= B);
        }
#endregion HYCALPER LOOPEND OPERATOR_complex+noncomplex
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       
        /// <summary>
        /// Add two complex numbers
        /// </summary>
        /// <param name="A">First summand</param>
        /// <param name="B">Second summand</param>
        /// <returns>Result</returns>
        public static  complex operator +( complex A,  Int64 B) {
            complex ret;
            ret.real =  (double) (A.real + B);
            ret.imag =  (double) A.imag;
            return ret;
        }
        /// <summary>
        /// Subtract two values
        /// </summary>
        /// <param name="A">Minuend</param>
        /// <param name="B">Subtrahend</param>
        /// <returns>result</returns>
        public static  complex operator -( complex A,  Int64 B) {
            complex ret;
            ret.real =  (double) (A.real - B);
            ret.imag =  (double) A.imag;
            return ret;
        }
        /// <summary>
        /// Multiply two values
        /// </summary>
        /// <param name="A">First factor</param>
        /// <param name="B">Second factor</param>
        /// <returns>result</returns>
        public static  complex operator *( complex A,  Int64 B) {
            complex ret;
            ret.real =  (double) (A.real * B);
            ret.imag =  (double) (A.imag * B);
            return ret;
        }
        /// <summary>
        /// Divide two numbers
        /// </summary>
        /// <param name="A">Divident</param>
        /// <param name="B">Divisor</param>
        /// <returns>result</returns>
        public static  complex operator /( complex A,  Int64 B) {
            if (IsNaN(A)) return NaN;
            
            if (A.real == 0 && A.imag == 0) {
                if (B == 0) return NaN; 
                return ( complex )0;
            } else {
                if (false)
                {
                    if (IsInfinity(A)) {
                        return NaN; 
                    } else {
                        return ( complex )0;
                    }
                }
            }
            complex ret;
            if (B == 0) return PositiveInfinity ;
            ret.real =  (double) (A.real / B);
            ret.imag =  (double) (A.imag / B);
            return ret;
        }
        /// <summary>
        /// Equality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>result</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator ==( complex A,  Int64 B) {
            return (A.real == B && A.imag == 0.0);
        }
        /// <summary>
        /// Unequality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real and imaginary parts of A and B are not equal, false otherwise</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator !=( complex A,  Int64 B) {
            return (A.imag != 0.0) || (A.real != B);
        }
        /// <summary>
        /// Freater than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator > ( complex A,  Int64 B) {
            return (A.real > B);
        }
        /// <summary>
        /// Lower than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <(  complex A,  Int64 B) {
            return (A.real < B);
        }
        /// <summary>
        /// Greater than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator >=( complex A,  Int64 B) {
            return (A.real >= B);
        }
        /// <summary>
        /// Lower than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <=( complex A,  Int64 B) {
            return (A.real <= B);
        }
       
        /// <summary>
        /// Add two complex numbers
        /// </summary>
        /// <param name="A">First summand</param>
        /// <param name="B">Second summand</param>
        /// <returns>Result</returns>
        public static  complex operator +( complex A,  Int32 B) {
            complex ret;
            ret.real =  (double) (A.real + B);
            ret.imag =  (double) A.imag;
            return ret;
        }
        /// <summary>
        /// Subtract two values
        /// </summary>
        /// <param name="A">Minuend</param>
        /// <param name="B">Subtrahend</param>
        /// <returns>result</returns>
        public static  complex operator -( complex A,  Int32 B) {
            complex ret;
            ret.real =  (double) (A.real - B);
            ret.imag =  (double) A.imag;
            return ret;
        }
        /// <summary>
        /// Multiply two values
        /// </summary>
        /// <param name="A">First factor</param>
        /// <param name="B">Second factor</param>
        /// <returns>result</returns>
        public static  complex operator *( complex A,  Int32 B) {
            complex ret;
            ret.real =  (double) (A.real * B);
            ret.imag =  (double) (A.imag * B);
            return ret;
        }
        /// <summary>
        /// Divide two numbers
        /// </summary>
        /// <param name="A">Divident</param>
        /// <param name="B">Divisor</param>
        /// <returns>result</returns>
        public static  complex operator /( complex A,  Int32 B) {
            if (IsNaN(A)) return NaN;
            
            if (A.real == 0 && A.imag == 0) {
                if (B == 0) return NaN; 
                return ( complex )0;
            } else {
                if (false)
                {
                    if (IsInfinity(A)) {
                        return NaN; 
                    } else {
                        return ( complex )0;
                    }
                }
            }
            complex ret;
            if (B == 0) return PositiveInfinity ;
            ret.real =  (double) (A.real / B);
            ret.imag =  (double) (A.imag / B);
            return ret;
        }
        /// <summary>
        /// Equality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>result</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator ==( complex A,  Int32 B) {
            return (A.real == B && A.imag == 0.0);
        }
        /// <summary>
        /// Unequality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real and imaginary parts of A and B are not equal, false otherwise</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator !=( complex A,  Int32 B) {
            return (A.imag != 0.0) || (A.real != B);
        }
        /// <summary>
        /// Freater than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator > ( complex A,  Int32 B) {
            return (A.real > B);
        }
        /// <summary>
        /// Lower than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <(  complex A,  Int32 B) {
            return (A.real < B);
        }
        /// <summary>
        /// Greater than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator >=( complex A,  Int32 B) {
            return (A.real >= B);
        }
        /// <summary>
        /// Lower than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <=( complex A,  Int32 B) {
            return (A.real <= B);
        }
       
        /// <summary>
        /// Add two complex numbers
        /// </summary>
        /// <param name="A">First summand</param>
        /// <param name="B">Second summand</param>
        /// <returns>Result</returns>
        public static  complex operator +( complex A,  float B) {
            complex ret;
            ret.real =  (double) (A.real + B);
            ret.imag =  (double) A.imag;
            return ret;
        }
        /// <summary>
        /// Subtract two values
        /// </summary>
        /// <param name="A">Minuend</param>
        /// <param name="B">Subtrahend</param>
        /// <returns>result</returns>
        public static  complex operator -( complex A,  float B) {
            complex ret;
            ret.real =  (double) (A.real - B);
            ret.imag =  (double) A.imag;
            return ret;
        }
        /// <summary>
        /// Multiply two values
        /// </summary>
        /// <param name="A">First factor</param>
        /// <param name="B">Second factor</param>
        /// <returns>result</returns>
        public static  complex operator *( complex A,  float B) {
            complex ret;
            ret.real =  (double) (A.real * B);
            ret.imag =  (double) (A.imag * B);
            return ret;
        }
        /// <summary>
        /// Divide two numbers
        /// </summary>
        /// <param name="A">Divident</param>
        /// <param name="B">Divisor</param>
        /// <returns>result</returns>
        public static  complex operator /( complex A,  float B) {
            if (IsNaN(A)) return NaN;
            if (float.IsNaN(B)) return NaN;
            if (A.real == 0 && A.imag == 0) {
                if (B == 0) return NaN; 
                return ( complex )0;
            } else {
                if (float.IsInfinity(B))
                {
                    if (IsInfinity(A)) {
                        return NaN; 
                    } else {
                        return ( complex )0;
                    }
                }
            }
            complex ret;
            if (B == 0) return PositiveInfinity ;
            ret.real =  (double) (A.real / B);
            ret.imag =  (double) (A.imag / B);
            return ret;
        }
        /// <summary>
        /// Equality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>result</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator ==( complex A,  float B) {
            return (A.real == B && A.imag == 0.0);
        }
        /// <summary>
        /// Unequality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real and imaginary parts of A and B are not equal, false otherwise</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator !=( complex A,  float B) {
            return (A.imag != 0.0) || (A.real != B);
        }
        /// <summary>
        /// Freater than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator > ( complex A,  float B) {
            return (A.real > B);
        }
        /// <summary>
        /// Lower than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <(  complex A,  float B) {
            return (A.real < B);
        }
        /// <summary>
        /// Greater than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator >=( complex A,  float B) {
            return (A.real >= B);
        }
        /// <summary>
        /// Lower than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <=( complex A,  float B) {
            return (A.real <= B);
        }
       
        /// <summary>
        /// Add two complex numbers
        /// </summary>
        /// <param name="A">First summand</param>
        /// <param name="B">Second summand</param>
        /// <returns>Result</returns>
        public static  complex operator +( complex A,  byte B) {
            complex ret;
            ret.real =  (double) (A.real + B);
            ret.imag =  (double) A.imag;
            return ret;
        }
        /// <summary>
        /// Subtract two values
        /// </summary>
        /// <param name="A">Minuend</param>
        /// <param name="B">Subtrahend</param>
        /// <returns>result</returns>
        public static  complex operator -( complex A,  byte B) {
            complex ret;
            ret.real =  (double) (A.real - B);
            ret.imag =  (double) A.imag;
            return ret;
        }
        /// <summary>
        /// Multiply two values
        /// </summary>
        /// <param name="A">First factor</param>
        /// <param name="B">Second factor</param>
        /// <returns>result</returns>
        public static  complex operator *( complex A,  byte B) {
            complex ret;
            ret.real =  (double) (A.real * B);
            ret.imag =  (double) (A.imag * B);
            return ret;
        }
        /// <summary>
        /// Divide two numbers
        /// </summary>
        /// <param name="A">Divident</param>
        /// <param name="B">Divisor</param>
        /// <returns>result</returns>
        public static  complex operator /( complex A,  byte B) {
            if (IsNaN(A)) return NaN;
            
            if (A.real == 0 && A.imag == 0) {
                if (B == 0) return NaN; 
                return ( complex )0;
            } else {
                if (false)
                {
                    if (IsInfinity(A)) {
                        return NaN; 
                    } else {
                        return ( complex )0;
                    }
                }
            }
            complex ret;
            if (B == 0) return PositiveInfinity ;
            ret.real =  (double) (A.real / B);
            ret.imag =  (double) (A.imag / B);
            return ret;
        }
        /// <summary>
        /// Equality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>result</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator ==( complex A,  byte B) {
            return (A.real == B && A.imag == 0.0);
        }
        /// <summary>
        /// Unequality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real and imaginary parts of A and B are not equal, false otherwise</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator !=( complex A,  byte B) {
            return (A.imag != 0.0) || (A.real != B);
        }
        /// <summary>
        /// Freater than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator > ( complex A,  byte B) {
            return (A.real > B);
        }
        /// <summary>
        /// Lower than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <(  complex A,  byte B) {
            return (A.real < B);
        }
        /// <summary>
        /// Greater than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator >=( complex A,  byte B) {
            return (A.real >= B);
        }
        /// <summary>
        /// Lower than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <=( complex A,  byte B) {
            return (A.real <= B);
        }

#endregion HYCALPER AUTO GENERATED CODE

#region HYCALPER LOOPSTART OPERATORS_noncomplex+complex
        /*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="after">
        Tret
    </source>
    <destination>complex</destination>
    <destination>complex</destination>
    <destination>complex</destination>
    <destination>complex</destination>
</type>
<type>
    <source locate="after">
        TinArr2 
    </source>
    <destination>complex</destination>
    <destination>complex</destination>
    <destination>complex</destination>
    <destination>complex</destination>
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
    <source locate="after">
        FCast
    </source>
    <destination>(double)</destination>
    <destination>(double)</destination>
    <destination>(double)</destination>
    <destination>(double)</destination>
</type>
<type>
    <source locate="after">
        infinity
    </source>
    <destination>complex.PositiveInfinity</destination>
    <destination>complex.PositiveInfinity</destination>
    <destination>complex.PositiveInfinity</destination>
    <destination>complex.PositiveInfinity</destination>
</type>
</hycalper>
 */
        /// <summary>
        /// Add two complex values
        /// </summary>
        /// <param name="A">First summand</param>
        /// <param name="B">Second summand</param>
        /// <returns>Result</returns>
        public static /*!HC:Tret*/ complex operator +(/*!HC:TinArr1*/ double A, /*!HC:TinArr2*/ complex B) {
            /*!HC:Tret*/ complex ret; 
            ret.real = /*!HC:FCast*/ (double) (A + B.real);
            ret.imag = /*!HC:FCast*/ (double) B.imag;
            return ret;
        }
        /// <summary>
        /// Subtract two values
        /// </summary>
        /// <param name="A">Minuend</param>
        /// <param name="B">Subtrahend</param>
        /// <returns>Result</returns>
        public static /*!HC:Tret*/ complex operator -(/*!HC:TinArr1*/ double A, /*!HC:TinArr2*/ complex B) {
            /*!HC:Tret*/ complex ret;
            ret.real = /*!HC:FCast*/ (double) (A - B.real);
            ret.imag = -/*!HC:FCast*/ (double) B.imag; 
            return ret;
        }
        /// <summary>
        /// Multiply two values
        /// </summary>
        /// <param name="A">First factor</param>
        /// <param name="B">Second factor</param>
        /// <returns>Result</returns>
        public static /*!HC:Tret*/ complex operator *(/*!HC:TinArr1*/ double A, /*!HC:TinArr2*/ complex B) {
            /*!HC:Tret*/ complex ret;
            ret.real = /*!HC:FCast*/ (double) (A * B.real);
            ret.imag = /*!HC:FCast*/ (double) (A * B.imag);
            return ret;
        }
        /// <summary>
        /// Divide two values
        /// </summary>
        /// <param name="A">Divident</param>
        /// <param name="B">Divisor</param>
        /// <returns>Result</returns>
        public static /*!HC:Tret*/ complex operator /(/*!HC:TinArr1*/ double A, /*!HC:TinArr2*/ complex B) {
            /*!HC:Tret*/ complex ret; 
            if (A == 0) {
                if (IsInfinity(B)) return NaN; 
            } else {
                if (IsInfinity(B)) return (/*!HC:Tret*/ complex )0; 
            }
            if (B.real == 0 && B.imag == 0) {
                return PositiveInfinity;
            }
            // this algorithm is taken from [1]. The one described in [2] was not taken. Tests 
            // did not show any advantage when using double precision floating point arithmetic.
            /*!HC:TRret*/ double tmp; 
            if (Math.Abs(B.real) >= Math.Abs(B.imag)) {
                tmp = /*!HC:FCast*/ (double) (B.imag * (1/B.real)); 
                ret.imag = /*!HC:FCast*/ (double) (B.real + B.imag*tmp); 
                ret.real = /*!HC:FCast*/ (double) A/ret.imag; 
                ret.imag = - /*!HC:FCast*/ (double) (A*tmp)/ret.imag; 
            } else {
                tmp = /*!HC:FCast*/ (double) (B.real * (1/B.imag));
                ret.imag = /*!HC:FCast*/ (double) (B.imag + B.real*tmp); 
                ret.real = /*!HC:FCast*/ (double) (A*tmp)/ret.imag; 
                ret.imag = - /*!HC:FCast*/ (double) A/ret.imag; 
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
        public static bool operator ==(/*!HC:TinArr1*/ double A, /*!HC:TinArr2*/ complex B) {
            return (B.real == A && B.imag == 0.0);
        }
        /// <summary>
        /// Unequality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real and imaginary parts of A and B are not equal, false otherwise</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator !=(/*!HC:TinArr1*/ double A, /*!HC:TinArr2*/ complex B) {
            return (B.imag != 0.0) || (B.real != A);
        }
        /// <summary>
        /// Greater than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator > (/*!HC:TinArr1*/ double A, /*!HC:TinArr2*/ complex B) {
            return (A > B.real);
        }
        /// <summary>
        /// Lower than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator < (/*!HC:TinArr1*/ double A, /*!HC:TinArr2*/ complex B) {
            return (A < B.real);
        }
        /// <summary>
        /// Greater than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator >=(/*!HC:TinArr1*/ double A, /*!HC:TinArr2*/ complex B) {
            return (A >= B.real);
        }
        /// <summary>
        /// Lower than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <=(/*!HC:TinArr1*/ double A, /*!HC:TinArr2*/ complex B) {
            return (A <= B.real);
        }
#endregion HYCALPER LOOPEND OPERATOR_noncomplex+complex
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       
        /// <summary>
        /// Add two complex values
        /// </summary>
        /// <param name="A">First summand</param>
        /// <param name="B">Second summand</param>
        /// <returns>Result</returns>
        public static  complex operator +( Int64 A,  complex B) {
            complex ret; 
            ret.real =  (double) (A + B.real);
            ret.imag =  (double) B.imag;
            return ret;
        }
        /// <summary>
        /// Subtract two values
        /// </summary>
        /// <param name="A">Minuend</param>
        /// <param name="B">Subtrahend</param>
        /// <returns>Result</returns>
        public static  complex operator -( Int64 A,  complex B) {
            complex ret;
            ret.real =  (double) (A - B.real);
            ret.imag = - (double) B.imag; 
            return ret;
        }
        /// <summary>
        /// Multiply two values
        /// </summary>
        /// <param name="A">First factor</param>
        /// <param name="B">Second factor</param>
        /// <returns>Result</returns>
        public static  complex operator *( Int64 A,  complex B) {
            complex ret;
            ret.real =  (double) (A * B.real);
            ret.imag =  (double) (A * B.imag);
            return ret;
        }
        /// <summary>
        /// Divide two values
        /// </summary>
        /// <param name="A">Divident</param>
        /// <param name="B">Divisor</param>
        /// <returns>Result</returns>
        public static  complex operator /( Int64 A,  complex B) {
            complex ret; 
            if (A == 0) {
                if (IsInfinity(B)) return NaN; 
            } else {
                if (IsInfinity(B)) return ( complex )0; 
            }
            if (B.real == 0 && B.imag == 0) {
                return PositiveInfinity;
            }
            // this algorithm is taken from [1]. The one described in [2] was not taken. Tests 
            // did not show any advantage when using double precision floating point arithmetic.
            double tmp; 
            if (Math.Abs(B.real) >= Math.Abs(B.imag)) {
                tmp =  (double) (B.imag * (1/B.real)); 
                ret.imag =  (double) (B.real + B.imag*tmp); 
                ret.real =  (double) A/ret.imag; 
                ret.imag = -  (double) (A*tmp)/ret.imag; 
            } else {
                tmp =  (double) (B.real * (1/B.imag));
                ret.imag =  (double) (B.imag + B.real*tmp); 
                ret.real =  (double) (A*tmp)/ret.imag; 
                ret.imag = -  (double) A/ret.imag; 
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
        public static bool operator ==( Int64 A,  complex B) {
            return (B.real == A && B.imag == 0.0);
        }
        /// <summary>
        /// Unequality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real and imaginary parts of A and B are not equal, false otherwise</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator !=( Int64 A,  complex B) {
            return (B.imag != 0.0) || (B.real != A);
        }
        /// <summary>
        /// Greater than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator > ( Int64 A,  complex B) {
            return (A > B.real);
        }
        /// <summary>
        /// Lower than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator < ( Int64 A,  complex B) {
            return (A < B.real);
        }
        /// <summary>
        /// Greater than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator >=( Int64 A,  complex B) {
            return (A >= B.real);
        }
        /// <summary>
        /// Lower than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <=( Int64 A,  complex B) {
            return (A <= B.real);
        }
       
        /// <summary>
        /// Add two complex values
        /// </summary>
        /// <param name="A">First summand</param>
        /// <param name="B">Second summand</param>
        /// <returns>Result</returns>
        public static  complex operator +( Int32 A,  complex B) {
            complex ret; 
            ret.real =  (double) (A + B.real);
            ret.imag =  (double) B.imag;
            return ret;
        }
        /// <summary>
        /// Subtract two values
        /// </summary>
        /// <param name="A">Minuend</param>
        /// <param name="B">Subtrahend</param>
        /// <returns>Result</returns>
        public static  complex operator -( Int32 A,  complex B) {
            complex ret;
            ret.real =  (double) (A - B.real);
            ret.imag = - (double) B.imag; 
            return ret;
        }
        /// <summary>
        /// Multiply two values
        /// </summary>
        /// <param name="A">First factor</param>
        /// <param name="B">Second factor</param>
        /// <returns>Result</returns>
        public static  complex operator *( Int32 A,  complex B) {
            complex ret;
            ret.real =  (double) (A * B.real);
            ret.imag =  (double) (A * B.imag);
            return ret;
        }
        /// <summary>
        /// Divide two values
        /// </summary>
        /// <param name="A">Divident</param>
        /// <param name="B">Divisor</param>
        /// <returns>Result</returns>
        public static  complex operator /( Int32 A,  complex B) {
            complex ret; 
            if (A == 0) {
                if (IsInfinity(B)) return NaN; 
            } else {
                if (IsInfinity(B)) return ( complex )0; 
            }
            if (B.real == 0 && B.imag == 0) {
                return PositiveInfinity;
            }
            // this algorithm is taken from [1]. The one described in [2] was not taken. Tests 
            // did not show any advantage when using double precision floating point arithmetic.
            double tmp; 
            if (Math.Abs(B.real) >= Math.Abs(B.imag)) {
                tmp =  (double) (B.imag * (1/B.real)); 
                ret.imag =  (double) (B.real + B.imag*tmp); 
                ret.real =  (double) A/ret.imag; 
                ret.imag = -  (double) (A*tmp)/ret.imag; 
            } else {
                tmp =  (double) (B.real * (1/B.imag));
                ret.imag =  (double) (B.imag + B.real*tmp); 
                ret.real =  (double) (A*tmp)/ret.imag; 
                ret.imag = -  (double) A/ret.imag; 
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
        public static bool operator ==( Int32 A,  complex B) {
            return (B.real == A && B.imag == 0.0);
        }
        /// <summary>
        /// Unequality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real and imaginary parts of A and B are not equal, false otherwise</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator !=( Int32 A,  complex B) {
            return (B.imag != 0.0) || (B.real != A);
        }
        /// <summary>
        /// Greater than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator > ( Int32 A,  complex B) {
            return (A > B.real);
        }
        /// <summary>
        /// Lower than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator < ( Int32 A,  complex B) {
            return (A < B.real);
        }
        /// <summary>
        /// Greater than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator >=( Int32 A,  complex B) {
            return (A >= B.real);
        }
        /// <summary>
        /// Lower than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <=( Int32 A,  complex B) {
            return (A <= B.real);
        }
       
        /// <summary>
        /// Add two complex values
        /// </summary>
        /// <param name="A">First summand</param>
        /// <param name="B">Second summand</param>
        /// <returns>Result</returns>
        public static  complex operator +( float A,  complex B) {
            complex ret; 
            ret.real =  (double) (A + B.real);
            ret.imag =  (double) B.imag;
            return ret;
        }
        /// <summary>
        /// Subtract two values
        /// </summary>
        /// <param name="A">Minuend</param>
        /// <param name="B">Subtrahend</param>
        /// <returns>Result</returns>
        public static  complex operator -( float A,  complex B) {
            complex ret;
            ret.real =  (double) (A - B.real);
            ret.imag = - (double) B.imag; 
            return ret;
        }
        /// <summary>
        /// Multiply two values
        /// </summary>
        /// <param name="A">First factor</param>
        /// <param name="B">Second factor</param>
        /// <returns>Result</returns>
        public static  complex operator *( float A,  complex B) {
            complex ret;
            ret.real =  (double) (A * B.real);
            ret.imag =  (double) (A * B.imag);
            return ret;
        }
        /// <summary>
        /// Divide two values
        /// </summary>
        /// <param name="A">Divident</param>
        /// <param name="B">Divisor</param>
        /// <returns>Result</returns>
        public static  complex operator /( float A,  complex B) {
            complex ret; 
            if (A == 0) {
                if (IsInfinity(B)) return NaN; 
            } else {
                if (IsInfinity(B)) return ( complex )0; 
            }
            if (B.real == 0 && B.imag == 0) {
                return PositiveInfinity;
            }
            // this algorithm is taken from [1]. The one described in [2] was not taken. Tests 
            // did not show any advantage when using double precision floating point arithmetic.
            double tmp; 
            if (Math.Abs(B.real) >= Math.Abs(B.imag)) {
                tmp =  (double) (B.imag * (1/B.real)); 
                ret.imag =  (double) (B.real + B.imag*tmp); 
                ret.real =  (double) A/ret.imag; 
                ret.imag = -  (double) (A*tmp)/ret.imag; 
            } else {
                tmp =  (double) (B.real * (1/B.imag));
                ret.imag =  (double) (B.imag + B.real*tmp); 
                ret.real =  (double) (A*tmp)/ret.imag; 
                ret.imag = -  (double) A/ret.imag; 
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
        public static bool operator ==( float A,  complex B) {
            return (B.real == A && B.imag == 0.0);
        }
        /// <summary>
        /// Unequality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real and imaginary parts of A and B are not equal, false otherwise</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator !=( float A,  complex B) {
            return (B.imag != 0.0) || (B.real != A);
        }
        /// <summary>
        /// Greater than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator > ( float A,  complex B) {
            return (A > B.real);
        }
        /// <summary>
        /// Lower than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator < ( float A,  complex B) {
            return (A < B.real);
        }
        /// <summary>
        /// Greater than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator >=( float A,  complex B) {
            return (A >= B.real);
        }
        /// <summary>
        /// Lower than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <=( float A,  complex B) {
            return (A <= B.real);
        }
       
        /// <summary>
        /// Add two complex values
        /// </summary>
        /// <param name="A">First summand</param>
        /// <param name="B">Second summand</param>
        /// <returns>Result</returns>
        public static  complex operator +( byte A,  complex B) {
            complex ret; 
            ret.real =  (double) (A + B.real);
            ret.imag =  (double) B.imag;
            return ret;
        }
        /// <summary>
        /// Subtract two values
        /// </summary>
        /// <param name="A">Minuend</param>
        /// <param name="B">Subtrahend</param>
        /// <returns>Result</returns>
        public static  complex operator -( byte A,  complex B) {
            complex ret;
            ret.real =  (double) (A - B.real);
            ret.imag = - (double) B.imag; 
            return ret;
        }
        /// <summary>
        /// Multiply two values
        /// </summary>
        /// <param name="A">First factor</param>
        /// <param name="B">Second factor</param>
        /// <returns>Result</returns>
        public static  complex operator *( byte A,  complex B) {
            complex ret;
            ret.real =  (double) (A * B.real);
            ret.imag =  (double) (A * B.imag);
            return ret;
        }
        /// <summary>
        /// Divide two values
        /// </summary>
        /// <param name="A">Divident</param>
        /// <param name="B">Divisor</param>
        /// <returns>Result</returns>
        public static  complex operator /( byte A,  complex B) {
            complex ret; 
            if (A == 0) {
                if (IsInfinity(B)) return NaN; 
            } else {
                if (IsInfinity(B)) return ( complex )0; 
            }
            if (B.real == 0 && B.imag == 0) {
                return PositiveInfinity;
            }
            // this algorithm is taken from [1]. The one described in [2] was not taken. Tests 
            // did not show any advantage when using double precision floating point arithmetic.
            double tmp; 
            if (Math.Abs(B.real) >= Math.Abs(B.imag)) {
                tmp =  (double) (B.imag * (1/B.real)); 
                ret.imag =  (double) (B.real + B.imag*tmp); 
                ret.real =  (double) A/ret.imag; 
                ret.imag = -  (double) (A*tmp)/ret.imag; 
            } else {
                tmp =  (double) (B.real * (1/B.imag));
                ret.imag =  (double) (B.imag + B.real*tmp); 
                ret.real =  (double) (A*tmp)/ret.imag; 
                ret.imag = -  (double) A/ret.imag; 
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
        public static bool operator ==( byte A,  complex B) {
            return (B.real == A && B.imag == 0.0);
        }
        /// <summary>
        /// Unequality comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real and imaginary parts of A and B are not equal, false otherwise</returns>
        /// <remarks>Real inputs are converted to a complex number and the result is compared to the complex input.</remarks>
        public static bool operator !=( byte A,  complex B) {
            return (B.imag != 0.0) || (B.real != A);
        }
        /// <summary>
        /// Greater than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator > ( byte A,  complex B) {
            return (A > B.real);
        }
        /// <summary>
        /// Lower than comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator < ( byte A,  complex B) {
            return (A < B.real);
        }
        /// <summary>
        /// Greater than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is greater than real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator >=( byte A,  complex B) {
            return (A >= B.real);
        }
        /// <summary>
        /// Lower than or equal to comparison for complex numbers
        /// </summary>
        /// <param name="A">Left side</param>
        /// <param name="B">Right side</param>
        /// <returns>true if real part of A is lower then real part of B, false otherwise</returns>
        /// <remarks>Only the real parts are compared!</remarks>
        public static bool operator <=( byte A,  complex B) {
            return (A <= B.real);
        }

#endregion HYCALPER AUTO GENERATED CODE

#region unary minus
        /// <summary>
        /// Unary minus operator
        /// </summary>
        /// <param name="A">Complex input</param>
        /// <returns>Complex number similar to A, having real and imag part negated</returns>
        public static complex operator -( complex A) {
            complex ret = new complex(); 
            ret.imag = -A.imag; 
            ret.real = -A.real; 
            return ret;
        }
#endregion

#region CAST_OPERATORS
        /// <summary>
        /// Cast value to complex number
        /// </summary>
        /// <param name="a">Value to cast</param>
        /// <returns>Complex number with the real part having the value of a and the imaginary part of 0.</returns>
        public static implicit operator complex(double a) {
            return new complex(a, 0.0);
        }
        /// <summary>
        /// Cast value to complex number
        /// </summary>
        /// <param name="a">Value to cast</param>
        /// <returns>Complex number with the real part having the value of a casted to double and the imaginary part is 0.</returns>
        public static implicit operator complex(fcomplex a) {
            return new complex(a.real, a.imag);
        }

        /// <summary>
        /// Cast value from complex number
        /// </summary>
        /// <param name="a">Complex value to cast</param>
        /// <returns>Double number with the real part of a </returns>
        public static explicit operator double(complex a) {
            return a.real; 
        }
        /// <summary>
        /// Cast value from complex number
        /// </summary>
        /// <param name="a">Complex value to cast</param>
        /// <returns>number with the real part of a </returns>
        /// <remarks>the return value is the result of a cast from double to float.</remarks>
        public static explicit operator float(complex a) {
            return (float)a.real;
        }
        /// <summary>
        /// Cast value from complex number
        /// </summary>
        /// <param name="a">Complex value to cast</param>
        /// <returns>float complex number with the real and imaginary parts being a copy of a </returns>
        /// <remarks>The real and imaginary parts are the result of a cast to float.</remarks>
        public static explicit operator fcomplex(complex a) {
            return new fcomplex((float)a.real, (float)a.imag);
        }
        /// <summary>
        /// Cast value from complex number
        /// </summary>
        /// <param name="a">Complex value to cast</param>
        /// <returns>Number with the real part of a </returns>
        /// <remarks>The return value is the result of a cast to Byte.</remarks>
        public static explicit operator byte(complex a) {
            return (byte) a.real; 
        }
        /// <summary>
        /// Cast value from complex number
        /// </summary>
        /// <param name="a">Complex value to cast</param>
        /// <returns>Number with the real part of a </returns>
        /// <remarks>The return value is the result of a cast to SByte.</remarks>
        public static explicit operator sbyte(complex a) {
            return (sbyte) a.real; 
        }
        /// <summary>
        /// Cast value from complex number
        /// </summary>
        /// <param name="a">Complex value to cast</param>
        /// <returns>Number with the real part of a </returns>
        /// <remarks>The return value is the result of a cast to Int16.</remarks>
        public static explicit operator short(complex a) {
            return (short) a.real; 
        }
        /// <summary>
        /// Cast value from complex number
        /// </summary>
        /// <param name="a">Complex value to cast</param>
        /// <returns>Number with the real part of a </returns>
        /// <remarks>The return value is the result of a cast to UInt16.</remarks>
        public static explicit operator ushort(complex a) {
            return (ushort) a.real; 
        }
        /// <summary>
        /// Cast value from complex number
        /// </summary>
        /// <param name="a">Complex value to cast</param>
        /// <returns>Number with the real part of a </returns>
        /// <remarks>The return value is the result of a cast to Int32.</remarks>
        public static explicit operator int(complex a) {
            return (int) a.real; 
        }
        /// <summary>
        /// Cast value from complex number
        /// </summary>
        /// <param name="a">Complex value to cast</param>
        /// <returns>Number with the real part of a </returns>
        /// <remarks>The return value is the result of a cast to UInt32.</remarks>
        public static explicit operator uint(complex a) {
            return (uint) a.real; 
        }
        /// <summary>
        /// Cast value from complex number
        /// </summary>
        /// <param name="a">Complex value to cast</param>
        /// <returns>number with the real part of a </returns>
        /// <remarks>the return value is the result of a cast to Int64.</remarks>
        public static explicit operator long(complex a) {
            return (long) a.real; 
        }
        /// <summary>
        /// Cast value from complex number
        /// </summary>
        /// <param name="a">Complex value to cast</param>
        /// <returns>number with the real part of a </returns>
        /// <remarks>the return value is the result of a cast to UInt64.</remarks>
        public static explicit operator ulong(complex a) {
            return (ulong) a.real; 
        }

        /// <summary>
        /// Implicitly converts a System.Numerics.Complex type to an ILNumerics.complex.
        /// </summary>
        /// <param name="a">System.Numerics.Complex number.</param>
        /// <returns>ILNumerics.complex number.</returns>
        public static implicit operator complex(System.Numerics.Complex a) {
            return new complex(a.Real, a.Imaginary); 
        }

#if NET8_0_OR_GREATER

        static complex IDecrementOperators<complex>.operator --(complex value) {
            return new complex(value.real--, value.imag); 
        }

        static complex IIncrementOperators<complex>.operator ++(complex value) {
            return new complex(value.real++, value.imag);
        }

        static complex IUnaryPlusOperators<complex, complex>.operator +(complex value) {
            return value;
        }
#endif

#endregion CAST_OPERATORS

        #region Functions Basic Math
        /// <summary>
        /// Absolute value of input
        /// </summary>
        /// <param name="input">Input value</param>
        /// <returns>The absolute value of the input</returns>
        public static double Abs(complex input) {
            return System.Numerics.Complex.Abs(new System.Numerics.Complex(input.real, input.imag)); 
            //return Math.Sqrt(input.real * input.real + input.imag * input.imag);
        }
        /// <summary>
        /// Phase angle of complex number
        /// </summary>
        /// <param name="input">Input value</param>
        /// <returns>The phase angle of the input</returns>
        /// <remarks>For the result the Atan2 function of the <see cref="Math"/> class is used.</remarks>
        public static double Angle(complex input) {
            return Math.Atan2(input.imag, input.real);
        }
        /// <summary>
        /// Arcus tangens of complex input
        /// </summary>
        /// <param name="input">Complex input</param>
        /// <returns>Arcus tangens of complex input</returns>
        /// <remarks></remarks>
        public static complex Atan(complex input) {
            complex ret = new complex(0, (float)0.5);
            return (ret * Log((complex.i + input) / (complex.i - input)));
        }
        /// <summary>
        /// Arcus cosinus of complex input
        /// </summary>
        /// <param name="input">Complex input</param>
        /// <returns>Arcus cosinus of input</returns>
        public static complex Acos(complex input) {
            complex ni = complex.i * -1.0;
            return complex.Log(complex.Sqrt(input * input - 1)
                + input) * ni;
        }
        /// <summary>
        /// Arcus cosinus of input
        /// </summary>
        /// <param name="input">Input value</param>
        /// <returns>Arcus cosinus of input</returns>
        public static complex Acos(double input) {
            if (Math.Abs(input) <= 1.0)
                return new complex(Math.Acos(input), 0.0);
            else {
                return Acos((complex)input);
            }
        }
        /// <summary>
        /// Arcus sinus of complex input
        /// </summary>
        /// <param name="input">Input value</param>
        /// <returns>Arcus sinus of input</returns>
        public static complex Asin(double input) {
            if (Math.Abs(input) <= 1.0)
                return new complex(Math.Asin(input), 0.0);
            else {
                return Asin((complex)input);
            }
        }
        /// <summary>
        /// Arcus sinus of input
        /// </summary>
        /// <param name="input">Input value</param>
        /// <returns>Arcus sinus of  input</returns>
        public static complex Asin(complex input) {
            complex ret = Acos(input);
            ret.real = Math.PI / 2 - ret.real;
            return ret;
        }
        /// <summary>
        /// Round towards positive infinity
        /// </summary>
        /// <param name="input">Input value</param>
        /// <returns>Result is the next integer value greater then input</returns>
        /// <remarks>ILMath.Ceiling operates in both: real and imaginary parts separately</remarks>
        public static complex Ceiling (complex input){
            return new complex(
                    Math.Ceiling(input.real),
                    Math.Ceiling(input.imag)
            );
        }
        /// <summary>
        /// Round towards negative infinity
        /// </summary>
        /// <param name="input">Input value</param>
        /// <returns>Result is the next integer value lower then input</returns>
        /// <remarks>ILMath.Floor operates in both: real and imaginary parts separately</remarks>
        public static complex Floor (complex input){
            return new complex(
                    Math.Floor(input.real),
                    Math.Floor(input.imag)
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
        public static complex Round(complex a) {
            return new complex(
                    Math.Round(a.real),
                    Math.Round(a.imag)
            );
        }
        /// <summary>
        /// Rounds towards nearest integer, optionally specify fractional digits and/or midpoint rounding mode.
        /// </summary>
        /// <param name="a">Input value.</param>
        /// <param name="digits">[Optional] Number of fractional digits to round to. Default: 0 (integral number).</param>
        /// <param name="roundingMode">[Optional] Midpoint rounding mode. Default: <see cref="MidpointRounding.ToEven"/>.</param>
        /// <returns>The value of <paramref name="a"/>, rounded to the nearest integer.</returns>
        /// <remarks><para>Rounding is performed for <see cref="real"/> and <see cref="imag"/> 
        /// separately.</para>
        /// <para>Numbers halfway between two others (0.5, 1.5,...) are rounded towards 
        /// the next even number. See: <see cref="System.MidpointRounding.ToEven"/>.</para></remarks>
        public static complex Round(complex a, int digits = 0, MidpointRounding roundingMode = MidpointRounding.ToEven) {
            return new complex(
                    Math.Round(a.real, digits, roundingMode),
                    Math.Round(a.imag, digits, roundingMode)
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
        public static complex Round(complex a, int digits) {
            return new complex(
                    Math.Round(a.real, digits),
                    Math.Round(a.imag, digits)
            );
        }
        /// <summary>
        /// Signum function
        /// </summary>
        /// <param name="input">Complex input </param>
        /// <returns>Sesult as input / Abs(input)</returns>
        /// <remarks>Sign(input) with input being complex returns the projection onto
        /// the unit circle. If input is 0+0i the result will be 0+0i.</remarks>
        public static complex Sign (complex input){
            if (input.real == 0.0 && input.imag == 0.0)
                return new complex(); 
            else {
                double mag = Math.Sqrt(input.real * input.real + input.imag * input.imag); 
                return new complex(
                    input.real / mag,
                    input.imag / mag);
            }
        }
        /// <summary>
        /// Truncate a floating point complex value
        /// </summary>
        /// <param name="input">Input value</param>
        /// <returns>Integer part of input</returns>
        /// <remarks>Operates on real and imaginary parts separately.</remarks>
        public static complex Truncate (complex input){
            return new complex(
                    Math.Truncate(input.real),
                    Math.Truncate(input.imag)
            );
        }
        /// <summary>
        /// Creates the complex conjugate of the complex value <paramref name="a"/>.
        /// </summary>
        /// <param name="a">The complex value.</param>
        /// <returns>The conjugate of a.</returns>
        /// <seealso cref="conj"/>
        /// <seealso cref="Conjugate()"/>
        public static complex Conjugate(complex a) {
            return new complex(a.real, -a.imag);
        }
        /// <summary>
        /// Cosinus
        /// </summary>
        /// <param name="input">Input value</param>
        /// <returns>Cosine of input</returns>
        /// <remarks><para>The cosine is computed by the trigonometric euler equation: </para>
        /// <para>0.5 * [exp(i input) + exp(-i input)]</para></remarks>
        public static complex Cos(complex input) {
            complex i = new complex(0, 1.0);
            complex mi = new complex(0, -1.0);
            return (Exp(i * input) + Exp(mi * input)) / 2.0;
        }
        /// <summary>
        /// Cosinus hyperbolicus
        /// </summary>
        /// <param name="input">Input</param>
        /// <returns>Cosine hyperbolicus of input</returns>
        /// <remarks><para>The cosine is computed by the trigonometric euler equation: </para>
        /// <para>(Exp(input) + Exp(-1.0 * input)) / 2.0</para></remarks>
        public static complex Cosh(complex input) {
            return (Exp(input) + Exp(-1.0 * input)) / 2.0;
        }
        /// <summary>
        /// Sinus
        /// </summary>
        /// <param name="input">Input value</param>
        /// <returns>Sinus of input</returns>
        /// <remarks><para>The sinus is computed by the trigonometric euler equation: </para>
        /// <para>(Exp(i * input) - Exp(-1.0 * i * input)) / (2.0 * i)</para></remarks>
        public static complex Sin(complex input) {
            complex i = new complex(0, 1.0);
            complex mi = new complex(0, -1.0);
            return (Exp(i * input) - Exp(mi * input)) / (2.0 * i);
        }
        /// <summary>
        /// Sinus hyperbolicus
        /// </summary>
        /// <param name="input">Input</param>
        /// <returns>Sinus hyperbolicus of input</returns>
        /// <remarks><para>The sinus hyperbolicus is computed by the trigonometric euler equation: </para>
        /// <para>(Exp(input) - Exp(-1.0 * input)) / 2.0</para></remarks>
        public static complex Sinh(complex input) {
            return (Exp(input) - Exp(-1.0 * input)) / 2.0;
        }
        /// <summary>
        /// Complex exponent
        /// </summary>
        /// <param name="exponent">Exponent</param>
        /// <returns>Result of exp(exponent)</returns>
        /// <remarks>For complex exponents, exp(exponent) is computed by
        /// <para>complex.FromPol(Math.Exp(exponent.real), exponent.imag)</para></remarks>
        public static complex Exp(complex exponent) {
            return complex.FromPol(Math.Exp(exponent.real), exponent.imag);
        }
        /// <summary>
        /// Complex power for real exponent
        /// </summary>
        /// <param name="input">Basis</param>
        /// <param name="exponent">Exponent</param>
        /// <returns>Result of input power exponent</returns>
        /// <remarks>The computation will be carried out by 
        /// <para>exp(log(input) * exponent)</para></remarks>
        public static complex Pow(complex input, double exponent) {
            complex ret = input.Log();
            ret.imag *= exponent;
            ret.real *= exponent;
            return ret.Exp();
        }
        /// <summary>
        /// Complex power - real basis, real exponent
        /// </summary>
        /// <param name="basis">Basis</param>
        /// <param name="exponent">Exponent</param>
        /// <returns>Complex number.</returns>
        /// <remarks>The result will be a complex number. For negative basis 
        /// the basis will be converted to a complex number and the power 
        /// will be computed in the complex plane.</remarks>
        public static complex Pow(double basis, double exponent) {
            if (basis >= 0.0)
                return Math.Pow(basis, exponent);
            else
                return Pow((complex)basis, exponent);
        }
        /// <summary>
        /// Complex power - complex exponent
        /// </summary>
        /// <param name="basis">Basis</param>
        /// <param name="exponent">Exponent</param>
        /// <returns>Complex number exp(log(basis) * exponent).</returns>
        /// <remarks>The result will be the complex number exp(log(basis) * exponent). </remarks>
        public static complex Pow(complex basis, complex exponent) {
            complex ret = (basis.Log() * exponent);
            return ret.Exp();
        }
        /// <summary>
        /// Square root
        /// </summary>
        /// <param name="input">Input value</param>
        /// <returns>The square root of input</returns>
        /// <remarks>If input is smaller than 0.0, the computation will be done in the complex plane. </remarks>
        public static complex Sqrt(double input) {
            if (input > 0.0)
                return new complex(Math.Sqrt(input), 0.0);
            else
                return Sqrt(new complex(input, 0.0));
        }
        /// <summary>
        /// Square root
        /// </summary>
        /// <param name="input">Input value</param>
        /// <returns>The square root of input</returns>
        /// <remarks>Numerical recipes in C: Appendix C </remarks>
        public static complex Sqrt(complex input) {

            return complex.FromPol(Math.Sqrt(input.Abs()), input.Angle() / 2.0); 

            // Reference : numerical recipes in C: Appendix C
            complex ret = new complex();
            double x, y, w, r;
            if (input.real == 0.0 && input.imag == 0.0)
                return ret;
            else {
                x = (double)Math.Abs(input.real);
                y = (double)Math.Abs(input.imag);
                if (x >= y) {
                    r = y / x;
                    w = Math.Sqrt(x) * Math.Sqrt(0.5 * (1.0 + Math.Sqrt(1.0 + r * r)));
                } else {
                    r = x / y;
                    w = Math.Sqrt(y) * Math.Sqrt(0.5 * (r + Math.Sqrt(1.0 + r * r)));
                }
                if (input.real >= 0.0) {
                    ret.real = w;
                    ret.imag = input.imag / (2.0 * w);
                } else {
                    ret.imag = (input.imag >= 0) ? w : -w;
                    ret.real = input.imag / (2.0 * ret.imag);
                }
                return ret;
            }
        }
        /// <summary>
        /// Tangens
        /// </summary>
        /// <param name="input">Input value</param>
        /// <returns>Tangens of input</returns>
        /// <remarks>The tangens is 
        /// <para>sin(input) / cos(input)</para>
        /// if cos(input) == 0.0+0.0i, PositiveInfinity will be returned.</remarks>
        public static complex Tan(complex input) {
            complex ci = Cos(input);
            if (ci.real == 0.0 && ci.imag == 0.0)
                return PositiveInfinity;
            return (Sin(input) / ci);
        }
        /// <summary>
        /// Tangens hyperbolicus
        /// </summary>
        /// <param name="input">Input value</param>
        /// <returns>Tangens hyperbolicus</returns>
        /// <remarks>The tangens hyperbolicus is 
        /// <para>sinh(input) / cosh(input)</para>
        /// if cosh(input) == 0.0+0.0i, PositiveInfinity will be returned.</remarks>
        public static complex Tanh(complex input) {
            complex si = Cosh(input);
            if (si.real == 0.0 && si.imag == 0.0)
                return PositiveInfinity;
            return (Sinh(input) / si);
        }
        /// <summary>
        /// Complex logarithm 
        /// </summary>
        /// <param name="input">Input value</param>
        /// <returns>Complex logarithm of input</returns>
        /// <remarks>The real part of the logarithm is computed by 
        /// <para>log (abs (input))</para>
        /// <para>The imaginary part holds the phase of input.</para>
        /// </remarks>
        public static complex Log(complex input) {
            complex ret = new complex();
            ret.real = Math.Log(Math.Sqrt(input.real * input.real + input.imag * input.imag));
            ret.imag = Math.Atan2(input.imag, input.real);
            return ret;
        }
        /// <summary>
        /// Logarithm of real input 
        /// </summary>
        /// <param name="input">Input value - may be negative</param>
        /// <returns>Complex logarithm</returns>
        public static complex Log(double input) {
            return Log (new complex(input,0.0)); 
        }
        /// <summary>
        /// Logarithm of base 10 of real input 
        /// </summary>
        /// <param name="input">Input value - may be negative</param>
        /// <returns>Complex logarithm of base 10</returns>
        public static complex Log10(double input) {
            return Log(new complex(input,0.0)) * 0.43429448190325176;
        }
        /// <summary>
        /// Logarithm of base 2 of real input 
        /// </summary>
        /// <param name="input">Input value - may be negative</param>
        /// <returns>Complex logarithm of base 2</returns>
        public static complex Log2(double input) {
            return Log(new complex(input,0.0)) * 1.4426950408889641;
        }
        /// <summary>
        /// Logarithm of base 10
        /// </summary>
        /// <param name="input">Input value</param>
        /// <returns>Logarithm of base 10</returns>
        /// <seealso cref="ILNumerics.complex.Log(complex)"/>
        public static complex Log10(complex input) {
            return Log(input) * 0.43429448190325176;
        }
        /// <summary>
        /// Logarithm of base 2
        /// </summary>
        /// <param name="input">Input value</param>
        /// <returns>Logarithm of base 2.</returns>
        /// <seealso cref="ILNumerics.complex.Log(complex)"/>
        public static complex Log2(complex input) {
            return Log(input) * 1.4426950408889634;
        }
        /// <summary>
        /// Convert polar notation into cartesian notation
        /// </summary>
        /// <param name="magnitude">Magnitude</param>
        /// <param name="angle">Phase</param>
        /// <returns>Complex value having magnitude and phase</returns>
        public static complex FromPol(double magnitude, double angle) {
            return new complex(
                magnitude * Math.Cos(angle),
                magnitude * Math.Sin(angle)
            );
        }
        /// <summary>
        /// Convert this complex number into a string representation.
        /// </summary>
        /// <returns>String displaying the complex number (full precision).</returns>
        public override String ToString() {
            return ILNumerics.F2NET.Helper.PrettyPrintNumber(1.0, this, 8, 0); 
            if (imag >= 0) {
                return $"{real}+{(double.IsInfinity(imag)?" ":"i")}{imag}";
            } else {
                return $"{real}-{((double.IsInfinity(imag) || double.IsNaN(imag)) ? " " : "i")}{-imag}";
            }
        }

        /// <summary>
        /// Magnitude of this complex instance
        /// </summary>
        /// <returns>Magnitude</returns>
        public double Abs() {

            return new System.Numerics.Complex(real, imag).Magnitude; 

            //// TODO: replace with own implementation!!! 
            //if (Double.IsInfinity(real) || Double.IsInfinity(imag)) {
            //    return double.PositiveInfinity;
            //}

            //// |value| == sqrt(a^2 + b^2)
            //// sqrt(a^2 + b^2) == a/a * sqrt(a^2 + b^2) = a * sqrt(a^2/a^2 + b^2/a^2)
            //// Using the above we can factor out the square of the larger component to dodge overflow.


            //double c = Math.Abs(real);
            //double d = Math.Abs(imag);

            //if (c > d) {
            //    double r = d / c;
            //    return c * Math.Sqrt(1.0 + r * r);
            //} else if (d == 0.0) {
            //    return c;  // c is either 0.0 or NaN
            //} else {
            //    double r = c / d;
            //    return d * Math.Sqrt(1.0 + r * r);
            //}
            //return Math.Sqrt(real * real + imag * imag);
        }
        /// <summary>
        /// Phase of this complex instance
        /// </summary>
        /// <returns>Phase</returns>
        public double Angle() {
            return Math.Atan2(imag, real);
        }
        /// <summary>
        /// Arcus cosinus of this complex instance
        /// </summary>
        /// <returns>Arcus cosinus</returns>
        public complex Acos() {
            complex ret = new complex(0, -1);
            return complex.Log(complex.Sqrt(this * this - 1)
                + this) * ret;
        }
        /// <summary>
        /// Arcus sinus of this complex instance
        /// </summary>
        /// <returns>arcus sinus</returns>
        public complex Asin() {
            complex ret = Acos(this);
            ret.real = Math.PI / 2 - ret.real;
            return ret;
        }
        /// <summary>
        /// Exponential / power of base e
        /// </summary>
        /// <returns>Power of base e</returns>
        public complex Exp() {
            return complex.FromPol(Math.Exp(real), imag);
        }
        /// <summary>
        /// Complex power real exponent
        /// </summary>
        /// <param name="exponent">Exponent</param>
        /// <returns>New complex number with result</returns>
        /// <remarks>If this instance is a and the exponent is e than 
        /// the result will be the complex number exp(log(a) * e). </remarks>
        public complex Pow(double exponent) {
            complex ret = Log();
            ret.imag *= exponent;
            ret.real *= exponent;
            return ret.Exp();
        }
        /// <summary>
        /// Complex power - complex exponent
        /// </summary>
        /// <param name="exponent">Exponent</param>
        /// <returns>Complex number exp(log(this) * exponent).</returns>
        /// <remarks>If this instance is a than 
        /// the result will be the complex number exp(log(a) * exponent). </remarks>
        public complex Pow(complex exponent) {
            complex ret = (Log() * exponent);
            return ret.Exp();
        }
        /// <summary>
        /// Square root of this complex value
        /// </summary>
        /// <returns>Square root of this complex value</returns>
        public complex Sqrt() {

            // we use MS Complex in the hope it will be optimized by the JIT
            return (complex)System.Numerics.Complex.Sqrt(new System.Numerics.Complex(real, imag)); 

            //// Reference : numerical recipes in C: Appendix C
            //complex ret = new complex();
            //double x, y, w, r;
            //if (real == 0.0 && imag == 0.0)
            //    return ret;
            //else {
            //    x = (double)Math.Abs(real);
            //    y = (double)Math.Abs(imag);
            //    if (x >= y) {
            //        r = y / x;
            //        w = Math.Sqrt(x) * Math.Sqrt(0.5 * (1.0 + Math.Sqrt(1.0 + r * r)));
            //    } else {
            //        r = x / y;
            //        w = Math.Sqrt(y) * Math.Sqrt(0.5 * (r + Math.Sqrt(1.0 + r * r)));
            //    }
            //    if (real >= 0.0) {
            //        ret.real = w;
            //        ret.imag = imag / (2.0 * w);
            //    } else {
            //        ret.imag = (imag >= 0) ? w : -w;
            //        ret.real = imag / ( 2.0 * ret.imag );
            //    }
            //    return ret;
            //}
        }
        /// <summary>
        /// Logarithm of base e
        /// </summary>
        /// <returns>Logarithm of base e</returns>
        /// <remarks>The logarithm of a complex number A is defined as follows: <br />
        /// <list type="none"><item>real part: log(abs(A))</item>
        /// <item>imag part: Atan2(imag(A),real(A))</item></list>
        /// </remarks>
        public complex Log() {
            complex ret = new complex();
            ret.real = Math.Log(Math.Sqrt(real * real + imag * imag));
            ret.imag = Math.Atan2(imag, real);
            return ret;
        }
        /// <summary>
        /// Test if any of real or imaginary parts are NAN's
        /// </summary>
        /// <param name="input">Complex number to test</param>
        /// <returns>true if any of real or imag part is not a number</returns>
        public static bool IsNaN(complex input) {
            if (double.IsNaN(input.real) || double.IsNaN(input.imag)) 
                return true; 
            else 
                return false; 
        }
        /// <summary>
        /// Test if any of real or imaginary parts are infinite
        /// </summary>
        /// <param name="input">Complex number to test</param>
        /// <returns>true if any of real or imag part is infinite</returns>
        public static bool IsInfinity(complex input) {  
            if (double.IsInfinity(input.real) || double.IsInfinity(input.imag)) 
                return true; 
            else 
                return false; 
        }
        /// <summary>
        /// Test if any of real or imaginary parts are pos.nfinite
        /// </summary>
        /// <param name="input">Complex number to test</param>
        /// <returns>true if any of real or imag part is positive infinite</returns>
        public static bool IsPositiveInfinity(complex input) {  
            if (double.IsPositiveInfinity(input.real) || double.IsPositiveInfinity(input.imag)) 
                return true; 
            else 
                return false; 
        }
        /// <summary>
        /// Test if any of real or imaginary parts are neg. infinite
        /// </summary>
        /// <param name="input">Complex number to test</param>
        /// <returns>true if any of real or imag part is negative infinite</returns>
        public static bool IsNegativeInfinity(complex input) {  
            if (double.IsNegativeInfinity(input.real) || double.IsNegativeInfinity(input.imag)) 
                return true; 
            else 
                return false; 
        }
        /// <summary>
        /// Test if any of real or imaginary parts are finite
        /// </summary>
        /// <param name="input">Complex number to test</param>
        /// <returns>true if any of real and imag part is finite</returns>
        public static bool IsFinite (complex input) {
            if (!complex.IsInfinity(input) && !complex.IsNaN(input))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Test if both of real or imaginary parts are 0
        /// </summary>
        /// <returns>true if real and imag part is 0</returns>
        public bool iszero() {
            if (real == 0.0 && imag == 0.0) 
                return true; 
            else 
                return false; 
        }
#endregion Functions Basic Math

#region Parse
        /// <summary>
        /// Converts the string <paramref name="text"/> containing a complex number into the complex number.
        /// </summary>
        /// <param name="text">String representation of the complex number.</param>
        /// <param name="style">Number style used for parsing individual parts (real, imag).</param>
        /// <param name="culture">The culture determining separators and floating point formats.</param>
        /// <returns>The complex number.</returns>
        /// <exception cref="FormatException">if the string is in an invalid format and/or cannot be converted.</exception>
        public static complex Parse(string text, NumberStyles style, CultureInfo culture) {
            string real, imag;
            if (!Partition(text, out real, out imag)) {
                throw new FormatException($"The string '{text}' is not a valid complex number.");
            }
            return new complex(double.Parse(real, style, culture), double.Parse(imag, style, culture)); 
        }
#if NET8_0_OR_GREATER
        /// <summary>
        /// Splits a string assumed to contain a complex number in format a+ib into real and imaginary parts. Removes 'i' identifiers.
        /// </summary>
        /// <param name="s">Number as text.</param>
        /// <param name="real">[Output] string with real part</param>
        /// <param name="imag">[Output] string with imaginary part.</param>
        public static bool Partition(ReadOnlySpan<char> s, out string real, out string imag) {
            return Partition(new string(s), out real, out imag); 
        }
#endif

        /// <summary>
        /// Splits a string assumed to contain a complex number in format a+ib into real and imaginary parts. Removes 'i' identifiers.
        /// </summary>
        /// <param name="text">Number as text.</param>
        /// <param name="real">[Output] string with real part</param>
        /// <param name="imag">[Output] string with imaginary part.</param>
        public static bool Partition(string text, out string real, out string imag) {

            // decide for the format, based on seperator.
            // 1) ';' exists: accepts <2.0; 2.0> and (1.0; 2.0) formats
            // 2) ';' not exists: accepts 1.0+i2.0 format
            var semInd = text.IndexOf(";"); 

            if (text.Contains(";")) {
                text = text.Trim(); 
                if ((text[0] == '<' || text[0] == '(') && (text[text.Length - 1] == '>' || text[text.Length-1] ==')')) { 
                    text = text.Trim().Replace(" ", string.Empty).ToLower();
                    string[] ret = text.Split(new char[] { '+', '-' }, StringSplitOptions.RemoveEmptyEntries);
                    real = ret[0].Trim(); 
                    imag = ret[1].Trim();
                    return true;
                } else {
                    real = "";
                    imag = "";
                    return false;
                }
            } else {

                text = text.Trim().Replace(" ", string.Empty).ToLower();
                if (string.IsNullOrEmpty(text)) {
                    real = ""; imag = "0";
                    return false;
                }
                string[] ret = text.Split(new char[] { '+', '-' }, StringSplitOptions.RemoveEmptyEntries);
                switch (ret.Length) {
                    case 1:

                        if (ret[0].Contains("i") || ret[0].Contains("j")) {
                            real = "0";
                            if (ret[0].Length > 1) {
                                imag = ret[0].Replace("i", "").Replace("j", "");
                            } else {
                                imag = "1";
                            }
                            imag = text.StartsWith("-") ? "-" + imag : imag;
                        } else {
                            real = text.StartsWith("-") ? "-" + ret[0] : ret[0];
                            imag = "0";
                        }
                        return true;
                    case 2:
                        if (ret[1].Contains("i") || ret[1].Contains("j")) {
                            if (!ret[0].Contains("i") && !ret[1].Contains("j")) {
                                real = text.StartsWith("-") ? "-" + ret[0] : ret[0];
                                if (ret[1].Length > 1) {
                                    imag = ret[1].Replace("i", "").Replace("j", "");
                                } else {
                                    imag = "1";
                                }
                                imag = text.LastIndexOf("-") >= ret[0].Length ? "-" + imag : imag;
                                return true;
                            }
                        } else {
                            if (ret[0].Contains("i") || ret[1].Contains("j")) {
                                real = text.LastIndexOf("-") >= ret[0].Length ? "-" + ret[1] : ret[1];
                                if (ret[0].Length > 1) {
                                    imag = ret[0].Replace("i", "").Replace("j", "");
                                } else {
                                    imag = "1";
                                }
                                imag = text.StartsWith("-") ? "-" + imag : imag;
                                return true;
                            }
                        }
                        break;
                }
                real = "";
                imag = "";
                return false;
            }
            //throw new FormatException($"The string '{text}' is not a valid complex number.");
        }

#if NET8_0_OR_GREATER
        static complex INumberBase<complex>.Abs(complex value) {
            return value.Abs();
        }

        static bool INumberBase<complex>.IsCanonical(complex value) => true;

        static bool INumberBase<complex>.IsComplexNumber(complex value) => true;

        static bool INumberBase<complex>.IsEvenInteger(complex value) => false;

        static bool INumberBase<complex>.IsImaginaryNumber(complex value) => value.real == 0 && !double.IsNaN(value.imag);

        static bool INumberBase<complex>.IsInteger(complex value) => false; 

        static bool INumberBase<complex>.IsNegative(complex value) {
            return value.imag == 0 && value.real < 0;
        }

        static bool INumberBase<complex>.IsNormal(complex value) {
            return double.IsNormal(value.real) && double.IsNormal(value.imag);
        }

        static bool INumberBase<complex>.IsOddInteger(complex value) => false; 

        static bool INumberBase<complex>.IsPositive(complex value) {
            return value.imag == 0 && value.real >= 0; 
        }

        static bool INumberBase<complex>.IsRealNumber(complex value) {
            return value.imag == 0 && double.IsNaN(value.real) == false; 
        }

        static bool INumberBase<complex>.IsSubnormal(complex value) {
            return double.IsSubnormal(value.real) || double.IsSubnormal(value.imag);
        }

        static bool INumberBase<complex>.IsZero(complex value) {
            return value == Zero; 
        }

        static complex INumberBase<complex>.MaxMagnitude(complex x, complex y) {
            return Complex.MaxMagnitude(System.Runtime.CompilerServices.Unsafe.BitCast<complex, Complex>(x), System.Runtime.CompilerServices.Unsafe.BitCast<complex, Complex>(y)); 
        }

        static T MaxMagnitudeNumberHelper<T>(T x, T y) where T : INumberBase<T>  => T.MaxMagnitudeNumber(x, y);
        static complex INumberBase<complex>.MaxMagnitudeNumber(complex x, complex y) {

            return MaxMagnitudeNumberHelper(System.Runtime.CompilerServices.Unsafe.BitCast<complex, Complex>(x), System.Runtime.CompilerServices.Unsafe.BitCast<complex, Complex>(y));
        }

        static complex INumberBase<complex>.MinMagnitude(complex x, complex y) {
            return Complex.MinMagnitude(System.Runtime.CompilerServices.Unsafe.BitCast<complex, Complex>(x), System.Runtime.CompilerServices.Unsafe.BitCast<complex, Complex>(y));
        }

        static T MinMagnitudeNumberHelper<T>(T x, T y) where T : INumberBase<T> => T.MinMagnitudeNumber(x, y);
        static complex INumberBase<complex>.MinMagnitudeNumber(complex x, complex y) {
            return MinMagnitudeNumberHelper(System.Runtime.CompilerServices.Unsafe.BitCast<complex, Complex>(x), System.Runtime.CompilerServices.Unsafe.BitCast<complex, Complex>(y));
        }

        static complex INumberBase<complex>.Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider provider) {
            if (!Partition(s, out string real, out string imag)) {
                throw new FormatException($"The string '{s}' is not a valid complex number.");
            }
            return new complex(double.Parse(s,style,provider), double.Parse(imag, style, provider));
        }

        static complex INumberBase<complex>.Parse(string s, NumberStyles style, IFormatProvider provider) {
            if (!Partition(s, out string real, out string imag)) {
                throw new FormatException($"The string '{s}' is not a valid complex number.");
            }
            return new complex(double.Parse(s, style, provider), double.Parse(imag, style, provider));
        }
        static bool INumberBase<complex>.TryConvertFromChecked<TOther>(TOther value, out complex result) {
            if(NumberBaseCall.TryConvertFromChecked(value, out Complex resultC)) {
                result = new complex(resultC.Real, resultC.Imaginary);
                return true; 
            }
            result = default;
            return false;
        }

        static bool INumberBase<complex>.TryConvertFromSaturating<TOther>(TOther value, out complex result) {
            if (NumberBaseCall.TryConvertFromSaturating(value, out Complex resultC)) {
                result = new complex(resultC.Real, resultC.Imaginary);
                return true;
            }
            result = default;
            return false;
        }

        static bool INumberBase<complex>.TryConvertFromTruncating<TOther>(TOther value, out complex result) {
            if (NumberBaseCall.TryConvertFromTruncating(value, out Complex resultC)) {
                result = new complex(resultC.Real, resultC.Imaginary);
                return true;
            }
            result = default;
            return false;
        }

        static bool INumberBase<complex>.TryConvertToChecked<TOther>(complex value, out TOther result) {
            return NumberBaseCall.TryConvertToChecked<Complex,TOther>(System.Runtime.CompilerServices.Unsafe.BitCast<complex, Complex>(value), out result); 
        }

        static bool INumberBase<complex>.TryConvertToSaturating<TOther>(complex value, out TOther result) {
            return NumberBaseCall.TryConvertToSaturating<Complex,TOther>(System.Runtime.CompilerServices.Unsafe.BitCast<complex, Complex>(value), out result); 
        }

        static bool INumberBase<complex>.TryConvertToTruncating<TOther>(complex value, out TOther result) {
            return NumberBaseCall.TryConvertToTruncating<Complex, TOther>(System.Runtime.CompilerServices.Unsafe.BitCast<complex, Complex>(value), out result);
        }

        static bool INumberBase<complex>.TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider provider, out complex result) {
            try {
                if (!Partition(s, out string real, out string imag)) {
                    throw new FormatException($"The string '{s}' is not a valid complex number.");
                }
                result = new complex(double.Parse(s, style, provider), double.Parse(imag, style, provider));
                return true;
            } catch (FormatException) {
                result = default;
                return false; 
            }
        }

        static bool INumberBase<complex>.TryParse(string s, NumberStyles style, IFormatProvider provider, out complex result) {
            try {
                if (!Partition(s, out string real, out string imag)) {
                    throw new FormatException($"The string '{s}' is not a valid complex number.");
                }
                result = new complex(double.Parse(s, style, provider), double.Parse(imag, style, provider));
                return true;
            } catch (FormatException) {
                result = default;
                return false;
            }
        }

        bool ISpanFormattable.TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider provider) {
            charsWritten = 0;

            // 1) real
            if (!real.TryFormat(destination, out int writtenRe, format, provider))
                return false;

            int pos = writtenRe;

            // 2) sign
            if ((uint)pos >= (uint)destination.Length)
                return false;

            // Treat -0.0 as negative too
            bool imagNeg = BitConverter.DoubleToInt64Bits(imag) < 0;
            destination[pos++] = imagNeg ? '-' : '+';

            // 3) abs(imag)
            double absIm = Math.Abs(imag);
            if (!absIm.TryFormat(destination.Slice(pos), out int writtenIm, format, provider))
                return false;
            // special case: imag == 1 -> 0+i instead of 0+1i
            // This is required for compatibility with older ILNumerics versions, prior v7.5.
            if (writtenIm != 1 && absIm != 1) {
                pos += writtenIm;
            }

            // 4) trailing 'i'
            if ((uint)pos >= (uint)destination.Length)
                return false;

            destination[pos++] = 'i';

            charsWritten = pos;
            return true;
        }        

        string IFormattable.ToString(string format, IFormatProvider formatProvider) {
            var provider = formatProvider ?? CultureInfo.CurrentCulture;
            format = string.IsNullOrEmpty(format) ? "G" : format;

            string re = real.ToString(format, provider);
            string im = Math.Abs(imag).ToString(format, provider);
            string sign = imag < 0 ? "-" : "+";

            return re + sign + im + "i";
        }
        static complex ISpanParsable<complex>.Parse(ReadOnlySpan<char> s, IFormatProvider provider) {
            if (!Partition(s, out string real, out string imag)) {
                throw new FormatException($"The string '{s}' is not a valid complex number.");
            }
            return new complex(double.Parse(real, provider), double.Parse(imag, provider));
        }

        static bool ISpanParsable<complex>.TryParse(ReadOnlySpan<char> s, IFormatProvider provider, out complex result) {
            try {
                if (!Partition(s, out string real, out string imag)) {
                    throw new FormatException($"The string '{s}' is not a valid complex number.");
                }
                result = new complex(double.Parse(real, provider), double.Parse(imag, provider));
                return true; 
            } catch (FormatException) {
                result = default;
                return false; 
            }
        }

        static complex IParsable<complex>.Parse(string s, IFormatProvider provider) {
            if (!Partition(s, out string real, out string imag)) {
                throw new FormatException($"The string '{s}' is not a valid complex number.");
            }
            return new complex(double.Parse(real, provider), double.Parse(imag, provider));

        }

        static bool IParsable<complex>.TryParse(string s, IFormatProvider provider, out complex result) {
            try {
                if (!Partition(s, out string real, out string imag)) {
                    throw new FormatException($"The string '{s}' is not a valid complex number.");
                }
                result = new complex(double.Parse(real, provider), double.Parse(imag, provider));
                return true;
            } catch (FormatException) {
                result = default;
                return false;
            } catch (OverflowException) {
                result = default;
                return false;
            }
        }

        internal static class NumberBaseCall {
            // Calls the target type's explicit INumberBase<T>.TryConvertFromChecked implementation
            public static bool TryConvertFromChecked<TTarget, TOther>(TOther value, out TTarget result)
                where TTarget : INumberBase<TTarget>
                where TOther : INumberBase<TOther>
                => TTarget.TryConvertFromChecked(value, out result);
            public static bool TryConvertFromSaturating<TTarget, TOther>(TOther value, out TTarget result)
                where TTarget : INumberBase<TTarget>
                where TOther : INumberBase<TOther>
                => TTarget.TryConvertFromSaturating(value, out result);
            public static bool TryConvertFromTruncating<TTarget, TOther>(TOther value, out TTarget result)
                where TTarget : INumberBase<TTarget>
                where TOther : INumberBase<TOther>
                => TTarget.TryConvertFromTruncating(value, out result);
            public static bool TryConvertToChecked<TBase, TTarget2>(TBase value, out TTarget2 result)
                where TTarget2 : INumberBase<TTarget2>
                where TBase : INumberBase<TBase>
                => TBase.TryConvertToChecked<TTarget2>(value, out result);
            public static bool TryConvertToSaturating<TBase, TTarget2>(TBase value, out TTarget2 result)
                where TTarget2 : INumberBase<TTarget2>
                where TBase : INumberBase<TBase>
                => TBase.TryConvertToSaturating<TTarget2>(value, out result);
            public static bool TryConvertToTruncating<TBase, TTarget2>(TBase value, out TTarget2 result)
                where TTarget2 : INumberBase<TTarget2>
                where TBase : INumberBase<TBase>
                => TBase.TryConvertToTruncating<TTarget2>(value, out result);

        }

#endif
        #endregion
    }

}
