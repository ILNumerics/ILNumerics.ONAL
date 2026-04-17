
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
//*> \brief \b DSYMM 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DSYMM(SIDE,UPLO,M,N,ALPHA,A,LDA,B,LDB,BETA,C,LDC) 
//* 
//*       .. Scalar Arguments .. 
//*       DOUBLE PRECISION ALPHA,BETA 
//*       INTEGER LDA,LDB,LDC,M,N 
//*       CHARACTER SIDE,UPLO 
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
//*> DSYMM  performs one of the matrix-matrix operations 
//*> 
//*>    C := alpha*A*B + beta*C, 
//*> 
//*> or 
//*> 
//*>    C := alpha*B*A + beta*C, 
//*> 
//*> where alpha and beta are scalars,  A is a symmetric matrix and  B and 
//*> C are  m by n matrices. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] SIDE 
//*> \verbatim 
//*>          SIDE is CHARACTER*1 
//*>           On entry,  SIDE  specifies whether  the  symmetric matrix  A 
//*>           appears on the  left or right  in the  operation as follows: 
//*> 
//*>              SIDE = 'L' or 'l'   C := alpha*A*B + beta*C, 
//*> 
//*>              SIDE = 'R' or 'r'   C := alpha*B*A + beta*C, 
//*> \endverbatim 
//*> 
//*> \param[in] UPLO 
//*> \verbatim 
//*>          UPLO is CHARACTER*1 
//*>           On  entry,   UPLO  specifies  whether  the  upper  or  lower 
//*>           triangular  part  of  the  symmetric  matrix   A  is  to  be 
//*>           referenced as follows: 
//*> 
//*>              UPLO = 'U' or 'u'   Only the upper triangular part of the 
//*>                                  symmetric matrix is to be referenced. 
//*> 
//*>              UPLO = 'L' or 'l'   Only the lower triangular part of the 
//*>                                  symmetric matrix is to be referenced. 
//*> \endverbatim 
//*> 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>           On entry,  M  specifies the number of rows of the matrix  C. 
//*>           M  must be at least zero. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>           On entry, N specifies the number of columns of the matrix C. 
//*>           N  must be at least zero. 
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
//*>           m  when  SIDE = 'L' or 'l'  and is  n otherwise. 
//*>           Before entry  with  SIDE = 'L' or 'l',  the  m by m  part of 
//*>           the array  A  must contain the  symmetric matrix,  such that 
//*>           when  UPLO = 'U' or 'u', the leading m by m upper triangular 
//*>           part of the array  A  must contain the upper triangular part 
//*>           of the  symmetric matrix and the  strictly  lower triangular 
//*>           part of  A  is not referenced,  and when  UPLO = 'L' or 'l', 
//*>           the leading  m by m  lower triangular part  of the  array  A 
//*>           must  contain  the  lower triangular part  of the  symmetric 
//*>           matrix and the  strictly upper triangular part of  A  is not 
//*>           referenced. 
//*>           Before entry  with  SIDE = 'R' or 'r',  the  n by n  part of 
//*>           the array  A  must contain the  symmetric matrix,  such that 
//*>           when  UPLO = 'U' or 'u', the leading n by n upper triangular 
//*>           part of the array  A  must contain the upper triangular part 
//*>           of the  symmetric matrix and the  strictly  lower triangular 
//*>           part of  A  is not referenced,  and when  UPLO = 'L' or 'l', 
//*>           the leading  n by n  lower triangular part  of the  array  A 
//*>           must  contain  the  lower triangular part  of the  symmetric 
//*>           matrix and the  strictly upper triangular part of  A  is not 
//*>           referenced. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>           On entry, LDA specifies the first dimension of A as declared 
//*>           in the calling (sub) program.  When  SIDE = 'L' or 'l'  then 
//*>           LDA must be at least  max( 1, m ), otherwise  LDA must be at 
//*>           least  max( 1, n ). 
//*> \endverbatim 
//*> 
//*> \param[in] B 
//*> \verbatim 
//*>          B is DOUBLE PRECISION array, dimension ( LDB, N ) 
//*>           Before entry, the leading  m by n part of the array  B  must 
//*>           contain the matrix B. 
//*> \endverbatim 
//*> 
//*> \param[in] LDB 
//*> \verbatim 
//*>          LDB is INTEGER 
//*>           On entry, LDB specifies the first dimension of B as declared 
//*>           in  the  calling  (sub)  program.   LDB  must  be  at  least 
//*>           max( 1, m ). 
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
//*>           On exit, the array  C  is overwritten by the  m by n updated 
//*>           matrix. 
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

	 
	public static void _36kg2k07(FString _m2cn2gjg, FString _9wyre9zc, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Double _r7cfteg3, Double* _vxfgpup9, ref Int32 _ocv8fk5c, Double* _p9n405a5, ref Int32 _ly9opahg, ref Double _bafcbx97, Double* _3crf0qn3, ref Int32 _1s3eymp4)
	{
#region variable declarations
Double _yc8h372p =  default;
Double _q3ig7mub =  default;
Int32 _b5p6od9s =  default;
Int32 _gro5yvfo =  default;
Int32 _znpjgsef =  default;
Int32 _umlkckdg =  default;
Int32 _o9a6qdux =  default;
Boolean _l08igmvf =  default;
Double _kxg5drh2 =  1d;
Double _d0547bi2 =  0d;
string fLanavab = default;
#endregion  variable declarations
_m2cn2gjg = _m2cn2gjg.Convert(1);
_9wyre9zc = _9wyre9zc.Convert(1);

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
		//*     Set NROWA as the number of rows of A. 
		//* 
		
		if (_w8y2rzgy(_m2cn2gjg ,"L" ))
		{
			
			_o9a6qdux = _ev4xhht5;
		}
		else
		{
			
			_o9a6qdux = _dxpq0xkr;
		}
		
		_l08igmvf = _w8y2rzgy(_9wyre9zc ,"U" );//* 
		//*     Test the input parameters. 
		//* 
		
		_gro5yvfo = (int)0;
		if ((!(_w8y2rzgy(_m2cn2gjg ,"L" ))) & (!(_w8y2rzgy(_m2cn2gjg ,"R" ))))
		{
			
			_gro5yvfo = (int)1;
		}
		else
		if ((!(_l08igmvf)) & (!(_w8y2rzgy(_9wyre9zc ,"L" ))))
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
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_o9a6qdux ))
		{
			
			_gro5yvfo = (int)7;
		}
		else
		if (_ly9opahg < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_ev4xhht5 ))
		{
			
			_gro5yvfo = (int)9;
		}
		else
		if (_1s3eymp4 < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_ev4xhht5 ))
		{
			
			_gro5yvfo = (int)12;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("DSYMM " ,ref _gro5yvfo );
			return;
		}
		//* 
		//*     Quick return if possible. 
		//* 
		
		if (((_ev4xhht5 == (int)0) | (_dxpq0xkr == (int)0)) | ((_r7cfteg3 == _d0547bi2) & (_bafcbx97 == _kxg5drh2)))
		return;//* 
		//*     And when  alpha.eq.zero. 
		//* 
		
		if (_r7cfteg3 == _d0547bi2)
		{
			
			if (_bafcbx97 == _d0547bi2)
			{
				
				{
					System.Int32 __81fgg2dlsvn3763 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3763 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3763;
					for (__81fgg2count3763 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3763 + __81fgg2step3763) / __81fgg2step3763)), _znpjgsef = __81fgg2dlsvn3763; __81fgg2count3763 != 0; __81fgg2count3763--, _znpjgsef += (__81fgg2step3763)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn3764 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3764 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3764;
							for (__81fgg2count3764 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3764 + __81fgg2step3764) / __81fgg2step3764)), _b5p6od9s = __81fgg2dlsvn3764; __81fgg2count3764 != 0; __81fgg2count3764--, _b5p6od9s += (__81fgg2step3764)) {

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
					System.Int32 __81fgg2dlsvn3765 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3765 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3765;
					for (__81fgg2count3765 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3765 + __81fgg2step3765) / __81fgg2step3765)), _znpjgsef = __81fgg2dlsvn3765; __81fgg2count3765 != 0; __81fgg2count3765--, _znpjgsef += (__81fgg2step3765)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn3766 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3766 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3766;
							for (__81fgg2count3766 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3766 + __81fgg2step3766) / __81fgg2step3766)), _b5p6od9s = __81fgg2dlsvn3766; __81fgg2count3766 != 0; __81fgg2count3766--, _b5p6od9s += (__81fgg2step3766)) {

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
		
		if (_w8y2rzgy(_m2cn2gjg ,"L" ))
		{
			//* 
			//*        Form  C := alpha*A*B + beta*C. 
			//* 
			
			if (_l08igmvf)
			{
				
				{
					System.Int32 __81fgg2dlsvn3767 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3767 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3767;
					for (__81fgg2count3767 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3767 + __81fgg2step3767) / __81fgg2step3767)), _znpjgsef = __81fgg2dlsvn3767; __81fgg2count3767 != 0; __81fgg2count3767--, _znpjgsef += (__81fgg2step3767)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn3768 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3768 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3768;
							for (__81fgg2count3768 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3768 + __81fgg2step3768) / __81fgg2step3768)), _b5p6od9s = __81fgg2dlsvn3768; __81fgg2count3768 != 0; __81fgg2count3768--, _b5p6od9s += (__81fgg2step3768)) {

							{
								
								_yc8h372p = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
								_q3ig7mub = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn3769 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step3769 = (System.Int32)((int)1);
									System.Int32 __81fgg2count3769;
									for (__81fgg2count3769 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s - (int)1) - __81fgg2dlsvn3769 + __81fgg2step3769) / __81fgg2step3769)), _umlkckdg = __81fgg2dlsvn3769; __81fgg2count3769 != 0; __81fgg2count3769--, _umlkckdg += (__81fgg2step3769)) {

									{
										
										*(_3crf0qn3+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) + (_yc8h372p * *(_vxfgpup9+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))));
										_q3ig7mub = (_q3ig7mub + (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) * *(_vxfgpup9+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))));
Mark50:;
										// continue
									}
																		}								}
								if (_bafcbx97 == _d0547bi2)
								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = ((_yc8h372p * *(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))) + (_r7cfteg3 * _q3ig7mub));
								}
								else
								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (((_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))) + (_yc8h372p * *(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)))) + (_r7cfteg3 * _q3ig7mub));
								}
								
Mark60:;
								// continue
							}
														}						}
Mark70:;
						// continue
					}
										}				}
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn3770 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3770 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3770;
					for (__81fgg2count3770 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3770 + __81fgg2step3770) / __81fgg2step3770)), _znpjgsef = __81fgg2dlsvn3770; __81fgg2count3770 != 0; __81fgg2count3770--, _znpjgsef += (__81fgg2step3770)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn3771 = (System.Int32)(_ev4xhht5);
							System.Int32 __81fgg2step3771 = (System.Int32)((int)-1);
							System.Int32 __81fgg2count3771;
							for (__81fgg2count3771 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn3771 + __81fgg2step3771) / __81fgg2step3771)), _b5p6od9s = __81fgg2dlsvn3771; __81fgg2count3771 != 0; __81fgg2count3771--, _b5p6od9s += (__81fgg2step3771)) {

							{
								
								_yc8h372p = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
								_q3ig7mub = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn3772 = (System.Int32)((_b5p6od9s + (int)1));
									const System.Int32 __81fgg2step3772 = (System.Int32)((int)1);
									System.Int32 __81fgg2count3772;
									for (__81fgg2count3772 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3772 + __81fgg2step3772) / __81fgg2step3772)), _umlkckdg = __81fgg2dlsvn3772; __81fgg2count3772 != 0; __81fgg2count3772--, _umlkckdg += (__81fgg2step3772)) {

									{
										
										*(_3crf0qn3+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) + (_yc8h372p * *(_vxfgpup9+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))));
										_q3ig7mub = (_q3ig7mub + (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) * *(_vxfgpup9+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))));
Mark80:;
										// continue
									}
																		}								}
								if (_bafcbx97 == _d0547bi2)
								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = ((_yc8h372p * *(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))) + (_r7cfteg3 * _q3ig7mub));
								}
								else
								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (((_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))) + (_yc8h372p * *(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)))) + (_r7cfteg3 * _q3ig7mub));
								}
								
Mark90:;
								// continue
							}
														}						}
Mark100:;
						// continue
					}
										}				}
			}
			
		}
		else
		{
			//* 
			//*        Form  C := alpha*B*A + beta*C. 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn3773 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step3773 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3773;
				for (__81fgg2count3773 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3773 + __81fgg2step3773) / __81fgg2step3773)), _znpjgsef = __81fgg2dlsvn3773; __81fgg2count3773 != 0; __81fgg2count3773--, _znpjgsef += (__81fgg2step3773)) {

				{
					
					_yc8h372p = (_r7cfteg3 * *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
					if (_bafcbx97 == _d0547bi2)
					{
						
						{
							System.Int32 __81fgg2dlsvn3774 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3774 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3774;
							for (__81fgg2count3774 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3774 + __81fgg2step3774) / __81fgg2step3774)), _b5p6od9s = __81fgg2dlsvn3774; __81fgg2count3774 != 0; __81fgg2count3774--, _b5p6od9s += (__81fgg2step3774)) {

							{
								
								*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_yc8h372p * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
Mark110:;
								// continue
							}
														}						}
					}
					else
					{
						
						{
							System.Int32 __81fgg2dlsvn3775 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3775 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3775;
							for (__81fgg2count3775 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3775 + __81fgg2step3775) / __81fgg2step3775)), _b5p6od9s = __81fgg2dlsvn3775; __81fgg2count3775 != 0; __81fgg2count3775--, _b5p6od9s += (__81fgg2step3775)) {

							{
								
								*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = ((_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))) + (_yc8h372p * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
Mark120:;
								// continue
							}
														}						}
					}
					
					{
						System.Int32 __81fgg2dlsvn3776 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3776 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3776;
						for (__81fgg2count3776 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn3776 + __81fgg2step3776) / __81fgg2step3776)), _umlkckdg = __81fgg2dlsvn3776; __81fgg2count3776 != 0; __81fgg2count3776--, _umlkckdg += (__81fgg2step3776)) {

						{
							
							if (_l08igmvf)
							{
								
								_yc8h372p = (_r7cfteg3 * *(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							}
							else
							{
								
								_yc8h372p = (_r7cfteg3 * *(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
							}
							
							{
								System.Int32 __81fgg2dlsvn3777 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3777 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3777;
								for (__81fgg2count3777 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3777 + __81fgg2step3777) / __81fgg2step3777)), _b5p6od9s = __81fgg2dlsvn3777; __81fgg2count3777 != 0; __81fgg2count3777--, _b5p6od9s += (__81fgg2step3777)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) + (_yc8h372p * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg))));
Mark130:;
									// continue
								}
																}							}
Mark140:;
							// continue
						}
												}					}
					{
						System.Int32 __81fgg2dlsvn3778 = (System.Int32)((_znpjgsef + (int)1));
						const System.Int32 __81fgg2step3778 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3778;
						for (__81fgg2count3778 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3778 + __81fgg2step3778) / __81fgg2step3778)), _umlkckdg = __81fgg2dlsvn3778; __81fgg2count3778 != 0; __81fgg2count3778--, _umlkckdg += (__81fgg2step3778)) {

						{
							
							if (_l08igmvf)
							{
								
								_yc8h372p = (_r7cfteg3 * *(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
							}
							else
							{
								
								_yc8h372p = (_r7cfteg3 * *(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							}
							
							{
								System.Int32 __81fgg2dlsvn3779 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3779 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3779;
								for (__81fgg2count3779 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3779 + __81fgg2step3779) / __81fgg2step3779)), _b5p6od9s = __81fgg2dlsvn3779; __81fgg2count3779 != 0; __81fgg2count3779--, _b5p6od9s += (__81fgg2step3779)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) + (_yc8h372p * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg))));
Mark150:;
									// continue
								}
																}							}
Mark160:;
							// continue
						}
												}					}
Mark170:;
					// continue
				}
								}			}
		}
		//* 
		
		return;//* 
		//*     End of DSYMM . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
