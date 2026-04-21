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
//*> \brief \b ZTRSV 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZTRSV(UPLO,TRANS,DIAG,N,A,LDA,X,INCX) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER INCX,LDA,N 
//*       CHARACTER DIAG,TRANS,UPLO 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX*16 A(LDA,*),X(*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZTRSV  solves one of the systems of equations 
//*> 
//*>    A*x = b,   or   A**T*x = b,   or   A**H*x = b, 
//*> 
//*> where b and x are n element vectors and A is an n by n unit, or 
//*> non-unit, upper or lower triangular matrix. 
//*> 
//*> No test for singularity or near-singularity is included in this 
//*> routine. Such tests must be performed before calling this routine. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] UPLO 
//*> \verbatim 
//*>          UPLO is CHARACTER*1 
//*>           On entry, UPLO specifies whether the matrix is an upper or 
//*>           lower triangular matrix as follows: 
//*> 
//*>              UPLO = 'U' or 'u'   A is an upper triangular matrix. 
//*> 
//*>              UPLO = 'L' or 'l'   A is a lower triangular matrix. 
//*> \endverbatim 
//*> 
//*> \param[in] TRANS 
//*> \verbatim 
//*>          TRANS is CHARACTER*1 
//*>           On entry, TRANS specifies the equations to be solved as 
//*>           follows: 
//*> 
//*>              TRANS = 'N' or 'n'   A*x = b. 
//*> 
//*>              TRANS = 'T' or 't'   A**T*x = b. 
//*> 
//*>              TRANS = 'C' or 'c'   A**H*x = b. 
//*> \endverbatim 
//*> 
//*> \param[in] DIAG 
//*> \verbatim 
//*>          DIAG is CHARACTER*1 
//*>           On entry, DIAG specifies whether or not A is unit 
//*>           triangular as follows: 
//*> 
//*>              DIAG = 'U' or 'u'   A is assumed to be unit triangular. 
//*> 
//*>              DIAG = 'N' or 'n'   A is not assumed to be unit 
//*>                                  triangular. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>           On entry, N specifies the order of the matrix A. 
//*>           N must be at least zero. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is COMPLEX*16 array, dimension ( LDA, N ) 
//*>           Before entry with  UPLO = 'U' or 'u', the leading n by n 
//*>           upper triangular part of the array A must contain the upper 
//*>           triangular matrix and the strictly lower triangular part of 
//*>           A is not referenced. 
//*>           Before entry with UPLO = 'L' or 'l', the leading n by n 
//*>           lower triangular part of the array A must contain the lower 
//*>           triangular matrix and the strictly upper triangular part of 
//*>           A is not referenced. 
//*>           Note that when  DIAG = 'U' or 'u', the diagonal elements of 
//*>           A are not referenced either, but are assumed to be unity. 
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
//*> \param[in,out] X 
//*> \verbatim 
//*>          X is COMPLEX*16 array, dimension at least 
//*>           ( 1 + ( n - 1 )*abs( INCX ) ). 
//*>           Before entry, the incremented array X must contain the n 
//*>           element right-hand side vector b. On exit, X is overwritten 
//*>           with the solution vector x. 
//*> \endverbatim 
//*> 
//*> \param[in] INCX 
//*> \verbatim 
//*>          INCX is INTEGER 
//*>           On entry, INCX specifies the increment for the elements of 
//*>           X. INCX must not be zero. 
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
//*> \ingroup complex16_blas_level2 
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

	 
	public static void _tqoxi2p2(FString _9wyre9zc, FString _scuo79v4, FString _2scffxp3, ref Int32 _dxpq0xkr, complex* _vxfgpup9, ref Int32 _ocv8fk5c, complex* _ta7zuy9k, ref Int32 _1eqjusqc)
	{
#region variable declarations
complex _d0547bi2 =   new fcomplex(0f,0f);
complex _1ajfmh55 =  default;
Int32 _b5p6od9s =  default;
Int32 _gro5yvfo =  default;
Int32 _b69ritwm =  default;
Int32 _znpjgsef =  default;
Int32 _m3loivrh =  default;
Int32 _ziknm33t =  default;
Boolean _moml4lap =  default;
Boolean _rcjmgxm4 =  default;
string fLanavab = default;
#endregion  variable declarations
_9wyre9zc = _9wyre9zc.Convert(1);
_scuo79v4 = _scuo79v4.Convert(1);
_2scffxp3 = _2scffxp3.Convert(1);

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
		if (((!(_w8y2rzgy(_scuo79v4 ,"N" ))) & (!(_w8y2rzgy(_scuo79v4 ,"T" )))) & (!(_w8y2rzgy(_scuo79v4 ,"C" ))))
		{
			
			_gro5yvfo = (int)2;
		}
		else
		if ((!(_w8y2rzgy(_2scffxp3 ,"U" ))) & (!(_w8y2rzgy(_2scffxp3 ,"N" ))))
		{
			
			_gro5yvfo = (int)3;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)4;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)6;
		}
		else
		if (_1eqjusqc == (int)0)
		{
			
			_gro5yvfo = (int)8;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("ZTRSV " ,ref _gro5yvfo );
			return;
		}
		//* 
		//*     Quick return if possible. 
		//* 
		
		if (_dxpq0xkr == (int)0)
		return;//* 
		
		_moml4lap = _w8y2rzgy(_scuo79v4 ,"T" );
		_rcjmgxm4 = _w8y2rzgy(_2scffxp3 ,"N" );//* 
		//*     Set up the start point in X if the increment is not unity. This 
		//*     will be  ( N - 1 )*INCX  too small for descending loops. 
		//* 
		
		if (_1eqjusqc <= (int)0)
		{
			
			_ziknm33t = ((int)1 - ((_dxpq0xkr - (int)1) * _1eqjusqc));
		}
		else
		if (_1eqjusqc != (int)1)
		{
			
			_ziknm33t = (int)1;
		}
		//* 
		//*     Start the operations. In this version the elements of A are 
		//*     accessed sequentially with one pass through A. 
		//* 
		
		if (_w8y2rzgy(_scuo79v4 ,"N" ))
		{
			//* 
			//*        Form  x := inv( A )*x. 
			//* 
			
			if (_w8y2rzgy(_9wyre9zc ,"U" ))
			{
				
				if (_1eqjusqc == (int)1)
				{
					
					{
						System.Int32 __81fgg2dlsvn2776 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step2776 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count2776;
						for (__81fgg2count2776 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn2776 + __81fgg2step2776) / __81fgg2step2776)), _znpjgsef = __81fgg2dlsvn2776; __81fgg2count2776 != 0; __81fgg2count2776--, _znpjgsef += (__81fgg2step2776)) {

						{
							
							if (*(_ta7zuy9k+(_znpjgsef - 1)) != _d0547bi2)
							{
								
								if (_rcjmgxm4)
								*(_ta7zuy9k+(_znpjgsef - 1)) = (*(_ta7zuy9k+(_znpjgsef - 1)) / *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
								_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
								{
									System.Int32 __81fgg2dlsvn2777 = (System.Int32)((_znpjgsef - (int)1));
									System.Int32 __81fgg2step2777 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count2777;
									for (__81fgg2count2777 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn2777 + __81fgg2step2777) / __81fgg2step2777)), _b5p6od9s = __81fgg2dlsvn2777; __81fgg2count2777 != 0; __81fgg2count2777--, _b5p6od9s += (__81fgg2step2777)) {

									{
										
										*(_ta7zuy9k+(_b5p6od9s - 1)) = (*(_ta7zuy9k+(_b5p6od9s - 1)) - (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
Mark10:;
										// continue
									}
																		}								}
							}
							
Mark20:;
							// continue
						}
												}					}
				}
				else
				{
					
					_m3loivrh = (_ziknm33t + ((_dxpq0xkr - (int)1) * _1eqjusqc));
					{
						System.Int32 __81fgg2dlsvn2778 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step2778 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count2778;
						for (__81fgg2count2778 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn2778 + __81fgg2step2778) / __81fgg2step2778)), _znpjgsef = __81fgg2dlsvn2778; __81fgg2count2778 != 0; __81fgg2count2778--, _znpjgsef += (__81fgg2step2778)) {

						{
							
							if (*(_ta7zuy9k+(_m3loivrh - 1)) != _d0547bi2)
							{
								
								if (_rcjmgxm4)
								*(_ta7zuy9k+(_m3loivrh - 1)) = (*(_ta7zuy9k+(_m3loivrh - 1)) / *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
								_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
								_b69ritwm = _m3loivrh;
								{
									System.Int32 __81fgg2dlsvn2779 = (System.Int32)((_znpjgsef - (int)1));
									System.Int32 __81fgg2step2779 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count2779;
									for (__81fgg2count2779 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn2779 + __81fgg2step2779) / __81fgg2step2779)), _b5p6od9s = __81fgg2dlsvn2779; __81fgg2count2779 != 0; __81fgg2count2779--, _b5p6od9s += (__81fgg2step2779)) {

									{
										
										_b69ritwm = (_b69ritwm - _1eqjusqc);
										*(_ta7zuy9k+(_b69ritwm - 1)) = (*(_ta7zuy9k+(_b69ritwm - 1)) - (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
Mark30:;
										// continue
									}
																		}								}
							}
							
							_m3loivrh = (_m3loivrh - _1eqjusqc);
Mark40:;
							// continue
						}
												}					}
				}
				
			}
			else
			{
				
				if (_1eqjusqc == (int)1)
				{
					
					{
						System.Int32 __81fgg2dlsvn2780 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step2780 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2780;
						for (__81fgg2count2780 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2780 + __81fgg2step2780) / __81fgg2step2780)), _znpjgsef = __81fgg2dlsvn2780; __81fgg2count2780 != 0; __81fgg2count2780--, _znpjgsef += (__81fgg2step2780)) {

						{
							
							if (*(_ta7zuy9k+(_znpjgsef - 1)) != _d0547bi2)
							{
								
								if (_rcjmgxm4)
								*(_ta7zuy9k+(_znpjgsef - 1)) = (*(_ta7zuy9k+(_znpjgsef - 1)) / *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
								_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
								{
									System.Int32 __81fgg2dlsvn2781 = (System.Int32)((_znpjgsef + (int)1));
									const System.Int32 __81fgg2step2781 = (System.Int32)((int)1);
									System.Int32 __81fgg2count2781;
									for (__81fgg2count2781 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2781 + __81fgg2step2781) / __81fgg2step2781)), _b5p6od9s = __81fgg2dlsvn2781; __81fgg2count2781 != 0; __81fgg2count2781--, _b5p6od9s += (__81fgg2step2781)) {

									{
										
										*(_ta7zuy9k+(_b5p6od9s - 1)) = (*(_ta7zuy9k+(_b5p6od9s - 1)) - (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
Mark50:;
										// continue
									}
																		}								}
							}
							
Mark60:;
							// continue
						}
												}					}
				}
				else
				{
					
					_m3loivrh = _ziknm33t;
					{
						System.Int32 __81fgg2dlsvn2782 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step2782 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2782;
						for (__81fgg2count2782 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2782 + __81fgg2step2782) / __81fgg2step2782)), _znpjgsef = __81fgg2dlsvn2782; __81fgg2count2782 != 0; __81fgg2count2782--, _znpjgsef += (__81fgg2step2782)) {

						{
							
							if (*(_ta7zuy9k+(_m3loivrh - 1)) != _d0547bi2)
							{
								
								if (_rcjmgxm4)
								*(_ta7zuy9k+(_m3loivrh - 1)) = (*(_ta7zuy9k+(_m3loivrh - 1)) / *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
								_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
								_b69ritwm = _m3loivrh;
								{
									System.Int32 __81fgg2dlsvn2783 = (System.Int32)((_znpjgsef + (int)1));
									const System.Int32 __81fgg2step2783 = (System.Int32)((int)1);
									System.Int32 __81fgg2count2783;
									for (__81fgg2count2783 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2783 + __81fgg2step2783) / __81fgg2step2783)), _b5p6od9s = __81fgg2dlsvn2783; __81fgg2count2783 != 0; __81fgg2count2783--, _b5p6od9s += (__81fgg2step2783)) {

									{
										
										_b69ritwm = (_b69ritwm + _1eqjusqc);
										*(_ta7zuy9k+(_b69ritwm - 1)) = (*(_ta7zuy9k+(_b69ritwm - 1)) - (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
Mark70:;
										// continue
									}
																		}								}
							}
							
							_m3loivrh = (_m3loivrh + _1eqjusqc);
Mark80:;
							// continue
						}
												}					}
				}
				
			}
			
		}
		else
		{
			//* 
			//*        Form  x := inv( A**T )*x  or  x := inv( A**H )*x. 
			//* 
			
			if (_w8y2rzgy(_9wyre9zc ,"U" ))
			{
				
				if (_1eqjusqc == (int)1)
				{
					
					{
						System.Int32 __81fgg2dlsvn2784 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step2784 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2784;
						for (__81fgg2count2784 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2784 + __81fgg2step2784) / __81fgg2step2784)), _znpjgsef = __81fgg2dlsvn2784; __81fgg2count2784 != 0; __81fgg2count2784--, _znpjgsef += (__81fgg2step2784)) {

						{
							
							_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
							if (_moml4lap)
							{
								
								{
									System.Int32 __81fgg2dlsvn2785 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step2785 = (System.Int32)((int)1);
									System.Int32 __81fgg2count2785;
									for (__81fgg2count2785 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn2785 + __81fgg2step2785) / __81fgg2step2785)), _b5p6od9s = __81fgg2dlsvn2785; __81fgg2count2785 != 0; __81fgg2count2785--, _b5p6od9s += (__81fgg2step2785)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 - (*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * *(_ta7zuy9k+(_b5p6od9s - 1))));
Mark90:;
										// continue
									}
																		}								}
								if (_rcjmgxm4)
								_1ajfmh55 = (_1ajfmh55 / *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							}
							else
							{
								
								{
									System.Int32 __81fgg2dlsvn2786 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step2786 = (System.Int32)((int)1);
									System.Int32 __81fgg2count2786;
									for (__81fgg2count2786 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn2786 + __81fgg2step2786) / __81fgg2step2786)), _b5p6od9s = __81fgg2dlsvn2786; __81fgg2count2786 != 0; __81fgg2count2786--, _b5p6od9s += (__81fgg2step2786)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 - (ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) * *(_ta7zuy9k+(_b5p6od9s - 1))));
Mark100:;
										// continue
									}
																		}								}
								if (_rcjmgxm4)
								_1ajfmh55 = (_1ajfmh55 / ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ));
							}
							
							*(_ta7zuy9k+(_znpjgsef - 1)) = _1ajfmh55;
Mark110:;
							// continue
						}
												}					}
				}
				else
				{
					
					_m3loivrh = _ziknm33t;
					{
						System.Int32 __81fgg2dlsvn2787 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step2787 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2787;
						for (__81fgg2count2787 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2787 + __81fgg2step2787) / __81fgg2step2787)), _znpjgsef = __81fgg2dlsvn2787; __81fgg2count2787 != 0; __81fgg2count2787--, _znpjgsef += (__81fgg2step2787)) {

						{
							
							_b69ritwm = _ziknm33t;
							_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
							if (_moml4lap)
							{
								
								{
									System.Int32 __81fgg2dlsvn2788 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step2788 = (System.Int32)((int)1);
									System.Int32 __81fgg2count2788;
									for (__81fgg2count2788 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn2788 + __81fgg2step2788) / __81fgg2step2788)), _b5p6od9s = __81fgg2dlsvn2788; __81fgg2count2788 != 0; __81fgg2count2788--, _b5p6od9s += (__81fgg2step2788)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 - (*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * *(_ta7zuy9k+(_b69ritwm - 1))));
										_b69ritwm = (_b69ritwm + _1eqjusqc);
Mark120:;
										// continue
									}
																		}								}
								if (_rcjmgxm4)
								_1ajfmh55 = (_1ajfmh55 / *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							}
							else
							{
								
								{
									System.Int32 __81fgg2dlsvn2789 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step2789 = (System.Int32)((int)1);
									System.Int32 __81fgg2count2789;
									for (__81fgg2count2789 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn2789 + __81fgg2step2789) / __81fgg2step2789)), _b5p6od9s = __81fgg2dlsvn2789; __81fgg2count2789 != 0; __81fgg2count2789--, _b5p6od9s += (__81fgg2step2789)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 - (ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) * *(_ta7zuy9k+(_b69ritwm - 1))));
										_b69ritwm = (_b69ritwm + _1eqjusqc);
Mark130:;
										// continue
									}
																		}								}
								if (_rcjmgxm4)
								_1ajfmh55 = (_1ajfmh55 / ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ));
							}
							
							*(_ta7zuy9k+(_m3loivrh - 1)) = _1ajfmh55;
							_m3loivrh = (_m3loivrh + _1eqjusqc);
Mark140:;
							// continue
						}
												}					}
				}
				
			}
			else
			{
				
				if (_1eqjusqc == (int)1)
				{
					
					{
						System.Int32 __81fgg2dlsvn2790 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step2790 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count2790;
						for (__81fgg2count2790 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn2790 + __81fgg2step2790) / __81fgg2step2790)), _znpjgsef = __81fgg2dlsvn2790; __81fgg2count2790 != 0; __81fgg2count2790--, _znpjgsef += (__81fgg2step2790)) {

						{
							
							_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
							if (_moml4lap)
							{
								
								{
									System.Int32 __81fgg2dlsvn2791 = (System.Int32)(_dxpq0xkr);
									System.Int32 __81fgg2step2791 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count2791;
									for (__81fgg2count2791 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef + (int)1) - __81fgg2dlsvn2791 + __81fgg2step2791) / __81fgg2step2791)), _b5p6od9s = __81fgg2dlsvn2791; __81fgg2count2791 != 0; __81fgg2count2791--, _b5p6od9s += (__81fgg2step2791)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 - (*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * *(_ta7zuy9k+(_b5p6od9s - 1))));
Mark150:;
										// continue
									}
																		}								}
								if (_rcjmgxm4)
								_1ajfmh55 = (_1ajfmh55 / *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							}
							else
							{
								
								{
									System.Int32 __81fgg2dlsvn2792 = (System.Int32)(_dxpq0xkr);
									System.Int32 __81fgg2step2792 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count2792;
									for (__81fgg2count2792 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef + (int)1) - __81fgg2dlsvn2792 + __81fgg2step2792) / __81fgg2step2792)), _b5p6od9s = __81fgg2dlsvn2792; __81fgg2count2792 != 0; __81fgg2count2792--, _b5p6od9s += (__81fgg2step2792)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 - (ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) * *(_ta7zuy9k+(_b5p6od9s - 1))));
Mark160:;
										// continue
									}
																		}								}
								if (_rcjmgxm4)
								_1ajfmh55 = (_1ajfmh55 / ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ));
							}
							
							*(_ta7zuy9k+(_znpjgsef - 1)) = _1ajfmh55;
Mark170:;
							// continue
						}
												}					}
				}
				else
				{
					
					_ziknm33t = (_ziknm33t + ((_dxpq0xkr - (int)1) * _1eqjusqc));
					_m3loivrh = _ziknm33t;
					{
						System.Int32 __81fgg2dlsvn2793 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step2793 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count2793;
						for (__81fgg2count2793 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn2793 + __81fgg2step2793) / __81fgg2step2793)), _znpjgsef = __81fgg2dlsvn2793; __81fgg2count2793 != 0; __81fgg2count2793--, _znpjgsef += (__81fgg2step2793)) {

						{
							
							_b69ritwm = _ziknm33t;
							_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
							if (_moml4lap)
							{
								
								{
									System.Int32 __81fgg2dlsvn2794 = (System.Int32)(_dxpq0xkr);
									System.Int32 __81fgg2step2794 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count2794;
									for (__81fgg2count2794 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef + (int)1) - __81fgg2dlsvn2794 + __81fgg2step2794) / __81fgg2step2794)), _b5p6od9s = __81fgg2dlsvn2794; __81fgg2count2794 != 0; __81fgg2count2794--, _b5p6od9s += (__81fgg2step2794)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 - (*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * *(_ta7zuy9k+(_b69ritwm - 1))));
										_b69ritwm = (_b69ritwm - _1eqjusqc);
Mark180:;
										// continue
									}
																		}								}
								if (_rcjmgxm4)
								_1ajfmh55 = (_1ajfmh55 / *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							}
							else
							{
								
								{
									System.Int32 __81fgg2dlsvn2795 = (System.Int32)(_dxpq0xkr);
									System.Int32 __81fgg2step2795 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count2795;
									for (__81fgg2count2795 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef + (int)1) - __81fgg2dlsvn2795 + __81fgg2step2795) / __81fgg2step2795)), _b5p6od9s = __81fgg2dlsvn2795; __81fgg2count2795 != 0; __81fgg2count2795--, _b5p6od9s += (__81fgg2step2795)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 - (ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) * *(_ta7zuy9k+(_b69ritwm - 1))));
										_b69ritwm = (_b69ritwm - _1eqjusqc);
Mark190:;
										// continue
									}
																		}								}
								if (_rcjmgxm4)
								_1ajfmh55 = (_1ajfmh55 / ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ));
							}
							
							*(_ta7zuy9k+(_m3loivrh - 1)) = _1ajfmh55;
							_m3loivrh = (_m3loivrh - _1eqjusqc);
Mark200:;
							// continue
						}
												}					}
				}
				
			}
			
		}
		//* 
		
		return;//* 
		//*     End of ZTRSV . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
