
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
//*> \brief \b DTRMV 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DTRMV(UPLO,TRANS,DIAG,N,A,LDA,X,INCX) 
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
//*> DTRMV  performs one of the matrix-vector operations 
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

	 
	public static void _lg2hqio3(FString _9wyre9zc, FString _scuo79v4, FString _2scffxp3, ref Int32 _dxpq0xkr, Double* _vxfgpup9, ref Int32 _ocv8fk5c, Double* _ta7zuy9k, ref Int32 _1eqjusqc)
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
			
			_ut9qalzx("DTRMV " ,ref _gro5yvfo );
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
						System.Int32 __81fgg2dlsvn482 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step482 = (System.Int32)((int)1);
						System.Int32 __81fgg2count482;
						for (__81fgg2count482 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn482 + __81fgg2step482) / __81fgg2step482)), _znpjgsef = __81fgg2dlsvn482; __81fgg2count482 != 0; __81fgg2count482--, _znpjgsef += (__81fgg2step482)) {

						{
							
							if (*(_ta7zuy9k+(_znpjgsef - 1)) != _d0547bi2)
							{
								
								_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
								{
									System.Int32 __81fgg2dlsvn483 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step483 = (System.Int32)((int)1);
									System.Int32 __81fgg2count483;
									for (__81fgg2count483 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn483 + __81fgg2step483) / __81fgg2step483)), _b5p6od9s = __81fgg2dlsvn483; __81fgg2count483 != 0; __81fgg2count483--, _b5p6od9s += (__81fgg2step483)) {

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
						System.Int32 __81fgg2dlsvn484 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step484 = (System.Int32)((int)1);
						System.Int32 __81fgg2count484;
						for (__81fgg2count484 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn484 + __81fgg2step484) / __81fgg2step484)), _znpjgsef = __81fgg2dlsvn484; __81fgg2count484 != 0; __81fgg2count484--, _znpjgsef += (__81fgg2step484)) {

						{
							
							if (*(_ta7zuy9k+(_m3loivrh - 1)) != _d0547bi2)
							{
								
								_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
								_b69ritwm = _ziknm33t;
								{
									System.Int32 __81fgg2dlsvn485 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step485 = (System.Int32)((int)1);
									System.Int32 __81fgg2count485;
									for (__81fgg2count485 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn485 + __81fgg2step485) / __81fgg2step485)), _b5p6od9s = __81fgg2dlsvn485; __81fgg2count485 != 0; __81fgg2count485--, _b5p6od9s += (__81fgg2step485)) {

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
						System.Int32 __81fgg2dlsvn486 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step486 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count486;
						for (__81fgg2count486 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn486 + __81fgg2step486) / __81fgg2step486)), _znpjgsef = __81fgg2dlsvn486; __81fgg2count486 != 0; __81fgg2count486--, _znpjgsef += (__81fgg2step486)) {

						{
							
							if (*(_ta7zuy9k+(_znpjgsef - 1)) != _d0547bi2)
							{
								
								_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
								{
									System.Int32 __81fgg2dlsvn487 = (System.Int32)(_dxpq0xkr);
									System.Int32 __81fgg2step487 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count487;
									for (__81fgg2count487 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef + (int)1) - __81fgg2dlsvn487 + __81fgg2step487) / __81fgg2step487)), _b5p6od9s = __81fgg2dlsvn487; __81fgg2count487 != 0; __81fgg2count487--, _b5p6od9s += (__81fgg2step487)) {

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
						System.Int32 __81fgg2dlsvn488 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step488 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count488;
						for (__81fgg2count488 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn488 + __81fgg2step488) / __81fgg2step488)), _znpjgsef = __81fgg2dlsvn488; __81fgg2count488 != 0; __81fgg2count488--, _znpjgsef += (__81fgg2step488)) {

						{
							
							if (*(_ta7zuy9k+(_m3loivrh - 1)) != _d0547bi2)
							{
								
								_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
								_b69ritwm = _ziknm33t;
								{
									System.Int32 __81fgg2dlsvn489 = (System.Int32)(_dxpq0xkr);
									System.Int32 __81fgg2step489 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count489;
									for (__81fgg2count489 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef + (int)1) - __81fgg2dlsvn489 + __81fgg2step489) / __81fgg2step489)), _b5p6od9s = __81fgg2dlsvn489; __81fgg2count489 != 0; __81fgg2count489--, _b5p6od9s += (__81fgg2step489)) {

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
						System.Int32 __81fgg2dlsvn490 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step490 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count490;
						for (__81fgg2count490 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn490 + __81fgg2step490) / __81fgg2step490)), _znpjgsef = __81fgg2dlsvn490; __81fgg2count490 != 0; __81fgg2count490--, _znpjgsef += (__81fgg2step490)) {

						{
							
							_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
							if (_rcjmgxm4)
							_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							{
								System.Int32 __81fgg2dlsvn491 = (System.Int32)((_znpjgsef - (int)1));
								System.Int32 __81fgg2step491 = (System.Int32)((int)-1);
								System.Int32 __81fgg2count491;
								for (__81fgg2count491 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn491 + __81fgg2step491) / __81fgg2step491)), _b5p6od9s = __81fgg2dlsvn491; __81fgg2count491 != 0; __81fgg2count491--, _b5p6od9s += (__81fgg2step491)) {

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
						System.Int32 __81fgg2dlsvn492 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step492 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count492;
						for (__81fgg2count492 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn492 + __81fgg2step492) / __81fgg2step492)), _znpjgsef = __81fgg2dlsvn492; __81fgg2count492 != 0; __81fgg2count492--, _znpjgsef += (__81fgg2step492)) {

						{
							
							_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
							_b69ritwm = _m3loivrh;
							if (_rcjmgxm4)
							_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							{
								System.Int32 __81fgg2dlsvn493 = (System.Int32)((_znpjgsef - (int)1));
								System.Int32 __81fgg2step493 = (System.Int32)((int)-1);
								System.Int32 __81fgg2count493;
								for (__81fgg2count493 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn493 + __81fgg2step493) / __81fgg2step493)), _b5p6od9s = __81fgg2dlsvn493; __81fgg2count493 != 0; __81fgg2count493--, _b5p6od9s += (__81fgg2step493)) {

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
						System.Int32 __81fgg2dlsvn494 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step494 = (System.Int32)((int)1);
						System.Int32 __81fgg2count494;
						for (__81fgg2count494 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn494 + __81fgg2step494) / __81fgg2step494)), _znpjgsef = __81fgg2dlsvn494; __81fgg2count494 != 0; __81fgg2count494--, _znpjgsef += (__81fgg2step494)) {

						{
							
							_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
							if (_rcjmgxm4)
							_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							{
								System.Int32 __81fgg2dlsvn495 = (System.Int32)((_znpjgsef + (int)1));
								const System.Int32 __81fgg2step495 = (System.Int32)((int)1);
								System.Int32 __81fgg2count495;
								for (__81fgg2count495 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn495 + __81fgg2step495) / __81fgg2step495)), _b5p6od9s = __81fgg2dlsvn495; __81fgg2count495 != 0; __81fgg2count495--, _b5p6od9s += (__81fgg2step495)) {

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
						System.Int32 __81fgg2dlsvn496 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step496 = (System.Int32)((int)1);
						System.Int32 __81fgg2count496;
						for (__81fgg2count496 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn496 + __81fgg2step496) / __81fgg2step496)), _znpjgsef = __81fgg2dlsvn496; __81fgg2count496 != 0; __81fgg2count496--, _znpjgsef += (__81fgg2step496)) {

						{
							
							_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
							_b69ritwm = _m3loivrh;
							if (_rcjmgxm4)
							_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							{
								System.Int32 __81fgg2dlsvn497 = (System.Int32)((_znpjgsef + (int)1));
								const System.Int32 __81fgg2step497 = (System.Int32)((int)1);
								System.Int32 __81fgg2count497;
								for (__81fgg2count497 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn497 + __81fgg2step497) / __81fgg2step497)), _b5p6od9s = __81fgg2dlsvn497; __81fgg2count497 != 0; __81fgg2count497--, _b5p6od9s += (__81fgg2step497)) {

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
		//*     End of DTRMV . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
