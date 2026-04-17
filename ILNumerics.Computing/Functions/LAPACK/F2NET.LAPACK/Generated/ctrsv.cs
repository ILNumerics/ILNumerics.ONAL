
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
//*> \brief \b CTRSV 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CTRSV(UPLO,TRANS,DIAG,N,A,LDA,X,INCX) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER INCX,LDA,N 
//*       CHARACTER DIAG,TRANS,UPLO 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX A(LDA,*),X(*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CTRSV  solves one of the systems of equations 
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
//*>          A is COMPLEX array, dimension ( LDA, N ) 
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
//*>          X is COMPLEX array, dimension at least 
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

	 
	public static void _354qig2c(FString _9wyre9zc, FString _scuo79v4, FString _2scffxp3, ref Int32 _dxpq0xkr, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, fcomplex* _ta7zuy9k, ref Int32 _1eqjusqc)
	{
#region variable declarations
fcomplex _d0547bi2 =   new fcomplex(0f,0f);
fcomplex _1ajfmh55 =  default;
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
			
			_ut9qalzx("CTRSV " ,ref _gro5yvfo );
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
						System.Int32 __81fgg2dlsvn2615 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step2615 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count2615;
						for (__81fgg2count2615 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn2615 + __81fgg2step2615) / __81fgg2step2615)), _znpjgsef = __81fgg2dlsvn2615; __81fgg2count2615 != 0; __81fgg2count2615--, _znpjgsef += (__81fgg2step2615)) {

						{
							
							if (*(_ta7zuy9k+(_znpjgsef - 1)) != _d0547bi2)
							{
								
								if (_rcjmgxm4)
								*(_ta7zuy9k+(_znpjgsef - 1)) = (*(_ta7zuy9k+(_znpjgsef - 1)) / *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
								_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
								{
									System.Int32 __81fgg2dlsvn2616 = (System.Int32)((_znpjgsef - (int)1));
									System.Int32 __81fgg2step2616 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count2616;
									for (__81fgg2count2616 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn2616 + __81fgg2step2616) / __81fgg2step2616)), _b5p6od9s = __81fgg2dlsvn2616; __81fgg2count2616 != 0; __81fgg2count2616--, _b5p6od9s += (__81fgg2step2616)) {

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
						System.Int32 __81fgg2dlsvn2617 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step2617 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count2617;
						for (__81fgg2count2617 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn2617 + __81fgg2step2617) / __81fgg2step2617)), _znpjgsef = __81fgg2dlsvn2617; __81fgg2count2617 != 0; __81fgg2count2617--, _znpjgsef += (__81fgg2step2617)) {

						{
							
							if (*(_ta7zuy9k+(_m3loivrh - 1)) != _d0547bi2)
							{
								
								if (_rcjmgxm4)
								*(_ta7zuy9k+(_m3loivrh - 1)) = (*(_ta7zuy9k+(_m3loivrh - 1)) / *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
								_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
								_b69ritwm = _m3loivrh;
								{
									System.Int32 __81fgg2dlsvn2618 = (System.Int32)((_znpjgsef - (int)1));
									System.Int32 __81fgg2step2618 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count2618;
									for (__81fgg2count2618 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn2618 + __81fgg2step2618) / __81fgg2step2618)), _b5p6od9s = __81fgg2dlsvn2618; __81fgg2count2618 != 0; __81fgg2count2618--, _b5p6od9s += (__81fgg2step2618)) {

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
						System.Int32 __81fgg2dlsvn2619 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step2619 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2619;
						for (__81fgg2count2619 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2619 + __81fgg2step2619) / __81fgg2step2619)), _znpjgsef = __81fgg2dlsvn2619; __81fgg2count2619 != 0; __81fgg2count2619--, _znpjgsef += (__81fgg2step2619)) {

						{
							
							if (*(_ta7zuy9k+(_znpjgsef - 1)) != _d0547bi2)
							{
								
								if (_rcjmgxm4)
								*(_ta7zuy9k+(_znpjgsef - 1)) = (*(_ta7zuy9k+(_znpjgsef - 1)) / *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
								_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
								{
									System.Int32 __81fgg2dlsvn2620 = (System.Int32)((_znpjgsef + (int)1));
									const System.Int32 __81fgg2step2620 = (System.Int32)((int)1);
									System.Int32 __81fgg2count2620;
									for (__81fgg2count2620 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2620 + __81fgg2step2620) / __81fgg2step2620)), _b5p6od9s = __81fgg2dlsvn2620; __81fgg2count2620 != 0; __81fgg2count2620--, _b5p6od9s += (__81fgg2step2620)) {

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
						System.Int32 __81fgg2dlsvn2621 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step2621 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2621;
						for (__81fgg2count2621 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2621 + __81fgg2step2621) / __81fgg2step2621)), _znpjgsef = __81fgg2dlsvn2621; __81fgg2count2621 != 0; __81fgg2count2621--, _znpjgsef += (__81fgg2step2621)) {

						{
							
							if (*(_ta7zuy9k+(_m3loivrh - 1)) != _d0547bi2)
							{
								
								if (_rcjmgxm4)
								*(_ta7zuy9k+(_m3loivrh - 1)) = (*(_ta7zuy9k+(_m3loivrh - 1)) / *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
								_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
								_b69ritwm = _m3loivrh;
								{
									System.Int32 __81fgg2dlsvn2622 = (System.Int32)((_znpjgsef + (int)1));
									const System.Int32 __81fgg2step2622 = (System.Int32)((int)1);
									System.Int32 __81fgg2count2622;
									for (__81fgg2count2622 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2622 + __81fgg2step2622) / __81fgg2step2622)), _b5p6od9s = __81fgg2dlsvn2622; __81fgg2count2622 != 0; __81fgg2count2622--, _b5p6od9s += (__81fgg2step2622)) {

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
						System.Int32 __81fgg2dlsvn2623 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step2623 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2623;
						for (__81fgg2count2623 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2623 + __81fgg2step2623) / __81fgg2step2623)), _znpjgsef = __81fgg2dlsvn2623; __81fgg2count2623 != 0; __81fgg2count2623--, _znpjgsef += (__81fgg2step2623)) {

						{
							
							_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
							if (_moml4lap)
							{
								
								{
									System.Int32 __81fgg2dlsvn2624 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step2624 = (System.Int32)((int)1);
									System.Int32 __81fgg2count2624;
									for (__81fgg2count2624 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn2624 + __81fgg2step2624) / __81fgg2step2624)), _b5p6od9s = __81fgg2dlsvn2624; __81fgg2count2624 != 0; __81fgg2count2624--, _b5p6od9s += (__81fgg2step2624)) {

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
									System.Int32 __81fgg2dlsvn2625 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step2625 = (System.Int32)((int)1);
									System.Int32 __81fgg2count2625;
									for (__81fgg2count2625 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn2625 + __81fgg2step2625) / __81fgg2step2625)), _b5p6od9s = __81fgg2dlsvn2625; __81fgg2count2625 != 0; __81fgg2count2625--, _b5p6od9s += (__81fgg2step2625)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 - (ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) * *(_ta7zuy9k+(_b5p6od9s - 1))));
Mark100:;
										// continue
									}
																		}								}
								if (_rcjmgxm4)
								_1ajfmh55 = (_1ajfmh55 / ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ));
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
						System.Int32 __81fgg2dlsvn2626 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step2626 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2626;
						for (__81fgg2count2626 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2626 + __81fgg2step2626) / __81fgg2step2626)), _znpjgsef = __81fgg2dlsvn2626; __81fgg2count2626 != 0; __81fgg2count2626--, _znpjgsef += (__81fgg2step2626)) {

						{
							
							_b69ritwm = _ziknm33t;
							_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
							if (_moml4lap)
							{
								
								{
									System.Int32 __81fgg2dlsvn2627 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step2627 = (System.Int32)((int)1);
									System.Int32 __81fgg2count2627;
									for (__81fgg2count2627 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn2627 + __81fgg2step2627) / __81fgg2step2627)), _b5p6od9s = __81fgg2dlsvn2627; __81fgg2count2627 != 0; __81fgg2count2627--, _b5p6od9s += (__81fgg2step2627)) {

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
									System.Int32 __81fgg2dlsvn2628 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step2628 = (System.Int32)((int)1);
									System.Int32 __81fgg2count2628;
									for (__81fgg2count2628 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn2628 + __81fgg2step2628) / __81fgg2step2628)), _b5p6od9s = __81fgg2dlsvn2628; __81fgg2count2628 != 0; __81fgg2count2628--, _b5p6od9s += (__81fgg2step2628)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 - (ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) * *(_ta7zuy9k+(_b69ritwm - 1))));
										_b69ritwm = (_b69ritwm + _1eqjusqc);
Mark130:;
										// continue
									}
																		}								}
								if (_rcjmgxm4)
								_1ajfmh55 = (_1ajfmh55 / ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ));
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
						System.Int32 __81fgg2dlsvn2629 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step2629 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count2629;
						for (__81fgg2count2629 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn2629 + __81fgg2step2629) / __81fgg2step2629)), _znpjgsef = __81fgg2dlsvn2629; __81fgg2count2629 != 0; __81fgg2count2629--, _znpjgsef += (__81fgg2step2629)) {

						{
							
							_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
							if (_moml4lap)
							{
								
								{
									System.Int32 __81fgg2dlsvn2630 = (System.Int32)(_dxpq0xkr);
									System.Int32 __81fgg2step2630 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count2630;
									for (__81fgg2count2630 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef + (int)1) - __81fgg2dlsvn2630 + __81fgg2step2630) / __81fgg2step2630)), _b5p6od9s = __81fgg2dlsvn2630; __81fgg2count2630 != 0; __81fgg2count2630--, _b5p6od9s += (__81fgg2step2630)) {

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
									System.Int32 __81fgg2dlsvn2631 = (System.Int32)(_dxpq0xkr);
									System.Int32 __81fgg2step2631 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count2631;
									for (__81fgg2count2631 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef + (int)1) - __81fgg2dlsvn2631 + __81fgg2step2631) / __81fgg2step2631)), _b5p6od9s = __81fgg2dlsvn2631; __81fgg2count2631 != 0; __81fgg2count2631--, _b5p6od9s += (__81fgg2step2631)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 - (ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) * *(_ta7zuy9k+(_b5p6od9s - 1))));
Mark160:;
										// continue
									}
																		}								}
								if (_rcjmgxm4)
								_1ajfmh55 = (_1ajfmh55 / ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ));
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
						System.Int32 __81fgg2dlsvn2632 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step2632 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count2632;
						for (__81fgg2count2632 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn2632 + __81fgg2step2632) / __81fgg2step2632)), _znpjgsef = __81fgg2dlsvn2632; __81fgg2count2632 != 0; __81fgg2count2632--, _znpjgsef += (__81fgg2step2632)) {

						{
							
							_b69ritwm = _ziknm33t;
							_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
							if (_moml4lap)
							{
								
								{
									System.Int32 __81fgg2dlsvn2633 = (System.Int32)(_dxpq0xkr);
									System.Int32 __81fgg2step2633 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count2633;
									for (__81fgg2count2633 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef + (int)1) - __81fgg2dlsvn2633 + __81fgg2step2633) / __81fgg2step2633)), _b5p6od9s = __81fgg2dlsvn2633; __81fgg2count2633 != 0; __81fgg2count2633--, _b5p6od9s += (__81fgg2step2633)) {

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
									System.Int32 __81fgg2dlsvn2634 = (System.Int32)(_dxpq0xkr);
									System.Int32 __81fgg2step2634 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count2634;
									for (__81fgg2count2634 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef + (int)1) - __81fgg2dlsvn2634 + __81fgg2step2634) / __81fgg2step2634)), _b5p6od9s = __81fgg2dlsvn2634; __81fgg2count2634 != 0; __81fgg2count2634--, _b5p6od9s += (__81fgg2step2634)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 - (ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) * *(_ta7zuy9k+(_b69ritwm - 1))));
										_b69ritwm = (_b69ritwm - _1eqjusqc);
Mark190:;
										// continue
									}
																		}								}
								if (_rcjmgxm4)
								_1ajfmh55 = (_1ajfmh55 / ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ));
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
		//*     End of CTRSV . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
