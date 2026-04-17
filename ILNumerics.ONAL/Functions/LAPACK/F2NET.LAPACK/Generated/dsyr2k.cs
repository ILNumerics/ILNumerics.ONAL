
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
//*> \brief \b DSYR2K 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DSYR2K(UPLO,TRANS,N,K,ALPHA,A,LDA,B,LDB,BETA,C,LDC) 
//* 
//*       .. Scalar Arguments .. 
//*       DOUBLE PRECISION ALPHA,BETA 
//*       INTEGER K,LDA,LDB,LDC,N 
//*       CHARACTER TRANS,UPLO 
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
//*> DSYR2K  performs one of the symmetric rank 2k operations 
//*> 
//*>    C := alpha*A*B**T + alpha*B*A**T + beta*C, 
//*> 
//*> or 
//*> 
//*>    C := alpha*A**T*B + alpha*B**T*A + beta*C, 
//*> 
//*> where  alpha and beta  are scalars, C is an  n by n  symmetric matrix 
//*> and  A and B  are  n by k  matrices  in the  first  case  and  k by n 
//*> matrices in the second case. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] UPLO 
//*> \verbatim 
//*>          UPLO is CHARACTER*1 
//*>           On  entry,   UPLO  specifies  whether  the  upper  or  lower 
//*>           triangular  part  of the  array  C  is to be  referenced  as 
//*>           follows: 
//*> 
//*>              UPLO = 'U' or 'u'   Only the  upper triangular part of  C 
//*>                                  is to be referenced. 
//*> 
//*>              UPLO = 'L' or 'l'   Only the  lower triangular part of  C 
//*>                                  is to be referenced. 
//*> \endverbatim 
//*> 
//*> \param[in] TRANS 
//*> \verbatim 
//*>          TRANS is CHARACTER*1 
//*>           On entry,  TRANS  specifies the operation to be performed as 
//*>           follows: 
//*> 
//*>              TRANS = 'N' or 'n'   C := alpha*A*B**T + alpha*B*A**T + 
//*>                                        beta*C. 
//*> 
//*>              TRANS = 'T' or 't'   C := alpha*A**T*B + alpha*B**T*A + 
//*>                                        beta*C. 
//*> 
//*>              TRANS = 'C' or 'c'   C := alpha*A**T*B + alpha*B**T*A + 
//*>                                        beta*C. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>           On entry,  N specifies the order of the matrix C.  N must be 
//*>           at least zero. 
//*> \endverbatim 
//*> 
//*> \param[in] K 
//*> \verbatim 
//*>          K is INTEGER 
//*>           On entry with  TRANS = 'N' or 'n',  K  specifies  the number 
//*>           of  columns  of the  matrices  A and B,  and on  entry  with 
//*>           TRANS = 'T' or 't' or 'C' or 'c',  K  specifies  the  number 
//*>           of rows of the matrices  A and B.  K must be at least  zero. 
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
//*>           k  when  TRANS = 'N' or 'n',  and is  n  otherwise. 
//*>           Before entry with  TRANS = 'N' or 'n',  the  leading  n by k 
//*>           part of the array  A  must contain the matrix  A,  otherwise 
//*>           the leading  k by n  part of the array  A  must contain  the 
//*>           matrix A. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>           On entry, LDA specifies the first dimension of A as declared 
//*>           in  the  calling  (sub)  program.   When  TRANS = 'N' or 'n' 
//*>           then  LDA must be at least  max( 1, n ), otherwise  LDA must 
//*>           be at least  max( 1, k ). 
//*> \endverbatim 
//*> 
//*> \param[in] B 
//*> \verbatim 
//*>          B is DOUBLE PRECISION array, dimension ( LDB, kb ), where kb is 
//*>           k  when  TRANS = 'N' or 'n',  and is  n  otherwise. 
//*>           Before entry with  TRANS = 'N' or 'n',  the  leading  n by k 
//*>           part of the array  B  must contain the matrix  B,  otherwise 
//*>           the leading  k by n  part of the array  B  must contain  the 
//*>           matrix B. 
//*> \endverbatim 
//*> 
//*> \param[in] LDB 
//*> \verbatim 
//*>          LDB is INTEGER 
//*>           On entry, LDB specifies the first dimension of B as declared 
//*>           in  the  calling  (sub)  program.   When  TRANS = 'N' or 'n' 
//*>           then  LDB must be at least  max( 1, n ), otherwise  LDB must 
//*>           be at least  max( 1, k ). 
//*> \endverbatim 
//*> 
//*> \param[in] BETA 
//*> \verbatim 
//*>          BETA is DOUBLE PRECISION. 
//*>           On entry, BETA specifies the scalar beta. 
//*> \endverbatim 
//*> 
//*> \param[in,out] C 
//*> \verbatim 
//*>          C is DOUBLE PRECISION array, dimension ( LDC, N ) 
//*>           Before entry  with  UPLO = 'U' or 'u',  the leading  n by n 
//*>           upper triangular part of the array C must contain the upper 
//*>           triangular part  of the  symmetric matrix  and the strictly 
//*>           lower triangular part of C is not referenced.  On exit, the 
//*>           upper triangular part of the array  C is overwritten by the 
//*>           upper triangular part of the updated matrix. 
//*>           Before entry  with  UPLO = 'L' or 'l',  the leading  n by n 
//*>           lower triangular part of the array C must contain the lower 
//*>           triangular part  of the  symmetric matrix  and the strictly 
//*>           upper triangular part of C is not referenced.  On exit, the 
//*>           lower triangular part of the array  C is overwritten by the 
//*>           lower triangular part of the updated matrix. 
//*> \endverbatim 
//*> 
//*> \param[in] LDC 
//*> \verbatim 
//*>          LDC is INTEGER 
//*>           On entry, LDC specifies the first dimension of C as declared 
//*>           in  the  calling  (sub)  program.   LDC  must  be  at  least 
//*>           max( 1, n ). 
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

	 
	public static void _wpcl5drw(FString _9wyre9zc, FString _scuo79v4, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, ref Double _r7cfteg3, Double* _vxfgpup9, ref Int32 _ocv8fk5c, Double* _p9n405a5, ref Int32 _ly9opahg, ref Double _bafcbx97, Double* _3crf0qn3, ref Int32 _1s3eymp4)
	{
#region variable declarations
Double _yc8h372p =  default;
Double _q3ig7mub =  default;
Int32 _b5p6od9s =  default;
Int32 _gro5yvfo =  default;
Int32 _znpjgsef =  default;
Int32 _68ec3gbh =  default;
Int32 _o9a6qdux =  default;
Boolean _l08igmvf =  default;
Double _kxg5drh2 =  1d;
Double _d0547bi2 =  0d;
string fLanavab = default;
#endregion  variable declarations
_9wyre9zc = _9wyre9zc.Convert(1);
_scuo79v4 = _scuo79v4.Convert(1);

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
		
		if (_w8y2rzgy(_scuo79v4 ,"N" ))
		{
			
			_o9a6qdux = _dxpq0xkr;
		}
		else
		{
			
			_o9a6qdux = _umlkckdg;
		}
		
		_l08igmvf = _w8y2rzgy(_9wyre9zc ,"U" );//* 
		
		_gro5yvfo = (int)0;
		if ((!(_l08igmvf)) & (!(_w8y2rzgy(_9wyre9zc ,"L" ))))
		{
			
			_gro5yvfo = (int)1;
		}
		else
		if (((!(_w8y2rzgy(_scuo79v4 ,"N" ))) & (!(_w8y2rzgy(_scuo79v4 ,"T" )))) & (!(_w8y2rzgy(_scuo79v4 ,"C" ))))
		{
			
			_gro5yvfo = (int)2;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)3;
		}
		else
		if (_umlkckdg < (int)0)
		{
			
			_gro5yvfo = (int)4;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_o9a6qdux ))
		{
			
			_gro5yvfo = (int)7;
		}
		else
		if (_ly9opahg < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_o9a6qdux ))
		{
			
			_gro5yvfo = (int)9;
		}
		else
		if (_1s3eymp4 < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)12;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("DSYR2K" ,ref _gro5yvfo );
			return;
		}
		//* 
		//*     Quick return if possible. 
		//* 
		
		if ((_dxpq0xkr == (int)0) | (((_r7cfteg3 == _d0547bi2) | (_umlkckdg == (int)0)) & (_bafcbx97 == _kxg5drh2)))
		return;//* 
		//*     And when  alpha.eq.zero. 
		//* 
		
		if (_r7cfteg3 == _d0547bi2)
		{
			
			if (_l08igmvf)
			{
				
				if (_bafcbx97 == _d0547bi2)
				{
					
					{
						System.Int32 __81fgg2dlsvn3117 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3117 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3117;
						for (__81fgg2count3117 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3117 + __81fgg2step3117) / __81fgg2step3117)), _znpjgsef = __81fgg2dlsvn3117; __81fgg2count3117 != 0; __81fgg2count3117--, _znpjgsef += (__81fgg2step3117)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn3118 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3118 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3118;
								for (__81fgg2count3118 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn3118 + __81fgg2step3118) / __81fgg2step3118)), _b5p6od9s = __81fgg2dlsvn3118; __81fgg2count3118 != 0; __81fgg2count3118--, _b5p6od9s += (__81fgg2step3118)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = _d0547bi2;
Mark10:;
									// continue
								}
																}							}
Mark20:;
							// continue
						}
												}					}
				}
				else
				{
					
					{
						System.Int32 __81fgg2dlsvn3119 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3119 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3119;
						for (__81fgg2count3119 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3119 + __81fgg2step3119) / __81fgg2step3119)), _znpjgsef = __81fgg2dlsvn3119; __81fgg2count3119 != 0; __81fgg2count3119--, _znpjgsef += (__81fgg2step3119)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn3120 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3120 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3120;
								for (__81fgg2count3120 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn3120 + __81fgg2step3120) / __81fgg2step3120)), _b5p6od9s = __81fgg2dlsvn3120; __81fgg2count3120 != 0; __81fgg2count3120--, _b5p6od9s += (__81fgg2step3120)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark30:;
									// continue
								}
																}							}
Mark40:;
							// continue
						}
												}					}
				}
				
			}
			else
			{
				
				if (_bafcbx97 == _d0547bi2)
				{
					
					{
						System.Int32 __81fgg2dlsvn3121 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3121 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3121;
						for (__81fgg2count3121 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3121 + __81fgg2step3121) / __81fgg2step3121)), _znpjgsef = __81fgg2dlsvn3121; __81fgg2count3121 != 0; __81fgg2count3121--, _znpjgsef += (__81fgg2step3121)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn3122 = (System.Int32)(_znpjgsef);
								const System.Int32 __81fgg2step3122 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3122;
								for (__81fgg2count3122 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3122 + __81fgg2step3122) / __81fgg2step3122)), _b5p6od9s = __81fgg2dlsvn3122; __81fgg2count3122 != 0; __81fgg2count3122--, _b5p6od9s += (__81fgg2step3122)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = _d0547bi2;
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
						System.Int32 __81fgg2dlsvn3123 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3123 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3123;
						for (__81fgg2count3123 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3123 + __81fgg2step3123) / __81fgg2step3123)), _znpjgsef = __81fgg2dlsvn3123; __81fgg2count3123 != 0; __81fgg2count3123--, _znpjgsef += (__81fgg2step3123)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn3124 = (System.Int32)(_znpjgsef);
								const System.Int32 __81fgg2step3124 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3124;
								for (__81fgg2count3124 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3124 + __81fgg2step3124) / __81fgg2step3124)), _b5p6od9s = __81fgg2dlsvn3124; __81fgg2count3124 != 0; __81fgg2count3124--, _b5p6od9s += (__81fgg2step3124)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
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
			
			return;
		}
		//* 
		//*     Start the operations. 
		//* 
		
		if (_w8y2rzgy(_scuo79v4 ,"N" ))
		{
			//* 
			//*        Form  C := alpha*A*B**T + alpha*B*A**T + C. 
			//* 
			
			if (_l08igmvf)
			{
				
				{
					System.Int32 __81fgg2dlsvn3125 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3125 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3125;
					for (__81fgg2count3125 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3125 + __81fgg2step3125) / __81fgg2step3125)), _znpjgsef = __81fgg2dlsvn3125; __81fgg2count3125 != 0; __81fgg2count3125--, _znpjgsef += (__81fgg2step3125)) {

					{
						
						if (_bafcbx97 == _d0547bi2)
						{
							
							{
								System.Int32 __81fgg2dlsvn3126 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3126 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3126;
								for (__81fgg2count3126 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn3126 + __81fgg2step3126) / __81fgg2step3126)), _b5p6od9s = __81fgg2dlsvn3126; __81fgg2count3126 != 0; __81fgg2count3126--, _b5p6od9s += (__81fgg2step3126)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = _d0547bi2;
Mark90:;
									// continue
								}
																}							}
						}
						else
						if (_bafcbx97 != _kxg5drh2)
						{
							
							{
								System.Int32 __81fgg2dlsvn3127 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3127 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3127;
								for (__81fgg2count3127 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn3127 + __81fgg2step3127) / __81fgg2step3127)), _b5p6od9s = __81fgg2dlsvn3127; __81fgg2count3127 != 0; __81fgg2count3127--, _b5p6od9s += (__81fgg2step3127)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark100:;
									// continue
								}
																}							}
						}
						
						{
							System.Int32 __81fgg2dlsvn3128 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3128 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3128;
							for (__81fgg2count3128 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn3128 + __81fgg2step3128) / __81fgg2step3128)), _68ec3gbh = __81fgg2dlsvn3128; __81fgg2count3128 != 0; __81fgg2count3128--, _68ec3gbh += (__81fgg2step3128)) {

							{
								
								if ((*(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) != _d0547bi2) | (*(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)) != _d0547bi2))
								{
									
									_yc8h372p = (_r7cfteg3 * *(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)));
									_q3ig7mub = (_r7cfteg3 * *(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)));
									{
										System.Int32 __81fgg2dlsvn3129 = (System.Int32)((int)1);
										const System.Int32 __81fgg2step3129 = (System.Int32)((int)1);
										System.Int32 __81fgg2count3129;
										for (__81fgg2count3129 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn3129 + __81fgg2step3129) / __81fgg2step3129)), _b5p6od9s = __81fgg2dlsvn3129; __81fgg2count3129 != 0; __81fgg2count3129--, _b5p6od9s += (__81fgg2step3129)) {

										{
											
											*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = ((*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) + (*(_vxfgpup9+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) * _yc8h372p)) + (*(_p9n405a5+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)) * _q3ig7mub));
Mark110:;
											// continue
										}
																				}									}
								}
								
Mark120:;
								// continue
							}
														}						}
Mark130:;
						// continue
					}
										}				}
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn3130 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3130 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3130;
					for (__81fgg2count3130 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3130 + __81fgg2step3130) / __81fgg2step3130)), _znpjgsef = __81fgg2dlsvn3130; __81fgg2count3130 != 0; __81fgg2count3130--, _znpjgsef += (__81fgg2step3130)) {

					{
						
						if (_bafcbx97 == _d0547bi2)
						{
							
							{
								System.Int32 __81fgg2dlsvn3131 = (System.Int32)(_znpjgsef);
								const System.Int32 __81fgg2step3131 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3131;
								for (__81fgg2count3131 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3131 + __81fgg2step3131) / __81fgg2step3131)), _b5p6od9s = __81fgg2dlsvn3131; __81fgg2count3131 != 0; __81fgg2count3131--, _b5p6od9s += (__81fgg2step3131)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = _d0547bi2;
Mark140:;
									// continue
								}
																}							}
						}
						else
						if (_bafcbx97 != _kxg5drh2)
						{
							
							{
								System.Int32 __81fgg2dlsvn3132 = (System.Int32)(_znpjgsef);
								const System.Int32 __81fgg2step3132 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3132;
								for (__81fgg2count3132 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3132 + __81fgg2step3132) / __81fgg2step3132)), _b5p6od9s = __81fgg2dlsvn3132; __81fgg2count3132 != 0; __81fgg2count3132--, _b5p6od9s += (__81fgg2step3132)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark150:;
									// continue
								}
																}							}
						}
						
						{
							System.Int32 __81fgg2dlsvn3133 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3133 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3133;
							for (__81fgg2count3133 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn3133 + __81fgg2step3133) / __81fgg2step3133)), _68ec3gbh = __81fgg2dlsvn3133; __81fgg2count3133 != 0; __81fgg2count3133--, _68ec3gbh += (__81fgg2step3133)) {

							{
								
								if ((*(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) != _d0547bi2) | (*(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)) != _d0547bi2))
								{
									
									_yc8h372p = (_r7cfteg3 * *(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)));
									_q3ig7mub = (_r7cfteg3 * *(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)));
									{
										System.Int32 __81fgg2dlsvn3134 = (System.Int32)(_znpjgsef);
										const System.Int32 __81fgg2step3134 = (System.Int32)((int)1);
										System.Int32 __81fgg2count3134;
										for (__81fgg2count3134 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3134 + __81fgg2step3134) / __81fgg2step3134)), _b5p6od9s = __81fgg2dlsvn3134; __81fgg2count3134 != 0; __81fgg2count3134--, _b5p6od9s += (__81fgg2step3134)) {

										{
											
											*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = ((*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) + (*(_vxfgpup9+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) * _yc8h372p)) + (*(_p9n405a5+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)) * _q3ig7mub));
Mark160:;
											// continue
										}
																				}									}
								}
								
Mark170:;
								// continue
							}
														}						}
Mark180:;
						// continue
					}
										}				}
			}
			
		}
		else
		{
			//* 
			//*        Form  C := alpha*A**T*B + alpha*B**T*A + C. 
			//* 
			
			if (_l08igmvf)
			{
				
				{
					System.Int32 __81fgg2dlsvn3135 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3135 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3135;
					for (__81fgg2count3135 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3135 + __81fgg2step3135) / __81fgg2step3135)), _znpjgsef = __81fgg2dlsvn3135; __81fgg2count3135 != 0; __81fgg2count3135--, _znpjgsef += (__81fgg2step3135)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn3136 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3136 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3136;
							for (__81fgg2count3136 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn3136 + __81fgg2step3136) / __81fgg2step3136)), _b5p6od9s = __81fgg2dlsvn3136; __81fgg2count3136 != 0; __81fgg2count3136--, _b5p6od9s += (__81fgg2step3136)) {

							{
								
								_yc8h372p = _d0547bi2;
								_q3ig7mub = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn3137 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step3137 = (System.Int32)((int)1);
									System.Int32 __81fgg2count3137;
									for (__81fgg2count3137 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn3137 + __81fgg2step3137) / __81fgg2step3137)), _68ec3gbh = __81fgg2dlsvn3137; __81fgg2count3137 != 0; __81fgg2count3137--, _68ec3gbh += (__81fgg2step3137)) {

									{
										
										_yc8h372p = (_yc8h372p + (*(_vxfgpup9+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) * *(_p9n405a5+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
										_q3ig7mub = (_q3ig7mub + (*(_p9n405a5+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ly9opahg)) * *(_vxfgpup9+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
Mark190:;
										// continue
									}
																		}								}
								if (_bafcbx97 == _d0547bi2)
								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = ((_r7cfteg3 * _yc8h372p) + (_r7cfteg3 * _q3ig7mub));
								}
								else
								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (((_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))) + (_r7cfteg3 * _yc8h372p)) + (_r7cfteg3 * _q3ig7mub));
								}
								
Mark200:;
								// continue
							}
														}						}
Mark210:;
						// continue
					}
										}				}
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn3138 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3138 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3138;
					for (__81fgg2count3138 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3138 + __81fgg2step3138) / __81fgg2step3138)), _znpjgsef = __81fgg2dlsvn3138; __81fgg2count3138 != 0; __81fgg2count3138--, _znpjgsef += (__81fgg2step3138)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn3139 = (System.Int32)(_znpjgsef);
							const System.Int32 __81fgg2step3139 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3139;
							for (__81fgg2count3139 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3139 + __81fgg2step3139) / __81fgg2step3139)), _b5p6od9s = __81fgg2dlsvn3139; __81fgg2count3139 != 0; __81fgg2count3139--, _b5p6od9s += (__81fgg2step3139)) {

							{
								
								_yc8h372p = _d0547bi2;
								_q3ig7mub = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn3140 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step3140 = (System.Int32)((int)1);
									System.Int32 __81fgg2count3140;
									for (__81fgg2count3140 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn3140 + __81fgg2step3140) / __81fgg2step3140)), _68ec3gbh = __81fgg2dlsvn3140; __81fgg2count3140 != 0; __81fgg2count3140--, _68ec3gbh += (__81fgg2step3140)) {

									{
										
										_yc8h372p = (_yc8h372p + (*(_vxfgpup9+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) * *(_p9n405a5+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
										_q3ig7mub = (_q3ig7mub + (*(_p9n405a5+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ly9opahg)) * *(_vxfgpup9+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
Mark220:;
										// continue
									}
																		}								}
								if (_bafcbx97 == _d0547bi2)
								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = ((_r7cfteg3 * _yc8h372p) + (_r7cfteg3 * _q3ig7mub));
								}
								else
								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (((_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))) + (_r7cfteg3 * _yc8h372p)) + (_r7cfteg3 * _q3ig7mub));
								}
								
Mark230:;
								// continue
							}
														}						}
Mark240:;
						// continue
					}
										}				}
			}
			
		}
		//* 
		
		return;//* 
		//*     End of DSYR2K. 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
