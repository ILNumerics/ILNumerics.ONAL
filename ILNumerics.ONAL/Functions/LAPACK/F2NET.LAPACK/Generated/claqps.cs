// MIT License
// 
// Copyright (c) 2026 ILNumerics GmbH
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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
//*> \brief \b CLAQPS computes a step of QR factorization with column pivoting of a real m-by-n matrix A by using BLAS level 3. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CLAQPS + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/claqps.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/claqps.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/claqps.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CLAQPS( M, N, OFFSET, NB, KB, A, LDA, JPVT, TAU, VN1, 
//*                          VN2, AUXV, F, LDF ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            KB, LDA, LDF, M, N, NB, OFFSET 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            JPVT( * ) 
//*       REAL               VN1( * ), VN2( * ) 
//*       COMPLEX            A( LDA, * ), AUXV( * ), F( LDF, * ), TAU( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CLAQPS computes a step of QR factorization with column pivoting 
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
//*>          A is COMPLEX array, dimension (LDA,N) 
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
//*>          TAU is COMPLEX array, dimension (KB) 
//*>          The scalar factors of the elementary reflectors. 
//*> \endverbatim 
//*> 
//*> \param[in,out] VN1 
//*> \verbatim 
//*>          VN1 is REAL array, dimension (N) 
//*>          The vector with the partial column norms. 
//*> \endverbatim 
//*> 
//*> \param[in,out] VN2 
//*> \verbatim 
//*>          VN2 is REAL array, dimension (N) 
//*>          The vector with the exact column norms. 
//*> \endverbatim 
//*> 
//*> \param[in,out] AUXV 
//*> \verbatim 
//*>          AUXV is COMPLEX array, dimension (NB) 
//*>          Auxiliary vector. 
//*> \endverbatim 
//*> 
//*> \param[in,out] F 
//*> \verbatim 
//*>          F is COMPLEX array, dimension (LDF,NB) 
//*>          Matrix  F**H = L * Y**H * A. 
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
//*> \ingroup complexOTHERauxiliary 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>    G. Quintana-Orti, Depto. de Informatica, Universidad Jaime I, Spain 
//*>    X. Sun, Computer Science Dept., Duke University, USA 
//*> 
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

	 
	public static void _9ljn14xr(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Int32 _1l9k9q9k, ref Int32 _f7059815, ref Int32 _93gbwsug, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, Int32* _laipxa7w, fcomplex* _0446f4de, Single* _noxmp3qo, Single* _m4m6epbx, fcomplex* _vuev98z6, fcomplex* _8plnuphw, ref Int32 _kauta3m8)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Single _kxg5drh2 =  1f;
fcomplex _gdjumcqt =   new fcomplex(0f,0f);
fcomplex _40vhxf9f =   new fcomplex(1f,0f);
Int32 _m1gysdbg =  default;
Int32 _znpjgsef =  default;
Int32 _umlkckdg =  default;
Int32 _5ovx73ge =  default;
Int32 _ucc3kcaj =  default;
Int32 _153xco97 =  default;
Int32 _kxcskn8m =  default;
Single _1ajfmh55 =  default;
Single _q3ig7mub =  default;
Single _krshq2hy =  default;
fcomplex _2v7qhxyg =  default;
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
		_krshq2hy = ILNumerics.F2NET.Intrinsics.SQRT(_d5tu038y("Epsilon" ) );//* 
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
			
			_153xco97 = ((_umlkckdg - (int)1) + _z5b2nqbf(ref Unsafe.AsRef((_dxpq0xkr - _umlkckdg) + (int)1) ,(_noxmp3qo+(_umlkckdg - 1)),ref Unsafe.AsRef((int)1) ));
			if (_153xco97 != _umlkckdg)
			{
				
				_1frbwlh0(ref _ev4xhht5 ,(_vxfgpup9+((int)1 - 1) + (_153xco97 - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,(_vxfgpup9+((int)1 - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
				_1frbwlh0(ref Unsafe.AsRef(_umlkckdg - (int)1) ,(_8plnuphw+(_153xco97 - 1) + ((int)1 - 1) * 1 * (_kauta3m8)),ref _kauta3m8 ,(_8plnuphw+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_kauta3m8)),ref _kauta3m8 );
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
					System.Int32 __81fgg2dlsvn1840 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1840 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1840;
					for (__81fgg2count1840 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg - (int)1) - __81fgg2dlsvn1840 + __81fgg2step1840) / __81fgg2step1840)), _znpjgsef = __81fgg2dlsvn1840; __81fgg2count1840 != 0; __81fgg2count1840--, _znpjgsef += (__81fgg2step1840)) {

					{
						
						*(_8plnuphw+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_kauta3m8)) = ILNumerics.F2NET.Intrinsics.CONJG(*(_8plnuphw+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_kauta3m8)) );
Mark20:;
						// continue
					}
										}				}
				_f0oh3lvv("No transpose" ,ref Unsafe.AsRef((_ev4xhht5 - _kxcskn8m) + (int)1) ,ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref Unsafe.AsRef(-(_40vhxf9f)) ,(_vxfgpup9+(_kxcskn8m - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_8plnuphw+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_kauta3m8)),ref _kauta3m8 ,ref Unsafe.AsRef(_40vhxf9f) ,(_vxfgpup9+(_kxcskn8m - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
				{
					System.Int32 __81fgg2dlsvn1841 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1841 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1841;
					for (__81fgg2count1841 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg - (int)1) - __81fgg2dlsvn1841 + __81fgg2step1841) / __81fgg2step1841)), _znpjgsef = __81fgg2dlsvn1841; __81fgg2count1841 != 0; __81fgg2count1841--, _znpjgsef += (__81fgg2step1841)) {

					{
						
						*(_8plnuphw+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_kauta3m8)) = ILNumerics.F2NET.Intrinsics.CONJG(*(_8plnuphw+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_kauta3m8)) );
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
				
				_ocp87dc1(ref Unsafe.AsRef((_ev4xhht5 - _kxcskn8m) + (int)1) ,ref Unsafe.AsRef(*(_vxfgpup9+(_kxcskn8m - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c))) ,(_vxfgpup9+(_kxcskn8m + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_0446f4de+(_umlkckdg - 1))) );
			}
			else
			{
				
				_ocp87dc1(ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_vxfgpup9+(_kxcskn8m - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c))) ,(_vxfgpup9+(_kxcskn8m - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_0446f4de+(_umlkckdg - 1))) );
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
				
				_f0oh3lvv("Conjugate transpose" ,ref Unsafe.AsRef((_ev4xhht5 - _kxcskn8m) + (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref Unsafe.AsRef(*(_0446f4de+(_umlkckdg - 1))) ,(_vxfgpup9+(_kxcskn8m - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_kxcskn8m - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_gdjumcqt) ,(_8plnuphw+(_umlkckdg + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_kauta3m8)),ref Unsafe.AsRef((int)1) );
			}
			//* 
			//*        Padding F(1:K,K) with zeros. 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn1842 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1842 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1842;
				for (__81fgg2count1842 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1842 + __81fgg2step1842) / __81fgg2step1842)), _znpjgsef = __81fgg2dlsvn1842; __81fgg2count1842 != 0; __81fgg2count1842--, _znpjgsef += (__81fgg2step1842)) {

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
				
				_f0oh3lvv("Conjugate transpose" ,ref Unsafe.AsRef((_ev4xhht5 - _kxcskn8m) + (int)1) ,ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref Unsafe.AsRef(-(*(_0446f4de+(_umlkckdg - 1)))) ,(_vxfgpup9+(_kxcskn8m - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_kxcskn8m - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_gdjumcqt) ,(_vuev98z6+((int)1 - 1)),ref Unsafe.AsRef((int)1) );//* 
				
				_f0oh3lvv("No transpose" ,ref _dxpq0xkr ,ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref Unsafe.AsRef(_40vhxf9f) ,(_8plnuphw+((int)1 - 1) + ((int)1 - 1) * 1 * (_kauta3m8)),ref _kauta3m8 ,(_vuev98z6+((int)1 - 1)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_40vhxf9f) ,(_8plnuphw+((int)1 - 1) + (_umlkckdg - 1) * 1 * (_kauta3m8)),ref Unsafe.AsRef((int)1) );
			}
			//* 
			//*        Update the current row of A: 
			//*        A(RK,K+1:N) := A(RK,K+1:N) - A(RK,1:K)*F(K+1:N,1:K)**H. 
			//* 
			
			if (_umlkckdg < _dxpq0xkr)
			{
				
				_5p0w9905("No transpose" ,"Conjugate transpose" ,ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref _umlkckdg ,ref Unsafe.AsRef(-(_40vhxf9f)) ,(_vxfgpup9+(_kxcskn8m - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_8plnuphw+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_kauta3m8)),ref _kauta3m8 ,ref Unsafe.AsRef(_40vhxf9f) ,(_vxfgpup9+(_kxcskn8m - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
			}
			//* 
			//*        Update partial column norms. 
			//* 
			
			if (_kxcskn8m < _5ovx73ge)
			{
				
				{
					System.Int32 __81fgg2dlsvn1843 = (System.Int32)((_umlkckdg + (int)1));
					const System.Int32 __81fgg2step1843 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1843;
					for (__81fgg2count1843 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1843 + __81fgg2step1843) / __81fgg2step1843)), _znpjgsef = __81fgg2dlsvn1843; __81fgg2count1843 != 0; __81fgg2count1843--, _znpjgsef += (__81fgg2step1843)) {

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
								
								*(_m4m6epbx+(_znpjgsef - 1)) = ILNumerics.F2NET.Intrinsics.REAL(_ucc3kcaj );
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
			
			_5p0w9905("No transpose" ,"Conjugate transpose" ,ref Unsafe.AsRef(_ev4xhht5 - _kxcskn8m) ,ref Unsafe.AsRef(_dxpq0xkr - _93gbwsug) ,ref _93gbwsug ,ref Unsafe.AsRef(-(_40vhxf9f)) ,(_vxfgpup9+(_kxcskn8m + (int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_8plnuphw+(_93gbwsug + (int)1 - 1) + ((int)1 - 1) * 1 * (_kauta3m8)),ref _kauta3m8 ,ref Unsafe.AsRef(_40vhxf9f) ,(_vxfgpup9+(_kxcskn8m + (int)1 - 1) + (_93gbwsug + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
		}
		//* 
		//*     Recomputation of difficult columns. 
		//* 
		
Mark60:;
		// continue
		if (_ucc3kcaj > (int)0)
		{
			
			_m1gysdbg = ILNumerics.F2NET.Intrinsics.NINT(*(_m4m6epbx+(_ucc3kcaj - 1)) );
			*(_noxmp3qo+(_ucc3kcaj - 1)) = _igbqnt3f(ref Unsafe.AsRef(_ev4xhht5 - _kxcskn8m) ,(_vxfgpup9+(_kxcskn8m + (int)1 - 1) + (_ucc3kcaj - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );//* 
			//*        NOTE: The computation of VN1( LSTICC ) relies on the fact that 
			//*        SNRM2 does not fail on vectors with norm below the value of 
			//*        SQRT(DLAMCH('S')) 
			//* 
			
			*(_m4m6epbx+(_ucc3kcaj - 1)) = *(_noxmp3qo+(_ucc3kcaj - 1));
			_ucc3kcaj = _m1gysdbg;goto Mark60;
		}
		//* 
		
		return;//* 
		//*     End of CLAQPS 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
