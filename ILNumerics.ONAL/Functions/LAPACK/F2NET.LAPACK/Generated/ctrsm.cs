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
//*> \brief \b CTRSM 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CTRSM(SIDE,UPLO,TRANSA,DIAG,M,N,ALPHA,A,LDA,B,LDB) 
//* 
//*       .. Scalar Arguments .. 
//*       COMPLEX ALPHA 
//*       INTEGER LDA,LDB,M,N 
//*       CHARACTER DIAG,SIDE,TRANSA,UPLO 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX A(LDA,*),B(LDB,*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CTRSM  solves one of the matrix equations 
//*> 
//*>    op( A )*X = alpha*B,   or   X*op( A ) = alpha*B, 
//*> 
//*> where alpha is a scalar, X and B are m by n matrices, A is a unit, or 
//*> non-unit,  upper or lower triangular matrix  and  op( A )  is one  of 
//*> 
//*>    op( A ) = A   or   op( A ) = A**T   or   op( A ) = A**H. 
//*> 
//*> The matrix X is overwritten on B. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] SIDE 
//*> \verbatim 
//*>          SIDE is CHARACTER*1 
//*>           On entry, SIDE specifies whether op( A ) appears on the left 
//*>           or right of X as follows: 
//*> 
//*>              SIDE = 'L' or 'l'   op( A )*X = alpha*B. 
//*> 
//*>              SIDE = 'R' or 'r'   X*op( A ) = alpha*B. 
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
//*>          ALPHA is COMPLEX 
//*>           On entry,  ALPHA specifies the scalar  alpha. When  alpha is 
//*>           zero then  A is not referenced and  B need not be set before 
//*>           entry. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is COMPLEX array, dimension ( LDA, k ), 
//*>           where k is m when SIDE = 'L' or 'l' 
//*>             and k is n when SIDE = 'R' or 'r'. 
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
//*>          B is COMPLEX array, dimension ( LDB, N ) 
//*>           Before entry,  the leading  m by n part of the array  B must 
//*>           contain  the  right-hand  side  matrix  B,  and  on exit  is 
//*>           overwritten by the solution matrix  X. 
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

	 
	public static void _goj1gzmg(FString _m2cn2gjg, FString _9wyre9zc, FString _742vrzth, FString _2scffxp3, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref fcomplex _r7cfteg3, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, fcomplex* _p9n405a5, ref Int32 _ly9opahg)
	{
#region variable declarations
fcomplex _1ajfmh55 =  default;
Int32 _b5p6od9s =  default;
Int32 _gro5yvfo =  default;
Int32 _znpjgsef =  default;
Int32 _umlkckdg =  default;
Int32 _o9a6qdux =  default;
Boolean _wln8whqb =  default;
Boolean _moml4lap =  default;
Boolean _rcjmgxm4 =  default;
Boolean _l08igmvf =  default;
fcomplex _kxg5drh2 =   new fcomplex(1f,0f);
fcomplex _d0547bi2 =   new fcomplex(0f,0f);
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
			
			_ut9qalzx("CTRSM " ,ref _gro5yvfo );
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
				System.Int32 __81fgg2dlsvn1612 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1612 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1612;
				for (__81fgg2count1612 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1612 + __81fgg2step1612) / __81fgg2step1612)), _znpjgsef = __81fgg2dlsvn1612; __81fgg2count1612 != 0; __81fgg2count1612--, _znpjgsef += (__81fgg2step1612)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn1613 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1613 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1613;
						for (__81fgg2count1613 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1613 + __81fgg2step1613) / __81fgg2step1613)), _b5p6od9s = __81fgg2dlsvn1613; __81fgg2count1613 != 0; __81fgg2count1613--, _b5p6od9s += (__81fgg2step1613)) {

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
				//*           Form  B := alpha*inv( A )*B. 
				//* 
				
				if (_l08igmvf)
				{
					
					{
						System.Int32 __81fgg2dlsvn1614 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1614 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1614;
						for (__81fgg2count1614 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1614 + __81fgg2step1614) / __81fgg2step1614)), _znpjgsef = __81fgg2dlsvn1614; __81fgg2count1614 != 0; __81fgg2count1614--, _znpjgsef += (__81fgg2step1614)) {

						{
							
							if (_r7cfteg3 != _kxg5drh2)
							{
								
								{
									System.Int32 __81fgg2dlsvn1615 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1615 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1615;
									for (__81fgg2count1615 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1615 + __81fgg2step1615) / __81fgg2step1615)), _b5p6od9s = __81fgg2dlsvn1615; __81fgg2count1615 != 0; __81fgg2count1615--, _b5p6od9s += (__81fgg2step1615)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
Mark30:;
										// continue
									}
																		}								}
							}
							
							{
								System.Int32 __81fgg2dlsvn1616 = (System.Int32)(_ev4xhht5);
								System.Int32 __81fgg2step1616 = (System.Int32)((int)-1);
								System.Int32 __81fgg2count1616;
								for (__81fgg2count1616 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1616 + __81fgg2step1616) / __81fgg2step1616)), _umlkckdg = __81fgg2dlsvn1616; __81fgg2count1616 != 0; __81fgg2count1616--, _umlkckdg += (__81fgg2step1616)) {

								{
									
									if (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) != _d0547bi2)
									{
										
										if (_rcjmgxm4)
										*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) / *(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
										{
											System.Int32 __81fgg2dlsvn1617 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1617 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1617;
											for (__81fgg2count1617 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg - (int)1) - __81fgg2dlsvn1617 + __81fgg2step1617) / __81fgg2step1617)), _b5p6od9s = __81fgg2dlsvn1617; __81fgg2count1617 != 0; __81fgg2count1617--, _b5p6od9s += (__81fgg2step1617)) {

											{
												
												*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) - (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) * *(_vxfgpup9+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c))));
Mark40:;
												// continue
											}
																						}										}
									}
									
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
						System.Int32 __81fgg2dlsvn1618 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1618 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1618;
						for (__81fgg2count1618 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1618 + __81fgg2step1618) / __81fgg2step1618)), _znpjgsef = __81fgg2dlsvn1618; __81fgg2count1618 != 0; __81fgg2count1618--, _znpjgsef += (__81fgg2step1618)) {

						{
							
							if (_r7cfteg3 != _kxg5drh2)
							{
								
								{
									System.Int32 __81fgg2dlsvn1619 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1619 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1619;
									for (__81fgg2count1619 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1619 + __81fgg2step1619) / __81fgg2step1619)), _b5p6od9s = __81fgg2dlsvn1619; __81fgg2count1619 != 0; __81fgg2count1619--, _b5p6od9s += (__81fgg2step1619)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
Mark70:;
										// continue
									}
																		}								}
							}
							
							{
								System.Int32 __81fgg2dlsvn1620 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1620 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1620;
								for (__81fgg2count1620 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1620 + __81fgg2step1620) / __81fgg2step1620)), _umlkckdg = __81fgg2dlsvn1620; __81fgg2count1620 != 0; __81fgg2count1620--, _umlkckdg += (__81fgg2step1620)) {

								{
									
									if (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) != _d0547bi2)
									{
										
										if (_rcjmgxm4)
										*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) / *(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
										{
											System.Int32 __81fgg2dlsvn1621 = (System.Int32)((_umlkckdg + (int)1));
											const System.Int32 __81fgg2step1621 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1621;
											for (__81fgg2count1621 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1621 + __81fgg2step1621) / __81fgg2step1621)), _b5p6od9s = __81fgg2dlsvn1621; __81fgg2count1621 != 0; __81fgg2count1621--, _b5p6od9s += (__81fgg2step1621)) {

											{
												
												*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) - (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) * *(_vxfgpup9+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c))));
Mark80:;
												// continue
											}
																						}										}
									}
									
Mark90:;
									// continue
								}
																}							}
Mark100:;
							// continue
						}
												}					}
				}
				
			}
			else
			{
				//* 
				//*           Form  B := alpha*inv( A**T )*B 
				//*           or    B := alpha*inv( A**H )*B. 
				//* 
				
				if (_l08igmvf)
				{
					
					{
						System.Int32 __81fgg2dlsvn1622 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1622 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1622;
						for (__81fgg2count1622 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1622 + __81fgg2step1622) / __81fgg2step1622)), _znpjgsef = __81fgg2dlsvn1622; __81fgg2count1622 != 0; __81fgg2count1622--, _znpjgsef += (__81fgg2step1622)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1623 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1623 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1623;
								for (__81fgg2count1623 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1623 + __81fgg2step1623) / __81fgg2step1623)), _b5p6od9s = __81fgg2dlsvn1623; __81fgg2count1623 != 0; __81fgg2count1623--, _b5p6od9s += (__81fgg2step1623)) {

								{
									
									_1ajfmh55 = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
									if (_moml4lap)
									{
										
										{
											System.Int32 __81fgg2dlsvn1624 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1624 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1624;
											for (__81fgg2count1624 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s - (int)1) - __81fgg2dlsvn1624 + __81fgg2step1624) / __81fgg2step1624)), _umlkckdg = __81fgg2dlsvn1624; __81fgg2count1624 != 0; __81fgg2count1624--, _umlkckdg += (__81fgg2step1624)) {

											{
												
												_1ajfmh55 = (_1ajfmh55 - (*(_vxfgpup9+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) * *(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
Mark110:;
												// continue
											}
																						}										}
										if (_rcjmgxm4)
										_1ajfmh55 = (_1ajfmh55 / *(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)));
									}
									else
									{
										
										{
											System.Int32 __81fgg2dlsvn1625 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1625 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1625;
											for (__81fgg2count1625 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s - (int)1) - __81fgg2dlsvn1625 + __81fgg2step1625) / __81fgg2step1625)), _umlkckdg = __81fgg2dlsvn1625; __81fgg2count1625 != 0; __81fgg2count1625--, _umlkckdg += (__81fgg2step1625)) {

											{
												
												_1ajfmh55 = (_1ajfmh55 - (ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ) * *(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
Mark120:;
												// continue
											}
																						}										}
										if (_rcjmgxm4)
										_1ajfmh55 = (_1ajfmh55 / ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ));
									}
									
									*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = _1ajfmh55;
Mark130:;
									// continue
								}
																}							}
Mark140:;
							// continue
						}
												}					}
				}
				else
				{
					
					{
						System.Int32 __81fgg2dlsvn1626 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1626 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1626;
						for (__81fgg2count1626 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1626 + __81fgg2step1626) / __81fgg2step1626)), _znpjgsef = __81fgg2dlsvn1626; __81fgg2count1626 != 0; __81fgg2count1626--, _znpjgsef += (__81fgg2step1626)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1627 = (System.Int32)(_ev4xhht5);
								System.Int32 __81fgg2step1627 = (System.Int32)((int)-1);
								System.Int32 __81fgg2count1627;
								for (__81fgg2count1627 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1627 + __81fgg2step1627) / __81fgg2step1627)), _b5p6od9s = __81fgg2dlsvn1627; __81fgg2count1627 != 0; __81fgg2count1627--, _b5p6od9s += (__81fgg2step1627)) {

								{
									
									_1ajfmh55 = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
									if (_moml4lap)
									{
										
										{
											System.Int32 __81fgg2dlsvn1628 = (System.Int32)((_b5p6od9s + (int)1));
											const System.Int32 __81fgg2step1628 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1628;
											for (__81fgg2count1628 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1628 + __81fgg2step1628) / __81fgg2step1628)), _umlkckdg = __81fgg2dlsvn1628; __81fgg2count1628 != 0; __81fgg2count1628--, _umlkckdg += (__81fgg2step1628)) {

											{
												
												_1ajfmh55 = (_1ajfmh55 - (*(_vxfgpup9+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) * *(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
Mark150:;
												// continue
											}
																						}										}
										if (_rcjmgxm4)
										_1ajfmh55 = (_1ajfmh55 / *(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)));
									}
									else
									{
										
										{
											System.Int32 __81fgg2dlsvn1629 = (System.Int32)((_b5p6od9s + (int)1));
											const System.Int32 __81fgg2step1629 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1629;
											for (__81fgg2count1629 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1629 + __81fgg2step1629) / __81fgg2step1629)), _umlkckdg = __81fgg2dlsvn1629; __81fgg2count1629 != 0; __81fgg2count1629--, _umlkckdg += (__81fgg2step1629)) {

											{
												
												_1ajfmh55 = (_1ajfmh55 - (ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ) * *(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
Mark160:;
												// continue
											}
																						}										}
										if (_rcjmgxm4)
										_1ajfmh55 = (_1ajfmh55 / ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ));
									}
									
									*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = _1ajfmh55;
Mark170:;
									// continue
								}
																}							}
Mark180:;
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
				//*           Form  B := alpha*B*inv( A ). 
				//* 
				
				if (_l08igmvf)
				{
					
					{
						System.Int32 __81fgg2dlsvn1630 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1630 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1630;
						for (__81fgg2count1630 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1630 + __81fgg2step1630) / __81fgg2step1630)), _znpjgsef = __81fgg2dlsvn1630; __81fgg2count1630 != 0; __81fgg2count1630--, _znpjgsef += (__81fgg2step1630)) {

						{
							
							if (_r7cfteg3 != _kxg5drh2)
							{
								
								{
									System.Int32 __81fgg2dlsvn1631 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1631 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1631;
									for (__81fgg2count1631 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1631 + __81fgg2step1631) / __81fgg2step1631)), _b5p6od9s = __81fgg2dlsvn1631; __81fgg2count1631 != 0; __81fgg2count1631--, _b5p6od9s += (__81fgg2step1631)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
Mark190:;
										// continue
									}
																		}								}
							}
							
							{
								System.Int32 __81fgg2dlsvn1632 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1632 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1632;
								for (__81fgg2count1632 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn1632 + __81fgg2step1632) / __81fgg2step1632)), _umlkckdg = __81fgg2dlsvn1632; __81fgg2count1632 != 0; __81fgg2count1632--, _umlkckdg += (__81fgg2step1632)) {

								{
									
									if (*(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										{
											System.Int32 __81fgg2dlsvn1633 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1633 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1633;
											for (__81fgg2count1633 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1633 + __81fgg2step1633) / __81fgg2step1633)), _b5p6od9s = __81fgg2dlsvn1633; __81fgg2count1633 != 0; __81fgg2count1633--, _b5p6od9s += (__81fgg2step1633)) {

											{
												
												*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) - (*(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg))));
Mark200:;
												// continue
											}
																						}										}
									}
									
Mark210:;
									// continue
								}
																}							}
							if (_rcjmgxm4)
							{
								
								_1ajfmh55 = (_kxg5drh2 / *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
								{
									System.Int32 __81fgg2dlsvn1634 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1634 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1634;
									for (__81fgg2count1634 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1634 + __81fgg2step1634) / __81fgg2step1634)), _b5p6od9s = __81fgg2dlsvn1634; __81fgg2count1634 != 0; __81fgg2count1634--, _b5p6od9s += (__81fgg2step1634)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
Mark220:;
										// continue
									}
																		}								}
							}
							
Mark230:;
							// continue
						}
												}					}
				}
				else
				{
					
					{
						System.Int32 __81fgg2dlsvn1635 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step1635 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count1635;
						for (__81fgg2count1635 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1635 + __81fgg2step1635) / __81fgg2step1635)), _znpjgsef = __81fgg2dlsvn1635; __81fgg2count1635 != 0; __81fgg2count1635--, _znpjgsef += (__81fgg2step1635)) {

						{
							
							if (_r7cfteg3 != _kxg5drh2)
							{
								
								{
									System.Int32 __81fgg2dlsvn1636 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1636 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1636;
									for (__81fgg2count1636 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1636 + __81fgg2step1636) / __81fgg2step1636)), _b5p6od9s = __81fgg2dlsvn1636; __81fgg2count1636 != 0; __81fgg2count1636--, _b5p6od9s += (__81fgg2step1636)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
Mark240:;
										// continue
									}
																		}								}
							}
							
							{
								System.Int32 __81fgg2dlsvn1637 = (System.Int32)((_znpjgsef + (int)1));
								const System.Int32 __81fgg2step1637 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1637;
								for (__81fgg2count1637 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1637 + __81fgg2step1637) / __81fgg2step1637)), _umlkckdg = __81fgg2dlsvn1637; __81fgg2count1637 != 0; __81fgg2count1637--, _umlkckdg += (__81fgg2step1637)) {

								{
									
									if (*(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										{
											System.Int32 __81fgg2dlsvn1638 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1638 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1638;
											for (__81fgg2count1638 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1638 + __81fgg2step1638) / __81fgg2step1638)), _b5p6od9s = __81fgg2dlsvn1638; __81fgg2count1638 != 0; __81fgg2count1638--, _b5p6od9s += (__81fgg2step1638)) {

											{
												
												*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) - (*(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg))));
Mark250:;
												// continue
											}
																						}										}
									}
									
Mark260:;
									// continue
								}
																}							}
							if (_rcjmgxm4)
							{
								
								_1ajfmh55 = (_kxg5drh2 / *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
								{
									System.Int32 __81fgg2dlsvn1639 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1639 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1639;
									for (__81fgg2count1639 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1639 + __81fgg2step1639) / __81fgg2step1639)), _b5p6od9s = __81fgg2dlsvn1639; __81fgg2count1639 != 0; __81fgg2count1639--, _b5p6od9s += (__81fgg2step1639)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
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
				
			}
			else
			{
				//* 
				//*           Form  B := alpha*B*inv( A**T ) 
				//*           or    B := alpha*B*inv( A**H ). 
				//* 
				
				if (_l08igmvf)
				{
					
					{
						System.Int32 __81fgg2dlsvn1640 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step1640 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count1640;
						for (__81fgg2count1640 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1640 + __81fgg2step1640) / __81fgg2step1640)), _umlkckdg = __81fgg2dlsvn1640; __81fgg2count1640 != 0; __81fgg2count1640--, _umlkckdg += (__81fgg2step1640)) {

						{
							
							if (_rcjmgxm4)
							{
								
								if (_moml4lap)
								{
									
									_1ajfmh55 = (_kxg5drh2 / *(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
								}
								else
								{
									
									_1ajfmh55 = (_kxg5drh2 / ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) ));
								}
								
								{
									System.Int32 __81fgg2dlsvn1641 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1641 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1641;
									for (__81fgg2count1641 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1641 + __81fgg2step1641) / __81fgg2step1641)), _b5p6od9s = __81fgg2dlsvn1641; __81fgg2count1641 != 0; __81fgg2count1641--, _b5p6od9s += (__81fgg2step1641)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)) = (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)));
Mark290:;
										// continue
									}
																		}								}
							}
							
							{
								System.Int32 __81fgg2dlsvn1642 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1642 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1642;
								for (__81fgg2count1642 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg - (int)1) - __81fgg2dlsvn1642 + __81fgg2step1642) / __81fgg2step1642)), _znpjgsef = __81fgg2dlsvn1642; __81fgg2count1642 != 0; __81fgg2count1642--, _znpjgsef += (__81fgg2step1642)) {

								{
									
									if (*(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										if (_moml4lap)
										{
											
											_1ajfmh55 = *(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c));
										}
										else
										{
											
											_1ajfmh55 = ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) );
										}
										
										{
											System.Int32 __81fgg2dlsvn1643 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1643 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1643;
											for (__81fgg2count1643 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1643 + __81fgg2step1643) / __81fgg2step1643)), _b5p6od9s = __81fgg2dlsvn1643; __81fgg2count1643 != 0; __81fgg2count1643--, _b5p6od9s += (__81fgg2step1643)) {

											{
												
												*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) - (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg))));
Mark300:;
												// continue
											}
																						}										}
									}
									
Mark310:;
									// continue
								}
																}							}
							if (_r7cfteg3 != _kxg5drh2)
							{
								
								{
									System.Int32 __81fgg2dlsvn1644 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1644 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1644;
									for (__81fgg2count1644 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1644 + __81fgg2step1644) / __81fgg2step1644)), _b5p6od9s = __81fgg2dlsvn1644; __81fgg2count1644 != 0; __81fgg2count1644--, _b5p6od9s += (__81fgg2step1644)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)) = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)));
Mark320:;
										// continue
									}
																		}								}
							}
							
Mark330:;
							// continue
						}
												}					}
				}
				else
				{
					
					{
						System.Int32 __81fgg2dlsvn1645 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1645 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1645;
						for (__81fgg2count1645 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1645 + __81fgg2step1645) / __81fgg2step1645)), _umlkckdg = __81fgg2dlsvn1645; __81fgg2count1645 != 0; __81fgg2count1645--, _umlkckdg += (__81fgg2step1645)) {

						{
							
							if (_rcjmgxm4)
							{
								
								if (_moml4lap)
								{
									
									_1ajfmh55 = (_kxg5drh2 / *(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
								}
								else
								{
									
									_1ajfmh55 = (_kxg5drh2 / ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) ));
								}
								
								{
									System.Int32 __81fgg2dlsvn1646 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1646 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1646;
									for (__81fgg2count1646 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1646 + __81fgg2step1646) / __81fgg2step1646)), _b5p6od9s = __81fgg2dlsvn1646; __81fgg2count1646 != 0; __81fgg2count1646--, _b5p6od9s += (__81fgg2step1646)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)) = (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)));
Mark340:;
										// continue
									}
																		}								}
							}
							
							{
								System.Int32 __81fgg2dlsvn1647 = (System.Int32)((_umlkckdg + (int)1));
								const System.Int32 __81fgg2step1647 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1647;
								for (__81fgg2count1647 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1647 + __81fgg2step1647) / __81fgg2step1647)), _znpjgsef = __81fgg2dlsvn1647; __81fgg2count1647 != 0; __81fgg2count1647--, _znpjgsef += (__81fgg2step1647)) {

								{
									
									if (*(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										if (_moml4lap)
										{
											
											_1ajfmh55 = *(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c));
										}
										else
										{
											
											_1ajfmh55 = ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) );
										}
										
										{
											System.Int32 __81fgg2dlsvn1648 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1648 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1648;
											for (__81fgg2count1648 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1648 + __81fgg2step1648) / __81fgg2step1648)), _b5p6od9s = __81fgg2dlsvn1648; __81fgg2count1648 != 0; __81fgg2count1648--, _b5p6od9s += (__81fgg2step1648)) {

											{
												
												*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) - (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg))));
Mark350:;
												// continue
											}
																						}										}
									}
									
Mark360:;
									// continue
								}
																}							}
							if (_r7cfteg3 != _kxg5drh2)
							{
								
								{
									System.Int32 __81fgg2dlsvn1649 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1649 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1649;
									for (__81fgg2count1649 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1649 + __81fgg2step1649) / __81fgg2step1649)), _b5p6od9s = __81fgg2dlsvn1649; __81fgg2count1649 != 0; __81fgg2count1649--, _b5p6od9s += (__81fgg2step1649)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)) = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)));
Mark370:;
										// continue
									}
																		}								}
							}
							
Mark380:;
							// continue
						}
												}					}
				}
				
			}
			
		}
		//* 
		
		return;//* 
		//*     End of CTRSM . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
