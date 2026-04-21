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
//*> \brief \b DBDSQR 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DBDSQR + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dbdsqr.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dbdsqr.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dbdsqr.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DBDSQR( UPLO, N, NCVT, NRU, NCC, D, E, VT, LDVT, U, 
//*                          LDU, C, LDC, WORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          UPLO 
//*       INTEGER            INFO, LDC, LDU, LDVT, N, NCC, NCVT, NRU 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   C( LDC, * ), D( * ), E( * ), U( LDU, * ), 
//*      $                   VT( LDVT, * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DBDSQR computes the singular values and, optionally, the right and/or 
//*> left singular vectors from the singular value decomposition (SVD) of 
//*> a real N-by-N (upper or lower) bidiagonal matrix B using the implicit 
//*> zero-shift QR algorithm.  The SVD of B has the form 
//*> 
//*>    B = Q * S * P**T 
//*> 
//*> where S is the diagonal matrix of singular values, Q is an orthogonal 
//*> matrix of left singular vectors, and P is an orthogonal matrix of 
//*> right singular vectors.  If left singular vectors are requested, this 
//*> subroutine actually returns U*Q instead of Q, and, if right singular 
//*> vectors are requested, this subroutine returns P**T*VT instead of 
//*> P**T, for given real input matrices U and VT.  When U and VT are the 
//*> orthogonal matrices that reduce a general matrix A to bidiagonal 
//*> form:  A = U*B*VT, as computed by DGEBRD, then 
//*> 
//*>    A = (U*Q) * S * (P**T*VT) 
//*> 
//*> is the SVD of A.  Optionally, the subroutine may also compute Q**T*C 
//*> for a given real input matrix C. 
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
//*>          D is DOUBLE PRECISION array, dimension (N) 
//*>          On entry, the n diagonal elements of the bidiagonal matrix B. 
//*>          On exit, if INFO=0, the singular values of B in decreasing 
//*>          order. 
//*> \endverbatim 
//*> 
//*> \param[in,out] E 
//*> \verbatim 
//*>          E is DOUBLE PRECISION array, dimension (N-1) 
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
//*>          VT is DOUBLE PRECISION array, dimension (LDVT, NCVT) 
//*>          On entry, an N-by-NCVT matrix VT. 
//*>          On exit, VT is overwritten by P**T * VT. 
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
//*>          U is DOUBLE PRECISION array, dimension (LDU, N) 
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
//*>          C is DOUBLE PRECISION array, dimension (LDC, NCC) 
//*>          On entry, an N-by-NCC matrix C. 
//*>          On exit, C is overwritten by Q**T * C. 
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
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is DOUBLE PRECISION array, dimension (4*(N-1)) 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit 
//*>          < 0:  If INFO = -i, the i-th argument had an illegal value 
//*>          > 0: 
//*>             if NCVT = NRU = NCC = 0, 
//*>                = 1, a split was marked by a positive value in E 
//*>                = 2, current block of Z not diagonalized after 30*N 
//*>                     iterations (in inner while loop) 
//*>                = 3, termination criterion of outer while loop not met 
//*>                     (program created more than N unreduced blocks) 
//*>             else NCVT = NRU = NCC = 0, 
//*>                   the algorithm did not converge; D and E contain the 
//*>                   elements of a bidiagonal matrix which is orthogonally 
//*>                   similar to the input matrix B;  if INFO = i, i 
//*>                   elements of E have not converged to zero. 
//*> \endverbatim 
//* 
//*> \par Internal Parameters: 
//*  ========================= 
//*> 
//*> \verbatim 
//*>  TOLMUL  DOUBLE PRECISION, default = max(10,min(100,EPS**(-1/8))) 
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
//*> 
//*> \endverbatim 
//* 
//*> \par Note: 
//*  =========== 
//*> 
//*> \verbatim 
//*>  Bug report from Cezary Dendek. 
//*>  On March 23rd 2017, the INTEGER variable MAXIT = MAXITR*N**2 is 
//*>  removed since it can overflow pretty easily (for N larger or equal 
//*>  than 18,919). We instead use MAXITDIVN = MAXITR*N. 
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
//*> \date June 2017 
//* 
//*> \ingroup auxOTHERcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _nvdqer79(FString _9wyre9zc, ref Int32 _dxpq0xkr, ref Int32 _0v6rtqiq, ref Int32 _tg62ial7, ref Int32 _bcsi4mx0, Double* _plfm7z8g, Double* _864fslqq, Double* _xdbczr8u, ref Int32 _h4ibbatv, Double* _7u55mqkq, ref Int32 _u6e6d39b, Double* _3crf0qn3, ref Int32 _1s3eymp4, Double* _apig8meb, ref Int32 _gro5yvfo)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Double _2v8pp9oq =  -1d;
Double _8id84wrk =  0.01d;
Double _76g572q1 =  10d;
Double _cu7spn6z =  100d;
Double _qpjmg2qj =  -0.125d;
Int32 _to3hwp46 =  (int)6;
Boolean _58l4so5d =  default;
Boolean _77utirhk =  default;
Int32 _b5p6od9s =  default;
Int32 _2d4fuxb5 =  default;
Int32 _ndl0tuhx =  default;
Int32 _em7fbywm =  default;
Int32 _zcnwr639 =  default;
Int32 _znpjgsef =  default;
Int32 _jnnmt81a =  default;
Int32 _zo9zhv2g =  default;
Int32 _ev4xhht5 =  default;
Int32 _24d6kw2x =  default;
Int32 _3xbv3idt =  default;
Int32 _y3zmt0e3 =  default;
Int32 _nowdo328 =  default;
Int32 _emfdpg6h =  default;
Int32 _kyn0ufud =  default;
Double _k08xhqq9 =  default;
Double _kgl39y18 =  default;
Double _bf18ryhs =  default;
Double _7sb5moji =  default;
Double _82tpdhyl =  default;
Double _p1iqarg6 =  default;
Double _8plnuphw =  default;
Double _mu73se41 =  default;
Double _ogkjl6gu =  default;
Double _4s3y5e1x =  default;
Double _5g3j4div =  default;
Double _6oypowww =  default;
Double _q2vwp05i =  default;
Double _e8rfnip7 =  default;
Double _sq0ypuw6 =  default;
Double _7d8q5cd7 =  default;
Double _ndcl48x4 =  default;
Double _jp87z33i =  default;
Double _6ggpocik =  default;
Double _t7m1e103 =  default;
Double _rhnpgpoi =  default;
Double _l1bltk1u =  default;
Double _25o9vyhd =  default;
Double _8tmd0ner =  default;
Double _j0ty6ytl =  default;
Double _txq1gp7u =  default;
Double _4117v60s =  default;
Double _zfw72syv =  default;
string fLanavab = default;
#endregion  variable declarations
_9wyre9zc = _9wyre9zc.Convert(1);

	{
		//* 
		//*  -- LAPACK computational routine (version 3.7.1) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     June 2017 
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
			
			_ut9qalzx("DBDSQR" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
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
			
			_ovwsxjz5(ref _dxpq0xkr ,_plfm7z8g ,_864fslqq ,_apig8meb ,ref _gro5yvfo );//* 
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
		
		_p1iqarg6 = _f43eg0w0("Epsilon" );
		_zfw72syv = _f43eg0w0("Safe minimum" );//* 
		//*     If matrix lower bidiagonal, rotate to be upper bidiagonal 
		//*     by applying Givens rotations on the left 
		//* 
		
		if (_58l4so5d)
		{
			
			{
				System.Int32 __81fgg2dlsvn281 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step281 = (System.Int32)((int)1);
				System.Int32 __81fgg2count281;
				for (__81fgg2count281 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn281 + __81fgg2step281) / __81fgg2step281)), _b5p6od9s = __81fgg2dlsvn281; __81fgg2count281 != 0; __81fgg2count281--, _b5p6od9s += (__81fgg2step281)) {

				{
					
					_uasfzoa5(ref Unsafe.AsRef(*(_plfm7z8g+(_b5p6od9s - 1))) ,ref Unsafe.AsRef(*(_864fslqq+(_b5p6od9s - 1))) ,ref _82tpdhyl ,ref _8tmd0ner ,ref _q2vwp05i );
					*(_plfm7z8g+(_b5p6od9s - 1)) = _q2vwp05i;
					*(_864fslqq+(_b5p6od9s - 1)) = (_8tmd0ner * *(_plfm7z8g+(_b5p6od9s + (int)1 - 1)));
					*(_plfm7z8g+(_b5p6od9s + (int)1 - 1)) = (_82tpdhyl * *(_plfm7z8g+(_b5p6od9s + (int)1 - 1)));
					*(_apig8meb+(_b5p6od9s - 1)) = _82tpdhyl;
					*(_apig8meb+(_3xbv3idt + _b5p6od9s - 1)) = _8tmd0ner;
Mark10:;
					// continue
				}
								}			}//* 
			//*        Update singular vectors if desired 
			//* 
			
			if (_tg62ial7 > (int)0)
			_sg7u7241("R" ,"V" ,"F" ,ref _tg62ial7 ,ref _dxpq0xkr ,(_apig8meb+((int)1 - 1)),(_apig8meb+(_dxpq0xkr - 1)),_7u55mqkq ,ref _u6e6d39b );
			if (_bcsi4mx0 > (int)0)
			_sg7u7241("L" ,"V" ,"F" ,ref _dxpq0xkr ,ref _bcsi4mx0 ,(_apig8meb+((int)1 - 1)),(_apig8meb+(_dxpq0xkr - 1)),_3crf0qn3 ,ref _1s3eymp4 );
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
			System.Int32 __81fgg2dlsvn282 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step282 = (System.Int32)((int)1);
			System.Int32 __81fgg2count282;
			for (__81fgg2count282 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn282 + __81fgg2step282) / __81fgg2step282)), _b5p6od9s = __81fgg2dlsvn282; __81fgg2count282 != 0; __81fgg2count282--, _b5p6od9s += (__81fgg2step282)) {

			{
				
				_t7m1e103 = ILNumerics.F2NET.Intrinsics.MAX(_t7m1e103 ,ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_b5p6od9s - 1)) ) );
Mark20:;
				// continue
			}
						}		}
		{
			System.Int32 __81fgg2dlsvn283 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step283 = (System.Int32)((int)1);
			System.Int32 __81fgg2count283;
			for (__81fgg2count283 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn283 + __81fgg2step283) / __81fgg2step283)), _b5p6od9s = __81fgg2dlsvn283; __81fgg2count283 != 0; __81fgg2count283--, _b5p6od9s += (__81fgg2step283)) {

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
				System.Int32 __81fgg2dlsvn284 = (System.Int32)((int)2);
				const System.Int32 __81fgg2step284 = (System.Int32)((int)1);
				System.Int32 __81fgg2count284;
				for (__81fgg2count284 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn284 + __81fgg2step284) / __81fgg2step284)), _b5p6od9s = __81fgg2dlsvn284; __81fgg2count284 != 0; __81fgg2count284--, _b5p6od9s += (__81fgg2step284)) {

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
			_25o9vyhd = (_25o9vyhd / ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.DBLE(_dxpq0xkr ) ));
			_j0ty6ytl = ILNumerics.F2NET.Intrinsics.MAX(_txq1gp7u * _25o9vyhd ,_to3hwp46 * (_dxpq0xkr * (_dxpq0xkr * _zfw72syv)) );
		}
		else
		{
			//* 
			//*        Absolute accuracy desired 
			//* 
			
			_j0ty6ytl = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_txq1gp7u ) * _t7m1e103 ,_to3hwp46 * (_dxpq0xkr * (_dxpq0xkr * _zfw72syv)) );
		}
		//* 
		//*     Prepare for main iteration loop for the singular values 
		//*     (MAXIT is the maximum number of passes through the inner 
		//*     loop permitted before nonconvergence signalled.) 
		//* 
		
		_24d6kw2x = (_to3hwp46 * _dxpq0xkr);
		_zcnwr639 = (int)0;
		_em7fbywm = (int)-1;
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
		
		if (_ev4xhht5 <= (int)1)goto Mark160;//* 
		
		if (_em7fbywm >= _dxpq0xkr)
		{
			
			_em7fbywm = (_em7fbywm - _dxpq0xkr);
			_zcnwr639 = (_zcnwr639 + (int)1);
			if (_zcnwr639 >= _24d6kw2x)goto Mark200;
		}
		//* 
		//*     Find diagonal block of matrix to work on 
		//* 
		
		if ((_txq1gp7u < _d0547bi2) & (ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_ev4xhht5 - 1)) ) <= _j0ty6ytl))
		*(_plfm7z8g+(_ev4xhht5 - 1)) = _d0547bi2;
		_t7m1e103 = ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_ev4xhht5 - 1)) );
		_rhnpgpoi = _t7m1e103;
		{
			System.Int32 __81fgg2dlsvn285 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step285 = (System.Int32)((int)1);
			System.Int32 __81fgg2count285;
			for (__81fgg2count285 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5 - (int)1) - __81fgg2dlsvn285 + __81fgg2step285) / __81fgg2step285)), _zo9zhv2g = __81fgg2dlsvn285; __81fgg2count285 != 0; __81fgg2count285--, _zo9zhv2g += (__81fgg2step285)) {

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
			
			_0jrnhb6x(ref Unsafe.AsRef(*(_plfm7z8g+(_ev4xhht5 - (int)1 - 1))) ,ref Unsafe.AsRef(*(_864fslqq+(_ev4xhht5 - (int)1 - 1))) ,ref Unsafe.AsRef(*(_plfm7z8g+(_ev4xhht5 - 1))) ,ref _sq0ypuw6 ,ref _7d8q5cd7 ,ref _jp87z33i ,ref _7sb5moji ,ref _ndcl48x4 ,ref _bf18ryhs );
			*(_plfm7z8g+(_ev4xhht5 - (int)1 - 1)) = _7d8q5cd7;
			*(_864fslqq+(_ev4xhht5 - (int)1 - 1)) = _d0547bi2;
			*(_plfm7z8g+(_ev4xhht5 - 1)) = _sq0ypuw6;//* 
			//*        Compute singular vectors, if desired 
			//* 
			
			if (_0v6rtqiq > (int)0)
			_2197fa5i(ref _0v6rtqiq ,(_xdbczr8u+(_ev4xhht5 - (int)1 - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv ,(_xdbczr8u+(_ev4xhht5 - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv ,ref _7sb5moji ,ref _jp87z33i );
			if (_tg62ial7 > (int)0)
			_2197fa5i(ref _tg62ial7 ,(_7u55mqkq+((int)1 - 1) + (_ev4xhht5 - (int)1 - 1) * 1 * (_u6e6d39b)),ref Unsafe.AsRef((int)1) ,(_7u55mqkq+((int)1 - 1) + (_ev4xhht5 - 1) * 1 * (_u6e6d39b)),ref Unsafe.AsRef((int)1) ,ref _bf18ryhs ,ref _ndcl48x4 );
			if (_bcsi4mx0 > (int)0)
			_2197fa5i(ref _bcsi4mx0 ,(_3crf0qn3+(_ev4xhht5 - (int)1 - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_3crf0qn3+(_ev4xhht5 - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,ref _bf18ryhs ,ref _ndcl48x4 );
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
					System.Int32 __81fgg2dlsvn286 = (System.Int32)(_jnnmt81a);
					const System.Int32 __81fgg2step286 = (System.Int32)((int)1);
					System.Int32 __81fgg2count286;
					for (__81fgg2count286 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5 - (int)1) - __81fgg2dlsvn286 + __81fgg2step286) / __81fgg2step286)), _zo9zhv2g = __81fgg2dlsvn286; __81fgg2count286 != 0; __81fgg2count286--, _zo9zhv2g += (__81fgg2step286)) {

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
					System.Int32 __81fgg2dlsvn287 = (System.Int32)((_ev4xhht5 - (int)1));
					System.Int32 __81fgg2step287 = (System.Int32)((int)-1);
					System.Int32 __81fgg2count287;
					for (__81fgg2count287 = System.Math.Max(0, (System.Int32)(((System.Int32)(_jnnmt81a) - __81fgg2dlsvn287 + __81fgg2step287) / __81fgg2step287)), _zo9zhv2g = __81fgg2dlsvn287; __81fgg2count287 != 0; __81fgg2count287--, _zo9zhv2g += (__81fgg2step287)) {

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
				_i0q1t486(ref Unsafe.AsRef(*(_plfm7z8g+(_ev4xhht5 - (int)1 - 1))) ,ref Unsafe.AsRef(*(_864fslqq+(_ev4xhht5 - (int)1 - 1))) ,ref Unsafe.AsRef(*(_plfm7z8g+(_ev4xhht5 - 1))) ,ref _e8rfnip7 ,ref _q2vwp05i );
			}
			else
			{
				
				_6ggpocik = ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_ev4xhht5 - 1)) );
				_i0q1t486(ref Unsafe.AsRef(*(_plfm7z8g+(_jnnmt81a - 1))) ,ref Unsafe.AsRef(*(_864fslqq+(_jnnmt81a - 1))) ,ref Unsafe.AsRef(*(_plfm7z8g+(_jnnmt81a + (int)1 - 1))) ,ref _e8rfnip7 ,ref _q2vwp05i );
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
					System.Int32 __81fgg2dlsvn288 = (System.Int32)(_jnnmt81a);
					const System.Int32 __81fgg2step288 = (System.Int32)((int)1);
					System.Int32 __81fgg2count288;
					for (__81fgg2count288 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5 - (int)1) - __81fgg2dlsvn288 + __81fgg2step288) / __81fgg2step288)), _b5p6od9s = __81fgg2dlsvn288; __81fgg2count288 != 0; __81fgg2count288--, _b5p6od9s += (__81fgg2step288)) {

					{
						
						_uasfzoa5(ref Unsafe.AsRef(*(_plfm7z8g+(_b5p6od9s - 1)) * _82tpdhyl) ,ref Unsafe.AsRef(*(_864fslqq+(_b5p6od9s - 1))) ,ref _82tpdhyl ,ref _8tmd0ner ,ref _q2vwp05i );
						if (_b5p6od9s > _jnnmt81a)
						*(_864fslqq+(_b5p6od9s - (int)1 - 1)) = (_6oypowww * _q2vwp05i);
						_uasfzoa5(ref Unsafe.AsRef(_5g3j4div * _q2vwp05i) ,ref Unsafe.AsRef(*(_plfm7z8g+(_b5p6od9s + (int)1 - 1)) * _8tmd0ner) ,ref _5g3j4div ,ref _6oypowww ,ref Unsafe.AsRef(*(_plfm7z8g+(_b5p6od9s - 1))) );
						*(_apig8meb+((_b5p6od9s - _jnnmt81a) + (int)1 - 1)) = _82tpdhyl;
						*(_apig8meb+(((_b5p6od9s - _jnnmt81a) + (int)1) + _3xbv3idt - 1)) = _8tmd0ner;
						*(_apig8meb+(((_b5p6od9s - _jnnmt81a) + (int)1) + _y3zmt0e3 - 1)) = _5g3j4div;
						*(_apig8meb+(((_b5p6od9s - _jnnmt81a) + (int)1) + _nowdo328 - 1)) = _6oypowww;
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
				_sg7u7241("L" ,"V" ,"F" ,ref Unsafe.AsRef((_ev4xhht5 - _jnnmt81a) + (int)1) ,ref _0v6rtqiq ,(_apig8meb+((int)1 - 1)),(_apig8meb+(_dxpq0xkr - 1)),(_xdbczr8u+(_jnnmt81a - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv );
				if (_tg62ial7 > (int)0)
				_sg7u7241("R" ,"V" ,"F" ,ref _tg62ial7 ,ref Unsafe.AsRef((_ev4xhht5 - _jnnmt81a) + (int)1) ,(_apig8meb+(_y3zmt0e3 + (int)1 - 1)),(_apig8meb+(_nowdo328 + (int)1 - 1)),(_7u55mqkq+((int)1 - 1) + (_jnnmt81a - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b );
				if (_bcsi4mx0 > (int)0)
				_sg7u7241("L" ,"V" ,"F" ,ref Unsafe.AsRef((_ev4xhht5 - _jnnmt81a) + (int)1) ,ref _bcsi4mx0 ,(_apig8meb+(_y3zmt0e3 + (int)1 - 1)),(_apig8meb+(_nowdo328 + (int)1 - 1)),(_3crf0qn3+(_jnnmt81a - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 );//* 
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
					System.Int32 __81fgg2dlsvn289 = (System.Int32)(_ev4xhht5);
					System.Int32 __81fgg2step289 = (System.Int32)((int)-1);
					System.Int32 __81fgg2count289;
					for (__81fgg2count289 = System.Math.Max(0, (System.Int32)(((System.Int32)(_jnnmt81a + (int)1) - __81fgg2dlsvn289 + __81fgg2step289) / __81fgg2step289)), _b5p6od9s = __81fgg2dlsvn289; __81fgg2count289 != 0; __81fgg2count289--, _b5p6od9s += (__81fgg2step289)) {

					{
						
						_uasfzoa5(ref Unsafe.AsRef(*(_plfm7z8g+(_b5p6od9s - 1)) * _82tpdhyl) ,ref Unsafe.AsRef(*(_864fslqq+(_b5p6od9s - (int)1 - 1))) ,ref _82tpdhyl ,ref _8tmd0ner ,ref _q2vwp05i );
						if (_b5p6od9s < _ev4xhht5)
						*(_864fslqq+(_b5p6od9s - 1)) = (_6oypowww * _q2vwp05i);
						_uasfzoa5(ref Unsafe.AsRef(_5g3j4div * _q2vwp05i) ,ref Unsafe.AsRef(*(_plfm7z8g+(_b5p6od9s - (int)1 - 1)) * _8tmd0ner) ,ref _5g3j4div ,ref _6oypowww ,ref Unsafe.AsRef(*(_plfm7z8g+(_b5p6od9s - 1))) );
						*(_apig8meb+(_b5p6od9s - _jnnmt81a - 1)) = _82tpdhyl;
						*(_apig8meb+((_b5p6od9s - _jnnmt81a) + _3xbv3idt - 1)) = (-(_8tmd0ner));
						*(_apig8meb+((_b5p6od9s - _jnnmt81a) + _y3zmt0e3 - 1)) = _5g3j4div;
						*(_apig8meb+((_b5p6od9s - _jnnmt81a) + _nowdo328 - 1)) = (-(_6oypowww));
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
				_sg7u7241("L" ,"V" ,"B" ,ref Unsafe.AsRef((_ev4xhht5 - _jnnmt81a) + (int)1) ,ref _0v6rtqiq ,(_apig8meb+(_y3zmt0e3 + (int)1 - 1)),(_apig8meb+(_nowdo328 + (int)1 - 1)),(_xdbczr8u+(_jnnmt81a - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv );
				if (_tg62ial7 > (int)0)
				_sg7u7241("R" ,"V" ,"B" ,ref _tg62ial7 ,ref Unsafe.AsRef((_ev4xhht5 - _jnnmt81a) + (int)1) ,(_apig8meb+((int)1 - 1)),(_apig8meb+(_dxpq0xkr - 1)),(_7u55mqkq+((int)1 - 1) + (_jnnmt81a - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b );
				if (_bcsi4mx0 > (int)0)
				_sg7u7241("L" ,"V" ,"B" ,ref Unsafe.AsRef((_ev4xhht5 - _jnnmt81a) + (int)1) ,ref _bcsi4mx0 ,(_apig8meb+((int)1 - 1)),(_apig8meb+(_dxpq0xkr - 1)),(_3crf0qn3+(_jnnmt81a - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 );//* 
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
					System.Int32 __81fgg2dlsvn290 = (System.Int32)(_jnnmt81a);
					const System.Int32 __81fgg2step290 = (System.Int32)((int)1);
					System.Int32 __81fgg2count290;
					for (__81fgg2count290 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5 - (int)1) - __81fgg2dlsvn290 + __81fgg2step290) / __81fgg2step290)), _b5p6od9s = __81fgg2dlsvn290; __81fgg2count290 != 0; __81fgg2count290--, _b5p6od9s += (__81fgg2step290)) {

					{
						
						_uasfzoa5(ref _8plnuphw ,ref _mu73se41 ,ref _7sb5moji ,ref _jp87z33i ,ref _q2vwp05i );
						if (_b5p6od9s > _jnnmt81a)
						*(_864fslqq+(_b5p6od9s - (int)1 - 1)) = _q2vwp05i;
						_8plnuphw = ((_7sb5moji * *(_plfm7z8g+(_b5p6od9s - 1))) + (_jp87z33i * *(_864fslqq+(_b5p6od9s - 1))));
						*(_864fslqq+(_b5p6od9s - 1)) = ((_7sb5moji * *(_864fslqq+(_b5p6od9s - 1))) - (_jp87z33i * *(_plfm7z8g+(_b5p6od9s - 1))));
						_mu73se41 = (_jp87z33i * *(_plfm7z8g+(_b5p6od9s + (int)1 - 1)));
						*(_plfm7z8g+(_b5p6od9s + (int)1 - 1)) = (_7sb5moji * *(_plfm7z8g+(_b5p6od9s + (int)1 - 1)));
						_uasfzoa5(ref _8plnuphw ,ref _mu73se41 ,ref _bf18ryhs ,ref _ndcl48x4 ,ref _q2vwp05i );
						*(_plfm7z8g+(_b5p6od9s - 1)) = _q2vwp05i;
						_8plnuphw = ((_bf18ryhs * *(_864fslqq+(_b5p6od9s - 1))) + (_ndcl48x4 * *(_plfm7z8g+(_b5p6od9s + (int)1 - 1))));
						*(_plfm7z8g+(_b5p6od9s + (int)1 - 1)) = ((_bf18ryhs * *(_plfm7z8g+(_b5p6od9s + (int)1 - 1))) - (_ndcl48x4 * *(_864fslqq+(_b5p6od9s - 1))));
						if (_b5p6od9s < (_ev4xhht5 - (int)1))
						{
							
							_mu73se41 = (_ndcl48x4 * *(_864fslqq+(_b5p6od9s + (int)1 - 1)));
							*(_864fslqq+(_b5p6od9s + (int)1 - 1)) = (_bf18ryhs * *(_864fslqq+(_b5p6od9s + (int)1 - 1)));
						}
						
						*(_apig8meb+((_b5p6od9s - _jnnmt81a) + (int)1 - 1)) = _7sb5moji;
						*(_apig8meb+(((_b5p6od9s - _jnnmt81a) + (int)1) + _3xbv3idt - 1)) = _jp87z33i;
						*(_apig8meb+(((_b5p6od9s - _jnnmt81a) + (int)1) + _y3zmt0e3 - 1)) = _bf18ryhs;
						*(_apig8meb+(((_b5p6od9s - _jnnmt81a) + (int)1) + _nowdo328 - 1)) = _ndcl48x4;
Mark140:;
						// continue
					}
										}				}
				*(_864fslqq+(_ev4xhht5 - (int)1 - 1)) = _8plnuphw;//* 
				//*           Update singular vectors 
				//* 
				
				if (_0v6rtqiq > (int)0)
				_sg7u7241("L" ,"V" ,"F" ,ref Unsafe.AsRef((_ev4xhht5 - _jnnmt81a) + (int)1) ,ref _0v6rtqiq ,(_apig8meb+((int)1 - 1)),(_apig8meb+(_dxpq0xkr - 1)),(_xdbczr8u+(_jnnmt81a - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv );
				if (_tg62ial7 > (int)0)
				_sg7u7241("R" ,"V" ,"F" ,ref _tg62ial7 ,ref Unsafe.AsRef((_ev4xhht5 - _jnnmt81a) + (int)1) ,(_apig8meb+(_y3zmt0e3 + (int)1 - 1)),(_apig8meb+(_nowdo328 + (int)1 - 1)),(_7u55mqkq+((int)1 - 1) + (_jnnmt81a - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b );
				if (_bcsi4mx0 > (int)0)
				_sg7u7241("L" ,"V" ,"F" ,ref Unsafe.AsRef((_ev4xhht5 - _jnnmt81a) + (int)1) ,ref _bcsi4mx0 ,(_apig8meb+(_y3zmt0e3 + (int)1 - 1)),(_apig8meb+(_nowdo328 + (int)1 - 1)),(_3crf0qn3+(_jnnmt81a - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 );//* 
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
					System.Int32 __81fgg2dlsvn291 = (System.Int32)(_ev4xhht5);
					System.Int32 __81fgg2step291 = (System.Int32)((int)-1);
					System.Int32 __81fgg2count291;
					for (__81fgg2count291 = System.Math.Max(0, (System.Int32)(((System.Int32)(_jnnmt81a + (int)1) - __81fgg2dlsvn291 + __81fgg2step291) / __81fgg2step291)), _b5p6od9s = __81fgg2dlsvn291; __81fgg2count291 != 0; __81fgg2count291--, _b5p6od9s += (__81fgg2step291)) {

					{
						
						_uasfzoa5(ref _8plnuphw ,ref _mu73se41 ,ref _7sb5moji ,ref _jp87z33i ,ref _q2vwp05i );
						if (_b5p6od9s < _ev4xhht5)
						*(_864fslqq+(_b5p6od9s - 1)) = _q2vwp05i;
						_8plnuphw = ((_7sb5moji * *(_plfm7z8g+(_b5p6od9s - 1))) + (_jp87z33i * *(_864fslqq+(_b5p6od9s - (int)1 - 1))));
						*(_864fslqq+(_b5p6od9s - (int)1 - 1)) = ((_7sb5moji * *(_864fslqq+(_b5p6od9s - (int)1 - 1))) - (_jp87z33i * *(_plfm7z8g+(_b5p6od9s - 1))));
						_mu73se41 = (_jp87z33i * *(_plfm7z8g+(_b5p6od9s - (int)1 - 1)));
						*(_plfm7z8g+(_b5p6od9s - (int)1 - 1)) = (_7sb5moji * *(_plfm7z8g+(_b5p6od9s - (int)1 - 1)));
						_uasfzoa5(ref _8plnuphw ,ref _mu73se41 ,ref _bf18ryhs ,ref _ndcl48x4 ,ref _q2vwp05i );
						*(_plfm7z8g+(_b5p6od9s - 1)) = _q2vwp05i;
						_8plnuphw = ((_bf18ryhs * *(_864fslqq+(_b5p6od9s - (int)1 - 1))) + (_ndcl48x4 * *(_plfm7z8g+(_b5p6od9s - (int)1 - 1))));
						*(_plfm7z8g+(_b5p6od9s - (int)1 - 1)) = ((_bf18ryhs * *(_plfm7z8g+(_b5p6od9s - (int)1 - 1))) - (_ndcl48x4 * *(_864fslqq+(_b5p6od9s - (int)1 - 1))));
						if (_b5p6od9s > (_jnnmt81a + (int)1))
						{
							
							_mu73se41 = (_ndcl48x4 * *(_864fslqq+(_b5p6od9s - (int)2 - 1)));
							*(_864fslqq+(_b5p6od9s - (int)2 - 1)) = (_bf18ryhs * *(_864fslqq+(_b5p6od9s - (int)2 - 1)));
						}
						
						*(_apig8meb+(_b5p6od9s - _jnnmt81a - 1)) = _7sb5moji;
						*(_apig8meb+((_b5p6od9s - _jnnmt81a) + _3xbv3idt - 1)) = (-(_jp87z33i));
						*(_apig8meb+((_b5p6od9s - _jnnmt81a) + _y3zmt0e3 - 1)) = _bf18ryhs;
						*(_apig8meb+((_b5p6od9s - _jnnmt81a) + _nowdo328 - 1)) = (-(_ndcl48x4));
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
				_sg7u7241("L" ,"V" ,"B" ,ref Unsafe.AsRef((_ev4xhht5 - _jnnmt81a) + (int)1) ,ref _0v6rtqiq ,(_apig8meb+(_y3zmt0e3 + (int)1 - 1)),(_apig8meb+(_nowdo328 + (int)1 - 1)),(_xdbczr8u+(_jnnmt81a - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv );
				if (_tg62ial7 > (int)0)
				_sg7u7241("R" ,"V" ,"B" ,ref _tg62ial7 ,ref Unsafe.AsRef((_ev4xhht5 - _jnnmt81a) + (int)1) ,(_apig8meb+((int)1 - 1)),(_apig8meb+(_dxpq0xkr - 1)),(_7u55mqkq+((int)1 - 1) + (_jnnmt81a - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b );
				if (_bcsi4mx0 > (int)0)
				_sg7u7241("L" ,"V" ,"B" ,ref Unsafe.AsRef((_ev4xhht5 - _jnnmt81a) + (int)1) ,ref _bcsi4mx0 ,(_apig8meb+((int)1 - 1)),(_apig8meb+(_dxpq0xkr - 1)),(_3crf0qn3+(_jnnmt81a - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 );
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
			System.Int32 __81fgg2dlsvn292 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step292 = (System.Int32)((int)1);
			System.Int32 __81fgg2count292;
			for (__81fgg2count292 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn292 + __81fgg2step292) / __81fgg2step292)), _b5p6od9s = __81fgg2dlsvn292; __81fgg2count292 != 0; __81fgg2count292--, _b5p6od9s += (__81fgg2step292)) {

			{
				
				if (*(_plfm7z8g+(_b5p6od9s - 1)) < _d0547bi2)
				{
					
					*(_plfm7z8g+(_b5p6od9s - 1)) = (-(*(_plfm7z8g+(_b5p6od9s - 1))));//* 
					//*           Change sign of singular vectors, if desired 
					//* 
					
					if (_0v6rtqiq > (int)0)
					_f6jqcjk1(ref _0v6rtqiq ,ref Unsafe.AsRef(_2v8pp9oq) ,(_xdbczr8u+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv );
				}
				
Mark170:;
				// continue
			}
						}		}//* 
		//*     Sort the singular values into decreasing order (insertion sort on 
		//*     singular values, but only one transposition per singular vector) 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn293 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step293 = (System.Int32)((int)1);
			System.Int32 __81fgg2count293;
			for (__81fgg2count293 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn293 + __81fgg2step293) / __81fgg2step293)), _b5p6od9s = __81fgg2dlsvn293; __81fgg2count293 != 0; __81fgg2count293--, _b5p6od9s += (__81fgg2step293)) {

			{
				//* 
				//*        Scan for smallest D(I) 
				//* 
				
				_ndl0tuhx = (int)1;
				_rhnpgpoi = *(_plfm7z8g+((int)1 - 1));
				{
					System.Int32 __81fgg2dlsvn294 = (System.Int32)((int)2);
					const System.Int32 __81fgg2step294 = (System.Int32)((int)1);
					System.Int32 __81fgg2count294;
					for (__81fgg2count294 = System.Math.Max(0, (System.Int32)(((System.Int32)((_dxpq0xkr + (int)1) - _b5p6od9s) - __81fgg2dlsvn294 + __81fgg2step294) / __81fgg2step294)), _znpjgsef = __81fgg2dlsvn294; __81fgg2count294 != 0; __81fgg2count294--, _znpjgsef += (__81fgg2step294)) {

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
					_trit81n6(ref _0v6rtqiq ,(_xdbczr8u+(_ndl0tuhx - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv ,(_xdbczr8u+((_dxpq0xkr + (int)1) - _b5p6od9s - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv );
					if (_tg62ial7 > (int)0)
					_trit81n6(ref _tg62ial7 ,(_7u55mqkq+((int)1 - 1) + (_ndl0tuhx - 1) * 1 * (_u6e6d39b)),ref Unsafe.AsRef((int)1) ,(_7u55mqkq+((int)1 - 1) + ((_dxpq0xkr + (int)1) - _b5p6od9s - 1) * 1 * (_u6e6d39b)),ref Unsafe.AsRef((int)1) );
					if (_bcsi4mx0 > (int)0)
					_trit81n6(ref _bcsi4mx0 ,(_3crf0qn3+(_ndl0tuhx - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_3crf0qn3+((_dxpq0xkr + (int)1) - _b5p6od9s - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 );
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
			System.Int32 __81fgg2dlsvn295 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step295 = (System.Int32)((int)1);
			System.Int32 __81fgg2count295;
			for (__81fgg2count295 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn295 + __81fgg2step295) / __81fgg2step295)), _b5p6od9s = __81fgg2dlsvn295; __81fgg2count295 != 0; __81fgg2count295--, _b5p6od9s += (__81fgg2step295)) {

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
		//*     End of DBDSQR 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
