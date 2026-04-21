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
//*> \brief \b DLALS0 applies back multiplying factors in solving the least squares problem using divide and conquer SVD approach. Used by sgelsd. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLALS0 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlals0.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlals0.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlals0.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLALS0( ICOMPQ, NL, NR, SQRE, NRHS, B, LDB, BX, LDBX, 
//*                          PERM, GIVPTR, GIVCOL, LDGCOL, GIVNUM, LDGNUM, 
//*                          POLES, DIFL, DIFR, Z, K, C, S, WORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            GIVPTR, ICOMPQ, INFO, K, LDB, LDBX, LDGCOL, 
//*      $                   LDGNUM, NL, NR, NRHS, SQRE 
//*       DOUBLE PRECISION   C, S 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            GIVCOL( LDGCOL, * ), PERM( * ) 
//*       DOUBLE PRECISION   B( LDB, * ), BX( LDBX, * ), DIFL( * ), 
//*      $                   DIFR( LDGNUM, * ), GIVNUM( LDGNUM, * ), 
//*      $                   POLES( LDGNUM, * ), WORK( * ), Z( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLALS0 applies back the multiplying factors of either the left or the 
//*> right singular vector matrix of a diagonal matrix appended by a row 
//*> to the right hand side matrix B in solving the least squares problem 
//*> using the divide-and-conquer SVD approach. 
//*> 
//*> For the left singular vector matrix, three types of orthogonal 
//*> matrices are involved: 
//*> 
//*> (1L) Givens rotations: the number of such rotations is GIVPTR; the 
//*>      pairs of columns/rows they were applied to are stored in GIVCOL; 
//*>      and the C- and S-values of these rotations are stored in GIVNUM. 
//*> 
//*> (2L) Permutation. The (NL+1)-st row of B is to be moved to the first 
//*>      row, and for J=2:N, PERM(J)-th row of B is to be moved to the 
//*>      J-th row. 
//*> 
//*> (3L) The left singular vector matrix of the remaining matrix. 
//*> 
//*> For the right singular vector matrix, four types of orthogonal 
//*> matrices are involved: 
//*> 
//*> (1R) The right singular vector matrix of the remaining matrix. 
//*> 
//*> (2R) If SQRE = 1, one extra Givens rotation to generate the right 
//*>      null space. 
//*> 
//*> (3R) The inverse transformation of (2L). 
//*> 
//*> (4R) The inverse transformation of (1L). 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] ICOMPQ 
//*> \verbatim 
//*>          ICOMPQ is INTEGER 
//*>         Specifies whether singular vectors are to be computed in 
//*>         factored form: 
//*>         = 0: Left singular vector matrix. 
//*>         = 1: Right singular vector matrix. 
//*> \endverbatim 
//*> 
//*> \param[in] NL 
//*> \verbatim 
//*>          NL is INTEGER 
//*>         The row dimension of the upper block. NL >= 1. 
//*> \endverbatim 
//*> 
//*> \param[in] NR 
//*> \verbatim 
//*>          NR is INTEGER 
//*>         The row dimension of the lower block. NR >= 1. 
//*> \endverbatim 
//*> 
//*> \param[in] SQRE 
//*> \verbatim 
//*>          SQRE is INTEGER 
//*>         = 0: the lower block is an NR-by-NR square matrix. 
//*>         = 1: the lower block is an NR-by-(NR+1) rectangular matrix. 
//*> 
//*>         The bidiagonal matrix has row dimension N = NL + NR + 1, 
//*>         and column dimension M = N + SQRE. 
//*> \endverbatim 
//*> 
//*> \param[in] NRHS 
//*> \verbatim 
//*>          NRHS is INTEGER 
//*>         The number of columns of B and BX. NRHS must be at least 1. 
//*> \endverbatim 
//*> 
//*> \param[in,out] B 
//*> \verbatim 
//*>          B is DOUBLE PRECISION array, dimension ( LDB, NRHS ) 
//*>         On input, B contains the right hand sides of the least 
//*>         squares problem in rows 1 through M. On output, B contains 
//*>         the solution X in rows 1 through N. 
//*> \endverbatim 
//*> 
//*> \param[in] LDB 
//*> \verbatim 
//*>          LDB is INTEGER 
//*>         The leading dimension of B. LDB must be at least 
//*>         max(1,MAX( M, N ) ). 
//*> \endverbatim 
//*> 
//*> \param[out] BX 
//*> \verbatim 
//*>          BX is DOUBLE PRECISION array, dimension ( LDBX, NRHS ) 
//*> \endverbatim 
//*> 
//*> \param[in] LDBX 
//*> \verbatim 
//*>          LDBX is INTEGER 
//*>         The leading dimension of BX. 
//*> \endverbatim 
//*> 
//*> \param[in] PERM 
//*> \verbatim 
//*>          PERM is INTEGER array, dimension ( N ) 
//*>         The permutations (from deflation and sorting) applied 
//*>         to the two blocks. 
//*> \endverbatim 
//*> 
//*> \param[in] GIVPTR 
//*> \verbatim 
//*>          GIVPTR is INTEGER 
//*>         The number of Givens rotations which took place in this 
//*>         subproblem. 
//*> \endverbatim 
//*> 
//*> \param[in] GIVCOL 
//*> \verbatim 
//*>          GIVCOL is INTEGER array, dimension ( LDGCOL, 2 ) 
//*>         Each pair of numbers indicates a pair of rows/columns 
//*>         involved in a Givens rotation. 
//*> \endverbatim 
//*> 
//*> \param[in] LDGCOL 
//*> \verbatim 
//*>          LDGCOL is INTEGER 
//*>         The leading dimension of GIVCOL, must be at least N. 
//*> \endverbatim 
//*> 
//*> \param[in] GIVNUM 
//*> \verbatim 
//*>          GIVNUM is DOUBLE PRECISION array, dimension ( LDGNUM, 2 ) 
//*>         Each number indicates the C or S value used in the 
//*>         corresponding Givens rotation. 
//*> \endverbatim 
//*> 
//*> \param[in] LDGNUM 
//*> \verbatim 
//*>          LDGNUM is INTEGER 
//*>         The leading dimension of arrays DIFR, POLES and 
//*>         GIVNUM, must be at least K. 
//*> \endverbatim 
//*> 
//*> \param[in] POLES 
//*> \verbatim 
//*>          POLES is DOUBLE PRECISION array, dimension ( LDGNUM, 2 ) 
//*>         On entry, POLES(1:K, 1) contains the new singular 
//*>         values obtained from solving the secular equation, and 
//*>         POLES(1:K, 2) is an array containing the poles in the secular 
//*>         equation. 
//*> \endverbatim 
//*> 
//*> \param[in] DIFL 
//*> \verbatim 
//*>          DIFL is DOUBLE PRECISION array, dimension ( K ). 
//*>         On entry, DIFL(I) is the distance between I-th updated 
//*>         (undeflated) singular value and the I-th (undeflated) old 
//*>         singular value. 
//*> \endverbatim 
//*> 
//*> \param[in] DIFR 
//*> \verbatim 
//*>          DIFR is DOUBLE PRECISION array, dimension ( LDGNUM, 2 ). 
//*>         On entry, DIFR(I, 1) contains the distances between I-th 
//*>         updated (undeflated) singular value and the I+1-th 
//*>         (undeflated) old singular value. And DIFR(I, 2) is the 
//*>         normalizing factor for the I-th right singular vector. 
//*> \endverbatim 
//*> 
//*> \param[in] Z 
//*> \verbatim 
//*>          Z is DOUBLE PRECISION array, dimension ( K ) 
//*>         Contain the components of the deflation-adjusted updating row 
//*>         vector. 
//*> \endverbatim 
//*> 
//*> \param[in] K 
//*> \verbatim 
//*>          K is INTEGER 
//*>         Contains the dimension of the non-deflated matrix, 
//*>         This is the order of the related secular equation. 1 <= K <=N. 
//*> \endverbatim 
//*> 
//*> \param[in] C 
//*> \verbatim 
//*>          C is DOUBLE PRECISION 
//*>         C contains garbage if SQRE =0 and the C-value of a Givens 
//*>         rotation related to the right null space if SQRE = 1. 
//*> \endverbatim 
//*> 
//*> \param[in] S 
//*> \verbatim 
//*>          S is DOUBLE PRECISION 
//*>         S contains garbage if SQRE =0 and the S-value of a Givens 
//*>         rotation related to the right null space if SQRE = 1. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is DOUBLE PRECISION array, dimension ( K ) 
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
//*> \date December 2016 
//* 
//*> \ingroup doubleOTHERcomputational 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>     Ming Gu and Ren-Cang Li, Computer Science Division, University of 
//*>       California at Berkeley, USA \n 
//*>     Osni Marques, LBNL/NERSC, USA \n 
//* 
//*  ===================================================================== 

	 
	public static void _hunskcqw(ref Int32 _y1be9goe, ref Int32 _zx57w4aj, ref Int32 _oqpc3yjg, ref Int32 _9qyq7j3e, ref Int32 _3nayvi7h, Double* _p9n405a5, ref Int32 _ly9opahg, Double* _uqckf55l, ref Int32 _hde8nv3t, Int32* _umao48xu, ref Int32 _8vecpt74, Int32* _0zwn6fsy, ref Int32 _uhi0ls8i, Double* _gh266ol1, ref Int32 _jlfchtn9, Double* _7nk40y8b, Double* _i8976ehd, Double* _doljbvm2, Double* _7e60fcso, ref Int32 _umlkckdg, ref Double _3crf0qn3, ref Double _irk8i6qr, Double* _apig8meb, ref Int32 _gro5yvfo)
	{
#region variable declarations
Double _kxg5drh2 =  1d;
Double _d0547bi2 =  0d;
Double _2v8pp9oq =  -1d;
Int32 _b5p6od9s =  default;
Int32 _znpjgsef =  default;
Int32 _ev4xhht5 =  default;
Int32 _dxpq0xkr =  default;
Int32 _qwh8ts9f =  default;
Double _ci8g9yb9 =  default;
Double _w6j9q0j8 =  default;
Double _a67yxlco =  default;
Double _ydtysk30 =  default;
Double _uzyphv61 =  default;
Double _1ajfmh55 =  default;
string fLanavab = default;
#endregion  variable declarations

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
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input parameters. 
		//* 
		
		_gro5yvfo = (int)0;
		_dxpq0xkr = ((_zx57w4aj + _oqpc3yjg) + (int)1);//* 
		
		if ((_y1be9goe < (int)0) | (_y1be9goe > (int)1))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (_zx57w4aj < (int)1)
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_oqpc3yjg < (int)1)
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if ((_9qyq7j3e < (int)0) | (_9qyq7j3e > (int)1))
		{
			
			_gro5yvfo = (int)-4;
		}
		else
		if (_3nayvi7h < (int)1)
		{
			
			_gro5yvfo = (int)-5;
		}
		else
		if (_ly9opahg < _dxpq0xkr)
		{
			
			_gro5yvfo = (int)-7;
		}
		else
		if (_hde8nv3t < _dxpq0xkr)
		{
			
			_gro5yvfo = (int)-9;
		}
		else
		if (_8vecpt74 < (int)0)
		{
			
			_gro5yvfo = (int)-11;
		}
		else
		if (_uhi0ls8i < _dxpq0xkr)
		{
			
			_gro5yvfo = (int)-13;
		}
		else
		if (_jlfchtn9 < _dxpq0xkr)
		{
			
			_gro5yvfo = (int)-15;
		}
		else
		if (_umlkckdg < (int)1)
		{
			
			_gro5yvfo = (int)-20;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("DLALS0" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		
		_ev4xhht5 = (_dxpq0xkr + _9qyq7j3e);
		_qwh8ts9f = (_zx57w4aj + (int)1);//* 
		
		if (_y1be9goe == (int)0)
		{
			//* 
			//*        Apply back orthogonal transformations from the left. 
			//* 
			//*        Step (1L): apply back the Givens rotations performed. 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn1871 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1871 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1871;
				for (__81fgg2count1871 = System.Math.Max(0, (System.Int32)(((System.Int32)(_8vecpt74) - __81fgg2dlsvn1871 + __81fgg2step1871) / __81fgg2step1871)), _b5p6od9s = __81fgg2dlsvn1871; __81fgg2count1871 != 0; __81fgg2count1871--, _b5p6od9s += (__81fgg2step1871)) {

				{
					
					_2197fa5i(ref _3nayvi7h ,(_p9n405a5+(*(_0zwn6fsy+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_uhi0ls8i)) - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_p9n405a5+(*(_0zwn6fsy+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_uhi0ls8i)) - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(*(_gh266ol1+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9))) ,ref Unsafe.AsRef(*(_gh266ol1+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_jlfchtn9))) );
Mark10:;
					// continue
				}
								}			}//* 
			//*        Step (2L): permute rows of B. 
			//* 
			
			_gvjhlct0(ref _3nayvi7h ,(_p9n405a5+(_qwh8ts9f - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_uqckf55l+((int)1 - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t );
			{
				System.Int32 __81fgg2dlsvn1872 = (System.Int32)((int)2);
				const System.Int32 __81fgg2step1872 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1872;
				for (__81fgg2count1872 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1872 + __81fgg2step1872) / __81fgg2step1872)), _b5p6od9s = __81fgg2dlsvn1872; __81fgg2count1872 != 0; __81fgg2count1872--, _b5p6od9s += (__81fgg2step1872)) {

				{
					
					_gvjhlct0(ref _3nayvi7h ,(_p9n405a5+(*(_umao48xu+(_b5p6od9s - 1)) - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_uqckf55l+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t );
Mark20:;
					// continue
				}
								}			}//* 
			//*        Step (3L): apply the inverse of the left singular vector 
			//*        matrix to BX. 
			//* 
			
			if (_umlkckdg == (int)1)
			{
				
				_gvjhlct0(ref _3nayvi7h ,_uqckf55l ,ref _hde8nv3t ,_p9n405a5 ,ref _ly9opahg );
				if (*(_7e60fcso+((int)1 - 1)) < _d0547bi2)
				{
					
					_f6jqcjk1(ref _3nayvi7h ,ref Unsafe.AsRef(_2v8pp9oq) ,_p9n405a5 ,ref _ly9opahg );
				}
				
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn1873 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1873 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1873;
					for (__81fgg2count1873 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1873 + __81fgg2step1873) / __81fgg2step1873)), _znpjgsef = __81fgg2dlsvn1873; __81fgg2count1873 != 0; __81fgg2count1873--, _znpjgsef += (__81fgg2step1873)) {

					{
						
						_ci8g9yb9 = *(_i8976ehd+(_znpjgsef - 1));
						_a67yxlco = *(_7nk40y8b+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_jlfchtn9));
						_ydtysk30 = (-(*(_7nk40y8b+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_jlfchtn9))));
						if (_znpjgsef < _umlkckdg)
						{
							
							_w6j9q0j8 = (-(*(_doljbvm2+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_jlfchtn9))));
							_uzyphv61 = (-(*(_7nk40y8b+(_znpjgsef + (int)1 - 1) + ((int)2 - 1) * 1 * (_jlfchtn9))));
						}
						
						if ((*(_7e60fcso+(_znpjgsef - 1)) == _d0547bi2) | (*(_7nk40y8b+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)) == _d0547bi2))
						{
							
							*(_apig8meb+(_znpjgsef - 1)) = _d0547bi2;
						}
						else
						{
							
							*(_apig8meb+(_znpjgsef - 1)) = (-((((*(_7nk40y8b+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)) * *(_7e60fcso+(_znpjgsef - 1))) / _ci8g9yb9) / (*(_7nk40y8b+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)) + _a67yxlco))));
						}
						
						{
							System.Int32 __81fgg2dlsvn1874 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step1874 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1874;
							for (__81fgg2count1874 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn1874 + __81fgg2step1874) / __81fgg2step1874)), _b5p6od9s = __81fgg2dlsvn1874; __81fgg2count1874 != 0; __81fgg2count1874--, _b5p6od9s += (__81fgg2step1874)) {

							{
								
								if ((*(_7e60fcso+(_b5p6od9s - 1)) == _d0547bi2) | (*(_7nk40y8b+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)) == _d0547bi2))
								{
									
									*(_apig8meb+(_b5p6od9s - 1)) = _d0547bi2;
								}
								else
								{
									
									*(_apig8meb+(_b5p6od9s - 1)) = (((*(_7nk40y8b+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)) * *(_7e60fcso+(_b5p6od9s - 1))) / (_mfhxzi0a(ref Unsafe.AsRef(*(_7nk40y8b+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9))) ,ref _ydtysk30 ) - _ci8g9yb9)) / (*(_7nk40y8b+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)) + _a67yxlco));
								}
								
Mark30:;
								// continue
							}
														}						}
						{
							System.Int32 __81fgg2dlsvn1875 = (System.Int32)((_znpjgsef + (int)1));
							const System.Int32 __81fgg2step1875 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1875;
							for (__81fgg2count1875 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1875 + __81fgg2step1875) / __81fgg2step1875)), _b5p6od9s = __81fgg2dlsvn1875; __81fgg2count1875 != 0; __81fgg2count1875--, _b5p6od9s += (__81fgg2step1875)) {

							{
								
								if ((*(_7e60fcso+(_b5p6od9s - 1)) == _d0547bi2) | (*(_7nk40y8b+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)) == _d0547bi2))
								{
									
									*(_apig8meb+(_b5p6od9s - 1)) = _d0547bi2;
								}
								else
								{
									
									*(_apig8meb+(_b5p6od9s - 1)) = (((*(_7nk40y8b+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)) * *(_7e60fcso+(_b5p6od9s - 1))) / (_mfhxzi0a(ref Unsafe.AsRef(*(_7nk40y8b+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9))) ,ref _uzyphv61 ) + _w6j9q0j8)) / (*(_7nk40y8b+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)) + _a67yxlco));
								}
								
Mark40:;
								// continue
							}
														}						}
						*(_apig8meb+((int)1 - 1)) = _2v8pp9oq;
						_1ajfmh55 = _gmlreqin(ref _umlkckdg ,_apig8meb ,ref Unsafe.AsRef((int)1) );
						_t5wmtd1j("T" ,ref _umlkckdg ,ref _3nayvi7h ,ref Unsafe.AsRef(_kxg5drh2) ,_uqckf55l ,ref _hde8nv3t ,_apig8meb ,ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,(_p9n405a5+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg );
						_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _1ajfmh55 ,ref Unsafe.AsRef(_kxg5drh2) ,ref Unsafe.AsRef((int)1) ,ref _3nayvi7h ,(_p9n405a5+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref _gro5yvfo );
Mark50:;
						// continue
					}
										}				}
			}
			//* 
			//*        Move the deflated rows of BX to B also. 
			//* 
			
			if (_umlkckdg < ILNumerics.F2NET.Intrinsics.MAX(_ev4xhht5 ,_dxpq0xkr ))
			_hhtvj1kb("A" ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref _3nayvi7h ,(_uqckf55l+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t ,(_p9n405a5+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg );
		}
		else
		{
			//* 
			//*        Apply back the right orthogonal transformations. 
			//* 
			//*        Step (1R): apply back the new right singular vector matrix 
			//*        to B. 
			//* 
			
			if (_umlkckdg == (int)1)
			{
				
				_gvjhlct0(ref _3nayvi7h ,_p9n405a5 ,ref _ly9opahg ,_uqckf55l ,ref _hde8nv3t );
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn1876 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1876 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1876;
					for (__81fgg2count1876 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1876 + __81fgg2step1876) / __81fgg2step1876)), _znpjgsef = __81fgg2dlsvn1876; __81fgg2count1876 != 0; __81fgg2count1876--, _znpjgsef += (__81fgg2step1876)) {

					{
						
						_ydtysk30 = *(_7nk40y8b+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_jlfchtn9));
						if (*(_7e60fcso+(_znpjgsef - 1)) == _d0547bi2)
						{
							
							*(_apig8meb+(_znpjgsef - 1)) = _d0547bi2;
						}
						else
						{
							
							*(_apig8meb+(_znpjgsef - 1)) = (-((((*(_7e60fcso+(_znpjgsef - 1)) / *(_i8976ehd+(_znpjgsef - 1))) / (_ydtysk30 + *(_7nk40y8b+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_jlfchtn9)))) / *(_doljbvm2+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)))));
						}
						
						{
							System.Int32 __81fgg2dlsvn1877 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step1877 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1877;
							for (__81fgg2count1877 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn1877 + __81fgg2step1877) / __81fgg2step1877)), _b5p6od9s = __81fgg2dlsvn1877; __81fgg2count1877 != 0; __81fgg2count1877--, _b5p6od9s += (__81fgg2step1877)) {

							{
								
								if (*(_7e60fcso+(_znpjgsef - 1)) == _d0547bi2)
								{
									
									*(_apig8meb+(_b5p6od9s - 1)) = _d0547bi2;
								}
								else
								{
									
									*(_apig8meb+(_b5p6od9s - 1)) = (((*(_7e60fcso+(_znpjgsef - 1)) / (_mfhxzi0a(ref _ydtysk30 ,ref Unsafe.AsRef(-(*(_7nk40y8b+(_b5p6od9s + (int)1 - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)))) ) - *(_doljbvm2+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_jlfchtn9)))) / (_ydtysk30 + *(_7nk40y8b+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_jlfchtn9)))) / *(_doljbvm2+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)));
								}
								
Mark60:;
								// continue
							}
														}						}
						{
							System.Int32 __81fgg2dlsvn1878 = (System.Int32)((_znpjgsef + (int)1));
							const System.Int32 __81fgg2step1878 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1878;
							for (__81fgg2count1878 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1878 + __81fgg2step1878) / __81fgg2step1878)), _b5p6od9s = __81fgg2dlsvn1878; __81fgg2count1878 != 0; __81fgg2count1878--, _b5p6od9s += (__81fgg2step1878)) {

							{
								
								if (*(_7e60fcso+(_znpjgsef - 1)) == _d0547bi2)
								{
									
									*(_apig8meb+(_b5p6od9s - 1)) = _d0547bi2;
								}
								else
								{
									
									*(_apig8meb+(_b5p6od9s - 1)) = (((*(_7e60fcso+(_znpjgsef - 1)) / (_mfhxzi0a(ref _ydtysk30 ,ref Unsafe.AsRef(-(*(_7nk40y8b+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)))) ) - *(_i8976ehd+(_b5p6od9s - 1)))) / (_ydtysk30 + *(_7nk40y8b+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_jlfchtn9)))) / *(_doljbvm2+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)));
								}
								
Mark70:;
								// continue
							}
														}						}
						_t5wmtd1j("T" ,ref _umlkckdg ,ref _3nayvi7h ,ref Unsafe.AsRef(_kxg5drh2) ,_p9n405a5 ,ref _ly9opahg ,_apig8meb ,ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,(_uqckf55l+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t );
Mark80:;
						// continue
					}
										}				}
			}
			//* 
			//*        Step (2R): if SQRE = 1, apply back the rotation that is 
			//*        related to the right null space of the subproblem. 
			//* 
			
			if (_9qyq7j3e == (int)1)
			{
				
				_gvjhlct0(ref _3nayvi7h ,(_p9n405a5+(_ev4xhht5 - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_uqckf55l+(_ev4xhht5 - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t );
				_2197fa5i(ref _3nayvi7h ,(_uqckf55l+((int)1 - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t ,(_uqckf55l+(_ev4xhht5 - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t ,ref _3crf0qn3 ,ref _irk8i6qr );
			}
			
			if (_umlkckdg < ILNumerics.F2NET.Intrinsics.MAX(_ev4xhht5 ,_dxpq0xkr ))
			_hhtvj1kb("A" ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref _3nayvi7h ,(_p9n405a5+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_uqckf55l+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t );//* 
			//*        Step (3R): permute rows of B. 
			//* 
			
			_gvjhlct0(ref _3nayvi7h ,(_uqckf55l+((int)1 - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t ,(_p9n405a5+(_qwh8ts9f - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg );
			if (_9qyq7j3e == (int)1)
			{
				
				_gvjhlct0(ref _3nayvi7h ,(_uqckf55l+(_ev4xhht5 - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t ,(_p9n405a5+(_ev4xhht5 - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg );
			}
			
			{
				System.Int32 __81fgg2dlsvn1879 = (System.Int32)((int)2);
				const System.Int32 __81fgg2step1879 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1879;
				for (__81fgg2count1879 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1879 + __81fgg2step1879) / __81fgg2step1879)), _b5p6od9s = __81fgg2dlsvn1879; __81fgg2count1879 != 0; __81fgg2count1879--, _b5p6od9s += (__81fgg2step1879)) {

				{
					
					_gvjhlct0(ref _3nayvi7h ,(_uqckf55l+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t ,(_p9n405a5+(*(_umao48xu+(_b5p6od9s - 1)) - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg );
Mark90:;
					// continue
				}
								}			}//* 
			//*        Step (4R): apply back the Givens rotations performed. 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn1880 = (System.Int32)(_8vecpt74);
				System.Int32 __81fgg2step1880 = (System.Int32)((int)-1);
				System.Int32 __81fgg2count1880;
				for (__81fgg2count1880 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1880 + __81fgg2step1880) / __81fgg2step1880)), _b5p6od9s = __81fgg2dlsvn1880; __81fgg2count1880 != 0; __81fgg2count1880--, _b5p6od9s += (__81fgg2step1880)) {

				{
					
					_2197fa5i(ref _3nayvi7h ,(_p9n405a5+(*(_0zwn6fsy+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_uhi0ls8i)) - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_p9n405a5+(*(_0zwn6fsy+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_uhi0ls8i)) - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(*(_gh266ol1+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9))) ,ref Unsafe.AsRef(-(*(_gh266ol1+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_jlfchtn9)))) );
Mark100:;
					// continue
				}
								}			}
		}
		//* 
		
		return;//* 
		//*     End of DLALS0 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
