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

#pragma warning disable CS0164, CS0219, CS0162
#if !OBSOLETE
using System;
using System.Security;
using System.IO;
using System.Collections.Generic;
using ILNumerics.F2NET.Formatting;
using ILNumerics.Core.MemoryLayer;
using ILNumerics.Core.Runtime;
using static ILNumerics.F2NET.Intrinsics; 
using static ILNumerics.F2NET.Array.Intrinsics; 
using System.Runtime.CompilerServices; 
using static ILNumerics.Globals;
using ILNumerics.F2NET.Array; 

namespace ILNumerics.F2NET { 
public static unsafe partial class LAPACK {
//*> \brief \b SSTERF 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SSTERF + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/ssterf.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/ssterf.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/ssterf.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SSTERF( N, D, E, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INFO, N 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               D( * ), E( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SSTERF computes all eigenvalues of a symmetric tridiagonal matrix 
//*> using the Pal-Walker-Kahan variant of the QL or QR algorithm. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix.  N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] D 
//*> \verbatim 
//*>          D is REAL array, dimension (N) 
//*>          On entry, the n diagonal elements of the tridiagonal matrix. 
//*>          On exit, if INFO = 0, the eigenvalues in ascending order. 
//*> \endverbatim 
//*> 
//*> \param[in,out] E 
//*> \verbatim 
//*>          E is REAL array, dimension (N-1) 
//*>          On entry, the (n-1) subdiagonal elements of the tridiagonal 
//*>          matrix. 
//*>          On exit, E has been destroyed. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit 
//*>          < 0:  if INFO = -i, the i-th argument had an illegal value 
//*>          > 0:  the algorithm failed to find all of the eigenvalues in 
//*>                a total of 30*N iterations; if INFO = i, then i 
//*>                elements of E have not converged to zero. 
//*> \endverbatim 
//* 
//*  Authors: 
//*  ======== 
//* 
//*> \author Univ. of Tennessee 
//*> \author Univ. of California Berkeley 
//*> \author Univ. of Colorado Denver 
//*> \author NAG Ltd. 
//* 
//*> \date December 2016 
//* 
//*> \ingroup auxOTHERcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _5rm4mqbz(ref Int32 _dxpq0xkr, Single* _plfm7z8g, Single* _864fslqq, ref Int32 _gro5yvfo)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Single _kxg5drh2 =  1f;
Single _5m0mjfxm =  2f;
Single _08e01ee2 =  3f;
Int32 _gaia76w5 =  (int)30;
Int32 _b5p6od9s =  default;
Int32 _g5graale =  default;
Int32 _jjlk8rbf =  default;
Int32 _68ec3gbh =  default;
Int32 _135aegwf =  default;
Int32 _1mp99t47 =  default;
Int32 _s1sstu6z =  default;
Int32 _clabrarh =  default;
Int32 _ev4xhht5 =  default;
Int32 _07mc27rj =  default;
Single _r7cfteg3 =  default;
Single _b8rxgs6o =  default;
Single _uv0s0qmf =  default;
Single _3crf0qn3 =  default;
Single _p1iqarg6 =  default;
Single _okg6tegz =  default;
Single _zf88apxo =  default;
Single _oo1g9svh =  default;
Single _6g82fvy1 =  default;
Single _ejwydfmr =  default;
Single _q2vwp05i =  default;
Single _wwj82gep =  default;
Single _uwqai9pg =  default;
Single _4hich2eu =  default;
Single _irk8i6qr =  default;
Single _odf6ja0t =  default;
Single _h75qnr7l =  default;
Single _91a1vq5f =  default;
Single _jjejn9g8 =  default;
Single _flwdikii =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK computational routine (version 3.7.0) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     December 2016 
		//* 
		//*     .. Scalar Arguments .. 
		//*     .. 
		//*     .. Array Arguments .. 
		//*     .. 
		//* 
		//*  ===================================================================== 
		//* 
		//*     .. Parameters .. 
		//*     .. 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input parameters. 
		//* 
		
		_gro5yvfo = (int)0;//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-1;
			_ut9qalzx("SSTERF" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		
		if (_dxpq0xkr <= (int)1)
		return;//* 
		//*     Determine the unit roundoff for this environment. 
		//* 
		
		_p1iqarg6 = _d5tu038y("E" );
		_okg6tegz = __POW2(_p1iqarg6);
		_h75qnr7l = _d5tu038y("S" );
		_odf6ja0t = (_kxg5drh2 / _h75qnr7l);
		_jjejn9g8 = (ILNumerics.F2NET.Intrinsics.SQRT(_odf6ja0t ) / _08e01ee2);
		_flwdikii = (ILNumerics.F2NET.Intrinsics.SQRT(_h75qnr7l ) / _okg6tegz);//* 
		//*     Compute the eigenvalues of the tridiagonal matrix. 
		//* 
		
		_07mc27rj = (_dxpq0xkr * _gaia76w5);
		_91a1vq5f = _d0547bi2;
		_jjlk8rbf = (int)0;//* 
		//*     Determine where the matrix splits and choose QL or QR iteration 
		//*     for each block, according to whether top or bottom diagonal 
		//*     element is smaller. 
		//* 
		
		_135aegwf = (int)1;//* 
		
Mark10:;
		// continue
		if (_135aegwf > _dxpq0xkr)goto Mark170;
		if (_135aegwf > (int)1)
		*(_864fslqq+(_135aegwf - (int)1 - 1)) = _d0547bi2;
		{
			System.Int32 __81fgg2dlsvn3433 = (System.Int32)(_135aegwf);
			const System.Int32 __81fgg2step3433 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3433;
			for (__81fgg2count3433 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn3433 + __81fgg2step3433) / __81fgg2step3433)), _ev4xhht5 = __81fgg2dlsvn3433; __81fgg2count3433 != 0; __81fgg2count3433--, _ev4xhht5 += (__81fgg2step3433)) {

			{
				
				if (ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_ev4xhht5 - 1)) ) <= ((ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_ev4xhht5 - 1)) ) ) * ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_ev4xhht5 + (int)1 - 1)) ) )) * _p1iqarg6))
				{
					
					*(_864fslqq+(_ev4xhht5 - 1)) = _d0547bi2;goto Mark30;
				}
				
Mark20:;
				// continue
			}
						}		}
		_ev4xhht5 = _dxpq0xkr;//* 
		
Mark30:;
		// continue
		_68ec3gbh = _135aegwf;
		_clabrarh = _68ec3gbh;
		_1mp99t47 = _ev4xhht5;
		_s1sstu6z = _1mp99t47;
		_135aegwf = (_ev4xhht5 + (int)1);
		if (_1mp99t47 == _68ec3gbh)goto Mark10;//* 
		//*     Scale submatrix in rows and columns L to LEND 
		//* 
		
		_b8rxgs6o = _5kajfnos("M" ,ref Unsafe.AsRef((_1mp99t47 - _68ec3gbh) + (int)1) ,(_plfm7z8g+(_68ec3gbh - 1)),(_864fslqq+(_68ec3gbh - 1)));
		_g5graale = (int)0;
		if (_b8rxgs6o == _d0547bi2)goto Mark10;
		if (_b8rxgs6o > _jjejn9g8)
		{
			
			_g5graale = (int)1;
			_cq2kgmi4("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _b8rxgs6o ,ref _jjejn9g8 ,ref Unsafe.AsRef((_1mp99t47 - _68ec3gbh) + (int)1) ,ref Unsafe.AsRef((int)1) ,(_plfm7z8g+(_68ec3gbh - 1)),ref _dxpq0xkr ,ref _gro5yvfo );
			_cq2kgmi4("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _b8rxgs6o ,ref _jjejn9g8 ,ref Unsafe.AsRef(_1mp99t47 - _68ec3gbh) ,ref Unsafe.AsRef((int)1) ,(_864fslqq+(_68ec3gbh - 1)),ref _dxpq0xkr ,ref _gro5yvfo );
		}
		else
		if (_b8rxgs6o < _flwdikii)
		{
			
			_g5graale = (int)2;
			_cq2kgmi4("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _b8rxgs6o ,ref _flwdikii ,ref Unsafe.AsRef((_1mp99t47 - _68ec3gbh) + (int)1) ,ref Unsafe.AsRef((int)1) ,(_plfm7z8g+(_68ec3gbh - 1)),ref _dxpq0xkr ,ref _gro5yvfo );
			_cq2kgmi4("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _b8rxgs6o ,ref _flwdikii ,ref Unsafe.AsRef(_1mp99t47 - _68ec3gbh) ,ref Unsafe.AsRef((int)1) ,(_864fslqq+(_68ec3gbh - 1)),ref _dxpq0xkr ,ref _gro5yvfo );
		}
		//* 
		
		{
			System.Int32 __81fgg2dlsvn3434 = (System.Int32)(_68ec3gbh);
			const System.Int32 __81fgg2step3434 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3434;
			for (__81fgg2count3434 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mp99t47 - (int)1) - __81fgg2dlsvn3434 + __81fgg2step3434) / __81fgg2step3434)), _b5p6od9s = __81fgg2dlsvn3434; __81fgg2count3434 != 0; __81fgg2count3434--, _b5p6od9s += (__81fgg2step3434)) {

			{
				
				*(_864fslqq+(_b5p6od9s - 1)) = __POW2(*(_864fslqq+(_b5p6od9s - 1)));
Mark40:;
				// continue
			}
						}		}//* 
		//*     Choose between QL and QR iteration 
		//* 
		
		if (ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_1mp99t47 - 1)) ) < ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_68ec3gbh - 1)) ))
		{
			
			_1mp99t47 = _clabrarh;
			_68ec3gbh = _s1sstu6z;
		}
		//* 
		
		if (_1mp99t47 >= _68ec3gbh)
		{
			//* 
			//*        QL Iteration 
			//* 
			//*        Look for small subdiagonal element. 
			//* 
			
Mark50:;
			// continue
			if (_68ec3gbh != _1mp99t47)
			{
				
				{
					System.Int32 __81fgg2dlsvn3435 = (System.Int32)(_68ec3gbh);
					const System.Int32 __81fgg2step3435 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3435;
					for (__81fgg2count3435 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mp99t47 - (int)1) - __81fgg2dlsvn3435 + __81fgg2step3435) / __81fgg2step3435)), _ev4xhht5 = __81fgg2dlsvn3435; __81fgg2count3435 != 0; __81fgg2count3435--, _ev4xhht5 += (__81fgg2step3435)) {

					{
						
						if (ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_ev4xhht5 - 1)) ) <= (_okg6tegz * ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_ev4xhht5 - 1)) * *(_plfm7z8g+(_ev4xhht5 + (int)1 - 1)) )))goto Mark70;
Mark60:;
						// continue
					}
										}				}
			}
			
			_ev4xhht5 = _1mp99t47;//* 
			
Mark70:;
			// continue
			if (_ev4xhht5 < _1mp99t47)
			*(_864fslqq+(_ev4xhht5 - 1)) = _d0547bi2;
			_ejwydfmr = *(_plfm7z8g+(_68ec3gbh - 1));
			if (_ev4xhht5 == _68ec3gbh)goto Mark90;//* 
			//*        If remaining matrix is 2 by 2, use SLAE2 to compute its 
			//*        eigenvalues. 
			//* 
			
			if (_ev4xhht5 == (_68ec3gbh + (int)1))
			{
				
				_4hich2eu = ILNumerics.F2NET.Intrinsics.SQRT(*(_864fslqq+(_68ec3gbh - 1)) );
				_7vaufgro(ref Unsafe.AsRef(*(_plfm7z8g+(_68ec3gbh - 1))) ,ref _4hich2eu ,ref Unsafe.AsRef(*(_plfm7z8g+(_68ec3gbh + (int)1 - 1))) ,ref _wwj82gep ,ref _uwqai9pg );
				*(_plfm7z8g+(_68ec3gbh - 1)) = _wwj82gep;
				*(_plfm7z8g+(_68ec3gbh + (int)1 - 1)) = _uwqai9pg;
				*(_864fslqq+(_68ec3gbh - 1)) = _d0547bi2;
				_68ec3gbh = (_68ec3gbh + (int)2);
				if (_68ec3gbh <= _1mp99t47)goto Mark50;goto Mark150;
			}
			//* 
			
			if (_jjlk8rbf == _07mc27rj)goto Mark150;
			_jjlk8rbf = (_jjlk8rbf + (int)1);//* 
			//*        Form shift. 
			//* 
			
			_4hich2eu = ILNumerics.F2NET.Intrinsics.SQRT(*(_864fslqq+(_68ec3gbh - 1)) );
			_91a1vq5f = ((*(_plfm7z8g+(_68ec3gbh + (int)1 - 1)) - _ejwydfmr) / (_5m0mjfxm * _4hich2eu));
			_q2vwp05i = _syk7170d(ref _91a1vq5f ,ref Unsafe.AsRef(_kxg5drh2) );
			_91a1vq5f = (_ejwydfmr - (_4hich2eu / (_91a1vq5f + ILNumerics.F2NET.Intrinsics.SIGN(_q2vwp05i ,_91a1vq5f ))));//* 
			
			_3crf0qn3 = _kxg5drh2;
			_irk8i6qr = _d0547bi2;
			_zf88apxo = (*(_plfm7z8g+(_ev4xhht5 - 1)) - _91a1vq5f);
			_ejwydfmr = (_zf88apxo * _zf88apxo);//* 
			//*        Inner loop 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn3436 = (System.Int32)((_ev4xhht5 - (int)1));
				System.Int32 __81fgg2step3436 = (System.Int32)((int)-1);
				System.Int32 __81fgg2count3436;
				for (__81fgg2count3436 = System.Math.Max(0, (System.Int32)(((System.Int32)(_68ec3gbh) - __81fgg2dlsvn3436 + __81fgg2step3436) / __81fgg2step3436)), _b5p6od9s = __81fgg2dlsvn3436; __81fgg2count3436 != 0; __81fgg2count3436--, _b5p6od9s += (__81fgg2step3436)) {

				{
					
					_uv0s0qmf = *(_864fslqq+(_b5p6od9s - 1));
					_q2vwp05i = (_ejwydfmr + _uv0s0qmf);
					if (_b5p6od9s != (_ev4xhht5 - (int)1))
					*(_864fslqq+(_b5p6od9s + (int)1 - 1)) = (_irk8i6qr * _q2vwp05i);
					_oo1g9svh = _3crf0qn3;
					_3crf0qn3 = (_ejwydfmr / _q2vwp05i);
					_irk8i6qr = (_uv0s0qmf / _q2vwp05i);
					_6g82fvy1 = _zf88apxo;
					_r7cfteg3 = *(_plfm7z8g+(_b5p6od9s - 1));
					_zf88apxo = ((_3crf0qn3 * (_r7cfteg3 - _91a1vq5f)) - (_irk8i6qr * _6g82fvy1));
					*(_plfm7z8g+(_b5p6od9s + (int)1 - 1)) = (_6g82fvy1 + (_r7cfteg3 - _zf88apxo));
					if (_3crf0qn3 != _d0547bi2)
					{
						
						_ejwydfmr = ((_zf88apxo * _zf88apxo) / _3crf0qn3);
					}
					else
					{
						
						_ejwydfmr = (_oo1g9svh * _uv0s0qmf);
					}
					
Mark80:;
					// continue
				}
								}			}//* 
			
			*(_864fslqq+(_68ec3gbh - 1)) = (_irk8i6qr * _ejwydfmr);
			*(_plfm7z8g+(_68ec3gbh - 1)) = (_91a1vq5f + _zf88apxo);goto Mark50;//* 
			//*        Eigenvalue found. 
			//* 
			
Mark90:;
			// continue
			*(_plfm7z8g+(_68ec3gbh - 1)) = _ejwydfmr;//* 
			
			_68ec3gbh = (_68ec3gbh + (int)1);
			if (_68ec3gbh <= _1mp99t47)goto Mark50;goto Mark150;//* 
			
		}
		else
		{
			//* 
			//*        QR Iteration 
			//* 
			//*        Look for small superdiagonal element. 
			//* 
			
Mark100:;
			// continue
			{
				System.Int32 __81fgg2dlsvn3437 = (System.Int32)(_68ec3gbh);
				System.Int32 __81fgg2step3437 = (System.Int32)((int)-1);
				System.Int32 __81fgg2count3437;
				for (__81fgg2count3437 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mp99t47 + (int)1) - __81fgg2dlsvn3437 + __81fgg2step3437) / __81fgg2step3437)), _ev4xhht5 = __81fgg2dlsvn3437; __81fgg2count3437 != 0; __81fgg2count3437--, _ev4xhht5 += (__81fgg2step3437)) {

				{
					
					if (ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_ev4xhht5 - (int)1 - 1)) ) <= (_okg6tegz * ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_ev4xhht5 - 1)) * *(_plfm7z8g+(_ev4xhht5 - (int)1 - 1)) )))goto Mark120;
Mark110:;
					// continue
				}
								}			}
			_ev4xhht5 = _1mp99t47;//* 
			
Mark120:;
			// continue
			if (_ev4xhht5 > _1mp99t47)
			*(_864fslqq+(_ev4xhht5 - (int)1 - 1)) = _d0547bi2;
			_ejwydfmr = *(_plfm7z8g+(_68ec3gbh - 1));
			if (_ev4xhht5 == _68ec3gbh)goto Mark140;//* 
			//*        If remaining matrix is 2 by 2, use SLAE2 to compute its 
			//*        eigenvalues. 
			//* 
			
			if (_ev4xhht5 == (_68ec3gbh - (int)1))
			{
				
				_4hich2eu = ILNumerics.F2NET.Intrinsics.SQRT(*(_864fslqq+(_68ec3gbh - (int)1 - 1)) );
				_7vaufgro(ref Unsafe.AsRef(*(_plfm7z8g+(_68ec3gbh - 1))) ,ref _4hich2eu ,ref Unsafe.AsRef(*(_plfm7z8g+(_68ec3gbh - (int)1 - 1))) ,ref _wwj82gep ,ref _uwqai9pg );
				*(_plfm7z8g+(_68ec3gbh - 1)) = _wwj82gep;
				*(_plfm7z8g+(_68ec3gbh - (int)1 - 1)) = _uwqai9pg;
				*(_864fslqq+(_68ec3gbh - (int)1 - 1)) = _d0547bi2;
				_68ec3gbh = (_68ec3gbh - (int)2);
				if (_68ec3gbh >= _1mp99t47)goto Mark100;goto Mark150;
			}
			//* 
			
			if (_jjlk8rbf == _07mc27rj)goto Mark150;
			_jjlk8rbf = (_jjlk8rbf + (int)1);//* 
			//*        Form shift. 
			//* 
			
			_4hich2eu = ILNumerics.F2NET.Intrinsics.SQRT(*(_864fslqq+(_68ec3gbh - (int)1 - 1)) );
			_91a1vq5f = ((*(_plfm7z8g+(_68ec3gbh - (int)1 - 1)) - _ejwydfmr) / (_5m0mjfxm * _4hich2eu));
			_q2vwp05i = _syk7170d(ref _91a1vq5f ,ref Unsafe.AsRef(_kxg5drh2) );
			_91a1vq5f = (_ejwydfmr - (_4hich2eu / (_91a1vq5f + ILNumerics.F2NET.Intrinsics.SIGN(_q2vwp05i ,_91a1vq5f ))));//* 
			
			_3crf0qn3 = _kxg5drh2;
			_irk8i6qr = _d0547bi2;
			_zf88apxo = (*(_plfm7z8g+(_ev4xhht5 - 1)) - _91a1vq5f);
			_ejwydfmr = (_zf88apxo * _zf88apxo);//* 
			//*        Inner loop 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn3438 = (System.Int32)(_ev4xhht5);
				const System.Int32 __81fgg2step3438 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3438;
				for (__81fgg2count3438 = System.Math.Max(0, (System.Int32)(((System.Int32)(_68ec3gbh - (int)1) - __81fgg2dlsvn3438 + __81fgg2step3438) / __81fgg2step3438)), _b5p6od9s = __81fgg2dlsvn3438; __81fgg2count3438 != 0; __81fgg2count3438--, _b5p6od9s += (__81fgg2step3438)) {

				{
					
					_uv0s0qmf = *(_864fslqq+(_b5p6od9s - 1));
					_q2vwp05i = (_ejwydfmr + _uv0s0qmf);
					if (_b5p6od9s != _ev4xhht5)
					*(_864fslqq+(_b5p6od9s - (int)1 - 1)) = (_irk8i6qr * _q2vwp05i);
					_oo1g9svh = _3crf0qn3;
					_3crf0qn3 = (_ejwydfmr / _q2vwp05i);
					_irk8i6qr = (_uv0s0qmf / _q2vwp05i);
					_6g82fvy1 = _zf88apxo;
					_r7cfteg3 = *(_plfm7z8g+(_b5p6od9s + (int)1 - 1));
					_zf88apxo = ((_3crf0qn3 * (_r7cfteg3 - _91a1vq5f)) - (_irk8i6qr * _6g82fvy1));
					*(_plfm7z8g+(_b5p6od9s - 1)) = (_6g82fvy1 + (_r7cfteg3 - _zf88apxo));
					if (_3crf0qn3 != _d0547bi2)
					{
						
						_ejwydfmr = ((_zf88apxo * _zf88apxo) / _3crf0qn3);
					}
					else
					{
						
						_ejwydfmr = (_oo1g9svh * _uv0s0qmf);
					}
					
Mark130:;
					// continue
				}
								}			}//* 
			
			*(_864fslqq+(_68ec3gbh - (int)1 - 1)) = (_irk8i6qr * _ejwydfmr);
			*(_plfm7z8g+(_68ec3gbh - 1)) = (_91a1vq5f + _zf88apxo);goto Mark100;//* 
			//*        Eigenvalue found. 
			//* 
			
Mark140:;
			// continue
			*(_plfm7z8g+(_68ec3gbh - 1)) = _ejwydfmr;//* 
			
			_68ec3gbh = (_68ec3gbh - (int)1);
			if (_68ec3gbh >= _1mp99t47)goto Mark100;goto Mark150;//* 
			
		}
		//* 
		//*     Undo scaling if necessary 
		//* 
		
Mark150:;
		// continue
		if (_g5graale == (int)1)
		_cq2kgmi4("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _jjejn9g8 ,ref _b8rxgs6o ,ref Unsafe.AsRef((_s1sstu6z - _clabrarh) + (int)1) ,ref Unsafe.AsRef((int)1) ,(_plfm7z8g+(_clabrarh - 1)),ref _dxpq0xkr ,ref _gro5yvfo );
		if (_g5graale == (int)2)
		_cq2kgmi4("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _flwdikii ,ref _b8rxgs6o ,ref Unsafe.AsRef((_s1sstu6z - _clabrarh) + (int)1) ,ref Unsafe.AsRef((int)1) ,(_plfm7z8g+(_clabrarh - 1)),ref _dxpq0xkr ,ref _gro5yvfo );//* 
		//*     Check for no convergence to an eigenvalue after a total 
		//*     of N*MAXIT iterations. 
		//* 
		
		if (_jjlk8rbf < _07mc27rj)goto Mark10;
		{
			System.Int32 __81fgg2dlsvn3439 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step3439 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3439;
			for (__81fgg2count3439 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn3439 + __81fgg2step3439) / __81fgg2step3439)), _b5p6od9s = __81fgg2dlsvn3439; __81fgg2count3439 != 0; __81fgg2count3439--, _b5p6od9s += (__81fgg2step3439)) {

			{
				
				if (*(_864fslqq+(_b5p6od9s - 1)) != _d0547bi2)
				_gro5yvfo = (_gro5yvfo + (int)1);
Mark160:;
				// continue
			}
						}		}goto Mark180;//* 
		//*     Sort eigenvalues in increasing order. 
		//* 
		
Mark170:;
		// continue
		_ezdvkw03("I" ,ref _dxpq0xkr ,_plfm7z8g ,ref _gro5yvfo );//* 
		
Mark180:;
		// continue
		return;//* 
		//*     End of SSTERF 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
