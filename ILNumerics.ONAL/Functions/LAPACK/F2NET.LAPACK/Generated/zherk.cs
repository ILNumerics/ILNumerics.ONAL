
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
//*> \brief \b ZHERK 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZHERK(UPLO,TRANS,N,K,ALPHA,A,LDA,BETA,C,LDC) 
//* 
//*       .. Scalar Arguments .. 
//*       DOUBLE PRECISION ALPHA,BETA 
//*       INTEGER K,LDA,LDC,N 
//*       CHARACTER TRANS,UPLO 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX*16 A(LDA,*),C(LDC,*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZHERK  performs one of the hermitian rank k operations 
//*> 
//*>    C := alpha*A*A**H + beta*C, 
//*> 
//*> or 
//*> 
//*>    C := alpha*A**H*A + beta*C, 
//*> 
//*> where  alpha and beta  are  real scalars,  C is an  n by n  hermitian 
//*> matrix and  A  is an  n by k  matrix in the  first case and a  k by n 
//*> matrix in the second case. 
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
//*>              TRANS = 'N' or 'n'   C := alpha*A*A**H + beta*C. 
//*> 
//*>              TRANS = 'C' or 'c'   C := alpha*A**H*A + beta*C. 
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
//*>           of  columns   of  the   matrix   A,   and  on   entry   with 
//*>           TRANS = 'C' or 'c',  K  specifies  the number of rows of the 
//*>           matrix A.  K must be at least zero. 
//*> \endverbatim 
//*> 
//*> \param[in] ALPHA 
//*> \verbatim 
//*>          ALPHA is DOUBLE PRECISION . 
//*>           On entry, ALPHA specifies the scalar alpha. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is COMPLEX*16 array, dimension ( LDA, ka ), where ka is 
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
//*> \param[in] BETA 
//*> \verbatim 
//*>          BETA is DOUBLE PRECISION. 
//*>           On entry, BETA specifies the scalar beta. 
//*> \endverbatim 
//*> 
//*> \param[in,out] C 
//*> \verbatim 
//*>          C is COMPLEX*16 array, dimension ( LDC, N ) 
//*>           Before entry  with  UPLO = 'U' or 'u',  the leading  n by n 
//*>           upper triangular part of the array C must contain the upper 
//*>           triangular part  of the  hermitian matrix  and the strictly 
//*>           lower triangular part of C is not referenced.  On exit, the 
//*>           upper triangular part of the array  C is overwritten by the 
//*>           upper triangular part of the updated matrix. 
//*>           Before entry  with  UPLO = 'L' or 'l',  the leading  n by n 
//*>           lower triangular part of the array C must contain the lower 
//*>           triangular part  of the  hermitian matrix  and the strictly 
//*>           upper triangular part of C is not referenced.  On exit, the 
//*>           lower triangular part of the array  C is overwritten by the 
//*>           lower triangular part of the updated matrix. 
//*>           Note that the imaginary parts of the diagonal elements need 
//*>           not be set,  they are assumed to be zero,  and on exit they 
//*>           are set to zero. 
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
//*> 
//*>  -- Modified 8-Nov-93 to set C(J,J) to DBLE( C(J,J) ) when BETA = 1. 
//*>     Ed Anderson, Cray Research Inc. 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _qd483w5z(FString _9wyre9zc, FString _scuo79v4, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, ref Double _r7cfteg3, complex* _vxfgpup9, ref Int32 _ocv8fk5c, ref Double _bafcbx97, complex* _3crf0qn3, ref Int32 _1s3eymp4)
	{
#region variable declarations
complex _1ajfmh55 =  default;
Double _b56sf68i =  default;
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
		if ((!(_w8y2rzgy(_scuo79v4 ,"N" ))) & (!(_w8y2rzgy(_scuo79v4 ,"C" ))))
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
		if (_1s3eymp4 < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)10;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("ZHERK " ,ref _gro5yvfo );
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
						System.Int32 __81fgg2dlsvn1652 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1652 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1652;
						for (__81fgg2count1652 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1652 + __81fgg2step1652) / __81fgg2step1652)), _znpjgsef = __81fgg2dlsvn1652; __81fgg2count1652 != 0; __81fgg2count1652--, _znpjgsef += (__81fgg2step1652)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1653 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1653 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1653;
								for (__81fgg2count1653 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn1653 + __81fgg2step1653) / __81fgg2step1653)), _b5p6od9s = __81fgg2dlsvn1653; __81fgg2count1653 != 0; __81fgg2count1653--, _b5p6od9s += (__81fgg2step1653)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = DCMPLX(_d0547bi2);
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
						System.Int32 __81fgg2dlsvn1654 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1654 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1654;
						for (__81fgg2count1654 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1654 + __81fgg2step1654) / __81fgg2step1654)), _znpjgsef = __81fgg2dlsvn1654; __81fgg2count1654 != 0; __81fgg2count1654--, _znpjgsef += (__81fgg2step1654)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1655 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1655 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1655;
								for (__81fgg2count1655 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn1655 + __81fgg2step1655) / __81fgg2step1655)), _b5p6od9s = __81fgg2dlsvn1655; __81fgg2count1655 != 0; __81fgg2count1655--, _b5p6od9s += (__81fgg2step1655)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark30:;
									// continue
								}
																}							}
							*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = DCMPLX((_bafcbx97 * ILNumerics.F2NET.Intrinsics.DBLE(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) )));
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
						System.Int32 __81fgg2dlsvn1656 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1656 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1656;
						for (__81fgg2count1656 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1656 + __81fgg2step1656) / __81fgg2step1656)), _znpjgsef = __81fgg2dlsvn1656; __81fgg2count1656 != 0; __81fgg2count1656--, _znpjgsef += (__81fgg2step1656)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1657 = (System.Int32)(_znpjgsef);
								const System.Int32 __81fgg2step1657 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1657;
								for (__81fgg2count1657 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1657 + __81fgg2step1657) / __81fgg2step1657)), _b5p6od9s = __81fgg2dlsvn1657; __81fgg2count1657 != 0; __81fgg2count1657--, _b5p6od9s += (__81fgg2step1657)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = DCMPLX(_d0547bi2);
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
						System.Int32 __81fgg2dlsvn1658 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1658 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1658;
						for (__81fgg2count1658 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1658 + __81fgg2step1658) / __81fgg2step1658)), _znpjgsef = __81fgg2dlsvn1658; __81fgg2count1658 != 0; __81fgg2count1658--, _znpjgsef += (__81fgg2step1658)) {

						{
							
							*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = DCMPLX((_bafcbx97 * ILNumerics.F2NET.Intrinsics.DBLE(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) )));
							{
								System.Int32 __81fgg2dlsvn1659 = (System.Int32)((_znpjgsef + (int)1));
								const System.Int32 __81fgg2step1659 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1659;
								for (__81fgg2count1659 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1659 + __81fgg2step1659) / __81fgg2step1659)), _b5p6od9s = __81fgg2dlsvn1659; __81fgg2count1659 != 0; __81fgg2count1659--, _b5p6od9s += (__81fgg2step1659)) {

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
			//*        Form  C := alpha*A*A**H + beta*C. 
			//* 
			
			if (_l08igmvf)
			{
				
				{
					System.Int32 __81fgg2dlsvn1660 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1660 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1660;
					for (__81fgg2count1660 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1660 + __81fgg2step1660) / __81fgg2step1660)), _znpjgsef = __81fgg2dlsvn1660; __81fgg2count1660 != 0; __81fgg2count1660--, _znpjgsef += (__81fgg2step1660)) {

					{
						
						if (_bafcbx97 == _d0547bi2)
						{
							
							{
								System.Int32 __81fgg2dlsvn1661 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1661 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1661;
								for (__81fgg2count1661 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn1661 + __81fgg2step1661) / __81fgg2step1661)), _b5p6od9s = __81fgg2dlsvn1661; __81fgg2count1661 != 0; __81fgg2count1661--, _b5p6od9s += (__81fgg2step1661)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = DCMPLX(_d0547bi2);
Mark90:;
									// continue
								}
																}							}
						}
						else
						if (_bafcbx97 != _kxg5drh2)
						{
							
							{
								System.Int32 __81fgg2dlsvn1662 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1662 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1662;
								for (__81fgg2count1662 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn1662 + __81fgg2step1662) / __81fgg2step1662)), _b5p6od9s = __81fgg2dlsvn1662; __81fgg2count1662 != 0; __81fgg2count1662--, _b5p6od9s += (__81fgg2step1662)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark100:;
									// continue
								}
																}							}
							*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = DCMPLX((_bafcbx97 * ILNumerics.F2NET.Intrinsics.DBLE(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) )));
						}
						else
						{
							
							*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = DCMPLX(ILNumerics.F2NET.Intrinsics.DBLE(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) ));
						}
						
						{
							System.Int32 __81fgg2dlsvn1663 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step1663 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1663;
							for (__81fgg2count1663 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1663 + __81fgg2step1663) / __81fgg2step1663)), _68ec3gbh = __81fgg2dlsvn1663; __81fgg2count1663 != 0; __81fgg2count1663--, _68ec3gbh += (__81fgg2step1663)) {

							{
								
								if (*(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) != ILNumerics.F2NET.Intrinsics.DCMPLX(_d0547bi2 ))
								{
									
									_1ajfmh55 = (_r7cfteg3 * ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) ));
									{
										System.Int32 __81fgg2dlsvn1664 = (System.Int32)((int)1);
										const System.Int32 __81fgg2step1664 = (System.Int32)((int)1);
										System.Int32 __81fgg2count1664;
										for (__81fgg2count1664 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn1664 + __81fgg2step1664) / __81fgg2step1664)), _b5p6od9s = __81fgg2dlsvn1664; __81fgg2count1664 != 0; __81fgg2count1664--, _b5p6od9s += (__81fgg2step1664)) {

										{
											
											*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) + (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c))));
Mark110:;
											// continue
										}
																				}									}
									*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = DCMPLX((ILNumerics.F2NET.Intrinsics.DBLE(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) ) + ILNumerics.F2NET.Intrinsics.DBLE(_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) )));
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
					System.Int32 __81fgg2dlsvn1665 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1665 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1665;
					for (__81fgg2count1665 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1665 + __81fgg2step1665) / __81fgg2step1665)), _znpjgsef = __81fgg2dlsvn1665; __81fgg2count1665 != 0; __81fgg2count1665--, _znpjgsef += (__81fgg2step1665)) {

					{
						
						if (_bafcbx97 == _d0547bi2)
						{
							
							{
								System.Int32 __81fgg2dlsvn1666 = (System.Int32)(_znpjgsef);
								const System.Int32 __81fgg2step1666 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1666;
								for (__81fgg2count1666 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1666 + __81fgg2step1666) / __81fgg2step1666)), _b5p6od9s = __81fgg2dlsvn1666; __81fgg2count1666 != 0; __81fgg2count1666--, _b5p6od9s += (__81fgg2step1666)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = DCMPLX(_d0547bi2);
Mark140:;
									// continue
								}
																}							}
						}
						else
						if (_bafcbx97 != _kxg5drh2)
						{
							
							*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = DCMPLX((_bafcbx97 * ILNumerics.F2NET.Intrinsics.DBLE(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) )));
							{
								System.Int32 __81fgg2dlsvn1667 = (System.Int32)((_znpjgsef + (int)1));
								const System.Int32 __81fgg2step1667 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1667;
								for (__81fgg2count1667 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1667 + __81fgg2step1667) / __81fgg2step1667)), _b5p6od9s = __81fgg2dlsvn1667; __81fgg2count1667 != 0; __81fgg2count1667--, _b5p6od9s += (__81fgg2step1667)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_bafcbx97 * *(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark150:;
									// continue
								}
																}							}
						}
						else
						{
							
							*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = DCMPLX(ILNumerics.F2NET.Intrinsics.DBLE(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) ));
						}
						
						{
							System.Int32 __81fgg2dlsvn1668 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step1668 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1668;
							for (__81fgg2count1668 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1668 + __81fgg2step1668) / __81fgg2step1668)), _68ec3gbh = __81fgg2dlsvn1668; __81fgg2count1668 != 0; __81fgg2count1668--, _68ec3gbh += (__81fgg2step1668)) {

							{
								
								if (*(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) != ILNumerics.F2NET.Intrinsics.DCMPLX(_d0547bi2 ))
								{
									
									_1ajfmh55 = (_r7cfteg3 * ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) ));
									*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = DCMPLX((ILNumerics.F2NET.Intrinsics.DBLE(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) ) + ILNumerics.F2NET.Intrinsics.DBLE(_1ajfmh55 * *(_vxfgpup9+(_znpjgsef - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) )));
									{
										System.Int32 __81fgg2dlsvn1669 = (System.Int32)((_znpjgsef + (int)1));
										const System.Int32 __81fgg2step1669 = (System.Int32)((int)1);
										System.Int32 __81fgg2count1669;
										for (__81fgg2count1669 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1669 + __81fgg2step1669) / __81fgg2step1669)), _b5p6od9s = __81fgg2dlsvn1669; __81fgg2count1669 != 0; __81fgg2count1669--, _b5p6od9s += (__81fgg2step1669)) {

										{
											
											*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) + (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c))));
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
			//*        Form  C := alpha*A**H*A + beta*C. 
			//* 
			
			if (_l08igmvf)
			{
				
				{
					System.Int32 __81fgg2dlsvn1670 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1670 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1670;
					for (__81fgg2count1670 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1670 + __81fgg2step1670) / __81fgg2step1670)), _znpjgsef = __81fgg2dlsvn1670; __81fgg2count1670 != 0; __81fgg2count1670--, _znpjgsef += (__81fgg2step1670)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn1671 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step1671 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1671;
							for (__81fgg2count1671 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn1671 + __81fgg2step1671) / __81fgg2step1671)), _b5p6od9s = __81fgg2dlsvn1671; __81fgg2count1671 != 0; __81fgg2count1671--, _b5p6od9s += (__81fgg2step1671)) {

							{
								
								_1ajfmh55 = DCMPLX(_d0547bi2);
								{
									System.Int32 __81fgg2dlsvn1672 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1672 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1672;
									for (__81fgg2count1672 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1672 + __81fgg2step1672) / __81fgg2step1672)), _68ec3gbh = __81fgg2dlsvn1672; __81fgg2count1672 != 0; __81fgg2count1672--, _68ec3gbh += (__81fgg2step1672)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 + (ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ) * *(_vxfgpup9+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
Mark190:;
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
								
Mark200:;
								// continue
							}
														}						}
						_b56sf68i = _d0547bi2;
						{
							System.Int32 __81fgg2dlsvn1673 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step1673 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1673;
							for (__81fgg2count1673 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1673 + __81fgg2step1673) / __81fgg2step1673)), _68ec3gbh = __81fgg2dlsvn1673; __81fgg2count1673 != 0; __81fgg2count1673--, _68ec3gbh += (__81fgg2step1673)) {

							{
								
								_b56sf68i = DBLE((_b56sf68i + (ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) * *(_vxfgpup9+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)))));
Mark210:;
								// continue
							}
														}						}
						if (_bafcbx97 == _d0547bi2)
						{
							
							*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = DCMPLX((_r7cfteg3 * _b56sf68i));
						}
						else
						{
							
							*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = DCMPLX(((_r7cfteg3 * _b56sf68i) + (_bafcbx97 * ILNumerics.F2NET.Intrinsics.DBLE(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) ))));
						}
						
Mark220:;
						// continue
					}
										}				}
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn1674 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1674 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1674;
					for (__81fgg2count1674 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1674 + __81fgg2step1674) / __81fgg2step1674)), _znpjgsef = __81fgg2dlsvn1674; __81fgg2count1674 != 0; __81fgg2count1674--, _znpjgsef += (__81fgg2step1674)) {

					{
						
						_b56sf68i = _d0547bi2;
						{
							System.Int32 __81fgg2dlsvn1675 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step1675 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1675;
							for (__81fgg2count1675 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1675 + __81fgg2step1675) / __81fgg2step1675)), _68ec3gbh = __81fgg2dlsvn1675; __81fgg2count1675 != 0; __81fgg2count1675--, _68ec3gbh += (__81fgg2step1675)) {

							{
								
								_b56sf68i = DBLE((_b56sf68i + (ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) * *(_vxfgpup9+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)))));
Mark230:;
								// continue
							}
														}						}
						if (_bafcbx97 == _d0547bi2)
						{
							
							*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = DCMPLX((_r7cfteg3 * _b56sf68i));
						}
						else
						{
							
							*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = DCMPLX(((_r7cfteg3 * _b56sf68i) + (_bafcbx97 * ILNumerics.F2NET.Intrinsics.DBLE(*(_3crf0qn3+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) ))));
						}
						
						{
							System.Int32 __81fgg2dlsvn1676 = (System.Int32)((_znpjgsef + (int)1));
							const System.Int32 __81fgg2step1676 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1676;
							for (__81fgg2count1676 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1676 + __81fgg2step1676) / __81fgg2step1676)), _b5p6od9s = __81fgg2dlsvn1676; __81fgg2count1676 != 0; __81fgg2count1676--, _b5p6od9s += (__81fgg2step1676)) {

							{
								
								_1ajfmh55 = DCMPLX(_d0547bi2);
								{
									System.Int32 __81fgg2dlsvn1677 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1677 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1677;
									for (__81fgg2count1677 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1677 + __81fgg2step1677) / __81fgg2step1677)), _68ec3gbh = __81fgg2dlsvn1677; __81fgg2count1677 != 0; __81fgg2count1677--, _68ec3gbh += (__81fgg2step1677)) {

									{
										
										_1ajfmh55 = (_1ajfmh55 + (ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ) * *(_vxfgpup9+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
Mark240:;
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
								
Mark250:;
								// continue
							}
														}						}
Mark260:;
						// continue
					}
										}				}
			}
			
		}
		//* 
		
		return;//* 
		//*     End of ZHERK . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
