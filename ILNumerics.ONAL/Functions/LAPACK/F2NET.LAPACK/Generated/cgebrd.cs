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
//*> \brief \b CGEBRD 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CGEBRD + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/cgebrd.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/cgebrd.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/cgebrd.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CGEBRD( M, N, A, LDA, D, E, TAUQ, TAUP, WORK, LWORK, 
//*                          INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INFO, LDA, LWORK, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               D( * ), E( * ) 
//*       COMPLEX            A( LDA, * ), TAUP( * ), TAUQ( * ), 
//*      $                   WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CGEBRD reduces a general complex M-by-N matrix A to upper or lower 
//*> bidiagonal form B by a unitary transformation: Q**H * A * P = B. 
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
//*>          A is COMPLEX array, dimension (LDA,N) 
//*>          On entry, the M-by-N general matrix to be reduced. 
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
//*>          D is REAL array, dimension (min(M,N)) 
//*>          The diagonal elements of the bidiagonal matrix B: 
//*>          D(i) = A(i,i). 
//*> \endverbatim 
//*> 
//*> \param[out] E 
//*> \verbatim 
//*>          E is REAL array, dimension (min(M,N)-1) 
//*>          The off-diagonal elements of the bidiagonal matrix B: 
//*>          if m >= n, E(i) = A(i,i+1) for i = 1,2,...,n-1; 
//*>          if m < n, E(i) = A(i+1,i) for i = 1,2,...,m-1. 
//*> \endverbatim 
//*> 
//*> \param[out] TAUQ 
//*> \verbatim 
//*>          TAUQ is COMPLEX array, dimension (min(M,N)) 
//*>          The scalar factors of the elementary reflectors which 
//*>          represent the unitary matrix Q. See Further Details. 
//*> \endverbatim 
//*> 
//*> \param[out] TAUP 
//*> \verbatim 
//*>          TAUP is COMPLEX array, dimension (min(M,N)) 
//*>          The scalar factors of the elementary reflectors which 
//*>          represent the unitary matrix P. See Further Details. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is COMPLEX array, dimension (MAX(1,LWORK)) 
//*>          On exit, if INFO = 0, WORK(1) returns the optimal LWORK. 
//*> \endverbatim 
//*> 
//*> \param[in] LWORK 
//*> \verbatim 
//*>          LWORK is INTEGER 
//*>          The length of the array WORK.  LWORK >= max(1,M,N). 
//*>          For optimum performance LWORK >= (M+N)*NB, where NB 
//*>          is the optimal blocksize. 
//*> 
//*>          If LWORK = -1, then a workspace query is assumed; the routine 
//*>          only calculates the optimal size of the WORK array, returns 
//*>          this value as the first entry of the WORK array, and no error 
//*>          message related to LWORK is issued by XERBLA. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit. 
//*>          < 0:  if INFO = -i, the i-th argument had an illegal value. 
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
//*> \date November 2017 
//* 
//*> \ingroup complexGEcomputational 
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
//*>  where tauq and taup are complex scalars, and v and u are complex 
//*>  vectors; v(1:i) = 0, v(i+1) = 1, and v(i+2:m) is stored on exit in 
//*>  A(i+2:m,i); u(1:i-1) = 0, u(i) = 1, and u(i+1:n) is stored on exit in 
//*>  A(i,i+1:n); tauq is stored in TAUQ(i) and taup in TAUP(i). 
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

	 
	public static void _u589zq9p(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, Single* _plfm7z8g, Single* _864fslqq, fcomplex* _enkrihm0, fcomplex* _fus6fnrl, fcomplex* _apig8meb, ref Int32 _6fnxzlyp, ref Int32 _gro5yvfo)
	{
#region variable declarations
fcomplex _kxg5drh2 =   new fcomplex(1f,0f);
Boolean _lhlgm7z5 =  default;
Int32 _b5p6od9s =  default;
Int32 _itfnbz60 =  default;
Int32 _znpjgsef =  default;
Int32 _cto1c31o =  default;
Int32 _agmtmdtw =  default;
Int32 _e4ueamrn =  default;
Int32 _qaseb1y7 =  default;
Int32 _f7059815 =  default;
Int32 _o80jnixx =  default;
Int32 _rtlyoyz3 =  default;
Int32 _m7jwdaw8 =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK computational routine (version 3.8.0) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     November 2017 
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
		//*     Test the input parameters 
		//* 
		
		_gro5yvfo = (int)0;
		_f7059815 = ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_4mvd6e4d(ref Unsafe.AsRef((int)1) ,"CGEBRD" ," " ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ) );
		_e4ueamrn = ((_ev4xhht5 + _dxpq0xkr) * _f7059815);
		*(_apig8meb+((int)1 - 1)) = CMPLX(ILNumerics.F2NET.Intrinsics.REAL(_e4ueamrn ));
		_lhlgm7z5 = (_6fnxzlyp == (int)-1);
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
		else
		if ((_6fnxzlyp < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_ev4xhht5 ,_dxpq0xkr )) & (!(_lhlgm7z5)))
		{
			
			_gro5yvfo = (int)-10;
		}
		
		if (_gro5yvfo < (int)0)
		{
			
			_ut9qalzx("CGEBRD" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		else
		if (_lhlgm7z5)
		{
			
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		_qaseb1y7 = ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 ,_dxpq0xkr );
		if (_qaseb1y7 == (int)0)
		{
			
			*(_apig8meb+((int)1 - 1)) = CMPLX((int)1);
			return;
		}
		//* 
		
		_m7jwdaw8 = ILNumerics.F2NET.Intrinsics.MAX(_ev4xhht5 ,_dxpq0xkr );
		_cto1c31o = _ev4xhht5;
		_agmtmdtw = _dxpq0xkr;//* 
		
		if ((_f7059815 > (int)1) & (_f7059815 < _qaseb1y7))
		{
			//* 
			//*        Set the crossover point NX. 
			//* 
			
			_rtlyoyz3 = ILNumerics.F2NET.Intrinsics.MAX(_f7059815 ,_4mvd6e4d(ref Unsafe.AsRef((int)3) ,"CGEBRD" ," " ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ) );//* 
			//*        Determine when to switch from blocked to unblocked code. 
			//* 
			
			if (_rtlyoyz3 < _qaseb1y7)
			{
				
				_m7jwdaw8 = ((_ev4xhht5 + _dxpq0xkr) * _f7059815);
				if (_6fnxzlyp < _m7jwdaw8)
				{
					//* 
					//*              Not enough work space for the optimal NB, consider using 
					//*              a smaller block size. 
					//* 
					
					_o80jnixx = _4mvd6e4d(ref Unsafe.AsRef((int)2) ,"CGEBRD" ," " ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) );
					if (_6fnxzlyp >= ((_ev4xhht5 + _dxpq0xkr) * _o80jnixx))
					{
						
						_f7059815 = (_6fnxzlyp / (_ev4xhht5 + _dxpq0xkr));
					}
					else
					{
						
						_f7059815 = (int)1;
						_rtlyoyz3 = _qaseb1y7;
					}
					
				}
				
			}
			
		}
		else
		{
			
			_rtlyoyz3 = _qaseb1y7;
		}
		//* 
		
		{
			System.Int32 __81fgg2dlsvn918 = (System.Int32)((int)1);
			System.Int32 __81fgg2step918 = (System.Int32)(_f7059815);
			System.Int32 __81fgg2count918;
			for (__81fgg2count918 = System.Math.Max(0, (System.Int32)(((System.Int32)(_qaseb1y7 - _rtlyoyz3) - __81fgg2dlsvn918 + __81fgg2step918) / __81fgg2step918)), _b5p6od9s = __81fgg2dlsvn918; __81fgg2count918 != 0; __81fgg2count918--, _b5p6od9s += (__81fgg2step918)) {

			{
				//* 
				//*        Reduce rows and columns i:i+ib-1 to bidiagonal form and return 
				//*        the matrices X and Y which are needed to update the unreduced 
				//*        part of the matrix 
				//* 
				
				_uo73vad1(ref Unsafe.AsRef((_ev4xhht5 - _b5p6od9s) + (int)1) ,ref Unsafe.AsRef((_dxpq0xkr - _b5p6od9s) + (int)1) ,ref _f7059815 ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_plfm7z8g+(_b5p6od9s - 1)),(_864fslqq+(_b5p6od9s - 1)),(_enkrihm0+(_b5p6od9s - 1)),(_fus6fnrl+(_b5p6od9s - 1)),_apig8meb ,ref _cto1c31o ,(_apig8meb+((_cto1c31o * _f7059815) + (int)1 - 1)),ref _agmtmdtw );//* 
				//*        Update the trailing submatrix A(i+ib:m,i+ib:n), using 
				//*        an update of the form  A := A - V*Y**H - X*U**H 
				//* 
				
				_5p0w9905("No transpose" ,"Conjugate transpose" ,ref Unsafe.AsRef(((_ev4xhht5 - _b5p6od9s) - _f7059815) + (int)1) ,ref Unsafe.AsRef(((_dxpq0xkr - _b5p6od9s) - _f7059815) + (int)1) ,ref _f7059815 ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+(_b5p6od9s + _f7059815 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_apig8meb+(((_cto1c31o * _f7059815) + _f7059815) + (int)1 - 1)),ref _agmtmdtw ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s + _f7059815 - 1) + (_b5p6od9s + _f7059815 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
				_5p0w9905("No transpose" ,"No transpose" ,ref Unsafe.AsRef(((_ev4xhht5 - _b5p6od9s) - _f7059815) + (int)1) ,ref Unsafe.AsRef(((_dxpq0xkr - _b5p6od9s) - _f7059815) + (int)1) ,ref _f7059815 ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_apig8meb+(_f7059815 + (int)1 - 1)),ref _cto1c31o ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + _f7059815 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s + _f7059815 - 1) + (_b5p6od9s + _f7059815 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );//* 
				//*        Copy diagonal and off-diagonal elements of B back into A 
				//* 
				
				if (_ev4xhht5 >= _dxpq0xkr)
				{
					
					{
						System.Int32 __81fgg2dlsvn919 = (System.Int32)(_b5p6od9s);
						const System.Int32 __81fgg2step919 = (System.Int32)((int)1);
						System.Int32 __81fgg2count919;
						for (__81fgg2count919 = System.Math.Max(0, (System.Int32)(((System.Int32)((_b5p6od9s + _f7059815) - (int)1) - __81fgg2dlsvn919 + __81fgg2step919) / __81fgg2step919)), _znpjgsef = __81fgg2dlsvn919; __81fgg2count919 != 0; __81fgg2count919--, _znpjgsef += (__81fgg2step919)) {

						{
							
							*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = CMPLX(*(_plfm7z8g+(_znpjgsef - 1)));
							*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef + (int)1 - 1) * 1 * (_ocv8fk5c)) = CMPLX(*(_864fslqq+(_znpjgsef - 1)));
Mark10:;
							// continue
						}
												}					}
				}
				else
				{
					
					{
						System.Int32 __81fgg2dlsvn920 = (System.Int32)(_b5p6od9s);
						const System.Int32 __81fgg2step920 = (System.Int32)((int)1);
						System.Int32 __81fgg2count920;
						for (__81fgg2count920 = System.Math.Max(0, (System.Int32)(((System.Int32)((_b5p6od9s + _f7059815) - (int)1) - __81fgg2dlsvn920 + __81fgg2step920) / __81fgg2step920)), _znpjgsef = __81fgg2dlsvn920; __81fgg2count920 != 0; __81fgg2count920--, _znpjgsef += (__81fgg2step920)) {

						{
							
							*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = CMPLX(*(_plfm7z8g+(_znpjgsef - 1)));
							*(_vxfgpup9+(_znpjgsef + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = CMPLX(*(_864fslqq+(_znpjgsef - 1)));
Mark20:;
							// continue
						}
												}					}
				}
				
Mark30:;
				// continue
			}
						}		}//* 
		//*     Use unblocked code to reduce the remainder of the matrix 
		//* 
		
		_t6ejoyuo(ref Unsafe.AsRef((_ev4xhht5 - _b5p6od9s) + (int)1) ,ref Unsafe.AsRef((_dxpq0xkr - _b5p6od9s) + (int)1) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_plfm7z8g+(_b5p6od9s - 1)),(_864fslqq+(_b5p6od9s - 1)),(_enkrihm0+(_b5p6od9s - 1)),(_fus6fnrl+(_b5p6od9s - 1)),_apig8meb ,ref _itfnbz60 );
		*(_apig8meb+((int)1 - 1)) = CMPLX(_m7jwdaw8);
		return;//* 
		//*     End of CGEBRD 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
