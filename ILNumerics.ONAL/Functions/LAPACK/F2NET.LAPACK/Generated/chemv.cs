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
//*> \brief \b CHEMV 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CHEMV(UPLO,N,ALPHA,A,LDA,X,INCX,BETA,Y,INCY) 
//* 
//*       .. Scalar Arguments .. 
//*       COMPLEX ALPHA,BETA 
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
//*> CHEMV  performs the matrix-vector  operation 
//*> 
//*>    y := alpha*A*x + beta*y, 
//*> 
//*> where alpha and beta are scalars, x and y are n element vectors and 
//*> A is an n by n hermitian matrix. 
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
//*> \param[in] A 
//*> \verbatim 
//*>          A is COMPLEX array, dimension ( LDA, N ) 
//*>           Before entry with  UPLO = 'U' or 'u', the leading n by n 
//*>           upper triangular part of the array A must contain the upper 
//*>           triangular part of the hermitian matrix and the strictly 
//*>           lower triangular part of A is not referenced. 
//*>           Before entry with UPLO = 'L' or 'l', the leading n by n 
//*>           lower triangular part of the array A must contain the lower 
//*>           triangular part of the hermitian matrix and the strictly 
//*>           upper triangular part of A is not referenced. 
//*>           Note that the imaginary parts of the diagonal elements need 
//*>           not be set and are assumed to be zero. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>           On entry, LDA specifies the first dimension of A as declared 
//*>           in the calling (sub) program. LDA must be at least 
//*>           max( 1, n ). 
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
//*> \param[in] BETA 
//*> \verbatim 
//*>          BETA is COMPLEX 
//*>           On entry, BETA specifies the scalar beta. When BETA is 
//*>           supplied as zero then Y need not be set on input. 
//*> \endverbatim 
//*> 
//*> \param[in,out] Y 
//*> \verbatim 
//*>          Y is COMPLEX array, dimension at least 
//*>           ( 1 + ( n - 1 )*abs( INCY ) ). 
//*>           Before entry, the incremented array Y must contain the n 
//*>           element vector y. On exit, Y is overwritten by the updated 
//*>           vector y. 
//*> \endverbatim 
//*> 
//*> \param[in] INCY 
//*> \verbatim 
//*>          INCY is INTEGER 
//*>           On entry, INCY specifies the increment for the elements of 
//*>           Y. INCY must not be zero. 
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
//*>  The vector and matrix arguments are not referenced when N = 0, or M = 0 
//*> 
//*>  -- Written on 22-October-1986. 
//*>     Jack Dongarra, Argonne National Lab. 
//*>     Jeremy Du Croz, Nag Central Office. 
//*>     Sven Hammarling, Nag Central Office. 
//*>     Richard Hanson, Sandia National Labs. 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _c637kid8(FString _9wyre9zc, ref Int32 _dxpq0xkr, ref fcomplex _r7cfteg3, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, fcomplex* _ta7zuy9k, ref Int32 _1eqjusqc, ref fcomplex _bafcbx97, fcomplex* _f3z3edv0, ref Int32 _bbrxgmj7)
	{
#region variable declarations
fcomplex _kxg5drh2 =   new fcomplex(1f,0f);
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
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)5;
		}
		else
		if (_1eqjusqc == (int)0)
		{
			
			_gro5yvfo = (int)7;
		}
		else
		if (_bbrxgmj7 == (int)0)
		{
			
			_gro5yvfo = (int)10;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("CHEMV " ,ref _gro5yvfo );
			return;
		}
		//* 
		//*     Quick return if possible. 
		//* 
		
		if ((_dxpq0xkr == (int)0) | ((_r7cfteg3 == _d0547bi2) & (_bafcbx97 == _kxg5drh2)))
		return;//* 
		//*     Set up the start points in  X  and  Y. 
		//* 
		
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
		//* 
		//*     Start the operations. In this version the elements of A are 
		//*     accessed sequentially with one pass through the triangular part 
		//*     of A. 
		//* 
		//*     First form  y := beta*y. 
		//* 
		
		if (_bafcbx97 != _kxg5drh2)
		{
			
			if (_bbrxgmj7 == (int)1)
			{
				
				if (_bafcbx97 == _d0547bi2)
				{
					
					{
						System.Int32 __81fgg2dlsvn3538 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3538 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3538;
						for (__81fgg2count3538 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3538 + __81fgg2step3538) / __81fgg2step3538)), _b5p6od9s = __81fgg2dlsvn3538; __81fgg2count3538 != 0; __81fgg2count3538--, _b5p6od9s += (__81fgg2step3538)) {

						{
							
							*(_f3z3edv0+(_b5p6od9s - 1)) = _d0547bi2;
Mark10:;
							// continue
						}
												}					}
				}
				else
				{
					
					{
						System.Int32 __81fgg2dlsvn3539 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3539 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3539;
						for (__81fgg2count3539 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3539 + __81fgg2step3539) / __81fgg2step3539)), _b5p6od9s = __81fgg2dlsvn3539; __81fgg2count3539 != 0; __81fgg2count3539--, _b5p6od9s += (__81fgg2step3539)) {

						{
							
							*(_f3z3edv0+(_b5p6od9s - 1)) = (_bafcbx97 * *(_f3z3edv0+(_b5p6od9s - 1)));
Mark20:;
							// continue
						}
												}					}
				}
				
			}
			else
			{
				
				_821h5yui = _ylb1uqzt;
				if (_bafcbx97 == _d0547bi2)
				{
					
					{
						System.Int32 __81fgg2dlsvn3540 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3540 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3540;
						for (__81fgg2count3540 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3540 + __81fgg2step3540) / __81fgg2step3540)), _b5p6od9s = __81fgg2dlsvn3540; __81fgg2count3540 != 0; __81fgg2count3540--, _b5p6od9s += (__81fgg2step3540)) {

						{
							
							*(_f3z3edv0+(_821h5yui - 1)) = _d0547bi2;
							_821h5yui = (_821h5yui + _bbrxgmj7);
Mark30:;
							// continue
						}
												}					}
				}
				else
				{
					
					{
						System.Int32 __81fgg2dlsvn3541 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3541 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3541;
						for (__81fgg2count3541 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3541 + __81fgg2step3541) / __81fgg2step3541)), _b5p6od9s = __81fgg2dlsvn3541; __81fgg2count3541 != 0; __81fgg2count3541--, _b5p6od9s += (__81fgg2step3541)) {

						{
							
							*(_f3z3edv0+(_821h5yui - 1)) = (_bafcbx97 * *(_f3z3edv0+(_821h5yui - 1)));
							_821h5yui = (_821h5yui + _bbrxgmj7);
Mark40:;
							// continue
						}
												}					}
				}
				
			}
			
		}
		
		if (_r7cfteg3 == _d0547bi2)
		return;
		if (_w8y2rzgy(_9wyre9zc ,"U" ))
		{
			//* 
			//*        Form  y  when A is stored in upper triangle. 
			//* 
			
			if ((_1eqjusqc == (int)1) & (_bbrxgmj7 == (int)1))
			{
				
				{
					System.Int32 __81fgg2dlsvn3542 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3542 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3542;
					for (__81fgg2count3542 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3542 + __81fgg2step3542) / __81fgg2step3542)), _znpjgsef = __81fgg2dlsvn3542; __81fgg2count3542 != 0; __81fgg2count3542--, _znpjgsef += (__81fgg2step3542)) {

					{
						
						_yc8h372p = (_r7cfteg3 * *(_ta7zuy9k+(_znpjgsef - 1)));
						_q3ig7mub = _d0547bi2;
						{
							System.Int32 __81fgg2dlsvn3543 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3543 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3543;
							for (__81fgg2count3543 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn3543 + __81fgg2step3543) / __81fgg2step3543)), _b5p6od9s = __81fgg2dlsvn3543; __81fgg2count3543 != 0; __81fgg2count3543--, _b5p6od9s += (__81fgg2step3543)) {

							{
								
								*(_f3z3edv0+(_b5p6od9s - 1)) = (*(_f3z3edv0+(_b5p6od9s - 1)) + (_yc8h372p * *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
								_q3ig7mub = (_q3ig7mub + (ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) * *(_ta7zuy9k+(_b5p6od9s - 1))));
Mark50:;
								// continue
							}
														}						}
						*(_f3z3edv0+(_znpjgsef - 1)) = ((*(_f3z3edv0+(_znpjgsef - 1)) + (_yc8h372p * ILNumerics.F2NET.Intrinsics.REAL(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ))) + (_r7cfteg3 * _q3ig7mub));
Mark60:;
						// continue
					}
										}				}
			}
			else
			{
				
				_m3loivrh = _ziknm33t;
				_lwc63p7q = _ylb1uqzt;
				{
					System.Int32 __81fgg2dlsvn3544 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3544 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3544;
					for (__81fgg2count3544 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3544 + __81fgg2step3544) / __81fgg2step3544)), _znpjgsef = __81fgg2dlsvn3544; __81fgg2count3544 != 0; __81fgg2count3544--, _znpjgsef += (__81fgg2step3544)) {

					{
						
						_yc8h372p = (_r7cfteg3 * *(_ta7zuy9k+(_m3loivrh - 1)));
						_q3ig7mub = _d0547bi2;
						_b69ritwm = _ziknm33t;
						_821h5yui = _ylb1uqzt;
						{
							System.Int32 __81fgg2dlsvn3545 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3545 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3545;
							for (__81fgg2count3545 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn3545 + __81fgg2step3545) / __81fgg2step3545)), _b5p6od9s = __81fgg2dlsvn3545; __81fgg2count3545 != 0; __81fgg2count3545--, _b5p6od9s += (__81fgg2step3545)) {

							{
								
								*(_f3z3edv0+(_821h5yui - 1)) = (*(_f3z3edv0+(_821h5yui - 1)) + (_yc8h372p * *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
								_q3ig7mub = (_q3ig7mub + (ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) * *(_ta7zuy9k+(_b69ritwm - 1))));
								_b69ritwm = (_b69ritwm + _1eqjusqc);
								_821h5yui = (_821h5yui + _bbrxgmj7);
Mark70:;
								// continue
							}
														}						}
						*(_f3z3edv0+(_lwc63p7q - 1)) = ((*(_f3z3edv0+(_lwc63p7q - 1)) + (_yc8h372p * ILNumerics.F2NET.Intrinsics.REAL(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ))) + (_r7cfteg3 * _q3ig7mub));
						_m3loivrh = (_m3loivrh + _1eqjusqc);
						_lwc63p7q = (_lwc63p7q + _bbrxgmj7);
Mark80:;
						// continue
					}
										}				}
			}
			
		}
		else
		{
			//* 
			//*        Form  y  when A is stored in lower triangle. 
			//* 
			
			if ((_1eqjusqc == (int)1) & (_bbrxgmj7 == (int)1))
			{
				
				{
					System.Int32 __81fgg2dlsvn3546 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3546 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3546;
					for (__81fgg2count3546 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3546 + __81fgg2step3546) / __81fgg2step3546)), _znpjgsef = __81fgg2dlsvn3546; __81fgg2count3546 != 0; __81fgg2count3546--, _znpjgsef += (__81fgg2step3546)) {

					{
						
						_yc8h372p = (_r7cfteg3 * *(_ta7zuy9k+(_znpjgsef - 1)));
						_q3ig7mub = _d0547bi2;
						*(_f3z3edv0+(_znpjgsef - 1)) = (*(_f3z3edv0+(_znpjgsef - 1)) + (_yc8h372p * ILNumerics.F2NET.Intrinsics.REAL(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) )));
						{
							System.Int32 __81fgg2dlsvn3547 = (System.Int32)((_znpjgsef + (int)1));
							const System.Int32 __81fgg2step3547 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3547;
							for (__81fgg2count3547 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3547 + __81fgg2step3547) / __81fgg2step3547)), _b5p6od9s = __81fgg2dlsvn3547; __81fgg2count3547 != 0; __81fgg2count3547--, _b5p6od9s += (__81fgg2step3547)) {

							{
								
								*(_f3z3edv0+(_b5p6od9s - 1)) = (*(_f3z3edv0+(_b5p6od9s - 1)) + (_yc8h372p * *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
								_q3ig7mub = (_q3ig7mub + (ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) * *(_ta7zuy9k+(_b5p6od9s - 1))));
Mark90:;
								// continue
							}
														}						}
						*(_f3z3edv0+(_znpjgsef - 1)) = (*(_f3z3edv0+(_znpjgsef - 1)) + (_r7cfteg3 * _q3ig7mub));
Mark100:;
						// continue
					}
										}				}
			}
			else
			{
				
				_m3loivrh = _ziknm33t;
				_lwc63p7q = _ylb1uqzt;
				{
					System.Int32 __81fgg2dlsvn3548 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3548 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3548;
					for (__81fgg2count3548 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3548 + __81fgg2step3548) / __81fgg2step3548)), _znpjgsef = __81fgg2dlsvn3548; __81fgg2count3548 != 0; __81fgg2count3548--, _znpjgsef += (__81fgg2step3548)) {

					{
						
						_yc8h372p = (_r7cfteg3 * *(_ta7zuy9k+(_m3loivrh - 1)));
						_q3ig7mub = _d0547bi2;
						*(_f3z3edv0+(_lwc63p7q - 1)) = (*(_f3z3edv0+(_lwc63p7q - 1)) + (_yc8h372p * ILNumerics.F2NET.Intrinsics.REAL(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) )));
						_b69ritwm = _m3loivrh;
						_821h5yui = _lwc63p7q;
						{
							System.Int32 __81fgg2dlsvn3549 = (System.Int32)((_znpjgsef + (int)1));
							const System.Int32 __81fgg2step3549 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3549;
							for (__81fgg2count3549 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3549 + __81fgg2step3549) / __81fgg2step3549)), _b5p6od9s = __81fgg2dlsvn3549; __81fgg2count3549 != 0; __81fgg2count3549--, _b5p6od9s += (__81fgg2step3549)) {

							{
								
								_b69ritwm = (_b69ritwm + _1eqjusqc);
								_821h5yui = (_821h5yui + _bbrxgmj7);
								*(_f3z3edv0+(_821h5yui - 1)) = (*(_f3z3edv0+(_821h5yui - 1)) + (_yc8h372p * *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
								_q3ig7mub = (_q3ig7mub + (ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) * *(_ta7zuy9k+(_b69ritwm - 1))));
Mark110:;
								// continue
							}
														}						}
						*(_f3z3edv0+(_lwc63p7q - 1)) = (*(_f3z3edv0+(_lwc63p7q - 1)) + (_r7cfteg3 * _q3ig7mub));
						_m3loivrh = (_m3loivrh + _1eqjusqc);
						_lwc63p7q = (_lwc63p7q + _bbrxgmj7);
Mark120:;
						// continue
					}
										}				}
			}
			
		}
		//* 
		
		return;//* 
		//*     End of CHEMV . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
