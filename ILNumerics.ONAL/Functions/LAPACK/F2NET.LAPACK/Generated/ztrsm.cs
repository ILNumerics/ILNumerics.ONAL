
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
//*> \brief \b ZTRSM 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZTRSM(SIDE,UPLO,TRANSA,DIAG,M,N,ALPHA,A,LDA,B,LDB) 
//* 
//*       .. Scalar Arguments .. 
//*       COMPLEX*16 ALPHA 
//*       INTEGER LDA,LDB,M,N 
//*       CHARACTER DIAG,SIDE,TRANSA,UPLO 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX*16 A(LDA,*),B(LDB,*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZTRSM  solves one of the matrix equations 
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
//*>          ALPHA is COMPLEX*16 
//*>           On entry,  ALPHA specifies the scalar  alpha. When  alpha is 
//*>           zero then  A is not referenced and  B need not be set before 
//*>           entry. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is COMPLEX*16 array, dimension ( LDA, k ), 
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
//*>          B is COMPLEX*16 array, dimension ( LDB, N ) 
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

	 
	public static void _qlsh8rhv(FString _m2cn2gjg, FString _9wyre9zc, FString _742vrzth, FString _2scffxp3, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref complex _r7cfteg3, complex* _vxfgpup9, ref Int32 _ocv8fk5c, complex* _p9n405a5, ref Int32 _ly9opahg)
	{
#region variable declarations
complex _1ajfmh55 =  default;
Int32 _b5p6od9s =  default;
Int32 _gro5yvfo =  default;
Int32 _znpjgsef =  default;
Int32 _umlkckdg =  default;
Int32 _o9a6qdux =  default;
Boolean _wln8whqb =  default;
Boolean _moml4lap =  default;
Boolean _rcjmgxm4 =  default;
Boolean _l08igmvf =  default;
complex _kxg5drh2 =   new fcomplex(1f,0f);
complex _d0547bi2 =   new fcomplex(0f,0f);
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
			
			_ut9qalzx("ZTRSM " ,ref _gro5yvfo );
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
				System.Int32 __81fgg2dlsvn1678 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1678 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1678;
				for (__81fgg2count1678 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1678 + __81fgg2step1678) / __81fgg2step1678)), _znpjgsef = __81fgg2dlsvn1678; __81fgg2count1678 != 0; __81fgg2count1678--, _znpjgsef += (__81fgg2step1678)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn1679 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1679 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1679;
						for (__81fgg2count1679 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1679 + __81fgg2step1679) / __81fgg2step1679)), _b5p6od9s = __81fgg2dlsvn1679; __81fgg2count1679 != 0; __81fgg2count1679--, _b5p6od9s += (__81fgg2step1679)) {

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
						System.Int32 __81fgg2dlsvn1680 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1680 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1680;
						for (__81fgg2count1680 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1680 + __81fgg2step1680) / __81fgg2step1680)), _znpjgsef = __81fgg2dlsvn1680; __81fgg2count1680 != 0; __81fgg2count1680--, _znpjgsef += (__81fgg2step1680)) {

						{
							
							if (_r7cfteg3 != _kxg5drh2)
							{
								
								{
									System.Int32 __81fgg2dlsvn1681 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1681 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1681;
									for (__81fgg2count1681 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1681 + __81fgg2step1681) / __81fgg2step1681)), _b5p6od9s = __81fgg2dlsvn1681; __81fgg2count1681 != 0; __81fgg2count1681--, _b5p6od9s += (__81fgg2step1681)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
Mark30:;
										// continue
									}
																		}								}
							}
							
							{
								System.Int32 __81fgg2dlsvn1682 = (System.Int32)(_ev4xhht5);
								System.Int32 __81fgg2step1682 = (System.Int32)((int)-1);
								System.Int32 __81fgg2count1682;
								for (__81fgg2count1682 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1682 + __81fgg2step1682) / __81fgg2step1682)), _umlkckdg = __81fgg2dlsvn1682; __81fgg2count1682 != 0; __81fgg2count1682--, _umlkckdg += (__81fgg2step1682)) {

								{
									
									if (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) != _d0547bi2)
									{
										
										if (_rcjmgxm4)
										*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) / *(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
										{
											System.Int32 __81fgg2dlsvn1683 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1683 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1683;
											for (__81fgg2count1683 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg - (int)1) - __81fgg2dlsvn1683 + __81fgg2step1683) / __81fgg2step1683)), _b5p6od9s = __81fgg2dlsvn1683; __81fgg2count1683 != 0; __81fgg2count1683--, _b5p6od9s += (__81fgg2step1683)) {

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
						System.Int32 __81fgg2dlsvn1684 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1684 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1684;
						for (__81fgg2count1684 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1684 + __81fgg2step1684) / __81fgg2step1684)), _znpjgsef = __81fgg2dlsvn1684; __81fgg2count1684 != 0; __81fgg2count1684--, _znpjgsef += (__81fgg2step1684)) {

						{
							
							if (_r7cfteg3 != _kxg5drh2)
							{
								
								{
									System.Int32 __81fgg2dlsvn1685 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1685 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1685;
									for (__81fgg2count1685 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1685 + __81fgg2step1685) / __81fgg2step1685)), _b5p6od9s = __81fgg2dlsvn1685; __81fgg2count1685 != 0; __81fgg2count1685--, _b5p6od9s += (__81fgg2step1685)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
Mark70:;
										// continue
									}
																		}								}
							}
							
							{
								System.Int32 __81fgg2dlsvn1686 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1686 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1686;
								for (__81fgg2count1686 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1686 + __81fgg2step1686) / __81fgg2step1686)), _umlkckdg = __81fgg2dlsvn1686; __81fgg2count1686 != 0; __81fgg2count1686--, _umlkckdg += (__81fgg2step1686)) {

								{
									
									if (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) != _d0547bi2)
									{
										
										if (_rcjmgxm4)
										*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) / *(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
										{
											System.Int32 __81fgg2dlsvn1687 = (System.Int32)((_umlkckdg + (int)1));
											const System.Int32 __81fgg2step1687 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1687;
											for (__81fgg2count1687 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1687 + __81fgg2step1687) / __81fgg2step1687)), _b5p6od9s = __81fgg2dlsvn1687; __81fgg2count1687 != 0; __81fgg2count1687--, _b5p6od9s += (__81fgg2step1687)) {

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
						System.Int32 __81fgg2dlsvn1688 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1688 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1688;
						for (__81fgg2count1688 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1688 + __81fgg2step1688) / __81fgg2step1688)), _znpjgsef = __81fgg2dlsvn1688; __81fgg2count1688 != 0; __81fgg2count1688--, _znpjgsef += (__81fgg2step1688)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1689 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1689 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1689;
								for (__81fgg2count1689 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1689 + __81fgg2step1689) / __81fgg2step1689)), _b5p6od9s = __81fgg2dlsvn1689; __81fgg2count1689 != 0; __81fgg2count1689--, _b5p6od9s += (__81fgg2step1689)) {

								{
									
									_1ajfmh55 = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
									if (_moml4lap)
									{
										
										{
											System.Int32 __81fgg2dlsvn1690 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1690 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1690;
											for (__81fgg2count1690 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s - (int)1) - __81fgg2dlsvn1690 + __81fgg2step1690) / __81fgg2step1690)), _umlkckdg = __81fgg2dlsvn1690; __81fgg2count1690 != 0; __81fgg2count1690--, _umlkckdg += (__81fgg2step1690)) {

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
											System.Int32 __81fgg2dlsvn1691 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1691 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1691;
											for (__81fgg2count1691 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s - (int)1) - __81fgg2dlsvn1691 + __81fgg2step1691) / __81fgg2step1691)), _umlkckdg = __81fgg2dlsvn1691; __81fgg2count1691 != 0; __81fgg2count1691--, _umlkckdg += (__81fgg2step1691)) {

											{
												
												_1ajfmh55 = (_1ajfmh55 - (ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ) * *(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
Mark120:;
												// continue
											}
																						}										}
										if (_rcjmgxm4)
										_1ajfmh55 = (_1ajfmh55 / ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ));
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
						System.Int32 __81fgg2dlsvn1692 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1692 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1692;
						for (__81fgg2count1692 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1692 + __81fgg2step1692) / __81fgg2step1692)), _znpjgsef = __81fgg2dlsvn1692; __81fgg2count1692 != 0; __81fgg2count1692--, _znpjgsef += (__81fgg2step1692)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1693 = (System.Int32)(_ev4xhht5);
								System.Int32 __81fgg2step1693 = (System.Int32)((int)-1);
								System.Int32 __81fgg2count1693;
								for (__81fgg2count1693 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1693 + __81fgg2step1693) / __81fgg2step1693)), _b5p6od9s = __81fgg2dlsvn1693; __81fgg2count1693 != 0; __81fgg2count1693--, _b5p6od9s += (__81fgg2step1693)) {

								{
									
									_1ajfmh55 = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
									if (_moml4lap)
									{
										
										{
											System.Int32 __81fgg2dlsvn1694 = (System.Int32)((_b5p6od9s + (int)1));
											const System.Int32 __81fgg2step1694 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1694;
											for (__81fgg2count1694 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1694 + __81fgg2step1694) / __81fgg2step1694)), _umlkckdg = __81fgg2dlsvn1694; __81fgg2count1694 != 0; __81fgg2count1694--, _umlkckdg += (__81fgg2step1694)) {

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
											System.Int32 __81fgg2dlsvn1695 = (System.Int32)((_b5p6od9s + (int)1));
											const System.Int32 __81fgg2step1695 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1695;
											for (__81fgg2count1695 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1695 + __81fgg2step1695) / __81fgg2step1695)), _umlkckdg = __81fgg2dlsvn1695; __81fgg2count1695 != 0; __81fgg2count1695--, _umlkckdg += (__81fgg2step1695)) {

											{
												
												_1ajfmh55 = (_1ajfmh55 - (ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ) * *(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
Mark160:;
												// continue
											}
																						}										}
										if (_rcjmgxm4)
										_1ajfmh55 = (_1ajfmh55 / ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ));
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
						System.Int32 __81fgg2dlsvn1696 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1696 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1696;
						for (__81fgg2count1696 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1696 + __81fgg2step1696) / __81fgg2step1696)), _znpjgsef = __81fgg2dlsvn1696; __81fgg2count1696 != 0; __81fgg2count1696--, _znpjgsef += (__81fgg2step1696)) {

						{
							
							if (_r7cfteg3 != _kxg5drh2)
							{
								
								{
									System.Int32 __81fgg2dlsvn1697 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1697 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1697;
									for (__81fgg2count1697 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1697 + __81fgg2step1697) / __81fgg2step1697)), _b5p6od9s = __81fgg2dlsvn1697; __81fgg2count1697 != 0; __81fgg2count1697--, _b5p6od9s += (__81fgg2step1697)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
Mark190:;
										// continue
									}
																		}								}
							}
							
							{
								System.Int32 __81fgg2dlsvn1698 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1698 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1698;
								for (__81fgg2count1698 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn1698 + __81fgg2step1698) / __81fgg2step1698)), _umlkckdg = __81fgg2dlsvn1698; __81fgg2count1698 != 0; __81fgg2count1698--, _umlkckdg += (__81fgg2step1698)) {

								{
									
									if (*(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										{
											System.Int32 __81fgg2dlsvn1699 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1699 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1699;
											for (__81fgg2count1699 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1699 + __81fgg2step1699) / __81fgg2step1699)), _b5p6od9s = __81fgg2dlsvn1699; __81fgg2count1699 != 0; __81fgg2count1699--, _b5p6od9s += (__81fgg2step1699)) {

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
									System.Int32 __81fgg2dlsvn1700 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1700 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1700;
									for (__81fgg2count1700 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1700 + __81fgg2step1700) / __81fgg2step1700)), _b5p6od9s = __81fgg2dlsvn1700; __81fgg2count1700 != 0; __81fgg2count1700--, _b5p6od9s += (__81fgg2step1700)) {

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
						System.Int32 __81fgg2dlsvn1701 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step1701 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count1701;
						for (__81fgg2count1701 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1701 + __81fgg2step1701) / __81fgg2step1701)), _znpjgsef = __81fgg2dlsvn1701; __81fgg2count1701 != 0; __81fgg2count1701--, _znpjgsef += (__81fgg2step1701)) {

						{
							
							if (_r7cfteg3 != _kxg5drh2)
							{
								
								{
									System.Int32 __81fgg2dlsvn1702 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1702 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1702;
									for (__81fgg2count1702 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1702 + __81fgg2step1702) / __81fgg2step1702)), _b5p6od9s = __81fgg2dlsvn1702; __81fgg2count1702 != 0; __81fgg2count1702--, _b5p6od9s += (__81fgg2step1702)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_r7cfteg3 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
Mark240:;
										// continue
									}
																		}								}
							}
							
							{
								System.Int32 __81fgg2dlsvn1703 = (System.Int32)((_znpjgsef + (int)1));
								const System.Int32 __81fgg2step1703 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1703;
								for (__81fgg2count1703 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1703 + __81fgg2step1703) / __81fgg2step1703)), _umlkckdg = __81fgg2dlsvn1703; __81fgg2count1703 != 0; __81fgg2count1703--, _umlkckdg += (__81fgg2step1703)) {

								{
									
									if (*(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										{
											System.Int32 __81fgg2dlsvn1704 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1704 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1704;
											for (__81fgg2count1704 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1704 + __81fgg2step1704) / __81fgg2step1704)), _b5p6od9s = __81fgg2dlsvn1704; __81fgg2count1704 != 0; __81fgg2count1704--, _b5p6od9s += (__81fgg2step1704)) {

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
									System.Int32 __81fgg2dlsvn1705 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1705 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1705;
									for (__81fgg2count1705 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1705 + __81fgg2step1705) / __81fgg2step1705)), _b5p6od9s = __81fgg2dlsvn1705; __81fgg2count1705 != 0; __81fgg2count1705--, _b5p6od9s += (__81fgg2step1705)) {

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
						System.Int32 __81fgg2dlsvn1706 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step1706 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count1706;
						for (__81fgg2count1706 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1706 + __81fgg2step1706) / __81fgg2step1706)), _umlkckdg = __81fgg2dlsvn1706; __81fgg2count1706 != 0; __81fgg2count1706--, _umlkckdg += (__81fgg2step1706)) {

						{
							
							if (_rcjmgxm4)
							{
								
								if (_moml4lap)
								{
									
									_1ajfmh55 = (_kxg5drh2 / *(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
								}
								else
								{
									
									_1ajfmh55 = (_kxg5drh2 / ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) ));
								}
								
								{
									System.Int32 __81fgg2dlsvn1707 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1707 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1707;
									for (__81fgg2count1707 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1707 + __81fgg2step1707) / __81fgg2step1707)), _b5p6od9s = __81fgg2dlsvn1707; __81fgg2count1707 != 0; __81fgg2count1707--, _b5p6od9s += (__81fgg2step1707)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)) = (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)));
Mark290:;
										// continue
									}
																		}								}
							}
							
							{
								System.Int32 __81fgg2dlsvn1708 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1708 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1708;
								for (__81fgg2count1708 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg - (int)1) - __81fgg2dlsvn1708 + __81fgg2step1708) / __81fgg2step1708)), _znpjgsef = __81fgg2dlsvn1708; __81fgg2count1708 != 0; __81fgg2count1708--, _znpjgsef += (__81fgg2step1708)) {

								{
									
									if (*(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										if (_moml4lap)
										{
											
											_1ajfmh55 = *(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c));
										}
										else
										{
											
											_1ajfmh55 = ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) );
										}
										
										{
											System.Int32 __81fgg2dlsvn1709 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1709 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1709;
											for (__81fgg2count1709 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1709 + __81fgg2step1709) / __81fgg2step1709)), _b5p6od9s = __81fgg2dlsvn1709; __81fgg2count1709 != 0; __81fgg2count1709--, _b5p6od9s += (__81fgg2step1709)) {

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
									System.Int32 __81fgg2dlsvn1710 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1710 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1710;
									for (__81fgg2count1710 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1710 + __81fgg2step1710) / __81fgg2step1710)), _b5p6od9s = __81fgg2dlsvn1710; __81fgg2count1710 != 0; __81fgg2count1710--, _b5p6od9s += (__81fgg2step1710)) {

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
						System.Int32 __81fgg2dlsvn1711 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1711 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1711;
						for (__81fgg2count1711 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1711 + __81fgg2step1711) / __81fgg2step1711)), _umlkckdg = __81fgg2dlsvn1711; __81fgg2count1711 != 0; __81fgg2count1711--, _umlkckdg += (__81fgg2step1711)) {

						{
							
							if (_rcjmgxm4)
							{
								
								if (_moml4lap)
								{
									
									_1ajfmh55 = (_kxg5drh2 / *(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
								}
								else
								{
									
									_1ajfmh55 = (_kxg5drh2 / ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) ));
								}
								
								{
									System.Int32 __81fgg2dlsvn1712 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1712 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1712;
									for (__81fgg2count1712 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1712 + __81fgg2step1712) / __81fgg2step1712)), _b5p6od9s = __81fgg2dlsvn1712; __81fgg2count1712 != 0; __81fgg2count1712--, _b5p6od9s += (__81fgg2step1712)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)) = (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)));
Mark340:;
										// continue
									}
																		}								}
							}
							
							{
								System.Int32 __81fgg2dlsvn1713 = (System.Int32)((_umlkckdg + (int)1));
								const System.Int32 __81fgg2step1713 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1713;
								for (__81fgg2count1713 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1713 + __81fgg2step1713) / __81fgg2step1713)), _znpjgsef = __81fgg2dlsvn1713; __81fgg2count1713 != 0; __81fgg2count1713--, _znpjgsef += (__81fgg2step1713)) {

								{
									
									if (*(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										if (_moml4lap)
										{
											
											_1ajfmh55 = *(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c));
										}
										else
										{
											
											_1ajfmh55 = ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) );
										}
										
										{
											System.Int32 __81fgg2dlsvn1714 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1714 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1714;
											for (__81fgg2count1714 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1714 + __81fgg2step1714) / __81fgg2step1714)), _b5p6od9s = __81fgg2dlsvn1714; __81fgg2count1714 != 0; __81fgg2count1714--, _b5p6od9s += (__81fgg2step1714)) {

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
									System.Int32 __81fgg2dlsvn1715 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1715 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1715;
									for (__81fgg2count1715 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1715 + __81fgg2step1715) / __81fgg2step1715)), _b5p6od9s = __81fgg2dlsvn1715; __81fgg2count1715 != 0; __81fgg2count1715--, _b5p6od9s += (__81fgg2step1715)) {

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
		//*     End of ZTRSM . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
