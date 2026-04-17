
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
//*> \brief \b ZHEMM 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZHEMM(SIDE,UPLO,M,N,ALPHA,A,LDA,B,LDB,BETA,C,LDC) 
//* 
//*       .. Scalar Arguments .. 
//*       COMPLEX*16 ALPHA,BETA 
//*       INTEGER LDA,LDB,LDC,M,N 
//*       CHARACTER SIDE,UPLO 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX*16 A(LDA,*),B(LDB,*),C(LDC,*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZHEMM  performs one of the matrix-matrix operations 
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
//*>          ALPHA is COMPLEX*16 
//*>           On entry, ALPHA specifies the scalar alpha. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is COMPLEX*16 array, dimension ( LDA, ka ), where ka is 
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
//*>          B is COMPLEX*16 array, dimension ( LDB, N ) 
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
//*>          BETA is COMPLEX*16 
//*>           On entry,  BETA  specifies the scalar  beta.  When  BETA  is 
//*>           supplied as zero then C need not be set on input. 
//*> \endverbatim 
//*> 
//*> \param[in,out] C 
//*> \verbatim 
//*>          C is COMPLEX*16 array, dimension ( LDC, N ) 
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

	 
	public static void _rsrrie0u(FString _m2cn2gjg, FString _9wyre9zc, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref complex _r7cfteg3, complex* _vxfgpup9, ref Int32 _ocv8fk5c, complex* _p9n405a5, ref Int32 _ly9opahg, ref complex _bafcbx97, complex* _3crf0qn3, ref Int32 _1s3eymp4)
	{
#region variable declarations
complex _yc8h372p =  default;
complex _q3ig7mub =  default;
Int32 _b5p6od9s =  default;
Int32 _gro5yvfo =  default;
Int32 _znpjgsef =  default;
Int32 _umlkckdg =  default;
Int32 _o9a6qdux =  default;
Boolean _l08igmvf =  default;
complex _kxg5drh2 =   new fcomplex(1f,0f);
complex _d0547bi2 =   new fcomplex(0f,0f);
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
			
			_ut9qalzx("ZHEMM " ,ref _gro5yvfo );
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
					System.Int32 __81fgg2dlsvn3949 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3949 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3949;
					for (__81fgg2count3949 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3949 + __81fgg2step3949) / __81fgg2step3949)), _znpjgsef = __81fgg2dlsvn3949; __81fgg2count3949 != 0; __81fgg2count3949--, _znpjgsef += (__81fgg2step3949)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn3950 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3950 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3950;
							for (__81fgg2count3950 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3950 + __81fgg2step3950) / __81fgg2step3950)), _b5p6od9s = __81fgg2dlsvn3950; __81fgg2count3950 != 0; __81fgg2count3950--, _b5p6od9s += (__81fgg2step3950)) {

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
					System.Int32 __81fgg2dlsvn3951 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3951 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3951;
					for (__81fgg2count3951 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3951 + __81fgg2step3951) / __81fgg2step3951)), _znpjgsef = __81fgg2dlsvn3951; __81fgg2count3951 != 0; __81fgg2count3951--, _znpjgsef += (__81fgg2step3951)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn3952 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3952 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3952;
							for (__81fgg2count3952 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3952 + __81fgg2step3952) / __81fgg2step3952)), _b5p6od9s = __81fgg2dlsvn3952; __81fgg2count3952 != 0; __81fgg2count3952--, _b5p6od9s += (__81fgg2step3952)) {

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
					System.Int32 __81fgg2dlsvn3953 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3953 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3953;
					for (__81fgg2count3953 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3953 + __81fgg2step3953) / __81fgg2step3953)), _znpjgsef = __81fgg2dlsvn3953; __81fgg2count3953 != 0; __81fgg2count3953--, _znpjgsef += (__81fgg2step3953)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn3954 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3954 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3954;
							for (__81fgg2count3954 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3954 + __81fgg2step3954) / __81fgg2step3954)), _b5p6od9s = __81fgg2dlsvn3954; __81fgg2count3954 != 0; __81fgg2count3954--, _b5p6od9s += (__81fgg2step3954)) {

							{
								
								_yc8h372p = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
								_q3ig7mub = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn3955 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step3955 = (System.Int32)((int)1);
									System.Int32 __81fgg2count3955;
									for (__81fgg2count3955 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s - (int)1) - __81fgg2dlsvn3955 + __81fgg2step3955) / __81fgg2step3955)), _umlkckdg = __81fgg2dlsvn3955; __81fgg2count3955 != 0; __81fgg2count3955--, _umlkckdg += (__81fgg2step3955)) {

									{
										
										*(_3crf0qn3+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) + (_yc8h372p * *(_vxfgpup9+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))));
										_q3ig7mub = (_q3ig7mub + (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) * ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) )));
Mark50:;
										// continue
									}
																		}								}
								if (_bafcbx97 == _d0547bi2)
								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = ((_yc8h372p * ILNumerics.F2NET.Intrinsics.DBLE(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) )) + (_r7cfteg3 * _q3ig7mub));
								}
								else
								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (((_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))) + (_yc8h372p * ILNumerics.F2NET.Intrinsics.DBLE(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ))) + (_r7cfteg3 * _q3ig7mub));
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
					System.Int32 __81fgg2dlsvn3956 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3956 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3956;
					for (__81fgg2count3956 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3956 + __81fgg2step3956) / __81fgg2step3956)), _znpjgsef = __81fgg2dlsvn3956; __81fgg2count3956 != 0; __81fgg2count3956--, _znpjgsef += (__81fgg2step3956)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn3957 = (System.Int32)(_ev4xhht5);
							System.Int32 __81fgg2step3957 = (System.Int32)((int)-1);
							System.Int32 __81fgg2count3957;
							for (__81fgg2count3957 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn3957 + __81fgg2step3957) / __81fgg2step3957)), _b5p6od9s = __81fgg2dlsvn3957; __81fgg2count3957 != 0; __81fgg2count3957--, _b5p6od9s += (__81fgg2step3957)) {

							{
								
								_yc8h372p = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
								_q3ig7mub = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn3958 = (System.Int32)((_b5p6od9s + (int)1));
									const System.Int32 __81fgg2step3958 = (System.Int32)((int)1);
									System.Int32 __81fgg2count3958;
									for (__81fgg2count3958 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3958 + __81fgg2step3958) / __81fgg2step3958)), _umlkckdg = __81fgg2dlsvn3958; __81fgg2count3958 != 0; __81fgg2count3958--, _umlkckdg += (__81fgg2step3958)) {

									{
										
										*(_3crf0qn3+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) + (_yc8h372p * *(_vxfgpup9+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))));
										_q3ig7mub = (_q3ig7mub + (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) * ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) )));
Mark80:;
										// continue
									}
																		}								}
								if (_bafcbx97 == _d0547bi2)
								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = ((_yc8h372p * ILNumerics.F2NET.Intrinsics.DBLE(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) )) + (_r7cfteg3 * _q3ig7mub));
								}
								else
								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (((_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))) + (_yc8h372p * ILNumerics.F2NET.Intrinsics.DBLE(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ))) + (_r7cfteg3 * _q3ig7mub));
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
				System.Int32 __81fgg2dlsvn3959 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step3959 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3959;
				for (__81fgg2count3959 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3959 + __81fgg2step3959) / __81fgg2step3959)), _znpjgsef = __81fgg2dlsvn3959; __81fgg2count3959 != 0; __81fgg2count3959--, _znpjgsef += (__81fgg2step3959)) {

				{
					
					_yc8h372p = (_r7cfteg3 * ILNumerics.F2NET.Intrinsics.DBLE(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ));
					if (_bafcbx97 == _d0547bi2)
					{
						
						{
							System.Int32 __81fgg2dlsvn3960 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3960 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3960;
							for (__81fgg2count3960 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3960 + __81fgg2step3960) / __81fgg2step3960)), _b5p6od9s = __81fgg2dlsvn3960; __81fgg2count3960 != 0; __81fgg2count3960--, _b5p6od9s += (__81fgg2step3960)) {

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
							System.Int32 __81fgg2dlsvn3961 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3961 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3961;
							for (__81fgg2count3961 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3961 + __81fgg2step3961) / __81fgg2step3961)), _b5p6od9s = __81fgg2dlsvn3961; __81fgg2count3961 != 0; __81fgg2count3961--, _b5p6od9s += (__81fgg2step3961)) {

							{
								
								*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = ((_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))) + (_yc8h372p * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
Mark120:;
								// continue
							}
														}						}
					}
					
					{
						System.Int32 __81fgg2dlsvn3962 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3962 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3962;
						for (__81fgg2count3962 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn3962 + __81fgg2step3962) / __81fgg2step3962)), _umlkckdg = __81fgg2dlsvn3962; __81fgg2count3962 != 0; __81fgg2count3962--, _umlkckdg += (__81fgg2step3962)) {

						{
							
							if (_l08igmvf)
							{
								
								_yc8h372p = (_r7cfteg3 * *(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							}
							else
							{
								
								_yc8h372p = (_r7cfteg3 * ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) ));
							}
							
							{
								System.Int32 __81fgg2dlsvn3963 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3963 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3963;
								for (__81fgg2count3963 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3963 + __81fgg2step3963) / __81fgg2step3963)), _b5p6od9s = __81fgg2dlsvn3963; __81fgg2count3963 != 0; __81fgg2count3963--, _b5p6od9s += (__81fgg2step3963)) {

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
						System.Int32 __81fgg2dlsvn3964 = (System.Int32)((_znpjgsef + (int)1));
						const System.Int32 __81fgg2step3964 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3964;
						for (__81fgg2count3964 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3964 + __81fgg2step3964) / __81fgg2step3964)), _umlkckdg = __81fgg2dlsvn3964; __81fgg2count3964 != 0; __81fgg2count3964--, _umlkckdg += (__81fgg2step3964)) {

						{
							
							if (_l08igmvf)
							{
								
								_yc8h372p = (_r7cfteg3 * ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) ));
							}
							else
							{
								
								_yc8h372p = (_r7cfteg3 * *(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							}
							
							{
								System.Int32 __81fgg2dlsvn3965 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3965 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3965;
								for (__81fgg2count3965 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3965 + __81fgg2step3965) / __81fgg2step3965)), _b5p6od9s = __81fgg2dlsvn3965; __81fgg2count3965 != 0; __81fgg2count3965--, _b5p6od9s += (__81fgg2step3965)) {

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
		//*     End of ZHEMM . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
