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
//*> \brief \b SSYRK 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SSYRK(UPLO,TRANS,N,K,ALPHA,A,LDA,BETA,C,LDC) 
//* 
//*       .. Scalar Arguments .. 
//*       REAL ALPHA,BETA 
//*       INTEGER K,LDA,LDC,N 
//*       CHARACTER TRANS,UPLO 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL A(LDA,*),C(LDC,*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SSYRK  performs one of the symmetric rank k operations 
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
//*>          ALPHA is REAL 
//*>           On entry, ALPHA specifies the scalar alpha. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is REAL array, dimension ( LDA, ka ), where ka is 
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
//*>          BETA is REAL 
//*>           On entry, BETA specifies the scalar beta. 
//*> \endverbatim 
//*> 
//*> \param[in,out] C 
//*> \verbatim 
//*>          C is REAL array, dimension ( LDC, N ) 
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
//*> \ingroup single_blas_level3 
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

	 
	public static void _6r4stbkx(FString _9wyre9zc, FString _scuo79v4, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, ref Single _r7cfteg3, Single* _vxfgpup9, ref Int32 _ocv8fk5c, ref Single _bafcbx97, Single* _3crf0qn3, ref Int32 _1s3eymp4)
	{
#region variable declarations
Single _1ajfmh55 =  default;
Int32 _b5p6od9s =  default;
Int32 _gro5yvfo =  default;
Int32 _znpjgsef =  default;
Int32 _68ec3gbh =  default;
Int32 _o9a6qdux =  default;
Boolean _l08igmvf =  default;
Single _kxg5drh2 =  1f;
Single _d0547bi2 =  0f;
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
			
			_ut9qalzx("SSYRK " ,ref _gro5yvfo );
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
						System.Int32 __81fgg2dlsvn1524 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1524 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1524;
						for (__81fgg2count1524 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1524 + __81fgg2step1524) / __81fgg2step1524)), _znpjgsef = __81fgg2dlsvn1524; __81fgg2count1524 != 0; __81fgg2count1524--, _znpjgsef += (__81fgg2step1524)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1525 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1525 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1525;
								for (__81fgg2count1525 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn1525 + __81fgg2step1525) / __81fgg2step1525)), _b5p6od9s = __81fgg2dlsvn1525; __81fgg2count1525 != 0; __81fgg2count1525--, _b5p6od9s += (__81fgg2step1525)) {

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
						System.Int32 __81fgg2dlsvn1526 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1526 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1526;
						for (__81fgg2count1526 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1526 + __81fgg2step1526) / __81fgg2step1526)), _znpjgsef = __81fgg2dlsvn1526; __81fgg2count1526 != 0; __81fgg2count1526--, _znpjgsef += (__81fgg2step1526)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1527 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1527 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1527;
								for (__81fgg2count1527 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn1527 + __81fgg2step1527) / __81fgg2step1527)), _b5p6od9s = __81fgg2dlsvn1527; __81fgg2count1527 != 0; __81fgg2count1527--, _b5p6od9s += (__81fgg2step1527)) {

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
						System.Int32 __81fgg2dlsvn1528 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1528 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1528;
						for (__81fgg2count1528 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1528 + __81fgg2step1528) / __81fgg2step1528)), _znpjgsef = __81fgg2dlsvn1528; __81fgg2count1528 != 0; __81fgg2count1528--, _znpjgsef += (__81fgg2step1528)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1529 = (System.Int32)(_znpjgsef);
								const System.Int32 __81fgg2step1529 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1529;
								for (__81fgg2count1529 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1529 + __81fgg2step1529) / __81fgg2step1529)), _b5p6od9s = __81fgg2dlsvn1529; __81fgg2count1529 != 0; __81fgg2count1529--, _b5p6od9s += (__81fgg2step1529)) {

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
						System.Int32 __81fgg2dlsvn1530 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1530 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1530;
						for (__81fgg2count1530 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1530 + __81fgg2step1530) / __81fgg2step1530)), _znpjgsef = __81fgg2dlsvn1530; __81fgg2count1530 != 0; __81fgg2count1530--, _znpjgsef += (__81fgg2step1530)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1531 = (System.Int32)(_znpjgsef);
								const System.Int32 __81fgg2step1531 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1531;
								for (__81fgg2count1531 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1531 + __81fgg2step1531) / __81fgg2step1531)), _b5p6od9s = __81fgg2dlsvn1531; __81fgg2count1531 != 0; __81fgg2count1531--, _b5p6od9s += (__81fgg2step1531)) {

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
					System.Int32 __81fgg2dlsvn1532 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1532 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1532;
					for (__81fgg2count1532 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1532 + __81fgg2step1532) / __81fgg2step1532)), _znpjgsef = __81fgg2dlsvn1532; __81fgg2count1532 != 0; __81fgg2count1532--, _znpjgsef += (__81fgg2step1532)) {

					{
						
						if (_bafcbx97 == _d0547bi2)
						{
							
							{
								System.Int32 __81fgg2dlsvn1533 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1533 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1533;
								for (__81fgg2count1533 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn1533 + __81fgg2step1533) / __81fgg2step1533)), _b5p6od9s = __81fgg2dlsvn1533; __81fgg2count1533 != 0; __81fgg2count1533--, _b5p6od9s += (__81fgg2step1533)) {

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
								System.Int32 __81fgg2dlsvn1534 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1534 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1534;
								for (__81fgg2count1534 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn1534 + __81fgg2step1534) / __81fgg2step1534)), _b5p6od9s = __81fgg2dlsvn1534; __81fgg2count1534 != 0; __81fgg2count1534--, _b5p6od9s += (__81fgg2step1534)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark100:;
									// continue
								}
																}							}
						}
						
						{
							System.Int32 __81fgg2dlsvn1535 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step1535 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1535;
							for (__81fgg2count1535 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1535 + __81fgg2step1535) / __81fgg2step1535)), _68ec3gbh = __81fgg2dlsvn1535; __81fgg2count1535 != 0; __81fgg2count1535--, _68ec3gbh += (__81fgg2step1535)) {

							{
								
								if (*(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
								{
									
									_1ajfmh55 = (_r7cfteg3 * *(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)));
									{
										System.Int32 __81fgg2dlsvn1536 = (System.Int32)((int)1);
										const System.Int32 __81fgg2step1536 = (System.Int32)((int)1);
										System.Int32 __81fgg2count1536;
										for (__81fgg2count1536 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn1536 + __81fgg2step1536) / __81fgg2step1536)), _b5p6od9s = __81fgg2dlsvn1536; __81fgg2count1536 != 0; __81fgg2count1536--, _b5p6od9s += (__81fgg2step1536)) {

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
					System.Int32 __81fgg2dlsvn1537 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1537 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1537;
					for (__81fgg2count1537 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1537 + __81fgg2step1537) / __81fgg2step1537)), _znpjgsef = __81fgg2dlsvn1537; __81fgg2count1537 != 0; __81fgg2count1537--, _znpjgsef += (__81fgg2step1537)) {

					{
						
						if (_bafcbx97 == _d0547bi2)
						{
							
							{
								System.Int32 __81fgg2dlsvn1538 = (System.Int32)(_znpjgsef);
								const System.Int32 __81fgg2step1538 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1538;
								for (__81fgg2count1538 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1538 + __81fgg2step1538) / __81fgg2step1538)), _b5p6od9s = __81fgg2dlsvn1538; __81fgg2count1538 != 0; __81fgg2count1538--, _b5p6od9s += (__81fgg2step1538)) {

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
								System.Int32 __81fgg2dlsvn1539 = (System.Int32)(_znpjgsef);
								const System.Int32 __81fgg2step1539 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1539;
								for (__81fgg2count1539 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1539 + __81fgg2step1539) / __81fgg2step1539)), _b5p6od9s = __81fgg2dlsvn1539; __81fgg2count1539 != 0; __81fgg2count1539--, _b5p6od9s += (__81fgg2step1539)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark150:;
									// continue
								}
																}							}
						}
						
						{
							System.Int32 __81fgg2dlsvn1540 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step1540 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1540;
							for (__81fgg2count1540 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1540 + __81fgg2step1540) / __81fgg2step1540)), _68ec3gbh = __81fgg2dlsvn1540; __81fgg2count1540 != 0; __81fgg2count1540--, _68ec3gbh += (__81fgg2step1540)) {

							{
								
								if (*(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
								{
									
									_1ajfmh55 = (_r7cfteg3 * *(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)));
									{
										System.Int32 __81fgg2dlsvn1541 = (System.Int32)(_znpjgsef);
										const System.Int32 __81fgg2step1541 = (System.Int32)((int)1);
										System.Int32 __81fgg2count1541;
										for (__81fgg2count1541 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1541 + __81fgg2step1541) / __81fgg2step1541)), _b5p6od9s = __81fgg2dlsvn1541; __81fgg2count1541 != 0; __81fgg2count1541--, _b5p6od9s += (__81fgg2step1541)) {

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
					System.Int32 __81fgg2dlsvn1542 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1542 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1542;
					for (__81fgg2count1542 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1542 + __81fgg2step1542) / __81fgg2step1542)), _znpjgsef = __81fgg2dlsvn1542; __81fgg2count1542 != 0; __81fgg2count1542--, _znpjgsef += (__81fgg2step1542)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn1543 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step1543 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1543;
							for (__81fgg2count1543 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn1543 + __81fgg2step1543) / __81fgg2step1543)), _b5p6od9s = __81fgg2dlsvn1543; __81fgg2count1543 != 0; __81fgg2count1543--, _b5p6od9s += (__81fgg2step1543)) {

							{
								
								_1ajfmh55 = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn1544 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1544 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1544;
									for (__81fgg2count1544 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1544 + __81fgg2step1544) / __81fgg2step1544)), _68ec3gbh = __81fgg2dlsvn1544; __81fgg2count1544 != 0; __81fgg2count1544--, _68ec3gbh += (__81fgg2step1544)) {

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
					System.Int32 __81fgg2dlsvn1545 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1545 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1545;
					for (__81fgg2count1545 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1545 + __81fgg2step1545) / __81fgg2step1545)), _znpjgsef = __81fgg2dlsvn1545; __81fgg2count1545 != 0; __81fgg2count1545--, _znpjgsef += (__81fgg2step1545)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn1546 = (System.Int32)(_znpjgsef);
							const System.Int32 __81fgg2step1546 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1546;
							for (__81fgg2count1546 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1546 + __81fgg2step1546) / __81fgg2step1546)), _b5p6od9s = __81fgg2dlsvn1546; __81fgg2count1546 != 0; __81fgg2count1546--, _b5p6od9s += (__81fgg2step1546)) {

							{
								
								_1ajfmh55 = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn1547 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1547 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1547;
									for (__81fgg2count1547 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1547 + __81fgg2step1547) / __81fgg2step1547)), _68ec3gbh = __81fgg2dlsvn1547; __81fgg2count1547 != 0; __81fgg2count1547--, _68ec3gbh += (__81fgg2step1547)) {

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
		//*     End of SSYRK . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
