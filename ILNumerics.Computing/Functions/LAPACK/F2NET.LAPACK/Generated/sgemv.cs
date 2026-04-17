
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
//*> \brief \b SGEMV 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SGEMV(TRANS,M,N,ALPHA,A,LDA,X,INCX,BETA,Y,INCY) 
//* 
//*       .. Scalar Arguments .. 
//*       REAL ALPHA,BETA 
//*       INTEGER INCX,INCY,LDA,M,N 
//*       CHARACTER TRANS 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL A(LDA,*),X(*),Y(*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SGEMV  performs one of the matrix-vector operations 
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
//*>          ALPHA is REAL 
//*>           On entry, ALPHA specifies the scalar alpha. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is REAL array, dimension ( LDA, N ) 
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
//*>          X is REAL array, dimension at least 
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
//*>          BETA is REAL 
//*>           On entry, BETA specifies the scalar beta. When BETA is 
//*>           supplied as zero then Y need not be set on input. 
//*> \endverbatim 
//*> 
//*> \param[in,out] Y 
//*> \verbatim 
//*>          Y is REAL array, dimension at least 
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
//*> \ingroup single_blas_level2 
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

	 
	public static void _9mvi1n8m(FString _scuo79v4, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Single _r7cfteg3, Single* _vxfgpup9, ref Int32 _ocv8fk5c, Single* _ta7zuy9k, ref Int32 _1eqjusqc, ref Single _bafcbx97, Single* _f3z3edv0, ref Int32 _bbrxgmj7)
	{
#region variable declarations
Single _kxg5drh2 =  1f;
Single _d0547bi2 =  0f;
Single _1ajfmh55 =  default;
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
			
			_ut9qalzx("SGEMV " ,ref _gro5yvfo );
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
						System.Int32 __81fgg2dlsvn767 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step767 = (System.Int32)((int)1);
						System.Int32 __81fgg2count767;
						for (__81fgg2count767 = System.Math.Max(0, (System.Int32)(((System.Int32)(_cf0mki7c) - __81fgg2dlsvn767 + __81fgg2step767) / __81fgg2step767)), _b5p6od9s = __81fgg2dlsvn767; __81fgg2count767 != 0; __81fgg2count767--, _b5p6od9s += (__81fgg2step767)) {

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
						System.Int32 __81fgg2dlsvn768 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step768 = (System.Int32)((int)1);
						System.Int32 __81fgg2count768;
						for (__81fgg2count768 = System.Math.Max(0, (System.Int32)(((System.Int32)(_cf0mki7c) - __81fgg2dlsvn768 + __81fgg2step768) / __81fgg2step768)), _b5p6od9s = __81fgg2dlsvn768; __81fgg2count768 != 0; __81fgg2count768--, _b5p6od9s += (__81fgg2step768)) {

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
						System.Int32 __81fgg2dlsvn769 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step769 = (System.Int32)((int)1);
						System.Int32 __81fgg2count769;
						for (__81fgg2count769 = System.Math.Max(0, (System.Int32)(((System.Int32)(_cf0mki7c) - __81fgg2dlsvn769 + __81fgg2step769) / __81fgg2step769)), _b5p6od9s = __81fgg2dlsvn769; __81fgg2count769 != 0; __81fgg2count769--, _b5p6od9s += (__81fgg2step769)) {

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
						System.Int32 __81fgg2dlsvn770 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step770 = (System.Int32)((int)1);
						System.Int32 __81fgg2count770;
						for (__81fgg2count770 = System.Math.Max(0, (System.Int32)(((System.Int32)(_cf0mki7c) - __81fgg2dlsvn770 + __81fgg2step770) / __81fgg2step770)), _b5p6od9s = __81fgg2dlsvn770; __81fgg2count770 != 0; __81fgg2count770--, _b5p6od9s += (__81fgg2step770)) {

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
					System.Int32 __81fgg2dlsvn771 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step771 = (System.Int32)((int)1);
					System.Int32 __81fgg2count771;
					for (__81fgg2count771 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn771 + __81fgg2step771) / __81fgg2step771)), _znpjgsef = __81fgg2dlsvn771; __81fgg2count771 != 0; __81fgg2count771--, _znpjgsef += (__81fgg2step771)) {

					{
						
						_1ajfmh55 = (_r7cfteg3 * *(_ta7zuy9k+(_m3loivrh - 1)));
						{
							System.Int32 __81fgg2dlsvn772 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step772 = (System.Int32)((int)1);
							System.Int32 __81fgg2count772;
							for (__81fgg2count772 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn772 + __81fgg2step772) / __81fgg2step772)), _b5p6od9s = __81fgg2dlsvn772; __81fgg2count772 != 0; __81fgg2count772--, _b5p6od9s += (__81fgg2step772)) {

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
					System.Int32 __81fgg2dlsvn773 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step773 = (System.Int32)((int)1);
					System.Int32 __81fgg2count773;
					for (__81fgg2count773 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn773 + __81fgg2step773) / __81fgg2step773)), _znpjgsef = __81fgg2dlsvn773; __81fgg2count773 != 0; __81fgg2count773--, _znpjgsef += (__81fgg2step773)) {

					{
						
						_1ajfmh55 = (_r7cfteg3 * *(_ta7zuy9k+(_m3loivrh - 1)));
						_821h5yui = _ylb1uqzt;
						{
							System.Int32 __81fgg2dlsvn774 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step774 = (System.Int32)((int)1);
							System.Int32 __81fgg2count774;
							for (__81fgg2count774 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn774 + __81fgg2step774) / __81fgg2step774)), _b5p6od9s = __81fgg2dlsvn774; __81fgg2count774 != 0; __81fgg2count774--, _b5p6od9s += (__81fgg2step774)) {

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
					System.Int32 __81fgg2dlsvn775 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step775 = (System.Int32)((int)1);
					System.Int32 __81fgg2count775;
					for (__81fgg2count775 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn775 + __81fgg2step775) / __81fgg2step775)), _znpjgsef = __81fgg2dlsvn775; __81fgg2count775 != 0; __81fgg2count775--, _znpjgsef += (__81fgg2step775)) {

					{
						
						_1ajfmh55 = _d0547bi2;
						{
							System.Int32 __81fgg2dlsvn776 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step776 = (System.Int32)((int)1);
							System.Int32 __81fgg2count776;
							for (__81fgg2count776 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn776 + __81fgg2step776) / __81fgg2step776)), _b5p6od9s = __81fgg2dlsvn776; __81fgg2count776 != 0; __81fgg2count776--, _b5p6od9s += (__81fgg2step776)) {

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
					System.Int32 __81fgg2dlsvn777 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step777 = (System.Int32)((int)1);
					System.Int32 __81fgg2count777;
					for (__81fgg2count777 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn777 + __81fgg2step777) / __81fgg2step777)), _znpjgsef = __81fgg2dlsvn777; __81fgg2count777 != 0; __81fgg2count777--, _znpjgsef += (__81fgg2step777)) {

					{
						
						_1ajfmh55 = _d0547bi2;
						_b69ritwm = _ziknm33t;
						{
							System.Int32 __81fgg2dlsvn778 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step778 = (System.Int32)((int)1);
							System.Int32 __81fgg2count778;
							for (__81fgg2count778 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn778 + __81fgg2step778) / __81fgg2step778)), _b5p6od9s = __81fgg2dlsvn778; __81fgg2count778 != 0; __81fgg2count778--, _b5p6od9s += (__81fgg2step778)) {

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
		//*     End of SGEMV . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
