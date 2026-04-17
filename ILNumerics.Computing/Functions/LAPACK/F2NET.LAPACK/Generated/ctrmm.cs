
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
//*> \brief \b CTRMM 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CTRMM(SIDE,UPLO,TRANSA,DIAG,M,N,ALPHA,A,LDA,B,LDB) 
//* 
//*       .. Scalar Arguments .. 
//*       COMPLEX ALPHA 
//*       INTEGER LDA,LDB,M,N 
//*       CHARACTER DIAG,SIDE,TRANSA,UPLO 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX A(LDA,*),B(LDB,*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CTRMM  performs one of the matrix-matrix operations 
//*> 
//*>    B := alpha*op( A )*B,   or   B := alpha*B*op( A ) 
//*> 
//*> where  alpha  is a scalar,  B  is an m by n matrix,  A  is a unit, or 
//*> non-unit,  upper or lower triangular matrix  and  op( A )  is one  of 
//*> 
//*>    op( A ) = A   or   op( A ) = A**T   or   op( A ) = A**H. 
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
//*>          ALPHA is COMPLEX 
//*>           On entry,  ALPHA specifies the scalar  alpha. When  alpha is 
//*>           zero then  A is not referenced and  B need not be set before 
//*>           entry. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is COMPLEX array, dimension ( LDA, k ), where k is m 
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
//*>          B is COMPLEX array, dimension ( LDB, N ). 
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

	 
	public static void _smeynpn3(FString _m2cn2gjg, FString _9wyre9zc, FString _742vrzth, FString _2scffxp3, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref fcomplex _r7cfteg3, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, fcomplex* _p9n405a5, ref Int32 _ly9opahg)
	{
#region variable declarations
fcomplex _1ajfmh55 =  default;
Int32 _b5p6od9s =  default;
Int32 _gro5yvfo =  default;
Int32 _znpjgsef =  default;
Int32 _umlkckdg =  default;
Int32 _o9a6qdux =  default;
Boolean _wln8whqb =  default;
Boolean _moml4lap =  default;
Boolean _rcjmgxm4 =  default;
Boolean _l08igmvf =  default;
fcomplex _kxg5drh2 =   new fcomplex(1f,0f);
fcomplex _d0547bi2 =   new fcomplex(0f,0f);
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
			
			_ut9qalzx("CTRMM " ,ref _gro5yvfo );
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
				System.Int32 __81fgg2dlsvn974 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step974 = (System.Int32)((int)1);
				System.Int32 __81fgg2count974;
				for (__81fgg2count974 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn974 + __81fgg2step974) / __81fgg2step974)), _znpjgsef = __81fgg2dlsvn974; __81fgg2count974 != 0; __81fgg2count974--, _znpjgsef += (__81fgg2step974)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn975 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step975 = (System.Int32)((int)1);
						System.Int32 __81fgg2count975;
						for (__81fgg2count975 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn975 + __81fgg2step975) / __81fgg2step975)), _b5p6od9s = __81fgg2dlsvn975; __81fgg2count975 != 0; __81fgg2count975--, _b5p6od9s += (__81fgg2step975)) {

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
						System.Int32 __81fgg2dlsvn976 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step976 = (System.Int32)((int)1);
						System.Int32 __81fgg2count976;
						for (__81fgg2count976 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn976 + __81fgg2step976) / __81fgg2step976)), _znpjgsef = __81fgg2dlsvn976; __81fgg2count976 != 0; __81fgg2count976--, _znpjgsef += (__81fgg2step976)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn977 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step977 = (System.Int32)((int)1);
								System.Int32 __81fgg2count977;
								for (__81fgg2count977 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn977 + __81fgg2step977) / __81fgg2step977)), _umlkckdg = __81fgg2dlsvn977; __81fgg2count977 != 0; __81fgg2count977--, _umlkckdg += (__81fgg2step977)) {

								{
									
									if (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) != _d0547bi2)
									{
										
										_1ajfmh55 = (_r7cfteg3 * *(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
										{
											System.Int32 __81fgg2dlsvn978 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step978 = (System.Int32)((int)1);
											System.Int32 __81fgg2count978;
											for (__81fgg2count978 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg - (int)1) - __81fgg2dlsvn978 + __81fgg2step978) / __81fgg2step978)), _b5p6od9s = __81fgg2dlsvn978; __81fgg2count978 != 0; __81fgg2count978--, _b5p6od9s += (__81fgg2step978)) {

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
						System.Int32 __81fgg2dlsvn979 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step979 = (System.Int32)((int)1);
						System.Int32 __81fgg2count979;
						for (__81fgg2count979 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn979 + __81fgg2step979) / __81fgg2step979)), _znpjgsef = __81fgg2dlsvn979; __81fgg2count979 != 0; __81fgg2count979--, _znpjgsef += (__81fgg2step979)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn980 = (System.Int32)(_ev4xhht5);
								System.Int32 __81fgg2step980 = (System.Int32)((int)-1);
								System.Int32 __81fgg2count980;
								for (__81fgg2count980 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn980 + __81fgg2step980) / __81fgg2step980)), _umlkckdg = __81fgg2dlsvn980; __81fgg2count980 != 0; __81fgg2count980--, _umlkckdg += (__81fgg2step980)) {

								{
									
									if (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) != _d0547bi2)
									{
										
										_1ajfmh55 = (_r7cfteg3 * *(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
										*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = _1ajfmh55;
										if (_rcjmgxm4)
										*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) * *(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
										{
											System.Int32 __81fgg2dlsvn981 = (System.Int32)((_umlkckdg + (int)1));
											const System.Int32 __81fgg2step981 = (System.Int32)((int)1);
											System.Int32 __81fgg2count981;
											for (__81fgg2count981 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn981 + __81fgg2step981) / __81fgg2step981)), _b5p6od9s = __81fgg2dlsvn981; __81fgg2count981 != 0; __81fgg2count981--, _b5p6od9s += (__81fgg2step981)) {

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
				//*           Form  B := alpha*A**T*B   or   B := alpha*A**H*B. 
				//* 
				
				if (_l08igmvf)
				{
					
					{
						System.Int32 __81fgg2dlsvn982 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step982 = (System.Int32)((int)1);
						System.Int32 __81fgg2count982;
						for (__81fgg2count982 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn982 + __81fgg2step982) / __81fgg2step982)), _znpjgsef = __81fgg2dlsvn982; __81fgg2count982 != 0; __81fgg2count982--, _znpjgsef += (__81fgg2step982)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn983 = (System.Int32)(_ev4xhht5);
								System.Int32 __81fgg2step983 = (System.Int32)((int)-1);
								System.Int32 __81fgg2count983;
								for (__81fgg2count983 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn983 + __81fgg2step983) / __81fgg2step983)), _b5p6od9s = __81fgg2dlsvn983; __81fgg2count983 != 0; __81fgg2count983--, _b5p6od9s += (__81fgg2step983)) {

								{
									
									_1ajfmh55 = *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg));
									if (_moml4lap)
									{
										
										if (_rcjmgxm4)
										_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)));
										{
											System.Int32 __81fgg2dlsvn984 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step984 = (System.Int32)((int)1);
											System.Int32 __81fgg2count984;
											for (__81fgg2count984 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s - (int)1) - __81fgg2dlsvn984 + __81fgg2step984) / __81fgg2step984)), _umlkckdg = __81fgg2dlsvn984; __81fgg2count984 != 0; __81fgg2count984--, _umlkckdg += (__81fgg2step984)) {

											{
												
												_1ajfmh55 = (_1ajfmh55 + (*(_vxfgpup9+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) * *(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
Mark90:;
												// continue
											}
																						}										}
									}
									else
									{
										
										if (_rcjmgxm4)
										_1ajfmh55 = (_1ajfmh55 * ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ));
										{
											System.Int32 __81fgg2dlsvn985 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step985 = (System.Int32)((int)1);
											System.Int32 __81fgg2count985;
											for (__81fgg2count985 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s - (int)1) - __81fgg2dlsvn985 + __81fgg2step985) / __81fgg2step985)), _umlkckdg = __81fgg2dlsvn985; __81fgg2count985 != 0; __81fgg2count985--, _umlkckdg += (__81fgg2step985)) {

											{
												
												_1ajfmh55 = (_1ajfmh55 + (ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ) * *(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
Mark100:;
												// continue
											}
																						}										}
									}
									
									*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_r7cfteg3 * _1ajfmh55);
Mark110:;
									// continue
								}
																}							}
Mark120:;
							// continue
						}
												}					}
				}
				else
				{
					
					{
						System.Int32 __81fgg2dlsvn986 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step986 = (System.Int32)((int)1);
						System.Int32 __81fgg2count986;
						for (__81fgg2count986 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn986 + __81fgg2step986) / __81fgg2step986)), _znpjgsef = __81fgg2dlsvn986; __81fgg2count986 != 0; __81fgg2count986--, _znpjgsef += (__81fgg2step986)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn987 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step987 = (System.Int32)((int)1);
								System.Int32 __81fgg2count987;
								for (__81fgg2count987 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn987 + __81fgg2step987) / __81fgg2step987)), _b5p6od9s = __81fgg2dlsvn987; __81fgg2count987 != 0; __81fgg2count987--, _b5p6od9s += (__81fgg2step987)) {

								{
									
									_1ajfmh55 = *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg));
									if (_moml4lap)
									{
										
										if (_rcjmgxm4)
										_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)));
										{
											System.Int32 __81fgg2dlsvn988 = (System.Int32)((_b5p6od9s + (int)1));
											const System.Int32 __81fgg2step988 = (System.Int32)((int)1);
											System.Int32 __81fgg2count988;
											for (__81fgg2count988 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn988 + __81fgg2step988) / __81fgg2step988)), _umlkckdg = __81fgg2dlsvn988; __81fgg2count988 != 0; __81fgg2count988--, _umlkckdg += (__81fgg2step988)) {

											{
												
												_1ajfmh55 = (_1ajfmh55 + (*(_vxfgpup9+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) * *(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
Mark130:;
												// continue
											}
																						}										}
									}
									else
									{
										
										if (_rcjmgxm4)
										_1ajfmh55 = (_1ajfmh55 * ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ));
										{
											System.Int32 __81fgg2dlsvn989 = (System.Int32)((_b5p6od9s + (int)1));
											const System.Int32 __81fgg2step989 = (System.Int32)((int)1);
											System.Int32 __81fgg2count989;
											for (__81fgg2count989 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn989 + __81fgg2step989) / __81fgg2step989)), _umlkckdg = __81fgg2dlsvn989; __81fgg2count989 != 0; __81fgg2count989--, _umlkckdg += (__81fgg2step989)) {

											{
												
												_1ajfmh55 = (_1ajfmh55 + (ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ) * *(_p9n405a5+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg))));
Mark140:;
												// continue
											}
																						}										}
									}
									
									*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_r7cfteg3 * _1ajfmh55);
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
				//*           Form  B := alpha*B*A. 
				//* 
				
				if (_l08igmvf)
				{
					
					{
						System.Int32 __81fgg2dlsvn990 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step990 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count990;
						for (__81fgg2count990 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn990 + __81fgg2step990) / __81fgg2step990)), _znpjgsef = __81fgg2dlsvn990; __81fgg2count990 != 0; __81fgg2count990--, _znpjgsef += (__81fgg2step990)) {

						{
							
							_1ajfmh55 = _r7cfteg3;
							if (_rcjmgxm4)
							_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							{
								System.Int32 __81fgg2dlsvn991 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step991 = (System.Int32)((int)1);
								System.Int32 __81fgg2count991;
								for (__81fgg2count991 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn991 + __81fgg2step991) / __81fgg2step991)), _b5p6od9s = __81fgg2dlsvn991; __81fgg2count991 != 0; __81fgg2count991--, _b5p6od9s += (__81fgg2step991)) {

								{
									
									*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
Mark170:;
									// continue
								}
																}							}
							{
								System.Int32 __81fgg2dlsvn992 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step992 = (System.Int32)((int)1);
								System.Int32 __81fgg2count992;
								for (__81fgg2count992 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn992 + __81fgg2step992) / __81fgg2step992)), _umlkckdg = __81fgg2dlsvn992; __81fgg2count992 != 0; __81fgg2count992--, _umlkckdg += (__81fgg2step992)) {

								{
									
									if (*(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										_1ajfmh55 = (_r7cfteg3 * *(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
										{
											System.Int32 __81fgg2dlsvn993 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step993 = (System.Int32)((int)1);
											System.Int32 __81fgg2count993;
											for (__81fgg2count993 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn993 + __81fgg2step993) / __81fgg2step993)), _b5p6od9s = __81fgg2dlsvn993; __81fgg2count993 != 0; __81fgg2count993--, _b5p6od9s += (__81fgg2step993)) {

											{
												
												*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) + (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg))));
Mark180:;
												// continue
											}
																						}										}
									}
									
Mark190:;
									// continue
								}
																}							}
Mark200:;
							// continue
						}
												}					}
				}
				else
				{
					
					{
						System.Int32 __81fgg2dlsvn994 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step994 = (System.Int32)((int)1);
						System.Int32 __81fgg2count994;
						for (__81fgg2count994 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn994 + __81fgg2step994) / __81fgg2step994)), _znpjgsef = __81fgg2dlsvn994; __81fgg2count994 != 0; __81fgg2count994--, _znpjgsef += (__81fgg2step994)) {

						{
							
							_1ajfmh55 = _r7cfteg3;
							if (_rcjmgxm4)
							_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
							{
								System.Int32 __81fgg2dlsvn995 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step995 = (System.Int32)((int)1);
								System.Int32 __81fgg2count995;
								for (__81fgg2count995 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn995 + __81fgg2step995) / __81fgg2step995)), _b5p6od9s = __81fgg2dlsvn995; __81fgg2count995 != 0; __81fgg2count995--, _b5p6od9s += (__81fgg2step995)) {

								{
									
									*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)));
Mark210:;
									// continue
								}
																}							}
							{
								System.Int32 __81fgg2dlsvn996 = (System.Int32)((_znpjgsef + (int)1));
								const System.Int32 __81fgg2step996 = (System.Int32)((int)1);
								System.Int32 __81fgg2count996;
								for (__81fgg2count996 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn996 + __81fgg2step996) / __81fgg2step996)), _umlkckdg = __81fgg2dlsvn996; __81fgg2count996 != 0; __81fgg2count996--, _umlkckdg += (__81fgg2step996)) {

								{
									
									if (*(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										_1ajfmh55 = (_r7cfteg3 * *(_vxfgpup9+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
										{
											System.Int32 __81fgg2dlsvn997 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step997 = (System.Int32)((int)1);
											System.Int32 __81fgg2count997;
											for (__81fgg2count997 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn997 + __81fgg2step997) / __81fgg2step997)), _b5p6od9s = __81fgg2dlsvn997; __81fgg2count997 != 0; __81fgg2count997--, _b5p6od9s += (__81fgg2step997)) {

											{
												
												*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) + (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg))));
Mark220:;
												// continue
											}
																						}										}
									}
									
Mark230:;
									// continue
								}
																}							}
Mark240:;
							// continue
						}
												}					}
				}
				
			}
			else
			{
				//* 
				//*           Form  B := alpha*B*A**T   or   B := alpha*B*A**H. 
				//* 
				
				if (_l08igmvf)
				{
					
					{
						System.Int32 __81fgg2dlsvn998 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step998 = (System.Int32)((int)1);
						System.Int32 __81fgg2count998;
						for (__81fgg2count998 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn998 + __81fgg2step998) / __81fgg2step998)), _umlkckdg = __81fgg2dlsvn998; __81fgg2count998 != 0; __81fgg2count998--, _umlkckdg += (__81fgg2step998)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn999 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step999 = (System.Int32)((int)1);
								System.Int32 __81fgg2count999;
								for (__81fgg2count999 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg - (int)1) - __81fgg2dlsvn999 + __81fgg2step999) / __81fgg2step999)), _znpjgsef = __81fgg2dlsvn999; __81fgg2count999 != 0; __81fgg2count999--, _znpjgsef += (__81fgg2step999)) {

								{
									
									if (*(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										if (_moml4lap)
										{
											
											_1ajfmh55 = (_r7cfteg3 * *(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
										}
										else
										{
											
											_1ajfmh55 = (_r7cfteg3 * ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) ));
										}
										
										{
											System.Int32 __81fgg2dlsvn1000 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1000 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1000;
											for (__81fgg2count1000 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1000 + __81fgg2step1000) / __81fgg2step1000)), _b5p6od9s = __81fgg2dlsvn1000; __81fgg2count1000 != 0; __81fgg2count1000--, _b5p6od9s += (__81fgg2step1000)) {

											{
												
												*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) + (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg))));
Mark250:;
												// continue
											}
																						}										}
									}
									
Mark260:;
									// continue
								}
																}							}
							_1ajfmh55 = _r7cfteg3;
							if (_rcjmgxm4)
							{
								
								if (_moml4lap)
								{
									
									_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
								}
								else
								{
									
									_1ajfmh55 = (_1ajfmh55 * ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) ));
								}
								
							}
							
							if (_1ajfmh55 != _kxg5drh2)
							{
								
								{
									System.Int32 __81fgg2dlsvn1001 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1001 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1001;
									for (__81fgg2count1001 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1001 + __81fgg2step1001) / __81fgg2step1001)), _b5p6od9s = __81fgg2dlsvn1001; __81fgg2count1001 != 0; __81fgg2count1001--, _b5p6od9s += (__81fgg2step1001)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)) = (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)));
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
				else
				{
					
					{
						System.Int32 __81fgg2dlsvn1002 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step1002 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count1002;
						for (__81fgg2count1002 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1002 + __81fgg2step1002) / __81fgg2step1002)), _umlkckdg = __81fgg2dlsvn1002; __81fgg2count1002 != 0; __81fgg2count1002--, _umlkckdg += (__81fgg2step1002)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1003 = (System.Int32)((_umlkckdg + (int)1));
								const System.Int32 __81fgg2step1003 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1003;
								for (__81fgg2count1003 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1003 + __81fgg2step1003) / __81fgg2step1003)), _znpjgsef = __81fgg2dlsvn1003; __81fgg2count1003 != 0; __81fgg2count1003--, _znpjgsef += (__81fgg2step1003)) {

								{
									
									if (*(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
									{
										
										if (_moml4lap)
										{
											
											_1ajfmh55 = (_r7cfteg3 * *(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
										}
										else
										{
											
											_1ajfmh55 = (_r7cfteg3 * ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) ));
										}
										
										{
											System.Int32 __81fgg2dlsvn1004 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step1004 = (System.Int32)((int)1);
											System.Int32 __81fgg2count1004;
											for (__81fgg2count1004 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1004 + __81fgg2step1004) / __81fgg2step1004)), _b5p6od9s = __81fgg2dlsvn1004; __81fgg2count1004 != 0; __81fgg2count1004--, _b5p6od9s += (__81fgg2step1004)) {

											{
												
												*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = (*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) + (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg))));
Mark290:;
												// continue
											}
																						}										}
									}
									
Mark300:;
									// continue
								}
																}							}
							_1ajfmh55 = _r7cfteg3;
							if (_rcjmgxm4)
							{
								
								if (_moml4lap)
								{
									
									_1ajfmh55 = (_1ajfmh55 * *(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
								}
								else
								{
									
									_1ajfmh55 = (_1ajfmh55 * ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) ));
								}
								
							}
							
							if (_1ajfmh55 != _kxg5drh2)
							{
								
								{
									System.Int32 __81fgg2dlsvn1005 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1005 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1005;
									for (__81fgg2count1005 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1005 + __81fgg2step1005) / __81fgg2step1005)), _b5p6od9s = __81fgg2dlsvn1005; __81fgg2count1005 != 0; __81fgg2count1005--, _b5p6od9s += (__81fgg2step1005)) {

									{
										
										*(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)) = (_1ajfmh55 * *(_p9n405a5+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)));
Mark310:;
										// continue
									}
																		}								}
							}
							
Mark320:;
							// continue
						}
												}					}
				}
				
			}
			
		}
		//* 
		
		return;//* 
		//*     End of CTRMM . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
