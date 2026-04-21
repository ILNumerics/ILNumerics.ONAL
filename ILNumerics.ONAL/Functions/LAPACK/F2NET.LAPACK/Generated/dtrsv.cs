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
//*> \brief \b DTRSV 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DTRSV(UPLO,TRANS,DIAG,N,A,LDA,X,INCX) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER INCX,LDA,N 
//*       CHARACTER DIAG,TRANS,UPLO 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION A(LDA,*),X(*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DTRSV  solves one of the systems of equations 
//*> 
//*>    A*x = b,   or   A**T*x = b, 
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
//*>              TRANS = 'C' or 'c'   A**T*x = b. 
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
//*>          A is DOUBLE PRECISION array, dimension ( LDA, N ) 
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
//*>          X is DOUBLE PRECISION array, dimension at least 
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
//*> 
//*>  Level 2 Blas routine. 
//*> 
//*>  -- Written on 22-October-1986. 
//*>     Jack Dongarra, Argonne National Lab. 
//*>     Jeremy Du Croz, Nag Central Office. 
//*>     Sven Hammarling, Nag Central Office. 
//*>     Richard Hanson, Sandia National Labs. 
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
//*> \ingroup double_blas_level1 
//* 
//*  ===================================================================== 

	 
	public static void _lhk0j7kt(FString _9wyre9zc, FString _scuo79v4, FString _2scffxp3, ref Int32 _dxpq0xkr, Double* _vxfgpup9, ref Int32 _ocv8fk5c, Double* _ta7zuy9k, ref Int32 _1eqjusqc)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Double _1ajfmh55 =  default;
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
		//*  -- Reference BLAS level1 routine (version 3.7.0) -- 
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
			
			_ut9qalzx("DTRSV " ,ref _gro5yvfo );
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
			//*        Form  x := inv( A )*x. 
			//* 
			
			if (_w8y2rzgy(_9wyre9zc ,"U" ))
			{
				
				if (_1eqjusqc == (int)1)
				{
					
					{
						System.Int32 __81fgg2dlsvn3747 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step3747 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count3747;
						for (__81fgg2count3747 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn3747 + __81fgg2step3747) / __81fgg2step3747)), _znpjgsef = __81fgg2dlsvn3747; __81fgg2count3747 != 0; __81fgg2count3747--, _znpjgsef += (__81fgg2step3747)) {

						{
							
							if (*(_ta7zuy9k+(_znpjgsef - 1)) != _d0547bi2)
							{
								
								if (_rcjmgxm4)
								*(_ta7zuy9k+(_znpjgsef - 1)) = (*(_ta7zuy9k+(_znpjgsef - 1)) / *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
								_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
								{
									System.Int32 __81fgg2dlsvn3748 = (System.Int32)((_znpjgsef - (int)1));
									System.Int32 __81fgg2step3748 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count3748;
									for (__81fgg2count3748 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn3748 + __81fgg2step3748) / __81fgg2step3748)), _b5p6od9s = __81fgg2dlsvn3748; __81fgg2count3748 != 0; __81fgg2count3748--, _b5p6od9s += (__81fgg2step3748)) {

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
						System.Int32 __81fgg2dlsvn3749 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step3749 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count3749;
						for (__81fgg2count3749 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn3749 + __81fgg2step3749) / __81fgg2step3749)), _znpjgsef = __81fgg2dlsvn3749; __81fgg2count3749 != 0; __81fgg2count3749--, _znpjgsef += (__81fgg2step3749)) {

						{
							
							if (*(_ta7zuy9k+(_m3loivrh - 1)) != _d0547bi2)
							{
								
								if (_rcjmgxm4)
								*(_ta7zuy9k+(_m3loivrh - 1)) = (*(_ta7zuy9k+(_m3loivrh - 1)) / *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
								_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
								_b69ritwm = _m3loivrh;
								{
									System.Int32 __81fgg2dlsvn3750 = (System.Int32)((_znpjgsef - (int)1));
									System.Int32 __81fgg2step3750 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count3750;
									for (__81fgg2count3750 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn3750 + __81fgg2step3750) / __81fgg2step3750)), _b5p6od9s = __81fgg2dlsvn3750; __81fgg2count3750 != 0; __81fgg2count3750--, _b5p6od9s += (__81fgg2step3750)) {

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
						System.Int32 __81fgg2dlsvn3751 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3751 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3751;
						for (__81fgg2count3751 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3751 + __81fgg2step3751) / __81fgg2step3751)), _znpjgsef = __81fgg2dlsvn3751; __81fgg2count3751 != 0; __81fgg2count3751--, _znpjgsef += (__81fgg2step3751)) {

						{
							
							if (*(_ta7zuy9k+(_znpjgsef - 1)) != _d0547bi2)
							{
								
								if (_rcjmgxm4)
								*(_ta7zuy9k+(_znpjgsef - 1)) = (*(_ta7zuy9k+(_znpjgsef - 1)) / *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
								_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
								{
									System.Int32 __81fgg2dlsvn3752 = (System.Int32)((_znpjgsef + (int)1));
									const System.Int32 __81fgg2step3752 = (System.Int32)((int)1);
									System.Int32 __81fgg2count3752;
									for (__81fgg2count3752 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3752 + __81fgg2step3752) / __81fgg2step3752)), _b5p6od9s = __81fgg2dlsvn3752; __81fgg2count3752 != 0; __81fgg2count3752--, _b5p6od9s += (__81fgg2step3752)) {

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
						System.Int32 __81fgg2dlsvn3753 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3753 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3753;
						for (__81fgg2count3753 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3753 + __81fgg2step3753) / __81fgg2step3753)), _znpjgsef = __81fgg2dlsvn3753; __81fgg2count3753 != 0; __81fgg2count3753--, _znpjgsef += (__81fgg2step3753)) {

						{
							
							if (*(_ta7zuy9k+(_m3loivrh - 1)) != _d0547bi2)
							{
								
								if (_rcjmgxm4)
								*(_ta7zuy9k+(_m3loivrh - 1)) = (*(_ta7zuy9k+(_m3loivrh - 1)) / *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
								_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
								_b69ritwm = _m3loivrh;
								{
									System.Int32 __81fgg2dlsvn3754 = (System.Int32)((_znpjgsef + (int)1));
									const System.Int32 __81fgg2step3754 = (System.Int32)((int)1);
									System.Int32 __81fgg2count3754;
									for (__81fgg2count3754 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3754 + __81fgg2step3754) / __81fgg2step3754)), _b5p6od9s = __81fgg2dlsvn3754; __81fgg2count3754 != 0; __81fgg2count3754--, _b5p6od9s += (__81fgg2step3754)) {

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
			//*        Form  x := inv( A**T )*x. 
			//* 
			
			if (_w8y2rzgy(_9wyre9zc ,"U" ))
			{
				
				if (_1eqjusqc == (int)1)
				{
					
					{
						System.Int32 __81fgg2dlsvn3755 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3755 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3755;
						for (__81fgg2count3755 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3755 + __81fgg2step3755) / __81fgg2step3755)), _znpjgsef = __81fgg2dlsvn3755; __81fgg2count3755 != 0; __81fgg2count3755--, _znpjgsef += (__81fgg2step3755)) {

						{
							
							_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
							{
								System.Int32 __81fgg2dlsvn3756 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3756 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3756;
								for (__81fgg2count3756 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn3756 + __81fgg2step3756) / __81fgg2step3756)), _b5p6od9s = __81fgg2dlsvn3756; __81fgg2count3756 != 0; __81fgg2count3756--, _b5p6od9s += (__81fgg2step3756)) {

								{
									
									_1ajfmh55 = (_1ajfmh55 - (*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * *(_ta7zuy9k+(_b5p6od9s - 1))));
Mark90:;
									// continue
								}
																}							}
							if (_rcjmgxm4)
							_1ajfmh55 = (_1ajfmh55 / *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							*(_ta7zuy9k+(_znpjgsef - 1)) = _1ajfmh55;
Mark100:;
							// continue
						}
												}					}
				}
				else
				{
					
					_m3loivrh = _ziknm33t;
					{
						System.Int32 __81fgg2dlsvn3757 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3757 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3757;
						for (__81fgg2count3757 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3757 + __81fgg2step3757) / __81fgg2step3757)), _znpjgsef = __81fgg2dlsvn3757; __81fgg2count3757 != 0; __81fgg2count3757--, _znpjgsef += (__81fgg2step3757)) {

						{
							
							_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
							_b69ritwm = _ziknm33t;
							{
								System.Int32 __81fgg2dlsvn3758 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3758 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3758;
								for (__81fgg2count3758 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn3758 + __81fgg2step3758) / __81fgg2step3758)), _b5p6od9s = __81fgg2dlsvn3758; __81fgg2count3758 != 0; __81fgg2count3758--, _b5p6od9s += (__81fgg2step3758)) {

								{
									
									_1ajfmh55 = (_1ajfmh55 - (*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * *(_ta7zuy9k+(_b69ritwm - 1))));
									_b69ritwm = (_b69ritwm + _1eqjusqc);
Mark110:;
									// continue
								}
																}							}
							if (_rcjmgxm4)
							_1ajfmh55 = (_1ajfmh55 / *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							*(_ta7zuy9k+(_m3loivrh - 1)) = _1ajfmh55;
							_m3loivrh = (_m3loivrh + _1eqjusqc);
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
						System.Int32 __81fgg2dlsvn3759 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step3759 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count3759;
						for (__81fgg2count3759 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn3759 + __81fgg2step3759) / __81fgg2step3759)), _znpjgsef = __81fgg2dlsvn3759; __81fgg2count3759 != 0; __81fgg2count3759--, _znpjgsef += (__81fgg2step3759)) {

						{
							
							_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
							{
								System.Int32 __81fgg2dlsvn3760 = (System.Int32)(_dxpq0xkr);
								System.Int32 __81fgg2step3760 = (System.Int32)((int)-1);
								System.Int32 __81fgg2count3760;
								for (__81fgg2count3760 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef + (int)1) - __81fgg2dlsvn3760 + __81fgg2step3760) / __81fgg2step3760)), _b5p6od9s = __81fgg2dlsvn3760; __81fgg2count3760 != 0; __81fgg2count3760--, _b5p6od9s += (__81fgg2step3760)) {

								{
									
									_1ajfmh55 = (_1ajfmh55 - (*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * *(_ta7zuy9k+(_b5p6od9s - 1))));
Mark130:;
									// continue
								}
																}							}
							if (_rcjmgxm4)
							_1ajfmh55 = (_1ajfmh55 / *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							*(_ta7zuy9k+(_znpjgsef - 1)) = _1ajfmh55;
Mark140:;
							// continue
						}
												}					}
				}
				else
				{
					
					_ziknm33t = (_ziknm33t + ((_dxpq0xkr - (int)1) * _1eqjusqc));
					_m3loivrh = _ziknm33t;
					{
						System.Int32 __81fgg2dlsvn3761 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step3761 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count3761;
						for (__81fgg2count3761 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn3761 + __81fgg2step3761) / __81fgg2step3761)), _znpjgsef = __81fgg2dlsvn3761; __81fgg2count3761 != 0; __81fgg2count3761--, _znpjgsef += (__81fgg2step3761)) {

						{
							
							_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
							_b69ritwm = _ziknm33t;
							{
								System.Int32 __81fgg2dlsvn3762 = (System.Int32)(_dxpq0xkr);
								System.Int32 __81fgg2step3762 = (System.Int32)((int)-1);
								System.Int32 __81fgg2count3762;
								for (__81fgg2count3762 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef + (int)1) - __81fgg2dlsvn3762 + __81fgg2step3762) / __81fgg2step3762)), _b5p6od9s = __81fgg2dlsvn3762; __81fgg2count3762 != 0; __81fgg2count3762--, _b5p6od9s += (__81fgg2step3762)) {

								{
									
									_1ajfmh55 = (_1ajfmh55 - (*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * *(_ta7zuy9k+(_b69ritwm - 1))));
									_b69ritwm = (_b69ritwm - _1eqjusqc);
Mark150:;
									// continue
								}
																}							}
							if (_rcjmgxm4)
							_1ajfmh55 = (_1ajfmh55 / *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							*(_ta7zuy9k+(_m3loivrh - 1)) = _1ajfmh55;
							_m3loivrh = (_m3loivrh - _1eqjusqc);
Mark160:;
							// continue
						}
												}					}
				}
				
			}
			
		}
		//* 
		
		return;//* 
		//*     End of DTRSV . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
