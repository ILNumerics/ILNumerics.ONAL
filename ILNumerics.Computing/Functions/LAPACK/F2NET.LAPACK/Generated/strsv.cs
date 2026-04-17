
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
//*> \brief \b STRSV 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE STRSV(UPLO,TRANS,DIAG,N,A,LDA,X,INCX) 
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
//*> STRSV  solves one of the systems of equations 
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
//*> \ingroup single_blas_level2 
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

	 
	public static void _eu05nebp(FString _9wyre9zc, FString _scuo79v4, FString _2scffxp3, ref Int32 _dxpq0xkr, Single* _vxfgpup9, ref Int32 _ocv8fk5c, Single* _ta7zuy9k, ref Int32 _1eqjusqc)
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
			
			_ut9qalzx("STRSV " ,ref _gro5yvfo );
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
						System.Int32 __81fgg2dlsvn3811 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step3811 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count3811;
						for (__81fgg2count3811 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn3811 + __81fgg2step3811) / __81fgg2step3811)), _znpjgsef = __81fgg2dlsvn3811; __81fgg2count3811 != 0; __81fgg2count3811--, _znpjgsef += (__81fgg2step3811)) {

						{
							
							if (*(_ta7zuy9k+(_znpjgsef - 1)) != _d0547bi2)
							{
								
								if (_rcjmgxm4)
								*(_ta7zuy9k+(_znpjgsef - 1)) = (*(_ta7zuy9k+(_znpjgsef - 1)) / *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
								_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
								{
									System.Int32 __81fgg2dlsvn3812 = (System.Int32)((_znpjgsef - (int)1));
									System.Int32 __81fgg2step3812 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count3812;
									for (__81fgg2count3812 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn3812 + __81fgg2step3812) / __81fgg2step3812)), _b5p6od9s = __81fgg2dlsvn3812; __81fgg2count3812 != 0; __81fgg2count3812--, _b5p6od9s += (__81fgg2step3812)) {

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
						System.Int32 __81fgg2dlsvn3813 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step3813 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count3813;
						for (__81fgg2count3813 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn3813 + __81fgg2step3813) / __81fgg2step3813)), _znpjgsef = __81fgg2dlsvn3813; __81fgg2count3813 != 0; __81fgg2count3813--, _znpjgsef += (__81fgg2step3813)) {

						{
							
							if (*(_ta7zuy9k+(_m3loivrh - 1)) != _d0547bi2)
							{
								
								if (_rcjmgxm4)
								*(_ta7zuy9k+(_m3loivrh - 1)) = (*(_ta7zuy9k+(_m3loivrh - 1)) / *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
								_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
								_b69ritwm = _m3loivrh;
								{
									System.Int32 __81fgg2dlsvn3814 = (System.Int32)((_znpjgsef - (int)1));
									System.Int32 __81fgg2step3814 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count3814;
									for (__81fgg2count3814 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn3814 + __81fgg2step3814) / __81fgg2step3814)), _b5p6od9s = __81fgg2dlsvn3814; __81fgg2count3814 != 0; __81fgg2count3814--, _b5p6od9s += (__81fgg2step3814)) {

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
						System.Int32 __81fgg2dlsvn3815 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3815 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3815;
						for (__81fgg2count3815 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3815 + __81fgg2step3815) / __81fgg2step3815)), _znpjgsef = __81fgg2dlsvn3815; __81fgg2count3815 != 0; __81fgg2count3815--, _znpjgsef += (__81fgg2step3815)) {

						{
							
							if (*(_ta7zuy9k+(_znpjgsef - 1)) != _d0547bi2)
							{
								
								if (_rcjmgxm4)
								*(_ta7zuy9k+(_znpjgsef - 1)) = (*(_ta7zuy9k+(_znpjgsef - 1)) / *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
								_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
								{
									System.Int32 __81fgg2dlsvn3816 = (System.Int32)((_znpjgsef + (int)1));
									const System.Int32 __81fgg2step3816 = (System.Int32)((int)1);
									System.Int32 __81fgg2count3816;
									for (__81fgg2count3816 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3816 + __81fgg2step3816) / __81fgg2step3816)), _b5p6od9s = __81fgg2dlsvn3816; __81fgg2count3816 != 0; __81fgg2count3816--, _b5p6od9s += (__81fgg2step3816)) {

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
						System.Int32 __81fgg2dlsvn3817 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3817 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3817;
						for (__81fgg2count3817 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3817 + __81fgg2step3817) / __81fgg2step3817)), _znpjgsef = __81fgg2dlsvn3817; __81fgg2count3817 != 0; __81fgg2count3817--, _znpjgsef += (__81fgg2step3817)) {

						{
							
							if (*(_ta7zuy9k+(_m3loivrh - 1)) != _d0547bi2)
							{
								
								if (_rcjmgxm4)
								*(_ta7zuy9k+(_m3loivrh - 1)) = (*(_ta7zuy9k+(_m3loivrh - 1)) / *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
								_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
								_b69ritwm = _m3loivrh;
								{
									System.Int32 __81fgg2dlsvn3818 = (System.Int32)((_znpjgsef + (int)1));
									const System.Int32 __81fgg2step3818 = (System.Int32)((int)1);
									System.Int32 __81fgg2count3818;
									for (__81fgg2count3818 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3818 + __81fgg2step3818) / __81fgg2step3818)), _b5p6od9s = __81fgg2dlsvn3818; __81fgg2count3818 != 0; __81fgg2count3818--, _b5p6od9s += (__81fgg2step3818)) {

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
						System.Int32 __81fgg2dlsvn3819 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3819 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3819;
						for (__81fgg2count3819 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3819 + __81fgg2step3819) / __81fgg2step3819)), _znpjgsef = __81fgg2dlsvn3819; __81fgg2count3819 != 0; __81fgg2count3819--, _znpjgsef += (__81fgg2step3819)) {

						{
							
							_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
							{
								System.Int32 __81fgg2dlsvn3820 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3820 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3820;
								for (__81fgg2count3820 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn3820 + __81fgg2step3820) / __81fgg2step3820)), _b5p6od9s = __81fgg2dlsvn3820; __81fgg2count3820 != 0; __81fgg2count3820--, _b5p6od9s += (__81fgg2step3820)) {

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
						System.Int32 __81fgg2dlsvn3821 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3821 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3821;
						for (__81fgg2count3821 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3821 + __81fgg2step3821) / __81fgg2step3821)), _znpjgsef = __81fgg2dlsvn3821; __81fgg2count3821 != 0; __81fgg2count3821--, _znpjgsef += (__81fgg2step3821)) {

						{
							
							_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
							_b69ritwm = _ziknm33t;
							{
								System.Int32 __81fgg2dlsvn3822 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3822 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3822;
								for (__81fgg2count3822 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn3822 + __81fgg2step3822) / __81fgg2step3822)), _b5p6od9s = __81fgg2dlsvn3822; __81fgg2count3822 != 0; __81fgg2count3822--, _b5p6od9s += (__81fgg2step3822)) {

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
						System.Int32 __81fgg2dlsvn3823 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step3823 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count3823;
						for (__81fgg2count3823 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn3823 + __81fgg2step3823) / __81fgg2step3823)), _znpjgsef = __81fgg2dlsvn3823; __81fgg2count3823 != 0; __81fgg2count3823--, _znpjgsef += (__81fgg2step3823)) {

						{
							
							_1ajfmh55 = *(_ta7zuy9k+(_znpjgsef - 1));
							{
								System.Int32 __81fgg2dlsvn3824 = (System.Int32)(_dxpq0xkr);
								System.Int32 __81fgg2step3824 = (System.Int32)((int)-1);
								System.Int32 __81fgg2count3824;
								for (__81fgg2count3824 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef + (int)1) - __81fgg2dlsvn3824 + __81fgg2step3824) / __81fgg2step3824)), _b5p6od9s = __81fgg2dlsvn3824; __81fgg2count3824 != 0; __81fgg2count3824--, _b5p6od9s += (__81fgg2step3824)) {

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
						System.Int32 __81fgg2dlsvn3825 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step3825 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count3825;
						for (__81fgg2count3825 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn3825 + __81fgg2step3825) / __81fgg2step3825)), _znpjgsef = __81fgg2dlsvn3825; __81fgg2count3825 != 0; __81fgg2count3825--, _znpjgsef += (__81fgg2step3825)) {

						{
							
							_1ajfmh55 = *(_ta7zuy9k+(_m3loivrh - 1));
							_b69ritwm = _ziknm33t;
							{
								System.Int32 __81fgg2dlsvn3826 = (System.Int32)(_dxpq0xkr);
								System.Int32 __81fgg2step3826 = (System.Int32)((int)-1);
								System.Int32 __81fgg2count3826;
								for (__81fgg2count3826 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef + (int)1) - __81fgg2dlsvn3826 + __81fgg2step3826) / __81fgg2step3826)), _b5p6od9s = __81fgg2dlsvn3826; __81fgg2count3826 != 0; __81fgg2count3826--, _b5p6od9s += (__81fgg2step3826)) {

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
		//*     End of STRSV . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
