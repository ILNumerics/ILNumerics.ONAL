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
//*> \brief \b ZHER2K 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZHER2K(UPLO,TRANS,N,K,ALPHA,A,LDA,B,LDB,BETA,C,LDC) 
//* 
//*       .. Scalar Arguments .. 
//*       COMPLEX*16 ALPHA 
//*       DOUBLE PRECISION BETA 
//*       INTEGER K,LDA,LDB,LDC,N 
//*       CHARACTER TRANS,UPLO 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX*16 A(LDA,*),B(LDB,*),C(LDC,*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZHER2K  performs one of the hermitian rank 2k operations 
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
//*>          ALPHA is COMPLEX*16 . 
//*>           On entry, ALPHA specifies the scalar alpha. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is COMPLEX*16 array, dimension ( LDA, ka ), where ka is 
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
//*>          B is COMPLEX*16 array, dimension ( LDB, kb ), where kb is 
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
//*>           Unchanged on exit. 
//*> \endverbatim 
//*> 
//*> \param[in] BETA 
//*> \verbatim 
//*>          BETA is DOUBLE PRECISION . 
//*>           On entry, BETA specifies the scalar beta. 
//*> \endverbatim 
//*> 
//*> \param[in,out] C 
//*> \verbatim 
//*>          C is COMPLEX*16 array, dimension ( LDC, N ) 
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
//*> \ingroup complex16_blas_level3 
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
//*>  -- Modified 8-Nov-93 to set C(J,J) to DBLE( C(J,J) ) when BETA = 1. 
//*>     Ed Anderson, Cray Research Inc. 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _y1qfl221(FString _9wyre9zc, FString _scuo79v4, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, ref complex _r7cfteg3, complex* _vxfgpup9, ref Int32 _ocv8fk5c, complex* _p9n405a5, ref Int32 _ly9opahg, ref Double _bafcbx97, complex* _3crf0qn3, ref Int32 _1s3eymp4)
	{
#region variable declarations
complex _yc8h372p =  default;
complex _q3ig7mub =  default;
Int32 _b5p6od9s =  default;
Int32 _gro5yvfo =  default;
Int32 _znpjgsef =  default;
Int32 _68ec3gbh =  default;
Int32 _o9a6qdux =  default;
Boolean _l08igmvf =  default;
Double _kxg5drh2 =  1d;
complex _d0547bi2 =   new fcomplex(0f,0f);
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
			
			_ut9qalzx("ZHER2K" ,ref _gro5yvfo );
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
				
				if (_bafcbx97 == ILNumerics.F2NET.Intrinsics.DBLE(_d0547bi2 ))
				{
					
					{
						System.Int32 __81fgg2dlsvn3624 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3624 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3624;
						for (__81fgg2count3624 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3624 + __81fgg2step3624) / __81fgg2step3624)), _znpjgsef = __81fgg2dlsvn3624; __81fgg2count3624 != 0; __81fgg2count3624--, _znpjgsef += (__81fgg2step3624)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn3625 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3625 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3625;
								for (__81fgg2count3625 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn3625 + __81fgg2step3625) / __81fgg2step3625)), _b5p6od9s = __81fgg2dlsvn3625; __81fgg2count3625 != 0; __81fgg2count3625--, _b5p6od9s += (__81fgg2step3625)) {

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
						System.Int32 __81fgg2dlsvn3626 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3626 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3626;
						for (__81fgg2count3626 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3626 + __81fgg2step3626) / __81fgg2step3626)), _znpjgsef = __81fgg2dlsvn3626; __81fgg2count3626 != 0; __81fgg2count3626--, _znpjgsef += (__81fgg2step3626)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn3627 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3627 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3627;
								for (__81fgg2count3627 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn3627 + __81fgg2step3627) / __81fgg2step3627)), _b5p6od9s = __81fgg2dlsvn3627; __81fgg2count3627 != 0; __81fgg2count3627--, _b5p6od9s += (__81fgg2step3627)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark30:;
									// continue
								}
																}							}
							*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = DCMPLX((_bafcbx97 * ILNumerics.F2NET.Intrinsics.DBLE(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) )));
Mark40:;
							// continue
						}
												}					}
				}
				
			}
			else
			{
				
				if (_bafcbx97 == ILNumerics.F2NET.Intrinsics.DBLE(_d0547bi2 ))
				{
					
					{
						System.Int32 __81fgg2dlsvn3628 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3628 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3628;
						for (__81fgg2count3628 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3628 + __81fgg2step3628) / __81fgg2step3628)), _znpjgsef = __81fgg2dlsvn3628; __81fgg2count3628 != 0; __81fgg2count3628--, _znpjgsef += (__81fgg2step3628)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn3629 = (System.Int32)(_znpjgsef);
								const System.Int32 __81fgg2step3629 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3629;
								for (__81fgg2count3629 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3629 + __81fgg2step3629) / __81fgg2step3629)), _b5p6od9s = __81fgg2dlsvn3629; __81fgg2count3629 != 0; __81fgg2count3629--, _b5p6od9s += (__81fgg2step3629)) {

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
						System.Int32 __81fgg2dlsvn3630 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3630 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3630;
						for (__81fgg2count3630 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3630 + __81fgg2step3630) / __81fgg2step3630)), _znpjgsef = __81fgg2dlsvn3630; __81fgg2count3630 != 0; __81fgg2count3630--, _znpjgsef += (__81fgg2step3630)) {

						{
							
							*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = DCMPLX((_bafcbx97 * ILNumerics.F2NET.Intrinsics.DBLE(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) )));
							{
								System.Int32 __81fgg2dlsvn3631 = (System.Int32)((_znpjgsef + (int)1));
								const System.Int32 __81fgg2step3631 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3631;
								for (__81fgg2count3631 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3631 + __81fgg2step3631) / __81fgg2step3631)), _b5p6od9s = __81fgg2dlsvn3631; __81fgg2count3631 != 0; __81fgg2count3631--, _b5p6od9s += (__81fgg2step3631)) {

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
					System.Int32 __81fgg2dlsvn3632 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3632 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3632;
					for (__81fgg2count3632 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3632 + __81fgg2step3632) / __81fgg2step3632)), _znpjgsef = __81fgg2dlsvn3632; __81fgg2count3632 != 0; __81fgg2count3632--, _znpjgsef += (__81fgg2step3632)) {

					{
						
						if (_bafcbx97 == ILNumerics.F2NET.Intrinsics.DBLE(_d0547bi2 ))
						{
							
							{
								System.Int32 __81fgg2dlsvn3633 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3633 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3633;
								for (__81fgg2count3633 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn3633 + __81fgg2step3633) / __81fgg2step3633)), _b5p6od9s = __81fgg2dlsvn3633; __81fgg2count3633 != 0; __81fgg2count3633--, _b5p6od9s += (__81fgg2step3633)) {

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
								System.Int32 __81fgg2dlsvn3634 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3634 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3634;
								for (__81fgg2count3634 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn3634 + __81fgg2step3634) / __81fgg2step3634)), _b5p6od9s = __81fgg2dlsvn3634; __81fgg2count3634 != 0; __81fgg2count3634--, _b5p6od9s += (__81fgg2step3634)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark100:;
									// continue
								}
																}							}
							*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = DCMPLX((_bafcbx97 * ILNumerics.F2NET.Intrinsics.DBLE(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) )));
						}
						else
						{
							
							*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = DCMPLX(ILNumerics.F2NET.Intrinsics.DBLE(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) ));
						}
						
						{
							System.Int32 __81fgg2dlsvn3635 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3635 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3635;
							for (__81fgg2count3635 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn3635 + __81fgg2step3635) / __81fgg2step3635)), _68ec3gbh = __81fgg2dlsvn3635; __81fgg2count3635 != 0; __81fgg2count3635--, _68ec3gbh += (__81fgg2step3635)) {

							{
								
								if ((*(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) != _d0547bi2) | (*(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)) != _d0547bi2))
								{
									
									_yc8h372p = (_r7cfteg3 * ILNumerics.F2NET.Intrinsics.DCONJG(*(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)) ));
									_q3ig7mub = ILNumerics.F2NET.Intrinsics.DCONJG(_r7cfteg3 * *(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) );
									{
										System.Int32 __81fgg2dlsvn3636 = (System.Int32)((int)1);
										const System.Int32 __81fgg2step3636 = (System.Int32)((int)1);
										System.Int32 __81fgg2count3636;
										for (__81fgg2count3636 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn3636 + __81fgg2step3636) / __81fgg2step3636)), _b5p6od9s = __81fgg2dlsvn3636; __81fgg2count3636 != 0; __81fgg2count3636--, _b5p6od9s += (__81fgg2step3636)) {

										{
											
											*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = ((*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) + (*(_vxfgpup9+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) * _yc8h372p)) + (*(_p9n405a5+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)) * _q3ig7mub));
Mark110:;
											// continue
										}
																				}									}
									*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = DCMPLX((ILNumerics.F2NET.Intrinsics.DBLE(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) ) + ILNumerics.F2NET.Intrinsics.DBLE((*(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) * _yc8h372p) + (*(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)) * _q3ig7mub) )));
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
					System.Int32 __81fgg2dlsvn3637 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3637 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3637;
					for (__81fgg2count3637 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3637 + __81fgg2step3637) / __81fgg2step3637)), _znpjgsef = __81fgg2dlsvn3637; __81fgg2count3637 != 0; __81fgg2count3637--, _znpjgsef += (__81fgg2step3637)) {

					{
						
						if (_bafcbx97 == ILNumerics.F2NET.Intrinsics.DBLE(_d0547bi2 ))
						{
							
							{
								System.Int32 __81fgg2dlsvn3638 = (System.Int32)(_znpjgsef);
								const System.Int32 __81fgg2step3638 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3638;
								for (__81fgg2count3638 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3638 + __81fgg2step3638) / __81fgg2step3638)), _b5p6od9s = __81fgg2dlsvn3638; __81fgg2count3638 != 0; __81fgg2count3638--, _b5p6od9s += (__81fgg2step3638)) {

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
								System.Int32 __81fgg2dlsvn3639 = (System.Int32)((_znpjgsef + (int)1));
								const System.Int32 __81fgg2step3639 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3639;
								for (__81fgg2count3639 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3639 + __81fgg2step3639) / __81fgg2step3639)), _b5p6od9s = __81fgg2dlsvn3639; __81fgg2count3639 != 0; __81fgg2count3639--, _b5p6od9s += (__81fgg2step3639)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark150:;
									// continue
								}
																}							}
							*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = DCMPLX((_bafcbx97 * ILNumerics.F2NET.Intrinsics.DBLE(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) )));
						}
						else
						{
							
							*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = DCMPLX(ILNumerics.F2NET.Intrinsics.DBLE(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) ));
						}
						
						{
							System.Int32 __81fgg2dlsvn3640 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3640 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3640;
							for (__81fgg2count3640 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn3640 + __81fgg2step3640) / __81fgg2step3640)), _68ec3gbh = __81fgg2dlsvn3640; __81fgg2count3640 != 0; __81fgg2count3640--, _68ec3gbh += (__81fgg2step3640)) {

							{
								
								if ((*(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) != _d0547bi2) | (*(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)) != _d0547bi2))
								{
									
									_yc8h372p = (_r7cfteg3 * ILNumerics.F2NET.Intrinsics.DCONJG(*(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)) ));
									_q3ig7mub = ILNumerics.F2NET.Intrinsics.DCONJG(_r7cfteg3 * *(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) );
									{
										System.Int32 __81fgg2dlsvn3641 = (System.Int32)((_znpjgsef + (int)1));
										const System.Int32 __81fgg2step3641 = (System.Int32)((int)1);
										System.Int32 __81fgg2count3641;
										for (__81fgg2count3641 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3641 + __81fgg2step3641) / __81fgg2step3641)), _b5p6od9s = __81fgg2dlsvn3641; __81fgg2count3641 != 0; __81fgg2count3641--, _b5p6od9s += (__81fgg2step3641)) {

										{
											
											*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = ((*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) + (*(_vxfgpup9+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) * _yc8h372p)) + (*(_p9n405a5+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)) * _q3ig7mub));
Mark160:;
											// continue
										}
																				}									}
									*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = DCMPLX((ILNumerics.F2NET.Intrinsics.DBLE(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) ) + ILNumerics.F2NET.Intrinsics.DBLE((*(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) * _yc8h372p) + (*(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)) * _q3ig7mub) )));
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
					System.Int32 __81fgg2dlsvn3642 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3642 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3642;
					for (__81fgg2count3642 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3642 + __81fgg2step3642) / __81fgg2step3642)), _znpjgsef = __81fgg2dlsvn3642; __81fgg2count3642 != 0; __81fgg2count3642--, _znpjgsef += (__81fgg2step3642)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn3643 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3643 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3643;
							for (__81fgg2count3643 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn3643 + __81fgg2step3643) / __81fgg2step3643)), _b5p6od9s = __81fgg2dlsvn3643; __81fgg2count3643 != 0; __81fgg2count3643--, _b5p6od9s += (__81fgg2step3643)) {

							{
								
								_yc8h372p = _d0547bi2;
								_q3ig7mub = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn3644 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step3644 = (System.Int32)((int)1);
									System.Int32 __81fgg2count3644;
									for (__81fgg2count3644 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn3644 + __81fgg2step3644) / __81fgg2step3644)), _68ec3gbh = __81fgg2dlsvn3644; __81fgg2count3644 != 0; __81fgg2count3644--, _68ec3gbh += (__81fgg2step3644)) {

									{
										
										_yc8h372p = (_yc8h372p + (ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ) * *(_p9n405a5+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
										_q3ig7mub = (_q3ig7mub + (ILNumerics.F2NET.Intrinsics.DCONJG(*(_p9n405a5+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ly9opahg)) ) * *(_vxfgpup9+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
Mark190:;
										// continue
									}
																		}								}
								if (_b5p6od9s == _znpjgsef)
								{
									
									if (_bafcbx97 == ILNumerics.F2NET.Intrinsics.DBLE(_d0547bi2 ))
									{
										
										*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = DCMPLX(ILNumerics.F2NET.Intrinsics.DBLE((_r7cfteg3 * _yc8h372p) + (ILNumerics.F2NET.Intrinsics.DCONJG(_r7cfteg3 ) * _q3ig7mub) ));
									}
									else
									{
										
										*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = DCMPLX(((_bafcbx97 * ILNumerics.F2NET.Intrinsics.DBLE(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) )) + ILNumerics.F2NET.Intrinsics.DBLE((_r7cfteg3 * _yc8h372p) + (ILNumerics.F2NET.Intrinsics.DCONJG(_r7cfteg3 ) * _q3ig7mub) )));
									}
									
								}
								else
								{
									
									if (_bafcbx97 == ILNumerics.F2NET.Intrinsics.DBLE(_d0547bi2 ))
									{
										
										*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = ((_r7cfteg3 * _yc8h372p) + (ILNumerics.F2NET.Intrinsics.DCONJG(_r7cfteg3 ) * _q3ig7mub));
									}
									else
									{
										
										*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (((_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))) + (_r7cfteg3 * _yc8h372p)) + (ILNumerics.F2NET.Intrinsics.DCONJG(_r7cfteg3 ) * _q3ig7mub));
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
					System.Int32 __81fgg2dlsvn3645 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3645 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3645;
					for (__81fgg2count3645 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3645 + __81fgg2step3645) / __81fgg2step3645)), _znpjgsef = __81fgg2dlsvn3645; __81fgg2count3645 != 0; __81fgg2count3645--, _znpjgsef += (__81fgg2step3645)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn3646 = (System.Int32)(_znpjgsef);
							const System.Int32 __81fgg2step3646 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3646;
							for (__81fgg2count3646 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3646 + __81fgg2step3646) / __81fgg2step3646)), _b5p6od9s = __81fgg2dlsvn3646; __81fgg2count3646 != 0; __81fgg2count3646--, _b5p6od9s += (__81fgg2step3646)) {

							{
								
								_yc8h372p = _d0547bi2;
								_q3ig7mub = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn3647 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step3647 = (System.Int32)((int)1);
									System.Int32 __81fgg2count3647;
									for (__81fgg2count3647 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn3647 + __81fgg2step3647) / __81fgg2step3647)), _68ec3gbh = __81fgg2dlsvn3647; __81fgg2count3647 != 0; __81fgg2count3647--, _68ec3gbh += (__81fgg2step3647)) {

									{
										
										_yc8h372p = (_yc8h372p + (ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ) * *(_p9n405a5+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
										_q3ig7mub = (_q3ig7mub + (ILNumerics.F2NET.Intrinsics.DCONJG(*(_p9n405a5+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ly9opahg)) ) * *(_vxfgpup9+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
Mark220:;
										// continue
									}
																		}								}
								if (_b5p6od9s == _znpjgsef)
								{
									
									if (_bafcbx97 == ILNumerics.F2NET.Intrinsics.DBLE(_d0547bi2 ))
									{
										
										*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = DCMPLX(ILNumerics.F2NET.Intrinsics.DBLE((_r7cfteg3 * _yc8h372p) + (ILNumerics.F2NET.Intrinsics.DCONJG(_r7cfteg3 ) * _q3ig7mub) ));
									}
									else
									{
										
										*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = DCMPLX(((_bafcbx97 * ILNumerics.F2NET.Intrinsics.DBLE(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) )) + ILNumerics.F2NET.Intrinsics.DBLE((_r7cfteg3 * _yc8h372p) + (ILNumerics.F2NET.Intrinsics.DCONJG(_r7cfteg3 ) * _q3ig7mub) )));
									}
									
								}
								else
								{
									
									if (_bafcbx97 == ILNumerics.F2NET.Intrinsics.DBLE(_d0547bi2 ))
									{
										
										*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = ((_r7cfteg3 * _yc8h372p) + (ILNumerics.F2NET.Intrinsics.DCONJG(_r7cfteg3 ) * _q3ig7mub));
									}
									else
									{
										
										*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (((_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))) + (_r7cfteg3 * _yc8h372p)) + (ILNumerics.F2NET.Intrinsics.DCONJG(_r7cfteg3 ) * _q3ig7mub));
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
		//*     End of ZHER2K. 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
