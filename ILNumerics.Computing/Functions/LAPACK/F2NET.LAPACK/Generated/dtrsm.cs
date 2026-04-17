
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
//*> \brief \b DTRSM 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DTRSM(SIDE,UPLO,TRANSA,DIAG,M,N,ALPHA,A,LDA,B,LDB) 
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
//*> DTRSM  solves one of the matrix equations 
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
//*>          ALPHA is DOUBLE PRECISION. 
//*>           On entry,  ALPHA specifies the scalar  alpha. When  alpha is 
//*>           zero then  A is not referenced and  B need not be set before 
//*>           entry. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is DOUBLE PRECISION array, dimension ( LDA, k ), 
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
//*>          B is DOUBLE PRECISION array, dimension ( LDB, N ) 
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
//*> \ingroup double_blas_level3 
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

	 
	public static void _isrbppbh(FString _m2cn2gjg, FString _9wyre9zc, FString _742vrzth, FString _2scffxp3, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Double _r7cfteg3, Double* _vxfgpup9, ref Int32 _ocv8fk5c, Double* _p9n405a5, ref Int32 _ly9opahg)
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
			
			_ut9qalzx("DTRSM " ,ref _gro5yvfo );
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
				System.Int32 __81fgg2dlsvn1486 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1486 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1486;
				for (__81fgg2count1486 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1486 + __81fgg2step1486) / __81fgg2step1486)), _znpjgsef = __81fgg2dlsvn1486; __81fgg2count1486 != 0; __81fgg2count1486--, _znpjgsef += (__81fgg2step1486)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn1487 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1487 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1487;
						for (__81fgg2count1487 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1487 + __81fgg2step1487) / __81fgg2step1487)), _b5p6od9s = __81fgg2dlsvn1487; __81fgg2count1487 != 0; __81fgg2count1487--, _b5p6od9s += (__81fgg2step1487)) {

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
						System.Int32 __81fgg2dlsvn1488 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1488 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1488;
						for (__81fgg2count1488 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1488 + __81fgg2step1488) / __81fgg2step1488)), _znpjgsef = __81fgg2dlsvn1488; __81fgg2count1488 != 0; __81fgg2count1488--, _znpjgsef += (__81fgg2step1488)) {

						{
							
							if (_r7cfteg3 != _kxg5drh2)
							{
								
								{
									System.Int32 __81fgg2dlsvn1489 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1489 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1489;
									for (__81fgg2count1489 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1489 + __81fgg2step1489) / __81fgg2step1489)), _b5p6od9s = __81fgg2dlsvn1489; __81fgg2count1489 != 0; __81fgg2count1489--, _b5p6od9s += (__81fgg2step1489)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
Mark30:;
										// continue
									}
																		}								}
							}
							
							{
								System.Int32 __81fgg2dlsvn1490 = (System.Int32)(_ev4xhht5);
								System.Int32 __81fgg2step1490 = (System.Int32)((int)-1);
								System.Int32 __81fgg2count1490;
								for (__81fgg2count1490 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1490 + __81fgg2step1490) / __81fgg2step1490)), _umlkckdg = __81fgg2dlsvn1490; __81fgg2count1490 != 0; __81fgg2count1490--, _umlkckdg += (__81fgg2step1490)) {

								{
									
									if (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) != _d0547bi2)
									{
										
										if (_rcjmgxm4)
										*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) / *(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
										{
											System.Int32 __81fgg2dlsvn1491 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1491 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1491;
											for (__81fgg2count1491 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg - (int)1) - __81fgg2dlsvn1491 + __81fgg2step1491) / __81fgg2step1491)), _b5p6od9s = __81fgg2dlsvn1491; __81fgg2count1491 != 0; __81fgg2count1491--, _b5p6od9s += (__81fgg2step1491)) {

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
						System.Int32 __81fgg2dlsvn1492 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1492 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1492;
						for (__81fgg2count1492 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1492 + __81fgg2step1492) / __81fgg2step1492)), _znpjgsef = __81fgg2dlsvn1492; __81fgg2count1492 != 0; __81fgg2count1492--, _znpjgsef += (__81fgg2step1492)) {

						{
							
							if (_r7cfteg3 != _kxg5drh2)
							{
								
								{
									System.Int32 __81fgg2dlsvn1493 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1493 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1493;
									for (__81fgg2count1493 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1493 + __81fgg2step1493) / __81fgg2step1493)), _b5p6od9s = __81fgg2dlsvn1493; __81fgg2count1493 != 0; __81fgg2count1493--, _b5p6od9s += (__81fgg2step1493)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
Mark70:;
										// continue
									}
																		}								}
							}
							
							{
								System.Int32 __81fgg2dlsvn1494 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1494 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1494;
								for (__81fgg2count1494 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1494 + __81fgg2step1494) / __81fgg2step1494)), _umlkckdg = __81fgg2dlsvn1494; __81fgg2count1494 != 0; __81fgg2count1494--, _umlkckdg += (__81fgg2step1494)) {

								{
									
									if (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) != _d0547bi2)
									{
										
										if (_rcjmgxm4)
										*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) / *(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
										{
											System.Int32 __81fgg2dlsvn1495 = (System.Int32)((_umlkckdg + (int)1));
											const System.Int32 __81fgg2step1495 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1495;
											for (__81fgg2count1495 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1495 + __81fgg2step1495) / __81fgg2step1495)), _b5p6od9s = __81fgg2dlsvn1495; __81fgg2count1495 != 0; __81fgg2count1495--, _b5p6od9s += (__81fgg2step1495)) {

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
						System.Int32 __81fgg2dlsvn1496 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1496 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1496;
						for (__81fgg2count1496 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1496 + __81fgg2step1496) / __81fgg2step1496)), _znpjgsef = __81fgg2dlsvn1496; __81fgg2count1496 != 0; __81fgg2count1496--, _znpjgsef += (__81fgg2step1496)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1497 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1497 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1497;
								for (__81fgg2count1497 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1497 + __81fgg2step1497) / __81fgg2step1497)), _b5p6od9s = __81fgg2dlsvn1497; __81fgg2count1497 != 0; __81fgg2count1497--, _b5p6od9s += (__81fgg2step1497)) {

								{
									
									_1ajfmh55 = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
									{
										System.Int32 __81fgg2dlsvn1498 = (System.Int32)((int)1);
										const System.Int32 __81fgg2step1498 = (System.Int32)((int)1);
										System.Int32 __81fgg2count1498;
										for (__81fgg2count1498 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s - (int)1) - __81fgg2dlsvn1498 + __81fgg2step1498) / __81fgg2step1498)), _umlkckdg = __81fgg2dlsvn1498; __81fgg2count1498 != 0; __81fgg2count1498--, _umlkckdg += (__81fgg2step1498)) {

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
						System.Int32 __81fgg2dlsvn1499 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1499 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1499;
						for (__81fgg2count1499 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1499 + __81fgg2step1499) / __81fgg2step1499)), _znpjgsef = __81fgg2dlsvn1499; __81fgg2count1499 != 0; __81fgg2count1499--, _znpjgsef += (__81fgg2step1499)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1500 = (System.Int32)(_ev4xhht5);
								System.Int32 __81fgg2step1500 = (System.Int32)((int)-1);
								System.Int32 __81fgg2count1500;
								for (__81fgg2count1500 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1500 + __81fgg2step1500) / __81fgg2step1500)), _b5p6od9s = __81fgg2dlsvn1500; __81fgg2count1500 != 0; __81fgg2count1500--, _b5p6od9s += (__81fgg2step1500)) {

								{
									
									_1ajfmh55 = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
									{
										System.Int32 __81fgg2dlsvn1501 = (System.Int32)((_b5p6od9s + (int)1));
										const System.Int32 __81fgg2step1501 = (System.Int32)((int)1);
										System.Int32 __81fgg2count1501;
										for (__81fgg2count1501 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1501 + __81fgg2step1501) / __81fgg2step1501)), _umlkckdg = __81fgg2dlsvn1501; __81fgg2count1501 != 0; __81fgg2count1501--, _umlkckdg += (__81fgg2step1501)) {

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
						System.Int32 __81fgg2dlsvn1502 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1502 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1502;
						for (__81fgg2count1502 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1502 + __81fgg2step1502) / __81fgg2step1502)), _znpjgsef = __81fgg2dlsvn1502; __81fgg2count1502 != 0; __81fgg2count1502--, _znpjgsef += (__81fgg2step1502)) {

						{
							
							if (_r7cfteg3 != _kxg5drh2)
							{
								
								{
									System.Int32 __81fgg2dlsvn1503 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1503 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1503;
									for (__81fgg2count1503 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1503 + __81fgg2step1503) / __81fgg2step1503)), _b5p6od9s = __81fgg2dlsvn1503; __81fgg2count1503 != 0; __81fgg2count1503--, _b5p6od9s += (__81fgg2step1503)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
Mark170:;
										// continue
									}
																		}								}
							}
							
							{
								System.Int32 __81fgg2dlsvn1504 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1504 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1504;
								for (__81fgg2count1504 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn1504 + __81fgg2step1504) / __81fgg2step1504)), _umlkckdg = __81fgg2dlsvn1504; __81fgg2count1504 != 0; __81fgg2count1504--, _umlkckdg += (__81fgg2step1504)) {

								{
									
									if (*(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										{
											System.Int32 __81fgg2dlsvn1505 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1505 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1505;
											for (__81fgg2count1505 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1505 + __81fgg2step1505) / __81fgg2step1505)), _b5p6od9s = __81fgg2dlsvn1505; __81fgg2count1505 != 0; __81fgg2count1505--, _b5p6od9s += (__81fgg2step1505)) {

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
									System.Int32 __81fgg2dlsvn1506 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1506 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1506;
									for (__81fgg2count1506 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1506 + __81fgg2step1506) / __81fgg2step1506)), _b5p6od9s = __81fgg2dlsvn1506; __81fgg2count1506 != 0; __81fgg2count1506--, _b5p6od9s += (__81fgg2step1506)) {

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
						System.Int32 __81fgg2dlsvn1507 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step1507 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count1507;
						for (__81fgg2count1507 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1507 + __81fgg2step1507) / __81fgg2step1507)), _znpjgsef = __81fgg2dlsvn1507; __81fgg2count1507 != 0; __81fgg2count1507--, _znpjgsef += (__81fgg2step1507)) {

						{
							
							if (_r7cfteg3 != _kxg5drh2)
							{
								
								{
									System.Int32 __81fgg2dlsvn1508 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1508 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1508;
									for (__81fgg2count1508 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1508 + __81fgg2step1508) / __81fgg2step1508)), _b5p6od9s = __81fgg2dlsvn1508; __81fgg2count1508 != 0; __81fgg2count1508--, _b5p6od9s += (__81fgg2step1508)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
Mark220:;
										// continue
									}
																		}								}
							}
							
							{
								System.Int32 __81fgg2dlsvn1509 = (System.Int32)((_znpjgsef + (int)1));
								const System.Int32 __81fgg2step1509 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1509;
								for (__81fgg2count1509 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1509 + __81fgg2step1509) / __81fgg2step1509)), _umlkckdg = __81fgg2dlsvn1509; __81fgg2count1509 != 0; __81fgg2count1509--, _umlkckdg += (__81fgg2step1509)) {

								{
									
									if (*(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										{
											System.Int32 __81fgg2dlsvn1510 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1510 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1510;
											for (__81fgg2count1510 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1510 + __81fgg2step1510) / __81fgg2step1510)), _b5p6od9s = __81fgg2dlsvn1510; __81fgg2count1510 != 0; __81fgg2count1510--, _b5p6od9s += (__81fgg2step1510)) {

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
									System.Int32 __81fgg2dlsvn1511 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1511 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1511;
									for (__81fgg2count1511 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1511 + __81fgg2step1511) / __81fgg2step1511)), _b5p6od9s = __81fgg2dlsvn1511; __81fgg2count1511 != 0; __81fgg2count1511--, _b5p6od9s += (__81fgg2step1511)) {

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
						System.Int32 __81fgg2dlsvn1512 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step1512 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count1512;
						for (__81fgg2count1512 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1512 + __81fgg2step1512) / __81fgg2step1512)), _umlkckdg = __81fgg2dlsvn1512; __81fgg2count1512 != 0; __81fgg2count1512--, _umlkckdg += (__81fgg2step1512)) {

						{
							
							if (_rcjmgxm4)
							{
								
								_1ajfmh55 = (_kxg5drh2 / *(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
								{
									System.Int32 __81fgg2dlsvn1513 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1513 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1513;
									for (__81fgg2count1513 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1513 + __81fgg2step1513) / __81fgg2step1513)), _b5p6od9s = __81fgg2dlsvn1513; __81fgg2count1513 != 0; __81fgg2count1513--, _b5p6od9s += (__81fgg2step1513)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)) = (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)));
Mark270:;
										// continue
									}
																		}								}
							}
							
							{
								System.Int32 __81fgg2dlsvn1514 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1514 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1514;
								for (__81fgg2count1514 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg - (int)1) - __81fgg2dlsvn1514 + __81fgg2step1514) / __81fgg2step1514)), _znpjgsef = __81fgg2dlsvn1514; __81fgg2count1514 != 0; __81fgg2count1514--, _znpjgsef += (__81fgg2step1514)) {

								{
									
									if (*(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										_1ajfmh55 = *(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c));
										{
											System.Int32 __81fgg2dlsvn1515 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1515 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1515;
											for (__81fgg2count1515 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1515 + __81fgg2step1515) / __81fgg2step1515)), _b5p6od9s = __81fgg2dlsvn1515; __81fgg2count1515 != 0; __81fgg2count1515--, _b5p6od9s += (__81fgg2step1515)) {

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
									System.Int32 __81fgg2dlsvn1516 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1516 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1516;
									for (__81fgg2count1516 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1516 + __81fgg2step1516) / __81fgg2step1516)), _b5p6od9s = __81fgg2dlsvn1516; __81fgg2count1516 != 0; __81fgg2count1516--, _b5p6od9s += (__81fgg2step1516)) {

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
						System.Int32 __81fgg2dlsvn1517 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1517 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1517;
						for (__81fgg2count1517 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1517 + __81fgg2step1517) / __81fgg2step1517)), _umlkckdg = __81fgg2dlsvn1517; __81fgg2count1517 != 0; __81fgg2count1517--, _umlkckdg += (__81fgg2step1517)) {

						{
							
							if (_rcjmgxm4)
							{
								
								_1ajfmh55 = (_kxg5drh2 / *(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
								{
									System.Int32 __81fgg2dlsvn1518 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1518 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1518;
									for (__81fgg2count1518 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1518 + __81fgg2step1518) / __81fgg2step1518)), _b5p6od9s = __81fgg2dlsvn1518; __81fgg2count1518 != 0; __81fgg2count1518--, _b5p6od9s += (__81fgg2step1518)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)) = (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)));
Mark320:;
										// continue
									}
																		}								}
							}
							
							{
								System.Int32 __81fgg2dlsvn1519 = (System.Int32)((_umlkckdg + (int)1));
								const System.Int32 __81fgg2step1519 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1519;
								for (__81fgg2count1519 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1519 + __81fgg2step1519) / __81fgg2step1519)), _znpjgsef = __81fgg2dlsvn1519; __81fgg2count1519 != 0; __81fgg2count1519--, _znpjgsef += (__81fgg2step1519)) {

								{
									
									if (*(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										_1ajfmh55 = *(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c));
										{
											System.Int32 __81fgg2dlsvn1520 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1520 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1520;
											for (__81fgg2count1520 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1520 + __81fgg2step1520) / __81fgg2step1520)), _b5p6od9s = __81fgg2dlsvn1520; __81fgg2count1520 != 0; __81fgg2count1520--, _b5p6od9s += (__81fgg2step1520)) {

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
									System.Int32 __81fgg2dlsvn1521 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1521 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1521;
									for (__81fgg2count1521 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1521 + __81fgg2step1521) / __81fgg2step1521)), _b5p6od9s = __81fgg2dlsvn1521; __81fgg2count1521 != 0; __81fgg2count1521--, _b5p6od9s += (__81fgg2step1521)) {

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
		//*     End of DTRSM . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
