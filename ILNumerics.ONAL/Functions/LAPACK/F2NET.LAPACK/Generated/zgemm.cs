
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
//*> \brief \b ZGEMM 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZGEMM(TRANSA,TRANSB,M,N,K,ALPHA,A,LDA,B,LDB,BETA,C,LDC) 
//* 
//*       .. Scalar Arguments .. 
//*       COMPLEX*16 ALPHA,BETA 
//*       INTEGER K,LDA,LDB,LDC,M,N 
//*       CHARACTER TRANSA,TRANSB 
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
//*> ZGEMM  performs one of the matrix-matrix operations 
//*> 
//*>    C := alpha*op( A )*op( B ) + beta*C, 
//*> 
//*> where  op( X ) is one of 
//*> 
//*>    op( X ) = X   or   op( X ) = X**T   or   op( X ) = X**H, 
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
//*>              TRANSA = 'C' or 'c',  op( A ) = A**H. 
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
//*>              TRANSB = 'C' or 'c',  op( B ) = B**H. 
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
//*>          ALPHA is COMPLEX*16 
//*>           On entry, ALPHA specifies the scalar alpha. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is COMPLEX*16 array, dimension ( LDA, ka ), where ka is 
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
//*>          B is COMPLEX*16 array, dimension ( LDB, kb ), where kb is 
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

	 
	public static void _xos1d1er(FString _742vrzth, FString _30rlu6np, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, ref complex _r7cfteg3, complex* _vxfgpup9, ref Int32 _ocv8fk5c, complex* _p9n405a5, ref Int32 _ly9opahg, ref complex _bafcbx97, complex* _3crf0qn3, ref Int32 _1s3eymp4)
	{
#region variable declarations
complex _1ajfmh55 =  default;
Int32 _b5p6od9s =  default;
Int32 _gro5yvfo =  default;
Int32 _znpjgsef =  default;
Int32 _68ec3gbh =  default;
Int32 _a7s478dp =  default;
Int32 _o9a6qdux =  default;
Int32 _80j3zf13 =  default;
Boolean _gzlxoamf =  default;
Boolean _i8jzem57 =  default;
Boolean _skucdj1b =  default;
Boolean _x18i1ben =  default;
complex _kxg5drh2 =   new fcomplex(1f,0f);
complex _d0547bi2 =   new fcomplex(0f,0f);
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
		//*     conjugated or transposed, set  CONJA and CONJB  as true if  A  and 
		//*     B  respectively are to be  transposed but  not conjugated  and set 
		//*     NROWA, NCOLA and  NROWB  as the number of rows and  columns  of  A 
		//*     and the number of rows of  B  respectively. 
		//* 
		
		_skucdj1b = _w8y2rzgy(_742vrzth ,"N" );
		_x18i1ben = _w8y2rzgy(_30rlu6np ,"N" );
		_gzlxoamf = _w8y2rzgy(_742vrzth ,"C" );
		_i8jzem57 = _w8y2rzgy(_30rlu6np ,"C" );
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
		if (((!(_skucdj1b)) & (!(_gzlxoamf))) & (!(_w8y2rzgy(_742vrzth ,"T" ))))
		{
			
			_gro5yvfo = (int)1;
		}
		else
		if (((!(_x18i1ben)) & (!(_i8jzem57))) & (!(_w8y2rzgy(_30rlu6np ,"T" ))))
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
			
			_ut9qalzx("ZGEMM " ,ref _gro5yvfo );
			return;
		}
		//* 
		//*     Quick return if possible. 
		//* 
		
		if (((_ev4xhht5 == (int)0) | (_dxpq0xkr == (int)0)) | (((_r7cfteg3 == _d0547bi2) | (_umlkckdg == (int)0)) & (_bafcbx97 == _kxg5drh2)))
		return;//* 
		//*     And when  alpha.eq.zero. 
		//* 
		
		if (_r7cfteg3 == _d0547bi2)
		{
			
			if (_bafcbx97 == _d0547bi2)
			{
				
				{
					System.Int32 __81fgg2dlsvn130 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step130 = (System.Int32)((int)1);
					System.Int32 __81fgg2count130;
					for (__81fgg2count130 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn130 + __81fgg2step130) / __81fgg2step130)), _znpjgsef = __81fgg2dlsvn130; __81fgg2count130 != 0; __81fgg2count130--, _znpjgsef += (__81fgg2step130)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn131 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step131 = (System.Int32)((int)1);
							System.Int32 __81fgg2count131;
							for (__81fgg2count131 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn131 + __81fgg2step131) / __81fgg2step131)), _b5p6od9s = __81fgg2dlsvn131; __81fgg2count131 != 0; __81fgg2count131--, _b5p6od9s += (__81fgg2step131)) {

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
					System.Int32 __81fgg2dlsvn132 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step132 = (System.Int32)((int)1);
					System.Int32 __81fgg2count132;
					for (__81fgg2count132 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn132 + __81fgg2step132) / __81fgg2step132)), _znpjgsef = __81fgg2dlsvn132; __81fgg2count132 != 0; __81fgg2count132--, _znpjgsef += (__81fgg2step132)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn133 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step133 = (System.Int32)((int)1);
							System.Int32 __81fgg2count133;
							for (__81fgg2count133 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn133 + __81fgg2step133) / __81fgg2step133)), _b5p6od9s = __81fgg2dlsvn133; __81fgg2count133 != 0; __81fgg2count133--, _b5p6od9s += (__81fgg2step133)) {

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
					System.Int32 __81fgg2dlsvn134 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step134 = (System.Int32)((int)1);
					System.Int32 __81fgg2count134;
					for (__81fgg2count134 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn134 + __81fgg2step134) / __81fgg2step134)), _znpjgsef = __81fgg2dlsvn134; __81fgg2count134 != 0; __81fgg2count134--, _znpjgsef += (__81fgg2step134)) {

					{
						
						if (_bafcbx97 == _d0547bi2)
						{
							
							{
								System.Int32 __81fgg2dlsvn135 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step135 = (System.Int32)((int)1);
								System.Int32 __81fgg2count135;
								for (__81fgg2count135 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn135 + __81fgg2step135) / __81fgg2step135)), _b5p6od9s = __81fgg2dlsvn135; __81fgg2count135 != 0; __81fgg2count135--, _b5p6od9s += (__81fgg2step135)) {

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
								System.Int32 __81fgg2dlsvn136 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step136 = (System.Int32)((int)1);
								System.Int32 __81fgg2count136;
								for (__81fgg2count136 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn136 + __81fgg2step136) / __81fgg2step136)), _b5p6od9s = __81fgg2dlsvn136; __81fgg2count136 != 0; __81fgg2count136--, _b5p6od9s += (__81fgg2step136)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark60:;
									// continue
								}
																}							}
						}
						
						{
							System.Int32 __81fgg2dlsvn137 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step137 = (System.Int32)((int)1);
							System.Int32 __81fgg2count137;
							for (__81fgg2count137 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn137 + __81fgg2step137) / __81fgg2step137)), _68ec3gbh = __81fgg2dlsvn137; __81fgg2count137 != 0; __81fgg2count137--, _68ec3gbh += (__81fgg2step137)) {

							{
								
								_1ajfmh55 = (_r7cfteg3 * *(_p9n405a5+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
								{
									System.Int32 __81fgg2dlsvn138 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step138 = (System.Int32)((int)1);
									System.Int32 __81fgg2count138;
									for (__81fgg2count138 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn138 + __81fgg2step138) / __81fgg2step138)), _b5p6od9s = __81fgg2dlsvn138; __81fgg2count138 != 0; __81fgg2count138--, _b5p6od9s += (__81fgg2step138)) {

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
			if (_gzlxoamf)
			{
				//* 
				//*           Form  C := alpha*A**H*B + beta*C. 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn139 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step139 = (System.Int32)((int)1);
					System.Int32 __81fgg2count139;
					for (__81fgg2count139 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn139 + __81fgg2step139) / __81fgg2step139)), _znpjgsef = __81fgg2dlsvn139; __81fgg2count139 != 0; __81fgg2count139--, _znpjgsef += (__81fgg2step139)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn140 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step140 = (System.Int32)((int)1);
							System.Int32 __81fgg2count140;
							for (__81fgg2count140 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn140 + __81fgg2step140) / __81fgg2step140)), _b5p6od9s = __81fgg2dlsvn140; __81fgg2count140 != 0; __81fgg2count140--, _b5p6od9s += (__81fgg2step140)) {

							{
								
								_1ajfmh55 = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn141 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step141 = (System.Int32)((int)1);
									System.Int32 __81fgg2count141;
									for (__81fgg2count141 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn141 + __81fgg2step141) / __81fgg2step141)), _68ec3gbh = __81fgg2dlsvn141; __81fgg2count141 != 0; __81fgg2count141--, _68ec3gbh += (__81fgg2step141)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 + (ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ) * *(_p9n405a5+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
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
			else
			{
				//* 
				//*           Form  C := alpha*A**T*B + beta*C 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn142 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step142 = (System.Int32)((int)1);
					System.Int32 __81fgg2count142;
					for (__81fgg2count142 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn142 + __81fgg2step142) / __81fgg2step142)), _znpjgsef = __81fgg2dlsvn142; __81fgg2count142 != 0; __81fgg2count142--, _znpjgsef += (__81fgg2step142)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn143 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step143 = (System.Int32)((int)1);
							System.Int32 __81fgg2count143;
							for (__81fgg2count143 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn143 + __81fgg2step143) / __81fgg2step143)), _b5p6od9s = __81fgg2dlsvn143; __81fgg2count143 != 0; __81fgg2count143--, _b5p6od9s += (__81fgg2step143)) {

							{
								
								_1ajfmh55 = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn144 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step144 = (System.Int32)((int)1);
									System.Int32 __81fgg2count144;
									for (__81fgg2count144 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn144 + __81fgg2step144) / __81fgg2step144)), _68ec3gbh = __81fgg2dlsvn144; __81fgg2count144 != 0; __81fgg2count144--, _68ec3gbh += (__81fgg2step144)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 + (*(_vxfgpup9+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) * *(_p9n405a5+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
Mark130:;
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
								
Mark140:;
								// continue
							}
														}						}
Mark150:;
						// continue
					}
										}				}
			}
			
		}
		else
		if (_skucdj1b)
		{
			
			if (_i8jzem57)
			{
				//* 
				//*           Form  C := alpha*A*B**H + beta*C. 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn145 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step145 = (System.Int32)((int)1);
					System.Int32 __81fgg2count145;
					for (__81fgg2count145 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn145 + __81fgg2step145) / __81fgg2step145)), _znpjgsef = __81fgg2dlsvn145; __81fgg2count145 != 0; __81fgg2count145--, _znpjgsef += (__81fgg2step145)) {

					{
						
						if (_bafcbx97 == _d0547bi2)
						{
							
							{
								System.Int32 __81fgg2dlsvn146 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step146 = (System.Int32)((int)1);
								System.Int32 __81fgg2count146;
								for (__81fgg2count146 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn146 + __81fgg2step146) / __81fgg2step146)), _b5p6od9s = __81fgg2dlsvn146; __81fgg2count146 != 0; __81fgg2count146--, _b5p6od9s += (__81fgg2step146)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = _d0547bi2;
Mark160:;
									// continue
								}
																}							}
						}
						else
						if (_bafcbx97 != _kxg5drh2)
						{
							
							{
								System.Int32 __81fgg2dlsvn147 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step147 = (System.Int32)((int)1);
								System.Int32 __81fgg2count147;
								for (__81fgg2count147 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn147 + __81fgg2step147) / __81fgg2step147)), _b5p6od9s = __81fgg2dlsvn147; __81fgg2count147 != 0; __81fgg2count147--, _b5p6od9s += (__81fgg2step147)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark170:;
									// continue
								}
																}							}
						}
						
						{
							System.Int32 __81fgg2dlsvn148 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step148 = (System.Int32)((int)1);
							System.Int32 __81fgg2count148;
							for (__81fgg2count148 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn148 + __81fgg2step148) / __81fgg2step148)), _68ec3gbh = __81fgg2dlsvn148; __81fgg2count148 != 0; __81fgg2count148--, _68ec3gbh += (__81fgg2step148)) {

							{
								
								_1ajfmh55 = (_r7cfteg3 * ILNumerics.F2NET.Intrinsics.DCONJG(*(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)) ));
								{
									System.Int32 __81fgg2dlsvn149 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step149 = (System.Int32)((int)1);
									System.Int32 __81fgg2count149;
									for (__81fgg2count149 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn149 + __81fgg2step149) / __81fgg2step149)), _b5p6od9s = __81fgg2dlsvn149; __81fgg2count149 != 0; __81fgg2count149--, _b5p6od9s += (__81fgg2step149)) {

									{
										
										*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) + (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c))));
Mark180:;
										// continue
									}
																		}								}
Mark190:;
								// continue
							}
														}						}
Mark200:;
						// continue
					}
										}				}
			}
			else
			{
				//* 
				//*           Form  C := alpha*A*B**T + beta*C 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn150 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step150 = (System.Int32)((int)1);
					System.Int32 __81fgg2count150;
					for (__81fgg2count150 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn150 + __81fgg2step150) / __81fgg2step150)), _znpjgsef = __81fgg2dlsvn150; __81fgg2count150 != 0; __81fgg2count150--, _znpjgsef += (__81fgg2step150)) {

					{
						
						if (_bafcbx97 == _d0547bi2)
						{
							
							{
								System.Int32 __81fgg2dlsvn151 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step151 = (System.Int32)((int)1);
								System.Int32 __81fgg2count151;
								for (__81fgg2count151 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn151 + __81fgg2step151) / __81fgg2step151)), _b5p6od9s = __81fgg2dlsvn151; __81fgg2count151 != 0; __81fgg2count151--, _b5p6od9s += (__81fgg2step151)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = _d0547bi2;
Mark210:;
									// continue
								}
																}							}
						}
						else
						if (_bafcbx97 != _kxg5drh2)
						{
							
							{
								System.Int32 __81fgg2dlsvn152 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step152 = (System.Int32)((int)1);
								System.Int32 __81fgg2count152;
								for (__81fgg2count152 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn152 + __81fgg2step152) / __81fgg2step152)), _b5p6od9s = __81fgg2dlsvn152; __81fgg2count152 != 0; __81fgg2count152--, _b5p6od9s += (__81fgg2step152)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark220:;
									// continue
								}
																}							}
						}
						
						{
							System.Int32 __81fgg2dlsvn153 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step153 = (System.Int32)((int)1);
							System.Int32 __81fgg2count153;
							for (__81fgg2count153 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn153 + __81fgg2step153) / __81fgg2step153)), _68ec3gbh = __81fgg2dlsvn153; __81fgg2count153 != 0; __81fgg2count153--, _68ec3gbh += (__81fgg2step153)) {

							{
								
								_1ajfmh55 = (_r7cfteg3 * *(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)));
								{
									System.Int32 __81fgg2dlsvn154 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step154 = (System.Int32)((int)1);
									System.Int32 __81fgg2count154;
									for (__81fgg2count154 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn154 + __81fgg2step154) / __81fgg2step154)), _b5p6od9s = __81fgg2dlsvn154; __81fgg2count154 != 0; __81fgg2count154--, _b5p6od9s += (__81fgg2step154)) {

									{
										
										*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) + (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c))));
Mark230:;
										// continue
									}
																		}								}
Mark240:;
								// continue
							}
														}						}
Mark250:;
						// continue
					}
										}				}
			}
			
		}
		else
		if (_gzlxoamf)
		{
			
			if (_i8jzem57)
			{
				//* 
				//*           Form  C := alpha*A**H*B**H + beta*C. 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn155 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step155 = (System.Int32)((int)1);
					System.Int32 __81fgg2count155;
					for (__81fgg2count155 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn155 + __81fgg2step155) / __81fgg2step155)), _znpjgsef = __81fgg2dlsvn155; __81fgg2count155 != 0; __81fgg2count155--, _znpjgsef += (__81fgg2step155)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn156 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step156 = (System.Int32)((int)1);
							System.Int32 __81fgg2count156;
							for (__81fgg2count156 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn156 + __81fgg2step156) / __81fgg2step156)), _b5p6od9s = __81fgg2dlsvn156; __81fgg2count156 != 0; __81fgg2count156--, _b5p6od9s += (__81fgg2step156)) {

							{
								
								_1ajfmh55 = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn157 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step157 = (System.Int32)((int)1);
									System.Int32 __81fgg2count157;
									for (__81fgg2count157 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn157 + __81fgg2step157) / __81fgg2step157)), _68ec3gbh = __81fgg2dlsvn157; __81fgg2count157 != 0; __81fgg2count157--, _68ec3gbh += (__81fgg2step157)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 + (ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ) * ILNumerics.F2NET.Intrinsics.DCONJG(*(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)) )));
Mark260:;
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
								
Mark270:;
								// continue
							}
														}						}
Mark280:;
						// continue
					}
										}				}
			}
			else
			{
				//* 
				//*           Form  C := alpha*A**H*B**T + beta*C 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn158 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step158 = (System.Int32)((int)1);
					System.Int32 __81fgg2count158;
					for (__81fgg2count158 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn158 + __81fgg2step158) / __81fgg2step158)), _znpjgsef = __81fgg2dlsvn158; __81fgg2count158 != 0; __81fgg2count158--, _znpjgsef += (__81fgg2step158)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn159 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step159 = (System.Int32)((int)1);
							System.Int32 __81fgg2count159;
							for (__81fgg2count159 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn159 + __81fgg2step159) / __81fgg2step159)), _b5p6od9s = __81fgg2dlsvn159; __81fgg2count159 != 0; __81fgg2count159--, _b5p6od9s += (__81fgg2step159)) {

							{
								
								_1ajfmh55 = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn160 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step160 = (System.Int32)((int)1);
									System.Int32 __81fgg2count160;
									for (__81fgg2count160 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn160 + __81fgg2step160) / __81fgg2step160)), _68ec3gbh = __81fgg2dlsvn160; __81fgg2count160 != 0; __81fgg2count160--, _68ec3gbh += (__81fgg2step160)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 + (ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ) * *(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg))));
Mark290:;
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
								
Mark300:;
								// continue
							}
														}						}
Mark310:;
						// continue
					}
										}				}
			}
			
		}
		else
		{
			
			if (_i8jzem57)
			{
				//* 
				//*           Form  C := alpha*A**T*B**H + beta*C 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn161 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step161 = (System.Int32)((int)1);
					System.Int32 __81fgg2count161;
					for (__81fgg2count161 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn161 + __81fgg2step161) / __81fgg2step161)), _znpjgsef = __81fgg2dlsvn161; __81fgg2count161 != 0; __81fgg2count161--, _znpjgsef += (__81fgg2step161)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn162 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step162 = (System.Int32)((int)1);
							System.Int32 __81fgg2count162;
							for (__81fgg2count162 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn162 + __81fgg2step162) / __81fgg2step162)), _b5p6od9s = __81fgg2dlsvn162; __81fgg2count162 != 0; __81fgg2count162--, _b5p6od9s += (__81fgg2step162)) {

							{
								
								_1ajfmh55 = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn163 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step163 = (System.Int32)((int)1);
									System.Int32 __81fgg2count163;
									for (__81fgg2count163 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn163 + __81fgg2step163) / __81fgg2step163)), _68ec3gbh = __81fgg2dlsvn163; __81fgg2count163 != 0; __81fgg2count163--, _68ec3gbh += (__81fgg2step163)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 + (*(_vxfgpup9+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) * ILNumerics.F2NET.Intrinsics.DCONJG(*(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg)) )));
Mark320:;
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
								
Mark330:;
								// continue
							}
														}						}
Mark340:;
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
					System.Int32 __81fgg2dlsvn164 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step164 = (System.Int32)((int)1);
					System.Int32 __81fgg2count164;
					for (__81fgg2count164 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn164 + __81fgg2step164) / __81fgg2step164)), _znpjgsef = __81fgg2dlsvn164; __81fgg2count164 != 0; __81fgg2count164--, _znpjgsef += (__81fgg2step164)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn165 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step165 = (System.Int32)((int)1);
							System.Int32 __81fgg2count165;
							for (__81fgg2count165 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn165 + __81fgg2step165) / __81fgg2step165)), _b5p6od9s = __81fgg2dlsvn165; __81fgg2count165 != 0; __81fgg2count165--, _b5p6od9s += (__81fgg2step165)) {

							{
								
								_1ajfmh55 = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn166 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step166 = (System.Int32)((int)1);
									System.Int32 __81fgg2count166;
									for (__81fgg2count166 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn166 + __81fgg2step166) / __81fgg2step166)), _68ec3gbh = __81fgg2dlsvn166; __81fgg2count166 != 0; __81fgg2count166--, _68ec3gbh += (__81fgg2step166)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 + (*(_vxfgpup9+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) * *(_p9n405a5+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ly9opahg))));
Mark350:;
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
								
Mark360:;
								// continue
							}
														}						}
Mark370:;
						// continue
					}
										}				}
			}
			
		}
		//* 
		
		return;//* 
		//*     End of ZGEMM . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
