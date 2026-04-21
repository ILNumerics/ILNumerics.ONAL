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
//*> \brief \b CHERK 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CHERK(UPLO,TRANS,N,K,ALPHA,A,LDA,BETA,C,LDC) 
//* 
//*       .. Scalar Arguments .. 
//*       REAL ALPHA,BETA 
//*       INTEGER K,LDA,LDC,N 
//*       CHARACTER TRANS,UPLO 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX A(LDA,*),C(LDC,*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CHERK  performs one of the hermitian rank k operations 
//*> 
//*>    C := alpha*A*A**H + beta*C, 
//*> 
//*> or 
//*> 
//*>    C := alpha*A**H*A + beta*C, 
//*> 
//*> where  alpha and beta  are  real scalars,  C is an  n by n  hermitian 
//*> matrix and  A  is an  n by k  matrix in the  first case and a  k by n 
//*> matrix in the second case. 
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
//*>              TRANS = 'N' or 'n'   C := alpha*A*A**H + beta*C. 
//*> 
//*>              TRANS = 'C' or 'c'   C := alpha*A**H*A + beta*C. 
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
//*>           TRANS = 'C' or 'c',  K  specifies  the number of rows of the 
//*>           matrix A.  K must be at least zero. 
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

	 
	public static void _xviy1sx7(FString _9wyre9zc, FString _scuo79v4, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, ref Single _r7cfteg3, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, ref Single _bafcbx97, fcomplex* _3crf0qn3, ref Int32 _1s3eymp4)
	{
#region variable declarations
fcomplex _1ajfmh55 =  default;
Single _b56sf68i =  default;
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
		if (_1s3eymp4 < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)10;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("CHERK " ,ref _gro5yvfo );
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
						System.Int32 __81fgg2dlsvn1586 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1586 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1586;
						for (__81fgg2count1586 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1586 + __81fgg2step1586) / __81fgg2step1586)), _znpjgsef = __81fgg2dlsvn1586; __81fgg2count1586 != 0; __81fgg2count1586--, _znpjgsef += (__81fgg2step1586)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1587 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1587 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1587;
								for (__81fgg2count1587 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn1587 + __81fgg2step1587) / __81fgg2step1587)), _b5p6od9s = __81fgg2dlsvn1587; __81fgg2count1587 != 0; __81fgg2count1587--, _b5p6od9s += (__81fgg2step1587)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = CMPLX(_d0547bi2);
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
						System.Int32 __81fgg2dlsvn1588 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1588 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1588;
						for (__81fgg2count1588 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1588 + __81fgg2step1588) / __81fgg2step1588)), _znpjgsef = __81fgg2dlsvn1588; __81fgg2count1588 != 0; __81fgg2count1588--, _znpjgsef += (__81fgg2step1588)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1589 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1589 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1589;
								for (__81fgg2count1589 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn1589 + __81fgg2step1589) / __81fgg2step1589)), _b5p6od9s = __81fgg2dlsvn1589; __81fgg2count1589 != 0; __81fgg2count1589--, _b5p6od9s += (__81fgg2step1589)) {

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
				
				if (_bafcbx97 == _d0547bi2)
				{
					
					{
						System.Int32 __81fgg2dlsvn1590 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1590 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1590;
						for (__81fgg2count1590 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1590 + __81fgg2step1590) / __81fgg2step1590)), _znpjgsef = __81fgg2dlsvn1590; __81fgg2count1590 != 0; __81fgg2count1590--, _znpjgsef += (__81fgg2step1590)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1591 = (System.Int32)(_znpjgsef);
								const System.Int32 __81fgg2step1591 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1591;
								for (__81fgg2count1591 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1591 + __81fgg2step1591) / __81fgg2step1591)), _b5p6od9s = __81fgg2dlsvn1591; __81fgg2count1591 != 0; __81fgg2count1591--, _b5p6od9s += (__81fgg2step1591)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = CMPLX(_d0547bi2);
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
						System.Int32 __81fgg2dlsvn1592 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1592 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1592;
						for (__81fgg2count1592 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1592 + __81fgg2step1592) / __81fgg2step1592)), _znpjgsef = __81fgg2dlsvn1592; __81fgg2count1592 != 0; __81fgg2count1592--, _znpjgsef += (__81fgg2step1592)) {

						{
							
							*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = CMPLX((_bafcbx97 * ILNumerics.F2NET.Intrinsics.REAL(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) )));
							{
								System.Int32 __81fgg2dlsvn1593 = (System.Int32)((_znpjgsef + (int)1));
								const System.Int32 __81fgg2step1593 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1593;
								for (__81fgg2count1593 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1593 + __81fgg2step1593) / __81fgg2step1593)), _b5p6od9s = __81fgg2dlsvn1593; __81fgg2count1593 != 0; __81fgg2count1593--, _b5p6od9s += (__81fgg2step1593)) {

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
			//*        Form  C := alpha*A*A**H + beta*C. 
			//* 
			
			if (_l08igmvf)
			{
				
				{
					System.Int32 __81fgg2dlsvn1594 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1594 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1594;
					for (__81fgg2count1594 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1594 + __81fgg2step1594) / __81fgg2step1594)), _znpjgsef = __81fgg2dlsvn1594; __81fgg2count1594 != 0; __81fgg2count1594--, _znpjgsef += (__81fgg2step1594)) {

					{
						
						if (_bafcbx97 == _d0547bi2)
						{
							
							{
								System.Int32 __81fgg2dlsvn1595 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1595 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1595;
								for (__81fgg2count1595 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn1595 + __81fgg2step1595) / __81fgg2step1595)), _b5p6od9s = __81fgg2dlsvn1595; __81fgg2count1595 != 0; __81fgg2count1595--, _b5p6od9s += (__81fgg2step1595)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = CMPLX(_d0547bi2);
Mark90:;
									// continue
								}
																}							}
						}
						else
						if (_bafcbx97 != _kxg5drh2)
						{
							
							{
								System.Int32 __81fgg2dlsvn1596 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1596 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1596;
								for (__81fgg2count1596 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn1596 + __81fgg2step1596) / __81fgg2step1596)), _b5p6od9s = __81fgg2dlsvn1596; __81fgg2count1596 != 0; __81fgg2count1596--, _b5p6od9s += (__81fgg2step1596)) {

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
							System.Int32 __81fgg2dlsvn1597 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step1597 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1597;
							for (__81fgg2count1597 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1597 + __81fgg2step1597) / __81fgg2step1597)), _68ec3gbh = __81fgg2dlsvn1597; __81fgg2count1597 != 0; __81fgg2count1597--, _68ec3gbh += (__81fgg2step1597)) {

							{
								
								if (*(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) != ILNumerics.F2NET.Intrinsics.CMPLX(_d0547bi2 ))
								{
									
									_1ajfmh55 = (_r7cfteg3 * ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) ));
									{
										System.Int32 __81fgg2dlsvn1598 = (System.Int32)((int)1);
										const System.Int32 __81fgg2step1598 = (System.Int32)((int)1);
										System.Int32 __81fgg2count1598;
										for (__81fgg2count1598 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn1598 + __81fgg2step1598) / __81fgg2step1598)), _b5p6od9s = __81fgg2dlsvn1598; __81fgg2count1598 != 0; __81fgg2count1598--, _b5p6od9s += (__81fgg2step1598)) {

										{
											
											*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) + (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c))));
Mark110:;
											// continue
										}
																				}									}
									*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = CMPLX((ILNumerics.F2NET.Intrinsics.REAL(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) ) + ILNumerics.F2NET.Intrinsics.REAL(_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) )));
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
					System.Int32 __81fgg2dlsvn1599 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1599 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1599;
					for (__81fgg2count1599 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1599 + __81fgg2step1599) / __81fgg2step1599)), _znpjgsef = __81fgg2dlsvn1599; __81fgg2count1599 != 0; __81fgg2count1599--, _znpjgsef += (__81fgg2step1599)) {

					{
						
						if (_bafcbx97 == _d0547bi2)
						{
							
							{
								System.Int32 __81fgg2dlsvn1600 = (System.Int32)(_znpjgsef);
								const System.Int32 __81fgg2step1600 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1600;
								for (__81fgg2count1600 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1600 + __81fgg2step1600) / __81fgg2step1600)), _b5p6od9s = __81fgg2dlsvn1600; __81fgg2count1600 != 0; __81fgg2count1600--, _b5p6od9s += (__81fgg2step1600)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = CMPLX(_d0547bi2);
Mark140:;
									// continue
								}
																}							}
						}
						else
						if (_bafcbx97 != _kxg5drh2)
						{
							
							*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = CMPLX((_bafcbx97 * ILNumerics.F2NET.Intrinsics.REAL(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) )));
							{
								System.Int32 __81fgg2dlsvn1601 = (System.Int32)((_znpjgsef + (int)1));
								const System.Int32 __81fgg2step1601 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1601;
								for (__81fgg2count1601 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1601 + __81fgg2step1601) / __81fgg2step1601)), _b5p6od9s = __81fgg2dlsvn1601; __81fgg2count1601 != 0; __81fgg2count1601--, _b5p6od9s += (__81fgg2step1601)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark150:;
									// continue
								}
																}							}
						}
						else
						{
							
							*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = CMPLX(ILNumerics.F2NET.Intrinsics.REAL(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) ));
						}
						
						{
							System.Int32 __81fgg2dlsvn1602 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step1602 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1602;
							for (__81fgg2count1602 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1602 + __81fgg2step1602) / __81fgg2step1602)), _68ec3gbh = __81fgg2dlsvn1602; __81fgg2count1602 != 0; __81fgg2count1602--, _68ec3gbh += (__81fgg2step1602)) {

							{
								
								if (*(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) != ILNumerics.F2NET.Intrinsics.CMPLX(_d0547bi2 ))
								{
									
									_1ajfmh55 = (_r7cfteg3 * ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) ));
									*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = CMPLX((ILNumerics.F2NET.Intrinsics.REAL(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) ) + ILNumerics.F2NET.Intrinsics.REAL(_1ajfmh55 * *(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) )));
									{
										System.Int32 __81fgg2dlsvn1603 = (System.Int32)((_znpjgsef + (int)1));
										const System.Int32 __81fgg2step1603 = (System.Int32)((int)1);
										System.Int32 __81fgg2count1603;
										for (__81fgg2count1603 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1603 + __81fgg2step1603) / __81fgg2step1603)), _b5p6od9s = __81fgg2dlsvn1603; __81fgg2count1603 != 0; __81fgg2count1603--, _b5p6od9s += (__81fgg2step1603)) {

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
			//*        Form  C := alpha*A**H*A + beta*C. 
			//* 
			
			if (_l08igmvf)
			{
				
				{
					System.Int32 __81fgg2dlsvn1604 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1604 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1604;
					for (__81fgg2count1604 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1604 + __81fgg2step1604) / __81fgg2step1604)), _znpjgsef = __81fgg2dlsvn1604; __81fgg2count1604 != 0; __81fgg2count1604--, _znpjgsef += (__81fgg2step1604)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn1605 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step1605 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1605;
							for (__81fgg2count1605 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn1605 + __81fgg2step1605) / __81fgg2step1605)), _b5p6od9s = __81fgg2dlsvn1605; __81fgg2count1605 != 0; __81fgg2count1605--, _b5p6od9s += (__81fgg2step1605)) {

							{
								
								_1ajfmh55 = CMPLX(_d0547bi2);
								{
									System.Int32 __81fgg2dlsvn1606 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1606 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1606;
									for (__81fgg2count1606 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1606 + __81fgg2step1606) / __81fgg2step1606)), _68ec3gbh = __81fgg2dlsvn1606; __81fgg2count1606 != 0; __81fgg2count1606--, _68ec3gbh += (__81fgg2step1606)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 + (ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ) * *(_vxfgpup9+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
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
						_b56sf68i = _d0547bi2;
						{
							System.Int32 __81fgg2dlsvn1607 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step1607 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1607;
							for (__81fgg2count1607 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1607 + __81fgg2step1607) / __81fgg2step1607)), _68ec3gbh = __81fgg2dlsvn1607; __81fgg2count1607 != 0; __81fgg2count1607--, _68ec3gbh += (__81fgg2step1607)) {

							{
								
								_b56sf68i = REAL((_b56sf68i + (ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) * *(_vxfgpup9+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)))));
Mark210:;
								// continue
							}
														}						}
						if (_bafcbx97 == _d0547bi2)
						{
							
							*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = CMPLX((_r7cfteg3 * _b56sf68i));
						}
						else
						{
							
							*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = CMPLX(((_r7cfteg3 * _b56sf68i) + (_bafcbx97 * ILNumerics.F2NET.Intrinsics.REAL(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) ))));
						}
						
Mark220:;
						// continue
					}
										}				}
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn1608 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1608 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1608;
					for (__81fgg2count1608 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1608 + __81fgg2step1608) / __81fgg2step1608)), _znpjgsef = __81fgg2dlsvn1608; __81fgg2count1608 != 0; __81fgg2count1608--, _znpjgsef += (__81fgg2step1608)) {

					{
						
						_b56sf68i = _d0547bi2;
						{
							System.Int32 __81fgg2dlsvn1609 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step1609 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1609;
							for (__81fgg2count1609 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1609 + __81fgg2step1609) / __81fgg2step1609)), _68ec3gbh = __81fgg2dlsvn1609; __81fgg2count1609 != 0; __81fgg2count1609--, _68ec3gbh += (__81fgg2step1609)) {

							{
								
								_b56sf68i = REAL((_b56sf68i + (ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) * *(_vxfgpup9+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)))));
Mark230:;
								// continue
							}
														}						}
						if (_bafcbx97 == _d0547bi2)
						{
							
							*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = CMPLX((_r7cfteg3 * _b56sf68i));
						}
						else
						{
							
							*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = CMPLX(((_r7cfteg3 * _b56sf68i) + (_bafcbx97 * ILNumerics.F2NET.Intrinsics.REAL(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) ))));
						}
						
						{
							System.Int32 __81fgg2dlsvn1610 = (System.Int32)((_znpjgsef + (int)1));
							const System.Int32 __81fgg2step1610 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1610;
							for (__81fgg2count1610 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1610 + __81fgg2step1610) / __81fgg2step1610)), _b5p6od9s = __81fgg2dlsvn1610; __81fgg2count1610 != 0; __81fgg2count1610--, _b5p6od9s += (__81fgg2step1610)) {

							{
								
								_1ajfmh55 = CMPLX(_d0547bi2);
								{
									System.Int32 __81fgg2dlsvn1611 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1611 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1611;
									for (__81fgg2count1611 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1611 + __81fgg2step1611) / __81fgg2step1611)), _68ec3gbh = __81fgg2dlsvn1611; __81fgg2count1611 != 0; __81fgg2count1611--, _68ec3gbh += (__81fgg2step1611)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 + (ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ) * *(_vxfgpup9+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
Mark240:;
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
								
Mark250:;
								// continue
							}
														}						}
Mark260:;
						// continue
					}
										}				}
			}
			
		}
		//* 
		
		return;//* 
		//*     End of CHERK . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
