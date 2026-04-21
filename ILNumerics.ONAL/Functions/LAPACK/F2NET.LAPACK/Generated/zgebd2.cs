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
//*> \brief \b ZGEBD2 reduces a general matrix to bidiagonal form using an unblocked algorithm. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZGEBD2 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zgebd2.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zgebd2.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zgebd2.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZGEBD2( M, N, A, LDA, D, E, TAUQ, TAUP, WORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INFO, LDA, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   D( * ), E( * ) 
//*       COMPLEX*16         A( LDA, * ), TAUP( * ), TAUQ( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZGEBD2 reduces a complex general m by n matrix A to upper or lower 
//*> real bidiagonal form B by a unitary transformation: Q**H * A * P = B. 
//*> 
//*> If m >= n, B is upper bidiagonal; if m < n, B is lower bidiagonal. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The number of rows in the matrix A.  M >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of columns in the matrix A.  N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is COMPLEX*16 array, dimension (LDA,N) 
//*>          On entry, the m by n general matrix to be reduced. 
//*>          On exit, 
//*>          if m >= n, the diagonal and the first superdiagonal are 
//*>            overwritten with the upper bidiagonal matrix B; the 
//*>            elements below the diagonal, with the array TAUQ, represent 
//*>            the unitary matrix Q as a product of elementary 
//*>            reflectors, and the elements above the first superdiagonal, 
//*>            with the array TAUP, represent the unitary matrix P as 
//*>            a product of elementary reflectors; 
//*>          if m < n, the diagonal and the first subdiagonal are 
//*>            overwritten with the lower bidiagonal matrix B; the 
//*>            elements below the first subdiagonal, with the array TAUQ, 
//*>            represent the unitary matrix Q as a product of 
//*>            elementary reflectors, and the elements above the diagonal, 
//*>            with the array TAUP, represent the unitary matrix P as 
//*>            a product of elementary reflectors. 
//*>          See Further Details. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max(1,M). 
//*> \endverbatim 
//*> 
//*> \param[out] D 
//*> \verbatim 
//*>          D is DOUBLE PRECISION array, dimension (min(M,N)) 
//*>          The diagonal elements of the bidiagonal matrix B: 
//*>          D(i) = A(i,i). 
//*> \endverbatim 
//*> 
//*> \param[out] E 
//*> \verbatim 
//*>          E is DOUBLE PRECISION array, dimension (min(M,N)-1) 
//*>          The off-diagonal elements of the bidiagonal matrix B: 
//*>          if m >= n, E(i) = A(i,i+1) for i = 1,2,...,n-1; 
//*>          if m < n, E(i) = A(i+1,i) for i = 1,2,...,m-1. 
//*> \endverbatim 
//*> 
//*> \param[out] TAUQ 
//*> \verbatim 
//*>          TAUQ is COMPLEX*16 array, dimension (min(M,N)) 
//*>          The scalar factors of the elementary reflectors which 
//*>          represent the unitary matrix Q. See Further Details. 
//*> \endverbatim 
//*> 
//*> \param[out] TAUP 
//*> \verbatim 
//*>          TAUP is COMPLEX*16 array, dimension (min(M,N)) 
//*>          The scalar factors of the elementary reflectors which 
//*>          represent the unitary matrix P. See Further Details. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is COMPLEX*16 array, dimension (max(M,N)) 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0: successful exit 
//*>          < 0: if INFO = -i, the i-th argument had an illegal value. 
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
//*> \ingroup complex16GEcomputational 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  The matrices Q and P are represented as products of elementary 
//*>  reflectors: 
//*> 
//*>  If m >= n, 
//*> 
//*>     Q = H(1) H(2) . . . H(n)  and  P = G(1) G(2) . . . G(n-1) 
//*> 
//*>  Each H(i) and G(i) has the form: 
//*> 
//*>     H(i) = I - tauq * v * v**H  and G(i) = I - taup * u * u**H 
//*> 
//*>  where tauq and taup are complex scalars, and v and u are complex 
//*>  vectors; v(1:i-1) = 0, v(i) = 1, and v(i+1:m) is stored on exit in 
//*>  A(i+1:m,i); u(1:i) = 0, u(i+1) = 1, and u(i+2:n) is stored on exit in 
//*>  A(i,i+2:n); tauq is stored in TAUQ(i) and taup in TAUP(i). 
//*> 
//*>  If m < n, 
//*> 
//*>     Q = H(1) H(2) . . . H(m-1)  and  P = G(1) G(2) . . . G(m) 
//*> 
//*>  Each H(i) and G(i) has the form: 
//*> 
//*>     H(i) = I - tauq * v * v**H  and G(i) = I - taup * u * u**H 
//*> 
//*>  where tauq and taup are complex scalars, v and u are complex vectors; 
//*>  v(1:i) = 0, v(i+1) = 1, and v(i+2:m) is stored on exit in A(i+2:m,i); 
//*>  u(1:i-1) = 0, u(i) = 1, and u(i+1:n) is stored on exit in A(i,i+1:n); 
//*>  tauq is stored in TAUQ(i) and taup in TAUP(i). 
//*> 
//*>  The contents of A on exit are illustrated by the following examples: 
//*> 
//*>  m = 6 and n = 5 (m > n):          m = 5 and n = 6 (m < n): 
//*> 
//*>    (  d   e   u1  u1  u1 )           (  d   u1  u1  u1  u1  u1 ) 
//*>    (  v1  d   e   u2  u2 )           (  e   d   u2  u2  u2  u2 ) 
//*>    (  v1  v2  d   e   u3 )           (  v1  e   d   u3  u3  u3 ) 
//*>    (  v1  v2  v3  d   e  )           (  v1  v2  e   d   u4  u4 ) 
//*>    (  v1  v2  v3  v4  d  )           (  v1  v2  v3  e   d   u5 ) 
//*>    (  v1  v2  v3  v4  v5 ) 
//*> 
//*>  where d and e denote diagonal and off-diagonal elements of B, vi 
//*>  denotes an element of the vector defining H(i), and ui an element of 
//*>  the vector defining G(i). 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _1yu0be6s(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, complex* _vxfgpup9, ref Int32 _ocv8fk5c, Double* _plfm7z8g, Double* _864fslqq, complex* _enkrihm0, complex* _fus6fnrl, complex* _apig8meb, ref Int32 _gro5yvfo)
	{
#region variable declarations
complex _d0547bi2 =   new fcomplex(0f,0f);
complex _kxg5drh2 =   new fcomplex(1f,0f);
Int32 _b5p6od9s =  default;
complex _r7cfteg3 =  default;
string fLanavab = default;
#endregion  variable declarations

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
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input parameters 
		//* 
		
		_gro5yvfo = (int)0;
		if (_ev4xhht5 < (int)0)
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_ev4xhht5 ))
		{
			
			_gro5yvfo = (int)-4;
		}
		
		if (_gro5yvfo < (int)0)
		{
			
			_ut9qalzx("ZGEBD2" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		
		if (_ev4xhht5 >= _dxpq0xkr)
		{
			//* 
			//*        Reduce to upper bidiagonal form 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn1138 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1138 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1138;
				for (__81fgg2count1138 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1138 + __81fgg2step1138) / __81fgg2step1138)), _b5p6od9s = __81fgg2dlsvn1138; __81fgg2count1138 != 0; __81fgg2count1138--, _b5p6od9s += (__81fgg2step1138)) {

				{
					//* 
					//*           Generate elementary reflector H(i) to annihilate A(i+1:m,i) 
					//* 
					
					_r7cfteg3 = *(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c));
					_4btmjfem(ref Unsafe.AsRef((_ev4xhht5 - _b5p6od9s) + (int)1) ,ref _r7cfteg3 ,(_vxfgpup9+(ILNumerics.F2NET.Intrinsics.MIN(_b5p6od9s + (int)1 ,_ev4xhht5 ) - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_enkrihm0+(_b5p6od9s - 1))) );
					*(_plfm7z8g+(_b5p6od9s - 1)) = DBLE(_r7cfteg3);
					*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;//* 
					//*           Apply H(i)**H to A(i:m,i+1:n) from the left 
					//* 
					
					if (_b5p6od9s < _dxpq0xkr)
					_h7ckdrdn("Left" ,ref Unsafe.AsRef((_ev4xhht5 - _b5p6od9s) + (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.DCONJG(*(_enkrihm0+(_b5p6od9s - 1)) )) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,_apig8meb );
					*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = DCMPLX(*(_plfm7z8g+(_b5p6od9s - 1)));//* 
					
					if (_b5p6od9s < _dxpq0xkr)
					{
						//* 
						//*              Generate elementary reflector G(i) to annihilate 
						//*              A(i,i+2:n) 
						//* 
						
						_42wgkyoq(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
						_r7cfteg3 = *(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c));
						_4btmjfem(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref _r7cfteg3 ,(_vxfgpup9+(_b5p6od9s - 1) + (ILNumerics.F2NET.Intrinsics.MIN(_b5p6od9s + (int)2 ,_dxpq0xkr ) - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(*(_fus6fnrl+(_b5p6od9s - 1))) );
						*(_864fslqq+(_b5p6od9s - 1)) = DBLE(_r7cfteg3);
						*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;//* 
						//*              Apply G(i) to A(i+1:m,i+1:n) from the right 
						//* 
						
						_h7ckdrdn("Right" ,ref Unsafe.AsRef(_ev4xhht5 - _b5p6od9s) ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(*(_fus6fnrl+(_b5p6od9s - 1))) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,_apig8meb );
						_42wgkyoq(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
						*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)) = DCMPLX(*(_864fslqq+(_b5p6od9s - 1)));
					}
					else
					{
						
						*(_fus6fnrl+(_b5p6od9s - 1)) = _d0547bi2;
					}
					
Mark10:;
					// continue
				}
								}			}
		}
		else
		{
			//* 
			//*        Reduce to lower bidiagonal form 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn1139 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1139 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1139;
				for (__81fgg2count1139 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1139 + __81fgg2step1139) / __81fgg2step1139)), _b5p6od9s = __81fgg2dlsvn1139; __81fgg2count1139 != 0; __81fgg2count1139--, _b5p6od9s += (__81fgg2step1139)) {

				{
					//* 
					//*           Generate elementary reflector G(i) to annihilate A(i,i+1:n) 
					//* 
					
					_42wgkyoq(ref Unsafe.AsRef((_dxpq0xkr - _b5p6od9s) + (int)1) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
					_r7cfteg3 = *(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c));
					_4btmjfem(ref Unsafe.AsRef((_dxpq0xkr - _b5p6od9s) + (int)1) ,ref _r7cfteg3 ,(_vxfgpup9+(_b5p6od9s - 1) + (ILNumerics.F2NET.Intrinsics.MIN(_b5p6od9s + (int)1 ,_dxpq0xkr ) - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(*(_fus6fnrl+(_b5p6od9s - 1))) );
					*(_plfm7z8g+(_b5p6od9s - 1)) = DBLE(_r7cfteg3);
					*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;//* 
					//*           Apply G(i) to A(i+1:m,i:n) from the right 
					//* 
					
					if (_b5p6od9s < _ev4xhht5)
					_h7ckdrdn("Right" ,ref Unsafe.AsRef(_ev4xhht5 - _b5p6od9s) ,ref Unsafe.AsRef((_dxpq0xkr - _b5p6od9s) + (int)1) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(*(_fus6fnrl+(_b5p6od9s - 1))) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,_apig8meb );
					_42wgkyoq(ref Unsafe.AsRef((_dxpq0xkr - _b5p6od9s) + (int)1) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
					*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = DCMPLX(*(_plfm7z8g+(_b5p6od9s - 1)));//* 
					
					if (_b5p6od9s < _ev4xhht5)
					{
						//* 
						//*              Generate elementary reflector H(i) to annihilate 
						//*              A(i+2:m,i) 
						//* 
						
						_r7cfteg3 = *(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c));
						_4btmjfem(ref Unsafe.AsRef(_ev4xhht5 - _b5p6od9s) ,ref _r7cfteg3 ,(_vxfgpup9+(ILNumerics.F2NET.Intrinsics.MIN(_b5p6od9s + (int)2 ,_ev4xhht5 ) - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_enkrihm0+(_b5p6od9s - 1))) );
						*(_864fslqq+(_b5p6od9s - 1)) = DBLE(_r7cfteg3);
						*(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;//* 
						//*              Apply H(i)**H to A(i+1:m,i+1:n) from the left 
						//* 
						
						_h7ckdrdn("Left" ,ref Unsafe.AsRef(_ev4xhht5 - _b5p6od9s) ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.DCONJG(*(_enkrihm0+(_b5p6od9s - 1)) )) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,_apig8meb );
						*(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = DCMPLX(*(_864fslqq+(_b5p6od9s - 1)));
					}
					else
					{
						
						*(_enkrihm0+(_b5p6od9s - 1)) = _d0547bi2;
					}
					
Mark20:;
					// continue
				}
								}			}
		}
		
		return;//* 
		//*     End of ZGEBD2 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
