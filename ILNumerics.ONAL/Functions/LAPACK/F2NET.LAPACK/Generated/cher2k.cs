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
//*> \brief \b CHER2K 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CHER2K(UPLO,TRANS,N,K,ALPHA,A,LDA,B,LDB,BETA,C,LDC) 
//* 
//*       .. Scalar Arguments .. 
//*       COMPLEX ALPHA 
//*       REAL BETA 
//*       INTEGER K,LDA,LDB,LDC,N 
//*       CHARACTER TRANS,UPLO 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX A(LDA,*),B(LDB,*),C(LDC,*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CHER2K  performs one of the hermitian rank 2k operations 
//*> 
//*>    C := alpha*A*B**H + conjg( alpha )*B*A**H + beta*C, 
//*> 
//*> or 
//*> 
//*>    C := alpha*A**H*B + conjg( alpha )*B**H*A + beta*C, 
//*> 
//*> where  alpha and beta  are scalars with  beta  real,  C is an  n by n 
//*> hermitian matrix and  A and B  are  n by k matrices in the first case 
//*> and  k by n  matrices in the second case. 
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
//*>              TRANS = 'N' or 'n'    C := alpha*A*B**H          + 
//*>                                         conjg( alpha )*B*A**H + 
//*>                                         beta*C. 
//*> 
//*>              TRANS = 'C' or 'c'    C := alpha*A**H*B          + 
//*>                                         conjg( alpha )*B**H*A + 
//*>                                         beta*C. 
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
//*>           TRANS = 'C' or 'c',  K  specifies  the number of rows of the 
//*>           matrices  A and B.  K must be at least zero. 
//*> \endverbatim 
//*> 
//*> \param[in] ALPHA 
//*> \verbatim 
//*>          ALPHA is COMPLEX 
//*>           On entry, ALPHA specifies the scalar alpha. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is COMPLEX array, dimension ( LDA, ka ), where ka is 
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
//*>          B is COMPLEX array, dimension ( LDB, kb ), where kb is 
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
//*>          C is COMPLEX array, dimension ( LDC, N ) 
//*>           Before entry  with  UPLO = 'U' or 'u',  the leading  n by n 
//*>           upper triangular part of the array C must contain the upper 
//*>           triangular part  of the  hermitian matrix  and the strictly 
//*>           lower triangular part of C is not referenced.  On exit, the 
//*>           upper triangular part of the array  C is overwritten by the 
//*>           upper triangular part of the updated matrix. 
//*>           Before entry  with  UPLO = 'L' or 'l',  the leading  n by n 
//*>           lower triangular part of the array C must contain the lower 
//*>           triangular part  of the  hermitian matrix  and the strictly 
//*>           upper triangular part of C is not referenced.  On exit, the 
//*>           lower triangular part of the array  C is overwritten by the 
//*>           lower triangular part of the updated matrix. 
//*>           Note that the imaginary parts of the diagonal elements need 
//*>           not be set,  they are assumed to be zero,  and on exit they 
//*>           are set to zero. 
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
//*> \ingroup complex_blas_level3 
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
//*> 
//*>  -- Modified 8-Nov-93 to set C(J,J) to REAL( C(J,J) ) when BETA = 1. 
//*>     Ed Anderson, Cray Research Inc. 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _qpixscj7(FString _9wyre9zc, FString _scuo79v4, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, ref fcomplex _r7cfteg3, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, fcomplex* _p9n405a5, ref Int32 _ly9opahg, ref Single _bafcbx97, fcomplex* _3crf0qn3, ref Int32 _1s3eymp4)
	{
#region variable declarations
fcomplex _yc8h372p =  default;
fcomplex _q3ig7mub =  default;
Int32 _b5p6od9s =  default;
Int32 _gro5yvfo =  default;
Int32 _znpjgsef =  default;
Int32 _68ec3gbh =  default;
Int32 _o9a6qdux =  default;
Boolean _l08igmvf =  default;
Single _kxg5drh2 =  1f;
fcomplex _d0547bi2 =   new fcomplex(0f,0f);
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
		if ((!(_w8y2rzgy(_scuo79v4 ,"N" ))) & (!(_w8y2rzgy(_scuo79v4 ,"C" ))))
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
			
			_ut9qalzx("CHER2K" ,ref _gro5yvfo );
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
				
				if (_bafcbx97 == ILNumerics.F2NET.Intrinsics.REAL(_d0547bi2 ))
				{
					
					{
						System.Int32 __81fgg2dlsvn3512 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3512 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3512;
						for (__81fgg2count3512 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3512 + __81fgg2step3512) / __81fgg2step3512)), _znpjgsef = __81fgg2dlsvn3512; __81fgg2count3512 != 0; __81fgg2count3512--, _znpjgsef += (__81fgg2step3512)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn3513 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3513 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3513;
								for (__81fgg2count3513 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn3513 + __81fgg2step3513) / __81fgg2step3513)), _b5p6od9s = __81fgg2dlsvn3513; __81fgg2count3513 != 0; __81fgg2count3513--, _b5p6od9s += (__81fgg2step3513)) {

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
						System.Int32 __81fgg2dlsvn3514 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3514 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3514;
						for (__81fgg2count3514 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3514 + __81fgg2step3514) / __81fgg2step3514)), _znpjgsef = __81fgg2dlsvn3514; __81fgg2count3514 != 0; __81fgg2count3514--, _znpjgsef += (__81fgg2step3514)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn3515 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3515 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3515;
								for (__81fgg2count3515 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn3515 + __81fgg2step3515) / __81fgg2step3515)), _b5p6od9s = __81fgg2dlsvn3515; __81fgg2count3515 != 0; __81fgg2count3515--, _b5p6od9s += (__81fgg2step3515)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark30:;
									// continue
								}
																}							}
							*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = CMPLX((_bafcbx97 * ILNumerics.F2NET.Intrinsics.REAL(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) )));
Mark40:;
							// continue
						}
												}					}
				}
				
			}
			else
			{
				
				if (_bafcbx97 == ILNumerics.F2NET.Intrinsics.REAL(_d0547bi2 ))
				{
					
					{
						System.Int32 __81fgg2dlsvn3516 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3516 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3516;
						for (__81fgg2count3516 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3516 + __81fgg2step3516) / __81fgg2step3516)), _znpjgsef = __81fgg2dlsvn3516; __81fgg2count3516 != 0; __81fgg2count3516--, _znpjgsef += (__81fgg2step3516)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn3517 = (System.Int32)(_znpjgsef);
								const System.Int32 __81fgg2step3517 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3517;
								for (__81fgg2count3517 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3517 + __81fgg2step3517) / __81fgg2step3517)), _b5p6od9s = __81fgg2dlsvn3517; __81fgg2count3517 != 0; __81fgg2count3517--, _b5p6od9s += (__81fgg2step3517)) {

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
						System.Int32 __81fgg2dlsvn3518 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3518 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3518;
						for (__81fgg2count3518 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3518 + __81fgg2step3518) / __81fgg2step3518)), _znpjgsef = __81fgg2dlsvn3518; __81fgg2count3518 != 0; __81fgg2count3518--, _znpjgsef += (__81fgg2step3518)) {

						{
							
							*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = CMPLX((_bafcbx97 * ILNumerics.F2NET.Intrinsics.REAL(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) )));
							{
								System.Int32 __81fgg2dlsvn3519 = (System.Int32)((_znpjgsef + (int)1));
								const System.Int32 __81fgg2step3519 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3519;
								for (__81fgg2count3519 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3519 + __81fgg2step3519) / __81fgg2step3519)), _b5p6od9s = __81fgg2dlsvn3519; __81fgg2count3519 != 0; __81fgg2count3519--, _b5p6od9s += (__81fgg2step3519)) {

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
			//*        Form  C := alpha*A*B**H + conjg( alpha )*B*A**H + 
			//*                   C. 
			//* 
			
			if (_l08igmvf)
			{
				
				{
					System.Int32 __81fgg2dlsvn3520 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3520 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3520;
					for (__81fgg2count3520 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3520 + __81fgg2step3520) / __81fgg2step3520)), _znpjgsef = __81fgg2dlsvn3520; __81fgg2count3520 != 0; __81fgg2count3520--, _znpjgsef += (__81fgg2step3520)) {

					{
						
						if (_bafcbx97 == ILNumerics.F2NET.Intrinsics.REAL(_d0547bi2 ))
						{
							
							{
								System.Int32 __81fgg2dlsvn3521 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3521 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3521;
								for (__81fgg2count3521 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn3521 + __81fgg2step3521) / __81fgg2step3521)), _b5p6od9s = __81fgg2dlsvn3521; __81fgg2count3521 != 0; __81fgg2count3521--, _b5p6od9s += (__81fgg2step3521)) {

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
								System.Int32 __81fgg2dlsvn3522 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3522 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3522;
								for (__81fgg2count3522 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn3522 + __81fgg2step3522) / __81fgg2step3522)), _b5p6od9s = __81fgg2dlsvn3522; __81fgg2count3522 != 0; __81fgg2count3522--, _b5p6od9s += (__81fgg2step3522)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark100:;
									// continue
								}
																}							}
							*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = CMPLX((_bafcbx97 * ILNumerics.F2NET.Intrinsics.REAL(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) )));
						}
						else
						{
							
							*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = CMPLX(ILNumerics.F2NET.Intrinsics.REAL(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) ));
						}
						
						{
							System.Int32 __81fgg2dlsvn3523 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3523 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3523;
							for (__81fgg2count3523 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn3523 + __81fgg2step3523) / __81fgg2step3523)), _68ec3gbh = __81fgg2dlsvn3523; __81fgg2count3523 != 0; __81fgg2count3523--, _68ec3gbh += (__81fgg2step3523)) {

							{
								
								if ((*(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) != _d0547bi2) | (*(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)) != _d0547bi2))
								{
									
									_yc8h372p = (_r7cfteg3 * ILNumerics.F2NET.Intrinsics.CONJG(*(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)) ));
									_q3ig7mub = ILNumerics.F2NET.Intrinsics.CONJG(_r7cfteg3 * *(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) );
									{
										System.Int32 __81fgg2dlsvn3524 = (System.Int32)((int)1);
										const System.Int32 __81fgg2step3524 = (System.Int32)((int)1);
										System.Int32 __81fgg2count3524;
										for (__81fgg2count3524 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn3524 + __81fgg2step3524) / __81fgg2step3524)), _b5p6od9s = __81fgg2dlsvn3524; __81fgg2count3524 != 0; __81fgg2count3524--, _b5p6od9s += (__81fgg2step3524)) {

										{
											
											*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = ((*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) + (*(_vxfgpup9+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) * _yc8h372p)) + (*(_p9n405a5+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)) * _q3ig7mub));
Mark110:;
											// continue
										}
																				}									}
									*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = CMPLX((ILNumerics.F2NET.Intrinsics.REAL(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) ) + ILNumerics.F2NET.Intrinsics.REAL((*(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) * _yc8h372p) + (*(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)) * _q3ig7mub) )));
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
					System.Int32 __81fgg2dlsvn3525 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3525 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3525;
					for (__81fgg2count3525 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3525 + __81fgg2step3525) / __81fgg2step3525)), _znpjgsef = __81fgg2dlsvn3525; __81fgg2count3525 != 0; __81fgg2count3525--, _znpjgsef += (__81fgg2step3525)) {

					{
						
						if (_bafcbx97 == ILNumerics.F2NET.Intrinsics.REAL(_d0547bi2 ))
						{
							
							{
								System.Int32 __81fgg2dlsvn3526 = (System.Int32)(_znpjgsef);
								const System.Int32 __81fgg2step3526 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3526;
								for (__81fgg2count3526 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3526 + __81fgg2step3526) / __81fgg2step3526)), _b5p6od9s = __81fgg2dlsvn3526; __81fgg2count3526 != 0; __81fgg2count3526--, _b5p6od9s += (__81fgg2step3526)) {

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
								System.Int32 __81fgg2dlsvn3527 = (System.Int32)((_znpjgsef + (int)1));
								const System.Int32 __81fgg2step3527 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3527;
								for (__81fgg2count3527 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3527 + __81fgg2step3527) / __81fgg2step3527)), _b5p6od9s = __81fgg2dlsvn3527; __81fgg2count3527 != 0; __81fgg2count3527--, _b5p6od9s += (__81fgg2step3527)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark150:;
									// continue
								}
																}							}
							*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = CMPLX((_bafcbx97 * ILNumerics.F2NET.Intrinsics.REAL(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) )));
						}
						else
						{
							
							*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = CMPLX(ILNumerics.F2NET.Intrinsics.REAL(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) ));
						}
						
						{
							System.Int32 __81fgg2dlsvn3528 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3528 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3528;
							for (__81fgg2count3528 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn3528 + __81fgg2step3528) / __81fgg2step3528)), _68ec3gbh = __81fgg2dlsvn3528; __81fgg2count3528 != 0; __81fgg2count3528--, _68ec3gbh += (__81fgg2step3528)) {

							{
								
								if ((*(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) != _d0547bi2) | (*(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)) != _d0547bi2))
								{
									
									_yc8h372p = (_r7cfteg3 * ILNumerics.F2NET.Intrinsics.CONJG(*(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)) ));
									_q3ig7mub = ILNumerics.F2NET.Intrinsics.CONJG(_r7cfteg3 * *(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) );
									{
										System.Int32 __81fgg2dlsvn3529 = (System.Int32)((_znpjgsef + (int)1));
										const System.Int32 __81fgg2step3529 = (System.Int32)((int)1);
										System.Int32 __81fgg2count3529;
										for (__81fgg2count3529 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3529 + __81fgg2step3529) / __81fgg2step3529)), _b5p6od9s = __81fgg2dlsvn3529; __81fgg2count3529 != 0; __81fgg2count3529--, _b5p6od9s += (__81fgg2step3529)) {

										{
											
											*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = ((*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) + (*(_vxfgpup9+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) * _yc8h372p)) + (*(_p9n405a5+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)) * _q3ig7mub));
Mark160:;
											// continue
										}
																				}									}
									*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = CMPLX((ILNumerics.F2NET.Intrinsics.REAL(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) ) + ILNumerics.F2NET.Intrinsics.REAL((*(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) * _yc8h372p) + (*(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)) * _q3ig7mub) )));
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
			//*        Form  C := alpha*A**H*B + conjg( alpha )*B**H*A + 
			//*                   C. 
			//* 
			
			if (_l08igmvf)
			{
				
				{
					System.Int32 __81fgg2dlsvn3530 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3530 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3530;
					for (__81fgg2count3530 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3530 + __81fgg2step3530) / __81fgg2step3530)), _znpjgsef = __81fgg2dlsvn3530; __81fgg2count3530 != 0; __81fgg2count3530--, _znpjgsef += (__81fgg2step3530)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn3531 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3531 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3531;
							for (__81fgg2count3531 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn3531 + __81fgg2step3531) / __81fgg2step3531)), _b5p6od9s = __81fgg2dlsvn3531; __81fgg2count3531 != 0; __81fgg2count3531--, _b5p6od9s += (__81fgg2step3531)) {

							{
								
								_yc8h372p = _d0547bi2;
								_q3ig7mub = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn3532 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step3532 = (System.Int32)((int)1);
									System.Int32 __81fgg2count3532;
									for (__81fgg2count3532 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn3532 + __81fgg2step3532) / __81fgg2step3532)), _68ec3gbh = __81fgg2dlsvn3532; __81fgg2count3532 != 0; __81fgg2count3532--, _68ec3gbh += (__81fgg2step3532)) {

									{
										
										_yc8h372p = (_yc8h372p + (ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ) * *(_p9n405a5+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
										_q3ig7mub = (_q3ig7mub + (ILNumerics.F2NET.Intrinsics.CONJG(*(_p9n405a5+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ly9opahg)) ) * *(_vxfgpup9+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
Mark190:;
										// continue
									}
																		}								}
								if (_b5p6od9s == _znpjgsef)
								{
									
									if (_bafcbx97 == ILNumerics.F2NET.Intrinsics.REAL(_d0547bi2 ))
									{
										
										*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = CMPLX(ILNumerics.F2NET.Intrinsics.REAL((_r7cfteg3 * _yc8h372p) + (ILNumerics.F2NET.Intrinsics.CONJG(_r7cfteg3 ) * _q3ig7mub) ));
									}
									else
									{
										
										*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = CMPLX(((_bafcbx97 * ILNumerics.F2NET.Intrinsics.REAL(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) )) + ILNumerics.F2NET.Intrinsics.REAL((_r7cfteg3 * _yc8h372p) + (ILNumerics.F2NET.Intrinsics.CONJG(_r7cfteg3 ) * _q3ig7mub) )));
									}
									
								}
								else
								{
									
									if (_bafcbx97 == ILNumerics.F2NET.Intrinsics.REAL(_d0547bi2 ))
									{
										
										*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = ((_r7cfteg3 * _yc8h372p) + (ILNumerics.F2NET.Intrinsics.CONJG(_r7cfteg3 ) * _q3ig7mub));
									}
									else
									{
										
										*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (((_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))) + (_r7cfteg3 * _yc8h372p)) + (ILNumerics.F2NET.Intrinsics.CONJG(_r7cfteg3 ) * _q3ig7mub));
									}
									
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
					System.Int32 __81fgg2dlsvn3533 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3533 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3533;
					for (__81fgg2count3533 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3533 + __81fgg2step3533) / __81fgg2step3533)), _znpjgsef = __81fgg2dlsvn3533; __81fgg2count3533 != 0; __81fgg2count3533--, _znpjgsef += (__81fgg2step3533)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn3534 = (System.Int32)(_znpjgsef);
							const System.Int32 __81fgg2step3534 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3534;
							for (__81fgg2count3534 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3534 + __81fgg2step3534) / __81fgg2step3534)), _b5p6od9s = __81fgg2dlsvn3534; __81fgg2count3534 != 0; __81fgg2count3534--, _b5p6od9s += (__81fgg2step3534)) {

							{
								
								_yc8h372p = _d0547bi2;
								_q3ig7mub = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn3535 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step3535 = (System.Int32)((int)1);
									System.Int32 __81fgg2count3535;
									for (__81fgg2count3535 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn3535 + __81fgg2step3535) / __81fgg2step3535)), _68ec3gbh = __81fgg2dlsvn3535; __81fgg2count3535 != 0; __81fgg2count3535--, _68ec3gbh += (__81fgg2step3535)) {

									{
										
										_yc8h372p = (_yc8h372p + (ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ) * *(_p9n405a5+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
										_q3ig7mub = (_q3ig7mub + (ILNumerics.F2NET.Intrinsics.CONJG(*(_p9n405a5+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ly9opahg)) ) * *(_vxfgpup9+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
Mark220:;
										// continue
									}
																		}								}
								if (_b5p6od9s == _znpjgsef)
								{
									
									if (_bafcbx97 == ILNumerics.F2NET.Intrinsics.REAL(_d0547bi2 ))
									{
										
										*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = CMPLX(ILNumerics.F2NET.Intrinsics.REAL((_r7cfteg3 * _yc8h372p) + (ILNumerics.F2NET.Intrinsics.CONJG(_r7cfteg3 ) * _q3ig7mub) ));
									}
									else
									{
										
										*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = CMPLX(((_bafcbx97 * ILNumerics.F2NET.Intrinsics.REAL(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) )) + ILNumerics.F2NET.Intrinsics.REAL((_r7cfteg3 * _yc8h372p) + (ILNumerics.F2NET.Intrinsics.CONJG(_r7cfteg3 ) * _q3ig7mub) )));
									}
									
								}
								else
								{
									
									if (_bafcbx97 == ILNumerics.F2NET.Intrinsics.REAL(_d0547bi2 ))
									{
										
										*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = ((_r7cfteg3 * _yc8h372p) + (ILNumerics.F2NET.Intrinsics.CONJG(_r7cfteg3 ) * _q3ig7mub));
									}
									else
									{
										
										*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (((_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))) + (_r7cfteg3 * _yc8h372p)) + (ILNumerics.F2NET.Intrinsics.CONJG(_r7cfteg3 ) * _q3ig7mub));
									}
									
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
		//*     End of CHER2K. 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
