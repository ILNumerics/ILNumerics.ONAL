
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
//*> \brief \b STRSM 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE STRSM(SIDE,UPLO,TRANSA,DIAG,M,N,ALPHA,A,LDA,B,LDB) 
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
//*> STRSM  solves one of the matrix equations 
//*> 
//*>    op( A )*X = alpha*B,   or   X*op( A ) = alpha*B, 
//*> 
//*> where alpha is a scalar, X and B are m by n matrices, A is a unit, or 
//*> non-unit,  upper or lower triangular matrix  and  op( A )  is one  of 
//*> 
//*>    op( A ) = A   or   op( A ) = A**T. 
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
//*>          A is REAL array, dimension ( LDA, k ), 
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
//*>          B is REAL array, dimension ( LDB, N ) 
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

	 
	public static void _ieiywhin(FString _m2cn2gjg, FString _9wyre9zc, FString _742vrzth, FString _2scffxp3, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Single _r7cfteg3, Single* _vxfgpup9, ref Int32 _ocv8fk5c, Single* _p9n405a5, ref Int32 _ly9opahg)
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
			
			_ut9qalzx("STRSM " ,ref _gro5yvfo );
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
				System.Int32 __81fgg2dlsvn1548 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1548 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1548;
				for (__81fgg2count1548 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1548 + __81fgg2step1548) / __81fgg2step1548)), _znpjgsef = __81fgg2dlsvn1548; __81fgg2count1548 != 0; __81fgg2count1548--, _znpjgsef += (__81fgg2step1548)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn1549 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1549 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1549;
						for (__81fgg2count1549 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1549 + __81fgg2step1549) / __81fgg2step1549)), _b5p6od9s = __81fgg2dlsvn1549; __81fgg2count1549 != 0; __81fgg2count1549--, _b5p6od9s += (__81fgg2step1549)) {

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
						System.Int32 __81fgg2dlsvn1550 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1550 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1550;
						for (__81fgg2count1550 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1550 + __81fgg2step1550) / __81fgg2step1550)), _znpjgsef = __81fgg2dlsvn1550; __81fgg2count1550 != 0; __81fgg2count1550--, _znpjgsef += (__81fgg2step1550)) {

						{
							
							if (_r7cfteg3 != _kxg5drh2)
							{
								
								{
									System.Int32 __81fgg2dlsvn1551 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1551 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1551;
									for (__81fgg2count1551 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1551 + __81fgg2step1551) / __81fgg2step1551)), _b5p6od9s = __81fgg2dlsvn1551; __81fgg2count1551 != 0; __81fgg2count1551--, _b5p6od9s += (__81fgg2step1551)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
Mark30:;
										// continue
									}
																		}								}
							}
							
							{
								System.Int32 __81fgg2dlsvn1552 = (System.Int32)(_ev4xhht5);
								System.Int32 __81fgg2step1552 = (System.Int32)((int)-1);
								System.Int32 __81fgg2count1552;
								for (__81fgg2count1552 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1552 + __81fgg2step1552) / __81fgg2step1552)), _umlkckdg = __81fgg2dlsvn1552; __81fgg2count1552 != 0; __81fgg2count1552--, _umlkckdg += (__81fgg2step1552)) {

								{
									
									if (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) != _d0547bi2)
									{
										
										if (_rcjmgxm4)
										*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) / *(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
										{
											System.Int32 __81fgg2dlsvn1553 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1553 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1553;
											for (__81fgg2count1553 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg - (int)1) - __81fgg2dlsvn1553 + __81fgg2step1553) / __81fgg2step1553)), _b5p6od9s = __81fgg2dlsvn1553; __81fgg2count1553 != 0; __81fgg2count1553--, _b5p6od9s += (__81fgg2step1553)) {

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
						System.Int32 __81fgg2dlsvn1554 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1554 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1554;
						for (__81fgg2count1554 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1554 + __81fgg2step1554) / __81fgg2step1554)), _znpjgsef = __81fgg2dlsvn1554; __81fgg2count1554 != 0; __81fgg2count1554--, _znpjgsef += (__81fgg2step1554)) {

						{
							
							if (_r7cfteg3 != _kxg5drh2)
							{
								
								{
									System.Int32 __81fgg2dlsvn1555 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1555 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1555;
									for (__81fgg2count1555 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1555 + __81fgg2step1555) / __81fgg2step1555)), _b5p6od9s = __81fgg2dlsvn1555; __81fgg2count1555 != 0; __81fgg2count1555--, _b5p6od9s += (__81fgg2step1555)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
Mark70:;
										// continue
									}
																		}								}
							}
							
							{
								System.Int32 __81fgg2dlsvn1556 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1556 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1556;
								for (__81fgg2count1556 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1556 + __81fgg2step1556) / __81fgg2step1556)), _umlkckdg = __81fgg2dlsvn1556; __81fgg2count1556 != 0; __81fgg2count1556--, _umlkckdg += (__81fgg2step1556)) {

								{
									
									if (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) != _d0547bi2)
									{
										
										if (_rcjmgxm4)
										*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) / *(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
										{
											System.Int32 __81fgg2dlsvn1557 = (System.Int32)((_umlkckdg + (int)1));
											const System.Int32 __81fgg2step1557 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1557;
											for (__81fgg2count1557 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1557 + __81fgg2step1557) / __81fgg2step1557)), _b5p6od9s = __81fgg2dlsvn1557; __81fgg2count1557 != 0; __81fgg2count1557--, _b5p6od9s += (__81fgg2step1557)) {

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
				//*           Form  B := alpha*inv( A**T )*B. 
				//* 
				
				if (_l08igmvf)
				{
					
					{
						System.Int32 __81fgg2dlsvn1558 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1558 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1558;
						for (__81fgg2count1558 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1558 + __81fgg2step1558) / __81fgg2step1558)), _znpjgsef = __81fgg2dlsvn1558; __81fgg2count1558 != 0; __81fgg2count1558--, _znpjgsef += (__81fgg2step1558)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1559 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1559 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1559;
								for (__81fgg2count1559 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1559 + __81fgg2step1559) / __81fgg2step1559)), _b5p6od9s = __81fgg2dlsvn1559; __81fgg2count1559 != 0; __81fgg2count1559--, _b5p6od9s += (__81fgg2step1559)) {

								{
									
									_1ajfmh55 = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
									{
										System.Int32 __81fgg2dlsvn1560 = (System.Int32)((int)1);
										const System.Int32 __81fgg2step1560 = (System.Int32)((int)1);
										System.Int32 __81fgg2count1560;
										for (__81fgg2count1560 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s - (int)1) - __81fgg2dlsvn1560 + __81fgg2step1560) / __81fgg2step1560)), _umlkckdg = __81fgg2dlsvn1560; __81fgg2count1560 != 0; __81fgg2count1560--, _umlkckdg += (__81fgg2step1560)) {

										{
											
											_1ajfmh55 = (_1ajfmh55 - (*(_vxfgpup9+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) * *(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
Mark110:;
											// continue
										}
																				}									}
									if (_rcjmgxm4)
									_1ajfmh55 = (_1ajfmh55 / *(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)));
									*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = _1ajfmh55;
Mark120:;
									// continue
								}
																}							}
Mark130:;
							// continue
						}
												}					}
				}
				else
				{
					
					{
						System.Int32 __81fgg2dlsvn1561 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1561 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1561;
						for (__81fgg2count1561 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1561 + __81fgg2step1561) / __81fgg2step1561)), _znpjgsef = __81fgg2dlsvn1561; __81fgg2count1561 != 0; __81fgg2count1561--, _znpjgsef += (__81fgg2step1561)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1562 = (System.Int32)(_ev4xhht5);
								System.Int32 __81fgg2step1562 = (System.Int32)((int)-1);
								System.Int32 __81fgg2count1562;
								for (__81fgg2count1562 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1562 + __81fgg2step1562) / __81fgg2step1562)), _b5p6od9s = __81fgg2dlsvn1562; __81fgg2count1562 != 0; __81fgg2count1562--, _b5p6od9s += (__81fgg2step1562)) {

								{
									
									_1ajfmh55 = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
									{
										System.Int32 __81fgg2dlsvn1563 = (System.Int32)((_b5p6od9s + (int)1));
										const System.Int32 __81fgg2step1563 = (System.Int32)((int)1);
										System.Int32 __81fgg2count1563;
										for (__81fgg2count1563 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1563 + __81fgg2step1563) / __81fgg2step1563)), _umlkckdg = __81fgg2dlsvn1563; __81fgg2count1563 != 0; __81fgg2count1563--, _umlkckdg += (__81fgg2step1563)) {

										{
											
											_1ajfmh55 = (_1ajfmh55 - (*(_vxfgpup9+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) * *(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
Mark140:;
											// continue
										}
																				}									}
									if (_rcjmgxm4)
									_1ajfmh55 = (_1ajfmh55 / *(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)));
									*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = _1ajfmh55;
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
				//*           Form  B := alpha*B*inv( A ). 
				//* 
				
				if (_l08igmvf)
				{
					
					{
						System.Int32 __81fgg2dlsvn1564 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1564 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1564;
						for (__81fgg2count1564 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1564 + __81fgg2step1564) / __81fgg2step1564)), _znpjgsef = __81fgg2dlsvn1564; __81fgg2count1564 != 0; __81fgg2count1564--, _znpjgsef += (__81fgg2step1564)) {

						{
							
							if (_r7cfteg3 != _kxg5drh2)
							{
								
								{
									System.Int32 __81fgg2dlsvn1565 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1565 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1565;
									for (__81fgg2count1565 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1565 + __81fgg2step1565) / __81fgg2step1565)), _b5p6od9s = __81fgg2dlsvn1565; __81fgg2count1565 != 0; __81fgg2count1565--, _b5p6od9s += (__81fgg2step1565)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
Mark170:;
										// continue
									}
																		}								}
							}
							
							{
								System.Int32 __81fgg2dlsvn1566 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1566 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1566;
								for (__81fgg2count1566 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn1566 + __81fgg2step1566) / __81fgg2step1566)), _umlkckdg = __81fgg2dlsvn1566; __81fgg2count1566 != 0; __81fgg2count1566--, _umlkckdg += (__81fgg2step1566)) {

								{
									
									if (*(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										{
											System.Int32 __81fgg2dlsvn1567 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1567 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1567;
											for (__81fgg2count1567 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1567 + __81fgg2step1567) / __81fgg2step1567)), _b5p6od9s = __81fgg2dlsvn1567; __81fgg2count1567 != 0; __81fgg2count1567--, _b5p6od9s += (__81fgg2step1567)) {

											{
												
												*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) - (*(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg))));
Mark180:;
												// continue
											}
																						}										}
									}
									
Mark190:;
									// continue
								}
																}							}
							if (_rcjmgxm4)
							{
								
								_1ajfmh55 = (_kxg5drh2 / *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
								{
									System.Int32 __81fgg2dlsvn1568 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1568 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1568;
									for (__81fgg2count1568 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1568 + __81fgg2step1568) / __81fgg2step1568)), _b5p6od9s = __81fgg2dlsvn1568; __81fgg2count1568 != 0; __81fgg2count1568--, _b5p6od9s += (__81fgg2step1568)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
Mark200:;
										// continue
									}
																		}								}
							}
							
Mark210:;
							// continue
						}
												}					}
				}
				else
				{
					
					{
						System.Int32 __81fgg2dlsvn1569 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step1569 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count1569;
						for (__81fgg2count1569 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1569 + __81fgg2step1569) / __81fgg2step1569)), _znpjgsef = __81fgg2dlsvn1569; __81fgg2count1569 != 0; __81fgg2count1569--, _znpjgsef += (__81fgg2step1569)) {

						{
							
							if (_r7cfteg3 != _kxg5drh2)
							{
								
								{
									System.Int32 __81fgg2dlsvn1570 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1570 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1570;
									for (__81fgg2count1570 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1570 + __81fgg2step1570) / __81fgg2step1570)), _b5p6od9s = __81fgg2dlsvn1570; __81fgg2count1570 != 0; __81fgg2count1570--, _b5p6od9s += (__81fgg2step1570)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
Mark220:;
										// continue
									}
																		}								}
							}
							
							{
								System.Int32 __81fgg2dlsvn1571 = (System.Int32)((_znpjgsef + (int)1));
								const System.Int32 __81fgg2step1571 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1571;
								for (__81fgg2count1571 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1571 + __81fgg2step1571) / __81fgg2step1571)), _umlkckdg = __81fgg2dlsvn1571; __81fgg2count1571 != 0; __81fgg2count1571--, _umlkckdg += (__81fgg2step1571)) {

								{
									
									if (*(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										{
											System.Int32 __81fgg2dlsvn1572 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1572 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1572;
											for (__81fgg2count1572 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1572 + __81fgg2step1572) / __81fgg2step1572)), _b5p6od9s = __81fgg2dlsvn1572; __81fgg2count1572 != 0; __81fgg2count1572--, _b5p6od9s += (__81fgg2step1572)) {

											{
												
												*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) - (*(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg))));
Mark230:;
												// continue
											}
																						}										}
									}
									
Mark240:;
									// continue
								}
																}							}
							if (_rcjmgxm4)
							{
								
								_1ajfmh55 = (_kxg5drh2 / *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
								{
									System.Int32 __81fgg2dlsvn1573 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1573 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1573;
									for (__81fgg2count1573 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1573 + __81fgg2step1573) / __81fgg2step1573)), _b5p6od9s = __81fgg2dlsvn1573; __81fgg2count1573 != 0; __81fgg2count1573--, _b5p6od9s += (__81fgg2step1573)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
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
				
			}
			else
			{
				//* 
				//*           Form  B := alpha*B*inv( A**T ). 
				//* 
				
				if (_l08igmvf)
				{
					
					{
						System.Int32 __81fgg2dlsvn1574 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step1574 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count1574;
						for (__81fgg2count1574 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1574 + __81fgg2step1574) / __81fgg2step1574)), _umlkckdg = __81fgg2dlsvn1574; __81fgg2count1574 != 0; __81fgg2count1574--, _umlkckdg += (__81fgg2step1574)) {

						{
							
							if (_rcjmgxm4)
							{
								
								_1ajfmh55 = (_kxg5drh2 / *(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
								{
									System.Int32 __81fgg2dlsvn1575 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1575 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1575;
									for (__81fgg2count1575 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1575 + __81fgg2step1575) / __81fgg2step1575)), _b5p6od9s = __81fgg2dlsvn1575; __81fgg2count1575 != 0; __81fgg2count1575--, _b5p6od9s += (__81fgg2step1575)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)) = (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)));
Mark270:;
										// continue
									}
																		}								}
							}
							
							{
								System.Int32 __81fgg2dlsvn1576 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1576 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1576;
								for (__81fgg2count1576 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg - (int)1) - __81fgg2dlsvn1576 + __81fgg2step1576) / __81fgg2step1576)), _znpjgsef = __81fgg2dlsvn1576; __81fgg2count1576 != 0; __81fgg2count1576--, _znpjgsef += (__81fgg2step1576)) {

								{
									
									if (*(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										_1ajfmh55 = *(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c));
										{
											System.Int32 __81fgg2dlsvn1577 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1577 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1577;
											for (__81fgg2count1577 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1577 + __81fgg2step1577) / __81fgg2step1577)), _b5p6od9s = __81fgg2dlsvn1577; __81fgg2count1577 != 0; __81fgg2count1577--, _b5p6od9s += (__81fgg2step1577)) {

											{
												
												*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) - (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg))));
Mark280:;
												// continue
											}
																						}										}
									}
									
Mark290:;
									// continue
								}
																}							}
							if (_r7cfteg3 != _kxg5drh2)
							{
								
								{
									System.Int32 __81fgg2dlsvn1578 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1578 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1578;
									for (__81fgg2count1578 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1578 + __81fgg2step1578) / __81fgg2step1578)), _b5p6od9s = __81fgg2dlsvn1578; __81fgg2count1578 != 0; __81fgg2count1578--, _b5p6od9s += (__81fgg2step1578)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)) = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)));
Mark300:;
										// continue
									}
																		}								}
							}
							
Mark310:;
							// continue
						}
												}					}
				}
				else
				{
					
					{
						System.Int32 __81fgg2dlsvn1579 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1579 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1579;
						for (__81fgg2count1579 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1579 + __81fgg2step1579) / __81fgg2step1579)), _umlkckdg = __81fgg2dlsvn1579; __81fgg2count1579 != 0; __81fgg2count1579--, _umlkckdg += (__81fgg2step1579)) {

						{
							
							if (_rcjmgxm4)
							{
								
								_1ajfmh55 = (_kxg5drh2 / *(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
								{
									System.Int32 __81fgg2dlsvn1580 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1580 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1580;
									for (__81fgg2count1580 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1580 + __81fgg2step1580) / __81fgg2step1580)), _b5p6od9s = __81fgg2dlsvn1580; __81fgg2count1580 != 0; __81fgg2count1580--, _b5p6od9s += (__81fgg2step1580)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)) = (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)));
Mark320:;
										// continue
									}
																		}								}
							}
							
							{
								System.Int32 __81fgg2dlsvn1581 = (System.Int32)((_umlkckdg + (int)1));
								const System.Int32 __81fgg2step1581 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1581;
								for (__81fgg2count1581 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1581 + __81fgg2step1581) / __81fgg2step1581)), _znpjgsef = __81fgg2dlsvn1581; __81fgg2count1581 != 0; __81fgg2count1581--, _znpjgsef += (__81fgg2step1581)) {

								{
									
									if (*(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										_1ajfmh55 = *(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c));
										{
											System.Int32 __81fgg2dlsvn1582 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1582 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1582;
											for (__81fgg2count1582 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1582 + __81fgg2step1582) / __81fgg2step1582)), _b5p6od9s = __81fgg2dlsvn1582; __81fgg2count1582 != 0; __81fgg2count1582--, _b5p6od9s += (__81fgg2step1582)) {

											{
												
												*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) - (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg))));
Mark330:;
												// continue
											}
																						}										}
									}
									
Mark340:;
									// continue
								}
																}							}
							if (_r7cfteg3 != _kxg5drh2)
							{
								
								{
									System.Int32 __81fgg2dlsvn1583 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1583 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1583;
									for (__81fgg2count1583 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1583 + __81fgg2step1583) / __81fgg2step1583)), _b5p6od9s = __81fgg2dlsvn1583; __81fgg2count1583 != 0; __81fgg2count1583--, _b5p6od9s += (__81fgg2step1583)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)) = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)));
Mark350:;
										// continue
									}
																		}								}
							}
							
Mark360:;
							// continue
						}
												}					}
				}
				
			}
			
		}
		//* 
		
		return;//* 
		//*     End of STRSM . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
