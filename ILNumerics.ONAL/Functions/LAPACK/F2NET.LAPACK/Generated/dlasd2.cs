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
//*> \brief \b DLASD2 merges the two sets of singular values together into a single sorted set. Used by sbdsdc. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLASD2 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlasd2.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlasd2.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlasd2.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLASD2( NL, NR, SQRE, K, D, Z, ALPHA, BETA, U, LDU, VT, 
//*                          LDVT, DSIGMA, U2, LDU2, VT2, LDVT2, IDXP, IDX, 
//*                          IDXC, IDXQ, COLTYP, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INFO, K, LDU, LDU2, LDVT, LDVT2, NL, NR, SQRE 
//*       DOUBLE PRECISION   ALPHA, BETA 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            COLTYP( * ), IDX( * ), IDXC( * ), IDXP( * ), 
//*      $                   IDXQ( * ) 
//*       DOUBLE PRECISION   D( * ), DSIGMA( * ), U( LDU, * ), 
//*      $                   U2( LDU2, * ), VT( LDVT, * ), VT2( LDVT2, * ), 
//*      $                   Z( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLASD2 merges the two sets of singular values together into a single 
//*> sorted set.  Then it tries to deflate the size of the problem. 
//*> There are two ways in which deflation can occur:  when two or more 
//*> singular values are close together or if there is a tiny entry in the 
//*> Z vector.  For each such occurrence the order of the related secular 
//*> equation problem is reduced by one. 
//*> 
//*> DLASD2 is called from DLASD1. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] NL 
//*> \verbatim 
//*>          NL is INTEGER 
//*>         The row dimension of the upper block.  NL >= 1. 
//*> \endverbatim 
//*> 
//*> \param[in] NR 
//*> \verbatim 
//*>          NR is INTEGER 
//*>         The row dimension of the lower block.  NR >= 1. 
//*> \endverbatim 
//*> 
//*> \param[in] SQRE 
//*> \verbatim 
//*>          SQRE is INTEGER 
//*>         = 0: the lower block is an NR-by-NR square matrix. 
//*>         = 1: the lower block is an NR-by-(NR+1) rectangular matrix. 
//*> 
//*>         The bidiagonal matrix has N = NL + NR + 1 rows and 
//*>         M = N + SQRE >= N columns. 
//*> \endverbatim 
//*> 
//*> \param[out] K 
//*> \verbatim 
//*>          K is INTEGER 
//*>         Contains the dimension of the non-deflated matrix, 
//*>         This is the order of the related secular equation. 1 <= K <=N. 
//*> \endverbatim 
//*> 
//*> \param[in,out] D 
//*> \verbatim 
//*>          D is DOUBLE PRECISION array, dimension(N) 
//*>         On entry D contains the singular values of the two submatrices 
//*>         to be combined.  On exit D contains the trailing (N-K) updated 
//*>         singular values (those which were deflated) sorted into 
//*>         increasing order. 
//*> \endverbatim 
//*> 
//*> \param[out] Z 
//*> \verbatim 
//*>          Z is DOUBLE PRECISION array, dimension(N) 
//*>         On exit Z contains the updating row vector in the secular 
//*>         equation. 
//*> \endverbatim 
//*> 
//*> \param[in] ALPHA 
//*> \verbatim 
//*>          ALPHA is DOUBLE PRECISION 
//*>         Contains the diagonal element associated with the added row. 
//*> \endverbatim 
//*> 
//*> \param[in] BETA 
//*> \verbatim 
//*>          BETA is DOUBLE PRECISION 
//*>         Contains the off-diagonal element associated with the added 
//*>         row. 
//*> \endverbatim 
//*> 
//*> \param[in,out] U 
//*> \verbatim 
//*>          U is DOUBLE PRECISION array, dimension(LDU,N) 
//*>         On entry U contains the left singular vectors of two 
//*>         submatrices in the two square blocks with corners at (1,1), 
//*>         (NL, NL), and (NL+2, NL+2), (N,N). 
//*>         On exit U contains the trailing (N-K) updated left singular 
//*>         vectors (those which were deflated) in its last N-K columns. 
//*> \endverbatim 
//*> 
//*> \param[in] LDU 
//*> \verbatim 
//*>          LDU is INTEGER 
//*>         The leading dimension of the array U.  LDU >= N. 
//*> \endverbatim 
//*> 
//*> \param[in,out] VT 
//*> \verbatim 
//*>          VT is DOUBLE PRECISION array, dimension(LDVT,M) 
//*>         On entry VT**T contains the right singular vectors of two 
//*>         submatrices in the two square blocks with corners at (1,1), 
//*>         (NL+1, NL+1), and (NL+2, NL+2), (M,M). 
//*>         On exit VT**T contains the trailing (N-K) updated right singular 
//*>         vectors (those which were deflated) in its last N-K columns. 
//*>         In case SQRE =1, the last row of VT spans the right null 
//*>         space. 
//*> \endverbatim 
//*> 
//*> \param[in] LDVT 
//*> \verbatim 
//*>          LDVT is INTEGER 
//*>         The leading dimension of the array VT.  LDVT >= M. 
//*> \endverbatim 
//*> 
//*> \param[out] DSIGMA 
//*> \verbatim 
//*>          DSIGMA is DOUBLE PRECISION array, dimension (N) 
//*>         Contains a copy of the diagonal elements (K-1 singular values 
//*>         and one zero) in the secular equation. 
//*> \endverbatim 
//*> 
//*> \param[out] U2 
//*> \verbatim 
//*>          U2 is DOUBLE PRECISION array, dimension(LDU2,N) 
//*>         Contains a copy of the first K-1 left singular vectors which 
//*>         will be used by DLASD3 in a matrix multiply (DGEMM) to solve 
//*>         for the new left singular vectors. U2 is arranged into four 
//*>         blocks. The first block contains a column with 1 at NL+1 and 
//*>         zero everywhere else; the second block contains non-zero 
//*>         entries only at and above NL; the third contains non-zero 
//*>         entries only below NL+1; and the fourth is dense. 
//*> \endverbatim 
//*> 
//*> \param[in] LDU2 
//*> \verbatim 
//*>          LDU2 is INTEGER 
//*>         The leading dimension of the array U2.  LDU2 >= N. 
//*> \endverbatim 
//*> 
//*> \param[out] VT2 
//*> \verbatim 
//*>          VT2 is DOUBLE PRECISION array, dimension(LDVT2,N) 
//*>         VT2**T contains a copy of the first K right singular vectors 
//*>         which will be used by DLASD3 in a matrix multiply (DGEMM) to 
//*>         solve for the new right singular vectors. VT2 is arranged into 
//*>         three blocks. The first block contains a row that corresponds 
//*>         to the special 0 diagonal element in SIGMA; the second block 
//*>         contains non-zeros only at and before NL +1; the third block 
//*>         contains non-zeros only at and after  NL +2. 
//*> \endverbatim 
//*> 
//*> \param[in] LDVT2 
//*> \verbatim 
//*>          LDVT2 is INTEGER 
//*>         The leading dimension of the array VT2.  LDVT2 >= M. 
//*> \endverbatim 
//*> 
//*> \param[out] IDXP 
//*> \verbatim 
//*>          IDXP is INTEGER array, dimension(N) 
//*>         This will contain the permutation used to place deflated 
//*>         values of D at the end of the array. On output IDXP(2:K) 
//*>         points to the nondeflated D-values and IDXP(K+1:N) 
//*>         points to the deflated singular values. 
//*> \endverbatim 
//*> 
//*> \param[out] IDX 
//*> \verbatim 
//*>          IDX is INTEGER array, dimension(N) 
//*>         This will contain the permutation used to sort the contents of 
//*>         D into ascending order. 
//*> \endverbatim 
//*> 
//*> \param[out] IDXC 
//*> \verbatim 
//*>          IDXC is INTEGER array, dimension(N) 
//*>         This will contain the permutation used to arrange the columns 
//*>         of the deflated U matrix into three groups:  the first group 
//*>         contains non-zero entries only at and above NL, the second 
//*>         contains non-zero entries only below NL+2, and the third is 
//*>         dense. 
//*> \endverbatim 
//*> 
//*> \param[in,out] IDXQ 
//*> \verbatim 
//*>          IDXQ is INTEGER array, dimension(N) 
//*>         This contains the permutation which separately sorts the two 
//*>         sub-problems in D into ascending order.  Note that entries in 
//*>         the first hlaf of this permutation must first be moved one 
//*>         position backward; and entries in the second half 
//*>         must first have NL+1 added to their values. 
//*> \endverbatim 
//*> 
//*> \param[out] COLTYP 
//*> \verbatim 
//*>          COLTYP is INTEGER array, dimension(N) 
//*>         As workspace, this will contain a label which will indicate 
//*>         which of the following types a column in the U2 matrix or a 
//*>         row in the VT2 matrix is: 
//*>         1 : non-zero in the upper half only 
//*>         2 : non-zero in the lower half only 
//*>         3 : dense 
//*>         4 : deflated 
//*> 
//*>         On exit, it is an array of dimension 4, with COLTYP(I) being 
//*>         the dimension of the I-th type columns. 
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
//*> \date June 2017 
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

	 
	public static void _ph6grb63(ref Int32 _zx57w4aj, ref Int32 _oqpc3yjg, ref Int32 _9qyq7j3e, ref Int32 _umlkckdg, Double* _plfm7z8g, Double* _7e60fcso, ref Double _r7cfteg3, ref Double _bafcbx97, Double* _7u55mqkq, ref Int32 _u6e6d39b, Double* _xdbczr8u, ref Int32 _h4ibbatv, Double* _1r8q3o4r, Double* _s6mwvivs, ref Int32 _qz188u0m, Double* _1469pg8i, ref Int32 _sh6ez9uf, Int32* _u0tzpo1z, Int32* _diodrai4, Int32* _dzf4x6zd, Int32* _ca64lzpg, Int32* _gj7zf775, ref Int32 _gro5yvfo)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)32 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Double _5m0mjfxm =  2d;
Double _2j4711hv =  8d;
Int32* _9wgmm024 =  (Int32*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Int32) * ((int)4);
Int32* _xtc5b1gc =  (Int32*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Int32) * ((int)4);
Int32 _hr7bl0wv =  default;
Int32 _b5p6od9s =  default;
Int32 _s0xtt601 =  default;
Int32 _z1w9cjgb =  default;
Int32 _bcl5dcha =  default;
Int32 _znpjgsef =  default;
Int32 _c2zk3fjj =  default;
Int32 _9v3wcik1 =  default;
Int32 _bddf4r7h =  default;
Int32 _ev4xhht5 =  default;
Int32 _dxpq0xkr =  default;
Int32 _qwh8ts9f =  default;
Int32 _3lfkfha8 =  default;
Double _3crf0qn3 =  default;
Double _p1iqarg6 =  default;
Double _k69jo0rq =  default;
Double _irk8i6qr =  default;
Double _0446f4de =  default;
Double _txq1gp7u =  default;
Double _rm9rpu83 =  default;
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
		//*     .. Local Arrays .. 
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
		
		if (_zx57w4aj < (int)1)
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (_oqpc3yjg < (int)1)
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if ((_9qyq7j3e != (int)1) & (_9qyq7j3e != (int)0))
		{
			
			_gro5yvfo = (int)-3;
		}
		//* 
		
		_dxpq0xkr = ((_zx57w4aj + _oqpc3yjg) + (int)1);
		_ev4xhht5 = (_dxpq0xkr + _9qyq7j3e);//* 
		
		if (_u6e6d39b < _dxpq0xkr)
		{
			
			_gro5yvfo = (int)-10;
		}
		else
		if (_h4ibbatv < _ev4xhht5)
		{
			
			_gro5yvfo = (int)-12;
		}
		else
		if (_qz188u0m < _dxpq0xkr)
		{
			
			_gro5yvfo = (int)-15;
		}
		else
		if (_sh6ez9uf < _ev4xhht5)
		{
			
			_gro5yvfo = (int)-17;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("DLASD2" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		
		_qwh8ts9f = (_zx57w4aj + (int)1);
		_3lfkfha8 = (_zx57w4aj + (int)2);//* 
		//*     Generate the first part of the vector Z; and move the singular 
		//*     values in the first part of D one position backward. 
		//* 
		
		_rm9rpu83 = (_r7cfteg3 * *(_xdbczr8u+(_qwh8ts9f - 1) + (_qwh8ts9f - 1) * 1 * (_h4ibbatv)));
		*(_7e60fcso+((int)1 - 1)) = _rm9rpu83;
		{
			System.Int32 __81fgg2dlsvn206 = (System.Int32)(_zx57w4aj);
			System.Int32 __81fgg2step206 = (System.Int32)((int)-1);
			System.Int32 __81fgg2count206;
			for (__81fgg2count206 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn206 + __81fgg2step206) / __81fgg2step206)), _b5p6od9s = __81fgg2dlsvn206; __81fgg2count206 != 0; __81fgg2count206--, _b5p6od9s += (__81fgg2step206)) {

			{
				
				*(_7e60fcso+(_b5p6od9s + (int)1 - 1)) = (_r7cfteg3 * *(_xdbczr8u+(_b5p6od9s - 1) + (_qwh8ts9f - 1) * 1 * (_h4ibbatv)));
				*(_plfm7z8g+(_b5p6od9s + (int)1 - 1)) = *(_plfm7z8g+(_b5p6od9s - 1));
				*(_ca64lzpg+(_b5p6od9s + (int)1 - 1)) = (*(_ca64lzpg+(_b5p6od9s - 1)) + (int)1);
Mark10:;
				// continue
			}
						}		}//* 
		//*     Generate the second part of the vector Z. 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn207 = (System.Int32)(_3lfkfha8);
			const System.Int32 __81fgg2step207 = (System.Int32)((int)1);
			System.Int32 __81fgg2count207;
			for (__81fgg2count207 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn207 + __81fgg2step207) / __81fgg2step207)), _b5p6od9s = __81fgg2dlsvn207; __81fgg2count207 != 0; __81fgg2count207--, _b5p6od9s += (__81fgg2step207)) {

			{
				
				*(_7e60fcso+(_b5p6od9s - 1)) = (_bafcbx97 * *(_xdbczr8u+(_b5p6od9s - 1) + (_3lfkfha8 - 1) * 1 * (_h4ibbatv)));
Mark20:;
				// continue
			}
						}		}//* 
		//*     Initialize some reference arrays. 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn208 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step208 = (System.Int32)((int)1);
			System.Int32 __81fgg2count208;
			for (__81fgg2count208 = System.Math.Max(0, (System.Int32)(((System.Int32)(_qwh8ts9f) - __81fgg2dlsvn208 + __81fgg2step208) / __81fgg2step208)), _b5p6od9s = __81fgg2dlsvn208; __81fgg2count208 != 0; __81fgg2count208--, _b5p6od9s += (__81fgg2step208)) {

			{
				
				*(_gj7zf775+(_b5p6od9s - 1)) = (int)1;
Mark30:;
				// continue
			}
						}		}
		{
			System.Int32 __81fgg2dlsvn209 = (System.Int32)(_3lfkfha8);
			const System.Int32 __81fgg2step209 = (System.Int32)((int)1);
			System.Int32 __81fgg2count209;
			for (__81fgg2count209 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn209 + __81fgg2step209) / __81fgg2step209)), _b5p6od9s = __81fgg2dlsvn209; __81fgg2count209 != 0; __81fgg2count209--, _b5p6od9s += (__81fgg2step209)) {

			{
				
				*(_gj7zf775+(_b5p6od9s - 1)) = (int)2;
Mark40:;
				// continue
			}
						}		}//* 
		//*     Sort the singular values into increasing order 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn210 = (System.Int32)(_3lfkfha8);
			const System.Int32 __81fgg2step210 = (System.Int32)((int)1);
			System.Int32 __81fgg2count210;
			for (__81fgg2count210 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn210 + __81fgg2step210) / __81fgg2step210)), _b5p6od9s = __81fgg2dlsvn210; __81fgg2count210 != 0; __81fgg2count210--, _b5p6od9s += (__81fgg2step210)) {

			{
				
				*(_ca64lzpg+(_b5p6od9s - 1)) = (*(_ca64lzpg+(_b5p6od9s - 1)) + _qwh8ts9f);
Mark50:;
				// continue
			}
						}		}//* 
		//*     DSIGMA, IDXC, IDXC, and the first column of U2 
		//*     are used as storage space. 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn211 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step211 = (System.Int32)((int)1);
			System.Int32 __81fgg2count211;
			for (__81fgg2count211 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn211 + __81fgg2step211) / __81fgg2step211)), _b5p6od9s = __81fgg2dlsvn211; __81fgg2count211 != 0; __81fgg2count211--, _b5p6od9s += (__81fgg2step211)) {

			{
				
				*(_1r8q3o4r+(_b5p6od9s - 1)) = *(_plfm7z8g+(*(_ca64lzpg+(_b5p6od9s - 1)) - 1));
				*(_s6mwvivs+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_qz188u0m)) = *(_7e60fcso+(*(_ca64lzpg+(_b5p6od9s - 1)) - 1));
				*(_dzf4x6zd+(_b5p6od9s - 1)) = *(_gj7zf775+(*(_ca64lzpg+(_b5p6od9s - 1)) - 1));
Mark60:;
				// continue
			}
						}		}//* 
		
		_csi3ymnh(ref _zx57w4aj ,ref _oqpc3yjg ,(_1r8q3o4r+((int)2 - 1)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef((int)1) ,(_diodrai4+((int)2 - 1)));//* 
		
		{
			System.Int32 __81fgg2dlsvn212 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step212 = (System.Int32)((int)1);
			System.Int32 __81fgg2count212;
			for (__81fgg2count212 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn212 + __81fgg2step212) / __81fgg2step212)), _b5p6od9s = __81fgg2dlsvn212; __81fgg2count212 != 0; __81fgg2count212--, _b5p6od9s += (__81fgg2step212)) {

			{
				
				_s0xtt601 = ((int)1 + *(_diodrai4+(_b5p6od9s - 1)));
				*(_plfm7z8g+(_b5p6od9s - 1)) = *(_1r8q3o4r+(_s0xtt601 - 1));
				*(_7e60fcso+(_b5p6od9s - 1)) = *(_s6mwvivs+(_s0xtt601 - 1) + ((int)1 - 1) * 1 * (_qz188u0m));
				*(_gj7zf775+(_b5p6od9s - 1)) = *(_dzf4x6zd+(_s0xtt601 - 1));
Mark70:;
				// continue
			}
						}		}//* 
		//*     Calculate the allowable deflation tolerance 
		//* 
		
		_p1iqarg6 = _f43eg0w0("Epsilon" );
		_txq1gp7u = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_r7cfteg3 ) ,ILNumerics.F2NET.Intrinsics.ABS(_bafcbx97 ) );
		_txq1gp7u = ((_2j4711hv * _p1iqarg6) * ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_dxpq0xkr - 1)) ) ,_txq1gp7u ));//* 
		//*     There are 2 kinds of deflation -- first a value in the z-vector 
		//*     is small, second two (or more) singular values are very close 
		//*     together (their difference is small). 
		//* 
		//*     If the value in the z-vector is small, we simply permute the 
		//*     array so that the corresponding singular value is moved to the 
		//*     end. 
		//* 
		//*     If two values in the D-vector are close, we perform a two-sided 
		//*     rotation designed to make one of the corresponding z-vector 
		//*     entries zero, and then permute the array so that the deflated 
		//*     singular value is moved to the end. 
		//* 
		//*     If there are multiple singular values then the problem deflates. 
		//*     Here the number of equal singular values are found.  As each equal 
		//*     singular value is found, an elementary reflector is computed to 
		//*     rotate the corresponding singular subspace so that the 
		//*     corresponding components of Z are zero in this new basis. 
		//* 
		
		_umlkckdg = (int)1;
		_bddf4r7h = (_dxpq0xkr + (int)1);
		{
			System.Int32 __81fgg2dlsvn213 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step213 = (System.Int32)((int)1);
			System.Int32 __81fgg2count213;
			for (__81fgg2count213 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn213 + __81fgg2step213) / __81fgg2step213)), _znpjgsef = __81fgg2dlsvn213; __81fgg2count213 != 0; __81fgg2count213--, _znpjgsef += (__81fgg2step213)) {

			{
				
				if (ILNumerics.F2NET.Intrinsics.ABS(*(_7e60fcso+(_znpjgsef - 1)) ) <= _txq1gp7u)
				{
					//* 
					//*           Deflate due to small z component. 
					//* 
					
					_bddf4r7h = (_bddf4r7h - (int)1);
					*(_u0tzpo1z+(_bddf4r7h - 1)) = _znpjgsef;
					*(_gj7zf775+(_znpjgsef - 1)) = (int)4;
					if (_znpjgsef == _dxpq0xkr)goto Mark120;
				}
				else
				{
					
					_9v3wcik1 = _znpjgsef;goto Mark90;
				}
				
Mark80:;
				// continue
			}
						}		}
Mark90:;
		// continue
		_znpjgsef = _9v3wcik1;
Mark100:;
		// continue
		_znpjgsef = (_znpjgsef + (int)1);
		if (_znpjgsef > _dxpq0xkr)goto Mark110;
		if (ILNumerics.F2NET.Intrinsics.ABS(*(_7e60fcso+(_znpjgsef - 1)) ) <= _txq1gp7u)
		{
			//* 
			//*        Deflate due to small z component. 
			//* 
			
			_bddf4r7h = (_bddf4r7h - (int)1);
			*(_u0tzpo1z+(_bddf4r7h - 1)) = _znpjgsef;
			*(_gj7zf775+(_znpjgsef - 1)) = (int)4;
		}
		else
		{
			//* 
			//*        Check if singular values are close enough to allow deflation. 
			//* 
			
			if (ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_znpjgsef - 1)) - *(_plfm7z8g+(_9v3wcik1 - 1)) ) <= _txq1gp7u)
			{
				//* 
				//*           Deflation is possible. 
				//* 
				
				_irk8i6qr = *(_7e60fcso+(_9v3wcik1 - 1));
				_3crf0qn3 = *(_7e60fcso+(_znpjgsef - 1));//* 
				//*           Find sqrt(a**2+b**2) without overflow or 
				//*           destructive underflow. 
				//* 
				
				_0446f4de = _1uc27645(ref _3crf0qn3 ,ref _irk8i6qr );
				_3crf0qn3 = (_3crf0qn3 / _0446f4de);
				_irk8i6qr = (-((_irk8i6qr / _0446f4de)));
				*(_7e60fcso+(_znpjgsef - 1)) = _0446f4de;
				*(_7e60fcso+(_9v3wcik1 - 1)) = _d0547bi2;//* 
				//*           Apply back the Givens rotation to the left and right 
				//*           singular vector matrices. 
				//* 
				
				_bcl5dcha = *(_ca64lzpg+(*(_diodrai4+(_9v3wcik1 - 1)) + (int)1 - 1));
				_z1w9cjgb = *(_ca64lzpg+(*(_diodrai4+(_znpjgsef - 1)) + (int)1 - 1));
				if (_bcl5dcha <= _qwh8ts9f)
				{
					
					_bcl5dcha = (_bcl5dcha - (int)1);
				}
				
				if (_z1w9cjgb <= _qwh8ts9f)
				{
					
					_z1w9cjgb = (_z1w9cjgb - (int)1);
				}
				
				_2197fa5i(ref _dxpq0xkr ,(_7u55mqkq+((int)1 - 1) + (_bcl5dcha - 1) * 1 * (_u6e6d39b)),ref Unsafe.AsRef((int)1) ,(_7u55mqkq+((int)1 - 1) + (_z1w9cjgb - 1) * 1 * (_u6e6d39b)),ref Unsafe.AsRef((int)1) ,ref _3crf0qn3 ,ref _irk8i6qr );
				_2197fa5i(ref _ev4xhht5 ,(_xdbczr8u+(_bcl5dcha - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv ,(_xdbczr8u+(_z1w9cjgb - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv ,ref _3crf0qn3 ,ref _irk8i6qr );
				if (*(_gj7zf775+(_znpjgsef - 1)) != *(_gj7zf775+(_9v3wcik1 - 1)))
				{
					
					*(_gj7zf775+(_znpjgsef - 1)) = (int)3;
				}
				
				*(_gj7zf775+(_9v3wcik1 - 1)) = (int)4;
				_bddf4r7h = (_bddf4r7h - (int)1);
				*(_u0tzpo1z+(_bddf4r7h - 1)) = _9v3wcik1;
				_9v3wcik1 = _znpjgsef;
			}
			else
			{
				
				_umlkckdg = (_umlkckdg + (int)1);
				*(_s6mwvivs+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_qz188u0m)) = *(_7e60fcso+(_9v3wcik1 - 1));
				*(_1r8q3o4r+(_umlkckdg - 1)) = *(_plfm7z8g+(_9v3wcik1 - 1));
				*(_u0tzpo1z+(_umlkckdg - 1)) = _9v3wcik1;
				_9v3wcik1 = _znpjgsef;
			}
			
		}
		goto Mark100;
Mark110:;
		// continue//* 
		//*     Record the last singular value. 
		//* 
		
		_umlkckdg = (_umlkckdg + (int)1);
		*(_s6mwvivs+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_qz188u0m)) = *(_7e60fcso+(_9v3wcik1 - 1));
		*(_1r8q3o4r+(_umlkckdg - 1)) = *(_plfm7z8g+(_9v3wcik1 - 1));
		*(_u0tzpo1z+(_umlkckdg - 1)) = _9v3wcik1;//* 
		
Mark120:;
		// continue//* 
		//*     Count up the total number of the various types of columns, then 
		//*     form a permutation which positions the four column types into 
		//*     four groups of uniform structure (although one or more of these 
		//*     groups may be empty). 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn214 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step214 = (System.Int32)((int)1);
			System.Int32 __81fgg2count214;
			for (__81fgg2count214 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)4) - __81fgg2dlsvn214 + __81fgg2step214) / __81fgg2step214)), _znpjgsef = __81fgg2dlsvn214; __81fgg2count214 != 0; __81fgg2count214--, _znpjgsef += (__81fgg2step214)) {

			{
				
				*(_9wgmm024+(_znpjgsef - 1)) = (int)0;
Mark130:;
				// continue
			}
						}		}
		{
			System.Int32 __81fgg2dlsvn215 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step215 = (System.Int32)((int)1);
			System.Int32 __81fgg2count215;
			for (__81fgg2count215 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn215 + __81fgg2step215) / __81fgg2step215)), _znpjgsef = __81fgg2dlsvn215; __81fgg2count215 != 0; __81fgg2count215--, _znpjgsef += (__81fgg2step215)) {

			{
				
				_hr7bl0wv = *(_gj7zf775+(_znpjgsef - 1));
				*(_9wgmm024+(_hr7bl0wv - 1)) = (*(_9wgmm024+(_hr7bl0wv - 1)) + (int)1);
Mark140:;
				// continue
			}
						}		}//* 
		//*     PSM(*) = Position in SubMatrix (of types 1 through 4) 
		//* 
		
		*(_xtc5b1gc+((int)1 - 1)) = (int)2;
		*(_xtc5b1gc+((int)2 - 1)) = ((int)2 + *(_9wgmm024+((int)1 - 1)));
		*(_xtc5b1gc+((int)3 - 1)) = (*(_xtc5b1gc+((int)2 - 1)) + *(_9wgmm024+((int)2 - 1)));
		*(_xtc5b1gc+((int)4 - 1)) = (*(_xtc5b1gc+((int)3 - 1)) + *(_9wgmm024+((int)3 - 1)));//* 
		//*     Fill out the IDXC array so that the permutation which it induces 
		//*     will place all type-1 columns first, all type-2 columns next, 
		//*     then all type-3's, and finally all type-4's, starting from the 
		//*     second column. This applies similarly to the rows of VT. 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn216 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step216 = (System.Int32)((int)1);
			System.Int32 __81fgg2count216;
			for (__81fgg2count216 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn216 + __81fgg2step216) / __81fgg2step216)), _znpjgsef = __81fgg2dlsvn216; __81fgg2count216 != 0; __81fgg2count216--, _znpjgsef += (__81fgg2step216)) {

			{
				
				_c2zk3fjj = *(_u0tzpo1z+(_znpjgsef - 1));
				_hr7bl0wv = *(_gj7zf775+(_c2zk3fjj - 1));
				*(_dzf4x6zd+(*(_xtc5b1gc+(_hr7bl0wv - 1)) - 1)) = _znpjgsef;
				*(_xtc5b1gc+(_hr7bl0wv - 1)) = (*(_xtc5b1gc+(_hr7bl0wv - 1)) + (int)1);
Mark150:;
				// continue
			}
						}		}//* 
		//*     Sort the singular values and corresponding singular vectors into 
		//*     DSIGMA, U2, and VT2 respectively.  The singular values/vectors 
		//*     which were not deflated go into the first K slots of DSIGMA, U2, 
		//*     and VT2 respectively, while those which were deflated go into the 
		//*     last N - K slots, except that the first column/row will be treated 
		//*     separately. 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn217 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step217 = (System.Int32)((int)1);
			System.Int32 __81fgg2count217;
			for (__81fgg2count217 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn217 + __81fgg2step217) / __81fgg2step217)), _znpjgsef = __81fgg2dlsvn217; __81fgg2count217 != 0; __81fgg2count217--, _znpjgsef += (__81fgg2step217)) {

			{
				
				_c2zk3fjj = *(_u0tzpo1z+(_znpjgsef - 1));
				*(_1r8q3o4r+(_znpjgsef - 1)) = *(_plfm7z8g+(_c2zk3fjj - 1));
				_z1w9cjgb = *(_ca64lzpg+(*(_diodrai4+(*(_u0tzpo1z+(*(_dzf4x6zd+(_znpjgsef - 1)) - 1)) - 1)) + (int)1 - 1));
				if (_z1w9cjgb <= _qwh8ts9f)
				{
					
					_z1w9cjgb = (_z1w9cjgb - (int)1);
				}
				
				_gvjhlct0(ref _dxpq0xkr ,(_7u55mqkq+((int)1 - 1) + (_z1w9cjgb - 1) * 1 * (_u6e6d39b)),ref Unsafe.AsRef((int)1) ,(_s6mwvivs+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_qz188u0m)),ref Unsafe.AsRef((int)1) );
				_gvjhlct0(ref _ev4xhht5 ,(_xdbczr8u+(_z1w9cjgb - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv ,(_1469pg8i+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_sh6ez9uf)),ref _sh6ez9uf );
Mark160:;
				// continue
			}
						}		}//* 
		//*     Determine DSIGMA(1), DSIGMA(2) and Z(1) 
		//* 
		
		*(_1r8q3o4r+((int)1 - 1)) = _d0547bi2;
		_k69jo0rq = (_txq1gp7u / _5m0mjfxm);
		if (ILNumerics.F2NET.Intrinsics.ABS(*(_1r8q3o4r+((int)2 - 1)) ) <= _k69jo0rq)
		*(_1r8q3o4r+((int)2 - 1)) = _k69jo0rq;
		if (_ev4xhht5 > _dxpq0xkr)
		{
			
			*(_7e60fcso+((int)1 - 1)) = _1uc27645(ref _rm9rpu83 ,ref Unsafe.AsRef(*(_7e60fcso+(_ev4xhht5 - 1))) );
			if (*(_7e60fcso+((int)1 - 1)) <= _txq1gp7u)
			{
				
				_3crf0qn3 = _kxg5drh2;
				_irk8i6qr = _d0547bi2;
				*(_7e60fcso+((int)1 - 1)) = _txq1gp7u;
			}
			else
			{
				
				_3crf0qn3 = (_rm9rpu83 / *(_7e60fcso+((int)1 - 1)));
				_irk8i6qr = (*(_7e60fcso+(_ev4xhht5 - 1)) / *(_7e60fcso+((int)1 - 1)));
			}
			
		}
		else
		{
			
			if (ILNumerics.F2NET.Intrinsics.ABS(_rm9rpu83 ) <= _txq1gp7u)
			{
				
				*(_7e60fcso+((int)1 - 1)) = _txq1gp7u;
			}
			else
			{
				
				*(_7e60fcso+((int)1 - 1)) = _rm9rpu83;
			}
			
		}
		//* 
		//*     Move the rest of the updating row to Z. 
		//* 
		
		_gvjhlct0(ref Unsafe.AsRef(_umlkckdg - (int)1) ,(_s6mwvivs+((int)2 - 1) + ((int)1 - 1) * 1 * (_qz188u0m)),ref Unsafe.AsRef((int)1) ,(_7e60fcso+((int)2 - 1)),ref Unsafe.AsRef((int)1) );//* 
		//*     Determine the first column of U2, the first row of VT2 and the 
		//*     last row of VT. 
		//* 
		
		_rta9tuwm("A" ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,_s6mwvivs ,ref _qz188u0m );
		*(_s6mwvivs+(_qwh8ts9f - 1) + ((int)1 - 1) * 1 * (_qz188u0m)) = _kxg5drh2;
		if (_ev4xhht5 > _dxpq0xkr)
		{
			
			{
				System.Int32 __81fgg2dlsvn218 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step218 = (System.Int32)((int)1);
				System.Int32 __81fgg2count218;
				for (__81fgg2count218 = System.Math.Max(0, (System.Int32)(((System.Int32)(_qwh8ts9f) - __81fgg2dlsvn218 + __81fgg2step218) / __81fgg2step218)), _b5p6od9s = __81fgg2dlsvn218; __81fgg2count218 != 0; __81fgg2count218--, _b5p6od9s += (__81fgg2step218)) {

				{
					
					*(_xdbczr8u+(_ev4xhht5 - 1) + (_b5p6od9s - 1) * 1 * (_h4ibbatv)) = (-((_irk8i6qr * *(_xdbczr8u+(_qwh8ts9f - 1) + (_b5p6od9s - 1) * 1 * (_h4ibbatv)))));
					*(_1469pg8i+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_sh6ez9uf)) = (_3crf0qn3 * *(_xdbczr8u+(_qwh8ts9f - 1) + (_b5p6od9s - 1) * 1 * (_h4ibbatv)));
Mark170:;
					// continue
				}
								}			}
			{
				System.Int32 __81fgg2dlsvn219 = (System.Int32)(_3lfkfha8);
				const System.Int32 __81fgg2step219 = (System.Int32)((int)1);
				System.Int32 __81fgg2count219;
				for (__81fgg2count219 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn219 + __81fgg2step219) / __81fgg2step219)), _b5p6od9s = __81fgg2dlsvn219; __81fgg2count219 != 0; __81fgg2count219--, _b5p6od9s += (__81fgg2step219)) {

				{
					
					*(_1469pg8i+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_sh6ez9uf)) = (_irk8i6qr * *(_xdbczr8u+(_ev4xhht5 - 1) + (_b5p6od9s - 1) * 1 * (_h4ibbatv)));
					*(_xdbczr8u+(_ev4xhht5 - 1) + (_b5p6od9s - 1) * 1 * (_h4ibbatv)) = (_3crf0qn3 * *(_xdbczr8u+(_ev4xhht5 - 1) + (_b5p6od9s - 1) * 1 * (_h4ibbatv)));
Mark180:;
					// continue
				}
								}			}
		}
		else
		{
			
			_gvjhlct0(ref _ev4xhht5 ,(_xdbczr8u+(_qwh8ts9f - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv ,(_1469pg8i+((int)1 - 1) + ((int)1 - 1) * 1 * (_sh6ez9uf)),ref _sh6ez9uf );
		}
		
		if (_ev4xhht5 > _dxpq0xkr)
		{
			
			_gvjhlct0(ref _ev4xhht5 ,(_xdbczr8u+(_ev4xhht5 - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv ,(_1469pg8i+(_ev4xhht5 - 1) + ((int)1 - 1) * 1 * (_sh6ez9uf)),ref _sh6ez9uf );
		}
		//* 
		//*     The deflated singular values and their corresponding vectors go 
		//*     into the back of D, U, and V respectively. 
		//* 
		
		if (_dxpq0xkr > _umlkckdg)
		{
			
			_gvjhlct0(ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,(_1r8q3o4r+(_umlkckdg + (int)1 - 1)),ref Unsafe.AsRef((int)1) ,(_plfm7z8g+(_umlkckdg + (int)1 - 1)),ref Unsafe.AsRef((int)1) );
			_hhtvj1kb("A" ,ref _dxpq0xkr ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,(_s6mwvivs+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_qz188u0m)),ref _qz188u0m ,(_7u55mqkq+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b );
			_hhtvj1kb("A" ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref _ev4xhht5 ,(_1469pg8i+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_sh6ez9uf)),ref _sh6ez9uf ,(_xdbczr8u+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv );
		}
		//* 
		//*     Copy CTOT into COLTYP for referencing in DLASD3. 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn220 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step220 = (System.Int32)((int)1);
			System.Int32 __81fgg2count220;
			for (__81fgg2count220 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)4) - __81fgg2dlsvn220 + __81fgg2step220) / __81fgg2step220)), _znpjgsef = __81fgg2dlsvn220; __81fgg2count220 != 0; __81fgg2count220--, _znpjgsef += (__81fgg2step220)) {

			{
				
				*(_gj7zf775+(_znpjgsef - 1)) = *(_9wgmm024+(_znpjgsef - 1));
Mark190:;
				// continue
			}
						}		}//* 
		
		return;//* 
		//*     End of DLASD2 
		//* 
		
	}
	
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
