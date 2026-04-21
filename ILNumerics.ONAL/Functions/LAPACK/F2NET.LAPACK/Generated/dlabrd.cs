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
//*> \brief \b DLABRD reduces the first nb rows and columns of a general matrix to a bidiagonal form. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLABRD + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlabrd.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlabrd.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlabrd.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLABRD( M, N, NB, A, LDA, D, E, TAUQ, TAUP, X, LDX, Y, 
//*                          LDY ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            LDA, LDX, LDY, M, N, NB 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   A( LDA, * ), D( * ), E( * ), TAUP( * ), 
//*      $                   TAUQ( * ), X( LDX, * ), Y( LDY, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLABRD reduces the first NB rows and columns of a real general 
//*> m by n matrix A to upper or lower bidiagonal form by an orthogonal 
//*> transformation Q**T * A * P, and returns the matrices X and Y which 
//*> are needed to apply the transformation to the unreduced part of A. 
//*> 
//*> If m >= n, A is reduced to upper bidiagonal form; if m < n, to lower 
//*> bidiagonal form. 
//*> 
//*> This is an auxiliary routine called by DGEBRD 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The number of rows in the matrix A. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of columns in the matrix A. 
//*> \endverbatim 
//*> 
//*> \param[in] NB 
//*> \verbatim 
//*>          NB is INTEGER 
//*>          The number of leading rows and columns of A to be reduced. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is DOUBLE PRECISION array, dimension (LDA,N) 
//*>          On entry, the m by n general matrix to be reduced. 
//*>          On exit, the first NB rows and columns of the matrix are 
//*>          overwritten; the rest of the array is unchanged. 
//*>          If m >= n, elements on and below the diagonal in the first NB 
//*>            columns, with the array TAUQ, represent the orthogonal 
//*>            matrix Q as a product of elementary reflectors; and 
//*>            elements above the diagonal in the first NB rows, with the 
//*>            array TAUP, represent the orthogonal matrix P as a product 
//*>            of elementary reflectors. 
//*>          If m < n, elements below the diagonal in the first NB 
//*>            columns, with the array TAUQ, represent the orthogonal 
//*>            matrix Q as a product of elementary reflectors, and 
//*>            elements on and above the diagonal in the first NB rows, 
//*>            with the array TAUP, represent the orthogonal matrix P as 
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
//*>          D is DOUBLE PRECISION array, dimension (NB) 
//*>          The diagonal elements of the first NB rows and columns of 
//*>          the reduced matrix.  D(i) = A(i,i). 
//*> \endverbatim 
//*> 
//*> \param[out] E 
//*> \verbatim 
//*>          E is DOUBLE PRECISION array, dimension (NB) 
//*>          The off-diagonal elements of the first NB rows and columns of 
//*>          the reduced matrix. 
//*> \endverbatim 
//*> 
//*> \param[out] TAUQ 
//*> \verbatim 
//*>          TAUQ is DOUBLE PRECISION array, dimension (NB) 
//*>          The scalar factors of the elementary reflectors which 
//*>          represent the orthogonal matrix Q. See Further Details. 
//*> \endverbatim 
//*> 
//*> \param[out] TAUP 
//*> \verbatim 
//*>          TAUP is DOUBLE PRECISION array, dimension (NB) 
//*>          The scalar factors of the elementary reflectors which 
//*>          represent the orthogonal matrix P. See Further Details. 
//*> \endverbatim 
//*> 
//*> \param[out] X 
//*> \verbatim 
//*>          X is DOUBLE PRECISION array, dimension (LDX,NB) 
//*>          The m-by-nb matrix X required to update the unreduced part 
//*>          of A. 
//*> \endverbatim 
//*> 
//*> \param[in] LDX 
//*> \verbatim 
//*>          LDX is INTEGER 
//*>          The leading dimension of the array X. LDX >= max(1,M). 
//*> \endverbatim 
//*> 
//*> \param[out] Y 
//*> \verbatim 
//*>          Y is DOUBLE PRECISION array, dimension (LDY,NB) 
//*>          The n-by-nb matrix Y required to update the unreduced part 
//*>          of A. 
//*> \endverbatim 
//*> 
//*> \param[in] LDY 
//*> \verbatim 
//*>          LDY is INTEGER 
//*>          The leading dimension of the array Y. LDY >= max(1,N). 
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
//*> \ingroup doubleOTHERauxiliary 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  The matrices Q and P are represented as products of elementary 
//*>  reflectors: 
//*> 
//*>     Q = H(1) H(2) . . . H(nb)  and  P = G(1) G(2) . . . G(nb) 
//*> 
//*>  Each H(i) and G(i) has the form: 
//*> 
//*>     H(i) = I - tauq * v * v**T  and G(i) = I - taup * u * u**T 
//*> 
//*>  where tauq and taup are real scalars, and v and u are real vectors. 
//*> 
//*>  If m >= n, v(1:i-1) = 0, v(i) = 1, and v(i:m) is stored on exit in 
//*>  A(i:m,i); u(1:i) = 0, u(i+1) = 1, and u(i+1:n) is stored on exit in 
//*>  A(i,i+1:n); tauq is stored in TAUQ(i) and taup in TAUP(i). 
//*> 
//*>  If m < n, v(1:i) = 0, v(i+1) = 1, and v(i+1:m) is stored on exit in 
//*>  A(i+2:m,i); u(1:i-1) = 0, u(i) = 1, and u(i:n) is stored on exit in 
//*>  A(i,i+1:n); tauq is stored in TAUQ(i) and taup in TAUP(i). 
//*> 
//*>  The elements of the vectors v and u together form the m-by-nb matrix 
//*>  V and the nb-by-n matrix U**T which are needed, with X and Y, to apply 
//*>  the transformation to the unreduced part of the matrix, using a block 
//*>  update of the form:  A := A - V*Y**T - X*U**T. 
//*> 
//*>  The contents of A on exit are illustrated by the following examples 
//*>  with nb = 2: 
//*> 
//*>  m = 6 and n = 5 (m > n):          m = 5 and n = 6 (m < n): 
//*> 
//*>    (  1   1   u1  u1  u1 )           (  1   u1  u1  u1  u1  u1 ) 
//*>    (  v1  1   1   u2  u2 )           (  1   1   u2  u2  u2  u2 ) 
//*>    (  v1  v2  a   a   a  )           (  v1  1   a   a   a   a  ) 
//*>    (  v1  v2  a   a   a  )           (  v1  v2  a   a   a   a  ) 
//*>    (  v1  v2  a   a   a  )           (  v1  v2  a   a   a   a  ) 
//*>    (  v1  v2  a   a   a  ) 
//*> 
//*>  where a denotes an element of the original matrix which is unchanged, 
//*>  vi denotes an element of the vector defining H(i), and ui an element 
//*>  of the vector defining G(i). 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _xx6lxhwk(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Int32 _f7059815, Double* _vxfgpup9, ref Int32 _ocv8fk5c, Double* _plfm7z8g, Double* _864fslqq, Double* _enkrihm0, Double* _fus6fnrl, Double* _ta7zuy9k, ref Int32 _eeyyzhrs, Double* _f3z3edv0, ref Int32 _92z7u0w4)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Int32 _b5p6od9s =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK auxiliary routine (version 3.7.1) -- 
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
		//*     Quick return if possible 
		//* 
		
		if ((_ev4xhht5 <= (int)0) | (_dxpq0xkr <= (int)0))
		return;//* 
		
		if (_ev4xhht5 >= _dxpq0xkr)
		{
			//* 
			//*        Reduce to upper bidiagonal form 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn412 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step412 = (System.Int32)((int)1);
				System.Int32 __81fgg2count412;
				for (__81fgg2count412 = System.Math.Max(0, (System.Int32)(((System.Int32)(_f7059815) - __81fgg2dlsvn412 + __81fgg2step412) / __81fgg2step412)), _b5p6od9s = __81fgg2dlsvn412; __81fgg2count412 != 0; __81fgg2count412--, _b5p6od9s += (__81fgg2step412)) {

				{
					//* 
					//*           Update A(i:m,i) 
					//* 
					
					_t5wmtd1j("No transpose" ,ref Unsafe.AsRef((_ev4xhht5 - _b5p6od9s) + (int)1) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_f3z3edv0+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_92z7u0w4)),ref _92z7u0w4 ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
					_t5wmtd1j("No transpose" ,ref Unsafe.AsRef((_ev4xhht5 - _b5p6od9s) + (int)1) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_ta7zuy9k+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)),ref _eeyyzhrs ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );//* 
					//*           Generate reflection Q(i) to annihilate A(i+1:m,i) 
					//* 
					
					_a51k3mk0(ref Unsafe.AsRef((_ev4xhht5 - _b5p6od9s) + (int)1) ,ref Unsafe.AsRef(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))) ,(_vxfgpup9+(ILNumerics.F2NET.Intrinsics.MIN(_b5p6od9s + (int)1 ,_ev4xhht5 ) - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_enkrihm0+(_b5p6od9s - 1))) );
					*(_plfm7z8g+(_b5p6od9s - 1)) = *(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c));
					if (_b5p6od9s < _dxpq0xkr)
					{
						
						*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;//* 
						//*              Compute Y(i+1:n,i) 
						//* 
						
						_t5wmtd1j("Transpose" ,ref Unsafe.AsRef((_ev4xhht5 - _b5p6od9s) + (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,(_f3z3edv0+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_92z7u0w4)),ref Unsafe.AsRef((int)1) );
						_t5wmtd1j("Transpose" ,ref Unsafe.AsRef((_ev4xhht5 - _b5p6od9s) + (int)1) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,(_f3z3edv0+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_92z7u0w4)),ref Unsafe.AsRef((int)1) );
						_t5wmtd1j("No transpose" ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_f3z3edv0+(_b5p6od9s + (int)1 - 1) + ((int)1 - 1) * 1 * (_92z7u0w4)),ref _92z7u0w4 ,(_f3z3edv0+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_92z7u0w4)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_f3z3edv0+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_92z7u0w4)),ref Unsafe.AsRef((int)1) );
						_t5wmtd1j("Transpose" ,ref Unsafe.AsRef((_ev4xhht5 - _b5p6od9s) + (int)1) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_ta7zuy9k+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)),ref _eeyyzhrs ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,(_f3z3edv0+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_92z7u0w4)),ref Unsafe.AsRef((int)1) );
						_t5wmtd1j("Transpose" ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_f3z3edv0+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_92z7u0w4)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_f3z3edv0+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_92z7u0w4)),ref Unsafe.AsRef((int)1) );
						_f6jqcjk1(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(*(_enkrihm0+(_b5p6od9s - 1))) ,(_f3z3edv0+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_92z7u0w4)),ref Unsafe.AsRef((int)1) );//* 
						//*              Update A(i,i+1:n) 
						//* 
						
						_t5wmtd1j("No transpose" ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref _b5p6od9s ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_f3z3edv0+(_b5p6od9s + (int)1 - 1) + ((int)1 - 1) * 1 * (_92z7u0w4)),ref _92z7u0w4 ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
						_t5wmtd1j("Transpose" ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_ta7zuy9k+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)),ref _eeyyzhrs ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );//* 
						//*              Generate reflection P(i) to annihilate A(i,i+2:n) 
						//* 
						
						_a51k3mk0(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c))) ,(_vxfgpup9+(_b5p6od9s - 1) + (ILNumerics.F2NET.Intrinsics.MIN(_b5p6od9s + (int)2 ,_dxpq0xkr ) - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(*(_fus6fnrl+(_b5p6od9s - 1))) );
						*(_864fslqq+(_b5p6od9s - 1)) = *(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c));
						*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;//* 
						//*              Compute X(i+1:m,i) 
						//* 
						
						_t5wmtd1j("No transpose" ,ref Unsafe.AsRef(_ev4xhht5 - _b5p6od9s) ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_d0547bi2) ,(_ta7zuy9k+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_eeyyzhrs)),ref Unsafe.AsRef((int)1) );
						_t5wmtd1j("Transpose" ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref _b5p6od9s ,ref Unsafe.AsRef(_kxg5drh2) ,(_f3z3edv0+(_b5p6od9s + (int)1 - 1) + ((int)1 - 1) * 1 * (_92z7u0w4)),ref _92z7u0w4 ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_d0547bi2) ,(_ta7zuy9k+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_eeyyzhrs)),ref Unsafe.AsRef((int)1) );
						_t5wmtd1j("No transpose" ,ref Unsafe.AsRef(_ev4xhht5 - _b5p6od9s) ,ref _b5p6od9s ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_ta7zuy9k+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_eeyyzhrs)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_ta7zuy9k+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_eeyyzhrs)),ref Unsafe.AsRef((int)1) );
						_t5wmtd1j("No transpose" ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_d0547bi2) ,(_ta7zuy9k+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_eeyyzhrs)),ref Unsafe.AsRef((int)1) );
						_t5wmtd1j("No transpose" ,ref Unsafe.AsRef(_ev4xhht5 - _b5p6od9s) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_ta7zuy9k+(_b5p6od9s + (int)1 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)),ref _eeyyzhrs ,(_ta7zuy9k+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_eeyyzhrs)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_ta7zuy9k+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_eeyyzhrs)),ref Unsafe.AsRef((int)1) );
						_f6jqcjk1(ref Unsafe.AsRef(_ev4xhht5 - _b5p6od9s) ,ref Unsafe.AsRef(*(_fus6fnrl+(_b5p6od9s - 1))) ,(_ta7zuy9k+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_eeyyzhrs)),ref Unsafe.AsRef((int)1) );
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
				System.Int32 __81fgg2dlsvn413 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step413 = (System.Int32)((int)1);
				System.Int32 __81fgg2count413;
				for (__81fgg2count413 = System.Math.Max(0, (System.Int32)(((System.Int32)(_f7059815) - __81fgg2dlsvn413 + __81fgg2step413) / __81fgg2step413)), _b5p6od9s = __81fgg2dlsvn413; __81fgg2count413 != 0; __81fgg2count413--, _b5p6od9s += (__81fgg2step413)) {

				{
					//* 
					//*           Update A(i,i:n) 
					//* 
					
					_t5wmtd1j("No transpose" ,ref Unsafe.AsRef((_dxpq0xkr - _b5p6od9s) + (int)1) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_f3z3edv0+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_92z7u0w4)),ref _92z7u0w4 ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
					_t5wmtd1j("Transpose" ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef((_dxpq0xkr - _b5p6od9s) + (int)1) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_ta7zuy9k+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)),ref _eeyyzhrs ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );//* 
					//*           Generate reflection P(i) to annihilate A(i,i+1:n) 
					//* 
					
					_a51k3mk0(ref Unsafe.AsRef((_dxpq0xkr - _b5p6od9s) + (int)1) ,ref Unsafe.AsRef(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))) ,(_vxfgpup9+(_b5p6od9s - 1) + (ILNumerics.F2NET.Intrinsics.MIN(_b5p6od9s + (int)1 ,_dxpq0xkr ) - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(*(_fus6fnrl+(_b5p6od9s - 1))) );
					*(_plfm7z8g+(_b5p6od9s - 1)) = *(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c));
					if (_b5p6od9s < _ev4xhht5)
					{
						
						*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;//* 
						//*              Compute X(i+1:m,i) 
						//* 
						
						_t5wmtd1j("No transpose" ,ref Unsafe.AsRef(_ev4xhht5 - _b5p6od9s) ,ref Unsafe.AsRef((_dxpq0xkr - _b5p6od9s) + (int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_d0547bi2) ,(_ta7zuy9k+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_eeyyzhrs)),ref Unsafe.AsRef((int)1) );
						_t5wmtd1j("Transpose" ,ref Unsafe.AsRef((_dxpq0xkr - _b5p6od9s) + (int)1) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_f3z3edv0+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_92z7u0w4)),ref _92z7u0w4 ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_d0547bi2) ,(_ta7zuy9k+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_eeyyzhrs)),ref Unsafe.AsRef((int)1) );
						_t5wmtd1j("No transpose" ,ref Unsafe.AsRef(_ev4xhht5 - _b5p6od9s) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_ta7zuy9k+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_eeyyzhrs)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_ta7zuy9k+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_eeyyzhrs)),ref Unsafe.AsRef((int)1) );
						_t5wmtd1j("No transpose" ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef((_dxpq0xkr - _b5p6od9s) + (int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_d0547bi2) ,(_ta7zuy9k+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_eeyyzhrs)),ref Unsafe.AsRef((int)1) );
						_t5wmtd1j("No transpose" ,ref Unsafe.AsRef(_ev4xhht5 - _b5p6od9s) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_ta7zuy9k+(_b5p6od9s + (int)1 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)),ref _eeyyzhrs ,(_ta7zuy9k+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_eeyyzhrs)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_ta7zuy9k+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_eeyyzhrs)),ref Unsafe.AsRef((int)1) );
						_f6jqcjk1(ref Unsafe.AsRef(_ev4xhht5 - _b5p6od9s) ,ref Unsafe.AsRef(*(_fus6fnrl+(_b5p6od9s - 1))) ,(_ta7zuy9k+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_eeyyzhrs)),ref Unsafe.AsRef((int)1) );//* 
						//*              Update A(i+1:m,i) 
						//* 
						
						_t5wmtd1j("No transpose" ,ref Unsafe.AsRef(_ev4xhht5 - _b5p6od9s) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_f3z3edv0+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_92z7u0w4)),ref _92z7u0w4 ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
						_t5wmtd1j("No transpose" ,ref Unsafe.AsRef(_ev4xhht5 - _b5p6od9s) ,ref _b5p6od9s ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_ta7zuy9k+(_b5p6od9s + (int)1 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)),ref _eeyyzhrs ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );//* 
						//*              Generate reflection Q(i) to annihilate A(i+2:m,i) 
						//* 
						
						_a51k3mk0(ref Unsafe.AsRef(_ev4xhht5 - _b5p6od9s) ,ref Unsafe.AsRef(*(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))) ,(_vxfgpup9+(ILNumerics.F2NET.Intrinsics.MIN(_b5p6od9s + (int)2 ,_ev4xhht5 ) - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_enkrihm0+(_b5p6od9s - 1))) );
						*(_864fslqq+(_b5p6od9s - 1)) = *(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c));
						*(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;//* 
						//*              Compute Y(i+1:n,i) 
						//* 
						
						_t5wmtd1j("Transpose" ,ref Unsafe.AsRef(_ev4xhht5 - _b5p6od9s) ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,(_f3z3edv0+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_92z7u0w4)),ref Unsafe.AsRef((int)1) );
						_t5wmtd1j("Transpose" ,ref Unsafe.AsRef(_ev4xhht5 - _b5p6od9s) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,(_f3z3edv0+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_92z7u0w4)),ref Unsafe.AsRef((int)1) );
						_t5wmtd1j("No transpose" ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_f3z3edv0+(_b5p6od9s + (int)1 - 1) + ((int)1 - 1) * 1 * (_92z7u0w4)),ref _92z7u0w4 ,(_f3z3edv0+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_92z7u0w4)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_f3z3edv0+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_92z7u0w4)),ref Unsafe.AsRef((int)1) );
						_t5wmtd1j("Transpose" ,ref Unsafe.AsRef(_ev4xhht5 - _b5p6od9s) ,ref _b5p6od9s ,ref Unsafe.AsRef(_kxg5drh2) ,(_ta7zuy9k+(_b5p6od9s + (int)1 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)),ref _eeyyzhrs ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,(_f3z3edv0+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_92z7u0w4)),ref Unsafe.AsRef((int)1) );
						_t5wmtd1j("Transpose" ,ref _b5p6od9s ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_f3z3edv0+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_92z7u0w4)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_f3z3edv0+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_92z7u0w4)),ref Unsafe.AsRef((int)1) );
						_f6jqcjk1(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(*(_enkrihm0+(_b5p6od9s - 1))) ,(_f3z3edv0+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_92z7u0w4)),ref Unsafe.AsRef((int)1) );
					}
					
Mark20:;
					// continue
				}
								}			}
		}
		
		return;//* 
		//*     End of DLABRD 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
