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
//*> \brief \b ZTRMM 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZTRMM(SIDE,UPLO,TRANSA,DIAG,M,N,ALPHA,A,LDA,B,LDB) 
//* 
//*       .. Scalar Arguments .. 
//*       COMPLEX*16 ALPHA 
//*       INTEGER LDA,LDB,M,N 
//*       CHARACTER DIAG,SIDE,TRANSA,UPLO 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX*16 A(LDA,*),B(LDB,*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZTRMM  performs one of the matrix-matrix operations 
//*> 
//*>    B := alpha*op( A )*B,   or   B := alpha*B*op( A ) 
//*> 
//*> where  alpha  is a scalar,  B  is an m by n matrix,  A  is a unit, or 
//*> non-unit,  upper or lower triangular matrix  and  op( A )  is one  of 
//*> 
//*>    op( A ) = A   or   op( A ) = A**T   or   op( A ) = A**H. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] SIDE 
//*> \verbatim 
//*>          SIDE is CHARACTER*1 
//*>           On entry,  SIDE specifies whether  op( A ) multiplies B from 
//*>           the left or right as follows: 
//*> 
//*>              SIDE = 'L' or 'l'   B := alpha*op( A )*B. 
//*> 
//*>              SIDE = 'R' or 'r'   B := alpha*B*op( A ). 
//*> \endverbatim 
//*> 
//*> \param[in] UPLO 
//*> \verbatim 
//*>          UPLO is CHARACTER*1 
//*>           On entry, UPLO specifies whether the matrix A is an upper or 
//*>           lower triangular matrix as follows: 
//*> 
//*>              UPLO = 'U' or 'u'   A is an upper triangular matrix. 
//*> 
//*>              UPLO = 'L' or 'l'   A is a lower triangular matrix. 
//*> \endverbatim 
//*> 
//*> \param[in] TRANSA 
//*> \verbatim 
//*>          TRANSA is CHARACTER*1 
//*>           On entry, TRANSA specifies the form of op( A ) to be used in 
//*>           the matrix multiplication as follows: 
//*> 
//*>              TRANSA = 'N' or 'n'   op( A ) = A. 
//*> 
//*>              TRANSA = 'T' or 't'   op( A ) = A**T. 
//*> 
//*>              TRANSA = 'C' or 'c'   op( A ) = A**H. 
//*> \endverbatim 
//*> 
//*> \param[in] DIAG 
//*> \verbatim 
//*>          DIAG is CHARACTER*1 
//*>           On entry, DIAG specifies whether or not A is unit triangular 
//*>           as follows: 
//*> 
//*>              DIAG = 'U' or 'u'   A is assumed to be unit triangular. 
//*> 
//*>              DIAG = 'N' or 'n'   A is not assumed to be unit 
//*>                                  triangular. 
//*> \endverbatim 
//*> 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>           On entry, M specifies the number of rows of B. M must be at 
//*>           least zero. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>           On entry, N specifies the number of columns of B.  N must be 
//*>           at least zero. 
//*> \endverbatim 
//*> 
//*> \param[in] ALPHA 
//*> \verbatim 
//*>          ALPHA is COMPLEX*16 
//*>           On entry,  ALPHA specifies the scalar  alpha. When  alpha is 
//*>           zero then  A is not referenced and  B need not be set before 
//*>           entry. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is COMPLEX*16 array, dimension ( LDA, k ), where k is m 
//*>           when  SIDE = 'L' or 'l'  and is  n  when  SIDE = 'R' or 'r'. 
//*>           Before entry  with  UPLO = 'U' or 'u',  the  leading  k by k 
//*>           upper triangular part of the array  A must contain the upper 
//*>           triangular matrix  and the strictly lower triangular part of 
//*>           A is not referenced. 
//*>           Before entry  with  UPLO = 'L' or 'l',  the  leading  k by k 
//*>           lower triangular part of the array  A must contain the lower 
//*>           triangular matrix  and the strictly upper triangular part of 
//*>           A is not referenced. 
//*>           Note that when  DIAG = 'U' or 'u',  the diagonal elements of 
//*>           A  are not referenced either,  but are assumed to be  unity. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>           On entry, LDA specifies the first dimension of A as declared 
//*>           in the calling (sub) program.  When  SIDE = 'L' or 'l'  then 
//*>           LDA  must be at least  max( 1, m ),  when  SIDE = 'R' or 'r' 
//*>           then LDA must be at least max( 1, n ). 
//*> \endverbatim 
//*> 
//*> \param[in,out] B 
//*> \verbatim 
//*>          B is COMPLEX*16 array, dimension ( LDB, N ). 
//*>           Before entry,  the leading  m by n part of the array  B must 
//*>           contain the matrix  B,  and  on exit  is overwritten  by the 
//*>           transformed matrix. 
//*> \endverbatim 
//*> 
//*> \param[in] LDB 
//*> \verbatim 
//*>          LDB is INTEGER 
//*>           On entry, LDB specifies the first dimension of B as declared 
//*>           in  the  calling  (sub)  program.   LDB  must  be  at  least 
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
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _dbxixtiz(FString _m2cn2gjg, FString _9wyre9zc, FString _742vrzth, FString _2scffxp3, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref complex _r7cfteg3, complex* _vxfgpup9, ref Int32 _ocv8fk5c, complex* _p9n405a5, ref Int32 _ly9opahg)
	{
#region variable declarations
complex _1ajfmh55 =  default;
Int32 _b5p6od9s =  default;
Int32 _gro5yvfo =  default;
Int32 _znpjgsef =  default;
Int32 _umlkckdg =  default;
Int32 _o9a6qdux =  default;
Boolean _wln8whqb =  default;
Boolean _moml4lap =  default;
Boolean _rcjmgxm4 =  default;
Boolean _l08igmvf =  default;
complex _kxg5drh2 =   new fcomplex(1f,0f);
complex _d0547bi2 =   new fcomplex(0f,0f);
string fLanavab = default;
#endregion  variable declarations
_m2cn2gjg = _m2cn2gjg.Convert(1);
_9wyre9zc = _9wyre9zc.Convert(1);
_742vrzth = _742vrzth.Convert(1);
_2scffxp3 = _2scffxp3.Convert(1);

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
		
		_wln8whqb = _w8y2rzgy(_m2cn2gjg ,"L" );
		if (_wln8whqb)
		{
			
			_o9a6qdux = _ev4xhht5;
		}
		else
		{
			
			_o9a6qdux = _dxpq0xkr;
		}
		
		_moml4lap = _w8y2rzgy(_742vrzth ,"T" );
		_rcjmgxm4 = _w8y2rzgy(_2scffxp3 ,"N" );
		_l08igmvf = _w8y2rzgy(_9wyre9zc ,"U" );//* 
		
		_gro5yvfo = (int)0;
		if ((!(_wln8whqb)) & (!(_w8y2rzgy(_m2cn2gjg ,"R" ))))
		{
			
			_gro5yvfo = (int)1;
		}
		else
		if ((!(_l08igmvf)) & (!(_w8y2rzgy(_9wyre9zc ,"L" ))))
		{
			
			_gro5yvfo = (int)2;
		}
		else
		if (((!(_w8y2rzgy(_742vrzth ,"N" ))) & (!(_w8y2rzgy(_742vrzth ,"T" )))) & (!(_w8y2rzgy(_742vrzth ,"C" ))))
		{
			
			_gro5yvfo = (int)3;
		}
		else
		if ((!(_w8y2rzgy(_2scffxp3 ,"U" ))) & (!(_w8y2rzgy(_2scffxp3 ,"N" ))))
		{
			
			_gro5yvfo = (int)4;
		}
		else
		if (_ev4xhht5 < (int)0)
		{
			
			_gro5yvfo = (int)5;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)6;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_o9a6qdux ))
		{
			
			_gro5yvfo = (int)9;
		}
		else
		if (_ly9opahg < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_ev4xhht5 ))
		{
			
			_gro5yvfo = (int)11;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("ZTRMM " ,ref _gro5yvfo );
			return;
		}
		//* 
		//*     Quick return if possible. 
		//* 
		
		if ((_ev4xhht5 == (int)0) | (_dxpq0xkr == (int)0))
		return;//* 
		//*     And when  alpha.eq.zero. 
		//* 
		
		if (_r7cfteg3 == _d0547bi2)
		{
			
			{
				System.Int32 __81fgg2dlsvn1199 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1199 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1199;
				for (__81fgg2count1199 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1199 + __81fgg2step1199) / __81fgg2step1199)), _znpjgsef = __81fgg2dlsvn1199; __81fgg2count1199 != 0; __81fgg2count1199--, _znpjgsef += (__81fgg2step1199)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn1200 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1200 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1200;
						for (__81fgg2count1200 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1200 + __81fgg2step1200) / __81fgg2step1200)), _b5p6od9s = __81fgg2dlsvn1200; __81fgg2count1200 != 0; __81fgg2count1200--, _b5p6od9s += (__81fgg2step1200)) {

						{
							
							*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = _d0547bi2;
Mark10:;
							// continue
						}
												}					}
Mark20:;
					// continue
				}
								}			}
			return;
		}
		//* 
		//*     Start the operations. 
		//* 
		
		if (_wln8whqb)
		{
			
			if (_w8y2rzgy(_742vrzth ,"N" ))
			{
				//* 
				//*           Form  B := alpha*A*B. 
				//* 
				
				if (_l08igmvf)
				{
					
					{
						System.Int32 __81fgg2dlsvn1201 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1201 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1201;
						for (__81fgg2count1201 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1201 + __81fgg2step1201) / __81fgg2step1201)), _znpjgsef = __81fgg2dlsvn1201; __81fgg2count1201 != 0; __81fgg2count1201--, _znpjgsef += (__81fgg2step1201)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1202 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1202 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1202;
								for (__81fgg2count1202 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1202 + __81fgg2step1202) / __81fgg2step1202)), _umlkckdg = __81fgg2dlsvn1202; __81fgg2count1202 != 0; __81fgg2count1202--, _umlkckdg += (__81fgg2step1202)) {

								{
									
									if (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) != _d0547bi2)
									{
										
										_1ajfmh55 = (_r7cfteg3 * *(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
										{
											System.Int32 __81fgg2dlsvn1203 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1203 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1203;
											for (__81fgg2count1203 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg - (int)1) - __81fgg2dlsvn1203 + __81fgg2step1203) / __81fgg2step1203)), _b5p6od9s = __81fgg2dlsvn1203; __81fgg2count1203 != 0; __81fgg2count1203--, _b5p6od9s += (__81fgg2step1203)) {

											{
												
												*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) + (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c))));
Mark30:;
												// continue
											}
																						}										}
										if (_rcjmgxm4)
										_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
										*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = _1ajfmh55;
									}
									
Mark40:;
									// continue
								}
																}							}
Mark50:;
							// continue
						}
												}					}
				}
				else
				{
					
					{
						System.Int32 __81fgg2dlsvn1204 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1204 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1204;
						for (__81fgg2count1204 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1204 + __81fgg2step1204) / __81fgg2step1204)), _znpjgsef = __81fgg2dlsvn1204; __81fgg2count1204 != 0; __81fgg2count1204--, _znpjgsef += (__81fgg2step1204)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1205 = (System.Int32)(_ev4xhht5);
								System.Int32 __81fgg2step1205 = (System.Int32)((int)-1);
								System.Int32 __81fgg2count1205;
								for (__81fgg2count1205 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1205 + __81fgg2step1205) / __81fgg2step1205)), _umlkckdg = __81fgg2dlsvn1205; __81fgg2count1205 != 0; __81fgg2count1205--, _umlkckdg += (__81fgg2step1205)) {

								{
									
									if (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) != _d0547bi2)
									{
										
										_1ajfmh55 = (_r7cfteg3 * *(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
										*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = _1ajfmh55;
										if (_rcjmgxm4)
										*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) * *(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
										{
											System.Int32 __81fgg2dlsvn1206 = (System.Int32)((_umlkckdg + (int)1));
											const System.Int32 __81fgg2step1206 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1206;
											for (__81fgg2count1206 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1206 + __81fgg2step1206) / __81fgg2step1206)), _b5p6od9s = __81fgg2dlsvn1206; __81fgg2count1206 != 0; __81fgg2count1206--, _b5p6od9s += (__81fgg2step1206)) {

											{
												
												*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) + (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c))));
Mark60:;
												// continue
											}
																						}										}
									}
									
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
			else
			{
				//* 
				//*           Form  B := alpha*A**T*B   or   B := alpha*A**H*B. 
				//* 
				
				if (_l08igmvf)
				{
					
					{
						System.Int32 __81fgg2dlsvn1207 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1207 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1207;
						for (__81fgg2count1207 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1207 + __81fgg2step1207) / __81fgg2step1207)), _znpjgsef = __81fgg2dlsvn1207; __81fgg2count1207 != 0; __81fgg2count1207--, _znpjgsef += (__81fgg2step1207)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1208 = (System.Int32)(_ev4xhht5);
								System.Int32 __81fgg2step1208 = (System.Int32)((int)-1);
								System.Int32 __81fgg2count1208;
								for (__81fgg2count1208 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1208 + __81fgg2step1208) / __81fgg2step1208)), _b5p6od9s = __81fgg2dlsvn1208; __81fgg2count1208 != 0; __81fgg2count1208--, _b5p6od9s += (__81fgg2step1208)) {

								{
									
									_1ajfmh55 = *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg));
									if (_moml4lap)
									{
										
										if (_rcjmgxm4)
										_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)));
										{
											System.Int32 __81fgg2dlsvn1209 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1209 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1209;
											for (__81fgg2count1209 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s - (int)1) - __81fgg2dlsvn1209 + __81fgg2step1209) / __81fgg2step1209)), _umlkckdg = __81fgg2dlsvn1209; __81fgg2count1209 != 0; __81fgg2count1209--, _umlkckdg += (__81fgg2step1209)) {

											{
												
												_1ajfmh55 = (_1ajfmh55 + (*(_vxfgpup9+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) * *(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
Mark90:;
												// continue
											}
																						}										}
									}
									else
									{
										
										if (_rcjmgxm4)
										_1ajfmh55 = (_1ajfmh55 * ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ));
										{
											System.Int32 __81fgg2dlsvn1210 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1210 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1210;
											for (__81fgg2count1210 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s - (int)1) - __81fgg2dlsvn1210 + __81fgg2step1210) / __81fgg2step1210)), _umlkckdg = __81fgg2dlsvn1210; __81fgg2count1210 != 0; __81fgg2count1210--, _umlkckdg += (__81fgg2step1210)) {

											{
												
												_1ajfmh55 = (_1ajfmh55 + (ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ) * *(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
Mark100:;
												// continue
											}
																						}										}
									}
									
									*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_r7cfteg3 * _1ajfmh55);
Mark110:;
									// continue
								}
																}							}
Mark120:;
							// continue
						}
												}					}
				}
				else
				{
					
					{
						System.Int32 __81fgg2dlsvn1211 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1211 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1211;
						for (__81fgg2count1211 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1211 + __81fgg2step1211) / __81fgg2step1211)), _znpjgsef = __81fgg2dlsvn1211; __81fgg2count1211 != 0; __81fgg2count1211--, _znpjgsef += (__81fgg2step1211)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1212 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1212 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1212;
								for (__81fgg2count1212 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1212 + __81fgg2step1212) / __81fgg2step1212)), _b5p6od9s = __81fgg2dlsvn1212; __81fgg2count1212 != 0; __81fgg2count1212--, _b5p6od9s += (__81fgg2step1212)) {

								{
									
									_1ajfmh55 = *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg));
									if (_moml4lap)
									{
										
										if (_rcjmgxm4)
										_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)));
										{
											System.Int32 __81fgg2dlsvn1213 = (System.Int32)((_b5p6od9s + (int)1));
											const System.Int32 __81fgg2step1213 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1213;
											for (__81fgg2count1213 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1213 + __81fgg2step1213) / __81fgg2step1213)), _umlkckdg = __81fgg2dlsvn1213; __81fgg2count1213 != 0; __81fgg2count1213--, _umlkckdg += (__81fgg2step1213)) {

											{
												
												_1ajfmh55 = (_1ajfmh55 + (*(_vxfgpup9+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) * *(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
Mark130:;
												// continue
											}
																						}										}
									}
									else
									{
										
										if (_rcjmgxm4)
										_1ajfmh55 = (_1ajfmh55 * ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ));
										{
											System.Int32 __81fgg2dlsvn1214 = (System.Int32)((_b5p6od9s + (int)1));
											const System.Int32 __81fgg2step1214 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1214;
											for (__81fgg2count1214 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1214 + __81fgg2step1214) / __81fgg2step1214)), _umlkckdg = __81fgg2dlsvn1214; __81fgg2count1214 != 0; __81fgg2count1214--, _umlkckdg += (__81fgg2step1214)) {

											{
												
												_1ajfmh55 = (_1ajfmh55 + (ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ) * *(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
Mark140:;
												// continue
											}
																						}										}
									}
									
									*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_r7cfteg3 * _1ajfmh55);
Mark150:;
									// continue
								}
																}							}
Mark160:;
							// continue
						}
												}					}
				}
				
			}
			
		}
		else
		{
			
			if (_w8y2rzgy(_742vrzth ,"N" ))
			{
				//* 
				//*           Form  B := alpha*B*A. 
				//* 
				
				if (_l08igmvf)
				{
					
					{
						System.Int32 __81fgg2dlsvn1215 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step1215 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count1215;
						for (__81fgg2count1215 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1215 + __81fgg2step1215) / __81fgg2step1215)), _znpjgsef = __81fgg2dlsvn1215; __81fgg2count1215 != 0; __81fgg2count1215--, _znpjgsef += (__81fgg2step1215)) {

						{
							
							_1ajfmh55 = _r7cfteg3;
							if (_rcjmgxm4)
							_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							{
								System.Int32 __81fgg2dlsvn1216 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1216 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1216;
								for (__81fgg2count1216 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1216 + __81fgg2step1216) / __81fgg2step1216)), _b5p6od9s = __81fgg2dlsvn1216; __81fgg2count1216 != 0; __81fgg2count1216--, _b5p6od9s += (__81fgg2step1216)) {

								{
									
									*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
Mark170:;
									// continue
								}
																}							}
							{
								System.Int32 __81fgg2dlsvn1217 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1217 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1217;
								for (__81fgg2count1217 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn1217 + __81fgg2step1217) / __81fgg2step1217)), _umlkckdg = __81fgg2dlsvn1217; __81fgg2count1217 != 0; __81fgg2count1217--, _umlkckdg += (__81fgg2step1217)) {

								{
									
									if (*(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										_1ajfmh55 = (_r7cfteg3 * *(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
										{
											System.Int32 __81fgg2dlsvn1218 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1218 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1218;
											for (__81fgg2count1218 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1218 + __81fgg2step1218) / __81fgg2step1218)), _b5p6od9s = __81fgg2dlsvn1218; __81fgg2count1218 != 0; __81fgg2count1218--, _b5p6od9s += (__81fgg2step1218)) {

											{
												
												*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) + (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg))));
Mark180:;
												// continue
											}
																						}										}
									}
									
Mark190:;
									// continue
								}
																}							}
Mark200:;
							// continue
						}
												}					}
				}
				else
				{
					
					{
						System.Int32 __81fgg2dlsvn1219 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1219 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1219;
						for (__81fgg2count1219 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1219 + __81fgg2step1219) / __81fgg2step1219)), _znpjgsef = __81fgg2dlsvn1219; __81fgg2count1219 != 0; __81fgg2count1219--, _znpjgsef += (__81fgg2step1219)) {

						{
							
							_1ajfmh55 = _r7cfteg3;
							if (_rcjmgxm4)
							_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							{
								System.Int32 __81fgg2dlsvn1220 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1220 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1220;
								for (__81fgg2count1220 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1220 + __81fgg2step1220) / __81fgg2step1220)), _b5p6od9s = __81fgg2dlsvn1220; __81fgg2count1220 != 0; __81fgg2count1220--, _b5p6od9s += (__81fgg2step1220)) {

								{
									
									*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
Mark210:;
									// continue
								}
																}							}
							{
								System.Int32 __81fgg2dlsvn1221 = (System.Int32)((_znpjgsef + (int)1));
								const System.Int32 __81fgg2step1221 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1221;
								for (__81fgg2count1221 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1221 + __81fgg2step1221) / __81fgg2step1221)), _umlkckdg = __81fgg2dlsvn1221; __81fgg2count1221 != 0; __81fgg2count1221--, _umlkckdg += (__81fgg2step1221)) {

								{
									
									if (*(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										_1ajfmh55 = (_r7cfteg3 * *(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
										{
											System.Int32 __81fgg2dlsvn1222 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1222 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1222;
											for (__81fgg2count1222 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1222 + __81fgg2step1222) / __81fgg2step1222)), _b5p6od9s = __81fgg2dlsvn1222; __81fgg2count1222 != 0; __81fgg2count1222--, _b5p6od9s += (__81fgg2step1222)) {

											{
												
												*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) + (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg))));
Mark220:;
												// continue
											}
																						}										}
									}
									
Mark230:;
									// continue
								}
																}							}
Mark240:;
							// continue
						}
												}					}
				}
				
			}
			else
			{
				//* 
				//*           Form  B := alpha*B*A**T   or   B := alpha*B*A**H. 
				//* 
				
				if (_l08igmvf)
				{
					
					{
						System.Int32 __81fgg2dlsvn1223 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1223 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1223;
						for (__81fgg2count1223 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1223 + __81fgg2step1223) / __81fgg2step1223)), _umlkckdg = __81fgg2dlsvn1223; __81fgg2count1223 != 0; __81fgg2count1223--, _umlkckdg += (__81fgg2step1223)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1224 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1224 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1224;
								for (__81fgg2count1224 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg - (int)1) - __81fgg2dlsvn1224 + __81fgg2step1224) / __81fgg2step1224)), _znpjgsef = __81fgg2dlsvn1224; __81fgg2count1224 != 0; __81fgg2count1224--, _znpjgsef += (__81fgg2step1224)) {

								{
									
									if (*(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										if (_moml4lap)
										{
											
											_1ajfmh55 = (_r7cfteg3 * *(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
										}
										else
										{
											
											_1ajfmh55 = (_r7cfteg3 * ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) ));
										}
										
										{
											System.Int32 __81fgg2dlsvn1225 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1225 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1225;
											for (__81fgg2count1225 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1225 + __81fgg2step1225) / __81fgg2step1225)), _b5p6od9s = __81fgg2dlsvn1225; __81fgg2count1225 != 0; __81fgg2count1225--, _b5p6od9s += (__81fgg2step1225)) {

											{
												
												*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) + (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg))));
Mark250:;
												// continue
											}
																						}										}
									}
									
Mark260:;
									// continue
								}
																}							}
							_1ajfmh55 = _r7cfteg3;
							if (_rcjmgxm4)
							{
								
								if (_moml4lap)
								{
									
									_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
								}
								else
								{
									
									_1ajfmh55 = (_1ajfmh55 * ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) ));
								}
								
							}
							
							if (_1ajfmh55 != _kxg5drh2)
							{
								
								{
									System.Int32 __81fgg2dlsvn1226 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1226 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1226;
									for (__81fgg2count1226 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1226 + __81fgg2step1226) / __81fgg2step1226)), _b5p6od9s = __81fgg2dlsvn1226; __81fgg2count1226 != 0; __81fgg2count1226--, _b5p6od9s += (__81fgg2step1226)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)) = (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)));
Mark270:;
										// continue
									}
																		}								}
							}
							
Mark280:;
							// continue
						}
												}					}
				}
				else
				{
					
					{
						System.Int32 __81fgg2dlsvn1227 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step1227 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count1227;
						for (__81fgg2count1227 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1227 + __81fgg2step1227) / __81fgg2step1227)), _umlkckdg = __81fgg2dlsvn1227; __81fgg2count1227 != 0; __81fgg2count1227--, _umlkckdg += (__81fgg2step1227)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1228 = (System.Int32)((_umlkckdg + (int)1));
								const System.Int32 __81fgg2step1228 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1228;
								for (__81fgg2count1228 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1228 + __81fgg2step1228) / __81fgg2step1228)), _znpjgsef = __81fgg2dlsvn1228; __81fgg2count1228 != 0; __81fgg2count1228--, _znpjgsef += (__81fgg2step1228)) {

								{
									
									if (*(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										if (_moml4lap)
										{
											
											_1ajfmh55 = (_r7cfteg3 * *(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
										}
										else
										{
											
											_1ajfmh55 = (_r7cfteg3 * ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) ));
										}
										
										{
											System.Int32 __81fgg2dlsvn1229 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1229 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1229;
											for (__81fgg2count1229 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1229 + __81fgg2step1229) / __81fgg2step1229)), _b5p6od9s = __81fgg2dlsvn1229; __81fgg2count1229 != 0; __81fgg2count1229--, _b5p6od9s += (__81fgg2step1229)) {

											{
												
												*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) + (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg))));
Mark290:;
												// continue
											}
																						}										}
									}
									
Mark300:;
									// continue
								}
																}							}
							_1ajfmh55 = _r7cfteg3;
							if (_rcjmgxm4)
							{
								
								if (_moml4lap)
								{
									
									_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
								}
								else
								{
									
									_1ajfmh55 = (_1ajfmh55 * ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) ));
								}
								
							}
							
							if (_1ajfmh55 != _kxg5drh2)
							{
								
								{
									System.Int32 __81fgg2dlsvn1230 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1230 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1230;
									for (__81fgg2count1230 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1230 + __81fgg2step1230) / __81fgg2step1230)), _b5p6od9s = __81fgg2dlsvn1230; __81fgg2count1230 != 0; __81fgg2count1230--, _b5p6od9s += (__81fgg2step1230)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)) = (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)));
Mark310:;
										// continue
									}
																		}								}
							}
							
Mark320:;
							// continue
						}
												}					}
				}
				
			}
			
		}
		//* 
		
		return;//* 
		//*     End of ZTRMM . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
