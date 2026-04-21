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
//*> \brief \b STRMM 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE STRMM(SIDE,UPLO,TRANSA,DIAG,M,N,ALPHA,A,LDA,B,LDB) 
//* 
//*       .. Scalar Arguments .. 
//*       REAL ALPHA 
//*       INTEGER LDA,LDB,M,N 
//*       CHARACTER DIAG,SIDE,TRANSA,UPLO 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL A(LDA,*),B(LDB,*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> STRMM  performs one of the matrix-matrix operations 
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
//*>          ALPHA is REAL 
//*>           On entry,  ALPHA specifies the scalar  alpha. When  alpha is 
//*>           zero then  A is not referenced and  B need not be set before 
//*>           entry. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is REAL array, dimension ( LDA, k ), where k is m 
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
//*>          B is REAL array, dimension ( LDB, N ) 
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

	 
	public static void _sdtp2num(FString _m2cn2gjg, FString _9wyre9zc, FString _742vrzth, FString _2scffxp3, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Single _r7cfteg3, Single* _vxfgpup9, ref Int32 _ocv8fk5c, Single* _p9n405a5, ref Int32 _ly9opahg)
	{
#region variable declarations
Single _1ajfmh55 =  default;
Int32 _b5p6od9s =  default;
Int32 _gro5yvfo =  default;
Int32 _znpjgsef =  default;
Int32 _umlkckdg =  default;
Int32 _o9a6qdux =  default;
Boolean _wln8whqb =  default;
Boolean _rcjmgxm4 =  default;
Boolean _l08igmvf =  default;
Single _kxg5drh2 =  1f;
Single _d0547bi2 =  0f;
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
			
			_ut9qalzx("STRMM " ,ref _gro5yvfo );
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
				System.Int32 __81fgg2dlsvn815 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step815 = (System.Int32)((int)1);
				System.Int32 __81fgg2count815;
				for (__81fgg2count815 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn815 + __81fgg2step815) / __81fgg2step815)), _znpjgsef = __81fgg2dlsvn815; __81fgg2count815 != 0; __81fgg2count815--, _znpjgsef += (__81fgg2step815)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn816 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step816 = (System.Int32)((int)1);
						System.Int32 __81fgg2count816;
						for (__81fgg2count816 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn816 + __81fgg2step816) / __81fgg2step816)), _b5p6od9s = __81fgg2dlsvn816; __81fgg2count816 != 0; __81fgg2count816--, _b5p6od9s += (__81fgg2step816)) {

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
						System.Int32 __81fgg2dlsvn817 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step817 = (System.Int32)((int)1);
						System.Int32 __81fgg2count817;
						for (__81fgg2count817 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn817 + __81fgg2step817) / __81fgg2step817)), _znpjgsef = __81fgg2dlsvn817; __81fgg2count817 != 0; __81fgg2count817--, _znpjgsef += (__81fgg2step817)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn818 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step818 = (System.Int32)((int)1);
								System.Int32 __81fgg2count818;
								for (__81fgg2count818 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn818 + __81fgg2step818) / __81fgg2step818)), _umlkckdg = __81fgg2dlsvn818; __81fgg2count818 != 0; __81fgg2count818--, _umlkckdg += (__81fgg2step818)) {

								{
									
									if (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) != _d0547bi2)
									{
										
										_1ajfmh55 = (_r7cfteg3 * *(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
										{
											System.Int32 __81fgg2dlsvn819 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step819 = (System.Int32)((int)1);
											System.Int32 __81fgg2count819;
											for (__81fgg2count819 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg - (int)1) - __81fgg2dlsvn819 + __81fgg2step819) / __81fgg2step819)), _b5p6od9s = __81fgg2dlsvn819; __81fgg2count819 != 0; __81fgg2count819--, _b5p6od9s += (__81fgg2step819)) {

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
						System.Int32 __81fgg2dlsvn820 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step820 = (System.Int32)((int)1);
						System.Int32 __81fgg2count820;
						for (__81fgg2count820 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn820 + __81fgg2step820) / __81fgg2step820)), _znpjgsef = __81fgg2dlsvn820; __81fgg2count820 != 0; __81fgg2count820--, _znpjgsef += (__81fgg2step820)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn821 = (System.Int32)(_ev4xhht5);
								System.Int32 __81fgg2step821 = (System.Int32)((int)-1);
								System.Int32 __81fgg2count821;
								for (__81fgg2count821 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn821 + __81fgg2step821) / __81fgg2step821)), _umlkckdg = __81fgg2dlsvn821; __81fgg2count821 != 0; __81fgg2count821--, _umlkckdg += (__81fgg2step821)) {

								{
									
									if (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) != _d0547bi2)
									{
										
										_1ajfmh55 = (_r7cfteg3 * *(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
										*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = _1ajfmh55;
										if (_rcjmgxm4)
										*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) * *(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
										{
											System.Int32 __81fgg2dlsvn822 = (System.Int32)((_umlkckdg + (int)1));
											const System.Int32 __81fgg2step822 = (System.Int32)((int)1);
											System.Int32 __81fgg2count822;
											for (__81fgg2count822 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn822 + __81fgg2step822) / __81fgg2step822)), _b5p6od9s = __81fgg2dlsvn822; __81fgg2count822 != 0; __81fgg2count822--, _b5p6od9s += (__81fgg2step822)) {

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
						System.Int32 __81fgg2dlsvn823 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step823 = (System.Int32)((int)1);
						System.Int32 __81fgg2count823;
						for (__81fgg2count823 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn823 + __81fgg2step823) / __81fgg2step823)), _znpjgsef = __81fgg2dlsvn823; __81fgg2count823 != 0; __81fgg2count823--, _znpjgsef += (__81fgg2step823)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn824 = (System.Int32)(_ev4xhht5);
								System.Int32 __81fgg2step824 = (System.Int32)((int)-1);
								System.Int32 __81fgg2count824;
								for (__81fgg2count824 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn824 + __81fgg2step824) / __81fgg2step824)), _b5p6od9s = __81fgg2dlsvn824; __81fgg2count824 != 0; __81fgg2count824--, _b5p6od9s += (__81fgg2step824)) {

								{
									
									_1ajfmh55 = *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg));
									if (_rcjmgxm4)
									_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)));
									{
										System.Int32 __81fgg2dlsvn825 = (System.Int32)((int)1);
										const System.Int32 __81fgg2step825 = (System.Int32)((int)1);
										System.Int32 __81fgg2count825;
										for (__81fgg2count825 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s - (int)1) - __81fgg2dlsvn825 + __81fgg2step825) / __81fgg2step825)), _umlkckdg = __81fgg2dlsvn825; __81fgg2count825 != 0; __81fgg2count825--, _umlkckdg += (__81fgg2step825)) {

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
						System.Int32 __81fgg2dlsvn826 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step826 = (System.Int32)((int)1);
						System.Int32 __81fgg2count826;
						for (__81fgg2count826 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn826 + __81fgg2step826) / __81fgg2step826)), _znpjgsef = __81fgg2dlsvn826; __81fgg2count826 != 0; __81fgg2count826--, _znpjgsef += (__81fgg2step826)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn827 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step827 = (System.Int32)((int)1);
								System.Int32 __81fgg2count827;
								for (__81fgg2count827 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn827 + __81fgg2step827) / __81fgg2step827)), _b5p6od9s = __81fgg2dlsvn827; __81fgg2count827 != 0; __81fgg2count827--, _b5p6od9s += (__81fgg2step827)) {

								{
									
									_1ajfmh55 = *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg));
									if (_rcjmgxm4)
									_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)));
									{
										System.Int32 __81fgg2dlsvn828 = (System.Int32)((_b5p6od9s + (int)1));
										const System.Int32 __81fgg2step828 = (System.Int32)((int)1);
										System.Int32 __81fgg2count828;
										for (__81fgg2count828 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn828 + __81fgg2step828) / __81fgg2step828)), _umlkckdg = __81fgg2dlsvn828; __81fgg2count828 != 0; __81fgg2count828--, _umlkckdg += (__81fgg2step828)) {

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
						System.Int32 __81fgg2dlsvn829 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step829 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count829;
						for (__81fgg2count829 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn829 + __81fgg2step829) / __81fgg2step829)), _znpjgsef = __81fgg2dlsvn829; __81fgg2count829 != 0; __81fgg2count829--, _znpjgsef += (__81fgg2step829)) {

						{
							
							_1ajfmh55 = _r7cfteg3;
							if (_rcjmgxm4)
							_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							{
								System.Int32 __81fgg2dlsvn830 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step830 = (System.Int32)((int)1);
								System.Int32 __81fgg2count830;
								for (__81fgg2count830 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn830 + __81fgg2step830) / __81fgg2step830)), _b5p6od9s = __81fgg2dlsvn830; __81fgg2count830 != 0; __81fgg2count830--, _b5p6od9s += (__81fgg2step830)) {

								{
									
									*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
Mark150:;
									// continue
								}
																}							}
							{
								System.Int32 __81fgg2dlsvn831 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step831 = (System.Int32)((int)1);
								System.Int32 __81fgg2count831;
								for (__81fgg2count831 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn831 + __81fgg2step831) / __81fgg2step831)), _umlkckdg = __81fgg2dlsvn831; __81fgg2count831 != 0; __81fgg2count831--, _umlkckdg += (__81fgg2step831)) {

								{
									
									if (*(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										_1ajfmh55 = (_r7cfteg3 * *(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
										{
											System.Int32 __81fgg2dlsvn832 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step832 = (System.Int32)((int)1);
											System.Int32 __81fgg2count832;
											for (__81fgg2count832 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn832 + __81fgg2step832) / __81fgg2step832)), _b5p6od9s = __81fgg2dlsvn832; __81fgg2count832 != 0; __81fgg2count832--, _b5p6od9s += (__81fgg2step832)) {

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
						System.Int32 __81fgg2dlsvn833 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step833 = (System.Int32)((int)1);
						System.Int32 __81fgg2count833;
						for (__81fgg2count833 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn833 + __81fgg2step833) / __81fgg2step833)), _znpjgsef = __81fgg2dlsvn833; __81fgg2count833 != 0; __81fgg2count833--, _znpjgsef += (__81fgg2step833)) {

						{
							
							_1ajfmh55 = _r7cfteg3;
							if (_rcjmgxm4)
							_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							{
								System.Int32 __81fgg2dlsvn834 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step834 = (System.Int32)((int)1);
								System.Int32 __81fgg2count834;
								for (__81fgg2count834 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn834 + __81fgg2step834) / __81fgg2step834)), _b5p6od9s = __81fgg2dlsvn834; __81fgg2count834 != 0; __81fgg2count834--, _b5p6od9s += (__81fgg2step834)) {

								{
									
									*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
Mark190:;
									// continue
								}
																}							}
							{
								System.Int32 __81fgg2dlsvn835 = (System.Int32)((_znpjgsef + (int)1));
								const System.Int32 __81fgg2step835 = (System.Int32)((int)1);
								System.Int32 __81fgg2count835;
								for (__81fgg2count835 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn835 + __81fgg2step835) / __81fgg2step835)), _umlkckdg = __81fgg2dlsvn835; __81fgg2count835 != 0; __81fgg2count835--, _umlkckdg += (__81fgg2step835)) {

								{
									
									if (*(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										_1ajfmh55 = (_r7cfteg3 * *(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
										{
											System.Int32 __81fgg2dlsvn836 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step836 = (System.Int32)((int)1);
											System.Int32 __81fgg2count836;
											for (__81fgg2count836 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn836 + __81fgg2step836) / __81fgg2step836)), _b5p6od9s = __81fgg2dlsvn836; __81fgg2count836 != 0; __81fgg2count836--, _b5p6od9s += (__81fgg2step836)) {

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
						System.Int32 __81fgg2dlsvn837 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step837 = (System.Int32)((int)1);
						System.Int32 __81fgg2count837;
						for (__81fgg2count837 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn837 + __81fgg2step837) / __81fgg2step837)), _umlkckdg = __81fgg2dlsvn837; __81fgg2count837 != 0; __81fgg2count837--, _umlkckdg += (__81fgg2step837)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn838 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step838 = (System.Int32)((int)1);
								System.Int32 __81fgg2count838;
								for (__81fgg2count838 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg - (int)1) - __81fgg2dlsvn838 + __81fgg2step838) / __81fgg2step838)), _znpjgsef = __81fgg2dlsvn838; __81fgg2count838 != 0; __81fgg2count838--, _znpjgsef += (__81fgg2step838)) {

								{
									
									if (*(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										_1ajfmh55 = (_r7cfteg3 * *(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
										{
											System.Int32 __81fgg2dlsvn839 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step839 = (System.Int32)((int)1);
											System.Int32 __81fgg2count839;
											for (__81fgg2count839 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn839 + __81fgg2step839) / __81fgg2step839)), _b5p6od9s = __81fgg2dlsvn839; __81fgg2count839 != 0; __81fgg2count839--, _b5p6od9s += (__81fgg2step839)) {

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
									System.Int32 __81fgg2dlsvn840 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step840 = (System.Int32)((int)1);
									System.Int32 __81fgg2count840;
									for (__81fgg2count840 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn840 + __81fgg2step840) / __81fgg2step840)), _b5p6od9s = __81fgg2dlsvn840; __81fgg2count840 != 0; __81fgg2count840--, _b5p6od9s += (__81fgg2step840)) {

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
						System.Int32 __81fgg2dlsvn841 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step841 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count841;
						for (__81fgg2count841 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn841 + __81fgg2step841) / __81fgg2step841)), _umlkckdg = __81fgg2dlsvn841; __81fgg2count841 != 0; __81fgg2count841--, _umlkckdg += (__81fgg2step841)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn842 = (System.Int32)((_umlkckdg + (int)1));
								const System.Int32 __81fgg2step842 = (System.Int32)((int)1);
								System.Int32 __81fgg2count842;
								for (__81fgg2count842 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn842 + __81fgg2step842) / __81fgg2step842)), _znpjgsef = __81fgg2dlsvn842; __81fgg2count842 != 0; __81fgg2count842--, _znpjgsef += (__81fgg2step842)) {

								{
									
									if (*(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										_1ajfmh55 = (_r7cfteg3 * *(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
										{
											System.Int32 __81fgg2dlsvn843 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step843 = (System.Int32)((int)1);
											System.Int32 __81fgg2count843;
											for (__81fgg2count843 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn843 + __81fgg2step843) / __81fgg2step843)), _b5p6od9s = __81fgg2dlsvn843; __81fgg2count843 != 0; __81fgg2count843--, _b5p6od9s += (__81fgg2step843)) {

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
									System.Int32 __81fgg2dlsvn844 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step844 = (System.Int32)((int)1);
									System.Int32 __81fgg2count844;
									for (__81fgg2count844 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn844 + __81fgg2step844) / __81fgg2step844)), _b5p6od9s = __81fgg2dlsvn844; __81fgg2count844 != 0; __81fgg2count844--, _b5p6od9s += (__81fgg2step844)) {

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
		//*     End of STRMM . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
