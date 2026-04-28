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
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Text;
#if DEFAULT_INTEGER_64
using DEFAULT_INTEGER = System.Int64;
#else 
using DEFAULT_INTEGER = System.Int32;
#endif

#pragma warning disable CS1591

namespace ILNumerics.F2NET {
    public unsafe static partial class Intrinsics {

        public static double FLOOR(double a) {
            return Math.Floor(a);
        }
        public static float FLOOR(float a) {
            return (float)Math.Floor(a);
        }

        public static float MIN(float a, float b) {
            return Math.Min(a, b);
        }
        public static float MIN0(float a, float b) {
            return Math.Min(a, b);
        }
        public static float AMIN1(float a, float b) {
            return Math.Min(a, b);
        }
        public static float DMIN1(float a, float b) {
            return Math.Min(a, b);
        }
        public static float MIN(float a, float b, float c) {
            return Math.Min(Math.Min(a, b), c);
        }
        public static float MIN(float a, float b, float c, float d) {
            return Math.Min(Math.Min(a, b), Math.Min(c, d));
        }
        public static Double MIN(double a, double b) {
            return Math.Min(a, b);
        }
        public static Double MIN0(double a, double b) {
            return Math.Min(a, b);
        }
        public static Double DMIN1(double a, double b) {
            return Math.Min(a, b);
        }
        public static Double MIN(double a, double b, double c) {
            return Math.Min(Math.Min(a, b), c);
        }
        public static Double MIN(double a, double b, double c, double d) {
            return Math.Min(Math.Min(a, b), Math.Min(c, d));
        }
        public static Double MIN(double a, double b, double c, double d, double e) {
            return Math.Min(Math.Min(Math.Min(a, b), Math.Min(c, d)), e);
        }
        public static long MIN(long a, long b) {
            return Math.Min(a, b);
        }
        public static int MIN(int a, int b) {
            return Math.Min(a, b);
        }
        public static int MIN0(int a, int b) {
            return Math.Min(a, b);
        }
        public static long MIN(long a, long b, long c) {
            return Math.Min(Math.Min(a, b), c);
        }
        public static int MIN(int a, int b, int c) {
            return Math.Min(Math.Min(a, b), c);
        }
        public static int MIN(int a, int b, int c, int d) {
            return Math.Min(Math.Min(a, b), Math.Min(c,d));
        }
        public static long MIN(long a, long b, long c, long d) {
            return Math.Min(Math.Min(a, b), Math.Min(c, d));
        }
        public static int MIN(int a, int b, int c, int d, int e) {
            return Math.Min(Math.Min(Math.Min(a, b), Math.Min(c, d)), e);
        }
        public static float MAX(float a, float b) {
            return Math.Max(a, b);
        }
        public static float AMAX1(float a, float b) {
            return Math.Max(a, b);
        }
        public static float DMAX1(float a, float b) {
            return Math.Max(a, b);
        }
        public static float MAX(float a, float b, float c) {
            return Math.Max(Math.Max(a, b), c);
        }
        public static float MAX(float a, float b, float c, float d) {
            return Math.Max(Math.Max(a, b), Math.Max(c, d));
        }
        public static float MAX(float a, float b, float c, float d, float e) {
            return Math.Max(Math.Max(Math.Max(a, b), Math.Max(c, d)), e);
        }
        public static float MAX(float a, float b, float c, float d, float e, float f) {
            return Math.Max(Math.Max(Math.Max(a, b), Math.Max(c, d)), Math.Max(e,f));
        }
        public static float MAX(float a, float b, float c, float d, float e, float f, float g) {
            return Math.Max(Math.Max(Math.Max(a, b), Math.Max(c, d)), Math.Max(e, Math.Max(f,g)));
        }
        public static Double MAX(double a, double b) {
            return Math.Max(a, b);
        }
        public static Double DMAX1(double a, double b) {
            return Math.Max(a, b);
        }
        public static Double MAX(double a, double b, double c) {
            return Math.Max(Math.Max(a, b), c);
        }
        public static Double MAX(double a, double b, double c, double d) {
            return Math.Max(Math.Max(a, b), Math.Max(c, d));
        }
        public static Double MAX(double a, double b, double c, double d, double e) {
            return Math.Max(Math.Max(Math.Max(a, b), Math.Max(c, d)), e);
        }
        public static Double MAX(double a, double b, double c, double d, double e, double f) {
            return Math.Max(Math.Max(Math.Max(a, b), Math.Max(c, d)), Math.Max(e, f));
        }
        public static Double MAX(double a, double b, double c, double d, double e, double f, double g) {
            return Math.Max(Math.Max(Math.Max(a, b), Math.Max(c, d)), Math.Max(e, Math.Max(f, g)));
        }
        public static long MAX(long a, long b) {
            return Math.Max(a, b);
        }
        public static int MAX(int a, int b) {
            return Math.Max(a, b);
        }
        public static long MAX(long a, long b, long c) {
            return Math.Max(Math.Max(a, b), c);
        }
        public static int MAX(int a, int b, int c) {
            return Math.Max(Math.Max(a, b), c);
        }
        public static long MAX(long a, long b, long c, long d) {
            return Math.Max(Math.Max(a, b), Math.Max(c, d));
        }
        public static int MAX(int a, int b, int c, int d) {
            return Math.Max(Math.Max(a, b), Math.Max(c,d));
        }
        public static long MAX(long a, long b, long c, long d, long e) {
            return Math.Max(Math.Max(Math.Max(a, b), Math.Max(c, d)), e);
        }
        public static int MAX(int a, int b, int c, int d, int e) {
            return Math.Max(Math.Max(Math.Max(a, b), Math.Max(c, d)), e);
        }
        public static int MAX(int a, int b, int c, int d, int e, int f) {
            return Math.Max(Math.Max(Math.Max(a, b), Math.Max(c, d)), Math.Max(e, f));
        }
        public static int MAX(int a, int b, int c, int d, int e, int f, int g) {
            return Math.Max(Math.Max(Math.Max(a, b), Math.Max(c, d)), Math.Max(e, Math.Max(f, g)));
        }
        public static int MAX(int a, int b, int c, int d, int e, int f, int g, int h, int i, int j, int k, int l) {
            return Math.Max(Math.Max(Math.Max(Math.Max(a, b), Math.Max(c, d)), Math.Max(Math.Max(e, f), Math.Max(g, h))), Math.Max(i, Math.Max(j, Math.Max(k, l))));
        }
        public static int MAX(int a, int b, int c, int d, int e, int f, int g, int h, int i, int j, int k, int l, int m) {
            return Math.Max(Math.Max(Math.Max(Math.Max(a, b), Math.Max(c, d)), Math.Max(Math.Max(e, f), Math.Max(g, h))), Math.Max(i, Math.Max(j, Math.Max(k, Math.Max(l, m)))));
        }
        public static double DSQRT(double a) {
            return Math.Sqrt(a);
        }
        public static double SQRT(double a) {
            return Math.Sqrt(a);
        }
        public static float SQRT(float a) {
            return (float)Math.Sqrt(a);
        }
        public static fcomplex SQRT(fcomplex a) {
            return fcomplex.Sqrt(a);
        }
        public static complex SQRT(complex a) {
            return complex.Sqrt(a);
        }

        //public static bool LSAME(FString a, FString b) {
        //    return string.Compare(a, b, true) == 0; 
        //}
        public static DEFAULT_INTEGER INT(double a) {
            /*13.13.47 INT (A, KIND)
Optional Argument. KIND
Description. Convert to integer type.
Class. Elemental function.
Arguments.
A must be of type integer, real, or complex.
KIND (optional) must be a scalar integer initialization expression.
Result Type and Type Parameter. Integer. If KIND is present, the kind type parameter is that specified
by KIND; otherwise, the kind type parameter is that of default integer type.
Result Value.
Case (i): If A is of type integer, INT (A) = A.
Case (ii): If A is of type real, there are two cases: if , INT (A) has the value 0; if , INT
(A) is the integer whose magnitude is the largest integer that does not exceed the magnitude
of A and whose sign is the same as the sign of A.
Case (iii): If A is of type complex, INT (A) is the value obtained by applying the case (ii) rule to the
real part of A.
A < 1 A ≥ 1
ISO/IEC 1539 : 1991 (E)
207
The result is undefined if the processor cannot represent the result in the specified integer type.
Example. INT (–3.7) has the value –3.
            */
            return (DEFAULT_INTEGER)a;
        }
        public static DEFAULT_INTEGER INT(float a) {
            return (DEFAULT_INTEGER)a;
        }
        public static DEFAULT_INTEGER INT(DEFAULT_INTEGER a) { // don't ask. This is sometimes used, though...
            return a;
        }
        public static DEFAULT_INTEGER INT(complex a) {
            return (DEFAULT_INTEGER)a.real;
        }
        public static DEFAULT_INTEGER INT(fcomplex a) {
            return (DEFAULT_INTEGER)a.real;
        }

        public static DEFAULT_INTEGER ICHAR(char c) {
            return ASCIIEncoding.ASCII.GetBytes(new string(c, 1))[0];
        }
        public static DEFAULT_INTEGER ICHAR(FString c) {
            return ASCIIEncoding.ASCII.GetBytes(c)[0];
        }
        public static char CHAR(int i) {
            if (i < 0 || i > byte.MaxValue) {
                throw new ArgumentException($"Invalid value for CHAR. Min=0,Max={byte.MaxValue}. Found: I={i}.");
            }
            return ASCIIEncoding.ASCII.GetChars(new byte[] { (byte)i })[0];
        }
        public static char CHAR(long i) {
            if (i < 0 || i > byte.MaxValue) {
                throw new ArgumentException($"Invalid value for CHAR. Min=0,Max={byte.MaxValue}. Found: I={i}.");
            }
            return ASCIIEncoding.ASCII.GetChars(new byte[] { (byte)i })[0];
        }

        public static DEFAULT_INTEGER LEN(FString s) {
            if (s.Length < 0) return s.ToString().Length; 
            return s.Length; 
        }

        public static DEFAULT_INTEGER LEN_TRIM(FString s) {
            return s.ToString().TrimEnd().Length;
        }

        public static float REAL(long a) {
            return a;
        }
        public static float FLOAT(long a) {
            return a;
        }
        public static float REAL(int a) {
            return a;
        }
        public static float FLOAT(int a) {
            return a;
        }
        public static float REAL(float a) {
            return a;
        }
        public static float REAL(double a) {
            return (float)a;
        }
        public static float REAL(ILNumerics.complex a) {
            return (float)a.real;
        }
        public static float REAL(ILNumerics.fcomplex a) {
            return a.real;
        }
        public static double DREAL(int a) {
            return a;
        }
        public static double DREAL(long a) {
            return a;
        }
        public static double DREAL(float a) {
            return a;
        }
        public static double DREAL(double a) {
            return a;
        }
        public static double DREAL(ILNumerics.complex a) {
            return a.real;
        }
        public static double DREAL(ILNumerics.fcomplex a) {
            return a.real;
        }

        public static long IABS(long a) {
            return Math.Abs(a);
        }
        public static float ABS(float a) {
            return (float)Math.Abs(a);
        }
        public static double ABS(double a) {
            return Math.Abs(a);
        }
        public static double ABS(complex a) {
            return System.Numerics.Complex.Abs(new System.Numerics.Complex(a.real, a.imag)); 
        }
        public static long ABS(long a) {
            return Math.Abs(a);
        }
        public static int ABS(int a) {
            return Math.Abs(a);
        }
        public static float ABS(fcomplex a) {
            return fcomplex.Abs(a);
        }
        public static double DABS(double a) {
            return Math.Abs(a);
        }
        public static float CABS(fcomplex a) {
            return a.Abs();
        }
        public static double QABS(complex a) {
            return a.Abs();
        }
        public static double ZABS(complex a) {
            return a.Abs();
        }
        /// <summary>
        /// Alias used by GFortran compiler (and PG ?).
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static double CDABS(complex a) {
            return a.Abs();
        }


        public static double MOD(double a, double p) {
            return a - ((long)(a / p) * p);
        }
        public static float MOD(float a, float p) {
            return a - ((long)(a / p) * p);
        }
        public static long MOD(long a, long p) {
            return a - ((long)(a / p) * p);
        }
        public static int MOD(int a, int p) {
            return a - ((int)(a / p) * p);
        }
        public static double DSIGN(double a, double b) {
            if (b >= 0) {
                return Math.Abs(a);
            } else {
                return -Math.Abs(a);
            }
        }
        public static double SIGN(double a, double b) {
            if (b >= 0) {
                return Math.Abs(a);
            } else {
                return -Math.Abs(a);
            }
        }
        public static float SIGN(float a, float b) {
            if (b >= 0) {
                return Math.Abs(a);
            } else {
                return -Math.Abs(a);
            }
        }
        public static long SIGN(long a, long b) {
            if (b >= 0) {
                return Math.Abs(a);
            } else {
                return -Math.Abs(a);
            }
        }
        public static DEFAULT_INTEGER NINT(double a) {
            return (DEFAULT_INTEGER)Math.Round(a, MidpointRounding.AwayFromZero);
        }
        public static DEFAULT_INTEGER NINT(float a) {
            return (DEFAULT_INTEGER)Math.Round(a, MidpointRounding.AwayFromZero);
        }
        public static DEFAULT_INTEGER IDNINT(double a) {
            return (DEFAULT_INTEGER)Math.Round(a, MidpointRounding.AwayFromZero);
        }
        public static DEFAULT_INTEGER IDNINT(float a) {
            return (DEFAULT_INTEGER)Math.Round(a, MidpointRounding.AwayFromZero);
        }
        public static DEFAULT_INTEGER CEILING(float a) {
            return (DEFAULT_INTEGER)Math.Ceiling(a);
        }
        public static DEFAULT_INTEGER CEILING(double a) {
            return (DEFAULT_INTEGER)Math.Ceiling(a);
        }
        public static double DLOG(double a) {
            return Math.Log(a);
        }
        public static double LOG(double a) {
            return Math.Log(a);
        }
        public static double LOG10(double a) {
            return Math.Log10(a);
        }
        public static float LOG(float a) {
            return (float)Math.Log(a);
        }
        public static float ALOG(float a) {
            return (float)Math.Log(a);
        }
        public static float LOG10(float a) {
            return (float)Math.Log10(a);
        }
        public static ILNumerics.complex LOG(ILNumerics.complex a) {
            return ILNumerics.complex.Log(a);
        }
        public static ILNumerics.complex LOG10(ILNumerics.complex a) {
            return ILNumerics.complex.Log10(a);
        }
        public static ILNumerics.fcomplex LOG(ILNumerics.fcomplex a) {
            return ILNumerics.fcomplex.Log(a);
        }
        public static ILNumerics.fcomplex LOG10(ILNumerics.fcomplex a) {
            return ILNumerics.fcomplex.Log10(a);
        }

        public static double DBLE(double a) {  // just for completeness ('generic'). Some FORTRAN guys want to feel very secure...
            return a;
        }
        public static double DBLE(float a) {
            return a;
        }
        public static double DBLE(complex a) {
            return a.real;
        }
        public static double DBLE(fcomplex a) {
            return a.real;
        }
        public static double DBLE(long a) {
            return a;
        }
        public static double DBLE(int a) {
            return a;
        }
        public static double EPSILON(double a) {
            return 2.2204460492503131e-16;
        }
        public static float EPSILON(float a) {
            return 1.1920929e-7f;
        }
        public static double TINY(double a) {
            // on ifort this gives:       2.225073858507201D-308 
            // CLR gives: double.Epsilon = 4.94065645841247E-324
            return 2.2250738585072014e-308; // 2.225073858507201E-308;
        }

        // work around case bug in DLAMCH
        //public static double tiny(double a) {
        //    return double.Epsilon;
        //}
        public static double zero = 0.0; 


        public static float TINY(float a) {
            // use 'LAPACK-save' value: 1.17549435e-38 (ifort)
            return BitConverter.ToSingle(BitConverter.GetBytes(8388608), 0); 
        }
        public static long MAXEXPONENT(double a) {
            // See: https://software.intel.com/content/dam/develop/external/us/en/documents/oneapi_fortran_compiler.pdf
            // page 1131
            // retrieved 2021-01-14

            return 1024;
        }
        public static DEFAULT_INTEGER MAXEXPONENT(float a) {
            // See: https://software.intel.com/content/dam/develop/external/us/en/documents/oneapi_fortran_compiler.pdf
            // page 1131
            // retrieved 2021-01-14

            return 128;
        }
        public static DEFAULT_INTEGER MINEXPONENT(double a) {
            // See: https://software.intel.com/content/dam/develop/external/us/en/documents/oneapi_fortran_compiler.pdf
            // page 1131
            // retrieved 2021-01-14

            return -1021;
        }
        public static DEFAULT_INTEGER MINEXPONENT(float a) {
            // See: https://software.intel.com/content/dam/develop/external/us/en/documents/oneapi_fortran_compiler.pdf
            // page 1131
            // retrieved 2021-01-14

            return -125;
        }
        public static DEFAULT_INTEGER DIGITS(float a) {
            // See: https://software.intel.com/content/dam/develop/external/us/en/documents/oneapi_fortran_compiler.pdf
            // page 1131
            // retrieved 2021-01-14

            return 24;
        }
        public static DEFAULT_INTEGER DIGITS(double a) {
            // See: https://software.intel.com/content/dam/develop/external/us/en/documents/oneapi_fortran_compiler.pdf
            // page 1131
            // retrieved 2021-01-14

            return 53;
        }
        public static DEFAULT_INTEGER RADIX(double a) {
            return 2;
        }
        public static DEFAULT_INTEGER RADIX(float a) {
            return 2;
        }
        public static DEFAULT_INTEGER RADIX(long a) {
            return 2;
        }

        public static double HUGE(double a) {
            return double.MaxValue;
        }
        public static float HUGE(float a) {
            return float.MaxValue;
        }

        public static double COS(double a) {
            return Math.Cos(a);
        }
        public static double DCOS(double a) {
            return Math.Cos(a);
        }
        public static float COS(float a) {
            return (float)Math.Cos(a);
        }

        public static float ACOS(float a) {
            return (float)Math.Acos(a);
        }
        public static double ACOS(double a) {
            return Math.Acos(a);
        }
        public static double DACOS(double a) {
            return Math.Acos(a);
        }


        public static double __POW(double a, double b) {
            if (b == 2.0) {
                return a * a;
            }
            return Math.Pow(a, b);
        }
        public static complex __POW(complex a, double b) {
            return complex.Pow(a, b);
        }
        public static complex __POW2(complex a) {
            return a * a;
        }
        public static double __POW2(double a) {
            return a * a;
        }
        public static float __POW2(float a) {
            return a * a;
        }
        public static long __POW2(long a) {
            return a * a;
        }
        public static int __POW2(int a) {
            return a * a;
        }
        public static fcomplex __POW2(fcomplex a) {
            return a * a;
        }
        public static fcomplex __POW(fcomplex a, double b) {
            // TODO: check if another (easier / more straight forward?) implementaion would give better result compatibility with ifort etc.? 
            return fcomplex.Pow(a, b);
        }
        public static float __POW(float a, float b) {
            if (b == 2.0f) {
                return a * a;
            }
            return (float)Math.Pow(a, b);
        }
        public static long __POW(long a, long b) {
            if (a == 2 && Math.Abs(b) < 64) {
                return 1L << (int)b;
            } else if (b == 2.0) {
                return a * a;
            }
            return (long)Math.Pow(a, b);
        }
        public static int __POW(int a, int b) {
            if (a == 2) {
                return (int)(1L << b); 
            } else if (b == 2.0) {
                return a * a;
            }
            return (int)Math.Pow(a, b);
        }

        public static complex DCONJG(fcomplex a) {
            return a.conj;
        }
        public static complex DCONJG(complex a) {
            return a.conj;
        }
        public static fcomplex CONJG(fcomplex a) {
            return a.conj;
        }
        public static complex CONJG(complex a) {
            return a.conj;
        }

        public static fcomplex CMPLX(long real, long imag = 0) {
            return new fcomplex(real, imag);
        }
        public static fcomplex CMPLX(int real, int imag = 0) {
            return new fcomplex(real, imag);
        }
        public static fcomplex CMPLX(float real, float imag = 0) {
            return new fcomplex(real, imag);
        }
        public static fcomplex CMPLX(double real, double imag = 0) {
            return new fcomplex((float)real, (float)imag);
        }
        public static fcomplex CMPLX(complex a) {
            return new fcomplex((float)a.real, (float)a.imag);
        }

        public static complex DCMPLX(long real, long imag = 0) {
            return new complex(real, imag);
        }
        public static complex DCMPLX(int real, int imag = 0) {
            return new complex(real, imag);
        }
        public static complex DCMPLX(float real, float imag = 0) {
            return new complex(real, imag);
        }
        public static complex DCMPLX(double real, double imag = 0) {
            return new complex(real, imag);
        }
        public static complex DCMPLX(complex a) {
            return new complex(a.real, a.imag);
        }

        public static float AIMAG(fcomplex a) {
            return a.imag;
        }
        public static double AIMAG(complex a) {
            return a.imag;
        }
        public static double DIMAG(complex a) {
            return a.imag;
        }

        #region SIN; exp, etc. 

        public static float ATAN(float a) {
            return (float)Math.Atan(a);
        }
        public static double ATAN(double a) {
            return Math.Atan(a);
        }
        public static double DATAN(double a) {
            return Math.Atan(a);
        }

        public static float ATAN2(float a, float b) {
            return (float)Math.Atan2(a, b);
        }
        //public static double ATAN2(double a, float b) {
        //    return Math.Atan2(a, b);
        //}
        public static double ATAN2(double a, double b) {
            return Math.Atan2(a, b);
        }
        public static float SIN(float a) {
            return (float)Math.Sin(a);
        }
        public static double SIN(double a) {
            return Math.Sin(a);
        }
        public static double DSIN(double a) {
            return Math.Sin(a);
        }
        public static fcomplex SIN(fcomplex a) {
            return fcomplex.Sin(a);
        }
        public static complex SIN(complex a) {
            return complex.Sin(a);
        }
        public static float EXP(float a) {
            return (float)Math.Exp(a);
        }
        public static double EXP(double a) {
            return Math.Exp(a);
        }
        public static fcomplex EXP(fcomplex a) {
            return fcomplex.Exp(a);
        }
        public static complex EXP(complex a) {
            return complex.Exp(a);
        }

        #endregion

        #region helper / overrides
        public static float _CPU_TIME_NET_F4() {
            return (float)(Stopwatch.GetTimestamp() / (double)Stopwatch.Frequency);
        }
        public static double _CPU_TIME_NET_F8() {
            return Stopwatch.GetTimestamp() /  (double)Stopwatch.Frequency;
        }
        #endregion

    }
}
