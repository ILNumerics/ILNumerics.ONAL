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
//*> \brief \b SSYR2K 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SSYR2K(UPLO,TRANS,N,K,ALPHA,A,LDA,B,LDB,BETA,C,LDC) 
//* 
//*       .. Scalar Arguments .. 
//*       REAL ALPHA,BETA 
//*       INTEGER K,LDA,LDB,LDC,N 
//*       CHARACTER TRANS,UPLO 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL A(LDA,*),B(LDB,*),C(LDC,*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SSYR2K  performs one of the symmetric rank 2k operations 
//*> 
//*>    C := alpha*A*B**T + alpha*B*A**T + beta*C, 
//*> 
//*> or 
//*> 
//*>    C := alpha*A**T*B + alpha*B**T*A + beta*C, 
//*> 
//*> where  alpha and beta  are scalars, C is an  n by n  symmetric matrix 
//*> and  A and B  are  n by k  matrices  in the  first  case  and  k by n 
//*> matrices in the second case. 
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
//*>              TRANS = 'N' or 'n'   C := alpha*A*B**T + alpha*B*A**T + 
//*>                                        beta*C. 
//*> 
//*>              TRANS = 'T' or 't'   C := alpha*A**T*B + alpha*B**T*A + 
//*>                                        beta*C. 
//*> 
//*>              TRANS = 'C' or 'c'   C := alpha*A**T*B + alpha*B**T*A + 
//*>                                        beta*C. 
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
//*>           of  columns  of the  matrices  A and B,  and on  entry  with 
//*>           TRANS = 'T' or 't' or 'C' or 'c',  K  specifies  the  number 
//*>           of rows of the matrices  A and B.  K must be at least  zero. 
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
//*> \param[in] B 
//*> \verbatim 
//*>          B is REAL array, dimension ( LDB, kb ), where kb is 
//*>           k  when  TRANS = 'N' or 'n',  and is  n  otherwise. 
//*>           Before entry with  TRANS = 'N' or 'n',  the  leading  n by k 
//*>           part of the array  B  must contain the matrix  B,  otherwise 
//*>           the leading  k by n  part of the array  B  must contain  the 
//*>           matrix B. 
//*> \endverbatim 
//*> 
//*> \param[in] LDB 
//*> \verbatim 
//*>          LDB is INTEGER 
//*>           On entry, LDB specifies the first dimension of B as declared 
//*>           in  the  calling  (sub)  program.   When  TRANS = 'N' or 'n' 
//*>           then  LDB must be at least  max( 1, n ), otherwise  LDB must 
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
//*> 
//*>  -- Written on 8-February-1989. 
//*>     Jack Dongarra, Argonne National Laboratory. 
//*>     Iain Duff, AERE Harwell. 
//*>     Jeremy Du Croz, Numerical Algorithms Group Ltd. 
//*>     Sven Hammarling, Numerical Algorithms Group Ltd. 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _kk3tn6zp(FString _9wyre9zc, FString _scuo79v4, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, ref Single _r7cfteg3, Single* _vxfgpup9, ref Int32 _ocv8fk5c, Single* _p9n405a5, ref Int32 _ly9opahg, ref Single _bafcbx97, Single* _3crf0qn3, ref Int32 _1s3eymp4)
	{
#region variable declarations
Single _yc8h372p =  default;
Single _q3ig7mub =  default;
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
		if (_ly9opahg < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_o9a6qdux ))
		{
			
			_gro5yvfo = (int)9;
		}
		else
		if (_1s3eymp4 < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)12;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("SSYR2K" ,ref _gro5yvfo );
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
						System.Int32 __81fgg2dlsvn3458 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3458 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3458;
						for (__81fgg2count3458 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3458 + __81fgg2step3458) / __81fgg2step3458)), _znpjgsef = __81fgg2dlsvn3458; __81fgg2count3458 != 0; __81fgg2count3458--, _znpjgsef += (__81fgg2step3458)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn3459 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3459 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3459;
								for (__81fgg2count3459 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn3459 + __81fgg2step3459) / __81fgg2step3459)), _b5p6od9s = __81fgg2dlsvn3459; __81fgg2count3459 != 0; __81fgg2count3459--, _b5p6od9s += (__81fgg2step3459)) {

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
						System.Int32 __81fgg2dlsvn3460 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3460 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3460;
						for (__81fgg2count3460 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3460 + __81fgg2step3460) / __81fgg2step3460)), _znpjgsef = __81fgg2dlsvn3460; __81fgg2count3460 != 0; __81fgg2count3460--, _znpjgsef += (__81fgg2step3460)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn3461 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3461 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3461;
								for (__81fgg2count3461 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn3461 + __81fgg2step3461) / __81fgg2step3461)), _b5p6od9s = __81fgg2dlsvn3461; __81fgg2count3461 != 0; __81fgg2count3461--, _b5p6od9s += (__81fgg2step3461)) {

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
						System.Int32 __81fgg2dlsvn3462 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3462 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3462;
						for (__81fgg2count3462 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3462 + __81fgg2step3462) / __81fgg2step3462)), _znpjgsef = __81fgg2dlsvn3462; __81fgg2count3462 != 0; __81fgg2count3462--, _znpjgsef += (__81fgg2step3462)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn3463 = (System.Int32)(_znpjgsef);
								const System.Int32 __81fgg2step3463 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3463;
								for (__81fgg2count3463 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3463 + __81fgg2step3463) / __81fgg2step3463)), _b5p6od9s = __81fgg2dlsvn3463; __81fgg2count3463 != 0; __81fgg2count3463--, _b5p6od9s += (__81fgg2step3463)) {

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
						System.Int32 __81fgg2dlsvn3464 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3464 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3464;
						for (__81fgg2count3464 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3464 + __81fgg2step3464) / __81fgg2step3464)), _znpjgsef = __81fgg2dlsvn3464; __81fgg2count3464 != 0; __81fgg2count3464--, _znpjgsef += (__81fgg2step3464)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn3465 = (System.Int32)(_znpjgsef);
								const System.Int32 __81fgg2step3465 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3465;
								for (__81fgg2count3465 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3465 + __81fgg2step3465) / __81fgg2step3465)), _b5p6od9s = __81fgg2dlsvn3465; __81fgg2count3465 != 0; __81fgg2count3465--, _b5p6od9s += (__81fgg2step3465)) {

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
			//*        Form  C := alpha*A*B**T + alpha*B*A**T + C. 
			//* 
			
			if (_l08igmvf)
			{
				
				{
					System.Int32 __81fgg2dlsvn3466 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3466 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3466;
					for (__81fgg2count3466 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3466 + __81fgg2step3466) / __81fgg2step3466)), _znpjgsef = __81fgg2dlsvn3466; __81fgg2count3466 != 0; __81fgg2count3466--, _znpjgsef += (__81fgg2step3466)) {

					{
						
						if (_bafcbx97 == _d0547bi2)
						{
							
							{
								System.Int32 __81fgg2dlsvn3467 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3467 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3467;
								for (__81fgg2count3467 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn3467 + __81fgg2step3467) / __81fgg2step3467)), _b5p6od9s = __81fgg2dlsvn3467; __81fgg2count3467 != 0; __81fgg2count3467--, _b5p6od9s += (__81fgg2step3467)) {

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
								System.Int32 __81fgg2dlsvn3468 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3468 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3468;
								for (__81fgg2count3468 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn3468 + __81fgg2step3468) / __81fgg2step3468)), _b5p6od9s = __81fgg2dlsvn3468; __81fgg2count3468 != 0; __81fgg2count3468--, _b5p6od9s += (__81fgg2step3468)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark100:;
									// continue
								}
																}							}
						}
						
						{
							System.Int32 __81fgg2dlsvn3469 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3469 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3469;
							for (__81fgg2count3469 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn3469 + __81fgg2step3469) / __81fgg2step3469)), _68ec3gbh = __81fgg2dlsvn3469; __81fgg2count3469 != 0; __81fgg2count3469--, _68ec3gbh += (__81fgg2step3469)) {

							{
								
								if ((*(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) != _d0547bi2) | (*(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)) != _d0547bi2))
								{
									
									_yc8h372p = (_r7cfteg3 * *(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)));
									_q3ig7mub = (_r7cfteg3 * *(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)));
									{
										System.Int32 __81fgg2dlsvn3470 = (System.Int32)((int)1);
										const System.Int32 __81fgg2step3470 = (System.Int32)((int)1);
										System.Int32 __81fgg2count3470;
										for (__81fgg2count3470 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn3470 + __81fgg2step3470) / __81fgg2step3470)), _b5p6od9s = __81fgg2dlsvn3470; __81fgg2count3470 != 0; __81fgg2count3470--, _b5p6od9s += (__81fgg2step3470)) {

										{
											
											*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = ((*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) + (*(_vxfgpup9+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) * _yc8h372p)) + (*(_p9n405a5+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)) * _q3ig7mub));
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
					System.Int32 __81fgg2dlsvn3471 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3471 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3471;
					for (__81fgg2count3471 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3471 + __81fgg2step3471) / __81fgg2step3471)), _znpjgsef = __81fgg2dlsvn3471; __81fgg2count3471 != 0; __81fgg2count3471--, _znpjgsef += (__81fgg2step3471)) {

					{
						
						if (_bafcbx97 == _d0547bi2)
						{
							
							{
								System.Int32 __81fgg2dlsvn3472 = (System.Int32)(_znpjgsef);
								const System.Int32 __81fgg2step3472 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3472;
								for (__81fgg2count3472 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3472 + __81fgg2step3472) / __81fgg2step3472)), _b5p6od9s = __81fgg2dlsvn3472; __81fgg2count3472 != 0; __81fgg2count3472--, _b5p6od9s += (__81fgg2step3472)) {

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
								System.Int32 __81fgg2dlsvn3473 = (System.Int32)(_znpjgsef);
								const System.Int32 __81fgg2step3473 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3473;
								for (__81fgg2count3473 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3473 + __81fgg2step3473) / __81fgg2step3473)), _b5p6od9s = __81fgg2dlsvn3473; __81fgg2count3473 != 0; __81fgg2count3473--, _b5p6od9s += (__81fgg2step3473)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark150:;
									// continue
								}
																}							}
						}
						
						{
							System.Int32 __81fgg2dlsvn3474 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3474 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3474;
							for (__81fgg2count3474 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn3474 + __81fgg2step3474) / __81fgg2step3474)), _68ec3gbh = __81fgg2dlsvn3474; __81fgg2count3474 != 0; __81fgg2count3474--, _68ec3gbh += (__81fgg2step3474)) {

							{
								
								if ((*(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) != _d0547bi2) | (*(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)) != _d0547bi2))
								{
									
									_yc8h372p = (_r7cfteg3 * *(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)));
									_q3ig7mub = (_r7cfteg3 * *(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)));
									{
										System.Int32 __81fgg2dlsvn3475 = (System.Int32)(_znpjgsef);
										const System.Int32 __81fgg2step3475 = (System.Int32)((int)1);
										System.Int32 __81fgg2count3475;
										for (__81fgg2count3475 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3475 + __81fgg2step3475) / __81fgg2step3475)), _b5p6od9s = __81fgg2dlsvn3475; __81fgg2count3475 != 0; __81fgg2count3475--, _b5p6od9s += (__81fgg2step3475)) {

										{
											
											*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = ((*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) + (*(_vxfgpup9+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) * _yc8h372p)) + (*(_p9n405a5+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)) * _q3ig7mub));
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
			//*        Form  C := alpha*A**T*B + alpha*B**T*A + C. 
			//* 
			
			if (_l08igmvf)
			{
				
				{
					System.Int32 __81fgg2dlsvn3476 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3476 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3476;
					for (__81fgg2count3476 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3476 + __81fgg2step3476) / __81fgg2step3476)), _znpjgsef = __81fgg2dlsvn3476; __81fgg2count3476 != 0; __81fgg2count3476--, _znpjgsef += (__81fgg2step3476)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn3477 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3477 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3477;
							for (__81fgg2count3477 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn3477 + __81fgg2step3477) / __81fgg2step3477)), _b5p6od9s = __81fgg2dlsvn3477; __81fgg2count3477 != 0; __81fgg2count3477--, _b5p6od9s += (__81fgg2step3477)) {

							{
								
								_yc8h372p = _d0547bi2;
								_q3ig7mub = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn3478 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step3478 = (System.Int32)((int)1);
									System.Int32 __81fgg2count3478;
									for (__81fgg2count3478 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn3478 + __81fgg2step3478) / __81fgg2step3478)), _68ec3gbh = __81fgg2dlsvn3478; __81fgg2count3478 != 0; __81fgg2count3478--, _68ec3gbh += (__81fgg2step3478)) {

									{
										
										_yc8h372p = (_yc8h372p + (*(_vxfgpup9+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) * *(_p9n405a5+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
										_q3ig7mub = (_q3ig7mub + (*(_p9n405a5+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ly9opahg)) * *(_vxfgpup9+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
Mark190:;
										// continue
									}
																		}								}
								if (_bafcbx97 == _d0547bi2)
								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = ((_r7cfteg3 * _yc8h372p) + (_r7cfteg3 * _q3ig7mub));
								}
								else
								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (((_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))) + (_r7cfteg3 * _yc8h372p)) + (_r7cfteg3 * _q3ig7mub));
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
					System.Int32 __81fgg2dlsvn3479 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3479 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3479;
					for (__81fgg2count3479 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3479 + __81fgg2step3479) / __81fgg2step3479)), _znpjgsef = __81fgg2dlsvn3479; __81fgg2count3479 != 0; __81fgg2count3479--, _znpjgsef += (__81fgg2step3479)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn3480 = (System.Int32)(_znpjgsef);
							const System.Int32 __81fgg2step3480 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3480;
							for (__81fgg2count3480 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3480 + __81fgg2step3480) / __81fgg2step3480)), _b5p6od9s = __81fgg2dlsvn3480; __81fgg2count3480 != 0; __81fgg2count3480--, _b5p6od9s += (__81fgg2step3480)) {

							{
								
								_yc8h372p = _d0547bi2;
								_q3ig7mub = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn3481 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step3481 = (System.Int32)((int)1);
									System.Int32 __81fgg2count3481;
									for (__81fgg2count3481 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn3481 + __81fgg2step3481) / __81fgg2step3481)), _68ec3gbh = __81fgg2dlsvn3481; __81fgg2count3481 != 0; __81fgg2count3481--, _68ec3gbh += (__81fgg2step3481)) {

									{
										
										_yc8h372p = (_yc8h372p + (*(_vxfgpup9+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) * *(_p9n405a5+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
										_q3ig7mub = (_q3ig7mub + (*(_p9n405a5+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ly9opahg)) * *(_vxfgpup9+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
Mark220:;
										// continue
									}
																		}								}
								if (_bafcbx97 == _d0547bi2)
								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = ((_r7cfteg3 * _yc8h372p) + (_r7cfteg3 * _q3ig7mub));
								}
								else
								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (((_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))) + (_r7cfteg3 * _yc8h372p)) + (_r7cfteg3 * _q3ig7mub));
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
		//*     End of SSYR2K. 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
