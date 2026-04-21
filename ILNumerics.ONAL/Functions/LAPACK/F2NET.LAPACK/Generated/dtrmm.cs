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
//*> \brief \b DTRMM 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DTRMM(SIDE,UPLO,TRANSA,DIAG,M,N,ALPHA,A,LDA,B,LDB) 
//* 
//*       .. Scalar Arguments .. 
//*       DOUBLE PRECISION ALPHA 
//*       INTEGER LDA,LDB,M,N 
//*       CHARACTER DIAG,SIDE,TRANSA,UPLO 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION A(LDA,*),B(LDB,*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DTRMM  performs one of the matrix-matrix operations 
//*> 
//*>    B := alpha*op( A )*B,   or   B := alpha*B*op( A ), 
//*> 
//*> where  alpha  is a scalar,  B  is an m by n matrix,  A  is a unit, or 
//*> non-unit,  upper or lower triangular matrix  and  op( A )  is one  of 
//*> 
//*>    op( A ) = A   or   op( A ) = A**T. 
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
//*>              TRANSA = 'C' or 'c'   op( A ) = A**T. 
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
//*>          ALPHA is DOUBLE PRECISION. 
//*>           On entry,  ALPHA specifies the scalar  alpha. When  alpha is 
//*>           zero then  A is not referenced and  B need not be set before 
//*>           entry. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>           A is DOUBLE PRECISION array, dimension ( LDA, k ), where k is m 
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
//*>          B is DOUBLE PRECISION array, dimension ( LDB, N ) 
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

	 
	public static void _birntqim(FString _m2cn2gjg, FString _9wyre9zc, FString _742vrzth, FString _2scffxp3, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Double _r7cfteg3, Double* _vxfgpup9, ref Int32 _ocv8fk5c, Double* _p9n405a5, ref Int32 _ly9opahg)
	{
#region variable declarations
Double _1ajfmh55 =  default;
Int32 _b5p6od9s =  default;
Int32 _gro5yvfo =  default;
Int32 _znpjgsef =  default;
Int32 _umlkckdg =  default;
Int32 _o9a6qdux =  default;
Boolean _wln8whqb =  default;
Boolean _rcjmgxm4 =  default;
Boolean _l08igmvf =  default;
Double _kxg5drh2 =  1d;
Double _d0547bi2 =  0d;
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
			
			_ut9qalzx("DTRMM " ,ref _gro5yvfo );
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
				System.Int32 __81fgg2dlsvn440 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step440 = (System.Int32)((int)1);
				System.Int32 __81fgg2count440;
				for (__81fgg2count440 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn440 + __81fgg2step440) / __81fgg2step440)), _znpjgsef = __81fgg2dlsvn440; __81fgg2count440 != 0; __81fgg2count440--, _znpjgsef += (__81fgg2step440)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn441 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step441 = (System.Int32)((int)1);
						System.Int32 __81fgg2count441;
						for (__81fgg2count441 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn441 + __81fgg2step441) / __81fgg2step441)), _b5p6od9s = __81fgg2dlsvn441; __81fgg2count441 != 0; __81fgg2count441--, _b5p6od9s += (__81fgg2step441)) {

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
						System.Int32 __81fgg2dlsvn442 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step442 = (System.Int32)((int)1);
						System.Int32 __81fgg2count442;
						for (__81fgg2count442 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn442 + __81fgg2step442) / __81fgg2step442)), _znpjgsef = __81fgg2dlsvn442; __81fgg2count442 != 0; __81fgg2count442--, _znpjgsef += (__81fgg2step442)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn443 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step443 = (System.Int32)((int)1);
								System.Int32 __81fgg2count443;
								for (__81fgg2count443 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn443 + __81fgg2step443) / __81fgg2step443)), _umlkckdg = __81fgg2dlsvn443; __81fgg2count443 != 0; __81fgg2count443--, _umlkckdg += (__81fgg2step443)) {

								{
									
									if (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) != _d0547bi2)
									{
										
										_1ajfmh55 = (_r7cfteg3 * *(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
										{
											System.Int32 __81fgg2dlsvn444 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step444 = (System.Int32)((int)1);
											System.Int32 __81fgg2count444;
											for (__81fgg2count444 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg - (int)1) - __81fgg2dlsvn444 + __81fgg2step444) / __81fgg2step444)), _b5p6od9s = __81fgg2dlsvn444; __81fgg2count444 != 0; __81fgg2count444--, _b5p6od9s += (__81fgg2step444)) {

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
						System.Int32 __81fgg2dlsvn445 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step445 = (System.Int32)((int)1);
						System.Int32 __81fgg2count445;
						for (__81fgg2count445 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn445 + __81fgg2step445) / __81fgg2step445)), _znpjgsef = __81fgg2dlsvn445; __81fgg2count445 != 0; __81fgg2count445--, _znpjgsef += (__81fgg2step445)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn446 = (System.Int32)(_ev4xhht5);
								System.Int32 __81fgg2step446 = (System.Int32)((int)-1);
								System.Int32 __81fgg2count446;
								for (__81fgg2count446 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn446 + __81fgg2step446) / __81fgg2step446)), _umlkckdg = __81fgg2dlsvn446; __81fgg2count446 != 0; __81fgg2count446--, _umlkckdg += (__81fgg2step446)) {

								{
									
									if (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) != _d0547bi2)
									{
										
										_1ajfmh55 = (_r7cfteg3 * *(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
										*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = _1ajfmh55;
										if (_rcjmgxm4)
										*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) * *(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
										{
											System.Int32 __81fgg2dlsvn447 = (System.Int32)((_umlkckdg + (int)1));
											const System.Int32 __81fgg2step447 = (System.Int32)((int)1);
											System.Int32 __81fgg2count447;
											for (__81fgg2count447 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn447 + __81fgg2step447) / __81fgg2step447)), _b5p6od9s = __81fgg2dlsvn447; __81fgg2count447 != 0; __81fgg2count447--, _b5p6od9s += (__81fgg2step447)) {

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
				//*           Form  B := alpha*A**T*B. 
				//* 
				
				if (_l08igmvf)
				{
					
					{
						System.Int32 __81fgg2dlsvn448 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step448 = (System.Int32)((int)1);
						System.Int32 __81fgg2count448;
						for (__81fgg2count448 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn448 + __81fgg2step448) / __81fgg2step448)), _znpjgsef = __81fgg2dlsvn448; __81fgg2count448 != 0; __81fgg2count448--, _znpjgsef += (__81fgg2step448)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn449 = (System.Int32)(_ev4xhht5);
								System.Int32 __81fgg2step449 = (System.Int32)((int)-1);
								System.Int32 __81fgg2count449;
								for (__81fgg2count449 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn449 + __81fgg2step449) / __81fgg2step449)), _b5p6od9s = __81fgg2dlsvn449; __81fgg2count449 != 0; __81fgg2count449--, _b5p6od9s += (__81fgg2step449)) {

								{
									
									_1ajfmh55 = *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg));
									if (_rcjmgxm4)
									_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)));
									{
										System.Int32 __81fgg2dlsvn450 = (System.Int32)((int)1);
										const System.Int32 __81fgg2step450 = (System.Int32)((int)1);
										System.Int32 __81fgg2count450;
										for (__81fgg2count450 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s - (int)1) - __81fgg2dlsvn450 + __81fgg2step450) / __81fgg2step450)), _umlkckdg = __81fgg2dlsvn450; __81fgg2count450 != 0; __81fgg2count450--, _umlkckdg += (__81fgg2step450)) {

										{
											
											_1ajfmh55 = (_1ajfmh55 + (*(_vxfgpup9+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) * *(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
Mark90:;
											// continue
										}
																				}									}
									*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_r7cfteg3 * _1ajfmh55);
Mark100:;
									// continue
								}
																}							}
Mark110:;
							// continue
						}
												}					}
				}
				else
				{
					
					{
						System.Int32 __81fgg2dlsvn451 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step451 = (System.Int32)((int)1);
						System.Int32 __81fgg2count451;
						for (__81fgg2count451 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn451 + __81fgg2step451) / __81fgg2step451)), _znpjgsef = __81fgg2dlsvn451; __81fgg2count451 != 0; __81fgg2count451--, _znpjgsef += (__81fgg2step451)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn452 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step452 = (System.Int32)((int)1);
								System.Int32 __81fgg2count452;
								for (__81fgg2count452 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn452 + __81fgg2step452) / __81fgg2step452)), _b5p6od9s = __81fgg2dlsvn452; __81fgg2count452 != 0; __81fgg2count452--, _b5p6od9s += (__81fgg2step452)) {

								{
									
									_1ajfmh55 = *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg));
									if (_rcjmgxm4)
									_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)));
									{
										System.Int32 __81fgg2dlsvn453 = (System.Int32)((_b5p6od9s + (int)1));
										const System.Int32 __81fgg2step453 = (System.Int32)((int)1);
										System.Int32 __81fgg2count453;
										for (__81fgg2count453 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn453 + __81fgg2step453) / __81fgg2step453)), _umlkckdg = __81fgg2dlsvn453; __81fgg2count453 != 0; __81fgg2count453--, _umlkckdg += (__81fgg2step453)) {

										{
											
											_1ajfmh55 = (_1ajfmh55 + (*(_vxfgpup9+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) * *(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
Mark120:;
											// continue
										}
																				}									}
									*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_r7cfteg3 * _1ajfmh55);
Mark130:;
									// continue
								}
																}							}
Mark140:;
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
						System.Int32 __81fgg2dlsvn454 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step454 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count454;
						for (__81fgg2count454 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn454 + __81fgg2step454) / __81fgg2step454)), _znpjgsef = __81fgg2dlsvn454; __81fgg2count454 != 0; __81fgg2count454--, _znpjgsef += (__81fgg2step454)) {

						{
							
							_1ajfmh55 = _r7cfteg3;
							if (_rcjmgxm4)
							_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							{
								System.Int32 __81fgg2dlsvn455 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step455 = (System.Int32)((int)1);
								System.Int32 __81fgg2count455;
								for (__81fgg2count455 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn455 + __81fgg2step455) / __81fgg2step455)), _b5p6od9s = __81fgg2dlsvn455; __81fgg2count455 != 0; __81fgg2count455--, _b5p6od9s += (__81fgg2step455)) {

								{
									
									*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
Mark150:;
									// continue
								}
																}							}
							{
								System.Int32 __81fgg2dlsvn456 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step456 = (System.Int32)((int)1);
								System.Int32 __81fgg2count456;
								for (__81fgg2count456 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn456 + __81fgg2step456) / __81fgg2step456)), _umlkckdg = __81fgg2dlsvn456; __81fgg2count456 != 0; __81fgg2count456--, _umlkckdg += (__81fgg2step456)) {

								{
									
									if (*(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										_1ajfmh55 = (_r7cfteg3 * *(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
										{
											System.Int32 __81fgg2dlsvn457 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step457 = (System.Int32)((int)1);
											System.Int32 __81fgg2count457;
											for (__81fgg2count457 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn457 + __81fgg2step457) / __81fgg2step457)), _b5p6od9s = __81fgg2dlsvn457; __81fgg2count457 != 0; __81fgg2count457--, _b5p6od9s += (__81fgg2step457)) {

											{
												
												*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) + (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg))));
Mark160:;
												// continue
											}
																						}										}
									}
									
Mark170:;
									// continue
								}
																}							}
Mark180:;
							// continue
						}
												}					}
				}
				else
				{
					
					{
						System.Int32 __81fgg2dlsvn458 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step458 = (System.Int32)((int)1);
						System.Int32 __81fgg2count458;
						for (__81fgg2count458 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn458 + __81fgg2step458) / __81fgg2step458)), _znpjgsef = __81fgg2dlsvn458; __81fgg2count458 != 0; __81fgg2count458--, _znpjgsef += (__81fgg2step458)) {

						{
							
							_1ajfmh55 = _r7cfteg3;
							if (_rcjmgxm4)
							_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							{
								System.Int32 __81fgg2dlsvn459 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step459 = (System.Int32)((int)1);
								System.Int32 __81fgg2count459;
								for (__81fgg2count459 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn459 + __81fgg2step459) / __81fgg2step459)), _b5p6od9s = __81fgg2dlsvn459; __81fgg2count459 != 0; __81fgg2count459--, _b5p6od9s += (__81fgg2step459)) {

								{
									
									*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
Mark190:;
									// continue
								}
																}							}
							{
								System.Int32 __81fgg2dlsvn460 = (System.Int32)((_znpjgsef + (int)1));
								const System.Int32 __81fgg2step460 = (System.Int32)((int)1);
								System.Int32 __81fgg2count460;
								for (__81fgg2count460 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn460 + __81fgg2step460) / __81fgg2step460)), _umlkckdg = __81fgg2dlsvn460; __81fgg2count460 != 0; __81fgg2count460--, _umlkckdg += (__81fgg2step460)) {

								{
									
									if (*(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										_1ajfmh55 = (_r7cfteg3 * *(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
										{
											System.Int32 __81fgg2dlsvn461 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step461 = (System.Int32)((int)1);
											System.Int32 __81fgg2count461;
											for (__81fgg2count461 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn461 + __81fgg2step461) / __81fgg2step461)), _b5p6od9s = __81fgg2dlsvn461; __81fgg2count461 != 0; __81fgg2count461--, _b5p6od9s += (__81fgg2step461)) {

											{
												
												*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) + (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg))));
Mark200:;
												// continue
											}
																						}										}
									}
									
Mark210:;
									// continue
								}
																}							}
Mark220:;
							// continue
						}
												}					}
				}
				
			}
			else
			{
				//* 
				//*           Form  B := alpha*B*A**T. 
				//* 
				
				if (_l08igmvf)
				{
					
					{
						System.Int32 __81fgg2dlsvn462 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step462 = (System.Int32)((int)1);
						System.Int32 __81fgg2count462;
						for (__81fgg2count462 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn462 + __81fgg2step462) / __81fgg2step462)), _umlkckdg = __81fgg2dlsvn462; __81fgg2count462 != 0; __81fgg2count462--, _umlkckdg += (__81fgg2step462)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn463 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step463 = (System.Int32)((int)1);
								System.Int32 __81fgg2count463;
								for (__81fgg2count463 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg - (int)1) - __81fgg2dlsvn463 + __81fgg2step463) / __81fgg2step463)), _znpjgsef = __81fgg2dlsvn463; __81fgg2count463 != 0; __81fgg2count463--, _znpjgsef += (__81fgg2step463)) {

								{
									
									if (*(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										_1ajfmh55 = (_r7cfteg3 * *(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
										{
											System.Int32 __81fgg2dlsvn464 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step464 = (System.Int32)((int)1);
											System.Int32 __81fgg2count464;
											for (__81fgg2count464 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn464 + __81fgg2step464) / __81fgg2step464)), _b5p6od9s = __81fgg2dlsvn464; __81fgg2count464 != 0; __81fgg2count464--, _b5p6od9s += (__81fgg2step464)) {

											{
												
												*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) + (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg))));
Mark230:;
												// continue
											}
																						}										}
									}
									
Mark240:;
									// continue
								}
																}							}
							_1ajfmh55 = _r7cfteg3;
							if (_rcjmgxm4)
							_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
							if (_1ajfmh55 != _kxg5drh2)
							{
								
								{
									System.Int32 __81fgg2dlsvn465 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step465 = (System.Int32)((int)1);
									System.Int32 __81fgg2count465;
									for (__81fgg2count465 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn465 + __81fgg2step465) / __81fgg2step465)), _b5p6od9s = __81fgg2dlsvn465; __81fgg2count465 != 0; __81fgg2count465--, _b5p6od9s += (__81fgg2step465)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)) = (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)));
Mark250:;
										// continue
									}
																		}								}
							}
							
Mark260:;
							// continue
						}
												}					}
				}
				else
				{
					
					{
						System.Int32 __81fgg2dlsvn466 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step466 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count466;
						for (__81fgg2count466 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn466 + __81fgg2step466) / __81fgg2step466)), _umlkckdg = __81fgg2dlsvn466; __81fgg2count466 != 0; __81fgg2count466--, _umlkckdg += (__81fgg2step466)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn467 = (System.Int32)((_umlkckdg + (int)1));
								const System.Int32 __81fgg2step467 = (System.Int32)((int)1);
								System.Int32 __81fgg2count467;
								for (__81fgg2count467 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn467 + __81fgg2step467) / __81fgg2step467)), _znpjgsef = __81fgg2dlsvn467; __81fgg2count467 != 0; __81fgg2count467--, _znpjgsef += (__81fgg2step467)) {

								{
									
									if (*(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										_1ajfmh55 = (_r7cfteg3 * *(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
										{
											System.Int32 __81fgg2dlsvn468 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step468 = (System.Int32)((int)1);
											System.Int32 __81fgg2count468;
											for (__81fgg2count468 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn468 + __81fgg2step468) / __81fgg2step468)), _b5p6od9s = __81fgg2dlsvn468; __81fgg2count468 != 0; __81fgg2count468--, _b5p6od9s += (__81fgg2step468)) {

											{
												
												*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) + (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg))));
Mark270:;
												// continue
											}
																						}										}
									}
									
Mark280:;
									// continue
								}
																}							}
							_1ajfmh55 = _r7cfteg3;
							if (_rcjmgxm4)
							_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
							if (_1ajfmh55 != _kxg5drh2)
							{
								
								{
									System.Int32 __81fgg2dlsvn469 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step469 = (System.Int32)((int)1);
									System.Int32 __81fgg2count469;
									for (__81fgg2count469 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn469 + __81fgg2step469) / __81fgg2step469)), _b5p6od9s = __81fgg2dlsvn469; __81fgg2count469 != 0; __81fgg2count469--, _b5p6od9s += (__81fgg2step469)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)) = (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)));
Mark290:;
										// continue
									}
																		}								}
							}
							
Mark300:;
							// continue
						}
												}					}
				}
				
			}
			
		}
		//* 
		
		return;//* 
		//*     End of DTRMM . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
