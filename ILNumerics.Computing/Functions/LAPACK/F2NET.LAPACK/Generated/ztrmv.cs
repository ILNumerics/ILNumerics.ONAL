
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
//*> \brief \b ZTRMV 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZTRMV(UPLO,TRANS,DIAG,N,A,LDA,X,INCX) 
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
//*> ZTRMV  performs one of the matrix-vector operations 
//*> 
//*>    x := A*x,   or   x := A**T*x,   or   x := A**H*x, 
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
//*>              TRANS = 'C' or 'c'   x := A**H*x. 
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
//*>          A is COMPLEX*16 array, dimension ( LDA, N ). 
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
//*> \ingroup complex16_blas_level2 
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

	 
	public static void _xajlj6s7(FString _9wyre9zc, FString _scuo79v4, FString _2scffxp3, ref Int32 _dxpq0xkr, complex* _vxfgpup9, ref Int32 _ocv8fk5c, complex* _ta7zuy9k, ref Int32 _1eqjusqc)
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
			
			_ut9qalzx("ZTRMV " ,ref _gro5yvfo );
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
			//*        Form  x := A*x. 
			//* 
			
			if (_w8y2rzgy(_9wyre9zc ,"U" ))
			{
				
				if (_1eqjusqc == (int)1)
				{
					
					{
						System.Int32 __81fgg2dlsvn1243 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1243 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1243;
						for (__81fgg2count1243 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1243 + __81fgg2step1243) / __81fgg2step1243)), _znpjgsef = __81fgg2dlsvn1243; __81fgg2count1243 != 0; __81fgg2count1243--, _znpjgsef += (__81fgg2step1243)) {

						{
							
							if (*(_ta7zuy9k+(_znpjgsef - 1)) != _d0547bi2)
							{
								
								_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
								{
									System.Int32 __81fgg2dlsvn1244 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1244 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1244;
									for (__81fgg2count1244 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn1244 + __81fgg2step1244) / __81fgg2step1244)), _b5p6od9s = __81fgg2dlsvn1244; __81fgg2count1244 != 0; __81fgg2count1244--, _b5p6od9s += (__81fgg2step1244)) {

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
						System.Int32 __81fgg2dlsvn1245 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1245 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1245;
						for (__81fgg2count1245 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1245 + __81fgg2step1245) / __81fgg2step1245)), _znpjgsef = __81fgg2dlsvn1245; __81fgg2count1245 != 0; __81fgg2count1245--, _znpjgsef += (__81fgg2step1245)) {

						{
							
							if (*(_ta7zuy9k+(_m3loivrh - 1)) != _d0547bi2)
							{
								
								_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
								_b69ritwm = _ziknm33t;
								{
									System.Int32 __81fgg2dlsvn1246 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1246 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1246;
									for (__81fgg2count1246 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn1246 + __81fgg2step1246) / __81fgg2step1246)), _b5p6od9s = __81fgg2dlsvn1246; __81fgg2count1246 != 0; __81fgg2count1246--, _b5p6od9s += (__81fgg2step1246)) {

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
						System.Int32 __81fgg2dlsvn1247 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step1247 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count1247;
						for (__81fgg2count1247 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1247 + __81fgg2step1247) / __81fgg2step1247)), _znpjgsef = __81fgg2dlsvn1247; __81fgg2count1247 != 0; __81fgg2count1247--, _znpjgsef += (__81fgg2step1247)) {

						{
							
							if (*(_ta7zuy9k+(_znpjgsef - 1)) != _d0547bi2)
							{
								
								_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
								{
									System.Int32 __81fgg2dlsvn1248 = (System.Int32)(_dxpq0xkr);
									System.Int32 __81fgg2step1248 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count1248;
									for (__81fgg2count1248 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef + (int)1) - __81fgg2dlsvn1248 + __81fgg2step1248) / __81fgg2step1248)), _b5p6od9s = __81fgg2dlsvn1248; __81fgg2count1248 != 0; __81fgg2count1248--, _b5p6od9s += (__81fgg2step1248)) {

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
						System.Int32 __81fgg2dlsvn1249 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step1249 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count1249;
						for (__81fgg2count1249 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1249 + __81fgg2step1249) / __81fgg2step1249)), _znpjgsef = __81fgg2dlsvn1249; __81fgg2count1249 != 0; __81fgg2count1249--, _znpjgsef += (__81fgg2step1249)) {

						{
							
							if (*(_ta7zuy9k+(_m3loivrh - 1)) != _d0547bi2)
							{
								
								_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
								_b69ritwm = _ziknm33t;
								{
									System.Int32 __81fgg2dlsvn1250 = (System.Int32)(_dxpq0xkr);
									System.Int32 __81fgg2step1250 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count1250;
									for (__81fgg2count1250 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef + (int)1) - __81fgg2dlsvn1250 + __81fgg2step1250) / __81fgg2step1250)), _b5p6od9s = __81fgg2dlsvn1250; __81fgg2count1250 != 0; __81fgg2count1250--, _b5p6od9s += (__81fgg2step1250)) {

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
			//*        Form  x := A**T*x  or  x := A**H*x. 
			//* 
			
			if (_w8y2rzgy(_9wyre9zc ,"U" ))
			{
				
				if (_1eqjusqc == (int)1)
				{
					
					{
						System.Int32 __81fgg2dlsvn1251 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step1251 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count1251;
						for (__81fgg2count1251 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1251 + __81fgg2step1251) / __81fgg2step1251)), _znpjgsef = __81fgg2dlsvn1251; __81fgg2count1251 != 0; __81fgg2count1251--, _znpjgsef += (__81fgg2step1251)) {

						{
							
							_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
							if (_moml4lap)
							{
								
								if (_rcjmgxm4)
								_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
								{
									System.Int32 __81fgg2dlsvn1252 = (System.Int32)((_znpjgsef - (int)1));
									System.Int32 __81fgg2step1252 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count1252;
									for (__81fgg2count1252 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1252 + __81fgg2step1252) / __81fgg2step1252)), _b5p6od9s = __81fgg2dlsvn1252; __81fgg2count1252 != 0; __81fgg2count1252--, _b5p6od9s += (__81fgg2step1252)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 + (*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * *(_ta7zuy9k+(_b5p6od9s - 1))));
Mark90:;
										// continue
									}
																		}								}
							}
							else
							{
								
								if (_rcjmgxm4)
								_1ajfmh55 = (_1ajfmh55 * ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ));
								{
									System.Int32 __81fgg2dlsvn1253 = (System.Int32)((_znpjgsef - (int)1));
									System.Int32 __81fgg2step1253 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count1253;
									for (__81fgg2count1253 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1253 + __81fgg2step1253) / __81fgg2step1253)), _b5p6od9s = __81fgg2dlsvn1253; __81fgg2count1253 != 0; __81fgg2count1253--, _b5p6od9s += (__81fgg2step1253)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 + (ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) * *(_ta7zuy9k+(_b5p6od9s - 1))));
Mark100:;
										// continue
									}
																		}								}
							}
							
							*(_ta7zuy9k+(_znpjgsef - 1)) = _1ajfmh55;
Mark110:;
							// continue
						}
												}					}
				}
				else
				{
					
					_m3loivrh = (_ziknm33t + ((_dxpq0xkr - (int)1) * _1eqjusqc));
					{
						System.Int32 __81fgg2dlsvn1254 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step1254 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count1254;
						for (__81fgg2count1254 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1254 + __81fgg2step1254) / __81fgg2step1254)), _znpjgsef = __81fgg2dlsvn1254; __81fgg2count1254 != 0; __81fgg2count1254--, _znpjgsef += (__81fgg2step1254)) {

						{
							
							_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
							_b69ritwm = _m3loivrh;
							if (_moml4lap)
							{
								
								if (_rcjmgxm4)
								_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
								{
									System.Int32 __81fgg2dlsvn1255 = (System.Int32)((_znpjgsef - (int)1));
									System.Int32 __81fgg2step1255 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count1255;
									for (__81fgg2count1255 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1255 + __81fgg2step1255) / __81fgg2step1255)), _b5p6od9s = __81fgg2dlsvn1255; __81fgg2count1255 != 0; __81fgg2count1255--, _b5p6od9s += (__81fgg2step1255)) {

									{
										
										_b69ritwm = (_b69ritwm - _1eqjusqc);
										_1ajfmh55 = (_1ajfmh55 + (*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * *(_ta7zuy9k+(_b69ritwm - 1))));
Mark120:;
										// continue
									}
																		}								}
							}
							else
							{
								
								if (_rcjmgxm4)
								_1ajfmh55 = (_1ajfmh55 * ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ));
								{
									System.Int32 __81fgg2dlsvn1256 = (System.Int32)((_znpjgsef - (int)1));
									System.Int32 __81fgg2step1256 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count1256;
									for (__81fgg2count1256 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1256 + __81fgg2step1256) / __81fgg2step1256)), _b5p6od9s = __81fgg2dlsvn1256; __81fgg2count1256 != 0; __81fgg2count1256--, _b5p6od9s += (__81fgg2step1256)) {

									{
										
										_b69ritwm = (_b69ritwm - _1eqjusqc);
										_1ajfmh55 = (_1ajfmh55 + (ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) * *(_ta7zuy9k+(_b69ritwm - 1))));
Mark130:;
										// continue
									}
																		}								}
							}
							
							*(_ta7zuy9k+(_m3loivrh - 1)) = _1ajfmh55;
							_m3loivrh = (_m3loivrh - _1eqjusqc);
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
						System.Int32 __81fgg2dlsvn1257 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1257 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1257;
						for (__81fgg2count1257 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1257 + __81fgg2step1257) / __81fgg2step1257)), _znpjgsef = __81fgg2dlsvn1257; __81fgg2count1257 != 0; __81fgg2count1257--, _znpjgsef += (__81fgg2step1257)) {

						{
							
							_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
							if (_moml4lap)
							{
								
								if (_rcjmgxm4)
								_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
								{
									System.Int32 __81fgg2dlsvn1258 = (System.Int32)((_znpjgsef + (int)1));
									const System.Int32 __81fgg2step1258 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1258;
									for (__81fgg2count1258 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1258 + __81fgg2step1258) / __81fgg2step1258)), _b5p6od9s = __81fgg2dlsvn1258; __81fgg2count1258 != 0; __81fgg2count1258--, _b5p6od9s += (__81fgg2step1258)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 + (*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * *(_ta7zuy9k+(_b5p6od9s - 1))));
Mark150:;
										// continue
									}
																		}								}
							}
							else
							{
								
								if (_rcjmgxm4)
								_1ajfmh55 = (_1ajfmh55 * ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ));
								{
									System.Int32 __81fgg2dlsvn1259 = (System.Int32)((_znpjgsef + (int)1));
									const System.Int32 __81fgg2step1259 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1259;
									for (__81fgg2count1259 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1259 + __81fgg2step1259) / __81fgg2step1259)), _b5p6od9s = __81fgg2dlsvn1259; __81fgg2count1259 != 0; __81fgg2count1259--, _b5p6od9s += (__81fgg2step1259)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 + (ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) * *(_ta7zuy9k+(_b5p6od9s - 1))));
Mark160:;
										// continue
									}
																		}								}
							}
							
							*(_ta7zuy9k+(_znpjgsef - 1)) = _1ajfmh55;
Mark170:;
							// continue
						}
												}					}
				}
				else
				{
					
					_m3loivrh = _ziknm33t;
					{
						System.Int32 __81fgg2dlsvn1260 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1260 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1260;
						for (__81fgg2count1260 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1260 + __81fgg2step1260) / __81fgg2step1260)), _znpjgsef = __81fgg2dlsvn1260; __81fgg2count1260 != 0; __81fgg2count1260--, _znpjgsef += (__81fgg2step1260)) {

						{
							
							_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
							_b69ritwm = _m3loivrh;
							if (_moml4lap)
							{
								
								if (_rcjmgxm4)
								_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
								{
									System.Int32 __81fgg2dlsvn1261 = (System.Int32)((_znpjgsef + (int)1));
									const System.Int32 __81fgg2step1261 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1261;
									for (__81fgg2count1261 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1261 + __81fgg2step1261) / __81fgg2step1261)), _b5p6od9s = __81fgg2dlsvn1261; __81fgg2count1261 != 0; __81fgg2count1261--, _b5p6od9s += (__81fgg2step1261)) {

									{
										
										_b69ritwm = (_b69ritwm + _1eqjusqc);
										_1ajfmh55 = (_1ajfmh55 + (*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * *(_ta7zuy9k+(_b69ritwm - 1))));
Mark180:;
										// continue
									}
																		}								}
							}
							else
							{
								
								if (_rcjmgxm4)
								_1ajfmh55 = (_1ajfmh55 * ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ));
								{
									System.Int32 __81fgg2dlsvn1262 = (System.Int32)((_znpjgsef + (int)1));
									const System.Int32 __81fgg2step1262 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1262;
									for (__81fgg2count1262 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1262 + __81fgg2step1262) / __81fgg2step1262)), _b5p6od9s = __81fgg2dlsvn1262; __81fgg2count1262 != 0; __81fgg2count1262--, _b5p6od9s += (__81fgg2step1262)) {

									{
										
										_b69ritwm = (_b69ritwm + _1eqjusqc);
										_1ajfmh55 = (_1ajfmh55 + (ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) * *(_ta7zuy9k+(_b69ritwm - 1))));
Mark190:;
										// continue
									}
																		}								}
							}
							
							*(_ta7zuy9k+(_m3loivrh - 1)) = _1ajfmh55;
							_m3loivrh = (_m3loivrh + _1eqjusqc);
Mark200:;
							// continue
						}
												}					}
				}
				
			}
			
		}
		//* 
		
		return;//* 
		//*     End of ZTRMV . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
