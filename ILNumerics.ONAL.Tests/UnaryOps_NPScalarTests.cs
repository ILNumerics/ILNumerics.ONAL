//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics; 
using static ILNumerics.Globals;
using static ILNumerics.ILMath;

namespace ILNumerics.Core.UnitTests {

    [TestClass]
    public class Unary_NPScalarTests : NumpyTestClass {

        #region HYCALPER LOOPSTART sin-kind
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
Math
</source>
<destination>Math</destination>
<destination>complex</destination>
<destination>fcomplex</destination>
</type>
</hycalper>
*/

        [TestMethod]
        public void NPScalar_acos_double() {

            Array<double> A = (double)1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(acos(A) == (double)Math.Acos(1.0));

            A = (double)0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(acos(A) == (double)Math.Acos(0.0));

        }
        [TestMethod]
        public void NPScalar_asin_double() {

            Array<double> A = (double)1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(asin(A) == (double)Math.Asin(1.0));

            A = (double)0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(asin(A) == (double)Math.Asin(0.0));

        }
        [TestMethod]
        public void NPScalar_atan_double() {

            Array<double> A = (double)1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(atan(A) == (double)Math.Atan(1.0));

            A = (double)0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(atan(A) == (double)Math.Atan(0.0));

        }
        [TestMethod]
        public void NPScalar_ceil_double() {

            Array<double> A = (double)1.1f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(ceil(A) == (double)2.0);

            A = (double)(-1.5f);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(ceil(A) == (double)(-1.0));

        }
        [TestMethod]
        public void NPScalar_cos_double() {

            Array<double> A = (double)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cos(A) == (double)Math.Cos(1.0));

            A = (double)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cos(A) == (double)Math.Cos(0.0));

        }
        [TestMethod]
        public void NPScalar_cosh_double() {

            Array<double> A = (double)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cosh(A) == (double)Math.Cosh(1.0));

            A = (double)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cosh(A) == (double)Math.Cosh(0.0));

        }
        [TestMethod]
        public void NPScalar_exp_double() {

            Array<double> A = (double)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(exp(A) == (double)Math.Exp(1.0));

            A = (double)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(exp(A) == (double)Math.Exp(0.0));

        }
        [TestMethod]
        public void NPScalar_fix_double() {

            Array<double> A = (double)1.1f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(fix(A) == (double)1.0);

            A = (double)(-1.5f);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(fix(A) == (double)(-1.0));

        }
        [TestMethod]
        public void NPScalar_floor_double() {

            Array<double> A = (double)1.1f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(floor(A) == (double)1.0);

            A = (double)(-1.5f);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(floor(A) == (double)(-2.0));

        }

        [TestMethod]
        public void NPScalar_isfinite_double() {

            Array<double> A = (double)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isfinite(A) == true);

            A = (double)Double.PositiveInfinity;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isfinite(A) == false);

        }
        [TestMethod]
        public void NPScalar_isinf_double() {

            Array<double> A = (double)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isfinite(A) == true);

            A = (double)Double.PositiveInfinity;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isfinite(A) == false);

            A = (double)Double.NegativeInfinity;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isfinite(A) == false);

        }
        [TestMethod]
        public void NPScalar_isnan_double() {

            Array<double> A = (double)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isnan(A) == false);

            A = (double)Double.NaN;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isnan(A) == true);

        }
        [TestMethod]
        public void NPScalar_isnegativeinfinity_double() {

            Array<double> A = (double)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isneginf(A) == false);

            A = (double)Double.NegativeInfinity;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isneginf(A) == true);

        }
        [TestMethod]
        public void NPScalar_ispositiveinfinity_double() {

            Array<double> A = (double)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isposinf(A) == false);

            A = (double)Double.PositiveInfinity;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isposinf(A) == true);

        }
        [TestMethod]
        public void NPScalar_log_double() {

            Array<double> A = (double)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(log(A) == (double)Math.Log(1.0));

            A = (double)(-1);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            if (A.IsComplex) {
                Assert.IsTrue(log((complex)A.GetValue(0)) == complex.Log(new complex(-1, 0))); 
            } else {
                Assert.IsTrue(isnan(log(A)));
            }

        }
        [TestMethod]
        public void NPScalar_log10_double() {

            Array<double> A = (double)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(log10(A) == (double)Math.Log10(1.0));

            A = (double)(-1);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            if (A.IsComplex) {
                Assert.IsTrue(log10((complex)A.GetValue(0)) == complex.Log10(new complex(-1, 0)));
            } else {
                Assert.IsTrue(isnan(log10(A)));
            }

        }
        [TestMethod]
        public void NPScalar_round_double() {

            Array<double> A = (double)2.5f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(round(A) == (double)2);

            A = (double)(-2.5);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(round(A) == (double)(-2));

        }
        [TestMethod]
        public void NPScalar_roundp1_double() {

            Array<double> A = (double)2.4119f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(round(A,3) == (double)2.412);

            A = (double)(-2.4119);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(abs(round(A,3) - (double)(-2.412)) < epsf);

        }
        [TestMethod]
        public void NPScalar_sign_double() {

            Array<double> A = (double)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sign(A) == Math.Sign(1.0));

            A = (double)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sign(A) == Math.Sign(0.0));
        }
        [TestMethod]
        public void NPScalar_sin_double() {

            Array<double> A = (double)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sin(A) == (double)Math.Sin(1.0));

            A = (double)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sin(A) == (double)Math.Sin(0.0));
        }
        [TestMethod]
        public void NPScalar_sinh_double() {

            Array<double> A = (double)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sinh(A) == (double)Math.Sinh(1.0));

            A = (double)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sinh(A) == (double)Math.Sinh(0.0));
        }
        [TestMethod]
        public void NPScalar_sqrt_double() {

            Array<double> A = (double)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sqrt(A) == (double)Math.Sqrt(1.0));

            A = (double)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sqrt(A) == (double)Math.Sqrt(0.0));
        }
        [TestMethod]
        public void NPScalar_tan_double() {

            Array<double> A = (double)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(tan(A) == (double)Math.Tan(1.0));

            A = (double)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(tan(A) == (double)Math.Tan(0.0));
        }
        [TestMethod]
        public void NPScalar_tanh_double() {

            Array<double> A = (double)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(tanh(A) == (double)Math.Tanh(1.0));

            A = (double)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(tanh(A) == (double)Math.Tanh(0.0));
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        [TestMethod]
        public void NPScalar_acos_fcomplex() {

            Array<fcomplex> A = (fcomplex)1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(acos(A) == (fcomplex)fcomplex.Acos(1.0));

            A = (fcomplex)0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(acos(A) == (fcomplex)fcomplex.Acos(0.0));

        }
        [TestMethod]
        public void NPScalar_asin_fcomplex() {

            Array<fcomplex> A = (fcomplex)1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(asin(A) == (fcomplex)fcomplex.Asin(1.0));

            A = (fcomplex)0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(asin(A) == (fcomplex)fcomplex.Asin(0.0));

        }
        [TestMethod]
        public void NPScalar_atan_fcomplex() {

            Array<fcomplex> A = (fcomplex)1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(atan(A) == (fcomplex)fcomplex.Atan(1.0));

            A = (fcomplex)0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(atan(A) == (fcomplex)fcomplex.Atan(0.0));

        }
        [TestMethod]
        public void NPScalar_ceil_fcomplex() {

            Array<fcomplex> A = (fcomplex)1.1f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(ceil(A) == (fcomplex)2.0);

            A = (fcomplex)(-1.5f);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(ceil(A) == (fcomplex)(-1.0));

        }
        [TestMethod]
        public void NPScalar_cos_fcomplex() {

            Array<fcomplex> A = (fcomplex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cos(A) == (fcomplex)fcomplex.Cos(1.0));

            A = (fcomplex)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cos(A) == (fcomplex)fcomplex.Cos(0.0));

        }
        [TestMethod]
        public void NPScalar_cosh_fcomplex() {

            Array<fcomplex> A = (fcomplex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cosh(A) == (fcomplex)fcomplex.Cosh(1.0));

            A = (fcomplex)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cosh(A) == (fcomplex)fcomplex.Cosh(0.0));

        }
        [TestMethod]
        public void NPScalar_exp_fcomplex() {

            Array<fcomplex> A = (fcomplex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(exp(A) == (fcomplex)fcomplex.Exp(1.0));

            A = (fcomplex)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(exp(A) == (fcomplex)fcomplex.Exp(0.0));

        }
        [TestMethod]
        public void NPScalar_fix_fcomplex() {

            Array<fcomplex> A = (fcomplex)1.1f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(fix(A) == (fcomplex)1.0);

            A = (fcomplex)(-1.5f);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(fix(A) == (fcomplex)(-1.0));

        }
        [TestMethod]
        public void NPScalar_floor_fcomplex() {

            Array<fcomplex> A = (fcomplex)1.1f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(floor(A) == (fcomplex)1.0);

            A = (fcomplex)(-1.5f);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(floor(A) == (fcomplex)(-2.0));

        }

        [TestMethod]
        public void NPScalar_isfinite_fcomplex() {

            Array<fcomplex> A = (fcomplex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isfinite(A) == true);

            A = (fcomplex)Double.PositiveInfinity;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isfinite(A) == false);

        }
        [TestMethod]
        public void NPScalar_isinf_fcomplex() {

            Array<fcomplex> A = (fcomplex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isfinite(A) == true);

            A = (fcomplex)Double.PositiveInfinity;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isfinite(A) == false);

            A = (fcomplex)Double.NegativeInfinity;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isfinite(A) == false);

        }
        [TestMethod]
        public void NPScalar_isnan_fcomplex() {

            Array<fcomplex> A = (fcomplex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isnan(A) == false);

            A = (fcomplex)Double.NaN;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isnan(A) == true);

        }
        [TestMethod]
        public void NPScalar_isnegativeinfinity_fcomplex() {

            Array<fcomplex> A = (fcomplex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isneginf(A) == false);

            A = (fcomplex)Double.NegativeInfinity;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isneginf(A) == true);

        }
        [TestMethod]
        public void NPScalar_ispositiveinfinity_fcomplex() {

            Array<fcomplex> A = (fcomplex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isposinf(A) == false);

            A = (fcomplex)Double.PositiveInfinity;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isposinf(A) == true);

        }
        [TestMethod]
        public void NPScalar_log_fcomplex() {

            Array<fcomplex> A = (fcomplex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(log(A) == (fcomplex)fcomplex.Log(1.0));

            A = (fcomplex)(-1);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            if (A.IsComplex) {
                Assert.IsTrue(log((complex)A.GetValue(0)) == complex.Log(new complex(-1, 0))); 
            } else {
                Assert.IsTrue(isnan(log(A)));
            }

        }
        [TestMethod]
        public void NPScalar_log10_fcomplex() {

            Array<fcomplex> A = (fcomplex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(log10(A) == (fcomplex)fcomplex.Log10(1.0));

            A = (fcomplex)(-1);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            if (A.IsComplex) {
                Assert.IsTrue(log10((complex)A.GetValue(0)) == complex.Log10(new complex(-1, 0)));
            } else {
                Assert.IsTrue(isnan(log10(A)));
            }

        }
        [TestMethod]
        public void NPScalar_round_fcomplex() {

            Array<fcomplex> A = (fcomplex)2.5f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(round(A) == (fcomplex)2);

            A = (fcomplex)(-2.5);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(round(A) == (fcomplex)(-2));

        }
        [TestMethod]
        public void NPScalar_roundp1_fcomplex() {

            Array<fcomplex> A = (fcomplex)2.4119f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(round(A,3) == (fcomplex)2.412);

            A = (fcomplex)(-2.4119);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(abs(round(A,3) - (fcomplex)(-2.412)) < epsf);

        }
        [TestMethod]
        public void NPScalar_sign_fcomplex() {

            Array<fcomplex> A = (fcomplex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sign(A) == fcomplex.Sign(1.0));

            A = (fcomplex)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sign(A) == fcomplex.Sign(0.0));
        }
        [TestMethod]
        public void NPScalar_sin_fcomplex() {

            Array<fcomplex> A = (fcomplex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sin(A) == (fcomplex)fcomplex.Sin(1.0));

            A = (fcomplex)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sin(A) == (fcomplex)fcomplex.Sin(0.0));
        }
        [TestMethod]
        public void NPScalar_sinh_fcomplex() {

            Array<fcomplex> A = (fcomplex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sinh(A) == (fcomplex)fcomplex.Sinh(1.0));

            A = (fcomplex)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sinh(A) == (fcomplex)fcomplex.Sinh(0.0));
        }
        [TestMethod]
        public void NPScalar_sqrt_fcomplex() {

            Array<fcomplex> A = (fcomplex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sqrt(A) == (fcomplex)fcomplex.Sqrt(1.0));

            A = (fcomplex)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sqrt(A) == (fcomplex)fcomplex.Sqrt(0.0));
        }
        [TestMethod]
        public void NPScalar_tan_fcomplex() {

            Array<fcomplex> A = (fcomplex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(tan(A) == (fcomplex)fcomplex.Tan(1.0));

            A = (fcomplex)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(tan(A) == (fcomplex)fcomplex.Tan(0.0));
        }
        [TestMethod]
        public void NPScalar_tanh_fcomplex() {

            Array<fcomplex> A = (fcomplex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(tanh(A) == (fcomplex)fcomplex.Tanh(1.0));

            A = (fcomplex)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(tanh(A) == (fcomplex)fcomplex.Tanh(0.0));
        }
       

        [TestMethod]
        public void NPScalar_acos_complex() {

            Array<complex> A = (complex)1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(acos(A) == (complex)complex.Acos(1.0));

            A = (complex)0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(acos(A) == (complex)complex.Acos(0.0));

        }
        [TestMethod]
        public void NPScalar_asin_complex() {

            Array<complex> A = (complex)1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(asin(A) == (complex)complex.Asin(1.0));

            A = (complex)0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(asin(A) == (complex)complex.Asin(0.0));

        }
        [TestMethod]
        public void NPScalar_atan_complex() {

            Array<complex> A = (complex)1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(atan(A) == (complex)complex.Atan(1.0));

            A = (complex)0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(atan(A) == (complex)complex.Atan(0.0));

        }
        [TestMethod]
        public void NPScalar_ceil_complex() {

            Array<complex> A = (complex)1.1f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(ceil(A) == (complex)2.0);

            A = (complex)(-1.5f);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(ceil(A) == (complex)(-1.0));

        }
        [TestMethod]
        public void NPScalar_cos_complex() {

            Array<complex> A = (complex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cos(A) == (complex)complex.Cos(1.0));

            A = (complex)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cos(A) == (complex)complex.Cos(0.0));

        }
        [TestMethod]
        public void NPScalar_cosh_complex() {

            Array<complex> A = (complex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cosh(A) == (complex)complex.Cosh(1.0));

            A = (complex)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cosh(A) == (complex)complex.Cosh(0.0));

        }
        [TestMethod]
        public void NPScalar_exp_complex() {

            Array<complex> A = (complex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(exp(A) == (complex)complex.Exp(1.0));

            A = (complex)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(exp(A) == (complex)complex.Exp(0.0));

        }
        [TestMethod]
        public void NPScalar_fix_complex() {

            Array<complex> A = (complex)1.1f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(fix(A) == (complex)1.0);

            A = (complex)(-1.5f);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(fix(A) == (complex)(-1.0));

        }
        [TestMethod]
        public void NPScalar_floor_complex() {

            Array<complex> A = (complex)1.1f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(floor(A) == (complex)1.0);

            A = (complex)(-1.5f);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(floor(A) == (complex)(-2.0));

        }

        [TestMethod]
        public void NPScalar_isfinite_complex() {

            Array<complex> A = (complex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isfinite(A) == true);

            A = (complex)Double.PositiveInfinity;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isfinite(A) == false);

        }
        [TestMethod]
        public void NPScalar_isinf_complex() {

            Array<complex> A = (complex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isfinite(A) == true);

            A = (complex)Double.PositiveInfinity;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isfinite(A) == false);

            A = (complex)Double.NegativeInfinity;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isfinite(A) == false);

        }
        [TestMethod]
        public void NPScalar_isnan_complex() {

            Array<complex> A = (complex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isnan(A) == false);

            A = (complex)Double.NaN;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isnan(A) == true);

        }
        [TestMethod]
        public void NPScalar_isnegativeinfinity_complex() {

            Array<complex> A = (complex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isneginf(A) == false);

            A = (complex)Double.NegativeInfinity;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isneginf(A) == true);

        }
        [TestMethod]
        public void NPScalar_ispositiveinfinity_complex() {

            Array<complex> A = (complex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isposinf(A) == false);

            A = (complex)Double.PositiveInfinity;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isposinf(A) == true);

        }
        [TestMethod]
        public void NPScalar_log_complex() {

            Array<complex> A = (complex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(log(A) == (complex)complex.Log(1.0));

            A = (complex)(-1);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            if (A.IsComplex) {
                Assert.IsTrue(log((complex)A.GetValue(0)) == complex.Log(new complex(-1, 0))); 
            } else {
                Assert.IsTrue(isnan(log(A)));
            }

        }
        [TestMethod]
        public void NPScalar_log10_complex() {

            Array<complex> A = (complex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(log10(A) == (complex)complex.Log10(1.0));

            A = (complex)(-1);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            if (A.IsComplex) {
                Assert.IsTrue(log10((complex)A.GetValue(0)) == complex.Log10(new complex(-1, 0)));
            } else {
                Assert.IsTrue(isnan(log10(A)));
            }

        }
        [TestMethod]
        public void NPScalar_round_complex() {

            Array<complex> A = (complex)2.5f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(round(A) == (complex)2);

            A = (complex)(-2.5);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(round(A) == (complex)(-2));

        }
        [TestMethod]
        public void NPScalar_roundp1_complex() {

            Array<complex> A = (complex)2.4119f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(round(A,3) == (complex)2.412);

            A = (complex)(-2.4119);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(abs(round(A,3) - (complex)(-2.412)) < epsf);

        }
        [TestMethod]
        public void NPScalar_sign_complex() {

            Array<complex> A = (complex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sign(A) == complex.Sign(1.0));

            A = (complex)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sign(A) == complex.Sign(0.0));
        }
        [TestMethod]
        public void NPScalar_sin_complex() {

            Array<complex> A = (complex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sin(A) == (complex)complex.Sin(1.0));

            A = (complex)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sin(A) == (complex)complex.Sin(0.0));
        }
        [TestMethod]
        public void NPScalar_sinh_complex() {

            Array<complex> A = (complex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sinh(A) == (complex)complex.Sinh(1.0));

            A = (complex)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sinh(A) == (complex)complex.Sinh(0.0));
        }
        [TestMethod]
        public void NPScalar_sqrt_complex() {

            Array<complex> A = (complex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sqrt(A) == (complex)complex.Sqrt(1.0));

            A = (complex)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sqrt(A) == (complex)complex.Sqrt(0.0));
        }
        [TestMethod]
        public void NPScalar_tan_complex() {

            Array<complex> A = (complex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(tan(A) == (complex)complex.Tan(1.0));

            A = (complex)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(tan(A) == (complex)complex.Tan(0.0));
        }
        [TestMethod]
        public void NPScalar_tanh_complex() {

            Array<complex> A = (complex)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(tanh(A) == (complex)complex.Tanh(1.0));

            A = (complex)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(tanh(A) == (complex)complex.Tanh(0.0));
        }
       

        [TestMethod]
        public void NPScalar_acos_float() {

            Array<float> A = (float)1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(acos(A) == (float)Math.Acos(1.0));

            A = (float)0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(acos(A) == (float)Math.Acos(0.0));

        }
        [TestMethod]
        public void NPScalar_asin_float() {

            Array<float> A = (float)1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(asin(A) == (float)Math.Asin(1.0));

            A = (float)0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(asin(A) == (float)Math.Asin(0.0));

        }
        [TestMethod]
        public void NPScalar_atan_float() {

            Array<float> A = (float)1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(atan(A) == (float)Math.Atan(1.0));

            A = (float)0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(atan(A) == (float)Math.Atan(0.0));

        }
        [TestMethod]
        public void NPScalar_ceil_float() {

            Array<float> A = (float)1.1f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(ceil(A) == (float)2.0);

            A = (float)(-1.5f);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(ceil(A) == (float)(-1.0));

        }
        [TestMethod]
        public void NPScalar_cos_float() {

            Array<float> A = (float)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cos(A) == (float)Math.Cos(1.0));

            A = (float)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cos(A) == (float)Math.Cos(0.0));

        }
        [TestMethod]
        public void NPScalar_cosh_float() {

            Array<float> A = (float)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cosh(A) == (float)Math.Cosh(1.0));

            A = (float)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cosh(A) == (float)Math.Cosh(0.0));

        }
        [TestMethod]
        public void NPScalar_exp_float() {

            Array<float> A = (float)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(exp(A) == (float)Math.Exp(1.0));

            A = (float)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(exp(A) == (float)Math.Exp(0.0));

        }
        [TestMethod]
        public void NPScalar_fix_float() {

            Array<float> A = (float)1.1f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(fix(A) == (float)1.0);

            A = (float)(-1.5f);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(fix(A) == (float)(-1.0));

        }
        [TestMethod]
        public void NPScalar_floor_float() {

            Array<float> A = (float)1.1f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(floor(A) == (float)1.0);

            A = (float)(-1.5f);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(floor(A) == (float)(-2.0));

        }

        [TestMethod]
        public void NPScalar_isfinite_float() {

            Array<float> A = (float)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isfinite(A) == true);

            A = (float)Double.PositiveInfinity;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isfinite(A) == false);

        }
        [TestMethod]
        public void NPScalar_isinf_float() {

            Array<float> A = (float)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isfinite(A) == true);

            A = (float)Double.PositiveInfinity;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isfinite(A) == false);

            A = (float)Double.NegativeInfinity;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isfinite(A) == false);

        }
        [TestMethod]
        public void NPScalar_isnan_float() {

            Array<float> A = (float)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isnan(A) == false);

            A = (float)Double.NaN;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isnan(A) == true);

        }
        [TestMethod]
        public void NPScalar_isnegativeinfinity_float() {

            Array<float> A = (float)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isneginf(A) == false);

            A = (float)Double.NegativeInfinity;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isneginf(A) == true);

        }
        [TestMethod]
        public void NPScalar_ispositiveinfinity_float() {

            Array<float> A = (float)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isposinf(A) == false);

            A = (float)Double.PositiveInfinity;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(isposinf(A) == true);

        }
        [TestMethod]
        public void NPScalar_log_float() {

            Array<float> A = (float)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(log(A) == (float)Math.Log(1.0));

            A = (float)(-1);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            if (A.IsComplex) {
                Assert.IsTrue(log((complex)A.GetValue(0)) == complex.Log(new complex(-1, 0))); 
            } else {
                Assert.IsTrue(isnan(log(A)));
            }

        }
        [TestMethod]
        public void NPScalar_log10_float() {

            Array<float> A = (float)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(log10(A) == (float)Math.Log10(1.0));

            A = (float)(-1);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            if (A.IsComplex) {
                Assert.IsTrue(log10((complex)A.GetValue(0)) == complex.Log10(new complex(-1, 0)));
            } else {
                Assert.IsTrue(isnan(log10(A)));
            }

        }
        [TestMethod]
        public void NPScalar_round_float() {

            Array<float> A = (float)2.5f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(round(A) == (float)2);

            A = (float)(-2.5);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(round(A) == (float)(-2));

        }
        [TestMethod]
        public void NPScalar_roundp1_float() {

            Array<float> A = (float)2.4119f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(round(A,3) == (float)2.412);

            A = (float)(-2.4119);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(abs(round(A,3) - (float)(-2.412)) < epsf);

        }
        [TestMethod]
        public void NPScalar_sign_float() {

            Array<float> A = (float)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sign(A) == Math.Sign(1.0));

            A = (float)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sign(A) == Math.Sign(0.0));
        }
        [TestMethod]
        public void NPScalar_sin_float() {

            Array<float> A = (float)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sin(A) == (float)Math.Sin(1.0));

            A = (float)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sin(A) == (float)Math.Sin(0.0));
        }
        [TestMethod]
        public void NPScalar_sinh_float() {

            Array<float> A = (float)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sinh(A) == (float)Math.Sinh(1.0));

            A = (float)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sinh(A) == (float)Math.Sinh(0.0));
        }
        [TestMethod]
        public void NPScalar_sqrt_float() {

            Array<float> A = (float)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sqrt(A) == (float)Math.Sqrt(1.0));

            A = (float)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sqrt(A) == (float)Math.Sqrt(0.0));
        }
        [TestMethod]
        public void NPScalar_tan_float() {

            Array<float> A = (float)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(tan(A) == (float)Math.Tan(1.0));

            A = (float)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(tan(A) == (float)Math.Tan(0.0));
        }
        [TestMethod]
        public void NPScalar_tanh_float() {

            Array<float> A = (float)1.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(tanh(A) == (float)Math.Tanh(1.0));

            A = (float)0.0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(tanh(A) == (float)Math.Tanh(0.0));
        }

#endregion HYCALPER AUTO GENERATED CODE

        [TestMethod]
        public void NPScalar_abs_double() {

            Array<double> A = -1.1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(abs(A) == 1.1);
        }
        [TestMethod]
        public void NPScalar_abs_float() {

            Array<float> A = -1.1f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(abs(A) == 1.1f);
        }
        [TestMethod]
        public void NPScalar_abs_fcomplex() {

            Array<fcomplex> A = new fcomplex(-1.1f, -epsf);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(abs(A).GetValue(0) == (float)Math.Sqrt(-1.1f * -1.1f + -epsf * -epsf));
        }
        [TestMethod] 
        public void NPScalar_abs_complex() {

            Array<complex> A = new complex(-1.1f, -epsf);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(Math.Abs(abs(A).GetValue(0) - Math.Sqrt(-1.1f * -1.1f + -epsf * -epsf)) < epsf);
        }
        #region HYCALPER LOOPSTART BITNEG
        /*!HC:TYPELIST:
<hycalper>
    <type>
    <source locate="here">
        sbyte
    </source>
        <destination>byte</destination>
        <destination>short</destination>
        <destination>ushort</destination>
        <destination>int</destination>
        <destination>uint</destination>
        <destination>long</destination>
        <destination>ulong</destination>
    </type>
    <type>
    <source locate="here">
        -2
    </source>
        <destination>byte.MaxValue - 1</destination>
        <destination>-2</destination>
        <destination>ushort.MaxValue - 1</destination>
        <destination>-2</destination>
        <destination>uint.MaxValue - 1</destination>
        <destination>-2</destination>
        <destination>ulong.MaxValue - 1</destination>
    </type>
</hycalper>
        */

        [TestMethod] 
        public void NPScalar_bitneg_sbyte() {

            Array<sbyte> A = (sbyte)1; 
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(bitneg(A).GetValue(0) == -2);
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        [TestMethod] 
        public void NPScalar_bitneg_ulong() {

            Array<ulong> A = (ulong)1; 
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(bitneg(A).GetValue(0) == ulong.MaxValue - 1);
        }
       

        [TestMethod] 
        public void NPScalar_bitneg_long() {

            Array<long> A = (long)1; 
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(bitneg(A).GetValue(0) == -2);
        }
       

        [TestMethod] 
        public void NPScalar_bitneg_uint() {

            Array<uint> A = (uint)1; 
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(bitneg(A).GetValue(0) == uint.MaxValue - 1);
        }
       

        [TestMethod] 
        public void NPScalar_bitneg_int() {

            Array<int> A = (int)1; 
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(bitneg(A).GetValue(0) == -2);
        }
       

        [TestMethod] 
        public void NPScalar_bitneg_ushort() {

            Array<ushort> A = (ushort)1; 
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(bitneg(A).GetValue(0) == ushort.MaxValue - 1);
        }
       

        [TestMethod] 
        public void NPScalar_bitneg_short() {

            Array<short> A = (short)1; 
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(bitneg(A).GetValue(0) == -2);
        }
       

        [TestMethod] 
        public void NPScalar_bitneg_byte() {

            Array<byte> A = (byte)1; 
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(bitneg(A).GetValue(0) == byte.MaxValue - 1);
        }

#endregion HYCALPER AUTO GENERATED CODE

        #region HYCALPER LOOPSTART conj, conj
        /*!HC:TYPELIST:
<hycalper>
    <type>
    <source locate="here">
        complex
    </source>
        <destination>fcomplex</destination>
    </type>
    <type>
    <source locate="here">
        -2
    </source>
        <destination>byte.MaxValue - 1</destination>
    </type>
</hycalper>
        */
        [TestMethod]
        public void NPScalar_conj_inpl_complex() {

            Array<complex> A = new complex(1,-14);

            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Array<complex> B = A.C;
            B.conj(); 
            Assert.IsTrue(conj(A) == new complex(A.GetValue(0).real, -A.GetValue(0).imag));
            Assert.IsTrue(conj(A) == B);
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       
        [TestMethod]
        public void NPScalar_conj_inpl_fcomplex() {

            Array<fcomplex> A = new fcomplex(1,-14);

            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Array<fcomplex> B = A.C;
            B.conj(); 
            Assert.IsTrue(conj(A) == new fcomplex(A.GetValue(0).real, -A.GetValue(0).imag));
            Assert.IsTrue(conj(A) == B);
        }

#endregion HYCALPER AUTO GENERATED CODE

        [TestMethod]
        public void NPScalar_not_bool() {

            Logical a = true;
            Assert.IsTrue(a.S.NumberOfDimensions == 0);
            Assert.IsFalse(not(a));

        }
        [TestMethod]
        public void NPScalar_negate_alltypes() {

            Array<double> a = -1.1; 
            Assert.IsTrue(a.S.NumberOfDimensions == 0); 

            Assert.IsTrue(negate(-1.2) == 1.2);
            Assert.IsTrue(negate((Array<float>)(-1.1f)) == 1.1f);
            Assert.IsTrue(negate((Array<long>) (-13L)) == 13);
            Assert.IsTrue(negate((Array<int>)(-15)) == 15);
            Assert.IsTrue(negate((Array<short>)(-16)) == 16);
            Assert.IsTrue(negate((Array<sbyte>)(-18)) == 18);

        }

        #region HYCALPER LOOPSTART to...
        /*!HC:TYPELIST:
<hycalper>
<type>
<source locate="here">
Double
</source>
<destination>float</destination>
<destination>complex</destination>
<destination>fcomplex</destination>
<destination>sbyte</destination>
<destination>byte</destination>
<destination>short</destination>
<destination>ushort</destination>
<destination>int</destination>
<destination>uint</destination>
<destination>long</destination>
<destination>ulong</destination>
</type>
<type>
<source locate="here">
1.0
</source>
<destination>1f</destination>
<destination>new complex(1.0, 0)</destination>
<destination>new fcomplex(1f, 0)</destination>
<destination>1</destination>
<destination>1</destination>
<destination>1</destination>
<destination>1</destination>
<destination>1</destination>
<destination>1</destination>
<destination>1</destination>
<destination>1</destination>
</type>
</hycalper>
*/

        [TestMethod]
        public void NPScalar_To_FromDouble() {

            Array<Double> A = 1.0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            var b = todouble(A).GetValue(0);
            Assert.IsTrue(b.GetType() == typeof(double));
            Assert.IsTrue(b == 1);

            var c = tosingle(A).GetValue(0);
            Assert.IsTrue(c.GetType() == typeof(float));
            Assert.IsTrue(c == 1);

            var d = touint64(A).GetValue(0);
            Assert.IsTrue(d.GetType() == typeof(ulong));
            Assert.IsTrue(d == 1);

            var e = touint32(A).GetValue(0);
            Assert.IsTrue(e.GetType() == typeof(uint));
            Assert.IsTrue(e == 1);

            var f = touint16(A).GetValue(0);
            Assert.IsTrue(f.GetType() == typeof(ushort));
            Assert.IsTrue(f == 1);

            var g = touint8(A).GetValue(0);
            Assert.IsTrue(g.GetType() == typeof(byte));
            Assert.IsTrue(g == 1);

            var h = toint64(A).GetValue(0);
            Assert.IsTrue(h.GetType() == typeof(long));
            Assert.IsTrue(h == 1);

            var i = toint32(A).GetValue(0);
            Assert.IsTrue(i.GetType() == typeof(int));
            Assert.IsTrue(i == 1);

            var j = toint16(A).GetValue(0);
            Assert.IsTrue(j.GetType() == typeof(short));
            Assert.IsTrue(j == 1);

            var k = toint8(A).GetValue(0);
            Assert.IsTrue(k.GetType() == typeof(sbyte));
            Assert.IsTrue(k == 1);

        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        [TestMethod]
        public void NPScalar_To_Fromulong() {

            Array<ulong> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            var b = todouble(A).GetValue(0);
            Assert.IsTrue(b.GetType() == typeof(double));
            Assert.IsTrue(b == 1);

            var c = tosingle(A).GetValue(0);
            Assert.IsTrue(c.GetType() == typeof(float));
            Assert.IsTrue(c == 1);

            var d = touint64(A).GetValue(0);
            Assert.IsTrue(d.GetType() == typeof(ulong));
            Assert.IsTrue(d == 1);

            var e = touint32(A).GetValue(0);
            Assert.IsTrue(e.GetType() == typeof(uint));
            Assert.IsTrue(e == 1);

            var f = touint16(A).GetValue(0);
            Assert.IsTrue(f.GetType() == typeof(ushort));
            Assert.IsTrue(f == 1);

            var g = touint8(A).GetValue(0);
            Assert.IsTrue(g.GetType() == typeof(byte));
            Assert.IsTrue(g == 1);

            var h = toint64(A).GetValue(0);
            Assert.IsTrue(h.GetType() == typeof(long));
            Assert.IsTrue(h == 1);

            var i = toint32(A).GetValue(0);
            Assert.IsTrue(i.GetType() == typeof(int));
            Assert.IsTrue(i == 1);

            var j = toint16(A).GetValue(0);
            Assert.IsTrue(j.GetType() == typeof(short));
            Assert.IsTrue(j == 1);

            var k = toint8(A).GetValue(0);
            Assert.IsTrue(k.GetType() == typeof(sbyte));
            Assert.IsTrue(k == 1);

        }
       

        [TestMethod]
        public void NPScalar_To_Fromlong() {

            Array<long> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            var b = todouble(A).GetValue(0);
            Assert.IsTrue(b.GetType() == typeof(double));
            Assert.IsTrue(b == 1);

            var c = tosingle(A).GetValue(0);
            Assert.IsTrue(c.GetType() == typeof(float));
            Assert.IsTrue(c == 1);

            var d = touint64(A).GetValue(0);
            Assert.IsTrue(d.GetType() == typeof(ulong));
            Assert.IsTrue(d == 1);

            var e = touint32(A).GetValue(0);
            Assert.IsTrue(e.GetType() == typeof(uint));
            Assert.IsTrue(e == 1);

            var f = touint16(A).GetValue(0);
            Assert.IsTrue(f.GetType() == typeof(ushort));
            Assert.IsTrue(f == 1);

            var g = touint8(A).GetValue(0);
            Assert.IsTrue(g.GetType() == typeof(byte));
            Assert.IsTrue(g == 1);

            var h = toint64(A).GetValue(0);
            Assert.IsTrue(h.GetType() == typeof(long));
            Assert.IsTrue(h == 1);

            var i = toint32(A).GetValue(0);
            Assert.IsTrue(i.GetType() == typeof(int));
            Assert.IsTrue(i == 1);

            var j = toint16(A).GetValue(0);
            Assert.IsTrue(j.GetType() == typeof(short));
            Assert.IsTrue(j == 1);

            var k = toint8(A).GetValue(0);
            Assert.IsTrue(k.GetType() == typeof(sbyte));
            Assert.IsTrue(k == 1);

        }
       

        [TestMethod]
        public void NPScalar_To_Fromuint() {

            Array<uint> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            var b = todouble(A).GetValue(0);
            Assert.IsTrue(b.GetType() == typeof(double));
            Assert.IsTrue(b == 1);

            var c = tosingle(A).GetValue(0);
            Assert.IsTrue(c.GetType() == typeof(float));
            Assert.IsTrue(c == 1);

            var d = touint64(A).GetValue(0);
            Assert.IsTrue(d.GetType() == typeof(ulong));
            Assert.IsTrue(d == 1);

            var e = touint32(A).GetValue(0);
            Assert.IsTrue(e.GetType() == typeof(uint));
            Assert.IsTrue(e == 1);

            var f = touint16(A).GetValue(0);
            Assert.IsTrue(f.GetType() == typeof(ushort));
            Assert.IsTrue(f == 1);

            var g = touint8(A).GetValue(0);
            Assert.IsTrue(g.GetType() == typeof(byte));
            Assert.IsTrue(g == 1);

            var h = toint64(A).GetValue(0);
            Assert.IsTrue(h.GetType() == typeof(long));
            Assert.IsTrue(h == 1);

            var i = toint32(A).GetValue(0);
            Assert.IsTrue(i.GetType() == typeof(int));
            Assert.IsTrue(i == 1);

            var j = toint16(A).GetValue(0);
            Assert.IsTrue(j.GetType() == typeof(short));
            Assert.IsTrue(j == 1);

            var k = toint8(A).GetValue(0);
            Assert.IsTrue(k.GetType() == typeof(sbyte));
            Assert.IsTrue(k == 1);

        }
       

        [TestMethod]
        public void NPScalar_To_Fromint() {

            Array<int> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            var b = todouble(A).GetValue(0);
            Assert.IsTrue(b.GetType() == typeof(double));
            Assert.IsTrue(b == 1);

            var c = tosingle(A).GetValue(0);
            Assert.IsTrue(c.GetType() == typeof(float));
            Assert.IsTrue(c == 1);

            var d = touint64(A).GetValue(0);
            Assert.IsTrue(d.GetType() == typeof(ulong));
            Assert.IsTrue(d == 1);

            var e = touint32(A).GetValue(0);
            Assert.IsTrue(e.GetType() == typeof(uint));
            Assert.IsTrue(e == 1);

            var f = touint16(A).GetValue(0);
            Assert.IsTrue(f.GetType() == typeof(ushort));
            Assert.IsTrue(f == 1);

            var g = touint8(A).GetValue(0);
            Assert.IsTrue(g.GetType() == typeof(byte));
            Assert.IsTrue(g == 1);

            var h = toint64(A).GetValue(0);
            Assert.IsTrue(h.GetType() == typeof(long));
            Assert.IsTrue(h == 1);

            var i = toint32(A).GetValue(0);
            Assert.IsTrue(i.GetType() == typeof(int));
            Assert.IsTrue(i == 1);

            var j = toint16(A).GetValue(0);
            Assert.IsTrue(j.GetType() == typeof(short));
            Assert.IsTrue(j == 1);

            var k = toint8(A).GetValue(0);
            Assert.IsTrue(k.GetType() == typeof(sbyte));
            Assert.IsTrue(k == 1);

        }
       

        [TestMethod]
        public void NPScalar_To_Fromushort() {

            Array<ushort> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            var b = todouble(A).GetValue(0);
            Assert.IsTrue(b.GetType() == typeof(double));
            Assert.IsTrue(b == 1);

            var c = tosingle(A).GetValue(0);
            Assert.IsTrue(c.GetType() == typeof(float));
            Assert.IsTrue(c == 1);

            var d = touint64(A).GetValue(0);
            Assert.IsTrue(d.GetType() == typeof(ulong));
            Assert.IsTrue(d == 1);

            var e = touint32(A).GetValue(0);
            Assert.IsTrue(e.GetType() == typeof(uint));
            Assert.IsTrue(e == 1);

            var f = touint16(A).GetValue(0);
            Assert.IsTrue(f.GetType() == typeof(ushort));
            Assert.IsTrue(f == 1);

            var g = touint8(A).GetValue(0);
            Assert.IsTrue(g.GetType() == typeof(byte));
            Assert.IsTrue(g == 1);

            var h = toint64(A).GetValue(0);
            Assert.IsTrue(h.GetType() == typeof(long));
            Assert.IsTrue(h == 1);

            var i = toint32(A).GetValue(0);
            Assert.IsTrue(i.GetType() == typeof(int));
            Assert.IsTrue(i == 1);

            var j = toint16(A).GetValue(0);
            Assert.IsTrue(j.GetType() == typeof(short));
            Assert.IsTrue(j == 1);

            var k = toint8(A).GetValue(0);
            Assert.IsTrue(k.GetType() == typeof(sbyte));
            Assert.IsTrue(k == 1);

        }
       

        [TestMethod]
        public void NPScalar_To_Fromshort() {

            Array<short> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            var b = todouble(A).GetValue(0);
            Assert.IsTrue(b.GetType() == typeof(double));
            Assert.IsTrue(b == 1);

            var c = tosingle(A).GetValue(0);
            Assert.IsTrue(c.GetType() == typeof(float));
            Assert.IsTrue(c == 1);

            var d = touint64(A).GetValue(0);
            Assert.IsTrue(d.GetType() == typeof(ulong));
            Assert.IsTrue(d == 1);

            var e = touint32(A).GetValue(0);
            Assert.IsTrue(e.GetType() == typeof(uint));
            Assert.IsTrue(e == 1);

            var f = touint16(A).GetValue(0);
            Assert.IsTrue(f.GetType() == typeof(ushort));
            Assert.IsTrue(f == 1);

            var g = touint8(A).GetValue(0);
            Assert.IsTrue(g.GetType() == typeof(byte));
            Assert.IsTrue(g == 1);

            var h = toint64(A).GetValue(0);
            Assert.IsTrue(h.GetType() == typeof(long));
            Assert.IsTrue(h == 1);

            var i = toint32(A).GetValue(0);
            Assert.IsTrue(i.GetType() == typeof(int));
            Assert.IsTrue(i == 1);

            var j = toint16(A).GetValue(0);
            Assert.IsTrue(j.GetType() == typeof(short));
            Assert.IsTrue(j == 1);

            var k = toint8(A).GetValue(0);
            Assert.IsTrue(k.GetType() == typeof(sbyte));
            Assert.IsTrue(k == 1);

        }
       

        [TestMethod]
        public void NPScalar_To_Frombyte() {

            Array<byte> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            var b = todouble(A).GetValue(0);
            Assert.IsTrue(b.GetType() == typeof(double));
            Assert.IsTrue(b == 1);

            var c = tosingle(A).GetValue(0);
            Assert.IsTrue(c.GetType() == typeof(float));
            Assert.IsTrue(c == 1);

            var d = touint64(A).GetValue(0);
            Assert.IsTrue(d.GetType() == typeof(ulong));
            Assert.IsTrue(d == 1);

            var e = touint32(A).GetValue(0);
            Assert.IsTrue(e.GetType() == typeof(uint));
            Assert.IsTrue(e == 1);

            var f = touint16(A).GetValue(0);
            Assert.IsTrue(f.GetType() == typeof(ushort));
            Assert.IsTrue(f == 1);

            var g = touint8(A).GetValue(0);
            Assert.IsTrue(g.GetType() == typeof(byte));
            Assert.IsTrue(g == 1);

            var h = toint64(A).GetValue(0);
            Assert.IsTrue(h.GetType() == typeof(long));
            Assert.IsTrue(h == 1);

            var i = toint32(A).GetValue(0);
            Assert.IsTrue(i.GetType() == typeof(int));
            Assert.IsTrue(i == 1);

            var j = toint16(A).GetValue(0);
            Assert.IsTrue(j.GetType() == typeof(short));
            Assert.IsTrue(j == 1);

            var k = toint8(A).GetValue(0);
            Assert.IsTrue(k.GetType() == typeof(sbyte));
            Assert.IsTrue(k == 1);

        }
       

        [TestMethod]
        public void NPScalar_To_Fromsbyte() {

            Array<sbyte> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            var b = todouble(A).GetValue(0);
            Assert.IsTrue(b.GetType() == typeof(double));
            Assert.IsTrue(b == 1);

            var c = tosingle(A).GetValue(0);
            Assert.IsTrue(c.GetType() == typeof(float));
            Assert.IsTrue(c == 1);

            var d = touint64(A).GetValue(0);
            Assert.IsTrue(d.GetType() == typeof(ulong));
            Assert.IsTrue(d == 1);

            var e = touint32(A).GetValue(0);
            Assert.IsTrue(e.GetType() == typeof(uint));
            Assert.IsTrue(e == 1);

            var f = touint16(A).GetValue(0);
            Assert.IsTrue(f.GetType() == typeof(ushort));
            Assert.IsTrue(f == 1);

            var g = touint8(A).GetValue(0);
            Assert.IsTrue(g.GetType() == typeof(byte));
            Assert.IsTrue(g == 1);

            var h = toint64(A).GetValue(0);
            Assert.IsTrue(h.GetType() == typeof(long));
            Assert.IsTrue(h == 1);

            var i = toint32(A).GetValue(0);
            Assert.IsTrue(i.GetType() == typeof(int));
            Assert.IsTrue(i == 1);

            var j = toint16(A).GetValue(0);
            Assert.IsTrue(j.GetType() == typeof(short));
            Assert.IsTrue(j == 1);

            var k = toint8(A).GetValue(0);
            Assert.IsTrue(k.GetType() == typeof(sbyte));
            Assert.IsTrue(k == 1);

        }
       

        [TestMethod]
        public void NPScalar_To_Fromfcomplex() {

            Array<fcomplex> A = new fcomplex(1f, 0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            var b = todouble(A).GetValue(0);
            Assert.IsTrue(b.GetType() == typeof(double));
            Assert.IsTrue(b == 1);

            var c = tosingle(A).GetValue(0);
            Assert.IsTrue(c.GetType() == typeof(float));
            Assert.IsTrue(c == 1);

            var d = touint64(A).GetValue(0);
            Assert.IsTrue(d.GetType() == typeof(ulong));
            Assert.IsTrue(d == 1);

            var e = touint32(A).GetValue(0);
            Assert.IsTrue(e.GetType() == typeof(uint));
            Assert.IsTrue(e == 1);

            var f = touint16(A).GetValue(0);
            Assert.IsTrue(f.GetType() == typeof(ushort));
            Assert.IsTrue(f == 1);

            var g = touint8(A).GetValue(0);
            Assert.IsTrue(g.GetType() == typeof(byte));
            Assert.IsTrue(g == 1);

            var h = toint64(A).GetValue(0);
            Assert.IsTrue(h.GetType() == typeof(long));
            Assert.IsTrue(h == 1);

            var i = toint32(A).GetValue(0);
            Assert.IsTrue(i.GetType() == typeof(int));
            Assert.IsTrue(i == 1);

            var j = toint16(A).GetValue(0);
            Assert.IsTrue(j.GetType() == typeof(short));
            Assert.IsTrue(j == 1);

            var k = toint8(A).GetValue(0);
            Assert.IsTrue(k.GetType() == typeof(sbyte));
            Assert.IsTrue(k == 1);

        }
       

        [TestMethod]
        public void NPScalar_To_Fromcomplex() {

            Array<complex> A = new complex(1.0, 0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            var b = todouble(A).GetValue(0);
            Assert.IsTrue(b.GetType() == typeof(double));
            Assert.IsTrue(b == 1);

            var c = tosingle(A).GetValue(0);
            Assert.IsTrue(c.GetType() == typeof(float));
            Assert.IsTrue(c == 1);

            var d = touint64(A).GetValue(0);
            Assert.IsTrue(d.GetType() == typeof(ulong));
            Assert.IsTrue(d == 1);

            var e = touint32(A).GetValue(0);
            Assert.IsTrue(e.GetType() == typeof(uint));
            Assert.IsTrue(e == 1);

            var f = touint16(A).GetValue(0);
            Assert.IsTrue(f.GetType() == typeof(ushort));
            Assert.IsTrue(f == 1);

            var g = touint8(A).GetValue(0);
            Assert.IsTrue(g.GetType() == typeof(byte));
            Assert.IsTrue(g == 1);

            var h = toint64(A).GetValue(0);
            Assert.IsTrue(h.GetType() == typeof(long));
            Assert.IsTrue(h == 1);

            var i = toint32(A).GetValue(0);
            Assert.IsTrue(i.GetType() == typeof(int));
            Assert.IsTrue(i == 1);

            var j = toint16(A).GetValue(0);
            Assert.IsTrue(j.GetType() == typeof(short));
            Assert.IsTrue(j == 1);

            var k = toint8(A).GetValue(0);
            Assert.IsTrue(k.GetType() == typeof(sbyte));
            Assert.IsTrue(k == 1);

        }
       

        [TestMethod]
        public void NPScalar_To_Fromfloat() {

            Array<float> A = 1f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            var b = todouble(A).GetValue(0);
            Assert.IsTrue(b.GetType() == typeof(double));
            Assert.IsTrue(b == 1);

            var c = tosingle(A).GetValue(0);
            Assert.IsTrue(c.GetType() == typeof(float));
            Assert.IsTrue(c == 1);

            var d = touint64(A).GetValue(0);
            Assert.IsTrue(d.GetType() == typeof(ulong));
            Assert.IsTrue(d == 1);

            var e = touint32(A).GetValue(0);
            Assert.IsTrue(e.GetType() == typeof(uint));
            Assert.IsTrue(e == 1);

            var f = touint16(A).GetValue(0);
            Assert.IsTrue(f.GetType() == typeof(ushort));
            Assert.IsTrue(f == 1);

            var g = touint8(A).GetValue(0);
            Assert.IsTrue(g.GetType() == typeof(byte));
            Assert.IsTrue(g == 1);

            var h = toint64(A).GetValue(0);
            Assert.IsTrue(h.GetType() == typeof(long));
            Assert.IsTrue(h == 1);

            var i = toint32(A).GetValue(0);
            Assert.IsTrue(i.GetType() == typeof(int));
            Assert.IsTrue(i == 1);

            var j = toint16(A).GetValue(0);
            Assert.IsTrue(j.GetType() == typeof(short));
            Assert.IsTrue(j == 1);

            var k = toint8(A).GetValue(0);
            Assert.IsTrue(k.GetType() == typeof(sbyte));
            Assert.IsTrue(k == 1);

        }

#endregion HYCALPER AUTO GENERATED CODE

    }
}
