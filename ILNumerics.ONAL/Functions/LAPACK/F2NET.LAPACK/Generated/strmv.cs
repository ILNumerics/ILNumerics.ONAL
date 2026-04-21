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
//*> \brief \b STRMV 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE STRMV(UPLO,TRANS,DIAG,N,A,LDA,X,INCX) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER INCX,LDA,N 
//*       CHARACTER DIAG,TRANS,UPLO 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL A(LDA,*),X(*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> STRMV  performs one of the matrix-vector operations 
//*> 
//*>    x := A*x,   or   x := A**T*x, 
//*> 
//*> where x is an n element vector and  A is an n by n unit, or non-unit, 
//*> upper or lower triangular matrix. 
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
//*>           On entry, TRANS specifies the operation to be performed as 
//*>           follows: 
//*> 
//*>              TRANS = 'N' or 'n'   x := A*x. 
//*> 
//*>              TRANS = 'T' or 't'   x := A**T*x. 
//*> 
//*>              TRANS = 'C' or 'c'   x := A**T*x. 
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
//*>          A is REAL array, dimension ( LDA, N ) 
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
//*>          X is REAL array, dimension at least 
//*>           ( 1 + ( n - 1 )*abs( INCX ) ). 
//*>           Before entry, the incremented array X must contain the n 
//*>           element vector x. On exit, X is overwritten with the 
//*>           transformed vector x. 
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

	 
	public static void _aocvqojl(FString _9wyre9zc, FString _scuo79v4, FString _2scffxp3, ref Int32 _dxpq0xkr, Single* _vxfgpup9, ref Int32 _ocv8fk5c, Single* _ta7zuy9k, ref Int32 _1eqjusqc)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Single _1ajfmh55 =  default;
Int32 _b5p6od9s =  default;
Int32 _gro5yvfo =  default;
Int32 _b69ritwm =  default;
Int32 _znpjgsef =  default;
Int32 _m3loivrh =  default;
Int32 _ziknm33t =  default;
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
			
			_ut9qalzx("STRMV " ,ref _gro5yvfo );
			return;
		}
		//* 
		//*     Quick return if possible. 
		//* 
		
		if (_dxpq0xkr == (int)0)
		return;//* 
		
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
			//*        Form  x := A*x. 
			//* 
			
			if (_w8y2rzgy(_9wyre9zc ,"U" ))
			{
				
				if (_1eqjusqc == (int)1)
				{
					
					{
						System.Int32 __81fgg2dlsvn857 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step857 = (System.Int32)((int)1);
						System.Int32 __81fgg2count857;
						for (__81fgg2count857 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn857 + __81fgg2step857) / __81fgg2step857)), _znpjgsef = __81fgg2dlsvn857; __81fgg2count857 != 0; __81fgg2count857--, _znpjgsef += (__81fgg2step857)) {

						{
							
							if (*(_ta7zuy9k+(_znpjgsef - 1)) != _d0547bi2)
							{
								
								_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
								{
									System.Int32 __81fgg2dlsvn858 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step858 = (System.Int32)((int)1);
									System.Int32 __81fgg2count858;
									for (__81fgg2count858 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn858 + __81fgg2step858) / __81fgg2step858)), _b5p6od9s = __81fgg2dlsvn858; __81fgg2count858 != 0; __81fgg2count858--, _b5p6od9s += (__81fgg2step858)) {

									{
										
										*(_ta7zuy9k+(_b5p6od9s - 1)) = (*(_ta7zuy9k+(_b5p6od9s - 1)) + (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
Mark10:;
										// continue
									}
																		}								}
								if (_rcjmgxm4)
								*(_ta7zuy9k+(_znpjgsef - 1)) = (*(_ta7zuy9k+(_znpjgsef - 1)) * *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							}
							
Mark20:;
							// continue
						}
												}					}
				}
				else
				{
					
					_m3loivrh = _ziknm33t;
					{
						System.Int32 __81fgg2dlsvn859 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step859 = (System.Int32)((int)1);
						System.Int32 __81fgg2count859;
						for (__81fgg2count859 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn859 + __81fgg2step859) / __81fgg2step859)), _znpjgsef = __81fgg2dlsvn859; __81fgg2count859 != 0; __81fgg2count859--, _znpjgsef += (__81fgg2step859)) {

						{
							
							if (*(_ta7zuy9k+(_m3loivrh - 1)) != _d0547bi2)
							{
								
								_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
								_b69ritwm = _ziknm33t;
								{
									System.Int32 __81fgg2dlsvn860 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step860 = (System.Int32)((int)1);
									System.Int32 __81fgg2count860;
									for (__81fgg2count860 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn860 + __81fgg2step860) / __81fgg2step860)), _b5p6od9s = __81fgg2dlsvn860; __81fgg2count860 != 0; __81fgg2count860--, _b5p6od9s += (__81fgg2step860)) {

									{
										
										*(_ta7zuy9k+(_b69ritwm - 1)) = (*(_ta7zuy9k+(_b69ritwm - 1)) + (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
										_b69ritwm = (_b69ritwm + _1eqjusqc);
Mark30:;
										// continue
									}
																		}								}
								if (_rcjmgxm4)
								*(_ta7zuy9k+(_m3loivrh - 1)) = (*(_ta7zuy9k+(_m3loivrh - 1)) * *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							}
							
							_m3loivrh = (_m3loivrh + _1eqjusqc);
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
						System.Int32 __81fgg2dlsvn861 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step861 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count861;
						for (__81fgg2count861 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn861 + __81fgg2step861) / __81fgg2step861)), _znpjgsef = __81fgg2dlsvn861; __81fgg2count861 != 0; __81fgg2count861--, _znpjgsef += (__81fgg2step861)) {

						{
							
							if (*(_ta7zuy9k+(_znpjgsef - 1)) != _d0547bi2)
							{
								
								_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
								{
									System.Int32 __81fgg2dlsvn862 = (System.Int32)(_dxpq0xkr);
									System.Int32 __81fgg2step862 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count862;
									for (__81fgg2count862 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef + (int)1) - __81fgg2dlsvn862 + __81fgg2step862) / __81fgg2step862)), _b5p6od9s = __81fgg2dlsvn862; __81fgg2count862 != 0; __81fgg2count862--, _b5p6od9s += (__81fgg2step862)) {

									{
										
										*(_ta7zuy9k+(_b5p6od9s - 1)) = (*(_ta7zuy9k+(_b5p6od9s - 1)) + (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
Mark50:;
										// continue
									}
																		}								}
								if (_rcjmgxm4)
								*(_ta7zuy9k+(_znpjgsef - 1)) = (*(_ta7zuy9k+(_znpjgsef - 1)) * *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							}
							
Mark60:;
							// continue
						}
												}					}
				}
				else
				{
					
					_ziknm33t = (_ziknm33t + ((_dxpq0xkr - (int)1) * _1eqjusqc));
					_m3loivrh = _ziknm33t;
					{
						System.Int32 __81fgg2dlsvn863 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step863 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count863;
						for (__81fgg2count863 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn863 + __81fgg2step863) / __81fgg2step863)), _znpjgsef = __81fgg2dlsvn863; __81fgg2count863 != 0; __81fgg2count863--, _znpjgsef += (__81fgg2step863)) {

						{
							
							if (*(_ta7zuy9k+(_m3loivrh - 1)) != _d0547bi2)
							{
								
								_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
								_b69ritwm = _ziknm33t;
								{
									System.Int32 __81fgg2dlsvn864 = (System.Int32)(_dxpq0xkr);
									System.Int32 __81fgg2step864 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count864;
									for (__81fgg2count864 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef + (int)1) - __81fgg2dlsvn864 + __81fgg2step864) / __81fgg2step864)), _b5p6od9s = __81fgg2dlsvn864; __81fgg2count864 != 0; __81fgg2count864--, _b5p6od9s += (__81fgg2step864)) {

									{
										
										*(_ta7zuy9k+(_b69ritwm - 1)) = (*(_ta7zuy9k+(_b69ritwm - 1)) + (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
										_b69ritwm = (_b69ritwm - _1eqjusqc);
Mark70:;
										// continue
									}
																		}								}
								if (_rcjmgxm4)
								*(_ta7zuy9k+(_m3loivrh - 1)) = (*(_ta7zuy9k+(_m3loivrh - 1)) * *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							}
							
							_m3loivrh = (_m3loivrh - _1eqjusqc);
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
			//*        Form  x := A**T*x. 
			//* 
			
			if (_w8y2rzgy(_9wyre9zc ,"U" ))
			{
				
				if (_1eqjusqc == (int)1)
				{
					
					{
						System.Int32 __81fgg2dlsvn865 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step865 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count865;
						for (__81fgg2count865 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn865 + __81fgg2step865) / __81fgg2step865)), _znpjgsef = __81fgg2dlsvn865; __81fgg2count865 != 0; __81fgg2count865--, _znpjgsef += (__81fgg2step865)) {

						{
							
							_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
							if (_rcjmgxm4)
							_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							{
								System.Int32 __81fgg2dlsvn866 = (System.Int32)((_znpjgsef - (int)1));
								System.Int32 __81fgg2step866 = (System.Int32)((int)-1);
								System.Int32 __81fgg2count866;
								for (__81fgg2count866 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn866 + __81fgg2step866) / __81fgg2step866)), _b5p6od9s = __81fgg2dlsvn866; __81fgg2count866 != 0; __81fgg2count866--, _b5p6od9s += (__81fgg2step866)) {

								{
									
									_1ajfmh55 = (_1ajfmh55 + (*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * *(_ta7zuy9k+(_b5p6od9s - 1))));
Mark90:;
									// continue
								}
																}							}
							*(_ta7zuy9k+(_znpjgsef - 1)) = _1ajfmh55;
Mark100:;
							// continue
						}
												}					}
				}
				else
				{
					
					_m3loivrh = (_ziknm33t + ((_dxpq0xkr - (int)1) * _1eqjusqc));
					{
						System.Int32 __81fgg2dlsvn867 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step867 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count867;
						for (__81fgg2count867 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn867 + __81fgg2step867) / __81fgg2step867)), _znpjgsef = __81fgg2dlsvn867; __81fgg2count867 != 0; __81fgg2count867--, _znpjgsef += (__81fgg2step867)) {

						{
							
							_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
							_b69ritwm = _m3loivrh;
							if (_rcjmgxm4)
							_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							{
								System.Int32 __81fgg2dlsvn868 = (System.Int32)((_znpjgsef - (int)1));
								System.Int32 __81fgg2step868 = (System.Int32)((int)-1);
								System.Int32 __81fgg2count868;
								for (__81fgg2count868 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn868 + __81fgg2step868) / __81fgg2step868)), _b5p6od9s = __81fgg2dlsvn868; __81fgg2count868 != 0; __81fgg2count868--, _b5p6od9s += (__81fgg2step868)) {

								{
									
									_b69ritwm = (_b69ritwm - _1eqjusqc);
									_1ajfmh55 = (_1ajfmh55 + (*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * *(_ta7zuy9k+(_b69ritwm - 1))));
Mark110:;
									// continue
								}
																}							}
							*(_ta7zuy9k+(_m3loivrh - 1)) = _1ajfmh55;
							_m3loivrh = (_m3loivrh - _1eqjusqc);
Mark120:;
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
						System.Int32 __81fgg2dlsvn869 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step869 = (System.Int32)((int)1);
						System.Int32 __81fgg2count869;
						for (__81fgg2count869 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn869 + __81fgg2step869) / __81fgg2step869)), _znpjgsef = __81fgg2dlsvn869; __81fgg2count869 != 0; __81fgg2count869--, _znpjgsef += (__81fgg2step869)) {

						{
							
							_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
							if (_rcjmgxm4)
							_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							{
								System.Int32 __81fgg2dlsvn870 = (System.Int32)((_znpjgsef + (int)1));
								const System.Int32 __81fgg2step870 = (System.Int32)((int)1);
								System.Int32 __81fgg2count870;
								for (__81fgg2count870 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn870 + __81fgg2step870) / __81fgg2step870)), _b5p6od9s = __81fgg2dlsvn870; __81fgg2count870 != 0; __81fgg2count870--, _b5p6od9s += (__81fgg2step870)) {

								{
									
									_1ajfmh55 = (_1ajfmh55 + (*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * *(_ta7zuy9k+(_b5p6od9s - 1))));
Mark130:;
									// continue
								}
																}							}
							*(_ta7zuy9k+(_znpjgsef - 1)) = _1ajfmh55;
Mark140:;
							// continue
						}
												}					}
				}
				else
				{
					
					_m3loivrh = _ziknm33t;
					{
						System.Int32 __81fgg2dlsvn871 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step871 = (System.Int32)((int)1);
						System.Int32 __81fgg2count871;
						for (__81fgg2count871 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn871 + __81fgg2step871) / __81fgg2step871)), _znpjgsef = __81fgg2dlsvn871; __81fgg2count871 != 0; __81fgg2count871--, _znpjgsef += (__81fgg2step871)) {

						{
							
							_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
							_b69ritwm = _m3loivrh;
							if (_rcjmgxm4)
							_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							{
								System.Int32 __81fgg2dlsvn872 = (System.Int32)((_znpjgsef + (int)1));
								const System.Int32 __81fgg2step872 = (System.Int32)((int)1);
								System.Int32 __81fgg2count872;
								for (__81fgg2count872 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn872 + __81fgg2step872) / __81fgg2step872)), _b5p6od9s = __81fgg2dlsvn872; __81fgg2count872 != 0; __81fgg2count872--, _b5p6od9s += (__81fgg2step872)) {

								{
									
									_b69ritwm = (_b69ritwm + _1eqjusqc);
									_1ajfmh55 = (_1ajfmh55 + (*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * *(_ta7zuy9k+(_b69ritwm - 1))));
Mark150:;
									// continue
								}
																}							}
							*(_ta7zuy9k+(_m3loivrh - 1)) = _1ajfmh55;
							_m3loivrh = (_m3loivrh + _1eqjusqc);
Mark160:;
							// continue
						}
												}					}
				}
				
			}
			
		}
		//* 
		
		return;//* 
		//*     End of STRMV . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
