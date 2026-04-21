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
//*> \brief \b SLASDA computes the singular value decomposition (SVD) of a real upper bidiagonal matrix with diagonal d and off-diagonal e. Used by sbdsdc. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLASDA + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slasda.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slasda.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slasda.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLASDA( ICOMPQ, SMLSIZ, N, SQRE, D, E, U, LDU, VT, K, 
//*                          DIFL, DIFR, Z, POLES, GIVPTR, GIVCOL, LDGCOL, 
//*                          PERM, GIVNUM, C, S, WORK, IWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            ICOMPQ, INFO, LDGCOL, LDU, N, SMLSIZ, SQRE 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            GIVCOL( LDGCOL, * ), GIVPTR( * ), IWORK( * ), 
//*      $                   K( * ), PERM( LDGCOL, * ) 
//*       REAL               C( * ), D( * ), DIFL( LDU, * ), DIFR( LDU, * ), 
//*      $                   E( * ), GIVNUM( LDU, * ), POLES( LDU, * ), 
//*      $                   S( * ), U( LDU, * ), VT( LDU, * ), WORK( * ), 
//*      $                   Z( LDU, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> Using a divide and conquer approach, SLASDA computes the singular 
//*> value decomposition (SVD) of a real upper bidiagonal N-by-M matrix 
//*> B with diagonal D and offdiagonal E, where M = N + SQRE. The 
//*> algorithm computes the singular values in the SVD B = U * S * VT. 
//*> The orthogonal matrices U and VT are optionally computed in 
//*> compact form. 
//*> 
//*> A related subroutine, SLASD0, computes the singular values and 
//*> the singular vectors in explicit form. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] ICOMPQ 
//*> \verbatim 
//*>          ICOMPQ is INTEGER 
//*>         Specifies whether singular vectors are to be computed 
//*>         in compact form, as follows 
//*>         = 0: Compute singular values only. 
//*>         = 1: Compute singular vectors of upper bidiagonal 
//*>              matrix in compact form. 
//*> \endverbatim 
//*> 
//*> \param[in] SMLSIZ 
//*> \verbatim 
//*>          SMLSIZ is INTEGER 
//*>         The maximum size of the subproblems at the bottom of the 
//*>         computation tree. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>         The row dimension of the upper bidiagonal matrix. This is 
//*>         also the dimension of the main diagonal array D. 
//*> \endverbatim 
//*> 
//*> \param[in] SQRE 
//*> \verbatim 
//*>          SQRE is INTEGER 
//*>         Specifies the column dimension of the bidiagonal matrix. 
//*>         = 0: The bidiagonal matrix has column dimension M = N; 
//*>         = 1: The bidiagonal matrix has column dimension M = N + 1. 
//*> \endverbatim 
//*> 
//*> \param[in,out] D 
//*> \verbatim 
//*>          D is REAL array, dimension ( N ) 
//*>         On entry D contains the main diagonal of the bidiagonal 
//*>         matrix. On exit D, if INFO = 0, contains its singular values. 
//*> \endverbatim 
//*> 
//*> \param[in] E 
//*> \verbatim 
//*>          E is REAL array, dimension ( M-1 ) 
//*>         Contains the subdiagonal entries of the bidiagonal matrix. 
//*>         On exit, E has been destroyed. 
//*> \endverbatim 
//*> 
//*> \param[out] U 
//*> \verbatim 
//*>          U is REAL array, 
//*>         dimension ( LDU, SMLSIZ ) if ICOMPQ = 1, and not referenced 
//*>         if ICOMPQ = 0. If ICOMPQ = 1, on exit, U contains the left 
//*>         singular vector matrices of all subproblems at the bottom 
//*>         level. 
//*> \endverbatim 
//*> 
//*> \param[in] LDU 
//*> \verbatim 
//*>          LDU is INTEGER, LDU = > N. 
//*>         The leading dimension of arrays U, VT, DIFL, DIFR, POLES, 
//*>         GIVNUM, and Z. 
//*> \endverbatim 
//*> 
//*> \param[out] VT 
//*> \verbatim 
//*>          VT is REAL array, 
//*>         dimension ( LDU, SMLSIZ+1 ) if ICOMPQ = 1, and not referenced 
//*>         if ICOMPQ = 0. If ICOMPQ = 1, on exit, VT**T contains the right 
//*>         singular vector matrices of all subproblems at the bottom 
//*>         level. 
//*> \endverbatim 
//*> 
//*> \param[out] K 
//*> \verbatim 
//*>          K is INTEGER array, dimension ( N ) 
//*>         if ICOMPQ = 1 and dimension 1 if ICOMPQ = 0. 
//*>         If ICOMPQ = 1, on exit, K(I) is the dimension of the I-th 
//*>         secular equation on the computation tree. 
//*> \endverbatim 
//*> 
//*> \param[out] DIFL 
//*> \verbatim 
//*>          DIFL is REAL array, dimension ( LDU, NLVL ), 
//*>         where NLVL = floor(log_2 (N/SMLSIZ))). 
//*> \endverbatim 
//*> 
//*> \param[out] DIFR 
//*> \verbatim 
//*>          DIFR is REAL array, 
//*>                  dimension ( LDU, 2 * NLVL ) if ICOMPQ = 1 and 
//*>                  dimension ( N ) if ICOMPQ = 0. 
//*>         If ICOMPQ = 1, on exit, DIFL(1:N, I) and DIFR(1:N, 2 * I - 1) 
//*>         record distances between singular values on the I-th 
//*>         level and singular values on the (I -1)-th level, and 
//*>         DIFR(1:N, 2 * I ) contains the normalizing factors for 
//*>         the right singular vector matrix. See SLASD8 for details. 
//*> \endverbatim 
//*> 
//*> \param[out] Z 
//*> \verbatim 
//*>          Z is REAL array, 
//*>                  dimension ( LDU, NLVL ) if ICOMPQ = 1 and 
//*>                  dimension ( N ) if ICOMPQ = 0. 
//*>         The first K elements of Z(1, I) contain the components of 
//*>         the deflation-adjusted updating row vector for subproblems 
//*>         on the I-th level. 
//*> \endverbatim 
//*> 
//*> \param[out] POLES 
//*> \verbatim 
//*>          POLES is REAL array, 
//*>         dimension ( LDU, 2 * NLVL ) if ICOMPQ = 1, and not referenced 
//*>         if ICOMPQ = 0. If ICOMPQ = 1, on exit, POLES(1, 2*I - 1) and 
//*>         POLES(1, 2*I) contain  the new and old singular values 
//*>         involved in the secular equations on the I-th level. 
//*> \endverbatim 
//*> 
//*> \param[out] GIVPTR 
//*> \verbatim 
//*>          GIVPTR is INTEGER array, 
//*>         dimension ( N ) if ICOMPQ = 1, and not referenced if 
//*>         ICOMPQ = 0. If ICOMPQ = 1, on exit, GIVPTR( I ) records 
//*>         the number of Givens rotations performed on the I-th 
//*>         problem on the computation tree. 
//*> \endverbatim 
//*> 
//*> \param[out] GIVCOL 
//*> \verbatim 
//*>          GIVCOL is INTEGER array, 
//*>         dimension ( LDGCOL, 2 * NLVL ) if ICOMPQ = 1, and not 
//*>         referenced if ICOMPQ = 0. If ICOMPQ = 1, on exit, for each I, 
//*>         GIVCOL(1, 2 *I - 1) and GIVCOL(1, 2 *I) record the locations 
//*>         of Givens rotations performed on the I-th level on the 
//*>         computation tree. 
//*> \endverbatim 
//*> 
//*> \param[in] LDGCOL 
//*> \verbatim 
//*>          LDGCOL is INTEGER, LDGCOL = > N. 
//*>         The leading dimension of arrays GIVCOL and PERM. 
//*> \endverbatim 
//*> 
//*> \param[out] PERM 
//*> \verbatim 
//*>          PERM is INTEGER array, dimension ( LDGCOL, NLVL ) 
//*>         if ICOMPQ = 1, and not referenced 
//*>         if ICOMPQ = 0. If ICOMPQ = 1, on exit, PERM(1, I) records 
//*>         permutations done on the I-th level of the computation tree. 
//*> \endverbatim 
//*> 
//*> \param[out] GIVNUM 
//*> \verbatim 
//*>          GIVNUM is REAL array, 
//*>         dimension ( LDU,  2 * NLVL ) if ICOMPQ = 1, and not 
//*>         referenced if ICOMPQ = 0. If ICOMPQ = 1, on exit, for each I, 
//*>         GIVNUM(1, 2 *I - 1) and GIVNUM(1, 2 *I) record the C- and S- 
//*>         values of Givens rotations performed on the I-th level on 
//*>         the computation tree. 
//*> \endverbatim 
//*> 
//*> \param[out] C 
//*> \verbatim 
//*>          C is REAL array, 
//*>         dimension ( N ) if ICOMPQ = 1, and dimension 1 if ICOMPQ = 0. 
//*>         If ICOMPQ = 1 and the I-th subproblem is not square, on exit, 
//*>         C( I ) contains the C-value of a Givens rotation related to 
//*>         the right null space of the I-th subproblem. 
//*> \endverbatim 
//*> 
//*> \param[out] S 
//*> \verbatim 
//*>          S is REAL array, dimension ( N ) if 
//*>         ICOMPQ = 1, and dimension 1 if ICOMPQ = 0. If ICOMPQ = 1 
//*>         and the I-th subproblem is not square, on exit, S( I ) 
//*>         contains the S-value of a Givens rotation related to 
//*>         the right null space of the I-th subproblem. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is REAL array, dimension 
//*>         (6 * N + (SMLSIZ + 1)*(SMLSIZ + 1)). 
//*> \endverbatim 
//*> 
//*> \param[out] IWORK 
//*> \verbatim 
//*>          IWORK is INTEGER array, dimension (7*N). 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit. 
//*>          < 0:  if INFO = -i, the i-th argument had an illegal value. 
//*>          > 0:  if INFO = 1, a singular value did not converge 
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
//*> \ingroup OTHERauxiliary 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>     Ming Gu and Huan Ren, Computer Science Division, University of 
//*>     California at Berkeley, USA 
//*> 
//*  ===================================================================== 

	 
	public static void _cui96f25(ref Int32 _y1be9goe, ref Int32 _q1xpyios, ref Int32 _dxpq0xkr, ref Int32 _9qyq7j3e, Single* _plfm7z8g, Single* _864fslqq, Single* _7u55mqkq, ref Int32 _u6e6d39b, Single* _xdbczr8u, Int32* _umlkckdg, Single* _i8976ehd, Single* _doljbvm2, Single* _7e60fcso, Single* _7nk40y8b, Int32* _8vecpt74, Int32* _0zwn6fsy, ref Int32 _uhi0ls8i, Int32* _umao48xu, Single* _gh266ol1, Single* _3crf0qn3, Single* _irk8i6qr, Single* _apig8meb, Int32* _4b6rt45i, ref Int32 _gro5yvfo)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Single _kxg5drh2 =  1f;
Int32 _b5p6od9s =  default;
Int32 _egqdmelt =  default;
Int32 _8jzcrkri =  default;
Int32 _ca64lzpg =  default;
Int32 _n6rt1nys =  default;
Int32 _3dimb00t =  default;
Int32 _q8fqp221 =  default;
Int32 _m1gysdbg =  default;
Int32 _426n50rt =  default;
Int32 _znpjgsef =  default;
Int32 _es1scagl =  default;
Int32 _jnnmt81a =  default;
Int32 _u0afxfs0 =  default;
Int32 _4ekj6112 =  default;
Int32 _ev4xhht5 =  default;
Int32 _bcsi4mx0 =  default;
Int32 _rwm6akyl =  default;
Int32 _v4ofzyw5 =  default;
Int32 _b53e0l58 =  default;
Int32 _9bq9s7q7 =  default;
Int32 _zx57w4aj =  default;
Int32 _cyu21nam =  default;
Int32 _qwh8ts9f =  default;
Int32 _0n683y3x =  default;
Int32 _oqpc3yjg =  default;
Int32 _5tcb1chw =  default;
Int32 _bds60snh =  default;
Int32 _tg62ial7 =  default;
Int32 _a6b624z0 =  default;
Int32 _c7mgu9pg =  default;
Int32 _kku1nkf4 =  default;
Int32 _x0hi7bqn =  default;
Int32 _w7xcjdw0 =  default;
Int32 _0zxfmdr3 =  default;
Int32 _ppzorcqs =  default;
Int32 _h1q3br85 =  default;
Single _r7cfteg3 =  default;
Single _bafcbx97 =  default;
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
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input parameters. 
		//* 
		
		_gro5yvfo = (int)0;//* 
		
		if ((_y1be9goe < (int)0) | (_y1be9goe > (int)1))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (_q1xpyios < (int)3)
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if ((_9qyq7j3e < (int)0) | (_9qyq7j3e > (int)1))
		{
			
			_gro5yvfo = (int)-4;
		}
		else
		if (_u6e6d39b < (_dxpq0xkr + _9qyq7j3e))
		{
			
			_gro5yvfo = (int)-8;
		}
		else
		if (_uhi0ls8i < _dxpq0xkr)
		{
			
			_gro5yvfo = (int)-17;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("SLASDA" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		
		_ev4xhht5 = (_dxpq0xkr + _9qyq7j3e);//* 
		//*     If the input matrix is too small, call SLASDQ to find the SVD. 
		//* 
		
		if (_dxpq0xkr <= _q1xpyios)
		{
			
			if (_y1be9goe == (int)0)
			{
				
				_zfmooian("U" ,ref _9qyq7j3e ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,_plfm7z8g ,_864fslqq ,_xdbczr8u ,ref _u6e6d39b ,_7u55mqkq ,ref _u6e6d39b ,_7u55mqkq ,ref _u6e6d39b ,_apig8meb ,ref _gro5yvfo );
			}
			else
			{
				
				_zfmooian("U" ,ref _9qyq7j3e ,ref _dxpq0xkr ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)0) ,_plfm7z8g ,_864fslqq ,_xdbczr8u ,ref _u6e6d39b ,_7u55mqkq ,ref _u6e6d39b ,_7u55mqkq ,ref _u6e6d39b ,_apig8meb ,ref _gro5yvfo );
			}
			
			return;
		}
		//* 
		//*     Book-keeping and  set up the computation tree. 
		//* 
		
		_q8fqp221 = (int)1;
		_b53e0l58 = (_q8fqp221 + _dxpq0xkr);
		_9bq9s7q7 = (_b53e0l58 + _dxpq0xkr);
		_ca64lzpg = (_9bq9s7q7 + _dxpq0xkr);
		_426n50rt = (_ca64lzpg + _dxpq0xkr);//* 
		
		_bcsi4mx0 = (int)0;
		_tg62ial7 = (int)0;//* 
		
		_kku1nkf4 = (_q1xpyios + (int)1);
		_w7xcjdw0 = (int)1;
		_ppzorcqs = (_w7xcjdw0 + _ev4xhht5);
		_a6b624z0 = (_ppzorcqs + _ev4xhht5);
		_c7mgu9pg = (_a6b624z0 + (_kku1nkf4 * _kku1nkf4));//* 
		
		_k6l39brz(ref _dxpq0xkr ,ref _0n683y3x ,ref _rwm6akyl ,(_4b6rt45i+(_q8fqp221 - 1)),(_4b6rt45i+(_b53e0l58 - 1)),(_4b6rt45i+(_9bq9s7q7 - 1)),ref _q1xpyios );//* 
		//*     for the nodes on bottom level of the tree, solve 
		//*     their subproblems by SLASDQ. 
		//* 
		
		_v4ofzyw5 = ((_rwm6akyl + (int)1) / (int)2);
		{
			System.Int32 __81fgg2dlsvn737 = (System.Int32)(_v4ofzyw5);
			const System.Int32 __81fgg2step737 = (System.Int32)((int)1);
			System.Int32 __81fgg2count737;
			for (__81fgg2count737 = System.Math.Max(0, (System.Int32)(((System.Int32)(_rwm6akyl) - __81fgg2dlsvn737 + __81fgg2step737) / __81fgg2step737)), _b5p6od9s = __81fgg2dlsvn737; __81fgg2count737 != 0; __81fgg2count737--, _b5p6od9s += (__81fgg2step737)) {

			{
				//* 
				//*        IC : center row of each node 
				//*        NL : number of rows of left  subproblem 
				//*        NR : number of rows of right subproblem 
				//*        NLF: starting row of the left   subproblem 
				//*        NRF: starting row of the right  subproblem 
				//* 
				
				_egqdmelt = (_b5p6od9s - (int)1);
				_8jzcrkri = *(_4b6rt45i+(_q8fqp221 + _egqdmelt - 1));
				_zx57w4aj = *(_4b6rt45i+(_b53e0l58 + _egqdmelt - 1));
				_qwh8ts9f = (_zx57w4aj + (int)1);
				_oqpc3yjg = *(_4b6rt45i+(_9bq9s7q7 + _egqdmelt - 1));
				_cyu21nam = (_8jzcrkri - _zx57w4aj);
				_5tcb1chw = (_8jzcrkri + (int)1);
				_n6rt1nys = ((_ca64lzpg + _cyu21nam) - (int)2);
				_0zxfmdr3 = ((_w7xcjdw0 + _cyu21nam) - (int)1);
				_h1q3br85 = ((_ppzorcqs + _cyu21nam) - (int)1);
				_x0hi7bqn = (int)1;
				if (_y1be9goe == (int)0)
				{
					
					_t013e1c8("A" ,ref _qwh8ts9f ,ref _qwh8ts9f ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_kxg5drh2) ,(_apig8meb+(_a6b624z0 - 1)),ref _kku1nkf4 );
					_zfmooian("U" ,ref _x0hi7bqn ,ref _zx57w4aj ,ref _qwh8ts9f ,ref _tg62ial7 ,ref _bcsi4mx0 ,(_plfm7z8g+(_cyu21nam - 1)),(_864fslqq+(_cyu21nam - 1)),(_apig8meb+(_a6b624z0 - 1)),ref _kku1nkf4 ,(_apig8meb+(_c7mgu9pg - 1)),ref _zx57w4aj ,(_apig8meb+(_c7mgu9pg - 1)),ref _zx57w4aj ,(_apig8meb+(_c7mgu9pg - 1)),ref _gro5yvfo );
					_m1gysdbg = (_a6b624z0 + (_zx57w4aj * _kku1nkf4));
					_wcs7ne88(ref _qwh8ts9f ,(_apig8meb+(_a6b624z0 - 1)),ref Unsafe.AsRef((int)1) ,(_apig8meb+(_0zxfmdr3 - 1)),ref Unsafe.AsRef((int)1) );
					_wcs7ne88(ref _qwh8ts9f ,(_apig8meb+(_m1gysdbg - 1)),ref Unsafe.AsRef((int)1) ,(_apig8meb+(_h1q3br85 - 1)),ref Unsafe.AsRef((int)1) );
				}
				else
				{
					
					_t013e1c8("A" ,ref _zx57w4aj ,ref _zx57w4aj ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_kxg5drh2) ,(_7u55mqkq+(_cyu21nam - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b );
					_t013e1c8("A" ,ref _qwh8ts9f ,ref _qwh8ts9f ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_kxg5drh2) ,(_xdbczr8u+(_cyu21nam - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b );
					_zfmooian("U" ,ref _x0hi7bqn ,ref _zx57w4aj ,ref _qwh8ts9f ,ref _zx57w4aj ,ref _bcsi4mx0 ,(_plfm7z8g+(_cyu21nam - 1)),(_864fslqq+(_cyu21nam - 1)),(_xdbczr8u+(_cyu21nam - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_7u55mqkq+(_cyu21nam - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_7u55mqkq+(_cyu21nam - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_apig8meb+(_a6b624z0 - 1)),ref _gro5yvfo );
					_wcs7ne88(ref _qwh8ts9f ,(_xdbczr8u+(_cyu21nam - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)),ref Unsafe.AsRef((int)1) ,(_apig8meb+(_0zxfmdr3 - 1)),ref Unsafe.AsRef((int)1) );
					_wcs7ne88(ref _qwh8ts9f ,(_xdbczr8u+(_cyu21nam - 1) + (_qwh8ts9f - 1) * 1 * (_u6e6d39b)),ref Unsafe.AsRef((int)1) ,(_apig8meb+(_h1q3br85 - 1)),ref Unsafe.AsRef((int)1) );
				}
				
				if (_gro5yvfo != (int)0)
				{
					
					return;
				}
				
				{
					System.Int32 __81fgg2dlsvn738 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step738 = (System.Int32)((int)1);
					System.Int32 __81fgg2count738;
					for (__81fgg2count738 = System.Math.Max(0, (System.Int32)(((System.Int32)(_zx57w4aj) - __81fgg2dlsvn738 + __81fgg2step738) / __81fgg2step738)), _znpjgsef = __81fgg2dlsvn738; __81fgg2count738 != 0; __81fgg2count738--, _znpjgsef += (__81fgg2step738)) {

					{
						
						*(_4b6rt45i+(_n6rt1nys + _znpjgsef - 1)) = _znpjgsef;
Mark10:;
						// continue
					}
										}				}
				if ((_b5p6od9s == _rwm6akyl) & (_9qyq7j3e == (int)0))
				{
					
					_x0hi7bqn = (int)0;
				}
				else
				{
					
					_x0hi7bqn = (int)1;
				}
				
				_n6rt1nys = (_n6rt1nys + _qwh8ts9f);
				_0zxfmdr3 = (_0zxfmdr3 + _qwh8ts9f);
				_h1q3br85 = (_h1q3br85 + _qwh8ts9f);
				_bds60snh = (_oqpc3yjg + _x0hi7bqn);
				if (_y1be9goe == (int)0)
				{
					
					_t013e1c8("A" ,ref _bds60snh ,ref _bds60snh ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_kxg5drh2) ,(_apig8meb+(_a6b624z0 - 1)),ref _kku1nkf4 );
					_zfmooian("U" ,ref _x0hi7bqn ,ref _oqpc3yjg ,ref _bds60snh ,ref _tg62ial7 ,ref _bcsi4mx0 ,(_plfm7z8g+(_5tcb1chw - 1)),(_864fslqq+(_5tcb1chw - 1)),(_apig8meb+(_a6b624z0 - 1)),ref _kku1nkf4 ,(_apig8meb+(_c7mgu9pg - 1)),ref _oqpc3yjg ,(_apig8meb+(_c7mgu9pg - 1)),ref _oqpc3yjg ,(_apig8meb+(_c7mgu9pg - 1)),ref _gro5yvfo );
					_m1gysdbg = (_a6b624z0 + ((_bds60snh - (int)1) * _kku1nkf4));
					_wcs7ne88(ref _bds60snh ,(_apig8meb+(_a6b624z0 - 1)),ref Unsafe.AsRef((int)1) ,(_apig8meb+(_0zxfmdr3 - 1)),ref Unsafe.AsRef((int)1) );
					_wcs7ne88(ref _bds60snh ,(_apig8meb+(_m1gysdbg - 1)),ref Unsafe.AsRef((int)1) ,(_apig8meb+(_h1q3br85 - 1)),ref Unsafe.AsRef((int)1) );
				}
				else
				{
					
					_t013e1c8("A" ,ref _oqpc3yjg ,ref _oqpc3yjg ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_kxg5drh2) ,(_7u55mqkq+(_5tcb1chw - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b );
					_t013e1c8("A" ,ref _bds60snh ,ref _bds60snh ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_kxg5drh2) ,(_xdbczr8u+(_5tcb1chw - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b );
					_zfmooian("U" ,ref _x0hi7bqn ,ref _oqpc3yjg ,ref _bds60snh ,ref _oqpc3yjg ,ref _bcsi4mx0 ,(_plfm7z8g+(_5tcb1chw - 1)),(_864fslqq+(_5tcb1chw - 1)),(_xdbczr8u+(_5tcb1chw - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_7u55mqkq+(_5tcb1chw - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_7u55mqkq+(_5tcb1chw - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_apig8meb+(_a6b624z0 - 1)),ref _gro5yvfo );
					_wcs7ne88(ref _bds60snh ,(_xdbczr8u+(_5tcb1chw - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)),ref Unsafe.AsRef((int)1) ,(_apig8meb+(_0zxfmdr3 - 1)),ref Unsafe.AsRef((int)1) );
					_wcs7ne88(ref _bds60snh ,(_xdbczr8u+(_5tcb1chw - 1) + (_bds60snh - 1) * 1 * (_u6e6d39b)),ref Unsafe.AsRef((int)1) ,(_apig8meb+(_h1q3br85 - 1)),ref Unsafe.AsRef((int)1) );
				}
				
				if (_gro5yvfo != (int)0)
				{
					
					return;
				}
				
				{
					System.Int32 __81fgg2dlsvn739 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step739 = (System.Int32)((int)1);
					System.Int32 __81fgg2count739;
					for (__81fgg2count739 = System.Math.Max(0, (System.Int32)(((System.Int32)(_oqpc3yjg) - __81fgg2dlsvn739 + __81fgg2step739) / __81fgg2step739)), _znpjgsef = __81fgg2dlsvn739; __81fgg2count739 != 0; __81fgg2count739--, _znpjgsef += (__81fgg2step739)) {

					{
						
						*(_4b6rt45i+(_n6rt1nys + _znpjgsef - 1)) = _znpjgsef;
Mark20:;
						// continue
					}
										}				}
Mark30:;
				// continue
			}
						}		}//* 
		//*     Now conquer each subproblem bottom-up. 
		//* 
		
		_znpjgsef = __POW((int)2, _0n683y3x);
		{
			System.Int32 __81fgg2dlsvn740 = (System.Int32)(_0n683y3x);
			System.Int32 __81fgg2step740 = (System.Int32)((int)-1);
			System.Int32 __81fgg2count740;
			for (__81fgg2count740 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn740 + __81fgg2step740) / __81fgg2step740)), _u0afxfs0 = __81fgg2dlsvn740; __81fgg2count740 != 0; __81fgg2count740--, _u0afxfs0 += (__81fgg2step740)) {

			{
				
				_4ekj6112 = ((_u0afxfs0 * (int)2) - (int)1);//* 
				//*        Find the first node LF and last node LL on 
				//*        the current level LVL. 
				//* 
				
				if (_u0afxfs0 == (int)1)
				{
					
					_es1scagl = (int)1;
					_jnnmt81a = (int)1;
				}
				else
				{
					
					_es1scagl = __POW((int)2, (_u0afxfs0 - (int)1));
					_jnnmt81a = (((int)2 * _es1scagl) - (int)1);
				}
				
				{
					System.Int32 __81fgg2dlsvn741 = (System.Int32)(_es1scagl);
					const System.Int32 __81fgg2step741 = (System.Int32)((int)1);
					System.Int32 __81fgg2count741;
					for (__81fgg2count741 = System.Math.Max(0, (System.Int32)(((System.Int32)(_jnnmt81a) - __81fgg2dlsvn741 + __81fgg2step741) / __81fgg2step741)), _b5p6od9s = __81fgg2dlsvn741; __81fgg2count741 != 0; __81fgg2count741--, _b5p6od9s += (__81fgg2step741)) {

					{
						
						_3dimb00t = (_b5p6od9s - (int)1);
						_8jzcrkri = *(_4b6rt45i+(_q8fqp221 + _3dimb00t - 1));
						_zx57w4aj = *(_4b6rt45i+(_b53e0l58 + _3dimb00t - 1));
						_oqpc3yjg = *(_4b6rt45i+(_9bq9s7q7 + _3dimb00t - 1));
						_cyu21nam = (_8jzcrkri - _zx57w4aj);
						_5tcb1chw = (_8jzcrkri + (int)1);
						if (_b5p6od9s == _jnnmt81a)
						{
							
							_x0hi7bqn = _9qyq7j3e;
						}
						else
						{
							
							_x0hi7bqn = (int)1;
						}
						
						_0zxfmdr3 = ((_w7xcjdw0 + _cyu21nam) - (int)1);
						_h1q3br85 = ((_ppzorcqs + _cyu21nam) - (int)1);
						_n6rt1nys = ((_ca64lzpg + _cyu21nam) - (int)1);
						_r7cfteg3 = *(_plfm7z8g+(_8jzcrkri - 1));
						_bafcbx97 = *(_864fslqq+(_8jzcrkri - 1));
						if (_y1be9goe == (int)0)
						{
							
							_4zx8qorc(ref _y1be9goe ,ref _zx57w4aj ,ref _oqpc3yjg ,ref _x0hi7bqn ,(_plfm7z8g+(_cyu21nam - 1)),(_apig8meb+(_0zxfmdr3 - 1)),(_apig8meb+(_h1q3br85 - 1)),ref _r7cfteg3 ,ref _bafcbx97 ,(_4b6rt45i+(_n6rt1nys - 1)),_umao48xu ,ref Unsafe.AsRef(*(_8vecpt74+((int)1 - 1))) ,_0zwn6fsy ,ref _uhi0ls8i ,_gh266ol1 ,ref _u6e6d39b ,_7nk40y8b ,_i8976ehd ,_doljbvm2 ,_7e60fcso ,ref Unsafe.AsRef(*(_umlkckdg+((int)1 - 1))) ,ref Unsafe.AsRef(*(_3crf0qn3+((int)1 - 1))) ,ref Unsafe.AsRef(*(_irk8i6qr+((int)1 - 1))) ,(_apig8meb+(_a6b624z0 - 1)),(_4b6rt45i+(_426n50rt - 1)),ref _gro5yvfo );
						}
						else
						{
							
							_znpjgsef = (_znpjgsef - (int)1);
							_4zx8qorc(ref _y1be9goe ,ref _zx57w4aj ,ref _oqpc3yjg ,ref _x0hi7bqn ,(_plfm7z8g+(_cyu21nam - 1)),(_apig8meb+(_0zxfmdr3 - 1)),(_apig8meb+(_h1q3br85 - 1)),ref _r7cfteg3 ,ref _bafcbx97 ,(_4b6rt45i+(_n6rt1nys - 1)),(_umao48xu+(_cyu21nam - 1) + (_u0afxfs0 - 1) * 1 * (_uhi0ls8i)),ref Unsafe.AsRef(*(_8vecpt74+(_znpjgsef - 1))) ,(_0zwn6fsy+(_cyu21nam - 1) + (_4ekj6112 - 1) * 1 * (_uhi0ls8i)),ref _uhi0ls8i ,(_gh266ol1+(_cyu21nam - 1) + (_4ekj6112 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_7nk40y8b+(_cyu21nam - 1) + (_4ekj6112 - 1) * 1 * (_u6e6d39b)),(_i8976ehd+(_cyu21nam - 1) + (_u0afxfs0 - 1) * 1 * (_u6e6d39b)),(_doljbvm2+(_cyu21nam - 1) + (_4ekj6112 - 1) * 1 * (_u6e6d39b)),(_7e60fcso+(_cyu21nam - 1) + (_u0afxfs0 - 1) * 1 * (_u6e6d39b)),ref Unsafe.AsRef(*(_umlkckdg+(_znpjgsef - 1))) ,ref Unsafe.AsRef(*(_3crf0qn3+(_znpjgsef - 1))) ,ref Unsafe.AsRef(*(_irk8i6qr+(_znpjgsef - 1))) ,(_apig8meb+(_a6b624z0 - 1)),(_4b6rt45i+(_426n50rt - 1)),ref _gro5yvfo );
						}
						
						if (_gro5yvfo != (int)0)
						{
							
							return;
						}
						
Mark40:;
						// continue
					}
										}				}
Mark50:;
				// continue
			}
						}		}//* 
		
		return;//* 
		//*     End of SLASDA 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
