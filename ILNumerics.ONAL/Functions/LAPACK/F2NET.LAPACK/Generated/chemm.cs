
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
//*> \brief \b CHEMM 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CHEMM(SIDE,UPLO,M,N,ALPHA,A,LDA,B,LDB,BETA,C,LDC) 
//* 
//*       .. Scalar Arguments .. 
//*       COMPLEX ALPHA,BETA 
//*       INTEGER LDA,LDB,LDC,M,N 
//*       CHARACTER SIDE,UPLO 
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
//*> CHEMM  performs one of the matrix-matrix operations 
//*> 
//*>    C := alpha*A*B + beta*C, 
//*> 
//*> or 
//*> 
//*>    C := alpha*B*A + beta*C, 
//*> 
//*> where alpha and beta are scalars, A is an hermitian matrix and  B and 
//*> C are m by n matrices. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] SIDE 
//*> \verbatim 
//*>          SIDE is CHARACTER*1 
//*>           On entry,  SIDE  specifies whether  the  hermitian matrix  A 
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
//*>           triangular  part  of  the  hermitian  matrix   A  is  to  be 
//*>           referenced as follows: 
//*> 
//*>              UPLO = 'U' or 'u'   Only the upper triangular part of the 
//*>                                  hermitian matrix is to be referenced. 
//*> 
//*>              UPLO = 'L' or 'l'   Only the lower triangular part of the 
//*>                                  hermitian matrix is to be referenced. 
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
//*>          ALPHA is COMPLEX 
//*>           On entry, ALPHA specifies the scalar alpha. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is COMPLEX array, dimension ( LDA, ka ), where ka is 
//*>           m  when  SIDE = 'L' or 'l'  and is n  otherwise. 
//*>           Before entry  with  SIDE = 'L' or 'l',  the  m by m  part of 
//*>           the array  A  must contain the  hermitian matrix,  such that 
//*>           when  UPLO = 'U' or 'u', the leading m by m upper triangular 
//*>           part of the array  A  must contain the upper triangular part 
//*>           of the  hermitian matrix and the  strictly  lower triangular 
//*>           part of  A  is not referenced,  and when  UPLO = 'L' or 'l', 
//*>           the leading  m by m  lower triangular part  of the  array  A 
//*>           must  contain  the  lower triangular part  of the  hermitian 
//*>           matrix and the  strictly upper triangular part of  A  is not 
//*>           referenced. 
//*>           Before entry  with  SIDE = 'R' or 'r',  the  n by n  part of 
//*>           the array  A  must contain the  hermitian matrix,  such that 
//*>           when  UPLO = 'U' or 'u', the leading n by n upper triangular 
//*>           part of the array  A  must contain the upper triangular part 
//*>           of the  hermitian matrix and the  strictly  lower triangular 
//*>           part of  A  is not referenced,  and when  UPLO = 'L' or 'l', 
//*>           the leading  n by n  lower triangular part  of the  array  A 
//*>           must  contain  the  lower triangular part  of the  hermitian 
//*>           matrix and the  strictly upper triangular part of  A  is not 
//*>           referenced. 
//*>           Note that the imaginary parts  of the diagonal elements need 
//*>           not be set, they are assumed to be zero. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>           On entry, LDA specifies the first dimension of A as declared 
//*>           in the  calling (sub) program. When  SIDE = 'L' or 'l'  then 
//*>           LDA must be at least  max( 1, m ), otherwise  LDA must be at 
//*>           least max( 1, n ). 
//*> \endverbatim 
//*> 
//*> \param[in] B 
//*> \verbatim 
//*>          B is COMPLEX array, dimension ( LDB, N ) 
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

	 
	public static void _7hsjii7x(FString _m2cn2gjg, FString _9wyre9zc, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref fcomplex _r7cfteg3, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, fcomplex* _p9n405a5, ref Int32 _ly9opahg, ref fcomplex _bafcbx97, fcomplex* _3crf0qn3, ref Int32 _1s3eymp4)
	{
#region variable declarations
fcomplex _yc8h372p =  default;
fcomplex _q3ig7mub =  default;
Int32 _b5p6od9s =  default;
Int32 _gro5yvfo =  default;
Int32 _znpjgsef =  default;
Int32 _umlkckdg =  default;
Int32 _o9a6qdux =  default;
Boolean _l08igmvf =  default;
fcomplex _kxg5drh2 =   new fcomplex(1f,0f);
fcomplex _d0547bi2 =   new fcomplex(0f,0f);
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
			
			_ut9qalzx("CHEMM " ,ref _gro5yvfo );
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
					System.Int32 __81fgg2dlsvn3888 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3888 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3888;
					for (__81fgg2count3888 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3888 + __81fgg2step3888) / __81fgg2step3888)), _znpjgsef = __81fgg2dlsvn3888; __81fgg2count3888 != 0; __81fgg2count3888--, _znpjgsef += (__81fgg2step3888)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn3889 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3889 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3889;
							for (__81fgg2count3889 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3889 + __81fgg2step3889) / __81fgg2step3889)), _b5p6od9s = __81fgg2dlsvn3889; __81fgg2count3889 != 0; __81fgg2count3889--, _b5p6od9s += (__81fgg2step3889)) {

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
					System.Int32 __81fgg2dlsvn3890 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3890 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3890;
					for (__81fgg2count3890 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3890 + __81fgg2step3890) / __81fgg2step3890)), _znpjgsef = __81fgg2dlsvn3890; __81fgg2count3890 != 0; __81fgg2count3890--, _znpjgsef += (__81fgg2step3890)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn3891 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3891 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3891;
							for (__81fgg2count3891 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3891 + __81fgg2step3891) / __81fgg2step3891)), _b5p6od9s = __81fgg2dlsvn3891; __81fgg2count3891 != 0; __81fgg2count3891--, _b5p6od9s += (__81fgg2step3891)) {

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
					System.Int32 __81fgg2dlsvn3892 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3892 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3892;
					for (__81fgg2count3892 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3892 + __81fgg2step3892) / __81fgg2step3892)), _znpjgsef = __81fgg2dlsvn3892; __81fgg2count3892 != 0; __81fgg2count3892--, _znpjgsef += (__81fgg2step3892)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn3893 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3893 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3893;
							for (__81fgg2count3893 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3893 + __81fgg2step3893) / __81fgg2step3893)), _b5p6od9s = __81fgg2dlsvn3893; __81fgg2count3893 != 0; __81fgg2count3893--, _b5p6od9s += (__81fgg2step3893)) {

							{
								
								_yc8h372p = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
								_q3ig7mub = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn3894 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step3894 = (System.Int32)((int)1);
									System.Int32 __81fgg2count3894;
									for (__81fgg2count3894 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s - (int)1) - __81fgg2dlsvn3894 + __81fgg2step3894) / __81fgg2step3894)), _umlkckdg = __81fgg2dlsvn3894; __81fgg2count3894 != 0; __81fgg2count3894--, _umlkckdg += (__81fgg2step3894)) {

									{
										
										*(_3crf0qn3+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) + (_yc8h372p * *(_vxfgpup9+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))));
										_q3ig7mub = (_q3ig7mub + (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) * ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) )));
Mark50:;
										// continue
									}
																		}								}
								if (_bafcbx97 == _d0547bi2)
								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = ((_yc8h372p * ILNumerics.F2NET.Intrinsics.REAL(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) )) + (_r7cfteg3 * _q3ig7mub));
								}
								else
								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (((_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))) + (_yc8h372p * ILNumerics.F2NET.Intrinsics.REAL(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ))) + (_r7cfteg3 * _q3ig7mub));
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
					System.Int32 __81fgg2dlsvn3895 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3895 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3895;
					for (__81fgg2count3895 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3895 + __81fgg2step3895) / __81fgg2step3895)), _znpjgsef = __81fgg2dlsvn3895; __81fgg2count3895 != 0; __81fgg2count3895--, _znpjgsef += (__81fgg2step3895)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn3896 = (System.Int32)(_ev4xhht5);
							System.Int32 __81fgg2step3896 = (System.Int32)((int)-1);
							System.Int32 __81fgg2count3896;
							for (__81fgg2count3896 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn3896 + __81fgg2step3896) / __81fgg2step3896)), _b5p6od9s = __81fgg2dlsvn3896; __81fgg2count3896 != 0; __81fgg2count3896--, _b5p6od9s += (__81fgg2step3896)) {

							{
								
								_yc8h372p = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
								_q3ig7mub = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn3897 = (System.Int32)((_b5p6od9s + (int)1));
									const System.Int32 __81fgg2step3897 = (System.Int32)((int)1);
									System.Int32 __81fgg2count3897;
									for (__81fgg2count3897 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3897 + __81fgg2step3897) / __81fgg2step3897)), _umlkckdg = __81fgg2dlsvn3897; __81fgg2count3897 != 0; __81fgg2count3897--, _umlkckdg += (__81fgg2step3897)) {

									{
										
										*(_3crf0qn3+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) + (_yc8h372p * *(_vxfgpup9+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))));
										_q3ig7mub = (_q3ig7mub + (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) * ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) )));
Mark80:;
										// continue
									}
																		}								}
								if (_bafcbx97 == _d0547bi2)
								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = ((_yc8h372p * ILNumerics.F2NET.Intrinsics.REAL(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) )) + (_r7cfteg3 * _q3ig7mub));
								}
								else
								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (((_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))) + (_yc8h372p * ILNumerics.F2NET.Intrinsics.REAL(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ))) + (_r7cfteg3 * _q3ig7mub));
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
				System.Int32 __81fgg2dlsvn3898 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step3898 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3898;
				for (__81fgg2count3898 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3898 + __81fgg2step3898) / __81fgg2step3898)), _znpjgsef = __81fgg2dlsvn3898; __81fgg2count3898 != 0; __81fgg2count3898--, _znpjgsef += (__81fgg2step3898)) {

				{
					
					_yc8h372p = (_r7cfteg3 * ILNumerics.F2NET.Intrinsics.REAL(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ));
					if (_bafcbx97 == _d0547bi2)
					{
						
						{
							System.Int32 __81fgg2dlsvn3899 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3899 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3899;
							for (__81fgg2count3899 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3899 + __81fgg2step3899) / __81fgg2step3899)), _b5p6od9s = __81fgg2dlsvn3899; __81fgg2count3899 != 0; __81fgg2count3899--, _b5p6od9s += (__81fgg2step3899)) {

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
							System.Int32 __81fgg2dlsvn3900 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3900 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3900;
							for (__81fgg2count3900 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3900 + __81fgg2step3900) / __81fgg2step3900)), _b5p6od9s = __81fgg2dlsvn3900; __81fgg2count3900 != 0; __81fgg2count3900--, _b5p6od9s += (__81fgg2step3900)) {

							{
								
								*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = ((_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))) + (_yc8h372p * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
Mark120:;
								// continue
							}
														}						}
					}
					
					{
						System.Int32 __81fgg2dlsvn3901 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3901 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3901;
						for (__81fgg2count3901 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn3901 + __81fgg2step3901) / __81fgg2step3901)), _umlkckdg = __81fgg2dlsvn3901; __81fgg2count3901 != 0; __81fgg2count3901--, _umlkckdg += (__81fgg2step3901)) {

						{
							
							if (_l08igmvf)
							{
								
								_yc8h372p = (_r7cfteg3 * *(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							}
							else
							{
								
								_yc8h372p = (_r7cfteg3 * ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) ));
							}
							
							{
								System.Int32 __81fgg2dlsvn3902 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3902 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3902;
								for (__81fgg2count3902 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3902 + __81fgg2step3902) / __81fgg2step3902)), _b5p6od9s = __81fgg2dlsvn3902; __81fgg2count3902 != 0; __81fgg2count3902--, _b5p6od9s += (__81fgg2step3902)) {

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
						System.Int32 __81fgg2dlsvn3903 = (System.Int32)((_znpjgsef + (int)1));
						const System.Int32 __81fgg2step3903 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3903;
						for (__81fgg2count3903 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3903 + __81fgg2step3903) / __81fgg2step3903)), _umlkckdg = __81fgg2dlsvn3903; __81fgg2count3903 != 0; __81fgg2count3903--, _umlkckdg += (__81fgg2step3903)) {

						{
							
							if (_l08igmvf)
							{
								
								_yc8h372p = (_r7cfteg3 * ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) ));
							}
							else
							{
								
								_yc8h372p = (_r7cfteg3 * *(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							}
							
							{
								System.Int32 __81fgg2dlsvn3904 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3904 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3904;
								for (__81fgg2count3904 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3904 + __81fgg2step3904) / __81fgg2step3904)), _b5p6od9s = __81fgg2dlsvn3904; __81fgg2count3904 != 0; __81fgg2count3904--, _b5p6od9s += (__81fgg2step3904)) {

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
		//*     End of CHEMM . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
