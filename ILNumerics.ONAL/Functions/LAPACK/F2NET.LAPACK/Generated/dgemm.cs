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
//*> \brief \b DGEMM 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DGEMM(TRANSA,TRANSB,M,N,K,ALPHA,A,LDA,B,LDB,BETA,C,LDC) 
//* 
//*       .. Scalar Arguments .. 
//*       DOUBLE PRECISION ALPHA,BETA 
//*       INTEGER K,LDA,LDB,LDC,M,N 
//*       CHARACTER TRANSA,TRANSB 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION A(LDA,*),B(LDB,*),C(LDC,*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DGEMM  performs one of the matrix-matrix operations 
//*> 
//*>    C := alpha*op( A )*op( B ) + beta*C, 
//*> 
//*> where  op( X ) is one of 
//*> 
//*>    op( X ) = X   or   op( X ) = X**T, 
//*> 
//*> alpha and beta are scalars, and A, B and C are matrices, with op( A ) 
//*> an m by k matrix,  op( B )  a  k by n matrix and  C an m by n matrix. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] TRANSA 
//*> \verbatim 
//*>          TRANSA is CHARACTER*1 
//*>           On entry, TRANSA specifies the form of op( A ) to be used in 
//*>           the matrix multiplication as follows: 
//*> 
//*>              TRANSA = 'N' or 'n',  op( A ) = A. 
//*> 
//*>              TRANSA = 'T' or 't',  op( A ) = A**T. 
//*> 
//*>              TRANSA = 'C' or 'c',  op( A ) = A**T. 
//*> \endverbatim 
//*> 
//*> \param[in] TRANSB 
//*> \verbatim 
//*>          TRANSB is CHARACTER*1 
//*>           On entry, TRANSB specifies the form of op( B ) to be used in 
//*>           the matrix multiplication as follows: 
//*> 
//*>              TRANSB = 'N' or 'n',  op( B ) = B. 
//*> 
//*>              TRANSB = 'T' or 't',  op( B ) = B**T. 
//*> 
//*>              TRANSB = 'C' or 'c',  op( B ) = B**T. 
//*> \endverbatim 
//*> 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>           On entry,  M  specifies  the number  of rows  of the  matrix 
//*>           op( A )  and of the  matrix  C.  M  must  be at least  zero. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>           On entry,  N  specifies the number  of columns of the matrix 
//*>           op( B ) and the number of columns of the matrix C. N must be 
//*>           at least zero. 
//*> \endverbatim 
//*> 
//*> \param[in] K 
//*> \verbatim 
//*>          K is INTEGER 
//*>           On entry,  K  specifies  the number of columns of the matrix 
//*>           op( A ) and the number of rows of the matrix op( B ). K must 
//*>           be at least  zero. 
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
//*>           k  when  TRANSA = 'N' or 'n',  and is  m  otherwise. 
//*>           Before entry with  TRANSA = 'N' or 'n',  the leading  m by k 
//*>           part of the array  A  must contain the matrix  A,  otherwise 
//*>           the leading  k by m  part of the array  A  must contain  the 
//*>           matrix A. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>           On entry, LDA specifies the first dimension of A as declared 
//*>           in the calling (sub) program. When  TRANSA = 'N' or 'n' then 
//*>           LDA must be at least  max( 1, m ), otherwise  LDA must be at 
//*>           least  max( 1, k ). 
//*> \endverbatim 
//*> 
//*> \param[in] B 
//*> \verbatim 
//*>          B is DOUBLE PRECISION array, dimension ( LDB, kb ), where kb is 
//*>           n  when  TRANSB = 'N' or 'n',  and is  k  otherwise. 
//*>           Before entry with  TRANSB = 'N' or 'n',  the leading  k by n 
//*>           part of the array  B  must contain the matrix  B,  otherwise 
//*>           the leading  n by k  part of the array  B  must contain  the 
//*>           matrix B. 
//*> \endverbatim 
//*> 
//*> \param[in] LDB 
//*> \verbatim 
//*>          LDB is INTEGER 
//*>           On entry, LDB specifies the first dimension of B as declared 
//*>           in the calling (sub) program. When  TRANSB = 'N' or 'n' then 
//*>           LDB must be at least  max( 1, k ), otherwise  LDB must be at 
//*>           least  max( 1, n ). 
//*> \endverbatim 
//*> 
//*> \param[in] BETA 
//*> \verbatim 
//*>          BETA is DOUBLE PRECISION. 
//*>           On entry,  BETA  specifies the scalar  beta.  When  BETA  is 
//*>           supplied as zero then C need not be set on input. 
//*> \endverbatim 
//*> 
//*> \param[in,out] C 
//*> \verbatim 
//*>          C is DOUBLE PRECISION array, dimension ( LDC, N ) 
//*>           Before entry, the leading  m by n  part of the array  C must 
//*>           contain the matrix  C,  except when  beta  is zero, in which 
//*>           case C need not be set on entry. 
//*>           On exit, the array  C  is overwritten by the  m by n  matrix 
//*>           ( alpha*op( A )*op( B ) + beta*C ). 
//*> \endverbatim 
//*> 
//*> \param[in] LDC 
//*> \verbatim 
//*>          LDC is INTEGER 
//*>           On entry, LDC specifies the first dimension of C as declared 
//*>           in  the  calling  (sub)  program.   LDC  must  be  at  least 
//*>           max( 1, m ). 
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

	 
	public static void _5nsxi69c(FString _742vrzth, FString _30rlu6np, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, ref Double _r7cfteg3, Double* _vxfgpup9, ref Int32 _ocv8fk5c, Double* _p9n405a5, ref Int32 _ly9opahg, ref Double _bafcbx97, Double* _3crf0qn3, ref Int32 _1s3eymp4)
	{
#region variable declarations
Double _1ajfmh55 =  default;
Int32 _b5p6od9s =  default;
Int32 _gro5yvfo =  default;
Int32 _znpjgsef =  default;
Int32 _68ec3gbh =  default;
Int32 _a7s478dp =  default;
Int32 _o9a6qdux =  default;
Int32 _80j3zf13 =  default;
Boolean _skucdj1b =  default;
Boolean _x18i1ben =  default;
Double _kxg5drh2 =  1d;
Double _d0547bi2 =  0d;
string fLanavab = default;
#endregion  variable declarations
_742vrzth = _742vrzth.Convert(1);
_30rlu6np = _30rlu6np.Convert(1);

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
		//*     Set  NOTA  and  NOTB  as  true if  A  and  B  respectively are not 
		//*     transposed and set  NROWA, NCOLA and  NROWB  as the number of rows 
		//*     and  columns of  A  and the  number of  rows  of  B  respectively. 
		//* 
		
		_skucdj1b = _w8y2rzgy(_742vrzth ,"N" );
		_x18i1ben = _w8y2rzgy(_30rlu6np ,"N" );
		if (_skucdj1b)
		{
			
			_o9a6qdux = _ev4xhht5;
			_a7s478dp = _umlkckdg;
		}
		else
		{
			
			_o9a6qdux = _umlkckdg;
			_a7s478dp = _ev4xhht5;
		}
		
		if (_x18i1ben)
		{
			
			_80j3zf13 = _umlkckdg;
		}
		else
		{
			
			_80j3zf13 = _dxpq0xkr;
		}
		//* 
		//*     Test the input parameters. 
		//* 
		
		_gro5yvfo = (int)0;
		if (((!(_skucdj1b)) & (!(_w8y2rzgy(_742vrzth ,"C" )))) & (!(_w8y2rzgy(_742vrzth ,"T" ))))
		{
			
			_gro5yvfo = (int)1;
		}
		else
		if (((!(_x18i1ben)) & (!(_w8y2rzgy(_30rlu6np ,"C" )))) & (!(_w8y2rzgy(_30rlu6np ,"T" ))))
		{
			
			_gro5yvfo = (int)2;
		}
		else
		if (_ev4xhht5 < (int)0)
		{
			
			_gro5yvfo = (int)3;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)4;
		}
		else
		if (_umlkckdg < (int)0)
		{
			
			_gro5yvfo = (int)5;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_o9a6qdux ))
		{
			
			_gro5yvfo = (int)8;
		}
		else
		if (_ly9opahg < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_80j3zf13 ))
		{
			
			_gro5yvfo = (int)10;
		}
		else
		if (_1s3eymp4 < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_ev4xhht5 ))
		{
			
			_gro5yvfo = (int)13;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("DGEMM " ,ref _gro5yvfo );
			return;
		}
		//* 
		//*     Quick return if possible. 
		//* 
		
		if (((_ev4xhht5 == (int)0) | (_dxpq0xkr == (int)0)) | (((_r7cfteg3 == _d0547bi2) | (_umlkckdg == (int)0)) & (_bafcbx97 == _kxg5drh2)))
		return;//* 
		//*     And if  alpha.eq.zero. 
		//* 
		
		if (_r7cfteg3 == _d0547bi2)
		{
			
			if (_bafcbx97 == _d0547bi2)
			{
				
				{
					System.Int32 __81fgg2dlsvn53 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step53 = (System.Int32)((int)1);
					System.Int32 __81fgg2count53;
					for (__81fgg2count53 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn53 + __81fgg2step53) / __81fgg2step53)), _znpjgsef = __81fgg2dlsvn53; __81fgg2count53 != 0; __81fgg2count53--, _znpjgsef += (__81fgg2step53)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn54 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step54 = (System.Int32)((int)1);
							System.Int32 __81fgg2count54;
							for (__81fgg2count54 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn54 + __81fgg2step54) / __81fgg2step54)), _b5p6od9s = __81fgg2dlsvn54; __81fgg2count54 != 0; __81fgg2count54--, _b5p6od9s += (__81fgg2step54)) {

							{
								
								*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = _d0547bi2;
Mark10:;
								// continue
							}
														}						}
Mark20:;
						// continue
					}
										}				}
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn55 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step55 = (System.Int32)((int)1);
					System.Int32 __81fgg2count55;
					for (__81fgg2count55 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn55 + __81fgg2step55) / __81fgg2step55)), _znpjgsef = __81fgg2dlsvn55; __81fgg2count55 != 0; __81fgg2count55--, _znpjgsef += (__81fgg2step55)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn56 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step56 = (System.Int32)((int)1);
							System.Int32 __81fgg2count56;
							for (__81fgg2count56 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn56 + __81fgg2step56) / __81fgg2step56)), _b5p6od9s = __81fgg2dlsvn56; __81fgg2count56 != 0; __81fgg2count56--, _b5p6od9s += (__81fgg2step56)) {

							{
								
								*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark30:;
								// continue
							}
														}						}
Mark40:;
						// continue
					}
										}				}
			}
			
			return;
		}
		//* 
		//*     Start the operations. 
		//* 
		
		if (_x18i1ben)
		{
			
			if (_skucdj1b)
			{
				//* 
				//*           Form  C := alpha*A*B + beta*C. 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn57 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step57 = (System.Int32)((int)1);
					System.Int32 __81fgg2count57;
					for (__81fgg2count57 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn57 + __81fgg2step57) / __81fgg2step57)), _znpjgsef = __81fgg2dlsvn57; __81fgg2count57 != 0; __81fgg2count57--, _znpjgsef += (__81fgg2step57)) {

					{
						
						if (_bafcbx97 == _d0547bi2)
						{
							
							{
								System.Int32 __81fgg2dlsvn58 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step58 = (System.Int32)((int)1);
								System.Int32 __81fgg2count58;
								for (__81fgg2count58 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn58 + __81fgg2step58) / __81fgg2step58)), _b5p6od9s = __81fgg2dlsvn58; __81fgg2count58 != 0; __81fgg2count58--, _b5p6od9s += (__81fgg2step58)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = _d0547bi2;
Mark50:;
									// continue
								}
																}							}
						}
						else
						if (_bafcbx97 != _kxg5drh2)
						{
							
							{
								System.Int32 __81fgg2dlsvn59 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step59 = (System.Int32)((int)1);
								System.Int32 __81fgg2count59;
								for (__81fgg2count59 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn59 + __81fgg2step59) / __81fgg2step59)), _b5p6od9s = __81fgg2dlsvn59; __81fgg2count59 != 0; __81fgg2count59--, _b5p6od9s += (__81fgg2step59)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark60:;
									// continue
								}
																}							}
						}
						
						{
							System.Int32 __81fgg2dlsvn60 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step60 = (System.Int32)((int)1);
							System.Int32 __81fgg2count60;
							for (__81fgg2count60 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn60 + __81fgg2step60) / __81fgg2step60)), _68ec3gbh = __81fgg2dlsvn60; __81fgg2count60 != 0; __81fgg2count60--, _68ec3gbh += (__81fgg2step60)) {

							{
								
								_1ajfmh55 = (_r7cfteg3 * *(_p9n405a5+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
								{
									System.Int32 __81fgg2dlsvn61 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step61 = (System.Int32)((int)1);
									System.Int32 __81fgg2count61;
									for (__81fgg2count61 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn61 + __81fgg2step61) / __81fgg2step61)), _b5p6od9s = __81fgg2dlsvn61; __81fgg2count61 != 0; __81fgg2count61--, _b5p6od9s += (__81fgg2step61)) {

									{
										
										*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) + (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c))));
Mark70:;
										// continue
									}
																		}								}
Mark80:;
								// continue
							}
														}						}
Mark90:;
						// continue
					}
										}				}
			}
			else
			{
				//* 
				//*           Form  C := alpha*A**T*B + beta*C 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn62 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step62 = (System.Int32)((int)1);
					System.Int32 __81fgg2count62;
					for (__81fgg2count62 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn62 + __81fgg2step62) / __81fgg2step62)), _znpjgsef = __81fgg2dlsvn62; __81fgg2count62 != 0; __81fgg2count62--, _znpjgsef += (__81fgg2step62)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn63 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step63 = (System.Int32)((int)1);
							System.Int32 __81fgg2count63;
							for (__81fgg2count63 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn63 + __81fgg2step63) / __81fgg2step63)), _b5p6od9s = __81fgg2dlsvn63; __81fgg2count63 != 0; __81fgg2count63--, _b5p6od9s += (__81fgg2step63)) {

							{
								
								_1ajfmh55 = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn64 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step64 = (System.Int32)((int)1);
									System.Int32 __81fgg2count64;
									for (__81fgg2count64 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn64 + __81fgg2step64) / __81fgg2step64)), _68ec3gbh = __81fgg2dlsvn64; __81fgg2count64 != 0; __81fgg2count64--, _68ec3gbh += (__81fgg2step64)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 + (*(_vxfgpup9+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) * *(_p9n405a5+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
Mark100:;
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
								
Mark110:;
								// continue
							}
														}						}
Mark120:;
						// continue
					}
										}				}
			}
			
		}
		else
		{
			
			if (_skucdj1b)
			{
				//* 
				//*           Form  C := alpha*A*B**T + beta*C 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn65 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step65 = (System.Int32)((int)1);
					System.Int32 __81fgg2count65;
					for (__81fgg2count65 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn65 + __81fgg2step65) / __81fgg2step65)), _znpjgsef = __81fgg2dlsvn65; __81fgg2count65 != 0; __81fgg2count65--, _znpjgsef += (__81fgg2step65)) {

					{
						
						if (_bafcbx97 == _d0547bi2)
						{
							
							{
								System.Int32 __81fgg2dlsvn66 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step66 = (System.Int32)((int)1);
								System.Int32 __81fgg2count66;
								for (__81fgg2count66 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn66 + __81fgg2step66) / __81fgg2step66)), _b5p6od9s = __81fgg2dlsvn66; __81fgg2count66 != 0; __81fgg2count66--, _b5p6od9s += (__81fgg2step66)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = _d0547bi2;
Mark130:;
									// continue
								}
																}							}
						}
						else
						if (_bafcbx97 != _kxg5drh2)
						{
							
							{
								System.Int32 __81fgg2dlsvn67 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step67 = (System.Int32)((int)1);
								System.Int32 __81fgg2count67;
								for (__81fgg2count67 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn67 + __81fgg2step67) / __81fgg2step67)), _b5p6od9s = __81fgg2dlsvn67; __81fgg2count67 != 0; __81fgg2count67--, _b5p6od9s += (__81fgg2step67)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark140:;
									// continue
								}
																}							}
						}
						
						{
							System.Int32 __81fgg2dlsvn68 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step68 = (System.Int32)((int)1);
							System.Int32 __81fgg2count68;
							for (__81fgg2count68 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn68 + __81fgg2step68) / __81fgg2step68)), _68ec3gbh = __81fgg2dlsvn68; __81fgg2count68 != 0; __81fgg2count68--, _68ec3gbh += (__81fgg2step68)) {

							{
								
								_1ajfmh55 = (_r7cfteg3 * *(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)));
								{
									System.Int32 __81fgg2dlsvn69 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step69 = (System.Int32)((int)1);
									System.Int32 __81fgg2count69;
									for (__81fgg2count69 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn69 + __81fgg2step69) / __81fgg2step69)), _b5p6od9s = __81fgg2dlsvn69; __81fgg2count69 != 0; __81fgg2count69--, _b5p6od9s += (__81fgg2step69)) {

									{
										
										*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) + (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c))));
Mark150:;
										// continue
									}
																		}								}
Mark160:;
								// continue
							}
														}						}
Mark170:;
						// continue
					}
										}				}
			}
			else
			{
				//* 
				//*           Form  C := alpha*A**T*B**T + beta*C 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn70 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step70 = (System.Int32)((int)1);
					System.Int32 __81fgg2count70;
					for (__81fgg2count70 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn70 + __81fgg2step70) / __81fgg2step70)), _znpjgsef = __81fgg2dlsvn70; __81fgg2count70 != 0; __81fgg2count70--, _znpjgsef += (__81fgg2step70)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn71 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step71 = (System.Int32)((int)1);
							System.Int32 __81fgg2count71;
							for (__81fgg2count71 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn71 + __81fgg2step71) / __81fgg2step71)), _b5p6od9s = __81fgg2dlsvn71; __81fgg2count71 != 0; __81fgg2count71--, _b5p6od9s += (__81fgg2step71)) {

							{
								
								_1ajfmh55 = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn72 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step72 = (System.Int32)((int)1);
									System.Int32 __81fgg2count72;
									for (__81fgg2count72 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn72 + __81fgg2step72) / __81fgg2step72)), _68ec3gbh = __81fgg2dlsvn72; __81fgg2count72 != 0; __81fgg2count72--, _68ec3gbh += (__81fgg2step72)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 + (*(_vxfgpup9+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) * *(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg))));
Mark180:;
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
								
Mark190:;
								// continue
							}
														}						}
Mark200:;
						// continue
					}
										}				}
			}
			
		}
		//* 
		
		return;//* 
		//*     End of DGEMM . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
