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
//*> \brief \b CSTEQR 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CSTEQR + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/csteqr.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/csteqr.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/csteqr.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CSTEQR( COMPZ, N, D, E, Z, LDZ, WORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          COMPZ 
//*       INTEGER            INFO, LDZ, N 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               D( * ), E( * ), WORK( * ) 
//*       COMPLEX            Z( LDZ, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CSTEQR computes all eigenvalues and, optionally, eigenvectors of a 
//*> symmetric tridiagonal matrix using the implicit QL or QR method. 
//*> The eigenvectors of a full or band complex Hermitian matrix can also 
//*> be found if CHETRD or CHPTRD or CHBTRD has been used to reduce this 
//*> matrix to tridiagonal form. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] COMPZ 
//*> \verbatim 
//*>          COMPZ is CHARACTER*1 
//*>          = 'N':  Compute eigenvalues only. 
//*>          = 'V':  Compute eigenvalues and eigenvectors of the original 
//*>                  Hermitian matrix.  On entry, Z must contain the 
//*>                  unitary matrix used to reduce the original matrix 
//*>                  to tridiagonal form. 
//*>          = 'I':  Compute eigenvalues and eigenvectors of the 
//*>                  tridiagonal matrix.  Z is initialized to the identity 
//*>                  matrix. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix.  N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] D 
//*> \verbatim 
//*>          D is REAL array, dimension (N) 
//*>          On entry, the diagonal elements of the tridiagonal matrix. 
//*>          On exit, if INFO = 0, the eigenvalues in ascending order. 
//*> \endverbatim 
//*> 
//*> \param[in,out] E 
//*> \verbatim 
//*>          E is REAL array, dimension (N-1) 
//*>          On entry, the (n-1) subdiagonal elements of the tridiagonal 
//*>          matrix. 
//*>          On exit, E has been destroyed. 
//*> \endverbatim 
//*> 
//*> \param[in,out] Z 
//*> \verbatim 
//*>          Z is COMPLEX array, dimension (LDZ, N) 
//*>          On entry, if  COMPZ = 'V', then Z contains the unitary 
//*>          matrix used in the reduction to tridiagonal form. 
//*>          On exit, if INFO = 0, then if COMPZ = 'V', Z contains the 
//*>          orthonormal eigenvectors of the original Hermitian matrix, 
//*>          and if COMPZ = 'I', Z contains the orthonormal eigenvectors 
//*>          of the symmetric tridiagonal matrix. 
//*>          If COMPZ = 'N', then Z is not referenced. 
//*> \endverbatim 
//*> 
//*> \param[in] LDZ 
//*> \verbatim 
//*>          LDZ is INTEGER 
//*>          The leading dimension of the array Z.  LDZ >= 1, and if 
//*>          eigenvectors are desired, then  LDZ >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is REAL array, dimension (max(1,2*N-2)) 
//*>          If COMPZ = 'N', then WORK is not referenced. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit 
//*>          < 0:  if INFO = -i, the i-th argument had an illegal value 
//*>          > 0:  the algorithm has failed to find all the eigenvalues in 
//*>                a total of 30*N iterations; if INFO = i, then i 
//*>                elements of E have not converged to zero; on exit, D 
//*>                and E contain the elements of a symmetric tridiagonal 
//*>                matrix which is unitarily similar to the original 
//*>                matrix. 
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

	 
	public static void _h4p1amig(FString _w65aho3z, ref Int32 _dxpq0xkr, Single* _plfm7z8g, Single* _864fslqq, fcomplex* _7e60fcso, ref Int32 _5l1tna8s, Single* _apig8meb, ref Int32 _gro5yvfo)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Single _kxg5drh2 =  1f;
Single _5m0mjfxm =  2f;
Single _08e01ee2 =  3f;
fcomplex _gdjumcqt =   new fcomplex(0f,0f);
fcomplex _40vhxf9f =   new fcomplex(1f,0f);
Int32 _gaia76w5 =  (int)30;
Int32 _b5p6od9s =  default;
Int32 _me1o3g0l =  default;
Int32 _retbwjxi =  default;
Int32 _g5graale =  default;
Int32 _znpjgsef =  default;
Int32 _jjlk8rbf =  default;
Int32 _umlkckdg =  default;
Int32 _68ec3gbh =  default;
Int32 _135aegwf =  default;
Int32 _1mp99t47 =  default;
Int32 _b1kw7yt3 =  default;
Int32 _u57y2pv6 =  default;
Int32 _s1sstu6z =  default;
Int32 _f5jjh6pc =  default;
Int32 _clabrarh =  default;
Int32 _ev4xhht5 =  default;
Int32 _e9y2lltf =  default;
Int32 _lhovsbvk =  default;
Int32 _3xbv3idt =  default;
Int32 _07mc27rj =  default;
Single _b8rxgs6o =  default;
Single _p9n405a5 =  default;
Single _3crf0qn3 =  default;
Single _p1iqarg6 =  default;
Single _okg6tegz =  default;
Single _8plnuphw =  default;
Single _mu73se41 =  default;
Single _ejwydfmr =  default;
Single _q2vwp05i =  default;
Single _wwj82gep =  default;
Single _uwqai9pg =  default;
Single _irk8i6qr =  default;
Single _odf6ja0t =  default;
Single _h75qnr7l =  default;
Single _jjejn9g8 =  default;
Single _flwdikii =  default;
Single _ts63qxkr =  default;
string fLanavab = default;
#endregion  variable declarations
_w65aho3z = _w65aho3z.Convert(1);

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
		
		_gro5yvfo = (int)0;//* 
		
		if (_w8y2rzgy(_w65aho3z ,"N" ))
		{
			
			_me1o3g0l = (int)0;
		}
		else
		if (_w8y2rzgy(_w65aho3z ,"V" ))
		{
			
			_me1o3g0l = (int)1;
		}
		else
		if (_w8y2rzgy(_w65aho3z ,"I" ))
		{
			
			_me1o3g0l = (int)2;
		}
		else
		{
			
			_me1o3g0l = (int)-1;
		}
		
		if (_me1o3g0l < (int)0)
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if ((_5l1tna8s < (int)1) | ((_me1o3g0l > (int)0) & (_5l1tna8s < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))))
		{
			
			_gro5yvfo = (int)-6;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("CSTEQR" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr == (int)0)
		return;//* 
		
		if (_dxpq0xkr == (int)1)
		{
			
			if (_me1o3g0l == (int)2)
			*(_7e60fcso+((int)1 - 1) + ((int)1 - 1) * 1 * (_5l1tna8s)) = _40vhxf9f;
			return;
		}
		//* 
		//*     Determine the unit roundoff and over/underflow thresholds. 
		//* 
		
		_p1iqarg6 = _d5tu038y("E" );
		_okg6tegz = __POW2(_p1iqarg6);
		_h75qnr7l = _d5tu038y("S" );
		_odf6ja0t = (_kxg5drh2 / _h75qnr7l);
		_jjejn9g8 = (ILNumerics.F2NET.Intrinsics.SQRT(_odf6ja0t ) / _08e01ee2);
		_flwdikii = (ILNumerics.F2NET.Intrinsics.SQRT(_h75qnr7l ) / _okg6tegz);//* 
		//*     Compute the eigenvalues and eigenvectors of the tridiagonal 
		//*     matrix. 
		//* 
		
		if (_me1o3g0l == (int)2)
		_663dvznc("Full" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_gdjumcqt) ,ref Unsafe.AsRef(_40vhxf9f) ,_7e60fcso ,ref _5l1tna8s );//* 
		
		_07mc27rj = (_dxpq0xkr * _gaia76w5);
		_jjlk8rbf = (int)0;//* 
		//*     Determine where the matrix splits and choose QL or QR iteration 
		//*     for each block, according to whether top or bottom diagonal 
		//*     element is smaller. 
		//* 
		
		_135aegwf = (int)1;
		_3xbv3idt = (_dxpq0xkr - (int)1);//* 
		
Mark10:;
		// continue
		if (_135aegwf > _dxpq0xkr)goto Mark160;
		if (_135aegwf > (int)1)
		*(_864fslqq+(_135aegwf - (int)1 - 1)) = _d0547bi2;
		if (_135aegwf <= _3xbv3idt)
		{
			
			{
				System.Int32 __81fgg2dlsvn3857 = (System.Int32)(_135aegwf);
				const System.Int32 __81fgg2step3857 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3857;
				for (__81fgg2count3857 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3xbv3idt) - __81fgg2dlsvn3857 + __81fgg2step3857) / __81fgg2step3857)), _ev4xhht5 = __81fgg2dlsvn3857; __81fgg2count3857 != 0; __81fgg2count3857--, _ev4xhht5 += (__81fgg2step3857)) {

				{
					
					_ts63qxkr = ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_ev4xhht5 - 1)) );
					if (_ts63qxkr == _d0547bi2)goto Mark30;
					if (_ts63qxkr <= ((ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_ev4xhht5 - 1)) ) ) * ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_ev4xhht5 + (int)1 - 1)) ) )) * _p1iqarg6))
					{
						
						*(_864fslqq+(_ev4xhht5 - 1)) = _d0547bi2;goto Mark30;
					}
					
Mark20:;
					// continue
				}
								}			}
		}
		
		_ev4xhht5 = _dxpq0xkr;//* 
		
Mark30:;
		// continue
		_68ec3gbh = _135aegwf;
		_clabrarh = _68ec3gbh;
		_1mp99t47 = _ev4xhht5;
		_s1sstu6z = _1mp99t47;
		_135aegwf = (_ev4xhht5 + (int)1);
		if (_1mp99t47 == _68ec3gbh)goto Mark10;//* 
		//*     Scale submatrix in rows and columns L to LEND 
		//* 
		
		_b8rxgs6o = _5kajfnos("I" ,ref Unsafe.AsRef((_1mp99t47 - _68ec3gbh) + (int)1) ,(_plfm7z8g+(_68ec3gbh - 1)),(_864fslqq+(_68ec3gbh - 1)));
		_g5graale = (int)0;
		if (_b8rxgs6o == _d0547bi2)goto Mark10;
		if (_b8rxgs6o > _jjejn9g8)
		{
			
			_g5graale = (int)1;
			_cq2kgmi4("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _b8rxgs6o ,ref _jjejn9g8 ,ref Unsafe.AsRef((_1mp99t47 - _68ec3gbh) + (int)1) ,ref Unsafe.AsRef((int)1) ,(_plfm7z8g+(_68ec3gbh - 1)),ref _dxpq0xkr ,ref _gro5yvfo );
			_cq2kgmi4("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _b8rxgs6o ,ref _jjejn9g8 ,ref Unsafe.AsRef(_1mp99t47 - _68ec3gbh) ,ref Unsafe.AsRef((int)1) ,(_864fslqq+(_68ec3gbh - 1)),ref _dxpq0xkr ,ref _gro5yvfo );
		}
		else
		if (_b8rxgs6o < _flwdikii)
		{
			
			_g5graale = (int)2;
			_cq2kgmi4("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _b8rxgs6o ,ref _flwdikii ,ref Unsafe.AsRef((_1mp99t47 - _68ec3gbh) + (int)1) ,ref Unsafe.AsRef((int)1) ,(_plfm7z8g+(_68ec3gbh - 1)),ref _dxpq0xkr ,ref _gro5yvfo );
			_cq2kgmi4("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _b8rxgs6o ,ref _flwdikii ,ref Unsafe.AsRef(_1mp99t47 - _68ec3gbh) ,ref Unsafe.AsRef((int)1) ,(_864fslqq+(_68ec3gbh - 1)),ref _dxpq0xkr ,ref _gro5yvfo );
		}
		//* 
		//*     Choose between QL and QR iteration 
		//* 
		
		if (ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_1mp99t47 - 1)) ) < ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_68ec3gbh - 1)) ))
		{
			
			_1mp99t47 = _clabrarh;
			_68ec3gbh = _s1sstu6z;
		}
		//* 
		
		if (_1mp99t47 > _68ec3gbh)
		{
			//* 
			//*        QL Iteration 
			//* 
			//*        Look for small subdiagonal element. 
			//* 
			
Mark40:;
			// continue
			if (_68ec3gbh != _1mp99t47)
			{
				
				_b1kw7yt3 = (_1mp99t47 - (int)1);
				{
					System.Int32 __81fgg2dlsvn3858 = (System.Int32)(_68ec3gbh);
					const System.Int32 __81fgg2step3858 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3858;
					for (__81fgg2count3858 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b1kw7yt3) - __81fgg2dlsvn3858 + __81fgg2step3858) / __81fgg2step3858)), _ev4xhht5 = __81fgg2dlsvn3858; __81fgg2count3858 != 0; __81fgg2count3858--, _ev4xhht5 += (__81fgg2step3858)) {

					{
						
						_ts63qxkr = __POW2(ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_ev4xhht5 - 1)) ));
						if (_ts63qxkr <= (((_okg6tegz * ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_ev4xhht5 - 1)) )) * ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_ev4xhht5 + (int)1 - 1)) )) + _h75qnr7l))goto Mark60;
Mark50:;
						// continue
					}
										}				}
			}
			//* 
			
			_ev4xhht5 = _1mp99t47;//* 
			
Mark60:;
			// continue
			if (_ev4xhht5 < _1mp99t47)
			*(_864fslqq+(_ev4xhht5 - 1)) = _d0547bi2;
			_ejwydfmr = *(_plfm7z8g+(_68ec3gbh - 1));
			if (_ev4xhht5 == _68ec3gbh)goto Mark80;//* 
			//*        If remaining matrix is 2-by-2, use SLAE2 or SLAEV2 
			//*        to compute its eigensystem. 
			//* 
			
			if (_ev4xhht5 == (_68ec3gbh + (int)1))
			{
				
				if (_me1o3g0l > (int)0)
				{
					
					_uh9ssfxw(ref Unsafe.AsRef(*(_plfm7z8g+(_68ec3gbh - 1))) ,ref Unsafe.AsRef(*(_864fslqq+(_68ec3gbh - 1))) ,ref Unsafe.AsRef(*(_plfm7z8g+(_68ec3gbh + (int)1 - 1))) ,ref _wwj82gep ,ref _uwqai9pg ,ref _3crf0qn3 ,ref _irk8i6qr );
					*(_apig8meb+(_68ec3gbh - 1)) = _3crf0qn3;
					*(_apig8meb+((_dxpq0xkr - (int)1) + _68ec3gbh - 1)) = _irk8i6qr;
					_40qhbg49("R" ,"V" ,"B" ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)2) ,(_apig8meb+(_68ec3gbh - 1)),(_apig8meb+((_dxpq0xkr - (int)1) + _68ec3gbh - 1)),(_7e60fcso+((int)1 - 1) + (_68ec3gbh - 1) * 1 * (_5l1tna8s)),ref _5l1tna8s );
				}
				else
				{
					
					_7vaufgro(ref Unsafe.AsRef(*(_plfm7z8g+(_68ec3gbh - 1))) ,ref Unsafe.AsRef(*(_864fslqq+(_68ec3gbh - 1))) ,ref Unsafe.AsRef(*(_plfm7z8g+(_68ec3gbh + (int)1 - 1))) ,ref _wwj82gep ,ref _uwqai9pg );
				}
				
				*(_plfm7z8g+(_68ec3gbh - 1)) = _wwj82gep;
				*(_plfm7z8g+(_68ec3gbh + (int)1 - 1)) = _uwqai9pg;
				*(_864fslqq+(_68ec3gbh - 1)) = _d0547bi2;
				_68ec3gbh = (_68ec3gbh + (int)2);
				if (_68ec3gbh <= _1mp99t47)goto Mark40;goto Mark140;
			}
			//* 
			
			if (_jjlk8rbf == _07mc27rj)goto Mark140;
			_jjlk8rbf = (_jjlk8rbf + (int)1);//* 
			//*        Form shift. 
			//* 
			
			_mu73se41 = ((*(_plfm7z8g+(_68ec3gbh + (int)1 - 1)) - _ejwydfmr) / (_5m0mjfxm * *(_864fslqq+(_68ec3gbh - 1))));
			_q2vwp05i = _syk7170d(ref _mu73se41 ,ref Unsafe.AsRef(_kxg5drh2) );
			_mu73se41 = ((*(_plfm7z8g+(_ev4xhht5 - 1)) - _ejwydfmr) + (*(_864fslqq+(_68ec3gbh - 1)) / (_mu73se41 + ILNumerics.F2NET.Intrinsics.SIGN(_q2vwp05i ,_mu73se41 ))));//* 
			
			_irk8i6qr = _kxg5drh2;
			_3crf0qn3 = _kxg5drh2;
			_ejwydfmr = _d0547bi2;//* 
			//*        Inner loop 
			//* 
			
			_lhovsbvk = (_ev4xhht5 - (int)1);
			{
				System.Int32 __81fgg2dlsvn3859 = (System.Int32)(_lhovsbvk);
				System.Int32 __81fgg2step3859 = (System.Int32)((int)-1);
				System.Int32 __81fgg2count3859;
				for (__81fgg2count3859 = System.Math.Max(0, (System.Int32)(((System.Int32)(_68ec3gbh) - __81fgg2dlsvn3859 + __81fgg2step3859) / __81fgg2step3859)), _b5p6od9s = __81fgg2dlsvn3859; __81fgg2count3859 != 0; __81fgg2count3859--, _b5p6od9s += (__81fgg2step3859)) {

				{
					
					_8plnuphw = (_irk8i6qr * *(_864fslqq+(_b5p6od9s - 1)));
					_p9n405a5 = (_3crf0qn3 * *(_864fslqq+(_b5p6od9s - 1)));
					_uf57gsrz(ref _mu73se41 ,ref _8plnuphw ,ref _3crf0qn3 ,ref _irk8i6qr ,ref _q2vwp05i );
					if (_b5p6od9s != (_ev4xhht5 - (int)1))
					*(_864fslqq+(_b5p6od9s + (int)1 - 1)) = _q2vwp05i;
					_mu73se41 = (*(_plfm7z8g+(_b5p6od9s + (int)1 - 1)) - _ejwydfmr);
					_q2vwp05i = (((*(_plfm7z8g+(_b5p6od9s - 1)) - _mu73se41) * _irk8i6qr) + ((_5m0mjfxm * _3crf0qn3) * _p9n405a5));
					_ejwydfmr = (_irk8i6qr * _q2vwp05i);
					*(_plfm7z8g+(_b5p6od9s + (int)1 - 1)) = (_mu73se41 + _ejwydfmr);
					_mu73se41 = ((_3crf0qn3 * _q2vwp05i) - _p9n405a5);//* 
					//*           If eigenvectors are desired, then save rotations. 
					//* 
					
					if (_me1o3g0l > (int)0)
					{
						
						*(_apig8meb+(_b5p6od9s - 1)) = _3crf0qn3;
						*(_apig8meb+((_dxpq0xkr - (int)1) + _b5p6od9s - 1)) = (-(_irk8i6qr));
					}
					//* 
					
Mark70:;
					// continue
				}
								}			}//* 
			//*        If eigenvectors are desired, then apply saved rotations. 
			//* 
			
			if (_me1o3g0l > (int)0)
			{
				
				_e9y2lltf = ((_ev4xhht5 - _68ec3gbh) + (int)1);
				_40qhbg49("R" ,"V" ,"B" ,ref _dxpq0xkr ,ref _e9y2lltf ,(_apig8meb+(_68ec3gbh - 1)),(_apig8meb+((_dxpq0xkr - (int)1) + _68ec3gbh - 1)),(_7e60fcso+((int)1 - 1) + (_68ec3gbh - 1) * 1 * (_5l1tna8s)),ref _5l1tna8s );
			}
			//* 
			
			*(_plfm7z8g+(_68ec3gbh - 1)) = (*(_plfm7z8g+(_68ec3gbh - 1)) - _ejwydfmr);
			*(_864fslqq+(_68ec3gbh - 1)) = _mu73se41;goto Mark40;//* 
			//*        Eigenvalue found. 
			//* 
			
Mark80:;
			// continue
			*(_plfm7z8g+(_68ec3gbh - 1)) = _ejwydfmr;//* 
			
			_68ec3gbh = (_68ec3gbh + (int)1);
			if (_68ec3gbh <= _1mp99t47)goto Mark40;goto Mark140;//* 
			
		}
		else
		{
			//* 
			//*        QR Iteration 
			//* 
			//*        Look for small superdiagonal element. 
			//* 
			
Mark90:;
			// continue
			if (_68ec3gbh != _1mp99t47)
			{
				
				_u57y2pv6 = (_1mp99t47 + (int)1);
				{
					System.Int32 __81fgg2dlsvn3860 = (System.Int32)(_68ec3gbh);
					System.Int32 __81fgg2step3860 = (System.Int32)((int)-1);
					System.Int32 __81fgg2count3860;
					for (__81fgg2count3860 = System.Math.Max(0, (System.Int32)(((System.Int32)(_u57y2pv6) - __81fgg2dlsvn3860 + __81fgg2step3860) / __81fgg2step3860)), _ev4xhht5 = __81fgg2dlsvn3860; __81fgg2count3860 != 0; __81fgg2count3860--, _ev4xhht5 += (__81fgg2step3860)) {

					{
						
						_ts63qxkr = __POW2(ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_ev4xhht5 - (int)1 - 1)) ));
						if (_ts63qxkr <= (((_okg6tegz * ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_ev4xhht5 - 1)) )) * ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_ev4xhht5 - (int)1 - 1)) )) + _h75qnr7l))goto Mark110;
Mark100:;
						// continue
					}
										}				}
			}
			//* 
			
			_ev4xhht5 = _1mp99t47;//* 
			
Mark110:;
			// continue
			if (_ev4xhht5 > _1mp99t47)
			*(_864fslqq+(_ev4xhht5 - (int)1 - 1)) = _d0547bi2;
			_ejwydfmr = *(_plfm7z8g+(_68ec3gbh - 1));
			if (_ev4xhht5 == _68ec3gbh)goto Mark130;//* 
			//*        If remaining matrix is 2-by-2, use SLAE2 or SLAEV2 
			//*        to compute its eigensystem. 
			//* 
			
			if (_ev4xhht5 == (_68ec3gbh - (int)1))
			{
				
				if (_me1o3g0l > (int)0)
				{
					
					_uh9ssfxw(ref Unsafe.AsRef(*(_plfm7z8g+(_68ec3gbh - (int)1 - 1))) ,ref Unsafe.AsRef(*(_864fslqq+(_68ec3gbh - (int)1 - 1))) ,ref Unsafe.AsRef(*(_plfm7z8g+(_68ec3gbh - 1))) ,ref _wwj82gep ,ref _uwqai9pg ,ref _3crf0qn3 ,ref _irk8i6qr );
					*(_apig8meb+(_ev4xhht5 - 1)) = _3crf0qn3;
					*(_apig8meb+((_dxpq0xkr - (int)1) + _ev4xhht5 - 1)) = _irk8i6qr;
					_40qhbg49("R" ,"V" ,"F" ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)2) ,(_apig8meb+(_ev4xhht5 - 1)),(_apig8meb+((_dxpq0xkr - (int)1) + _ev4xhht5 - 1)),(_7e60fcso+((int)1 - 1) + (_68ec3gbh - (int)1 - 1) * 1 * (_5l1tna8s)),ref _5l1tna8s );
				}
				else
				{
					
					_7vaufgro(ref Unsafe.AsRef(*(_plfm7z8g+(_68ec3gbh - (int)1 - 1))) ,ref Unsafe.AsRef(*(_864fslqq+(_68ec3gbh - (int)1 - 1))) ,ref Unsafe.AsRef(*(_plfm7z8g+(_68ec3gbh - 1))) ,ref _wwj82gep ,ref _uwqai9pg );
				}
				
				*(_plfm7z8g+(_68ec3gbh - (int)1 - 1)) = _wwj82gep;
				*(_plfm7z8g+(_68ec3gbh - 1)) = _uwqai9pg;
				*(_864fslqq+(_68ec3gbh - (int)1 - 1)) = _d0547bi2;
				_68ec3gbh = (_68ec3gbh - (int)2);
				if (_68ec3gbh >= _1mp99t47)goto Mark90;goto Mark140;
			}
			//* 
			
			if (_jjlk8rbf == _07mc27rj)goto Mark140;
			_jjlk8rbf = (_jjlk8rbf + (int)1);//* 
			//*        Form shift. 
			//* 
			
			_mu73se41 = ((*(_plfm7z8g+(_68ec3gbh - (int)1 - 1)) - _ejwydfmr) / (_5m0mjfxm * *(_864fslqq+(_68ec3gbh - (int)1 - 1))));
			_q2vwp05i = _syk7170d(ref _mu73se41 ,ref Unsafe.AsRef(_kxg5drh2) );
			_mu73se41 = ((*(_plfm7z8g+(_ev4xhht5 - 1)) - _ejwydfmr) + (*(_864fslqq+(_68ec3gbh - (int)1 - 1)) / (_mu73se41 + ILNumerics.F2NET.Intrinsics.SIGN(_q2vwp05i ,_mu73se41 ))));//* 
			
			_irk8i6qr = _kxg5drh2;
			_3crf0qn3 = _kxg5drh2;
			_ejwydfmr = _d0547bi2;//* 
			//*        Inner loop 
			//* 
			
			_f5jjh6pc = (_68ec3gbh - (int)1);
			{
				System.Int32 __81fgg2dlsvn3861 = (System.Int32)(_ev4xhht5);
				const System.Int32 __81fgg2step3861 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3861;
				for (__81fgg2count3861 = System.Math.Max(0, (System.Int32)(((System.Int32)(_f5jjh6pc) - __81fgg2dlsvn3861 + __81fgg2step3861) / __81fgg2step3861)), _b5p6od9s = __81fgg2dlsvn3861; __81fgg2count3861 != 0; __81fgg2count3861--, _b5p6od9s += (__81fgg2step3861)) {

				{
					
					_8plnuphw = (_irk8i6qr * *(_864fslqq+(_b5p6od9s - 1)));
					_p9n405a5 = (_3crf0qn3 * *(_864fslqq+(_b5p6od9s - 1)));
					_uf57gsrz(ref _mu73se41 ,ref _8plnuphw ,ref _3crf0qn3 ,ref _irk8i6qr ,ref _q2vwp05i );
					if (_b5p6od9s != _ev4xhht5)
					*(_864fslqq+(_b5p6od9s - (int)1 - 1)) = _q2vwp05i;
					_mu73se41 = (*(_plfm7z8g+(_b5p6od9s - 1)) - _ejwydfmr);
					_q2vwp05i = (((*(_plfm7z8g+(_b5p6od9s + (int)1 - 1)) - _mu73se41) * _irk8i6qr) + ((_5m0mjfxm * _3crf0qn3) * _p9n405a5));
					_ejwydfmr = (_irk8i6qr * _q2vwp05i);
					*(_plfm7z8g+(_b5p6od9s - 1)) = (_mu73se41 + _ejwydfmr);
					_mu73se41 = ((_3crf0qn3 * _q2vwp05i) - _p9n405a5);//* 
					//*           If eigenvectors are desired, then save rotations. 
					//* 
					
					if (_me1o3g0l > (int)0)
					{
						
						*(_apig8meb+(_b5p6od9s - 1)) = _3crf0qn3;
						*(_apig8meb+((_dxpq0xkr - (int)1) + _b5p6od9s - 1)) = _irk8i6qr;
					}
					//* 
					
Mark120:;
					// continue
				}
								}			}//* 
			//*        If eigenvectors are desired, then apply saved rotations. 
			//* 
			
			if (_me1o3g0l > (int)0)
			{
				
				_e9y2lltf = ((_68ec3gbh - _ev4xhht5) + (int)1);
				_40qhbg49("R" ,"V" ,"F" ,ref _dxpq0xkr ,ref _e9y2lltf ,(_apig8meb+(_ev4xhht5 - 1)),(_apig8meb+((_dxpq0xkr - (int)1) + _ev4xhht5 - 1)),(_7e60fcso+((int)1 - 1) + (_ev4xhht5 - 1) * 1 * (_5l1tna8s)),ref _5l1tna8s );
			}
			//* 
			
			*(_plfm7z8g+(_68ec3gbh - 1)) = (*(_plfm7z8g+(_68ec3gbh - 1)) - _ejwydfmr);
			*(_864fslqq+(_f5jjh6pc - 1)) = _mu73se41;goto Mark90;//* 
			//*        Eigenvalue found. 
			//* 
			
Mark130:;
			// continue
			*(_plfm7z8g+(_68ec3gbh - 1)) = _ejwydfmr;//* 
			
			_68ec3gbh = (_68ec3gbh - (int)1);
			if (_68ec3gbh >= _1mp99t47)goto Mark90;goto Mark140;//* 
			
		}
		//* 
		//*     Undo scaling if necessary 
		//* 
		
Mark140:;
		// continue
		if (_g5graale == (int)1)
		{
			
			_cq2kgmi4("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _jjejn9g8 ,ref _b8rxgs6o ,ref Unsafe.AsRef((_s1sstu6z - _clabrarh) + (int)1) ,ref Unsafe.AsRef((int)1) ,(_plfm7z8g+(_clabrarh - 1)),ref _dxpq0xkr ,ref _gro5yvfo );
			_cq2kgmi4("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _jjejn9g8 ,ref _b8rxgs6o ,ref Unsafe.AsRef(_s1sstu6z - _clabrarh) ,ref Unsafe.AsRef((int)1) ,(_864fslqq+(_clabrarh - 1)),ref _dxpq0xkr ,ref _gro5yvfo );
		}
		else
		if (_g5graale == (int)2)
		{
			
			_cq2kgmi4("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _flwdikii ,ref _b8rxgs6o ,ref Unsafe.AsRef((_s1sstu6z - _clabrarh) + (int)1) ,ref Unsafe.AsRef((int)1) ,(_plfm7z8g+(_clabrarh - 1)),ref _dxpq0xkr ,ref _gro5yvfo );
			_cq2kgmi4("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _flwdikii ,ref _b8rxgs6o ,ref Unsafe.AsRef(_s1sstu6z - _clabrarh) ,ref Unsafe.AsRef((int)1) ,(_864fslqq+(_clabrarh - 1)),ref _dxpq0xkr ,ref _gro5yvfo );
		}
		//* 
		//*     Check for no convergence to an eigenvalue after a total 
		//*     of N*MAXIT iterations. 
		//* 
		
		if (_jjlk8rbf == _07mc27rj)
		{
			
			{
				System.Int32 __81fgg2dlsvn3862 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step3862 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3862;
				for (__81fgg2count3862 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn3862 + __81fgg2step3862) / __81fgg2step3862)), _b5p6od9s = __81fgg2dlsvn3862; __81fgg2count3862 != 0; __81fgg2count3862--, _b5p6od9s += (__81fgg2step3862)) {

				{
					
					if (*(_864fslqq+(_b5p6od9s - 1)) != _d0547bi2)
					_gro5yvfo = (_gro5yvfo + (int)1);
Mark150:;
					// continue
				}
								}			}
			return;
		}
		goto Mark10;//* 
		//*     Order eigenvalues and eigenvectors. 
		//* 
		
Mark160:;
		// continue
		if (_me1o3g0l == (int)0)
		{
			//* 
			//*        Use Quick Sort 
			//* 
			
			_ezdvkw03("I" ,ref _dxpq0xkr ,_plfm7z8g ,ref _gro5yvfo );//* 
			
		}
		else
		{
			//* 
			//*        Use Selection Sort to minimize swaps of eigenvectors 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn3863 = (System.Int32)((int)2);
				const System.Int32 __81fgg2step3863 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3863;
				for (__81fgg2count3863 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3863 + __81fgg2step3863) / __81fgg2step3863)), _retbwjxi = __81fgg2dlsvn3863; __81fgg2count3863 != 0; __81fgg2count3863--, _retbwjxi += (__81fgg2step3863)) {

				{
					
					_b5p6od9s = (_retbwjxi - (int)1);
					_umlkckdg = _b5p6od9s;
					_ejwydfmr = *(_plfm7z8g+(_b5p6od9s - 1));
					{
						System.Int32 __81fgg2dlsvn3864 = (System.Int32)(_retbwjxi);
						const System.Int32 __81fgg2step3864 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3864;
						for (__81fgg2count3864 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3864 + __81fgg2step3864) / __81fgg2step3864)), _znpjgsef = __81fgg2dlsvn3864; __81fgg2count3864 != 0; __81fgg2count3864--, _znpjgsef += (__81fgg2step3864)) {

						{
							
							if (*(_plfm7z8g+(_znpjgsef - 1)) < _ejwydfmr)
							{
								
								_umlkckdg = _znpjgsef;
								_ejwydfmr = *(_plfm7z8g+(_znpjgsef - 1));
							}
							
Mark170:;
							// continue
						}
												}					}
					if (_umlkckdg != _b5p6od9s)
					{
						
						*(_plfm7z8g+(_umlkckdg - 1)) = *(_plfm7z8g+(_b5p6od9s - 1));
						*(_plfm7z8g+(_b5p6od9s - 1)) = _ejwydfmr;
						_1frbwlh0(ref _dxpq0xkr ,(_7e60fcso+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_5l1tna8s)),ref Unsafe.AsRef((int)1) ,(_7e60fcso+((int)1 - 1) + (_umlkckdg - 1) * 1 * (_5l1tna8s)),ref Unsafe.AsRef((int)1) );
					}
					
Mark180:;
					// continue
				}
								}			}
		}
		
		return;//* 
		//*     End of CSTEQR 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
