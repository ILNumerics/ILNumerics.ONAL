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
//*> \brief \b CHER2 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CHER2(UPLO,N,ALPHA,X,INCX,Y,INCY,A,LDA) 
//* 
//*       .. Scalar Arguments .. 
//*       COMPLEX ALPHA 
//*       INTEGER INCX,INCY,LDA,N 
//*       CHARACTER UPLO 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX A(LDA,*),X(*),Y(*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CHER2  performs the hermitian rank 2 operation 
//*> 
//*>    A := alpha*x*y**H + conjg( alpha )*y*x**H + A, 
//*> 
//*> where alpha is a scalar, x and y are n element vectors and A is an n 
//*> by n hermitian matrix. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] UPLO 
//*> \verbatim 
//*>          UPLO is CHARACTER*1 
//*>           On entry, UPLO specifies whether the upper or lower 
//*>           triangular part of the array A is to be referenced as 
//*>           follows: 
//*> 
//*>              UPLO = 'U' or 'u'   Only the upper triangular part of A 
//*>                                  is to be referenced. 
//*> 
//*>              UPLO = 'L' or 'l'   Only the lower triangular part of A 
//*>                                  is to be referenced. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>           On entry, N specifies the order of the matrix A. 
//*>           N must be at least zero. 
//*> \endverbatim 
//*> 
//*> \param[in] ALPHA 
//*> \verbatim 
//*>          ALPHA is COMPLEX 
//*>           On entry, ALPHA specifies the scalar alpha. 
//*> \endverbatim 
//*> 
//*> \param[in] X 
//*> \verbatim 
//*>          X is COMPLEX array, dimension at least 
//*>           ( 1 + ( n - 1 )*abs( INCX ) ). 
//*>           Before entry, the incremented array X must contain the n 
//*>           element vector x. 
//*> \endverbatim 
//*> 
//*> \param[in] INCX 
//*> \verbatim 
//*>          INCX is INTEGER 
//*>           On entry, INCX specifies the increment for the elements of 
//*>           X. INCX must not be zero. 
//*> \endverbatim 
//*> 
//*> \param[in] Y 
//*> \verbatim 
//*>          Y is COMPLEX array, dimension at least 
//*>           ( 1 + ( n - 1 )*abs( INCY ) ). 
//*>           Before entry, the incremented array Y must contain the n 
//*>           element vector y. 
//*> \endverbatim 
//*> 
//*> \param[in] INCY 
//*> \verbatim 
//*>          INCY is INTEGER 
//*>           On entry, INCY specifies the increment for the elements of 
//*>           Y. INCY must not be zero. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is COMPLEX array, dimension ( LDA, N ) 
//*>           Before entry with  UPLO = 'U' or 'u', the leading n by n 
//*>           upper triangular part of the array A must contain the upper 
//*>           triangular part of the hermitian matrix and the strictly 
//*>           lower triangular part of A is not referenced. On exit, the 
//*>           upper triangular part of the array A is overwritten by the 
//*>           upper triangular part of the updated matrix. 
//*>           Before entry with UPLO = 'L' or 'l', the leading n by n 
//*>           lower triangular part of the array A must contain the lower 
//*>           triangular part of the hermitian matrix and the strictly 
//*>           upper triangular part of A is not referenced. On exit, the 
//*>           lower triangular part of the array A is overwritten by the 
//*>           lower triangular part of the updated matrix. 
//*>           Note that the imaginary parts of the diagonal elements need 
//*>           not be set, they are assumed to be zero, and on exit they 
//*>           are set to zero. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>           On entry, LDA specifies the first dimension of A as declared 
//*>           in the calling (sub) program. LDA must be at least 
//*>           max( 1, n ). 
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
//*> \ingroup complex_blas_level2 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  Level 2 Blas routine. 
//*> 
//*>  -- Written on 22-October-1986. 
//*>     Jack Dongarra, Argonne National Lab. 
//*>     Jeremy Du Croz, Nag Central Office. 
//*>     Sven Hammarling, Nag Central Office. 
//*>     Richard Hanson, Sandia National Labs. 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _cj7bzwn5(FString _9wyre9zc, ref Int32 _dxpq0xkr, ref fcomplex _r7cfteg3, fcomplex* _ta7zuy9k, ref Int32 _1eqjusqc, fcomplex* _f3z3edv0, ref Int32 _bbrxgmj7, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c)
	{
#region variable declarations
fcomplex _d0547bi2 =   new fcomplex(0f,0f);
fcomplex _yc8h372p =  default;
fcomplex _q3ig7mub =  default;
Int32 _b5p6od9s =  default;
Int32 _gro5yvfo =  default;
Int32 _b69ritwm =  default;
Int32 _821h5yui =  default;
Int32 _znpjgsef =  default;
Int32 _m3loivrh =  default;
Int32 _lwc63p7q =  default;
Int32 _ziknm33t =  default;
Int32 _ylb1uqzt =  default;
string fLanavab = default;
#endregion  variable declarations
_9wyre9zc = _9wyre9zc.Convert(1);

	{
		//* 
		//*  -- Reference BLAS level2 routine (version 3.7.0) -- 
		//*  -- Reference BLAS is a software package provided by Univ. of Tennessee,    -- 
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
		//* 
		//*     Test the input parameters. 
		//* 
		
		_gro5yvfo = (int)0;
		if ((!(_w8y2rzgy(_9wyre9zc ,"U" ))) & (!(_w8y2rzgy(_9wyre9zc ,"L" ))))
		{
			
			_gro5yvfo = (int)1;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)2;
		}
		else
		if (_1eqjusqc == (int)0)
		{
			
			_gro5yvfo = (int)5;
		}
		else
		if (_bbrxgmj7 == (int)0)
		{
			
			_gro5yvfo = (int)7;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)9;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("CHER2 " ,ref _gro5yvfo );
			return;
		}
		//* 
		//*     Quick return if possible. 
		//* 
		
		if ((_dxpq0xkr == (int)0) | (_r7cfteg3 == _d0547bi2))
		return;//* 
		//*     Set up the start points in X and Y if the increments are not both 
		//*     unity. 
		//* 
		
		if ((_1eqjusqc != (int)1) | (_bbrxgmj7 != (int)1))
		{
			
			if (_1eqjusqc > (int)0)
			{
				
				_ziknm33t = (int)1;
			}
			else
			{
				
				_ziknm33t = ((int)1 - ((_dxpq0xkr - (int)1) * _1eqjusqc));
			}
			
			if (_bbrxgmj7 > (int)0)
			{
				
				_ylb1uqzt = (int)1;
			}
			else
			{
				
				_ylb1uqzt = ((int)1 - ((_dxpq0xkr - (int)1) * _bbrxgmj7));
			}
			
			_m3loivrh = _ziknm33t;
			_lwc63p7q = _ylb1uqzt;
		}
		//* 
		//*     Start the operations. In this version the elements of A are 
		//*     accessed sequentially with one pass through the triangular part 
		//*     of A. 
		//* 
		
		if (_w8y2rzgy(_9wyre9zc ,"U" ))
		{
			//* 
			//*        Form  A  when A is stored in the upper triangle. 
			//* 
			
			if ((_1eqjusqc == (int)1) & (_bbrxgmj7 == (int)1))
			{
				
				{
					System.Int32 __81fgg2dlsvn3550 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3550 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3550;
					for (__81fgg2count3550 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3550 + __81fgg2step3550) / __81fgg2step3550)), _znpjgsef = __81fgg2dlsvn3550; __81fgg2count3550 != 0; __81fgg2count3550--, _znpjgsef += (__81fgg2step3550)) {

					{
						
						if ((*(_ta7zuy9k+(_znpjgsef - 1)) != _d0547bi2) | (*(_f3z3edv0+(_znpjgsef - 1)) != _d0547bi2))
						{
							
							_yc8h372p = (_r7cfteg3 * ILNumerics.F2NET.Intrinsics.CONJG(*(_f3z3edv0+(_znpjgsef - 1)) ));
							_q3ig7mub = ILNumerics.F2NET.Intrinsics.CONJG(_r7cfteg3 * *(_ta7zuy9k+(_znpjgsef - 1)) );
							{
								System.Int32 __81fgg2dlsvn3551 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3551 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3551;
								for (__81fgg2count3551 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn3551 + __81fgg2step3551) / __81fgg2step3551)), _b5p6od9s = __81fgg2dlsvn3551; __81fgg2count3551 != 0; __81fgg2count3551--, _b5p6od9s += (__81fgg2step3551)) {

								{
									
									*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = ((*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) + (*(_ta7zuy9k+(_b5p6od9s - 1)) * _yc8h372p)) + (*(_f3z3edv0+(_b5p6od9s - 1)) * _q3ig7mub));
Mark10:;
									// continue
								}
																}							}
							*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = CMPLX((ILNumerics.F2NET.Intrinsics.REAL(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) + ILNumerics.F2NET.Intrinsics.REAL((*(_ta7zuy9k+(_znpjgsef - 1)) * _yc8h372p) + (*(_f3z3edv0+(_znpjgsef - 1)) * _q3ig7mub) )));
						}
						else
						{
							
							*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = CMPLX(ILNumerics.F2NET.Intrinsics.REAL(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ));
						}
						
Mark20:;
						// continue
					}
										}				}
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn3552 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3552 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3552;
					for (__81fgg2count3552 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3552 + __81fgg2step3552) / __81fgg2step3552)), _znpjgsef = __81fgg2dlsvn3552; __81fgg2count3552 != 0; __81fgg2count3552--, _znpjgsef += (__81fgg2step3552)) {

					{
						
						if ((*(_ta7zuy9k+(_m3loivrh - 1)) != _d0547bi2) | (*(_f3z3edv0+(_lwc63p7q - 1)) != _d0547bi2))
						{
							
							_yc8h372p = (_r7cfteg3 * ILNumerics.F2NET.Intrinsics.CONJG(*(_f3z3edv0+(_lwc63p7q - 1)) ));
							_q3ig7mub = ILNumerics.F2NET.Intrinsics.CONJG(_r7cfteg3 * *(_ta7zuy9k+(_m3loivrh - 1)) );
							_b69ritwm = _ziknm33t;
							_821h5yui = _ylb1uqzt;
							{
								System.Int32 __81fgg2dlsvn3553 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3553 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3553;
								for (__81fgg2count3553 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn3553 + __81fgg2step3553) / __81fgg2step3553)), _b5p6od9s = __81fgg2dlsvn3553; __81fgg2count3553 != 0; __81fgg2count3553--, _b5p6od9s += (__81fgg2step3553)) {

								{
									
									*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = ((*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) + (*(_ta7zuy9k+(_b69ritwm - 1)) * _yc8h372p)) + (*(_f3z3edv0+(_821h5yui - 1)) * _q3ig7mub));
									_b69ritwm = (_b69ritwm + _1eqjusqc);
									_821h5yui = (_821h5yui + _bbrxgmj7);
Mark30:;
									// continue
								}
																}							}
							*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = CMPLX((ILNumerics.F2NET.Intrinsics.REAL(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) + ILNumerics.F2NET.Intrinsics.REAL((*(_ta7zuy9k+(_m3loivrh - 1)) * _yc8h372p) + (*(_f3z3edv0+(_lwc63p7q - 1)) * _q3ig7mub) )));
						}
						else
						{
							
							*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = CMPLX(ILNumerics.F2NET.Intrinsics.REAL(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ));
						}
						
						_m3loivrh = (_m3loivrh + _1eqjusqc);
						_lwc63p7q = (_lwc63p7q + _bbrxgmj7);
Mark40:;
						// continue
					}
										}				}
			}
			
		}
		else
		{
			//* 
			//*        Form  A  when A is stored in the lower triangle. 
			//* 
			
			if ((_1eqjusqc == (int)1) & (_bbrxgmj7 == (int)1))
			{
				
				{
					System.Int32 __81fgg2dlsvn3554 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3554 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3554;
					for (__81fgg2count3554 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3554 + __81fgg2step3554) / __81fgg2step3554)), _znpjgsef = __81fgg2dlsvn3554; __81fgg2count3554 != 0; __81fgg2count3554--, _znpjgsef += (__81fgg2step3554)) {

					{
						
						if ((*(_ta7zuy9k+(_znpjgsef - 1)) != _d0547bi2) | (*(_f3z3edv0+(_znpjgsef - 1)) != _d0547bi2))
						{
							
							_yc8h372p = (_r7cfteg3 * ILNumerics.F2NET.Intrinsics.CONJG(*(_f3z3edv0+(_znpjgsef - 1)) ));
							_q3ig7mub = ILNumerics.F2NET.Intrinsics.CONJG(_r7cfteg3 * *(_ta7zuy9k+(_znpjgsef - 1)) );
							*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = CMPLX((ILNumerics.F2NET.Intrinsics.REAL(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) + ILNumerics.F2NET.Intrinsics.REAL((*(_ta7zuy9k+(_znpjgsef - 1)) * _yc8h372p) + (*(_f3z3edv0+(_znpjgsef - 1)) * _q3ig7mub) )));
							{
								System.Int32 __81fgg2dlsvn3555 = (System.Int32)((_znpjgsef + (int)1));
								const System.Int32 __81fgg2step3555 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3555;
								for (__81fgg2count3555 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3555 + __81fgg2step3555) / __81fgg2step3555)), _b5p6od9s = __81fgg2dlsvn3555; __81fgg2count3555 != 0; __81fgg2count3555--, _b5p6od9s += (__81fgg2step3555)) {

								{
									
									*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = ((*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) + (*(_ta7zuy9k+(_b5p6od9s - 1)) * _yc8h372p)) + (*(_f3z3edv0+(_b5p6od9s - 1)) * _q3ig7mub));
Mark50:;
									// continue
								}
																}							}
						}
						else
						{
							
							*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = CMPLX(ILNumerics.F2NET.Intrinsics.REAL(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ));
						}
						
Mark60:;
						// continue
					}
										}				}
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn3556 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3556 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3556;
					for (__81fgg2count3556 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3556 + __81fgg2step3556) / __81fgg2step3556)), _znpjgsef = __81fgg2dlsvn3556; __81fgg2count3556 != 0; __81fgg2count3556--, _znpjgsef += (__81fgg2step3556)) {

					{
						
						if ((*(_ta7zuy9k+(_m3loivrh - 1)) != _d0547bi2) | (*(_f3z3edv0+(_lwc63p7q - 1)) != _d0547bi2))
						{
							
							_yc8h372p = (_r7cfteg3 * ILNumerics.F2NET.Intrinsics.CONJG(*(_f3z3edv0+(_lwc63p7q - 1)) ));
							_q3ig7mub = ILNumerics.F2NET.Intrinsics.CONJG(_r7cfteg3 * *(_ta7zuy9k+(_m3loivrh - 1)) );
							*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = CMPLX((ILNumerics.F2NET.Intrinsics.REAL(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) + ILNumerics.F2NET.Intrinsics.REAL((*(_ta7zuy9k+(_m3loivrh - 1)) * _yc8h372p) + (*(_f3z3edv0+(_lwc63p7q - 1)) * _q3ig7mub) )));
							_b69ritwm = _m3loivrh;
							_821h5yui = _lwc63p7q;
							{
								System.Int32 __81fgg2dlsvn3557 = (System.Int32)((_znpjgsef + (int)1));
								const System.Int32 __81fgg2step3557 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3557;
								for (__81fgg2count3557 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3557 + __81fgg2step3557) / __81fgg2step3557)), _b5p6od9s = __81fgg2dlsvn3557; __81fgg2count3557 != 0; __81fgg2count3557--, _b5p6od9s += (__81fgg2step3557)) {

								{
									
									_b69ritwm = (_b69ritwm + _1eqjusqc);
									_821h5yui = (_821h5yui + _bbrxgmj7);
									*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = ((*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) + (*(_ta7zuy9k+(_b69ritwm - 1)) * _yc8h372p)) + (*(_f3z3edv0+(_821h5yui - 1)) * _q3ig7mub));
Mark70:;
									// continue
								}
																}							}
						}
						else
						{
							
							*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = CMPLX(ILNumerics.F2NET.Intrinsics.REAL(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ));
						}
						
						_m3loivrh = (_m3loivrh + _1eqjusqc);
						_lwc63p7q = (_lwc63p7q + _bbrxgmj7);
Mark80:;
						// continue
					}
										}				}
			}
			
		}
		//* 
		
		return;//* 
		//*     End of CHER2 . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
