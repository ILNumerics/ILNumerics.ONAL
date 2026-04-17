
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
//*> \brief \b DSYRK 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DSYRK(UPLO,TRANS,N,K,ALPHA,A,LDA,BETA,C,LDC) 
//* 
//*       .. Scalar Arguments .. 
//*       DOUBLE PRECISION ALPHA,BETA 
//*       INTEGER K,LDA,LDC,N 
//*       CHARACTER TRANS,UPLO 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION A(LDA,*),C(LDC,*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DSYRK  performs one of the symmetric rank k operations 
//*> 
//*>    C := alpha*A*A**T + beta*C, 
//*> 
//*> or 
//*> 
//*>    C := alpha*A**T*A + beta*C, 
//*> 
//*> where  alpha and beta  are scalars, C is an  n by n  symmetric matrix 
//*> and  A  is an  n by k  matrix in the first case and a  k by n  matrix 
//*> in the second case. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] UPLO 
//*> \verbatim 
//*>          UPLO is CHARACTER*1 
//*>           On  entry,   UPLO  specifies  whether  the  upper  or  lower 
//*>           triangular  part  of the  array  C  is to be  referenced  as 
//*>           follows: 
//*> 
//*>              UPLO = 'U' or 'u'   Only the  upper triangular part of  C 
//*>                                  is to be referenced. 
//*> 
//*>              UPLO = 'L' or 'l'   Only the  lower triangular part of  C 
//*>                                  is to be referenced. 
//*> \endverbatim 
//*> 
//*> \param[in] TRANS 
//*> \verbatim 
//*>          TRANS is CHARACTER*1 
//*>           On entry,  TRANS  specifies the operation to be performed as 
//*>           follows: 
//*> 
//*>              TRANS = 'N' or 'n'   C := alpha*A*A**T + beta*C. 
//*> 
//*>              TRANS = 'T' or 't'   C := alpha*A**T*A + beta*C. 
//*> 
//*>              TRANS = 'C' or 'c'   C := alpha*A**T*A + beta*C. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>           On entry,  N specifies the order of the matrix C.  N must be 
//*>           at least zero. 
//*> \endverbatim 
//*> 
//*> \param[in] K 
//*> \verbatim 
//*>          K is INTEGER 
//*>           On entry with  TRANS = 'N' or 'n',  K  specifies  the number 
//*>           of  columns   of  the   matrix   A,   and  on   entry   with 
//*>           TRANS = 'T' or 't' or 'C' or 'c',  K  specifies  the  number 
//*>           of rows of the matrix  A.  K must be at least zero. 
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
//*>          A is DOUBLE PRECISION array, dimension ( LDA, ka ), where ka is 
//*>           k  when  TRANS = 'N' or 'n',  and is  n  otherwise. 
//*>           Before entry with  TRANS = 'N' or 'n',  the  leading  n by k 
//*>           part of the array  A  must contain the matrix  A,  otherwise 
//*>           the leading  k by n  part of the array  A  must contain  the 
//*>           matrix A. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>           On entry, LDA specifies the first dimension of A as declared 
//*>           in  the  calling  (sub)  program.   When  TRANS = 'N' or 'n' 
//*>           then  LDA must be at least  max( 1, n ), otherwise  LDA must 
//*>           be at least  max( 1, k ). 
//*> \endverbatim 
//*> 
//*> \param[in] BETA 
//*> \verbatim 
//*>          BETA is DOUBLE PRECISION. 
//*>           On entry, BETA specifies the scalar beta. 
//*> \endverbatim 
//*> 
//*> \param[in,out] C 
//*> \verbatim 
//*>          C is DOUBLE PRECISION array, dimension ( LDC, N ) 
//*>           Before entry  with  UPLO = 'U' or 'u',  the leading  n by n 
//*>           upper triangular part of the array C must contain the upper 
//*>           triangular part  of the  symmetric matrix  and the strictly 
//*>           lower triangular part of C is not referenced.  On exit, the 
//*>           upper triangular part of the array  C is overwritten by the 
//*>           upper triangular part of the updated matrix. 
//*>           Before entry  with  UPLO = 'L' or 'l',  the leading  n by n 
//*>           lower triangular part of the array C must contain the lower 
//*>           triangular part  of the  symmetric matrix  and the strictly 
//*>           upper triangular part of C is not referenced.  On exit, the 
//*>           lower triangular part of the array  C is overwritten by the 
//*>           lower triangular part of the updated matrix. 
//*> \endverbatim 
//*> 
//*> \param[in] LDC 
//*> \verbatim 
//*>          LDC is INTEGER 
//*>           On entry, LDC specifies the first dimension of C as declared 
//*>           in  the  calling  (sub)  program.   LDC  must  be  at  least 
//*>           max( 1, n ). 
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
//*> \ingroup double_blas_level3 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  Level 3 Blas routine. 
//*> 
//*>  -- Written on 8-February-1989. 
//*>     Jack Dongarra, Argonne National Laboratory. 
//*>     Iain Duff, AERE Harwell. 
//*>     Jeremy Du Croz, Numerical Algorithms Group Ltd. 
//*>     Sven Hammarling, Numerical Algorithms Group Ltd. 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _1u4gibh6(FString _9wyre9zc, FString _scuo79v4, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, ref Double _r7cfteg3, Double* _vxfgpup9, ref Int32 _ocv8fk5c, ref Double _bafcbx97, Double* _3crf0qn3, ref Int32 _1s3eymp4)
	{
#region variable declarations
Double _1ajfmh55 =  default;
Int32 _b5p6od9s =  default;
Int32 _gro5yvfo =  default;
Int32 _znpjgsef =  default;
Int32 _68ec3gbh =  default;
Int32 _o9a6qdux =  default;
Boolean _l08igmvf =  default;
Double _kxg5drh2 =  1d;
Double _d0547bi2 =  0d;
string fLanavab = default;
#endregion  variable declarations
_9wyre9zc = _9wyre9zc.Convert(1);
_scuo79v4 = _scuo79v4.Convert(1);

	{
		//* 
		//*  -- Reference BLAS level3 routine (version 3.7.0) -- 
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
		//*     .. External Functions .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. Parameters .. 
		//*     .. 
		//* 
		//*     Test the input parameters. 
		//* 
		
		if (_w8y2rzgy(_scuo79v4 ,"N" ))
		{
			
			_o9a6qdux = _dxpq0xkr;
		}
		else
		{
			
			_o9a6qdux = _umlkckdg;
		}
		
		_l08igmvf = _w8y2rzgy(_9wyre9zc ,"U" );//* 
		
		_gro5yvfo = (int)0;
		if ((!(_l08igmvf)) & (!(_w8y2rzgy(_9wyre9zc ,"L" ))))
		{
			
			_gro5yvfo = (int)1;
		}
		else
		if (((!(_w8y2rzgy(_scuo79v4 ,"N" ))) & (!(_w8y2rzgy(_scuo79v4 ,"T" )))) & (!(_w8y2rzgy(_scuo79v4 ,"C" ))))
		{
			
			_gro5yvfo = (int)2;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)3;
		}
		else
		if (_umlkckdg < (int)0)
		{
			
			_gro5yvfo = (int)4;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_o9a6qdux ))
		{
			
			_gro5yvfo = (int)7;
		}
		else
		if (_1s3eymp4 < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)10;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("DSYRK " ,ref _gro5yvfo );
			return;
		}
		//* 
		//*     Quick return if possible. 
		//* 
		
		if ((_dxpq0xkr == (int)0) | (((_r7cfteg3 == _d0547bi2) | (_umlkckdg == (int)0)) & (_bafcbx97 == _kxg5drh2)))
		return;//* 
		//*     And when  alpha.eq.zero. 
		//* 
		
		if (_r7cfteg3 == _d0547bi2)
		{
			
			if (_l08igmvf)
			{
				
				if (_bafcbx97 == _d0547bi2)
				{
					
					{
						System.Int32 __81fgg2dlsvn1462 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1462 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1462;
						for (__81fgg2count1462 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1462 + __81fgg2step1462) / __81fgg2step1462)), _znpjgsef = __81fgg2dlsvn1462; __81fgg2count1462 != 0; __81fgg2count1462--, _znpjgsef += (__81fgg2step1462)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1463 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1463 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1463;
								for (__81fgg2count1463 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn1463 + __81fgg2step1463) / __81fgg2step1463)), _b5p6od9s = __81fgg2dlsvn1463; __81fgg2count1463 != 0; __81fgg2count1463--, _b5p6od9s += (__81fgg2step1463)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = _d0547bi2;
Mark10:;
									// continue
								}
																}							}
Mark20:;
							// continue
						}
												}					}
				}
				else
				{
					
					{
						System.Int32 __81fgg2dlsvn1464 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1464 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1464;
						for (__81fgg2count1464 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1464 + __81fgg2step1464) / __81fgg2step1464)), _znpjgsef = __81fgg2dlsvn1464; __81fgg2count1464 != 0; __81fgg2count1464--, _znpjgsef += (__81fgg2step1464)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1465 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1465 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1465;
								for (__81fgg2count1465 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn1465 + __81fgg2step1465) / __81fgg2step1465)), _b5p6od9s = __81fgg2dlsvn1465; __81fgg2count1465 != 0; __81fgg2count1465--, _b5p6od9s += (__81fgg2step1465)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark30:;
									// continue
								}
																}							}
Mark40:;
							// continue
						}
												}					}
				}
				
			}
			else
			{
				
				if (_bafcbx97 == _d0547bi2)
				{
					
					{
						System.Int32 __81fgg2dlsvn1466 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1466 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1466;
						for (__81fgg2count1466 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1466 + __81fgg2step1466) / __81fgg2step1466)), _znpjgsef = __81fgg2dlsvn1466; __81fgg2count1466 != 0; __81fgg2count1466--, _znpjgsef += (__81fgg2step1466)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1467 = (System.Int32)(_znpjgsef);
								const System.Int32 __81fgg2step1467 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1467;
								for (__81fgg2count1467 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1467 + __81fgg2step1467) / __81fgg2step1467)), _b5p6od9s = __81fgg2dlsvn1467; __81fgg2count1467 != 0; __81fgg2count1467--, _b5p6od9s += (__81fgg2step1467)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = _d0547bi2;
Mark50:;
									// continue
								}
																}							}
Mark60:;
							// continue
						}
												}					}
				}
				else
				{
					
					{
						System.Int32 __81fgg2dlsvn1468 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1468 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1468;
						for (__81fgg2count1468 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1468 + __81fgg2step1468) / __81fgg2step1468)), _znpjgsef = __81fgg2dlsvn1468; __81fgg2count1468 != 0; __81fgg2count1468--, _znpjgsef += (__81fgg2step1468)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1469 = (System.Int32)(_znpjgsef);
								const System.Int32 __81fgg2step1469 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1469;
								for (__81fgg2count1469 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1469 + __81fgg2step1469) / __81fgg2step1469)), _b5p6od9s = __81fgg2dlsvn1469; __81fgg2count1469 != 0; __81fgg2count1469--, _b5p6od9s += (__81fgg2step1469)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark70:;
									// continue
								}
																}							}
Mark80:;
							// continue
						}
												}					}
				}
				
			}
			
			return;
		}
		//* 
		//*     Start the operations. 
		//* 
		
		if (_w8y2rzgy(_scuo79v4 ,"N" ))
		{
			//* 
			//*        Form  C := alpha*A*A**T + beta*C. 
			//* 
			
			if (_l08igmvf)
			{
				
				{
					System.Int32 __81fgg2dlsvn1470 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1470 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1470;
					for (__81fgg2count1470 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1470 + __81fgg2step1470) / __81fgg2step1470)), _znpjgsef = __81fgg2dlsvn1470; __81fgg2count1470 != 0; __81fgg2count1470--, _znpjgsef += (__81fgg2step1470)) {

					{
						
						if (_bafcbx97 == _d0547bi2)
						{
							
							{
								System.Int32 __81fgg2dlsvn1471 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1471 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1471;
								for (__81fgg2count1471 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn1471 + __81fgg2step1471) / __81fgg2step1471)), _b5p6od9s = __81fgg2dlsvn1471; __81fgg2count1471 != 0; __81fgg2count1471--, _b5p6od9s += (__81fgg2step1471)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = _d0547bi2;
Mark90:;
									// continue
								}
																}							}
						}
						else
						if (_bafcbx97 != _kxg5drh2)
						{
							
							{
								System.Int32 __81fgg2dlsvn1472 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1472 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1472;
								for (__81fgg2count1472 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn1472 + __81fgg2step1472) / __81fgg2step1472)), _b5p6od9s = __81fgg2dlsvn1472; __81fgg2count1472 != 0; __81fgg2count1472--, _b5p6od9s += (__81fgg2step1472)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark100:;
									// continue
								}
																}							}
						}
						
						{
							System.Int32 __81fgg2dlsvn1473 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step1473 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1473;
							for (__81fgg2count1473 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1473 + __81fgg2step1473) / __81fgg2step1473)), _68ec3gbh = __81fgg2dlsvn1473; __81fgg2count1473 != 0; __81fgg2count1473--, _68ec3gbh += (__81fgg2step1473)) {

							{
								
								if (*(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
								{
									
									_1ajfmh55 = (_r7cfteg3 * *(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)));
									{
										System.Int32 __81fgg2dlsvn1474 = (System.Int32)((int)1);
										const System.Int32 __81fgg2step1474 = (System.Int32)((int)1);
										System.Int32 __81fgg2count1474;
										for (__81fgg2count1474 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn1474 + __81fgg2step1474) / __81fgg2step1474)), _b5p6od9s = __81fgg2dlsvn1474; __81fgg2count1474 != 0; __81fgg2count1474--, _b5p6od9s += (__81fgg2step1474)) {

										{
											
											*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) + (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c))));
Mark110:;
											// continue
										}
																				}									}
								}
								
Mark120:;
								// continue
							}
														}						}
Mark130:;
						// continue
					}
										}				}
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn1475 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1475 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1475;
					for (__81fgg2count1475 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1475 + __81fgg2step1475) / __81fgg2step1475)), _znpjgsef = __81fgg2dlsvn1475; __81fgg2count1475 != 0; __81fgg2count1475--, _znpjgsef += (__81fgg2step1475)) {

					{
						
						if (_bafcbx97 == _d0547bi2)
						{
							
							{
								System.Int32 __81fgg2dlsvn1476 = (System.Int32)(_znpjgsef);
								const System.Int32 __81fgg2step1476 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1476;
								for (__81fgg2count1476 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1476 + __81fgg2step1476) / __81fgg2step1476)), _b5p6od9s = __81fgg2dlsvn1476; __81fgg2count1476 != 0; __81fgg2count1476--, _b5p6od9s += (__81fgg2step1476)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = _d0547bi2;
Mark140:;
									// continue
								}
																}							}
						}
						else
						if (_bafcbx97 != _kxg5drh2)
						{
							
							{
								System.Int32 __81fgg2dlsvn1477 = (System.Int32)(_znpjgsef);
								const System.Int32 __81fgg2step1477 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1477;
								for (__81fgg2count1477 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1477 + __81fgg2step1477) / __81fgg2step1477)), _b5p6od9s = __81fgg2dlsvn1477; __81fgg2count1477 != 0; __81fgg2count1477--, _b5p6od9s += (__81fgg2step1477)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark150:;
									// continue
								}
																}							}
						}
						
						{
							System.Int32 __81fgg2dlsvn1478 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step1478 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1478;
							for (__81fgg2count1478 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1478 + __81fgg2step1478) / __81fgg2step1478)), _68ec3gbh = __81fgg2dlsvn1478; __81fgg2count1478 != 0; __81fgg2count1478--, _68ec3gbh += (__81fgg2step1478)) {

							{
								
								if (*(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
								{
									
									_1ajfmh55 = (_r7cfteg3 * *(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)));
									{
										System.Int32 __81fgg2dlsvn1479 = (System.Int32)(_znpjgsef);
										const System.Int32 __81fgg2step1479 = (System.Int32)((int)1);
										System.Int32 __81fgg2count1479;
										for (__81fgg2count1479 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1479 + __81fgg2step1479) / __81fgg2step1479)), _b5p6od9s = __81fgg2dlsvn1479; __81fgg2count1479 != 0; __81fgg2count1479--, _b5p6od9s += (__81fgg2step1479)) {

										{
											
											*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) + (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c))));
Mark160:;
											// continue
										}
																				}									}
								}
								
Mark170:;
								// continue
							}
														}						}
Mark180:;
						// continue
					}
										}				}
			}
			
		}
		else
		{
			//* 
			//*        Form  C := alpha*A**T*A + beta*C. 
			//* 
			
			if (_l08igmvf)
			{
				
				{
					System.Int32 __81fgg2dlsvn1480 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1480 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1480;
					for (__81fgg2count1480 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1480 + __81fgg2step1480) / __81fgg2step1480)), _znpjgsef = __81fgg2dlsvn1480; __81fgg2count1480 != 0; __81fgg2count1480--, _znpjgsef += (__81fgg2step1480)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn1481 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step1481 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1481;
							for (__81fgg2count1481 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn1481 + __81fgg2step1481) / __81fgg2step1481)), _b5p6od9s = __81fgg2dlsvn1481; __81fgg2count1481 != 0; __81fgg2count1481--, _b5p6od9s += (__81fgg2step1481)) {

							{
								
								_1ajfmh55 = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn1482 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1482 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1482;
									for (__81fgg2count1482 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1482 + __81fgg2step1482) / __81fgg2step1482)), _68ec3gbh = __81fgg2dlsvn1482; __81fgg2count1482 != 0; __81fgg2count1482--, _68ec3gbh += (__81fgg2step1482)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 + (*(_vxfgpup9+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) * *(_vxfgpup9+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
Mark190:;
										// continue
									}
																		}								}
								if (_bafcbx97 == _d0547bi2)
								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_r7cfteg3 * _1ajfmh55);
								}
								else
								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = ((_r7cfteg3 * _1ajfmh55) + (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))));
								}
								
Mark200:;
								// continue
							}
														}						}
Mark210:;
						// continue
					}
										}				}
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn1483 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1483 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1483;
					for (__81fgg2count1483 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1483 + __81fgg2step1483) / __81fgg2step1483)), _znpjgsef = __81fgg2dlsvn1483; __81fgg2count1483 != 0; __81fgg2count1483--, _znpjgsef += (__81fgg2step1483)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn1484 = (System.Int32)(_znpjgsef);
							const System.Int32 __81fgg2step1484 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1484;
							for (__81fgg2count1484 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1484 + __81fgg2step1484) / __81fgg2step1484)), _b5p6od9s = __81fgg2dlsvn1484; __81fgg2count1484 != 0; __81fgg2count1484--, _b5p6od9s += (__81fgg2step1484)) {

							{
								
								_1ajfmh55 = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn1485 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1485 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1485;
									for (__81fgg2count1485 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1485 + __81fgg2step1485) / __81fgg2step1485)), _68ec3gbh = __81fgg2dlsvn1485; __81fgg2count1485 != 0; __81fgg2count1485--, _68ec3gbh += (__81fgg2step1485)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 + (*(_vxfgpup9+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) * *(_vxfgpup9+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
Mark220:;
										// continue
									}
																		}								}
								if (_bafcbx97 == _d0547bi2)
								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_r7cfteg3 * _1ajfmh55);
								}
								else
								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = ((_r7cfteg3 * _1ajfmh55) + (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))));
								}
								
Mark230:;
								// continue
							}
														}						}
Mark240:;
						// continue
					}
										}				}
			}
			
		}
		//* 
		
		return;//* 
		//*     End of DSYRK . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
