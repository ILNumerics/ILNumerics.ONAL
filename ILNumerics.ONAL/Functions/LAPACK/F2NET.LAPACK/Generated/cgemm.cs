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
//*> \brief \b CGEMM 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CGEMM(TRANSA,TRANSB,M,N,K,ALPHA,A,LDA,B,LDB,BETA,C,LDC) 
//* 
//*       .. Scalar Arguments .. 
//*       COMPLEX ALPHA,BETA 
//*       INTEGER K,LDA,LDB,LDC,M,N 
//*       CHARACTER TRANSA,TRANSB 
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
//*> CGEMM  performs one of the matrix-matrix operations 
//*> 
//*>    C := alpha*op( A )*op( B ) + beta*C, 
//*> 
//*> where  op( X ) is one of 
//*> 
//*>    op( X ) = X   or   op( X ) = X**T   or   op( X ) = X**H, 
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
//*>              TRANSA = 'C' or 'c',  op( A ) = A**H. 
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
//*>              TRANSB = 'C' or 'c',  op( B ) = B**H. 
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
//*>          ALPHA is COMPLEX 
//*>           On entry, ALPHA specifies the scalar alpha. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is COMPLEX array, dimension ( LDA, ka ), where ka is 
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
//*>          B is COMPLEX array, dimension ( LDB, kb ), where kb is 
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
//*>          BETA is COMPLEX 
//*>           On entry,  BETA  specifies the scalar  beta.  When  BETA  is 
//*>           supplied as zero then C need not be set on input. 
//*> \endverbatim 
//*> 
//*> \param[in,out] C 
//*> \verbatim 
//*>          C is COMPLEX array, dimension ( LDC, N ) 
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
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _5p0w9905(FString _742vrzth, FString _30rlu6np, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, ref fcomplex _r7cfteg3, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, fcomplex* _p9n405a5, ref Int32 _ly9opahg, ref fcomplex _bafcbx97, fcomplex* _3crf0qn3, ref Int32 _1s3eymp4)
	{
#region variable declarations
fcomplex _1ajfmh55 =  default;
Int32 _b5p6od9s =  default;
Int32 _gro5yvfo =  default;
Int32 _znpjgsef =  default;
Int32 _68ec3gbh =  default;
Int32 _a7s478dp =  default;
Int32 _o9a6qdux =  default;
Int32 _80j3zf13 =  default;
Boolean _gzlxoamf =  default;
Boolean _i8jzem57 =  default;
Boolean _skucdj1b =  default;
Boolean _x18i1ben =  default;
fcomplex _kxg5drh2 =   new fcomplex(1f,0f);
fcomplex _d0547bi2 =   new fcomplex(0f,0f);
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
		//*     conjugated or transposed, set  CONJA and CONJB  as true if  A  and 
		//*     B  respectively are to be  transposed but  not conjugated  and set 
		//*     NROWA, NCOLA and  NROWB  as the number of rows and  columns  of  A 
		//*     and the number of rows of  B  respectively. 
		//* 
		
		_skucdj1b = _w8y2rzgy(_742vrzth ,"N" );
		_x18i1ben = _w8y2rzgy(_30rlu6np ,"N" );
		_gzlxoamf = _w8y2rzgy(_742vrzth ,"C" );
		_i8jzem57 = _w8y2rzgy(_30rlu6np ,"C" );
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
		if (((!(_skucdj1b)) & (!(_gzlxoamf))) & (!(_w8y2rzgy(_742vrzth ,"T" ))))
		{
			
			_gro5yvfo = (int)1;
		}
		else
		if (((!(_x18i1ben)) & (!(_i8jzem57))) & (!(_w8y2rzgy(_30rlu6np ,"T" ))))
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
			
			_ut9qalzx("CGEMM " ,ref _gro5yvfo );
			return;
		}
		//* 
		//*     Quick return if possible. 
		//* 
		
		if (((_ev4xhht5 == (int)0) | (_dxpq0xkr == (int)0)) | (((_r7cfteg3 == _d0547bi2) | (_umlkckdg == (int)0)) & (_bafcbx97 == _kxg5drh2)))
		return;//* 
		//*     And when  alpha.eq.zero. 
		//* 
		
		if (_r7cfteg3 == _d0547bi2)
		{
			
			if (_bafcbx97 == _d0547bi2)
			{
				
				{
					System.Int32 __81fgg2dlsvn93 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step93 = (System.Int32)((int)1);
					System.Int32 __81fgg2count93;
					for (__81fgg2count93 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn93 + __81fgg2step93) / __81fgg2step93)), _znpjgsef = __81fgg2dlsvn93; __81fgg2count93 != 0; __81fgg2count93--, _znpjgsef += (__81fgg2step93)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn94 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step94 = (System.Int32)((int)1);
							System.Int32 __81fgg2count94;
							for (__81fgg2count94 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn94 + __81fgg2step94) / __81fgg2step94)), _b5p6od9s = __81fgg2dlsvn94; __81fgg2count94 != 0; __81fgg2count94--, _b5p6od9s += (__81fgg2step94)) {

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
					System.Int32 __81fgg2dlsvn95 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step95 = (System.Int32)((int)1);
					System.Int32 __81fgg2count95;
					for (__81fgg2count95 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn95 + __81fgg2step95) / __81fgg2step95)), _znpjgsef = __81fgg2dlsvn95; __81fgg2count95 != 0; __81fgg2count95--, _znpjgsef += (__81fgg2step95)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn96 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step96 = (System.Int32)((int)1);
							System.Int32 __81fgg2count96;
							for (__81fgg2count96 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn96 + __81fgg2step96) / __81fgg2step96)), _b5p6od9s = __81fgg2dlsvn96; __81fgg2count96 != 0; __81fgg2count96--, _b5p6od9s += (__81fgg2step96)) {

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
					System.Int32 __81fgg2dlsvn97 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step97 = (System.Int32)((int)1);
					System.Int32 __81fgg2count97;
					for (__81fgg2count97 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn97 + __81fgg2step97) / __81fgg2step97)), _znpjgsef = __81fgg2dlsvn97; __81fgg2count97 != 0; __81fgg2count97--, _znpjgsef += (__81fgg2step97)) {

					{
						
						if (_bafcbx97 == _d0547bi2)
						{
							
							{
								System.Int32 __81fgg2dlsvn98 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step98 = (System.Int32)((int)1);
								System.Int32 __81fgg2count98;
								for (__81fgg2count98 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn98 + __81fgg2step98) / __81fgg2step98)), _b5p6od9s = __81fgg2dlsvn98; __81fgg2count98 != 0; __81fgg2count98--, _b5p6od9s += (__81fgg2step98)) {

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
								System.Int32 __81fgg2dlsvn99 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step99 = (System.Int32)((int)1);
								System.Int32 __81fgg2count99;
								for (__81fgg2count99 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn99 + __81fgg2step99) / __81fgg2step99)), _b5p6od9s = __81fgg2dlsvn99; __81fgg2count99 != 0; __81fgg2count99--, _b5p6od9s += (__81fgg2step99)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark60:;
									// continue
								}
																}							}
						}
						
						{
							System.Int32 __81fgg2dlsvn100 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step100 = (System.Int32)((int)1);
							System.Int32 __81fgg2count100;
							for (__81fgg2count100 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn100 + __81fgg2step100) / __81fgg2step100)), _68ec3gbh = __81fgg2dlsvn100; __81fgg2count100 != 0; __81fgg2count100--, _68ec3gbh += (__81fgg2step100)) {

							{
								
								_1ajfmh55 = (_r7cfteg3 * *(_p9n405a5+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
								{
									System.Int32 __81fgg2dlsvn101 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step101 = (System.Int32)((int)1);
									System.Int32 __81fgg2count101;
									for (__81fgg2count101 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn101 + __81fgg2step101) / __81fgg2step101)), _b5p6od9s = __81fgg2dlsvn101; __81fgg2count101 != 0; __81fgg2count101--, _b5p6od9s += (__81fgg2step101)) {

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
			if (_gzlxoamf)
			{
				//* 
				//*           Form  C := alpha*A**H*B + beta*C. 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn102 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step102 = (System.Int32)((int)1);
					System.Int32 __81fgg2count102;
					for (__81fgg2count102 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn102 + __81fgg2step102) / __81fgg2step102)), _znpjgsef = __81fgg2dlsvn102; __81fgg2count102 != 0; __81fgg2count102--, _znpjgsef += (__81fgg2step102)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn103 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step103 = (System.Int32)((int)1);
							System.Int32 __81fgg2count103;
							for (__81fgg2count103 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn103 + __81fgg2step103) / __81fgg2step103)), _b5p6od9s = __81fgg2dlsvn103; __81fgg2count103 != 0; __81fgg2count103--, _b5p6od9s += (__81fgg2step103)) {

							{
								
								_1ajfmh55 = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn104 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step104 = (System.Int32)((int)1);
									System.Int32 __81fgg2count104;
									for (__81fgg2count104 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn104 + __81fgg2step104) / __81fgg2step104)), _68ec3gbh = __81fgg2dlsvn104; __81fgg2count104 != 0; __81fgg2count104--, _68ec3gbh += (__81fgg2step104)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 + (ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ) * *(_p9n405a5+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
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
			else
			{
				//* 
				//*           Form  C := alpha*A**T*B + beta*C 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn105 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step105 = (System.Int32)((int)1);
					System.Int32 __81fgg2count105;
					for (__81fgg2count105 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn105 + __81fgg2step105) / __81fgg2step105)), _znpjgsef = __81fgg2dlsvn105; __81fgg2count105 != 0; __81fgg2count105--, _znpjgsef += (__81fgg2step105)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn106 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step106 = (System.Int32)((int)1);
							System.Int32 __81fgg2count106;
							for (__81fgg2count106 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn106 + __81fgg2step106) / __81fgg2step106)), _b5p6od9s = __81fgg2dlsvn106; __81fgg2count106 != 0; __81fgg2count106--, _b5p6od9s += (__81fgg2step106)) {

							{
								
								_1ajfmh55 = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn107 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step107 = (System.Int32)((int)1);
									System.Int32 __81fgg2count107;
									for (__81fgg2count107 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn107 + __81fgg2step107) / __81fgg2step107)), _68ec3gbh = __81fgg2dlsvn107; __81fgg2count107 != 0; __81fgg2count107--, _68ec3gbh += (__81fgg2step107)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 + (*(_vxfgpup9+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) * *(_p9n405a5+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
Mark130:;
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
								
Mark140:;
								// continue
							}
														}						}
Mark150:;
						// continue
					}
										}				}
			}
			
		}
		else
		if (_skucdj1b)
		{
			
			if (_i8jzem57)
			{
				//* 
				//*           Form  C := alpha*A*B**H + beta*C. 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn108 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step108 = (System.Int32)((int)1);
					System.Int32 __81fgg2count108;
					for (__81fgg2count108 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn108 + __81fgg2step108) / __81fgg2step108)), _znpjgsef = __81fgg2dlsvn108; __81fgg2count108 != 0; __81fgg2count108--, _znpjgsef += (__81fgg2step108)) {

					{
						
						if (_bafcbx97 == _d0547bi2)
						{
							
							{
								System.Int32 __81fgg2dlsvn109 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step109 = (System.Int32)((int)1);
								System.Int32 __81fgg2count109;
								for (__81fgg2count109 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn109 + __81fgg2step109) / __81fgg2step109)), _b5p6od9s = __81fgg2dlsvn109; __81fgg2count109 != 0; __81fgg2count109--, _b5p6od9s += (__81fgg2step109)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = _d0547bi2;
Mark160:;
									// continue
								}
																}							}
						}
						else
						if (_bafcbx97 != _kxg5drh2)
						{
							
							{
								System.Int32 __81fgg2dlsvn110 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step110 = (System.Int32)((int)1);
								System.Int32 __81fgg2count110;
								for (__81fgg2count110 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn110 + __81fgg2step110) / __81fgg2step110)), _b5p6od9s = __81fgg2dlsvn110; __81fgg2count110 != 0; __81fgg2count110--, _b5p6od9s += (__81fgg2step110)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark170:;
									// continue
								}
																}							}
						}
						
						{
							System.Int32 __81fgg2dlsvn111 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step111 = (System.Int32)((int)1);
							System.Int32 __81fgg2count111;
							for (__81fgg2count111 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn111 + __81fgg2step111) / __81fgg2step111)), _68ec3gbh = __81fgg2dlsvn111; __81fgg2count111 != 0; __81fgg2count111--, _68ec3gbh += (__81fgg2step111)) {

							{
								
								_1ajfmh55 = (_r7cfteg3 * ILNumerics.F2NET.Intrinsics.CONJG(*(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)) ));
								{
									System.Int32 __81fgg2dlsvn112 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step112 = (System.Int32)((int)1);
									System.Int32 __81fgg2count112;
									for (__81fgg2count112 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn112 + __81fgg2step112) / __81fgg2step112)), _b5p6od9s = __81fgg2dlsvn112; __81fgg2count112 != 0; __81fgg2count112--, _b5p6od9s += (__81fgg2step112)) {

									{
										
										*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) + (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c))));
Mark180:;
										// continue
									}
																		}								}
Mark190:;
								// continue
							}
														}						}
Mark200:;
						// continue
					}
										}				}
			}
			else
			{
				//* 
				//*           Form  C := alpha*A*B**T + beta*C 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn113 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step113 = (System.Int32)((int)1);
					System.Int32 __81fgg2count113;
					for (__81fgg2count113 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn113 + __81fgg2step113) / __81fgg2step113)), _znpjgsef = __81fgg2dlsvn113; __81fgg2count113 != 0; __81fgg2count113--, _znpjgsef += (__81fgg2step113)) {

					{
						
						if (_bafcbx97 == _d0547bi2)
						{
							
							{
								System.Int32 __81fgg2dlsvn114 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step114 = (System.Int32)((int)1);
								System.Int32 __81fgg2count114;
								for (__81fgg2count114 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn114 + __81fgg2step114) / __81fgg2step114)), _b5p6od9s = __81fgg2dlsvn114; __81fgg2count114 != 0; __81fgg2count114--, _b5p6od9s += (__81fgg2step114)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = _d0547bi2;
Mark210:;
									// continue
								}
																}							}
						}
						else
						if (_bafcbx97 != _kxg5drh2)
						{
							
							{
								System.Int32 __81fgg2dlsvn115 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step115 = (System.Int32)((int)1);
								System.Int32 __81fgg2count115;
								for (__81fgg2count115 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn115 + __81fgg2step115) / __81fgg2step115)), _b5p6od9s = __81fgg2dlsvn115; __81fgg2count115 != 0; __81fgg2count115--, _b5p6od9s += (__81fgg2step115)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark220:;
									// continue
								}
																}							}
						}
						
						{
							System.Int32 __81fgg2dlsvn116 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step116 = (System.Int32)((int)1);
							System.Int32 __81fgg2count116;
							for (__81fgg2count116 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn116 + __81fgg2step116) / __81fgg2step116)), _68ec3gbh = __81fgg2dlsvn116; __81fgg2count116 != 0; __81fgg2count116--, _68ec3gbh += (__81fgg2step116)) {

							{
								
								_1ajfmh55 = (_r7cfteg3 * *(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)));
								{
									System.Int32 __81fgg2dlsvn117 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step117 = (System.Int32)((int)1);
									System.Int32 __81fgg2count117;
									for (__81fgg2count117 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn117 + __81fgg2step117) / __81fgg2step117)), _b5p6od9s = __81fgg2dlsvn117; __81fgg2count117 != 0; __81fgg2count117--, _b5p6od9s += (__81fgg2step117)) {

									{
										
										*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) + (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c))));
Mark230:;
										// continue
									}
																		}								}
Mark240:;
								// continue
							}
														}						}
Mark250:;
						// continue
					}
										}				}
			}
			
		}
		else
		if (_gzlxoamf)
		{
			
			if (_i8jzem57)
			{
				//* 
				//*           Form  C := alpha*A**H*B**H + beta*C. 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn118 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step118 = (System.Int32)((int)1);
					System.Int32 __81fgg2count118;
					for (__81fgg2count118 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn118 + __81fgg2step118) / __81fgg2step118)), _znpjgsef = __81fgg2dlsvn118; __81fgg2count118 != 0; __81fgg2count118--, _znpjgsef += (__81fgg2step118)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn119 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step119 = (System.Int32)((int)1);
							System.Int32 __81fgg2count119;
							for (__81fgg2count119 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn119 + __81fgg2step119) / __81fgg2step119)), _b5p6od9s = __81fgg2dlsvn119; __81fgg2count119 != 0; __81fgg2count119--, _b5p6od9s += (__81fgg2step119)) {

							{
								
								_1ajfmh55 = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn120 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step120 = (System.Int32)((int)1);
									System.Int32 __81fgg2count120;
									for (__81fgg2count120 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn120 + __81fgg2step120) / __81fgg2step120)), _68ec3gbh = __81fgg2dlsvn120; __81fgg2count120 != 0; __81fgg2count120--, _68ec3gbh += (__81fgg2step120)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 + (ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ) * ILNumerics.F2NET.Intrinsics.CONJG(*(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)) )));
Mark260:;
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
								
Mark270:;
								// continue
							}
														}						}
Mark280:;
						// continue
					}
										}				}
			}
			else
			{
				//* 
				//*           Form  C := alpha*A**H*B**T + beta*C 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn121 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step121 = (System.Int32)((int)1);
					System.Int32 __81fgg2count121;
					for (__81fgg2count121 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn121 + __81fgg2step121) / __81fgg2step121)), _znpjgsef = __81fgg2dlsvn121; __81fgg2count121 != 0; __81fgg2count121--, _znpjgsef += (__81fgg2step121)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn122 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step122 = (System.Int32)((int)1);
							System.Int32 __81fgg2count122;
							for (__81fgg2count122 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn122 + __81fgg2step122) / __81fgg2step122)), _b5p6od9s = __81fgg2dlsvn122; __81fgg2count122 != 0; __81fgg2count122--, _b5p6od9s += (__81fgg2step122)) {

							{
								
								_1ajfmh55 = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn123 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step123 = (System.Int32)((int)1);
									System.Int32 __81fgg2count123;
									for (__81fgg2count123 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn123 + __81fgg2step123) / __81fgg2step123)), _68ec3gbh = __81fgg2dlsvn123; __81fgg2count123 != 0; __81fgg2count123--, _68ec3gbh += (__81fgg2step123)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 + (ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ) * *(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg))));
Mark290:;
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
								
Mark300:;
								// continue
							}
														}						}
Mark310:;
						// continue
					}
										}				}
			}
			
		}
		else
		{
			
			if (_i8jzem57)
			{
				//* 
				//*           Form  C := alpha*A**T*B**H + beta*C 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn124 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step124 = (System.Int32)((int)1);
					System.Int32 __81fgg2count124;
					for (__81fgg2count124 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn124 + __81fgg2step124) / __81fgg2step124)), _znpjgsef = __81fgg2dlsvn124; __81fgg2count124 != 0; __81fgg2count124--, _znpjgsef += (__81fgg2step124)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn125 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step125 = (System.Int32)((int)1);
							System.Int32 __81fgg2count125;
							for (__81fgg2count125 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn125 + __81fgg2step125) / __81fgg2step125)), _b5p6od9s = __81fgg2dlsvn125; __81fgg2count125 != 0; __81fgg2count125--, _b5p6od9s += (__81fgg2step125)) {

							{
								
								_1ajfmh55 = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn126 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step126 = (System.Int32)((int)1);
									System.Int32 __81fgg2count126;
									for (__81fgg2count126 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn126 + __81fgg2step126) / __81fgg2step126)), _68ec3gbh = __81fgg2dlsvn126; __81fgg2count126 != 0; __81fgg2count126--, _68ec3gbh += (__81fgg2step126)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 + (*(_vxfgpup9+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) * ILNumerics.F2NET.Intrinsics.CONJG(*(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)) )));
Mark320:;
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
								
Mark330:;
								// continue
							}
														}						}
Mark340:;
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
					System.Int32 __81fgg2dlsvn127 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step127 = (System.Int32)((int)1);
					System.Int32 __81fgg2count127;
					for (__81fgg2count127 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn127 + __81fgg2step127) / __81fgg2step127)), _znpjgsef = __81fgg2dlsvn127; __81fgg2count127 != 0; __81fgg2count127--, _znpjgsef += (__81fgg2step127)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn128 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step128 = (System.Int32)((int)1);
							System.Int32 __81fgg2count128;
							for (__81fgg2count128 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn128 + __81fgg2step128) / __81fgg2step128)), _b5p6od9s = __81fgg2dlsvn128; __81fgg2count128 != 0; __81fgg2count128--, _b5p6od9s += (__81fgg2step128)) {

							{
								
								_1ajfmh55 = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn129 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step129 = (System.Int32)((int)1);
									System.Int32 __81fgg2count129;
									for (__81fgg2count129 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn129 + __81fgg2step129) / __81fgg2step129)), _68ec3gbh = __81fgg2dlsvn129; __81fgg2count129 != 0; __81fgg2count129--, _68ec3gbh += (__81fgg2step129)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 + (*(_vxfgpup9+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) * *(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg))));
Mark350:;
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
								
Mark360:;
								// continue
							}
														}						}
Mark370:;
						// continue
					}
										}				}
			}
			
		}
		//* 
		
		return;//* 
		//*     End of CGEMM . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
