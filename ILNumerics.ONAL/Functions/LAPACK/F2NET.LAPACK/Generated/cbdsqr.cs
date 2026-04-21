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
//*> \brief \b CBDSQR 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CBDSQR + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/cbdsqr.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/cbdsqr.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/cbdsqr.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CBDSQR( UPLO, N, NCVT, NRU, NCC, D, E, VT, LDVT, U, 
//*                          LDU, C, LDC, RWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          UPLO 
//*       INTEGER            INFO, LDC, LDU, LDVT, N, NCC, NCVT, NRU 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               D( * ), E( * ), RWORK( * ) 
//*       COMPLEX            C( LDC, * ), U( LDU, * ), VT( LDVT, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CBDSQR computes the singular values and, optionally, the right and/or 
//*> left singular vectors from the singular value decomposition (SVD) of 
//*> a real N-by-N (upper or lower) bidiagonal matrix B using the implicit 
//*> zero-shift QR algorithm.  The SVD of B has the form 
//*> 
//*>    B = Q * S * P**H 
//*> 
//*> where S is the diagonal matrix of singular values, Q is an orthogonal 
//*> matrix of left singular vectors, and P is an orthogonal matrix of 
//*> right singular vectors.  If left singular vectors are requested, this 
//*> subroutine actually returns U*Q instead of Q, and, if right singular 
//*> vectors are requested, this subroutine returns P**H*VT instead of 
//*> P**H, for given complex input matrices U and VT.  When U and VT are 
//*> the unitary matrices that reduce a general matrix A to bidiagonal 
//*> form: A = U*B*VT, as computed by CGEBRD, then 
//*> 
//*>    A = (U*Q) * S * (P**H*VT) 
//*> 
//*> is the SVD of A.  Optionally, the subroutine may also compute Q**H*C 
//*> for a given complex input matrix C. 
//*> 
//*> See "Computing  Small Singular Values of Bidiagonal Matrices With 
//*> Guaranteed High Relative Accuracy," by J. Demmel and W. Kahan, 
//*> LAPACK Working Note #3 (or SIAM J. Sci. Statist. Comput. vol. 11, 
//*> no. 5, pp. 873-912, Sept 1990) and 
//*> "Accurate singular values and differential qd algorithms," by 
//*> B. Parlett and V. Fernando, Technical Report CPAM-554, Mathematics 
//*> Department, University of California at Berkeley, July 1992 
//*> for a detailed description of the algorithm. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] UPLO 
//*> \verbatim 
//*>          UPLO is CHARACTER*1 
//*>          = 'U':  B is upper bidiagonal; 
//*>          = 'L':  B is lower bidiagonal. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix B.  N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] NCVT 
//*> \verbatim 
//*>          NCVT is INTEGER 
//*>          The number of columns of the matrix VT. NCVT >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] NRU 
//*> \verbatim 
//*>          NRU is INTEGER 
//*>          The number of rows of the matrix U. NRU >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] NCC 
//*> \verbatim 
//*>          NCC is INTEGER 
//*>          The number of columns of the matrix C. NCC >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] D 
//*> \verbatim 
//*>          D is REAL array, dimension (N) 
//*>          On entry, the n diagonal elements of the bidiagonal matrix B. 
//*>          On exit, if INFO=0, the singular values of B in decreasing 
//*>          order. 
//*> \endverbatim 
//*> 
//*> \param[in,out] E 
//*> \verbatim 
//*>          E is REAL array, dimension (N-1) 
//*>          On entry, the N-1 offdiagonal elements of the bidiagonal 
//*>          matrix B. 
//*>          On exit, if INFO = 0, E is destroyed; if INFO > 0, D and E 
//*>          will contain the diagonal and superdiagonal elements of a 
//*>          bidiagonal matrix orthogonally equivalent to the one given 
//*>          as input. 
//*> \endverbatim 
//*> 
//*> \param[in,out] VT 
//*> \verbatim 
//*>          VT is COMPLEX array, dimension (LDVT, NCVT) 
//*>          On entry, an N-by-NCVT matrix VT. 
//*>          On exit, VT is overwritten by P**H * VT. 
//*>          Not referenced if NCVT = 0. 
//*> \endverbatim 
//*> 
//*> \param[in] LDVT 
//*> \verbatim 
//*>          LDVT is INTEGER 
//*>          The leading dimension of the array VT. 
//*>          LDVT >= max(1,N) if NCVT > 0; LDVT >= 1 if NCVT = 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] U 
//*> \verbatim 
//*>          U is COMPLEX array, dimension (LDU, N) 
//*>          On entry, an NRU-by-N matrix U. 
//*>          On exit, U is overwritten by U * Q. 
//*>          Not referenced if NRU = 0. 
//*> \endverbatim 
//*> 
//*> \param[in] LDU 
//*> \verbatim 
//*>          LDU is INTEGER 
//*>          The leading dimension of the array U.  LDU >= max(1,NRU). 
//*> \endverbatim 
//*> 
//*> \param[in,out] C 
//*> \verbatim 
//*>          C is COMPLEX array, dimension (LDC, NCC) 
//*>          On entry, an N-by-NCC matrix C. 
//*>          On exit, C is overwritten by Q**H * C. 
//*>          Not referenced if NCC = 0. 
//*> \endverbatim 
//*> 
//*> \param[in] LDC 
//*> \verbatim 
//*>          LDC is INTEGER 
//*>          The leading dimension of the array C. 
//*>          LDC >= max(1,N) if NCC > 0; LDC >=1 if NCC = 0. 
//*> \endverbatim 
//*> 
//*> \param[out] RWORK 
//*> \verbatim 
//*>          RWORK is REAL array, dimension (4*N) 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit 
//*>          < 0:  If INFO = -i, the i-th argument had an illegal value 
//*>          > 0:  the algorithm did not converge; D and E contain the 
//*>                elements of a bidiagonal matrix which is orthogonally 
//*>                similar to the input matrix B;  if INFO = i, i 
//*>                elements of E have not converged to zero. 
//*> \endverbatim 
//* 
//*> \par Internal Parameters: 
//*  ========================= 
//*> 
//*> \verbatim 
//*>  TOLMUL  REAL, default = max(10,min(100,EPS**(-1/8))) 
//*>          TOLMUL controls the convergence criterion of the QR loop. 
//*>          If it is positive, TOLMUL*EPS is the desired relative 
//*>             precision in the computed singular values. 
//*>          If it is negative, abs(TOLMUL*EPS*sigma_max) is the 
//*>             desired absolute accuracy in the computed singular 
//*>             values (corresponds to relative accuracy 
//*>             abs(TOLMUL*EPS) in the largest singular value. 
//*>          abs(TOLMUL) should be between 1 and 1/EPS, and preferably 
//*>             between 10 (for fast convergence) and .1/EPS 
//*>             (for there to be some accuracy in the results). 
//*>          Default is to lose at either one eighth or 2 of the 
//*>             available decimal digits in each computed singular value 
//*>             (whichever is smaller). 
//*> 
//*>  MAXITR  INTEGER, default = 6 
//*>          MAXITR controls the maximum number of passes of the 
//*>          algorithm through its inner loop. The algorithms stops 
//*>          (and so fails to converge) if the number of passes 
//*>          through the inner loop exceeds MAXITR*N**2. 
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
//*> \ingroup complexOTHERcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _ppf48s29(FString _9wyre9zc, ref Int32 _dxpq0xkr, ref Int32 _0v6rtqiq, ref Int32 _tg62ial7, ref Int32 _bcsi4mx0, Single* _plfm7z8g, Single* _864fslqq, fcomplex* _xdbczr8u, ref Int32 _h4ibbatv, fcomplex* _7u55mqkq, ref Int32 _u6e6d39b, fcomplex* _3crf0qn3, ref Int32 _1s3eymp4, Single* _dqanbbw3, ref Int32 _gro5yvfo)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Single _kxg5drh2 =  1f;
Single _2v8pp9oq =  -1f;
Single _8id84wrk =  0.01f;
Single _76g572q1 =  10f;
Single _cu7spn6z =  100f;
Single _qpjmg2qj =  -0.125f;
Int32 _to3hwp46 =  (int)6;
Boolean _58l4so5d =  default;
Boolean _77utirhk =  default;
Int32 _b5p6od9s =  default;
Int32 _2d4fuxb5 =  default;
Int32 _ndl0tuhx =  default;
Int32 _em7fbywm =  default;
Int32 _znpjgsef =  default;
Int32 _jnnmt81a =  default;
Int32 _zo9zhv2g =  default;
Int32 _ev4xhht5 =  default;
Int32 _gaia76w5 =  default;
Int32 _3xbv3idt =  default;
Int32 _y3zmt0e3 =  default;
Int32 _nowdo328 =  default;
Int32 _emfdpg6h =  default;
Int32 _kyn0ufud =  default;
Single _k08xhqq9 =  default;
Single _kgl39y18 =  default;
Single _bf18ryhs =  default;
Single _7sb5moji =  default;
Single _82tpdhyl =  default;
Single _p1iqarg6 =  default;
Single _8plnuphw =  default;
Single _mu73se41 =  default;
Single _ogkjl6gu =  default;
Single _4s3y5e1x =  default;
Single _5g3j4div =  default;
Single _6oypowww =  default;
Single _q2vwp05i =  default;
Single _e8rfnip7 =  default;
Single _sq0ypuw6 =  default;
Single _7d8q5cd7 =  default;
Single _ndcl48x4 =  default;
Single _jp87z33i =  default;
Single _6ggpocik =  default;
Single _t7m1e103 =  default;
Single _rhnpgpoi =  default;
Single _l1bltk1u =  default;
Single _25o9vyhd =  default;
Single _8tmd0ner =  default;
Single _j0ty6ytl =  default;
Single _txq1gp7u =  default;
Single _4117v60s =  default;
Single _zfw72syv =  default;
string fLanavab = default;
#endregion  variable declarations
_9wyre9zc = _9wyre9zc.Convert(1);

	{
		//* 
		//*  -- LAPACK computational routine (version 3.7.0) -- 
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
		//*     .. External Functions .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input parameters. 
		//* 
		
		_gro5yvfo = (int)0;
		_58l4so5d = _w8y2rzgy(_9wyre9zc ,"L" );
		if ((!(_w8y2rzgy(_9wyre9zc ,"U" ))) & (!(_58l4so5d)))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_0v6rtqiq < (int)0)
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if (_tg62ial7 < (int)0)
		{
			
			_gro5yvfo = (int)-4;
		}
		else
		if (_bcsi4mx0 < (int)0)
		{
			
			_gro5yvfo = (int)-5;
		}
		else
		if (((_0v6rtqiq == (int)0) & (_h4ibbatv < (int)1)) | ((_0v6rtqiq > (int)0) & (_h4ibbatv < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))))
		{
			
			_gro5yvfo = (int)-9;
		}
		else
		if (_u6e6d39b < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_tg62ial7 ))
		{
			
			_gro5yvfo = (int)-11;
		}
		else
		if (((_bcsi4mx0 == (int)0) & (_1s3eymp4 < (int)1)) | ((_bcsi4mx0 > (int)0) & (_1s3eymp4 < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))))
		{
			
			_gro5yvfo = (int)-13;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("CBDSQR" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		
		if (_dxpq0xkr == (int)0)
		return;
		if (_dxpq0xkr == (int)1)goto Mark160;//* 
		//*     ROTATE is true if any singular vectors desired, false otherwise 
		//* 
		
		_77utirhk = (((_0v6rtqiq > (int)0) | (_tg62ial7 > (int)0)) | (_bcsi4mx0 > (int)0));//* 
		//*     If no singular vectors desired, use qd algorithm 
		//* 
		
		if (!(_77utirhk))
		{
			
			_h56npt5m(ref _dxpq0xkr ,_plfm7z8g ,_864fslqq ,_dqanbbw3 ,ref _gro5yvfo );//* 
			//*     If INFO equals 2, dqds didn't finish, try to finish 
			//* 
			
			if (_gro5yvfo != (int)2)
			return;
			_gro5yvfo = (int)0;
		}
		//* 
		
		_3xbv3idt = (_dxpq0xkr - (int)1);
		_y3zmt0e3 = (_3xbv3idt + _3xbv3idt);
		_nowdo328 = (_y3zmt0e3 + _3xbv3idt);
		_2d4fuxb5 = (int)0;//* 
		//*     Get machine constants 
		//* 
		
		_p1iqarg6 = _d5tu038y("Epsilon" );
		_zfw72syv = _d5tu038y("Safe minimum" );//* 
		//*     If matrix lower bidiagonal, rotate to be upper bidiagonal 
		//*     by applying Givens rotations on the left 
		//* 
		
		if (_58l4so5d)
		{
			
			{
				System.Int32 __81fgg2dlsvn1370 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1370 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1370;
				for (__81fgg2count1370 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn1370 + __81fgg2step1370) / __81fgg2step1370)), _b5p6od9s = __81fgg2dlsvn1370; __81fgg2count1370 != 0; __81fgg2count1370--, _b5p6od9s += (__81fgg2step1370)) {

				{
					
					_uf57gsrz(ref Unsafe.AsRef(*(_plfm7z8g+(_b5p6od9s - 1))) ,ref Unsafe.AsRef(*(_864fslqq+(_b5p6od9s - 1))) ,ref _82tpdhyl ,ref _8tmd0ner ,ref _q2vwp05i );
					*(_plfm7z8g+(_b5p6od9s - 1)) = _q2vwp05i;
					*(_864fslqq+(_b5p6od9s - 1)) = (_8tmd0ner * *(_plfm7z8g+(_b5p6od9s + (int)1 - 1)));
					*(_plfm7z8g+(_b5p6od9s + (int)1 - 1)) = (_82tpdhyl * *(_plfm7z8g+(_b5p6od9s + (int)1 - 1)));
					*(_dqanbbw3+(_b5p6od9s - 1)) = _82tpdhyl;
					*(_dqanbbw3+(_3xbv3idt + _b5p6od9s - 1)) = _8tmd0ner;
Mark10:;
					// continue
				}
								}			}//* 
			//*        Update singular vectors if desired 
			//* 
			
			if (_tg62ial7 > (int)0)
			_40qhbg49("R" ,"V" ,"F" ,ref _tg62ial7 ,ref _dxpq0xkr ,(_dqanbbw3+((int)1 - 1)),(_dqanbbw3+(_dxpq0xkr - 1)),_7u55mqkq ,ref _u6e6d39b );
			if (_bcsi4mx0 > (int)0)
			_40qhbg49("L" ,"V" ,"F" ,ref _dxpq0xkr ,ref _bcsi4mx0 ,(_dqanbbw3+((int)1 - 1)),(_dqanbbw3+(_dxpq0xkr - 1)),_3crf0qn3 ,ref _1s3eymp4 );
		}
		//* 
		//*     Compute singular values to relative accuracy TOL 
		//*     (By setting TOL to be negative, algorithm will compute 
		//*     singular values to absolute accuracy ABS(TOL)*norm(input matrix)) 
		//* 
		
		_4117v60s = ILNumerics.F2NET.Intrinsics.MAX(_76g572q1 ,ILNumerics.F2NET.Intrinsics.MIN(_cu7spn6z ,__POW(_p1iqarg6, _qpjmg2qj) ) );
		_txq1gp7u = (_4117v60s * _p1iqarg6);//* 
		//*     Compute approximate maximum, minimum singular values 
		//* 
		
		_t7m1e103 = _d0547bi2;
		{
			System.Int32 __81fgg2dlsvn1371 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1371 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1371;
			for (__81fgg2count1371 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1371 + __81fgg2step1371) / __81fgg2step1371)), _b5p6od9s = __81fgg2dlsvn1371; __81fgg2count1371 != 0; __81fgg2count1371--, _b5p6od9s += (__81fgg2step1371)) {

			{
				
				_t7m1e103 = ILNumerics.F2NET.Intrinsics.MAX(_t7m1e103 ,ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_b5p6od9s - 1)) ) );
Mark20:;
				// continue
			}
						}		}
		{
			System.Int32 __81fgg2dlsvn1372 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1372 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1372;
			for (__81fgg2count1372 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn1372 + __81fgg2step1372) / __81fgg2step1372)), _b5p6od9s = __81fgg2dlsvn1372; __81fgg2count1372 != 0; __81fgg2count1372--, _b5p6od9s += (__81fgg2step1372)) {

			{
				
				_t7m1e103 = ILNumerics.F2NET.Intrinsics.MAX(_t7m1e103 ,ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_b5p6od9s - 1)) ) );
Mark30:;
				// continue
			}
						}		}
		_l1bltk1u = _d0547bi2;
		if (_txq1gp7u >= _d0547bi2)
		{
			//* 
			//*        Relative accuracy desired 
			//* 
			
			_25o9vyhd = ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+((int)1 - 1)) );
			if (_25o9vyhd == _d0547bi2)goto Mark50;
			_4s3y5e1x = _25o9vyhd;
			{
				System.Int32 __81fgg2dlsvn1373 = (System.Int32)((int)2);
				const System.Int32 __81fgg2step1373 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1373;
				for (__81fgg2count1373 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1373 + __81fgg2step1373) / __81fgg2step1373)), _b5p6od9s = __81fgg2dlsvn1373; __81fgg2count1373 != 0; __81fgg2count1373--, _b5p6od9s += (__81fgg2step1373)) {

				{
					
					_4s3y5e1x = (ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_b5p6od9s - 1)) ) * (_4s3y5e1x / (_4s3y5e1x + ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_b5p6od9s - (int)1 - 1)) ))));
					_25o9vyhd = ILNumerics.F2NET.Intrinsics.MIN(_25o9vyhd ,_4s3y5e1x );
					if (_25o9vyhd == _d0547bi2)goto Mark50;
Mark40:;
					// continue
				}
								}			}
Mark50:;
			// continue
			_25o9vyhd = (_25o9vyhd / ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.REAL(_dxpq0xkr ) ));
			_j0ty6ytl = ILNumerics.F2NET.Intrinsics.MAX(_txq1gp7u * _25o9vyhd ,((_to3hwp46 * _dxpq0xkr) * _dxpq0xkr) * _zfw72syv );
		}
		else
		{
			//* 
			//*        Absolute accuracy desired 
			//* 
			
			_j0ty6ytl = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_txq1gp7u ) * _t7m1e103 ,((_to3hwp46 * _dxpq0xkr) * _dxpq0xkr) * _zfw72syv );
		}
		//* 
		//*     Prepare for main iteration loop for the singular values 
		//*     (MAXIT is the maximum number of passes through the inner 
		//*     loop permitted before nonconvergence signalled.) 
		//* 
		
		_gaia76w5 = ((_to3hwp46 * _dxpq0xkr) * _dxpq0xkr);
		_em7fbywm = (int)0;
		_emfdpg6h = (int)-1;
		_kyn0ufud = (int)-1;//* 
		//*     M points to last element of unconverged part of matrix 
		//* 
		
		_ev4xhht5 = _dxpq0xkr;//* 
		//*     Begin main iteration loop 
		//* 
		
Mark60:;
		// continue//* 
		//*     Check for convergence or exceeding iteration count 
		//* 
		
		if (_ev4xhht5 <= (int)1)goto Mark160;
		if (_em7fbywm > _gaia76w5)goto Mark200;//* 
		//*     Find diagonal block of matrix to work on 
		//* 
		
		if ((_txq1gp7u < _d0547bi2) & (ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_ev4xhht5 - 1)) ) <= _j0ty6ytl))
		*(_plfm7z8g+(_ev4xhht5 - 1)) = _d0547bi2;
		_t7m1e103 = ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_ev4xhht5 - 1)) );
		_rhnpgpoi = _t7m1e103;
		{
			System.Int32 __81fgg2dlsvn1374 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1374 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1374;
			for (__81fgg2count1374 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5 - (int)1) - __81fgg2dlsvn1374 + __81fgg2step1374) / __81fgg2step1374)), _zo9zhv2g = __81fgg2dlsvn1374; __81fgg2count1374 != 0; __81fgg2count1374--, _zo9zhv2g += (__81fgg2step1374)) {

			{
				
				_jnnmt81a = (_ev4xhht5 - _zo9zhv2g);
				_kgl39y18 = ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_jnnmt81a - 1)) );
				_k08xhqq9 = ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_jnnmt81a - 1)) );
				if ((_txq1gp7u < _d0547bi2) & (_kgl39y18 <= _j0ty6ytl))
				*(_plfm7z8g+(_jnnmt81a - 1)) = _d0547bi2;
				if (_k08xhqq9 <= _j0ty6ytl)goto Mark80;
				_rhnpgpoi = ILNumerics.F2NET.Intrinsics.MIN(_rhnpgpoi ,_kgl39y18 );
				_t7m1e103 = ILNumerics.F2NET.Intrinsics.MAX(_t7m1e103 ,_kgl39y18 ,_k08xhqq9 );
Mark70:;
				// continue
			}
						}		}
		_jnnmt81a = (int)0;goto Mark90;
Mark80:;
		// continue
		*(_864fslqq+(_jnnmt81a - 1)) = _d0547bi2;//* 
		//*     Matrix splits since E(LL) = 0 
		//* 
		
		if (_jnnmt81a == (_ev4xhht5 - (int)1))
		{
			//* 
			//*        Convergence of bottom singular value, return to top of loop 
			//* 
			
			_ev4xhht5 = (_ev4xhht5 - (int)1);goto Mark60;
		}
		
Mark90:;
		// continue
		_jnnmt81a = (_jnnmt81a + (int)1);//* 
		//*     E(LL) through E(M-1) are nonzero, E(LL-1) is zero 
		//* 
		
		if (_jnnmt81a == (_ev4xhht5 - (int)1))
		{
			//* 
			//*        2 by 2 block, handle separately 
			//* 
			
			_eyqnke7t(ref Unsafe.AsRef(*(_plfm7z8g+(_ev4xhht5 - (int)1 - 1))) ,ref Unsafe.AsRef(*(_864fslqq+(_ev4xhht5 - (int)1 - 1))) ,ref Unsafe.AsRef(*(_plfm7z8g+(_ev4xhht5 - 1))) ,ref _sq0ypuw6 ,ref _7d8q5cd7 ,ref _jp87z33i ,ref _7sb5moji ,ref _ndcl48x4 ,ref _bf18ryhs );
			*(_plfm7z8g+(_ev4xhht5 - (int)1 - 1)) = _7d8q5cd7;
			*(_864fslqq+(_ev4xhht5 - (int)1 - 1)) = _d0547bi2;
			*(_plfm7z8g+(_ev4xhht5 - 1)) = _sq0ypuw6;//* 
			//*        Compute singular vectors, if desired 
			//* 
			
			if (_0v6rtqiq > (int)0)
			_fvro911c(ref _0v6rtqiq ,(_xdbczr8u+(_ev4xhht5 - (int)1 - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv ,(_xdbczr8u+(_ev4xhht5 - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv ,ref _7sb5moji ,ref _jp87z33i );
			if (_tg62ial7 > (int)0)
			_fvro911c(ref _tg62ial7 ,(_7u55mqkq+((int)1 - 1) + (_ev4xhht5 - (int)1 - 1) * 1 * (_u6e6d39b)),ref Unsafe.AsRef((int)1) ,(_7u55mqkq+((int)1 - 1) + (_ev4xhht5 - 1) * 1 * (_u6e6d39b)),ref Unsafe.AsRef((int)1) ,ref _bf18ryhs ,ref _ndcl48x4 );
			if (_bcsi4mx0 > (int)0)
			_fvro911c(ref _bcsi4mx0 ,(_3crf0qn3+(_ev4xhht5 - (int)1 - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_3crf0qn3+(_ev4xhht5 - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,ref _bf18ryhs ,ref _ndcl48x4 );
			_ev4xhht5 = (_ev4xhht5 - (int)2);goto Mark60;
		}
		//* 
		//*     If working on new submatrix, choose shift direction 
		//*     (from larger end diagonal element towards smaller) 
		//* 
		
		if ((_jnnmt81a > _kyn0ufud) | (_ev4xhht5 < _emfdpg6h))
		{
			
			if (ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_jnnmt81a - 1)) ) >= ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_ev4xhht5 - 1)) ))
			{
				//* 
				//*           Chase bulge from top (big end) to bottom (small end) 
				//* 
				
				_2d4fuxb5 = (int)1;
			}
			else
			{
				//* 
				//*           Chase bulge from bottom (big end) to top (small end) 
				//* 
				
				_2d4fuxb5 = (int)2;
			}
			
		}
		//* 
		//*     Apply convergence tests 
		//* 
		
		if (_2d4fuxb5 == (int)1)
		{
			//* 
			//*        Run convergence test in forward direction 
			//*        First apply standard test to bottom of matrix 
			//* 
			
			if ((ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_ev4xhht5 - (int)1 - 1)) ) <= (ILNumerics.F2NET.Intrinsics.ABS(_txq1gp7u ) * ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_ev4xhht5 - 1)) ))) | ((_txq1gp7u < _d0547bi2) & (ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_ev4xhht5 - (int)1 - 1)) ) <= _j0ty6ytl)))
			{
				
				*(_864fslqq+(_ev4xhht5 - (int)1 - 1)) = _d0547bi2;goto Mark60;
			}
			//* 
			
			if (_txq1gp7u >= _d0547bi2)
			{
				//* 
				//*           If relative accuracy desired, 
				//*           apply convergence criterion forward 
				//* 
				
				_4s3y5e1x = ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_jnnmt81a - 1)) );
				_l1bltk1u = _4s3y5e1x;
				{
					System.Int32 __81fgg2dlsvn1375 = (System.Int32)(_jnnmt81a);
					const System.Int32 __81fgg2step1375 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1375;
					for (__81fgg2count1375 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5 - (int)1) - __81fgg2dlsvn1375 + __81fgg2step1375) / __81fgg2step1375)), _zo9zhv2g = __81fgg2dlsvn1375; __81fgg2count1375 != 0; __81fgg2count1375--, _zo9zhv2g += (__81fgg2step1375)) {

					{
						
						if (ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_zo9zhv2g - 1)) ) <= (_txq1gp7u * _4s3y5e1x))
						{
							
							*(_864fslqq+(_zo9zhv2g - 1)) = _d0547bi2;goto Mark60;
						}
						
						_4s3y5e1x = (ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_zo9zhv2g + (int)1 - 1)) ) * (_4s3y5e1x / (_4s3y5e1x + ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_zo9zhv2g - 1)) ))));
						_l1bltk1u = ILNumerics.F2NET.Intrinsics.MIN(_l1bltk1u ,_4s3y5e1x );
Mark100:;
						// continue
					}
										}				}
			}
			//* 
			
		}
		else
		{
			//* 
			//*        Run convergence test in backward direction 
			//*        First apply standard test to top of matrix 
			//* 
			
			if ((ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_jnnmt81a - 1)) ) <= (ILNumerics.F2NET.Intrinsics.ABS(_txq1gp7u ) * ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_jnnmt81a - 1)) ))) | ((_txq1gp7u < _d0547bi2) & (ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_jnnmt81a - 1)) ) <= _j0ty6ytl)))
			{
				
				*(_864fslqq+(_jnnmt81a - 1)) = _d0547bi2;goto Mark60;
			}
			//* 
			
			if (_txq1gp7u >= _d0547bi2)
			{
				//* 
				//*           If relative accuracy desired, 
				//*           apply convergence criterion backward 
				//* 
				
				_4s3y5e1x = ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_ev4xhht5 - 1)) );
				_l1bltk1u = _4s3y5e1x;
				{
					System.Int32 __81fgg2dlsvn1376 = (System.Int32)((_ev4xhht5 - (int)1));
					System.Int32 __81fgg2step1376 = (System.Int32)((int)-1);
					System.Int32 __81fgg2count1376;
					for (__81fgg2count1376 = System.Math.Max(0, (System.Int32)(((System.Int32)(_jnnmt81a) - __81fgg2dlsvn1376 + __81fgg2step1376) / __81fgg2step1376)), _zo9zhv2g = __81fgg2dlsvn1376; __81fgg2count1376 != 0; __81fgg2count1376--, _zo9zhv2g += (__81fgg2step1376)) {

					{
						
						if (ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_zo9zhv2g - 1)) ) <= (_txq1gp7u * _4s3y5e1x))
						{
							
							*(_864fslqq+(_zo9zhv2g - 1)) = _d0547bi2;goto Mark60;
						}
						
						_4s3y5e1x = (ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_zo9zhv2g - 1)) ) * (_4s3y5e1x / (_4s3y5e1x + ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_zo9zhv2g - 1)) ))));
						_l1bltk1u = ILNumerics.F2NET.Intrinsics.MIN(_l1bltk1u ,_4s3y5e1x );
Mark110:;
						// continue
					}
										}				}
			}
			
		}
		
		_emfdpg6h = _jnnmt81a;
		_kyn0ufud = _ev4xhht5;//* 
		//*     Compute shift.  First, test if shifting would ruin relative 
		//*     accuracy, and if so set the shift to zero. 
		//* 
		
		if ((_txq1gp7u >= _d0547bi2) & (((_dxpq0xkr * _txq1gp7u) * (_l1bltk1u / _t7m1e103)) <= ILNumerics.F2NET.Intrinsics.MAX(_p1iqarg6 ,_8id84wrk * _txq1gp7u )))
		{
			//* 
			//*        Use a zero shift to avoid loss of relative accuracy 
			//* 
			
			_e8rfnip7 = _d0547bi2;
		}
		else
		{
			//* 
			//*        Compute the shift from 2-by-2 block at end of matrix 
			//* 
			
			if (_2d4fuxb5 == (int)1)
			{
				
				_6ggpocik = ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_jnnmt81a - 1)) );
				_2ul9qzme(ref Unsafe.AsRef(*(_plfm7z8g+(_ev4xhht5 - (int)1 - 1))) ,ref Unsafe.AsRef(*(_864fslqq+(_ev4xhht5 - (int)1 - 1))) ,ref Unsafe.AsRef(*(_plfm7z8g+(_ev4xhht5 - 1))) ,ref _e8rfnip7 ,ref _q2vwp05i );
			}
			else
			{
				
				_6ggpocik = ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_ev4xhht5 - 1)) );
				_2ul9qzme(ref Unsafe.AsRef(*(_plfm7z8g+(_jnnmt81a - 1))) ,ref Unsafe.AsRef(*(_864fslqq+(_jnnmt81a - 1))) ,ref Unsafe.AsRef(*(_plfm7z8g+(_jnnmt81a + (int)1 - 1))) ,ref _e8rfnip7 ,ref _q2vwp05i );
			}
			//* 
			//*        Test if shift negligible, and if so set to zero 
			//* 
			
			if (_6ggpocik > _d0547bi2)
			{
				
				if (__POW2((_e8rfnip7 / _6ggpocik)) < _p1iqarg6)
				_e8rfnip7 = _d0547bi2;
			}
			
		}
		//* 
		//*     Increment iteration count 
		//* 
		
		_em7fbywm = ((_em7fbywm + _ev4xhht5) - _jnnmt81a);//* 
		//*     If SHIFT = 0, do simplified QR iteration 
		//* 
		
		if (_e8rfnip7 == _d0547bi2)
		{
			
			if (_2d4fuxb5 == (int)1)
			{
				//* 
				//*           Chase bulge from top to bottom 
				//*           Save cosines and sines for later singular vector updates 
				//* 
				
				_82tpdhyl = _kxg5drh2;
				_5g3j4div = _kxg5drh2;
				{
					System.Int32 __81fgg2dlsvn1377 = (System.Int32)(_jnnmt81a);
					const System.Int32 __81fgg2step1377 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1377;
					for (__81fgg2count1377 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5 - (int)1) - __81fgg2dlsvn1377 + __81fgg2step1377) / __81fgg2step1377)), _b5p6od9s = __81fgg2dlsvn1377; __81fgg2count1377 != 0; __81fgg2count1377--, _b5p6od9s += (__81fgg2step1377)) {

					{
						
						_uf57gsrz(ref Unsafe.AsRef(*(_plfm7z8g+(_b5p6od9s - 1)) * _82tpdhyl) ,ref Unsafe.AsRef(*(_864fslqq+(_b5p6od9s - 1))) ,ref _82tpdhyl ,ref _8tmd0ner ,ref _q2vwp05i );
						if (_b5p6od9s > _jnnmt81a)
						*(_864fslqq+(_b5p6od9s - (int)1 - 1)) = (_6oypowww * _q2vwp05i);
						_uf57gsrz(ref Unsafe.AsRef(_5g3j4div * _q2vwp05i) ,ref Unsafe.AsRef(*(_plfm7z8g+(_b5p6od9s + (int)1 - 1)) * _8tmd0ner) ,ref _5g3j4div ,ref _6oypowww ,ref Unsafe.AsRef(*(_plfm7z8g+(_b5p6od9s - 1))) );
						*(_dqanbbw3+((_b5p6od9s - _jnnmt81a) + (int)1 - 1)) = _82tpdhyl;
						*(_dqanbbw3+(((_b5p6od9s - _jnnmt81a) + (int)1) + _3xbv3idt - 1)) = _8tmd0ner;
						*(_dqanbbw3+(((_b5p6od9s - _jnnmt81a) + (int)1) + _y3zmt0e3 - 1)) = _5g3j4div;
						*(_dqanbbw3+(((_b5p6od9s - _jnnmt81a) + (int)1) + _nowdo328 - 1)) = _6oypowww;
Mark120:;
						// continue
					}
										}				}
				_ogkjl6gu = (*(_plfm7z8g+(_ev4xhht5 - 1)) * _82tpdhyl);
				*(_plfm7z8g+(_ev4xhht5 - 1)) = (_ogkjl6gu * _5g3j4div);
				*(_864fslqq+(_ev4xhht5 - (int)1 - 1)) = (_ogkjl6gu * _6oypowww);//* 
				//*           Update singular vectors 
				//* 
				
				if (_0v6rtqiq > (int)0)
				_40qhbg49("L" ,"V" ,"F" ,ref Unsafe.AsRef((_ev4xhht5 - _jnnmt81a) + (int)1) ,ref _0v6rtqiq ,(_dqanbbw3+((int)1 - 1)),(_dqanbbw3+(_dxpq0xkr - 1)),(_xdbczr8u+(_jnnmt81a - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv );
				if (_tg62ial7 > (int)0)
				_40qhbg49("R" ,"V" ,"F" ,ref _tg62ial7 ,ref Unsafe.AsRef((_ev4xhht5 - _jnnmt81a) + (int)1) ,(_dqanbbw3+(_y3zmt0e3 + (int)1 - 1)),(_dqanbbw3+(_nowdo328 + (int)1 - 1)),(_7u55mqkq+((int)1 - 1) + (_jnnmt81a - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b );
				if (_bcsi4mx0 > (int)0)
				_40qhbg49("L" ,"V" ,"F" ,ref Unsafe.AsRef((_ev4xhht5 - _jnnmt81a) + (int)1) ,ref _bcsi4mx0 ,(_dqanbbw3+(_y3zmt0e3 + (int)1 - 1)),(_dqanbbw3+(_nowdo328 + (int)1 - 1)),(_3crf0qn3+(_jnnmt81a - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 );//* 
				//*           Test convergence 
				//* 
				
				if (ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_ev4xhht5 - (int)1 - 1)) ) <= _j0ty6ytl)
				*(_864fslqq+(_ev4xhht5 - (int)1 - 1)) = _d0547bi2;//* 
				
			}
			else
			{
				//* 
				//*           Chase bulge from bottom to top 
				//*           Save cosines and sines for later singular vector updates 
				//* 
				
				_82tpdhyl = _kxg5drh2;
				_5g3j4div = _kxg5drh2;
				{
					System.Int32 __81fgg2dlsvn1378 = (System.Int32)(_ev4xhht5);
					System.Int32 __81fgg2step1378 = (System.Int32)((int)-1);
					System.Int32 __81fgg2count1378;
					for (__81fgg2count1378 = System.Math.Max(0, (System.Int32)(((System.Int32)(_jnnmt81a + (int)1) - __81fgg2dlsvn1378 + __81fgg2step1378) / __81fgg2step1378)), _b5p6od9s = __81fgg2dlsvn1378; __81fgg2count1378 != 0; __81fgg2count1378--, _b5p6od9s += (__81fgg2step1378)) {

					{
						
						_uf57gsrz(ref Unsafe.AsRef(*(_plfm7z8g+(_b5p6od9s - 1)) * _82tpdhyl) ,ref Unsafe.AsRef(*(_864fslqq+(_b5p6od9s - (int)1 - 1))) ,ref _82tpdhyl ,ref _8tmd0ner ,ref _q2vwp05i );
						if (_b5p6od9s < _ev4xhht5)
						*(_864fslqq+(_b5p6od9s - 1)) = (_6oypowww * _q2vwp05i);
						_uf57gsrz(ref Unsafe.AsRef(_5g3j4div * _q2vwp05i) ,ref Unsafe.AsRef(*(_plfm7z8g+(_b5p6od9s - (int)1 - 1)) * _8tmd0ner) ,ref _5g3j4div ,ref _6oypowww ,ref Unsafe.AsRef(*(_plfm7z8g+(_b5p6od9s - 1))) );
						*(_dqanbbw3+(_b5p6od9s - _jnnmt81a - 1)) = _82tpdhyl;
						*(_dqanbbw3+((_b5p6od9s - _jnnmt81a) + _3xbv3idt - 1)) = (-(_8tmd0ner));
						*(_dqanbbw3+((_b5p6od9s - _jnnmt81a) + _y3zmt0e3 - 1)) = _5g3j4div;
						*(_dqanbbw3+((_b5p6od9s - _jnnmt81a) + _nowdo328 - 1)) = (-(_6oypowww));
Mark130:;
						// continue
					}
										}				}
				_ogkjl6gu = (*(_plfm7z8g+(_jnnmt81a - 1)) * _82tpdhyl);
				*(_plfm7z8g+(_jnnmt81a - 1)) = (_ogkjl6gu * _5g3j4div);
				*(_864fslqq+(_jnnmt81a - 1)) = (_ogkjl6gu * _6oypowww);//* 
				//*           Update singular vectors 
				//* 
				
				if (_0v6rtqiq > (int)0)
				_40qhbg49("L" ,"V" ,"B" ,ref Unsafe.AsRef((_ev4xhht5 - _jnnmt81a) + (int)1) ,ref _0v6rtqiq ,(_dqanbbw3+(_y3zmt0e3 + (int)1 - 1)),(_dqanbbw3+(_nowdo328 + (int)1 - 1)),(_xdbczr8u+(_jnnmt81a - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv );
				if (_tg62ial7 > (int)0)
				_40qhbg49("R" ,"V" ,"B" ,ref _tg62ial7 ,ref Unsafe.AsRef((_ev4xhht5 - _jnnmt81a) + (int)1) ,(_dqanbbw3+((int)1 - 1)),(_dqanbbw3+(_dxpq0xkr - 1)),(_7u55mqkq+((int)1 - 1) + (_jnnmt81a - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b );
				if (_bcsi4mx0 > (int)0)
				_40qhbg49("L" ,"V" ,"B" ,ref Unsafe.AsRef((_ev4xhht5 - _jnnmt81a) + (int)1) ,ref _bcsi4mx0 ,(_dqanbbw3+((int)1 - 1)),(_dqanbbw3+(_dxpq0xkr - 1)),(_3crf0qn3+(_jnnmt81a - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 );//* 
				//*           Test convergence 
				//* 
				
				if (ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_jnnmt81a - 1)) ) <= _j0ty6ytl)
				*(_864fslqq+(_jnnmt81a - 1)) = _d0547bi2;
			}
			
		}
		else
		{
			//* 
			//*        Use nonzero shift 
			//* 
			
			if (_2d4fuxb5 == (int)1)
			{
				//* 
				//*           Chase bulge from top to bottom 
				//*           Save cosines and sines for later singular vector updates 
				//* 
				
				_8plnuphw = ((ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_jnnmt81a - 1)) ) - _e8rfnip7) * (ILNumerics.F2NET.Intrinsics.SIGN(_kxg5drh2 ,*(_plfm7z8g+(_jnnmt81a - 1)) ) + (_e8rfnip7 / *(_plfm7z8g+(_jnnmt81a - 1)))));
				_mu73se41 = *(_864fslqq+(_jnnmt81a - 1));
				{
					System.Int32 __81fgg2dlsvn1379 = (System.Int32)(_jnnmt81a);
					const System.Int32 __81fgg2step1379 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1379;
					for (__81fgg2count1379 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5 - (int)1) - __81fgg2dlsvn1379 + __81fgg2step1379) / __81fgg2step1379)), _b5p6od9s = __81fgg2dlsvn1379; __81fgg2count1379 != 0; __81fgg2count1379--, _b5p6od9s += (__81fgg2step1379)) {

					{
						
						_uf57gsrz(ref _8plnuphw ,ref _mu73se41 ,ref _7sb5moji ,ref _jp87z33i ,ref _q2vwp05i );
						if (_b5p6od9s > _jnnmt81a)
						*(_864fslqq+(_b5p6od9s - (int)1 - 1)) = _q2vwp05i;
						_8plnuphw = ((_7sb5moji * *(_plfm7z8g+(_b5p6od9s - 1))) + (_jp87z33i * *(_864fslqq+(_b5p6od9s - 1))));
						*(_864fslqq+(_b5p6od9s - 1)) = ((_7sb5moji * *(_864fslqq+(_b5p6od9s - 1))) - (_jp87z33i * *(_plfm7z8g+(_b5p6od9s - 1))));
						_mu73se41 = (_jp87z33i * *(_plfm7z8g+(_b5p6od9s + (int)1 - 1)));
						*(_plfm7z8g+(_b5p6od9s + (int)1 - 1)) = (_7sb5moji * *(_plfm7z8g+(_b5p6od9s + (int)1 - 1)));
						_uf57gsrz(ref _8plnuphw ,ref _mu73se41 ,ref _bf18ryhs ,ref _ndcl48x4 ,ref _q2vwp05i );
						*(_plfm7z8g+(_b5p6od9s - 1)) = _q2vwp05i;
						_8plnuphw = ((_bf18ryhs * *(_864fslqq+(_b5p6od9s - 1))) + (_ndcl48x4 * *(_plfm7z8g+(_b5p6od9s + (int)1 - 1))));
						*(_plfm7z8g+(_b5p6od9s + (int)1 - 1)) = ((_bf18ryhs * *(_plfm7z8g+(_b5p6od9s + (int)1 - 1))) - (_ndcl48x4 * *(_864fslqq+(_b5p6od9s - 1))));
						if (_b5p6od9s < (_ev4xhht5 - (int)1))
						{
							
							_mu73se41 = (_ndcl48x4 * *(_864fslqq+(_b5p6od9s + (int)1 - 1)));
							*(_864fslqq+(_b5p6od9s + (int)1 - 1)) = (_bf18ryhs * *(_864fslqq+(_b5p6od9s + (int)1 - 1)));
						}
						
						*(_dqanbbw3+((_b5p6od9s - _jnnmt81a) + (int)1 - 1)) = _7sb5moji;
						*(_dqanbbw3+(((_b5p6od9s - _jnnmt81a) + (int)1) + _3xbv3idt - 1)) = _jp87z33i;
						*(_dqanbbw3+(((_b5p6od9s - _jnnmt81a) + (int)1) + _y3zmt0e3 - 1)) = _bf18ryhs;
						*(_dqanbbw3+(((_b5p6od9s - _jnnmt81a) + (int)1) + _nowdo328 - 1)) = _ndcl48x4;
Mark140:;
						// continue
					}
										}				}
				*(_864fslqq+(_ev4xhht5 - (int)1 - 1)) = _8plnuphw;//* 
				//*           Update singular vectors 
				//* 
				
				if (_0v6rtqiq > (int)0)
				_40qhbg49("L" ,"V" ,"F" ,ref Unsafe.AsRef((_ev4xhht5 - _jnnmt81a) + (int)1) ,ref _0v6rtqiq ,(_dqanbbw3+((int)1 - 1)),(_dqanbbw3+(_dxpq0xkr - 1)),(_xdbczr8u+(_jnnmt81a - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv );
				if (_tg62ial7 > (int)0)
				_40qhbg49("R" ,"V" ,"F" ,ref _tg62ial7 ,ref Unsafe.AsRef((_ev4xhht5 - _jnnmt81a) + (int)1) ,(_dqanbbw3+(_y3zmt0e3 + (int)1 - 1)),(_dqanbbw3+(_nowdo328 + (int)1 - 1)),(_7u55mqkq+((int)1 - 1) + (_jnnmt81a - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b );
				if (_bcsi4mx0 > (int)0)
				_40qhbg49("L" ,"V" ,"F" ,ref Unsafe.AsRef((_ev4xhht5 - _jnnmt81a) + (int)1) ,ref _bcsi4mx0 ,(_dqanbbw3+(_y3zmt0e3 + (int)1 - 1)),(_dqanbbw3+(_nowdo328 + (int)1 - 1)),(_3crf0qn3+(_jnnmt81a - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 );//* 
				//*           Test convergence 
				//* 
				
				if (ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_ev4xhht5 - (int)1 - 1)) ) <= _j0ty6ytl)
				*(_864fslqq+(_ev4xhht5 - (int)1 - 1)) = _d0547bi2;//* 
				
			}
			else
			{
				//* 
				//*           Chase bulge from bottom to top 
				//*           Save cosines and sines for later singular vector updates 
				//* 
				
				_8plnuphw = ((ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_ev4xhht5 - 1)) ) - _e8rfnip7) * (ILNumerics.F2NET.Intrinsics.SIGN(_kxg5drh2 ,*(_plfm7z8g+(_ev4xhht5 - 1)) ) + (_e8rfnip7 / *(_plfm7z8g+(_ev4xhht5 - 1)))));
				_mu73se41 = *(_864fslqq+(_ev4xhht5 - (int)1 - 1));
				{
					System.Int32 __81fgg2dlsvn1380 = (System.Int32)(_ev4xhht5);
					System.Int32 __81fgg2step1380 = (System.Int32)((int)-1);
					System.Int32 __81fgg2count1380;
					for (__81fgg2count1380 = System.Math.Max(0, (System.Int32)(((System.Int32)(_jnnmt81a + (int)1) - __81fgg2dlsvn1380 + __81fgg2step1380) / __81fgg2step1380)), _b5p6od9s = __81fgg2dlsvn1380; __81fgg2count1380 != 0; __81fgg2count1380--, _b5p6od9s += (__81fgg2step1380)) {

					{
						
						_uf57gsrz(ref _8plnuphw ,ref _mu73se41 ,ref _7sb5moji ,ref _jp87z33i ,ref _q2vwp05i );
						if (_b5p6od9s < _ev4xhht5)
						*(_864fslqq+(_b5p6od9s - 1)) = _q2vwp05i;
						_8plnuphw = ((_7sb5moji * *(_plfm7z8g+(_b5p6od9s - 1))) + (_jp87z33i * *(_864fslqq+(_b5p6od9s - (int)1 - 1))));
						*(_864fslqq+(_b5p6od9s - (int)1 - 1)) = ((_7sb5moji * *(_864fslqq+(_b5p6od9s - (int)1 - 1))) - (_jp87z33i * *(_plfm7z8g+(_b5p6od9s - 1))));
						_mu73se41 = (_jp87z33i * *(_plfm7z8g+(_b5p6od9s - (int)1 - 1)));
						*(_plfm7z8g+(_b5p6od9s - (int)1 - 1)) = (_7sb5moji * *(_plfm7z8g+(_b5p6od9s - (int)1 - 1)));
						_uf57gsrz(ref _8plnuphw ,ref _mu73se41 ,ref _bf18ryhs ,ref _ndcl48x4 ,ref _q2vwp05i );
						*(_plfm7z8g+(_b5p6od9s - 1)) = _q2vwp05i;
						_8plnuphw = ((_bf18ryhs * *(_864fslqq+(_b5p6od9s - (int)1 - 1))) + (_ndcl48x4 * *(_plfm7z8g+(_b5p6od9s - (int)1 - 1))));
						*(_plfm7z8g+(_b5p6od9s - (int)1 - 1)) = ((_bf18ryhs * *(_plfm7z8g+(_b5p6od9s - (int)1 - 1))) - (_ndcl48x4 * *(_864fslqq+(_b5p6od9s - (int)1 - 1))));
						if (_b5p6od9s > (_jnnmt81a + (int)1))
						{
							
							_mu73se41 = (_ndcl48x4 * *(_864fslqq+(_b5p6od9s - (int)2 - 1)));
							*(_864fslqq+(_b5p6od9s - (int)2 - 1)) = (_bf18ryhs * *(_864fslqq+(_b5p6od9s - (int)2 - 1)));
						}
						
						*(_dqanbbw3+(_b5p6od9s - _jnnmt81a - 1)) = _7sb5moji;
						*(_dqanbbw3+((_b5p6od9s - _jnnmt81a) + _3xbv3idt - 1)) = (-(_jp87z33i));
						*(_dqanbbw3+((_b5p6od9s - _jnnmt81a) + _y3zmt0e3 - 1)) = _bf18ryhs;
						*(_dqanbbw3+((_b5p6od9s - _jnnmt81a) + _nowdo328 - 1)) = (-(_ndcl48x4));
Mark150:;
						// continue
					}
										}				}
				*(_864fslqq+(_jnnmt81a - 1)) = _8plnuphw;//* 
				//*           Test convergence 
				//* 
				
				if (ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_jnnmt81a - 1)) ) <= _j0ty6ytl)
				*(_864fslqq+(_jnnmt81a - 1)) = _d0547bi2;//* 
				//*           Update singular vectors if desired 
				//* 
				
				if (_0v6rtqiq > (int)0)
				_40qhbg49("L" ,"V" ,"B" ,ref Unsafe.AsRef((_ev4xhht5 - _jnnmt81a) + (int)1) ,ref _0v6rtqiq ,(_dqanbbw3+(_y3zmt0e3 + (int)1 - 1)),(_dqanbbw3+(_nowdo328 + (int)1 - 1)),(_xdbczr8u+(_jnnmt81a - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv );
				if (_tg62ial7 > (int)0)
				_40qhbg49("R" ,"V" ,"B" ,ref _tg62ial7 ,ref Unsafe.AsRef((_ev4xhht5 - _jnnmt81a) + (int)1) ,(_dqanbbw3+((int)1 - 1)),(_dqanbbw3+(_dxpq0xkr - 1)),(_7u55mqkq+((int)1 - 1) + (_jnnmt81a - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b );
				if (_bcsi4mx0 > (int)0)
				_40qhbg49("L" ,"V" ,"B" ,ref Unsafe.AsRef((_ev4xhht5 - _jnnmt81a) + (int)1) ,ref _bcsi4mx0 ,(_dqanbbw3+((int)1 - 1)),(_dqanbbw3+(_dxpq0xkr - 1)),(_3crf0qn3+(_jnnmt81a - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 );
			}
			
		}
		//* 
		//*     QR iteration finished, go back and check convergence 
		//* 
		goto Mark60;//* 
		//*     All singular values converged, so make them positive 
		//* 
		
Mark160:;
		// continue
		{
			System.Int32 __81fgg2dlsvn1381 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1381 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1381;
			for (__81fgg2count1381 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1381 + __81fgg2step1381) / __81fgg2step1381)), _b5p6od9s = __81fgg2dlsvn1381; __81fgg2count1381 != 0; __81fgg2count1381--, _b5p6od9s += (__81fgg2step1381)) {

			{
				
				if (*(_plfm7z8g+(_b5p6od9s - 1)) < _d0547bi2)
				{
					
					*(_plfm7z8g+(_b5p6od9s - 1)) = (-(*(_plfm7z8g+(_b5p6od9s - 1))));//* 
					//*           Change sign of singular vectors, if desired 
					//* 
					
					if (_0v6rtqiq > (int)0)
					_2ylagitj(ref _0v6rtqiq ,ref Unsafe.AsRef(_2v8pp9oq) ,(_xdbczr8u+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv );
				}
				
Mark170:;
				// continue
			}
						}		}//* 
		//*     Sort the singular values into decreasing order (insertion sort on 
		//*     singular values, but only one transposition per singular vector) 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn1382 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1382 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1382;
			for (__81fgg2count1382 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn1382 + __81fgg2step1382) / __81fgg2step1382)), _b5p6od9s = __81fgg2dlsvn1382; __81fgg2count1382 != 0; __81fgg2count1382--, _b5p6od9s += (__81fgg2step1382)) {

			{
				//* 
				//*        Scan for smallest D(I) 
				//* 
				
				_ndl0tuhx = (int)1;
				_rhnpgpoi = *(_plfm7z8g+((int)1 - 1));
				{
					System.Int32 __81fgg2dlsvn1383 = (System.Int32)((int)2);
					const System.Int32 __81fgg2step1383 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1383;
					for (__81fgg2count1383 = System.Math.Max(0, (System.Int32)(((System.Int32)((_dxpq0xkr + (int)1) - _b5p6od9s) - __81fgg2dlsvn1383 + __81fgg2step1383) / __81fgg2step1383)), _znpjgsef = __81fgg2dlsvn1383; __81fgg2count1383 != 0; __81fgg2count1383--, _znpjgsef += (__81fgg2step1383)) {

					{
						
						if (*(_plfm7z8g+(_znpjgsef - 1)) <= _rhnpgpoi)
						{
							
							_ndl0tuhx = _znpjgsef;
							_rhnpgpoi = *(_plfm7z8g+(_znpjgsef - 1));
						}
						
Mark180:;
						// continue
					}
										}				}
				if (_ndl0tuhx != ((_dxpq0xkr + (int)1) - _b5p6od9s))
				{
					//* 
					//*           Swap singular values and vectors 
					//* 
					
					*(_plfm7z8g+(_ndl0tuhx - 1)) = *(_plfm7z8g+((_dxpq0xkr + (int)1) - _b5p6od9s - 1));
					*(_plfm7z8g+((_dxpq0xkr + (int)1) - _b5p6od9s - 1)) = _rhnpgpoi;
					if (_0v6rtqiq > (int)0)
					_1frbwlh0(ref _0v6rtqiq ,(_xdbczr8u+(_ndl0tuhx - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv ,(_xdbczr8u+((_dxpq0xkr + (int)1) - _b5p6od9s - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv );
					if (_tg62ial7 > (int)0)
					_1frbwlh0(ref _tg62ial7 ,(_7u55mqkq+((int)1 - 1) + (_ndl0tuhx - 1) * 1 * (_u6e6d39b)),ref Unsafe.AsRef((int)1) ,(_7u55mqkq+((int)1 - 1) + ((_dxpq0xkr + (int)1) - _b5p6od9s - 1) * 1 * (_u6e6d39b)),ref Unsafe.AsRef((int)1) );
					if (_bcsi4mx0 > (int)0)
					_1frbwlh0(ref _bcsi4mx0 ,(_3crf0qn3+(_ndl0tuhx - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_3crf0qn3+((_dxpq0xkr + (int)1) - _b5p6od9s - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 );
				}
				
Mark190:;
				// continue
			}
						}		}goto Mark220;//* 
		//*     Maximum number of iterations exceeded, failure to converge 
		//* 
		
Mark200:;
		// continue
		_gro5yvfo = (int)0;
		{
			System.Int32 __81fgg2dlsvn1384 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1384 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1384;
			for (__81fgg2count1384 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn1384 + __81fgg2step1384) / __81fgg2step1384)), _b5p6od9s = __81fgg2dlsvn1384; __81fgg2count1384 != 0; __81fgg2count1384--, _b5p6od9s += (__81fgg2step1384)) {

			{
				
				if (*(_864fslqq+(_b5p6od9s - 1)) != _d0547bi2)
				_gro5yvfo = (_gro5yvfo + (int)1);
Mark210:;
				// continue
			}
						}		}
Mark220:;
		// continue
		return;//* 
		//*     End of CBDSQR 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
