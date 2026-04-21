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
//*> \brief \b DGEMV 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DGEMV(TRANS,M,N,ALPHA,A,LDA,X,INCX,BETA,Y,INCY) 
//* 
//*       .. Scalar Arguments .. 
//*       DOUBLE PRECISION ALPHA,BETA 
//*       INTEGER INCX,INCY,LDA,M,N 
//*       CHARACTER TRANS 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION A(LDA,*),X(*),Y(*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DGEMV  performs one of the matrix-vector operations 
//*> 
//*>    y := alpha*A*x + beta*y,   or   y := alpha*A**T*x + beta*y, 
//*> 
//*> where alpha and beta are scalars, x and y are vectors and A is an 
//*> m by n matrix. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] TRANS 
//*> \verbatim 
//*>          TRANS is CHARACTER*1 
//*>           On entry, TRANS specifies the operation to be performed as 
//*>           follows: 
//*> 
//*>              TRANS = 'N' or 'n'   y := alpha*A*x + beta*y. 
//*> 
//*>              TRANS = 'T' or 't'   y := alpha*A**T*x + beta*y. 
//*> 
//*>              TRANS = 'C' or 'c'   y := alpha*A**T*x + beta*y. 
//*> \endverbatim 
//*> 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>           On entry, M specifies the number of rows of the matrix A. 
//*>           M must be at least zero. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>           On entry, N specifies the number of columns of the matrix A. 
//*>           N must be at least zero. 
//*> \endverbatim 
//*> 
//*> \param[in] ALPHA 
//*> \verbatim 
//*>          ALPHA is DOUBLE PRECISION. 
//*>           On entry, ALPHA specifies the scalar alpha. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is DOUBLE PRECISION array, dimension ( LDA, N ) 
//*>           Before entry, the leading m by n part of the array A must 
//*>           contain the matrix of coefficients. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>           On entry, LDA specifies the first dimension of A as declared 
//*>           in the calling (sub) program. LDA must be at least 
//*>           max( 1, m ). 
//*> \endverbatim 
//*> 
//*> \param[in] X 
//*> \verbatim 
//*>          X is DOUBLE PRECISION array, dimension at least 
//*>           ( 1 + ( n - 1 )*abs( INCX ) ) when TRANS = 'N' or 'n' 
//*>           and at least 
//*>           ( 1 + ( m - 1 )*abs( INCX ) ) otherwise. 
//*>           Before entry, the incremented array X must contain the 
//*>           vector x. 
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
//*>          BETA is DOUBLE PRECISION. 
//*>           On entry, BETA specifies the scalar beta. When BETA is 
//*>           supplied as zero then Y need not be set on input. 
//*> \endverbatim 
//*> 
//*> \param[in,out] Y 
//*> \verbatim 
//*>          Y is DOUBLE PRECISION array, dimension at least 
//*>           ( 1 + ( m - 1 )*abs( INCY ) ) when TRANS = 'N' or 'n' 
//*>           and at least 
//*>           ( 1 + ( n - 1 )*abs( INCY ) ) otherwise. 
//*>           Before entry with BETA non-zero, the incremented array Y 
//*>           must contain the vector y. On exit, Y is overwritten by the 
//*>           updated vector y. 
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
//*> \ingroup double_blas_level2 
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

	 
	public static void _t5wmtd1j(FString _scuo79v4, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Double _r7cfteg3, Double* _vxfgpup9, ref Int32 _ocv8fk5c, Double* _ta7zuy9k, ref Int32 _1eqjusqc, ref Double _bafcbx97, Double* _f3z3edv0, ref Int32 _bbrxgmj7)
	{
#region variable declarations
Double _kxg5drh2 =  1d;
Double _d0547bi2 =  0d;
Double _1ajfmh55 =  default;
Int32 _b5p6od9s =  default;
Int32 _gro5yvfo =  default;
Int32 _b69ritwm =  default;
Int32 _821h5yui =  default;
Int32 _znpjgsef =  default;
Int32 _m3loivrh =  default;
Int32 _lwc63p7q =  default;
Int32 _ziknm33t =  default;
Int32 _ylb1uqzt =  default;
Int32 _k2902mav =  default;
Int32 _cf0mki7c =  default;
string fLanavab = default;
#endregion  variable declarations
_scuo79v4 = _scuo79v4.Convert(1);

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
		if (((!(_w8y2rzgy(_scuo79v4 ,"N" ))) & (!(_w8y2rzgy(_scuo79v4 ,"T" )))) & (!(_w8y2rzgy(_scuo79v4 ,"C" ))))
		{
			
			_gro5yvfo = (int)1;
		}
		else
		if (_ev4xhht5 < (int)0)
		{
			
			_gro5yvfo = (int)2;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)3;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_ev4xhht5 ))
		{
			
			_gro5yvfo = (int)6;
		}
		else
		if (_1eqjusqc == (int)0)
		{
			
			_gro5yvfo = (int)8;
		}
		else
		if (_bbrxgmj7 == (int)0)
		{
			
			_gro5yvfo = (int)11;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("DGEMV " ,ref _gro5yvfo );
			return;
		}
		//* 
		//*     Quick return if possible. 
		//* 
		
		if (((_ev4xhht5 == (int)0) | (_dxpq0xkr == (int)0)) | ((_r7cfteg3 == _d0547bi2) & (_bafcbx97 == _kxg5drh2)))
		return;//* 
		//*     Set  LENX  and  LENY, the lengths of the vectors x and y, and set 
		//*     up the start points in  X  and  Y. 
		//* 
		
		if (_w8y2rzgy(_scuo79v4 ,"N" ))
		{
			
			_k2902mav = _dxpq0xkr;
			_cf0mki7c = _ev4xhht5;
		}
		else
		{
			
			_k2902mav = _ev4xhht5;
			_cf0mki7c = _dxpq0xkr;
		}
		
		if (_1eqjusqc > (int)0)
		{
			
			_ziknm33t = (int)1;
		}
		else
		{
			
			_ziknm33t = ((int)1 - ((_k2902mav - (int)1) * _1eqjusqc));
		}
		
		if (_bbrxgmj7 > (int)0)
		{
			
			_ylb1uqzt = (int)1;
		}
		else
		{
			
			_ylb1uqzt = ((int)1 - ((_cf0mki7c - (int)1) * _bbrxgmj7));
		}
		//* 
		//*     Start the operations. In this version the elements of A are 
		//*     accessed sequentially with one pass through A. 
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
						System.Int32 __81fgg2dlsvn392 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step392 = (System.Int32)((int)1);
						System.Int32 __81fgg2count392;
						for (__81fgg2count392 = System.Math.Max(0, (System.Int32)(((System.Int32)(_cf0mki7c) - __81fgg2dlsvn392 + __81fgg2step392) / __81fgg2step392)), _b5p6od9s = __81fgg2dlsvn392; __81fgg2count392 != 0; __81fgg2count392--, _b5p6od9s += (__81fgg2step392)) {

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
						System.Int32 __81fgg2dlsvn393 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step393 = (System.Int32)((int)1);
						System.Int32 __81fgg2count393;
						for (__81fgg2count393 = System.Math.Max(0, (System.Int32)(((System.Int32)(_cf0mki7c) - __81fgg2dlsvn393 + __81fgg2step393) / __81fgg2step393)), _b5p6od9s = __81fgg2dlsvn393; __81fgg2count393 != 0; __81fgg2count393--, _b5p6od9s += (__81fgg2step393)) {

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
						System.Int32 __81fgg2dlsvn394 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step394 = (System.Int32)((int)1);
						System.Int32 __81fgg2count394;
						for (__81fgg2count394 = System.Math.Max(0, (System.Int32)(((System.Int32)(_cf0mki7c) - __81fgg2dlsvn394 + __81fgg2step394) / __81fgg2step394)), _b5p6od9s = __81fgg2dlsvn394; __81fgg2count394 != 0; __81fgg2count394--, _b5p6od9s += (__81fgg2step394)) {

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
						System.Int32 __81fgg2dlsvn395 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step395 = (System.Int32)((int)1);
						System.Int32 __81fgg2count395;
						for (__81fgg2count395 = System.Math.Max(0, (System.Int32)(((System.Int32)(_cf0mki7c) - __81fgg2dlsvn395 + __81fgg2step395) / __81fgg2step395)), _b5p6od9s = __81fgg2dlsvn395; __81fgg2count395 != 0; __81fgg2count395--, _b5p6od9s += (__81fgg2step395)) {

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
		if (_w8y2rzgy(_scuo79v4 ,"N" ))
		{
			//* 
			//*        Form  y := alpha*A*x + y. 
			//* 
			
			_m3loivrh = _ziknm33t;
			if (_bbrxgmj7 == (int)1)
			{
				
				{
					System.Int32 __81fgg2dlsvn396 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step396 = (System.Int32)((int)1);
					System.Int32 __81fgg2count396;
					for (__81fgg2count396 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn396 + __81fgg2step396) / __81fgg2step396)), _znpjgsef = __81fgg2dlsvn396; __81fgg2count396 != 0; __81fgg2count396--, _znpjgsef += (__81fgg2step396)) {

					{
						
						_1ajfmh55 = (_r7cfteg3 * *(_ta7zuy9k+(_m3loivrh - 1)));
						{
							System.Int32 __81fgg2dlsvn397 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step397 = (System.Int32)((int)1);
							System.Int32 __81fgg2count397;
							for (__81fgg2count397 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn397 + __81fgg2step397) / __81fgg2step397)), _b5p6od9s = __81fgg2dlsvn397; __81fgg2count397 != 0; __81fgg2count397--, _b5p6od9s += (__81fgg2step397)) {

							{
								
								*(_f3z3edv0+(_b5p6od9s - 1)) = (*(_f3z3edv0+(_b5p6od9s - 1)) + (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
Mark50:;
								// continue
							}
														}						}
						_m3loivrh = (_m3loivrh + _1eqjusqc);
Mark60:;
						// continue
					}
										}				}
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn398 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step398 = (System.Int32)((int)1);
					System.Int32 __81fgg2count398;
					for (__81fgg2count398 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn398 + __81fgg2step398) / __81fgg2step398)), _znpjgsef = __81fgg2dlsvn398; __81fgg2count398 != 0; __81fgg2count398--, _znpjgsef += (__81fgg2step398)) {

					{
						
						_1ajfmh55 = (_r7cfteg3 * *(_ta7zuy9k+(_m3loivrh - 1)));
						_821h5yui = _ylb1uqzt;
						{
							System.Int32 __81fgg2dlsvn399 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step399 = (System.Int32)((int)1);
							System.Int32 __81fgg2count399;
							for (__81fgg2count399 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn399 + __81fgg2step399) / __81fgg2step399)), _b5p6od9s = __81fgg2dlsvn399; __81fgg2count399 != 0; __81fgg2count399--, _b5p6od9s += (__81fgg2step399)) {

							{
								
								*(_f3z3edv0+(_821h5yui - 1)) = (*(_f3z3edv0+(_821h5yui - 1)) + (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
								_821h5yui = (_821h5yui + _bbrxgmj7);
Mark70:;
								// continue
							}
														}						}
						_m3loivrh = (_m3loivrh + _1eqjusqc);
Mark80:;
						// continue
					}
										}				}
			}
			
		}
		else
		{
			//* 
			//*        Form  y := alpha*A**T*x + y. 
			//* 
			
			_lwc63p7q = _ylb1uqzt;
			if (_1eqjusqc == (int)1)
			{
				
				{
					System.Int32 __81fgg2dlsvn400 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step400 = (System.Int32)((int)1);
					System.Int32 __81fgg2count400;
					for (__81fgg2count400 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn400 + __81fgg2step400) / __81fgg2step400)), _znpjgsef = __81fgg2dlsvn400; __81fgg2count400 != 0; __81fgg2count400--, _znpjgsef += (__81fgg2step400)) {

					{
						
						_1ajfmh55 = _d0547bi2;
						{
							System.Int32 __81fgg2dlsvn401 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step401 = (System.Int32)((int)1);
							System.Int32 __81fgg2count401;
							for (__81fgg2count401 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn401 + __81fgg2step401) / __81fgg2step401)), _b5p6od9s = __81fgg2dlsvn401; __81fgg2count401 != 0; __81fgg2count401--, _b5p6od9s += (__81fgg2step401)) {

							{
								
								_1ajfmh55 = (_1ajfmh55 + (*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * *(_ta7zuy9k+(_b5p6od9s - 1))));
Mark90:;
								// continue
							}
														}						}
						*(_f3z3edv0+(_lwc63p7q - 1)) = (*(_f3z3edv0+(_lwc63p7q - 1)) + (_r7cfteg3 * _1ajfmh55));
						_lwc63p7q = (_lwc63p7q + _bbrxgmj7);
Mark100:;
						// continue
					}
										}				}
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn402 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step402 = (System.Int32)((int)1);
					System.Int32 __81fgg2count402;
					for (__81fgg2count402 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn402 + __81fgg2step402) / __81fgg2step402)), _znpjgsef = __81fgg2dlsvn402; __81fgg2count402 != 0; __81fgg2count402--, _znpjgsef += (__81fgg2step402)) {

					{
						
						_1ajfmh55 = _d0547bi2;
						_b69ritwm = _ziknm33t;
						{
							System.Int32 __81fgg2dlsvn403 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step403 = (System.Int32)((int)1);
							System.Int32 __81fgg2count403;
							for (__81fgg2count403 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn403 + __81fgg2step403) / __81fgg2step403)), _b5p6od9s = __81fgg2dlsvn403; __81fgg2count403 != 0; __81fgg2count403--, _b5p6od9s += (__81fgg2step403)) {

							{
								
								_1ajfmh55 = (_1ajfmh55 + (*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * *(_ta7zuy9k+(_b69ritwm - 1))));
								_b69ritwm = (_b69ritwm + _1eqjusqc);
Mark110:;
								// continue
							}
														}						}
						*(_f3z3edv0+(_lwc63p7q - 1)) = (*(_f3z3edv0+(_lwc63p7q - 1)) + (_r7cfteg3 * _1ajfmh55));
						_lwc63p7q = (_lwc63p7q + _bbrxgmj7);
Mark120:;
						// continue
					}
										}				}
			}
			
		}
		//* 
		
		return;//* 
		//*     End of DGEMV . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
