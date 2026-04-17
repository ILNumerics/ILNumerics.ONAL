
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
//*> \brief \b ZLAQPS computes a step of QR factorization with column pivoting of a real m-by-n matrix A by using BLAS level 3. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZLAQPS + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zlaqps.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zlaqps.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zlaqps.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZLAQPS( M, N, OFFSET, NB, KB, A, LDA, JPVT, TAU, VN1, 
//*                          VN2, AUXV, F, LDF ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            KB, LDA, LDF, M, N, NB, OFFSET 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            JPVT( * ) 
//*       DOUBLE PRECISION   VN1( * ), VN2( * ) 
//*       COMPLEX*16         A( LDA, * ), AUXV( * ), F( LDF, * ), TAU( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZLAQPS computes a step of QR factorization with column pivoting 
//*> of a complex M-by-N matrix A by using Blas-3.  It tries to factorize 
//*> NB columns from A starting from the row OFFSET+1, and updates all 
//*> of the matrix with Blas-3 xGEMM. 
//*> 
//*> In some cases, due to catastrophic cancellations, it cannot 
//*> factorize NB columns.  Hence, the actual number of factorized 
//*> columns is returned in KB. 
//*> 
//*> Block A(1:OFFSET,1:N) is accordingly pivoted, but not factorized. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The number of rows of the matrix A. M >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of columns of the matrix A. N >= 0 
//*> \endverbatim 
//*> 
//*> \param[in] OFFSET 
//*> \verbatim 
//*>          OFFSET is INTEGER 
//*>          The number of rows of A that have been factorized in 
//*>          previous steps. 
//*> \endverbatim 
//*> 
//*> \param[in] NB 
//*> \verbatim 
//*>          NB is INTEGER 
//*>          The number of columns to factorize. 
//*> \endverbatim 
//*> 
//*> \param[out] KB 
//*> \verbatim 
//*>          KB is INTEGER 
//*>          The number of columns actually factorized. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is COMPLEX*16 array, dimension (LDA,N) 
//*>          On entry, the M-by-N matrix A. 
//*>          On exit, block A(OFFSET+1:M,1:KB) is the triangular 
//*>          factor obtained and block A(1:OFFSET,1:N) has been 
//*>          accordingly pivoted, but no factorized. 
//*>          The rest of the matrix, block A(OFFSET+1:M,KB+1:N) has 
//*>          been updated. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A. LDA >= max(1,M). 
//*> \endverbatim 
//*> 
//*> \param[in,out] JPVT 
//*> \verbatim 
//*>          JPVT is INTEGER array, dimension (N) 
//*>          JPVT(I) = K <==> Column K of the full matrix A has been 
//*>          permuted into position I in AP. 
//*> \endverbatim 
//*> 
//*> \param[out] TAU 
//*> \verbatim 
//*>          TAU is COMPLEX*16 array, dimension (KB) 
//*>          The scalar factors of the elementary reflectors. 
//*> \endverbatim 
//*> 
//*> \param[in,out] VN1 
//*> \verbatim 
//*>          VN1 is DOUBLE PRECISION array, dimension (N) 
//*>          The vector with the partial column norms. 
//*> \endverbatim 
//*> 
//*> \param[in,out] VN2 
//*> \verbatim 
//*>          VN2 is DOUBLE PRECISION array, dimension (N) 
//*>          The vector with the exact column norms. 
//*> \endverbatim 
//*> 
//*> \param[in,out] AUXV 
//*> \verbatim 
//*>          AUXV is COMPLEX*16 array, dimension (NB) 
//*>          Auxiliary vector. 
//*> \endverbatim 
//*> 
//*> \param[in,out] F 
//*> \verbatim 
//*>          F is COMPLEX*16 array, dimension (LDF,NB) 
//*>          Matrix F**H = L * Y**H * A. 
//*> \endverbatim 
//*> 
//*> \param[in] LDF 
//*> \verbatim 
//*>          LDF is INTEGER 
//*>          The leading dimension of the array F. LDF >= max(1,N). 
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
//*> \ingroup complex16OTHERauxiliary 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>    G. Quintana-Orti, Depto. de Informatica, Universidad Jaime I, Spain 
//*>    X. Sun, Computer Science Dept., Duke University, USA 
//*> \n 
//*>  Partial column norm updating strategy modified on April 2011 
//*>    Z. Drmac and Z. Bujanovic, Dept. of Mathematics, 
//*>    University of Zagreb, Croatia. 
//* 
//*> \par References: 
//*  ================ 
//*> 
//*> LAPACK Working Note 176 
//* 
//*> \htmlonly 
//*> <a href="http://www.netlib.org/lapack/lawnspdf/lawn176.pdf">[PDF]</a> 
//*> \endhtmlonly 
//* 
//*  ===================================================================== 

	 
	public static void _n6mc62mn(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Int32 _1l9k9q9k, ref Int32 _f7059815, ref Int32 _93gbwsug, complex* _vxfgpup9, ref Int32 _ocv8fk5c, Int32* _laipxa7w, complex* _0446f4de, Double* _noxmp3qo, Double* _m4m6epbx, complex* _vuev98z6, complex* _8plnuphw, ref Int32 _kauta3m8)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
complex _gdjumcqt =   new fcomplex(0f,0f);
complex _40vhxf9f =   new fcomplex(1f,0f);
Int32 _m1gysdbg =  default;
Int32 _znpjgsef =  default;
Int32 _umlkckdg =  default;
Int32 _5ovx73ge =  default;
Int32 _ucc3kcaj =  default;
Int32 _153xco97 =  default;
Int32 _kxcskn8m =  default;
Double _1ajfmh55 =  default;
Double _q3ig7mub =  default;
Double _krshq2hy =  default;
complex _2v7qhxyg =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK auxiliary routine (version 3.7.0) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
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
		//*     .. Parameters .. 
		//*     .. 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		_5ovx73ge = ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 ,_dxpq0xkr + _1l9k9q9k );
		_ucc3kcaj = (int)0;
		_umlkckdg = (int)0;
		_krshq2hy = ILNumerics.F2NET.Intrinsics.SQRT(_f43eg0w0("Epsilon" ) );//* 
		//*     Beginning of while loop. 
		//* 
		
Mark10:;
		// continue
		if ((_umlkckdg < _f7059815) & (_ucc3kcaj == (int)0))
		{
			
			_umlkckdg = (_umlkckdg + (int)1);
			_kxcskn8m = (_1l9k9q9k + _umlkckdg);//* 
			//*        Determine ith pivot column and swap if necessary 
			//* 
			
			_153xco97 = ((_umlkckdg - (int)1) + _ei7om7ok(ref Unsafe.AsRef((_dxpq0xkr - _umlkckdg) + (int)1) ,(_noxmp3qo+(_umlkckdg - 1)),ref Unsafe.AsRef((int)1) ));
			if (_153xco97 != _umlkckdg)
			{
				
				_027ahmgj(ref _ev4xhht5 ,(_vxfgpup9+((int)1 - 1) + (_153xco97 - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,(_vxfgpup9+((int)1 - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
				_027ahmgj(ref Unsafe.AsRef(_umlkckdg - (int)1) ,(_8plnuphw+(_153xco97 - 1) + ((int)1 - 1) * 1 * (_kauta3m8)),ref _kauta3m8 ,(_8plnuphw+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_kauta3m8)),ref _kauta3m8 );
				_m1gysdbg = *(_laipxa7w+(_153xco97 - 1));
				*(_laipxa7w+(_153xco97 - 1)) = *(_laipxa7w+(_umlkckdg - 1));
				*(_laipxa7w+(_umlkckdg - 1)) = _m1gysdbg;
				*(_noxmp3qo+(_153xco97 - 1)) = *(_noxmp3qo+(_umlkckdg - 1));
				*(_m4m6epbx+(_153xco97 - 1)) = *(_m4m6epbx+(_umlkckdg - 1));
			}
			//* 
			//*        Apply previous Householder reflectors to column K: 
			//*        A(RK:M,K) := A(RK:M,K) - A(RK:M,1:K-1)*F(K,1:K-1)**H. 
			//* 
			
			if (_umlkckdg > (int)1)
			{
				
				{
					System.Int32 __81fgg2dlsvn1848 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1848 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1848;
					for (__81fgg2count1848 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg - (int)1) - __81fgg2dlsvn1848 + __81fgg2step1848) / __81fgg2step1848)), _znpjgsef = __81fgg2dlsvn1848; __81fgg2count1848 != 0; __81fgg2count1848--, _znpjgsef += (__81fgg2step1848)) {

					{
						
						*(_8plnuphw+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_kauta3m8)) = ILNumerics.F2NET.Intrinsics.DCONJG(*(_8plnuphw+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_kauta3m8)) );
Mark20:;
						// continue
					}
										}				}
				_xfaqgfxk("No transpose" ,ref Unsafe.AsRef((_ev4xhht5 - _kxcskn8m) + (int)1) ,ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref Unsafe.AsRef(-(_40vhxf9f)) ,(_vxfgpup9+(_kxcskn8m - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_8plnuphw+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_kauta3m8)),ref _kauta3m8 ,ref Unsafe.AsRef(_40vhxf9f) ,(_vxfgpup9+(_kxcskn8m - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
				{
					System.Int32 __81fgg2dlsvn1849 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1849 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1849;
					for (__81fgg2count1849 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg - (int)1) - __81fgg2dlsvn1849 + __81fgg2step1849) / __81fgg2step1849)), _znpjgsef = __81fgg2dlsvn1849; __81fgg2count1849 != 0; __81fgg2count1849--, _znpjgsef += (__81fgg2step1849)) {

					{
						
						*(_8plnuphw+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_kauta3m8)) = ILNumerics.F2NET.Intrinsics.DCONJG(*(_8plnuphw+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_kauta3m8)) );
Mark30:;
						// continue
					}
										}				}
			}
			//* 
			//*        Generate elementary reflector H(k). 
			//* 
			
			if (_kxcskn8m < _ev4xhht5)
			{
				
				_4btmjfem(ref Unsafe.AsRef((_ev4xhht5 - _kxcskn8m) + (int)1) ,ref Unsafe.AsRef(*(_vxfgpup9+(_kxcskn8m - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c))) ,(_vxfgpup9+(_kxcskn8m + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_0446f4de+(_umlkckdg - 1))) );
			}
			else
			{
				
				_4btmjfem(ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_vxfgpup9+(_kxcskn8m - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c))) ,(_vxfgpup9+(_kxcskn8m - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_0446f4de+(_umlkckdg - 1))) );
			}
			//* 
			
			_2v7qhxyg = *(_vxfgpup9+(_kxcskn8m - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c));
			*(_vxfgpup9+(_kxcskn8m - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) = _40vhxf9f;//* 
			//*        Compute Kth column of F: 
			//* 
			//*        Compute  F(K+1:N,K) := tau(K)*A(RK:M,K+1:N)**H*A(RK:M,K). 
			//* 
			
			if (_umlkckdg < _dxpq0xkr)
			{
				
				_xfaqgfxk("Conjugate transpose" ,ref Unsafe.AsRef((_ev4xhht5 - _kxcskn8m) + (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref Unsafe.AsRef(*(_0446f4de+(_umlkckdg - 1))) ,(_vxfgpup9+(_kxcskn8m - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_kxcskn8m - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_gdjumcqt) ,(_8plnuphw+(_umlkckdg + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_kauta3m8)),ref Unsafe.AsRef((int)1) );
			}
			//* 
			//*        Padding F(1:K,K) with zeros. 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn1850 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1850 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1850;
				for (__81fgg2count1850 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1850 + __81fgg2step1850) / __81fgg2step1850)), _znpjgsef = __81fgg2dlsvn1850; __81fgg2count1850 != 0; __81fgg2count1850--, _znpjgsef += (__81fgg2step1850)) {

				{
					
					*(_8plnuphw+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_kauta3m8)) = _gdjumcqt;
Mark40:;
					// continue
				}
								}			}//* 
			//*        Incremental updating of F: 
			//*        F(1:N,K) := F(1:N,K) - tau(K)*F(1:N,1:K-1)*A(RK:M,1:K-1)**H 
			//*                    *A(RK:M,K). 
			//* 
			
			if (_umlkckdg > (int)1)
			{
				
				_xfaqgfxk("Conjugate transpose" ,ref Unsafe.AsRef((_ev4xhht5 - _kxcskn8m) + (int)1) ,ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref Unsafe.AsRef(-(*(_0446f4de+(_umlkckdg - 1)))) ,(_vxfgpup9+(_kxcskn8m - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_kxcskn8m - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_gdjumcqt) ,(_vuev98z6+((int)1 - 1)),ref Unsafe.AsRef((int)1) );//* 
				
				_xfaqgfxk("No transpose" ,ref _dxpq0xkr ,ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref Unsafe.AsRef(_40vhxf9f) ,(_8plnuphw+((int)1 - 1) + ((int)1 - 1) * 1 * (_kauta3m8)),ref _kauta3m8 ,(_vuev98z6+((int)1 - 1)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_40vhxf9f) ,(_8plnuphw+((int)1 - 1) + (_umlkckdg - 1) * 1 * (_kauta3m8)),ref Unsafe.AsRef((int)1) );
			}
			//* 
			//*        Update the current row of A: 
			//*        A(RK,K+1:N) := A(RK,K+1:N) - A(RK,1:K)*F(K+1:N,1:K)**H. 
			//* 
			
			if (_umlkckdg < _dxpq0xkr)
			{
				
				_xos1d1er("No transpose" ,"Conjugate transpose" ,ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref _umlkckdg ,ref Unsafe.AsRef(-(_40vhxf9f)) ,(_vxfgpup9+(_kxcskn8m - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_8plnuphw+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_kauta3m8)),ref _kauta3m8 ,ref Unsafe.AsRef(_40vhxf9f) ,(_vxfgpup9+(_kxcskn8m - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
			}
			//* 
			//*        Update partial column norms. 
			//* 
			
			if (_kxcskn8m < _5ovx73ge)
			{
				
				{
					System.Int32 __81fgg2dlsvn1851 = (System.Int32)((_umlkckdg + (int)1));
					const System.Int32 __81fgg2step1851 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1851;
					for (__81fgg2count1851 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1851 + __81fgg2step1851) / __81fgg2step1851)), _znpjgsef = __81fgg2dlsvn1851; __81fgg2count1851 != 0; __81fgg2count1851--, _znpjgsef += (__81fgg2step1851)) {

					{
						
						if (*(_noxmp3qo+(_znpjgsef - 1)) != _d0547bi2)
						{
							//* 
							//*                 NOTE: The following 4 lines follow from the analysis in 
							//*                 Lapack Working Note 176. 
							//* 
							
							_1ajfmh55 = (ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_kxcskn8m - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) / *(_noxmp3qo+(_znpjgsef - 1)));
							_1ajfmh55 = ILNumerics.F2NET.Intrinsics.MAX(_d0547bi2 ,(_kxg5drh2 + _1ajfmh55) * (_kxg5drh2 - _1ajfmh55) );
							_q3ig7mub = (_1ajfmh55 * __POW2((*(_noxmp3qo+(_znpjgsef - 1)) / *(_m4m6epbx+(_znpjgsef - 1)))));
							if (_q3ig7mub <= _krshq2hy)
							{
								
								*(_m4m6epbx+(_znpjgsef - 1)) = ILNumerics.F2NET.Intrinsics.DBLE(_ucc3kcaj );
								_ucc3kcaj = _znpjgsef;
							}
							else
							{
								
								*(_noxmp3qo+(_znpjgsef - 1)) = (*(_noxmp3qo+(_znpjgsef - 1)) * ILNumerics.F2NET.Intrinsics.SQRT(_1ajfmh55 ));
							}
							
						}
						
Mark50:;
						// continue
					}
										}				}
			}
			//* 
			
			*(_vxfgpup9+(_kxcskn8m - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) = _2v7qhxyg;//* 
			//*        End of while loop. 
			//* 
			goto Mark10;
		}
		
		_93gbwsug = _umlkckdg;
		_kxcskn8m = (_1l9k9q9k + _93gbwsug);//* 
		//*     Apply the block reflector to the rest of the matrix: 
		//*     A(OFFSET+KB+1:M,KB+1:N) := A(OFFSET+KB+1:M,KB+1:N) - 
		//*                         A(OFFSET+KB+1:M,1:KB)*F(KB+1:N,1:KB)**H. 
		//* 
		
		if (_93gbwsug < ILNumerics.F2NET.Intrinsics.MIN(_dxpq0xkr ,_ev4xhht5 - _1l9k9q9k ))
		{
			
			_xos1d1er("No transpose" ,"Conjugate transpose" ,ref Unsafe.AsRef(_ev4xhht5 - _kxcskn8m) ,ref Unsafe.AsRef(_dxpq0xkr - _93gbwsug) ,ref _93gbwsug ,ref Unsafe.AsRef(-(_40vhxf9f)) ,(_vxfgpup9+(_kxcskn8m + (int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_8plnuphw+(_93gbwsug + (int)1 - 1) + ((int)1 - 1) * 1 * (_kauta3m8)),ref _kauta3m8 ,ref Unsafe.AsRef(_40vhxf9f) ,(_vxfgpup9+(_kxcskn8m + (int)1 - 1) + (_93gbwsug + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
		}
		//* 
		//*     Recomputation of difficult columns. 
		//* 
		
Mark60:;
		// continue
		if (_ucc3kcaj > (int)0)
		{
			
			_m1gysdbg = ILNumerics.F2NET.Intrinsics.NINT(*(_m4m6epbx+(_ucc3kcaj - 1)) );
			*(_noxmp3qo+(_ucc3kcaj - 1)) = _yzrhzz6l(ref Unsafe.AsRef(_ev4xhht5 - _kxcskn8m) ,(_vxfgpup9+(_kxcskn8m + (int)1 - 1) + (_ucc3kcaj - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );//* 
			//*        NOTE: The computation of VN1( LSTICC ) relies on the fact that 
			//*        SNRM2 does not fail on vectors with norm below the value of 
			//*        SQRT(DLAMCH('S')) 
			//* 
			
			*(_m4m6epbx+(_ucc3kcaj - 1)) = *(_noxmp3qo+(_ucc3kcaj - 1));
			_ucc3kcaj = _m1gysdbg;goto Mark60;
		}
		//* 
		
		return;//* 
		//*     End of ZLAQPS 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
