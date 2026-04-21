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
//*> \brief \b DLALSA computes the SVD of the coefficient matrix in compact form. Used by sgelsd. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLALSA + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlalsa.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlalsa.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlalsa.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLALSA( ICOMPQ, SMLSIZ, N, NRHS, B, LDB, BX, LDBX, U, 
//*                          LDU, VT, K, DIFL, DIFR, Z, POLES, GIVPTR, 
//*                          GIVCOL, LDGCOL, PERM, GIVNUM, C, S, WORK, 
//*                          IWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            ICOMPQ, INFO, LDB, LDBX, LDGCOL, LDU, N, NRHS, 
//*      $                   SMLSIZ 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            GIVCOL( LDGCOL, * ), GIVPTR( * ), IWORK( * ), 
//*      $                   K( * ), PERM( LDGCOL, * ) 
//*       DOUBLE PRECISION   B( LDB, * ), BX( LDBX, * ), C( * ), 
//*      $                   DIFL( LDU, * ), DIFR( LDU, * ), 
//*      $                   GIVNUM( LDU, * ), POLES( LDU, * ), S( * ), 
//*      $                   U( LDU, * ), VT( LDU, * ), WORK( * ), 
//*      $                   Z( LDU, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLALSA is an itermediate step in solving the least squares problem 
//*> by computing the SVD of the coefficient matrix in compact form (The 
//*> singular vectors are computed as products of simple orthorgonal 
//*> matrices.). 
//*> 
//*> If ICOMPQ = 0, DLALSA applies the inverse of the left singular vector 
//*> matrix of an upper bidiagonal matrix to the right hand side; and if 
//*> ICOMPQ = 1, DLALSA applies the right singular vector matrix to the 
//*> right hand side. The singular vector matrices were generated in 
//*> compact form by DLALSA. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] ICOMPQ 
//*> \verbatim 
//*>          ICOMPQ is INTEGER 
//*>         Specifies whether the left or the right singular vector 
//*>         matrix is involved. 
//*>         = 0: Left singular vector matrix 
//*>         = 1: Right singular vector matrix 
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
//*>         The row and column dimensions of the upper bidiagonal matrix. 
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
//*>         squares problem in rows 1 through M. 
//*>         On output, B contains the solution X in rows 1 through N. 
//*> \endverbatim 
//*> 
//*> \param[in] LDB 
//*> \verbatim 
//*>          LDB is INTEGER 
//*>         The leading dimension of B in the calling subprogram. 
//*>         LDB must be at least max(1,MAX( M, N ) ). 
//*> \endverbatim 
//*> 
//*> \param[out] BX 
//*> \verbatim 
//*>          BX is DOUBLE PRECISION array, dimension ( LDBX, NRHS ) 
//*>         On exit, the result of applying the left or right singular 
//*>         vector matrix to B. 
//*> \endverbatim 
//*> 
//*> \param[in] LDBX 
//*> \verbatim 
//*>          LDBX is INTEGER 
//*>         The leading dimension of BX. 
//*> \endverbatim 
//*> 
//*> \param[in] U 
//*> \verbatim 
//*>          U is DOUBLE PRECISION array, dimension ( LDU, SMLSIZ ). 
//*>         On entry, U contains the left singular vector matrices of all 
//*>         subproblems at the bottom level. 
//*> \endverbatim 
//*> 
//*> \param[in] LDU 
//*> \verbatim 
//*>          LDU is INTEGER, LDU = > N. 
//*>         The leading dimension of arrays U, VT, DIFL, DIFR, 
//*>         POLES, GIVNUM, and Z. 
//*> \endverbatim 
//*> 
//*> \param[in] VT 
//*> \verbatim 
//*>          VT is DOUBLE PRECISION array, dimension ( LDU, SMLSIZ+1 ). 
//*>         On entry, VT**T contains the right singular vector matrices of 
//*>         all subproblems at the bottom level. 
//*> \endverbatim 
//*> 
//*> \param[in] K 
//*> \verbatim 
//*>          K is INTEGER array, dimension ( N ). 
//*> \endverbatim 
//*> 
//*> \param[in] DIFL 
//*> \verbatim 
//*>          DIFL is DOUBLE PRECISION array, dimension ( LDU, NLVL ). 
//*>         where NLVL = INT(log_2 (N/(SMLSIZ+1))) + 1. 
//*> \endverbatim 
//*> 
//*> \param[in] DIFR 
//*> \verbatim 
//*>          DIFR is DOUBLE PRECISION array, dimension ( LDU, 2 * NLVL ). 
//*>         On entry, DIFL(*, I) and DIFR(*, 2 * I -1) record 
//*>         distances between singular values on the I-th level and 
//*>         singular values on the (I -1)-th level, and DIFR(*, 2 * I) 
//*>         record the normalizing factors of the right singular vectors 
//*>         matrices of subproblems on I-th level. 
//*> \endverbatim 
//*> 
//*> \param[in] Z 
//*> \verbatim 
//*>          Z is DOUBLE PRECISION array, dimension ( LDU, NLVL ). 
//*>         On entry, Z(1, I) contains the components of the deflation- 
//*>         adjusted updating row vector for subproblems on the I-th 
//*>         level. 
//*> \endverbatim 
//*> 
//*> \param[in] POLES 
//*> \verbatim 
//*>          POLES is DOUBLE PRECISION array, dimension ( LDU, 2 * NLVL ). 
//*>         On entry, POLES(*, 2 * I -1: 2 * I) contains the new and old 
//*>         singular values involved in the secular equations on the I-th 
//*>         level. 
//*> \endverbatim 
//*> 
//*> \param[in] GIVPTR 
//*> \verbatim 
//*>          GIVPTR is INTEGER array, dimension ( N ). 
//*>         On entry, GIVPTR( I ) records the number of Givens 
//*>         rotations performed on the I-th problem on the computation 
//*>         tree. 
//*> \endverbatim 
//*> 
//*> \param[in] GIVCOL 
//*> \verbatim 
//*>          GIVCOL is INTEGER array, dimension ( LDGCOL, 2 * NLVL ). 
//*>         On entry, for each I, GIVCOL(*, 2 * I - 1: 2 * I) records the 
//*>         locations of Givens rotations performed on the I-th level on 
//*>         the computation tree. 
//*> \endverbatim 
//*> 
//*> \param[in] LDGCOL 
//*> \verbatim 
//*>          LDGCOL is INTEGER, LDGCOL = > N. 
//*>         The leading dimension of arrays GIVCOL and PERM. 
//*> \endverbatim 
//*> 
//*> \param[in] PERM 
//*> \verbatim 
//*>          PERM is INTEGER array, dimension ( LDGCOL, NLVL ). 
//*>         On entry, PERM(*, I) records permutations done on the I-th 
//*>         level of the computation tree. 
//*> \endverbatim 
//*> 
//*> \param[in] GIVNUM 
//*> \verbatim 
//*>          GIVNUM is DOUBLE PRECISION array, dimension ( LDU, 2 * NLVL ). 
//*>         On entry, GIVNUM(*, 2 *I -1 : 2 * I) records the C- and S- 
//*>         values of Givens rotations performed on the I-th level on the 
//*>         computation tree. 
//*> \endverbatim 
//*> 
//*> \param[in] C 
//*> \verbatim 
//*>          C is DOUBLE PRECISION array, dimension ( N ). 
//*>         On entry, if the I-th subproblem is not square, 
//*>         C( I ) contains the C-value of a Givens rotation related to 
//*>         the right null space of the I-th subproblem. 
//*> \endverbatim 
//*> 
//*> \param[in] S 
//*> \verbatim 
//*>          S is DOUBLE PRECISION array, dimension ( N ). 
//*>         On entry, if the I-th subproblem is not square, 
//*>         S( I ) contains the S-value of a Givens rotation related to 
//*>         the right null space of the I-th subproblem. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is DOUBLE PRECISION array, dimension (N) 
//*> \endverbatim 
//*> 
//*> \param[out] IWORK 
//*> \verbatim 
//*>          IWORK is INTEGER array, dimension (3*N) 
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

	 
	public static void _hack1l4q(ref Int32 _y1be9goe, ref Int32 _q1xpyios, ref Int32 _dxpq0xkr, ref Int32 _3nayvi7h, Double* _p9n405a5, ref Int32 _ly9opahg, Double* _uqckf55l, ref Int32 _hde8nv3t, Double* _7u55mqkq, ref Int32 _u6e6d39b, Double* _xdbczr8u, Int32* _umlkckdg, Double* _i8976ehd, Double* _doljbvm2, Double* _7e60fcso, Double* _7nk40y8b, Int32* _8vecpt74, Int32* _0zwn6fsy, ref Int32 _uhi0ls8i, Int32* _umao48xu, Double* _gh266ol1, Double* _3crf0qn3, Double* _irk8i6qr, Double* _apig8meb, Int32* _4b6rt45i, ref Int32 _gro5yvfo)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Int32 _b5p6od9s =  default;
Int32 _egqdmelt =  default;
Int32 _8jzcrkri =  default;
Int32 _3dimb00t =  default;
Int32 _q8fqp221 =  default;
Int32 _znpjgsef =  default;
Int32 _es1scagl =  default;
Int32 _jnnmt81a =  default;
Int32 _u0afxfs0 =  default;
Int32 _4ekj6112 =  default;
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
Int32 _9qyq7j3e =  default;
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
		if (_dxpq0xkr < _q1xpyios)
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if (_3nayvi7h < (int)1)
		{
			
			_gro5yvfo = (int)-4;
		}
		else
		if (_ly9opahg < _dxpq0xkr)
		{
			
			_gro5yvfo = (int)-6;
		}
		else
		if (_hde8nv3t < _dxpq0xkr)
		{
			
			_gro5yvfo = (int)-8;
		}
		else
		if (_u6e6d39b < _dxpq0xkr)
		{
			
			_gro5yvfo = (int)-10;
		}
		else
		if (_uhi0ls8i < _dxpq0xkr)
		{
			
			_gro5yvfo = (int)-19;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("DLALSA" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Book-keeping and  setting up the computation tree. 
		//* 
		
		_q8fqp221 = (int)1;
		_b53e0l58 = (_q8fqp221 + _dxpq0xkr);
		_9bq9s7q7 = (_b53e0l58 + _dxpq0xkr);//* 
		
		_56ok77t2(ref _dxpq0xkr ,ref _0n683y3x ,ref _rwm6akyl ,(_4b6rt45i+(_q8fqp221 - 1)),(_4b6rt45i+(_b53e0l58 - 1)),(_4b6rt45i+(_9bq9s7q7 - 1)),ref _q1xpyios );//* 
		//*     The following code applies back the left singular vector factors. 
		//*     For applying back the right singular vector factors, go to 50. 
		//* 
		
		if (_y1be9goe == (int)1)
		{
			goto Mark50;
		}
		//* 
		//*     The nodes on the bottom level of the tree were solved 
		//*     by DLASDQ. The corresponding left and right singular vector 
		//*     matrices are in explicit form. First apply back the left 
		//*     singular vector matrices. 
		//* 
		
		_v4ofzyw5 = ((_rwm6akyl + (int)1) / (int)2);
		{
			System.Int32 __81fgg2dlsvn1864 = (System.Int32)(_v4ofzyw5);
			const System.Int32 __81fgg2step1864 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1864;
			for (__81fgg2count1864 = System.Math.Max(0, (System.Int32)(((System.Int32)(_rwm6akyl) - __81fgg2dlsvn1864 + __81fgg2step1864) / __81fgg2step1864)), _b5p6od9s = __81fgg2dlsvn1864; __81fgg2count1864 != 0; __81fgg2count1864--, _b5p6od9s += (__81fgg2step1864)) {

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
				_oqpc3yjg = *(_4b6rt45i+(_9bq9s7q7 + _egqdmelt - 1));
				_cyu21nam = (_8jzcrkri - _zx57w4aj);
				_5tcb1chw = (_8jzcrkri + (int)1);
				_5nsxi69c("T" ,"N" ,ref _zx57w4aj ,ref _3nayvi7h ,ref _zx57w4aj ,ref Unsafe.AsRef(_kxg5drh2) ,(_7u55mqkq+(_cyu21nam - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_p9n405a5+(_cyu21nam - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(_d0547bi2) ,(_uqckf55l+(_cyu21nam - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t );
				_5nsxi69c("T" ,"N" ,ref _oqpc3yjg ,ref _3nayvi7h ,ref _oqpc3yjg ,ref Unsafe.AsRef(_kxg5drh2) ,(_7u55mqkq+(_5tcb1chw - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_p9n405a5+(_5tcb1chw - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(_d0547bi2) ,(_uqckf55l+(_5tcb1chw - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t );
Mark10:;
				// continue
			}
						}		}//* 
		//*     Next copy the rows of B that correspond to unchanged rows 
		//*     in the bidiagonal matrix to BX. 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn1865 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1865 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1865;
			for (__81fgg2count1865 = System.Math.Max(0, (System.Int32)(((System.Int32)(_rwm6akyl) - __81fgg2dlsvn1865 + __81fgg2step1865) / __81fgg2step1865)), _b5p6od9s = __81fgg2dlsvn1865; __81fgg2count1865 != 0; __81fgg2count1865--, _b5p6od9s += (__81fgg2step1865)) {

			{
				
				_8jzcrkri = *(_4b6rt45i+((_q8fqp221 + _b5p6od9s) - (int)1 - 1));
				_gvjhlct0(ref _3nayvi7h ,(_p9n405a5+(_8jzcrkri - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_uqckf55l+(_8jzcrkri - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t );
Mark20:;
				// continue
			}
						}		}//* 
		//*     Finally go through the left singular vector matrices of all 
		//*     the other subproblems bottom-up on the tree. 
		//* 
		
		_znpjgsef = __POW((int)2, _0n683y3x);
		_9qyq7j3e = (int)0;//* 
		
		{
			System.Int32 __81fgg2dlsvn1866 = (System.Int32)(_0n683y3x);
			System.Int32 __81fgg2step1866 = (System.Int32)((int)-1);
			System.Int32 __81fgg2count1866;
			for (__81fgg2count1866 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1866 + __81fgg2step1866) / __81fgg2step1866)), _u0afxfs0 = __81fgg2dlsvn1866; __81fgg2count1866 != 0; __81fgg2count1866--, _u0afxfs0 += (__81fgg2step1866)) {

			{
				
				_4ekj6112 = (((int)2 * _u0afxfs0) - (int)1);//* 
				//*        find the first node LF and last node LL on 
				//*        the current level LVL 
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
					System.Int32 __81fgg2dlsvn1867 = (System.Int32)(_es1scagl);
					const System.Int32 __81fgg2step1867 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1867;
					for (__81fgg2count1867 = System.Math.Max(0, (System.Int32)(((System.Int32)(_jnnmt81a) - __81fgg2dlsvn1867 + __81fgg2step1867) / __81fgg2step1867)), _b5p6od9s = __81fgg2dlsvn1867; __81fgg2count1867 != 0; __81fgg2count1867--, _b5p6od9s += (__81fgg2step1867)) {

					{
						
						_3dimb00t = (_b5p6od9s - (int)1);
						_8jzcrkri = *(_4b6rt45i+(_q8fqp221 + _3dimb00t - 1));
						_zx57w4aj = *(_4b6rt45i+(_b53e0l58 + _3dimb00t - 1));
						_oqpc3yjg = *(_4b6rt45i+(_9bq9s7q7 + _3dimb00t - 1));
						_cyu21nam = (_8jzcrkri - _zx57w4aj);
						_5tcb1chw = (_8jzcrkri + (int)1);
						_znpjgsef = (_znpjgsef - (int)1);
						_hunskcqw(ref _y1be9goe ,ref _zx57w4aj ,ref _oqpc3yjg ,ref _9qyq7j3e ,ref _3nayvi7h ,(_uqckf55l+(_cyu21nam - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t ,(_p9n405a5+(_cyu21nam - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_umao48xu+(_cyu21nam - 1) + (_u0afxfs0 - 1) * 1 * (_uhi0ls8i)),ref Unsafe.AsRef(*(_8vecpt74+(_znpjgsef - 1))) ,(_0zwn6fsy+(_cyu21nam - 1) + (_4ekj6112 - 1) * 1 * (_uhi0ls8i)),ref _uhi0ls8i ,(_gh266ol1+(_cyu21nam - 1) + (_4ekj6112 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_7nk40y8b+(_cyu21nam - 1) + (_4ekj6112 - 1) * 1 * (_u6e6d39b)),(_i8976ehd+(_cyu21nam - 1) + (_u0afxfs0 - 1) * 1 * (_u6e6d39b)),(_doljbvm2+(_cyu21nam - 1) + (_4ekj6112 - 1) * 1 * (_u6e6d39b)),(_7e60fcso+(_cyu21nam - 1) + (_u0afxfs0 - 1) * 1 * (_u6e6d39b)),ref Unsafe.AsRef(*(_umlkckdg+(_znpjgsef - 1))) ,ref Unsafe.AsRef(*(_3crf0qn3+(_znpjgsef - 1))) ,ref Unsafe.AsRef(*(_irk8i6qr+(_znpjgsef - 1))) ,_apig8meb ,ref _gro5yvfo );
Mark30:;
						// continue
					}
										}				}
Mark40:;
				// continue
			}
						}		}goto Mark90;//* 
		//*     ICOMPQ = 1: applying back the right singular vector factors. 
		//* 
		
Mark50:;
		// continue//* 
		//*     First now go through the right singular vector matrices of all 
		//*     the tree nodes top-down. 
		//* 
		
		_znpjgsef = (int)0;
		{
			System.Int32 __81fgg2dlsvn1868 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1868 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1868;
			for (__81fgg2count1868 = System.Math.Max(0, (System.Int32)(((System.Int32)(_0n683y3x) - __81fgg2dlsvn1868 + __81fgg2step1868) / __81fgg2step1868)), _u0afxfs0 = __81fgg2dlsvn1868; __81fgg2count1868 != 0; __81fgg2count1868--, _u0afxfs0 += (__81fgg2step1868)) {

			{
				
				_4ekj6112 = (((int)2 * _u0afxfs0) - (int)1);//* 
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
					System.Int32 __81fgg2dlsvn1869 = (System.Int32)(_jnnmt81a);
					System.Int32 __81fgg2step1869 = (System.Int32)((int)-1);
					System.Int32 __81fgg2count1869;
					for (__81fgg2count1869 = System.Math.Max(0, (System.Int32)(((System.Int32)(_es1scagl) - __81fgg2dlsvn1869 + __81fgg2step1869) / __81fgg2step1869)), _b5p6od9s = __81fgg2dlsvn1869; __81fgg2count1869 != 0; __81fgg2count1869--, _b5p6od9s += (__81fgg2step1869)) {

					{
						
						_3dimb00t = (_b5p6od9s - (int)1);
						_8jzcrkri = *(_4b6rt45i+(_q8fqp221 + _3dimb00t - 1));
						_zx57w4aj = *(_4b6rt45i+(_b53e0l58 + _3dimb00t - 1));
						_oqpc3yjg = *(_4b6rt45i+(_9bq9s7q7 + _3dimb00t - 1));
						_cyu21nam = (_8jzcrkri - _zx57w4aj);
						_5tcb1chw = (_8jzcrkri + (int)1);
						if (_b5p6od9s == _jnnmt81a)
						{
							
							_9qyq7j3e = (int)0;
						}
						else
						{
							
							_9qyq7j3e = (int)1;
						}
						
						_znpjgsef = (_znpjgsef + (int)1);
						_hunskcqw(ref _y1be9goe ,ref _zx57w4aj ,ref _oqpc3yjg ,ref _9qyq7j3e ,ref _3nayvi7h ,(_p9n405a5+(_cyu21nam - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_uqckf55l+(_cyu21nam - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t ,(_umao48xu+(_cyu21nam - 1) + (_u0afxfs0 - 1) * 1 * (_uhi0ls8i)),ref Unsafe.AsRef(*(_8vecpt74+(_znpjgsef - 1))) ,(_0zwn6fsy+(_cyu21nam - 1) + (_4ekj6112 - 1) * 1 * (_uhi0ls8i)),ref _uhi0ls8i ,(_gh266ol1+(_cyu21nam - 1) + (_4ekj6112 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_7nk40y8b+(_cyu21nam - 1) + (_4ekj6112 - 1) * 1 * (_u6e6d39b)),(_i8976ehd+(_cyu21nam - 1) + (_u0afxfs0 - 1) * 1 * (_u6e6d39b)),(_doljbvm2+(_cyu21nam - 1) + (_4ekj6112 - 1) * 1 * (_u6e6d39b)),(_7e60fcso+(_cyu21nam - 1) + (_u0afxfs0 - 1) * 1 * (_u6e6d39b)),ref Unsafe.AsRef(*(_umlkckdg+(_znpjgsef - 1))) ,ref Unsafe.AsRef(*(_3crf0qn3+(_znpjgsef - 1))) ,ref Unsafe.AsRef(*(_irk8i6qr+(_znpjgsef - 1))) ,_apig8meb ,ref _gro5yvfo );
Mark60:;
						// continue
					}
										}				}
Mark70:;
				// continue
			}
						}		}//* 
		//*     The nodes on the bottom level of the tree were solved 
		//*     by DLASDQ. The corresponding right singular vector 
		//*     matrices are in explicit form. Apply them back. 
		//* 
		
		_v4ofzyw5 = ((_rwm6akyl + (int)1) / (int)2);
		{
			System.Int32 __81fgg2dlsvn1870 = (System.Int32)(_v4ofzyw5);
			const System.Int32 __81fgg2step1870 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1870;
			for (__81fgg2count1870 = System.Math.Max(0, (System.Int32)(((System.Int32)(_rwm6akyl) - __81fgg2dlsvn1870 + __81fgg2step1870) / __81fgg2step1870)), _b5p6od9s = __81fgg2dlsvn1870; __81fgg2count1870 != 0; __81fgg2count1870--, _b5p6od9s += (__81fgg2step1870)) {

			{
				
				_egqdmelt = (_b5p6od9s - (int)1);
				_8jzcrkri = *(_4b6rt45i+(_q8fqp221 + _egqdmelt - 1));
				_zx57w4aj = *(_4b6rt45i+(_b53e0l58 + _egqdmelt - 1));
				_oqpc3yjg = *(_4b6rt45i+(_9bq9s7q7 + _egqdmelt - 1));
				_qwh8ts9f = (_zx57w4aj + (int)1);
				if (_b5p6od9s == _rwm6akyl)
				{
					
					_bds60snh = _oqpc3yjg;
				}
				else
				{
					
					_bds60snh = (_oqpc3yjg + (int)1);
				}
				
				_cyu21nam = (_8jzcrkri - _zx57w4aj);
				_5tcb1chw = (_8jzcrkri + (int)1);
				_5nsxi69c("T" ,"N" ,ref _qwh8ts9f ,ref _3nayvi7h ,ref _qwh8ts9f ,ref Unsafe.AsRef(_kxg5drh2) ,(_xdbczr8u+(_cyu21nam - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_p9n405a5+(_cyu21nam - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(_d0547bi2) ,(_uqckf55l+(_cyu21nam - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t );
				_5nsxi69c("T" ,"N" ,ref _bds60snh ,ref _3nayvi7h ,ref _bds60snh ,ref Unsafe.AsRef(_kxg5drh2) ,(_xdbczr8u+(_5tcb1chw - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_p9n405a5+(_5tcb1chw - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(_d0547bi2) ,(_uqckf55l+(_5tcb1chw - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t );
Mark80:;
				// continue
			}
						}		}//* 
		
Mark90:;
		// continue//* 
		
		return;//* 
		//*     End of DLALSA 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
