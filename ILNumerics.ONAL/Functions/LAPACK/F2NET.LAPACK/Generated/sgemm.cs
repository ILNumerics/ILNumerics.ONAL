
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
//*> \brief \b SGEMM 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SGEMM(TRANSA,TRANSB,M,N,K,ALPHA,A,LDA,B,LDB,BETA,C,LDC) 
//* 
//*       .. Scalar Arguments .. 
//*       REAL ALPHA,BETA 
//*       INTEGER K,LDA,LDB,LDC,M,N 
//*       CHARACTER TRANSA,TRANSB 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL A(LDA,*),B(LDB,*),C(LDC,*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SGEMM  performs one of the matrix-matrix operations 
//*> 
//*>    C := alpha*op( A )*op( B ) + beta*C, 
//*> 
//*> where  op( X ) is one of 
//*> 
//*>    op( X ) = X   or   op( X ) = X**T, 
//*> 
//*> alpha and beta are scalars, and A, B and C are matrices, with op( A ) 
//*> an m by k matrix,  op( B )  a  k by n matrix and  C an m by n matrix. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] TRANSA 
//*> \verbatim 
//*>          TRANSA is CHARACTER*1 
//*>           On entry, TRANSA specifies the form of op( A ) to be used in 
//*>           the matrix multiplication as follows: 
//*> 
//*>              TRANSA = 'N' or 'n',  op( A ) = A. 
//*> 
//*>              TRANSA = 'T' or 't',  op( A ) = A**T. 
//*> 
//*>              TRANSA = 'C' or 'c',  op( A ) = A**T. 
//*> \endverbatim 
//*> 
//*> \param[in] TRANSB 
//*> \verbatim 
//*>          TRANSB is CHARACTER*1 
//*>           On entry, TRANSB specifies the form of op( B ) to be used in 
//*>           the matrix multiplication as follows: 
//*> 
//*>              TRANSB = 'N' or 'n',  op( B ) = B. 
//*> 
//*>              TRANSB = 'T' or 't',  op( B ) = B**T. 
//*> 
//*>              TRANSB = 'C' or 'c',  op( B ) = B**T. 
//*> \endverbatim 
//*> 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>           On entry,  M  specifies  the number  of rows  of the  matrix 
//*>           op( A )  and of the  matrix  C.  M  must  be at least  zero. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>           On entry,  N  specifies the number  of columns of the matrix 
//*>           op( B ) and the number of columns of the matrix C. N must be 
//*>           at least zero. 
//*> \endverbatim 
//*> 
//*> \param[in] K 
//*> \verbatim 
//*>          K is INTEGER 
//*>           On entry,  K  specifies  the number of columns of the matrix 
//*>           op( A ) and the number of rows of the matrix op( B ). K must 
//*>           be at least  zero. 
//*> \endverbatim 
//*> 
//*> \param[in] ALPHA 
//*> \verbatim 
//*>          ALPHA is REAL 
//*>           On entry, ALPHA specifies the scalar alpha. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is REAL array, dimension ( LDA, ka ), where ka is 
//*>           k  when  TRANSA = 'N' or 'n',  and is  m  otherwise. 
//*>           Before entry with  TRANSA = 'N' or 'n',  the leading  m by k 
//*>           part of the array  A  must contain the matrix  A,  otherwise 
//*>           the leading  k by m  part of the array  A  must contain  the 
//*>           matrix A. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>           On entry, LDA specifies the first dimension of A as declared 
//*>           in the calling (sub) program. When  TRANSA = 'N' or 'n' then 
//*>           LDA must be at least  max( 1, m ), otherwise  LDA must be at 
//*>           least  max( 1, k ). 
//*> \endverbatim 
//*> 
//*> \param[in] B 
//*> \verbatim 
//*>          B is REAL array, dimension ( LDB, kb ), where kb is 
//*>           n  when  TRANSB = 'N' or 'n',  and is  k  otherwise. 
//*>           Before entry with  TRANSB = 'N' or 'n',  the leading  k by n 
//*>           part of the array  B  must contain the matrix  B,  otherwise 
//*>           the leading  n by k  part of the array  B  must contain  the 
//*>           matrix B. 
//*> \endverbatim 
//*> 
//*> \param[in] LDB 
//*> \verbatim 
//*>          LDB is INTEGER 
//*>           On entry, LDB specifies the first dimension of B as declared 
//*>           in the calling (sub) program. When  TRANSB = 'N' or 'n' then 
//*>           LDB must be at least  max( 1, k ), otherwise  LDB must be at 
//*>           least  max( 1, n ). 
//*> \endverbatim 
//*> 
//*> \param[in] BETA 
//*> \verbatim 
//*>          BETA is REAL 
//*>           On entry,  BETA  specifies the scalar  beta.  When  BETA  is 
//*>           supplied as zero then C need not be set on input. 
//*> \endverbatim 
//*> 
//*> \param[in,out] C 
//*> \verbatim 
//*>          C is REAL array, dimension ( LDC, N ) 
//*>           Before entry, the leading  m by n  part of the array  C must 
//*>           contain the matrix  C,  except when  beta  is zero, in which 
//*>           case C need not be set on entry. 
//*>           On exit, the array  C  is overwritten by the  m by n  matrix 
//*>           ( alpha*op( A )*op( B ) + beta*C ). 
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

	 
	public static void _b8wa9454(FString _742vrzth, FString _30rlu6np, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, ref Single _r7cfteg3, Single* _vxfgpup9, ref Int32 _ocv8fk5c, Single* _p9n405a5, ref Int32 _ly9opahg, ref Single _bafcbx97, Single* _3crf0qn3, ref Int32 _1s3eymp4)
	{
#region variable declarations
Single _1ajfmh55 =  default;
Int32 _b5p6od9s =  default;
Int32 _gro5yvfo =  default;
Int32 _znpjgsef =  default;
Int32 _68ec3gbh =  default;
Int32 _a7s478dp =  default;
Int32 _o9a6qdux =  default;
Int32 _80j3zf13 =  default;
Boolean _skucdj1b =  default;
Boolean _x18i1ben =  default;
Single _kxg5drh2 =  1f;
Single _d0547bi2 =  0f;
string fLanavab = default;
#endregion  variable declarations
_742vrzth = _742vrzth.Convert(1);
_30rlu6np = _30rlu6np.Convert(1);

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
		//*     Set  NOTA  and  NOTB  as  true if  A  and  B  respectively are not 
		//*     transposed and set  NROWA, NCOLA and  NROWB  as the number of rows 
		//*     and  columns of  A  and the  number of  rows  of  B  respectively. 
		//* 
		
		_skucdj1b = _w8y2rzgy(_742vrzth ,"N" );
		_x18i1ben = _w8y2rzgy(_30rlu6np ,"N" );
		if (_skucdj1b)
		{
			
			_o9a6qdux = _ev4xhht5;
			_a7s478dp = _umlkckdg;
		}
		else
		{
			
			_o9a6qdux = _umlkckdg;
			_a7s478dp = _ev4xhht5;
		}
		
		if (_x18i1ben)
		{
			
			_80j3zf13 = _umlkckdg;
		}
		else
		{
			
			_80j3zf13 = _dxpq0xkr;
		}
		//* 
		//*     Test the input parameters. 
		//* 
		
		_gro5yvfo = (int)0;
		if (((!(_skucdj1b)) & (!(_w8y2rzgy(_742vrzth ,"C" )))) & (!(_w8y2rzgy(_742vrzth ,"T" ))))
		{
			
			_gro5yvfo = (int)1;
		}
		else
		if (((!(_x18i1ben)) & (!(_w8y2rzgy(_30rlu6np ,"C" )))) & (!(_w8y2rzgy(_30rlu6np ,"T" ))))
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
		if (_umlkckdg < (int)0)
		{
			
			_gro5yvfo = (int)5;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_o9a6qdux ))
		{
			
			_gro5yvfo = (int)8;
		}
		else
		if (_ly9opahg < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_80j3zf13 ))
		{
			
			_gro5yvfo = (int)10;
		}
		else
		if (_1s3eymp4 < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_ev4xhht5 ))
		{
			
			_gro5yvfo = (int)13;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("SGEMM " ,ref _gro5yvfo );
			return;
		}
		//* 
		//*     Quick return if possible. 
		//* 
		
		if (((_ev4xhht5 == (int)0) | (_dxpq0xkr == (int)0)) | (((_r7cfteg3 == _d0547bi2) | (_umlkckdg == (int)0)) & (_bafcbx97 == _kxg5drh2)))
		return;//* 
		//*     And if  alpha.eq.zero. 
		//* 
		
		if (_r7cfteg3 == _d0547bi2)
		{
			
			if (_bafcbx97 == _d0547bi2)
			{
				
				{
					System.Int32 __81fgg2dlsvn73 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step73 = (System.Int32)((int)1);
					System.Int32 __81fgg2count73;
					for (__81fgg2count73 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn73 + __81fgg2step73) / __81fgg2step73)), _znpjgsef = __81fgg2dlsvn73; __81fgg2count73 != 0; __81fgg2count73--, _znpjgsef += (__81fgg2step73)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn74 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step74 = (System.Int32)((int)1);
							System.Int32 __81fgg2count74;
							for (__81fgg2count74 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn74 + __81fgg2step74) / __81fgg2step74)), _b5p6od9s = __81fgg2dlsvn74; __81fgg2count74 != 0; __81fgg2count74--, _b5p6od9s += (__81fgg2step74)) {

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
					System.Int32 __81fgg2dlsvn75 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step75 = (System.Int32)((int)1);
					System.Int32 __81fgg2count75;
					for (__81fgg2count75 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn75 + __81fgg2step75) / __81fgg2step75)), _znpjgsef = __81fgg2dlsvn75; __81fgg2count75 != 0; __81fgg2count75--, _znpjgsef += (__81fgg2step75)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn76 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step76 = (System.Int32)((int)1);
							System.Int32 __81fgg2count76;
							for (__81fgg2count76 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn76 + __81fgg2step76) / __81fgg2step76)), _b5p6od9s = __81fgg2dlsvn76; __81fgg2count76 != 0; __81fgg2count76--, _b5p6od9s += (__81fgg2step76)) {

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
		
		if (_x18i1ben)
		{
			
			if (_skucdj1b)
			{
				//* 
				//*           Form  C := alpha*A*B + beta*C. 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn77 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step77 = (System.Int32)((int)1);
					System.Int32 __81fgg2count77;
					for (__81fgg2count77 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn77 + __81fgg2step77) / __81fgg2step77)), _znpjgsef = __81fgg2dlsvn77; __81fgg2count77 != 0; __81fgg2count77--, _znpjgsef += (__81fgg2step77)) {

					{
						
						if (_bafcbx97 == _d0547bi2)
						{
							
							{
								System.Int32 __81fgg2dlsvn78 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step78 = (System.Int32)((int)1);
								System.Int32 __81fgg2count78;
								for (__81fgg2count78 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn78 + __81fgg2step78) / __81fgg2step78)), _b5p6od9s = __81fgg2dlsvn78; __81fgg2count78 != 0; __81fgg2count78--, _b5p6od9s += (__81fgg2step78)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = _d0547bi2;
Mark50:;
									// continue
								}
																}							}
						}
						else
						if (_bafcbx97 != _kxg5drh2)
						{
							
							{
								System.Int32 __81fgg2dlsvn79 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step79 = (System.Int32)((int)1);
								System.Int32 __81fgg2count79;
								for (__81fgg2count79 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn79 + __81fgg2step79) / __81fgg2step79)), _b5p6od9s = __81fgg2dlsvn79; __81fgg2count79 != 0; __81fgg2count79--, _b5p6od9s += (__81fgg2step79)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark60:;
									// continue
								}
																}							}
						}
						
						{
							System.Int32 __81fgg2dlsvn80 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step80 = (System.Int32)((int)1);
							System.Int32 __81fgg2count80;
							for (__81fgg2count80 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn80 + __81fgg2step80) / __81fgg2step80)), _68ec3gbh = __81fgg2dlsvn80; __81fgg2count80 != 0; __81fgg2count80--, _68ec3gbh += (__81fgg2step80)) {

							{
								
								_1ajfmh55 = (_r7cfteg3 * *(_p9n405a5+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
								{
									System.Int32 __81fgg2dlsvn81 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step81 = (System.Int32)((int)1);
									System.Int32 __81fgg2count81;
									for (__81fgg2count81 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn81 + __81fgg2step81) / __81fgg2step81)), _b5p6od9s = __81fgg2dlsvn81; __81fgg2count81 != 0; __81fgg2count81--, _b5p6od9s += (__81fgg2step81)) {

									{
										
										*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) + (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c))));
Mark70:;
										// continue
									}
																		}								}
Mark80:;
								// continue
							}
														}						}
Mark90:;
						// continue
					}
										}				}
			}
			else
			{
				//* 
				//*           Form  C := alpha*A**T*B + beta*C 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn82 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step82 = (System.Int32)((int)1);
					System.Int32 __81fgg2count82;
					for (__81fgg2count82 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn82 + __81fgg2step82) / __81fgg2step82)), _znpjgsef = __81fgg2dlsvn82; __81fgg2count82 != 0; __81fgg2count82--, _znpjgsef += (__81fgg2step82)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn83 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step83 = (System.Int32)((int)1);
							System.Int32 __81fgg2count83;
							for (__81fgg2count83 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn83 + __81fgg2step83) / __81fgg2step83)), _b5p6od9s = __81fgg2dlsvn83; __81fgg2count83 != 0; __81fgg2count83--, _b5p6od9s += (__81fgg2step83)) {

							{
								
								_1ajfmh55 = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn84 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step84 = (System.Int32)((int)1);
									System.Int32 __81fgg2count84;
									for (__81fgg2count84 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn84 + __81fgg2step84) / __81fgg2step84)), _68ec3gbh = __81fgg2dlsvn84; __81fgg2count84 != 0; __81fgg2count84--, _68ec3gbh += (__81fgg2step84)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 + (*(_vxfgpup9+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) * *(_p9n405a5+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
Mark100:;
										// continue
									}
																		}								}
								if (_bafcbx97 == _d0547bi2)
								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_r7cfteg3 * _1ajfmh55);
								}
								else
								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = ((_r7cfteg3 * _1ajfmh55) + (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))));
								}
								
Mark110:;
								// continue
							}
														}						}
Mark120:;
						// continue
					}
										}				}
			}
			
		}
		else
		{
			
			if (_skucdj1b)
			{
				//* 
				//*           Form  C := alpha*A*B**T + beta*C 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn85 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step85 = (System.Int32)((int)1);
					System.Int32 __81fgg2count85;
					for (__81fgg2count85 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn85 + __81fgg2step85) / __81fgg2step85)), _znpjgsef = __81fgg2dlsvn85; __81fgg2count85 != 0; __81fgg2count85--, _znpjgsef += (__81fgg2step85)) {

					{
						
						if (_bafcbx97 == _d0547bi2)
						{
							
							{
								System.Int32 __81fgg2dlsvn86 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step86 = (System.Int32)((int)1);
								System.Int32 __81fgg2count86;
								for (__81fgg2count86 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn86 + __81fgg2step86) / __81fgg2step86)), _b5p6od9s = __81fgg2dlsvn86; __81fgg2count86 != 0; __81fgg2count86--, _b5p6od9s += (__81fgg2step86)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = _d0547bi2;
Mark130:;
									// continue
								}
																}							}
						}
						else
						if (_bafcbx97 != _kxg5drh2)
						{
							
							{
								System.Int32 __81fgg2dlsvn87 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step87 = (System.Int32)((int)1);
								System.Int32 __81fgg2count87;
								for (__81fgg2count87 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn87 + __81fgg2step87) / __81fgg2step87)), _b5p6od9s = __81fgg2dlsvn87; __81fgg2count87 != 0; __81fgg2count87--, _b5p6od9s += (__81fgg2step87)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark140:;
									// continue
								}
																}							}
						}
						
						{
							System.Int32 __81fgg2dlsvn88 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step88 = (System.Int32)((int)1);
							System.Int32 __81fgg2count88;
							for (__81fgg2count88 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn88 + __81fgg2step88) / __81fgg2step88)), _68ec3gbh = __81fgg2dlsvn88; __81fgg2count88 != 0; __81fgg2count88--, _68ec3gbh += (__81fgg2step88)) {

							{
								
								_1ajfmh55 = (_r7cfteg3 * *(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)));
								{
									System.Int32 __81fgg2dlsvn89 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step89 = (System.Int32)((int)1);
									System.Int32 __81fgg2count89;
									for (__81fgg2count89 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn89 + __81fgg2step89) / __81fgg2step89)), _b5p6od9s = __81fgg2dlsvn89; __81fgg2count89 != 0; __81fgg2count89--, _b5p6od9s += (__81fgg2step89)) {

									{
										
										*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) + (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c))));
Mark150:;
										// continue
									}
																		}								}
Mark160:;
								// continue
							}
														}						}
Mark170:;
						// continue
					}
										}				}
			}
			else
			{
				//* 
				//*           Form  C := alpha*A**T*B**T + beta*C 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn90 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step90 = (System.Int32)((int)1);
					System.Int32 __81fgg2count90;
					for (__81fgg2count90 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn90 + __81fgg2step90) / __81fgg2step90)), _znpjgsef = __81fgg2dlsvn90; __81fgg2count90 != 0; __81fgg2count90--, _znpjgsef += (__81fgg2step90)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn91 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step91 = (System.Int32)((int)1);
							System.Int32 __81fgg2count91;
							for (__81fgg2count91 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn91 + __81fgg2step91) / __81fgg2step91)), _b5p6od9s = __81fgg2dlsvn91; __81fgg2count91 != 0; __81fgg2count91--, _b5p6od9s += (__81fgg2step91)) {

							{
								
								_1ajfmh55 = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn92 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step92 = (System.Int32)((int)1);
									System.Int32 __81fgg2count92;
									for (__81fgg2count92 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn92 + __81fgg2step92) / __81fgg2step92)), _68ec3gbh = __81fgg2dlsvn92; __81fgg2count92 != 0; __81fgg2count92--, _68ec3gbh += (__81fgg2step92)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 + (*(_vxfgpup9+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) * *(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg))));
Mark180:;
										// continue
									}
																		}								}
								if (_bafcbx97 == _d0547bi2)
								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_r7cfteg3 * _1ajfmh55);
								}
								else
								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = ((_r7cfteg3 * _1ajfmh55) + (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))));
								}
								
Mark190:;
								// continue
							}
														}						}
Mark200:;
						// continue
					}
										}				}
			}
			
		}
		//* 
		
		return;//* 
		//*     End of SGEMM . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
