
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
//*> \brief \b CTRMV 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CTRMV(UPLO,TRANS,DIAG,N,A,LDA,X,INCX) 
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
//*> CTRMV  performs one of the matrix-vector operations 
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
//*>          A is COMPLEX array, dimension ( LDA, N ). 
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

	 
	public static void _09cah3zx(FString _9wyre9zc, FString _scuo79v4, FString _2scffxp3, ref Int32 _dxpq0xkr, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, fcomplex* _ta7zuy9k, ref Int32 _1eqjusqc)
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
			
			_ut9qalzx("CTRMV " ,ref _gro5yvfo );
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
						System.Int32 __81fgg2dlsvn1018 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1018 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1018;
						for (__81fgg2count1018 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1018 + __81fgg2step1018) / __81fgg2step1018)), _znpjgsef = __81fgg2dlsvn1018; __81fgg2count1018 != 0; __81fgg2count1018--, _znpjgsef += (__81fgg2step1018)) {

						{
							
							if (*(_ta7zuy9k+(_znpjgsef - 1)) != _d0547bi2)
							{
								
								_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
								{
									System.Int32 __81fgg2dlsvn1019 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1019 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1019;
									for (__81fgg2count1019 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn1019 + __81fgg2step1019) / __81fgg2step1019)), _b5p6od9s = __81fgg2dlsvn1019; __81fgg2count1019 != 0; __81fgg2count1019--, _b5p6od9s += (__81fgg2step1019)) {

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
						System.Int32 __81fgg2dlsvn1020 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1020 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1020;
						for (__81fgg2count1020 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1020 + __81fgg2step1020) / __81fgg2step1020)), _znpjgsef = __81fgg2dlsvn1020; __81fgg2count1020 != 0; __81fgg2count1020--, _znpjgsef += (__81fgg2step1020)) {

						{
							
							if (*(_ta7zuy9k+(_m3loivrh - 1)) != _d0547bi2)
							{
								
								_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
								_b69ritwm = _ziknm33t;
								{
									System.Int32 __81fgg2dlsvn1021 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1021 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1021;
									for (__81fgg2count1021 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn1021 + __81fgg2step1021) / __81fgg2step1021)), _b5p6od9s = __81fgg2dlsvn1021; __81fgg2count1021 != 0; __81fgg2count1021--, _b5p6od9s += (__81fgg2step1021)) {

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
						System.Int32 __81fgg2dlsvn1022 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step1022 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count1022;
						for (__81fgg2count1022 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1022 + __81fgg2step1022) / __81fgg2step1022)), _znpjgsef = __81fgg2dlsvn1022; __81fgg2count1022 != 0; __81fgg2count1022--, _znpjgsef += (__81fgg2step1022)) {

						{
							
							if (*(_ta7zuy9k+(_znpjgsef - 1)) != _d0547bi2)
							{
								
								_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
								{
									System.Int32 __81fgg2dlsvn1023 = (System.Int32)(_dxpq0xkr);
									System.Int32 __81fgg2step1023 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count1023;
									for (__81fgg2count1023 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef + (int)1) - __81fgg2dlsvn1023 + __81fgg2step1023) / __81fgg2step1023)), _b5p6od9s = __81fgg2dlsvn1023; __81fgg2count1023 != 0; __81fgg2count1023--, _b5p6od9s += (__81fgg2step1023)) {

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
						System.Int32 __81fgg2dlsvn1024 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step1024 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count1024;
						for (__81fgg2count1024 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1024 + __81fgg2step1024) / __81fgg2step1024)), _znpjgsef = __81fgg2dlsvn1024; __81fgg2count1024 != 0; __81fgg2count1024--, _znpjgsef += (__81fgg2step1024)) {

						{
							
							if (*(_ta7zuy9k+(_m3loivrh - 1)) != _d0547bi2)
							{
								
								_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
								_b69ritwm = _ziknm33t;
								{
									System.Int32 __81fgg2dlsvn1025 = (System.Int32)(_dxpq0xkr);
									System.Int32 __81fgg2step1025 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count1025;
									for (__81fgg2count1025 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef + (int)1) - __81fgg2dlsvn1025 + __81fgg2step1025) / __81fgg2step1025)), _b5p6od9s = __81fgg2dlsvn1025; __81fgg2count1025 != 0; __81fgg2count1025--, _b5p6od9s += (__81fgg2step1025)) {

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
						System.Int32 __81fgg2dlsvn1026 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step1026 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count1026;
						for (__81fgg2count1026 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1026 + __81fgg2step1026) / __81fgg2step1026)), _znpjgsef = __81fgg2dlsvn1026; __81fgg2count1026 != 0; __81fgg2count1026--, _znpjgsef += (__81fgg2step1026)) {

						{
							
							_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
							if (_moml4lap)
							{
								
								if (_rcjmgxm4)
								_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
								{
									System.Int32 __81fgg2dlsvn1027 = (System.Int32)((_znpjgsef - (int)1));
									System.Int32 __81fgg2step1027 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count1027;
									for (__81fgg2count1027 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1027 + __81fgg2step1027) / __81fgg2step1027)), _b5p6od9s = __81fgg2dlsvn1027; __81fgg2count1027 != 0; __81fgg2count1027--, _b5p6od9s += (__81fgg2step1027)) {

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
								_1ajfmh55 = (_1ajfmh55 * ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ));
								{
									System.Int32 __81fgg2dlsvn1028 = (System.Int32)((_znpjgsef - (int)1));
									System.Int32 __81fgg2step1028 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count1028;
									for (__81fgg2count1028 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1028 + __81fgg2step1028) / __81fgg2step1028)), _b5p6od9s = __81fgg2dlsvn1028; __81fgg2count1028 != 0; __81fgg2count1028--, _b5p6od9s += (__81fgg2step1028)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 + (ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) * *(_ta7zuy9k+(_b5p6od9s - 1))));
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
						System.Int32 __81fgg2dlsvn1029 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step1029 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count1029;
						for (__81fgg2count1029 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1029 + __81fgg2step1029) / __81fgg2step1029)), _znpjgsef = __81fgg2dlsvn1029; __81fgg2count1029 != 0; __81fgg2count1029--, _znpjgsef += (__81fgg2step1029)) {

						{
							
							_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
							_b69ritwm = _m3loivrh;
							if (_moml4lap)
							{
								
								if (_rcjmgxm4)
								_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
								{
									System.Int32 __81fgg2dlsvn1030 = (System.Int32)((_znpjgsef - (int)1));
									System.Int32 __81fgg2step1030 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count1030;
									for (__81fgg2count1030 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1030 + __81fgg2step1030) / __81fgg2step1030)), _b5p6od9s = __81fgg2dlsvn1030; __81fgg2count1030 != 0; __81fgg2count1030--, _b5p6od9s += (__81fgg2step1030)) {

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
								_1ajfmh55 = (_1ajfmh55 * ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ));
								{
									System.Int32 __81fgg2dlsvn1031 = (System.Int32)((_znpjgsef - (int)1));
									System.Int32 __81fgg2step1031 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count1031;
									for (__81fgg2count1031 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1031 + __81fgg2step1031) / __81fgg2step1031)), _b5p6od9s = __81fgg2dlsvn1031; __81fgg2count1031 != 0; __81fgg2count1031--, _b5p6od9s += (__81fgg2step1031)) {

									{
										
										_b69ritwm = (_b69ritwm - _1eqjusqc);
										_1ajfmh55 = (_1ajfmh55 + (ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) * *(_ta7zuy9k+(_b69ritwm - 1))));
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
						System.Int32 __81fgg2dlsvn1032 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1032 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1032;
						for (__81fgg2count1032 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1032 + __81fgg2step1032) / __81fgg2step1032)), _znpjgsef = __81fgg2dlsvn1032; __81fgg2count1032 != 0; __81fgg2count1032--, _znpjgsef += (__81fgg2step1032)) {

						{
							
							_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
							if (_moml4lap)
							{
								
								if (_rcjmgxm4)
								_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
								{
									System.Int32 __81fgg2dlsvn1033 = (System.Int32)((_znpjgsef + (int)1));
									const System.Int32 __81fgg2step1033 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1033;
									for (__81fgg2count1033 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1033 + __81fgg2step1033) / __81fgg2step1033)), _b5p6od9s = __81fgg2dlsvn1033; __81fgg2count1033 != 0; __81fgg2count1033--, _b5p6od9s += (__81fgg2step1033)) {

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
								_1ajfmh55 = (_1ajfmh55 * ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ));
								{
									System.Int32 __81fgg2dlsvn1034 = (System.Int32)((_znpjgsef + (int)1));
									const System.Int32 __81fgg2step1034 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1034;
									for (__81fgg2count1034 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1034 + __81fgg2step1034) / __81fgg2step1034)), _b5p6od9s = __81fgg2dlsvn1034; __81fgg2count1034 != 0; __81fgg2count1034--, _b5p6od9s += (__81fgg2step1034)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 + (ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) * *(_ta7zuy9k+(_b5p6od9s - 1))));
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
						System.Int32 __81fgg2dlsvn1035 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1035 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1035;
						for (__81fgg2count1035 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1035 + __81fgg2step1035) / __81fgg2step1035)), _znpjgsef = __81fgg2dlsvn1035; __81fgg2count1035 != 0; __81fgg2count1035--, _znpjgsef += (__81fgg2step1035)) {

						{
							
							_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
							_b69ritwm = _m3loivrh;
							if (_moml4lap)
							{
								
								if (_rcjmgxm4)
								_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
								{
									System.Int32 __81fgg2dlsvn1036 = (System.Int32)((_znpjgsef + (int)1));
									const System.Int32 __81fgg2step1036 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1036;
									for (__81fgg2count1036 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1036 + __81fgg2step1036) / __81fgg2step1036)), _b5p6od9s = __81fgg2dlsvn1036; __81fgg2count1036 != 0; __81fgg2count1036--, _b5p6od9s += (__81fgg2step1036)) {

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
								_1ajfmh55 = (_1ajfmh55 * ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ));
								{
									System.Int32 __81fgg2dlsvn1037 = (System.Int32)((_znpjgsef + (int)1));
									const System.Int32 __81fgg2step1037 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1037;
									for (__81fgg2count1037 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1037 + __81fgg2step1037) / __81fgg2step1037)), _b5p6od9s = __81fgg2dlsvn1037; __81fgg2count1037 != 0; __81fgg2count1037--, _b5p6od9s += (__81fgg2step1037)) {

									{
										
										_b69ritwm = (_b69ritwm + _1eqjusqc);
										_1ajfmh55 = (_1ajfmh55 + (ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) * *(_ta7zuy9k+(_b69ritwm - 1))));
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
		//*     End of CTRMV . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
