
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
//*> \brief \b CLALS0 applies back multiplying factors in solving the least squares problem using divide and conquer SVD approach. Used by sgelsd. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CLALS0 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/clals0.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/clals0.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/clals0.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CLALS0( ICOMPQ, NL, NR, SQRE, NRHS, B, LDB, BX, LDBX, 
//*                          PERM, GIVPTR, GIVCOL, LDGCOL, GIVNUM, LDGNUM, 
//*                          POLES, DIFL, DIFR, Z, K, C, S, RWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            GIVPTR, ICOMPQ, INFO, K, LDB, LDBX, LDGCOL, 
//*      $                   LDGNUM, NL, NR, NRHS, SQRE 
//*       REAL               C, S 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            GIVCOL( LDGCOL, * ), PERM( * ) 
//*       REAL               DIFL( * ), DIFR( LDGNUM, * ), 
//*      $                   GIVNUM( LDGNUM, * ), POLES( LDGNUM, * ), 
//*      $                   RWORK( * ), Z( * ) 
//*       COMPLEX            B( LDB, * ), BX( LDBX, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CLALS0 applies back the multiplying factors of either the left or the 
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
//*>          B is COMPLEX array, dimension ( LDB, NRHS ) 
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
//*>          BX is COMPLEX array, dimension ( LDBX, NRHS ) 
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
//*>          GIVNUM is REAL array, dimension ( LDGNUM, 2 ) 
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
//*>          POLES is REAL array, dimension ( LDGNUM, 2 ) 
//*>         On entry, POLES(1:K, 1) contains the new singular 
//*>         values obtained from solving the secular equation, and 
//*>         POLES(1:K, 2) is an array containing the poles in the secular 
//*>         equation. 
//*> \endverbatim 
//*> 
//*> \param[in] DIFL 
//*> \verbatim 
//*>          DIFL is REAL array, dimension ( K ). 
//*>         On entry, DIFL(I) is the distance between I-th updated 
//*>         (undeflated) singular value and the I-th (undeflated) old 
//*>         singular value. 
//*> \endverbatim 
//*> 
//*> \param[in] DIFR 
//*> \verbatim 
//*>          DIFR is REAL array, dimension ( LDGNUM, 2 ). 
//*>         On entry, DIFR(I, 1) contains the distances between I-th 
//*>         updated (undeflated) singular value and the I+1-th 
//*>         (undeflated) old singular value. And DIFR(I, 2) is the 
//*>         normalizing factor for the I-th right singular vector. 
//*> \endverbatim 
//*> 
//*> \param[in] Z 
//*> \verbatim 
//*>          Z is REAL array, dimension ( K ) 
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
//*>          C is REAL 
//*>         C contains garbage if SQRE =0 and the C-value of a Givens 
//*>         rotation related to the right null space if SQRE = 1. 
//*> \endverbatim 
//*> 
//*> \param[in] S 
//*> \verbatim 
//*>          S is REAL 
//*>         S contains garbage if SQRE =0 and the S-value of a Givens 
//*>         rotation related to the right null space if SQRE = 1. 
//*> \endverbatim 
//*> 
//*> \param[out] RWORK 
//*> \verbatim 
//*>          RWORK is REAL array, dimension 
//*>         ( K*(1+NRHS) + 2*NRHS ) 
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
//*> \ingroup complexOTHERcomputational 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>     Ming Gu and Ren-Cang Li, Computer Science Division, University of 
//*>       California at Berkeley, USA \n 
//*>     Osni Marques, LBNL/NERSC, USA \n 
//* 
//*  ===================================================================== 

	 
	public static void _ubpr5m2k(ref Int32 _y1be9goe, ref Int32 _zx57w4aj, ref Int32 _oqpc3yjg, ref Int32 _9qyq7j3e, ref Int32 _3nayvi7h, fcomplex* _p9n405a5, ref Int32 _ly9opahg, fcomplex* _uqckf55l, ref Int32 _hde8nv3t, Int32* _umao48xu, ref Int32 _8vecpt74, Int32* _0zwn6fsy, ref Int32 _uhi0ls8i, Single* _gh266ol1, ref Int32 _jlfchtn9, Single* _7nk40y8b, Single* _i8976ehd, Single* _doljbvm2, Single* _7e60fcso, ref Int32 _umlkckdg, ref Single _3crf0qn3, ref Single _irk8i6qr, Single* _dqanbbw3, ref Int32 _gro5yvfo)
	{
#region variable declarations
Single _kxg5drh2 =  1f;
Single _d0547bi2 =  0f;
Single _2v8pp9oq =  -1f;
Int32 _b5p6od9s =  default;
Int32 _znpjgsef =  default;
Int32 _vmeebitr =  default;
Int32 _hgi9ttgs =  default;
Int32 _ev4xhht5 =  default;
Int32 _dxpq0xkr =  default;
Int32 _qwh8ts9f =  default;
Single _ci8g9yb9 =  default;
Single _w6j9q0j8 =  default;
Single _a67yxlco =  default;
Single _ydtysk30 =  default;
Single _uzyphv61 =  default;
Single _1ajfmh55 =  default;
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
			
			_ut9qalzx("CLALS0" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
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
				System.Int32 __81fgg2dlsvn1969 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1969 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1969;
				for (__81fgg2count1969 = System.Math.Max(0, (System.Int32)(((System.Int32)(_8vecpt74) - __81fgg2dlsvn1969 + __81fgg2step1969) / __81fgg2step1969)), _b5p6od9s = __81fgg2dlsvn1969; __81fgg2count1969 != 0; __81fgg2count1969--, _b5p6od9s += (__81fgg2step1969)) {

				{
					
					_fvro911c(ref _3nayvi7h ,(_p9n405a5+(*(_0zwn6fsy+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_uhi0ls8i)) - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_p9n405a5+(*(_0zwn6fsy+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_uhi0ls8i)) - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(*(_gh266ol1+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9))) ,ref Unsafe.AsRef(*(_gh266ol1+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_jlfchtn9))) );
Mark10:;
					// continue
				}
								}			}//* 
			//*        Step (2L): permute rows of B. 
			//* 
			
			_33e0jk6i(ref _3nayvi7h ,(_p9n405a5+(_qwh8ts9f - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_uqckf55l+((int)1 - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t );
			{
				System.Int32 __81fgg2dlsvn1970 = (System.Int32)((int)2);
				const System.Int32 __81fgg2step1970 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1970;
				for (__81fgg2count1970 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1970 + __81fgg2step1970) / __81fgg2step1970)), _b5p6od9s = __81fgg2dlsvn1970; __81fgg2count1970 != 0; __81fgg2count1970--, _b5p6od9s += (__81fgg2step1970)) {

				{
					
					_33e0jk6i(ref _3nayvi7h ,(_p9n405a5+(*(_umao48xu+(_b5p6od9s - 1)) - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_uqckf55l+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t );
Mark20:;
					// continue
				}
								}			}//* 
			//*        Step (3L): apply the inverse of the left singular vector 
			//*        matrix to BX. 
			//* 
			
			if (_umlkckdg == (int)1)
			{
				
				_33e0jk6i(ref _3nayvi7h ,_uqckf55l ,ref _hde8nv3t ,_p9n405a5 ,ref _ly9opahg );
				if (*(_7e60fcso+((int)1 - 1)) < _d0547bi2)
				{
					
					_2ylagitj(ref _3nayvi7h ,ref Unsafe.AsRef(_2v8pp9oq) ,_p9n405a5 ,ref _ly9opahg );
				}
				
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn1971 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1971 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1971;
					for (__81fgg2count1971 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1971 + __81fgg2step1971) / __81fgg2step1971)), _znpjgsef = __81fgg2dlsvn1971; __81fgg2count1971 != 0; __81fgg2count1971--, _znpjgsef += (__81fgg2step1971)) {

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
							
							*(_dqanbbw3+(_znpjgsef - 1)) = _d0547bi2;
						}
						else
						{
							
							*(_dqanbbw3+(_znpjgsef - 1)) = (-((((*(_7nk40y8b+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)) * *(_7e60fcso+(_znpjgsef - 1))) / _ci8g9yb9) / (*(_7nk40y8b+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)) + _a67yxlco))));
						}
						
						{
							System.Int32 __81fgg2dlsvn1972 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step1972 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1972;
							for (__81fgg2count1972 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn1972 + __81fgg2step1972) / __81fgg2step1972)), _b5p6od9s = __81fgg2dlsvn1972; __81fgg2count1972 != 0; __81fgg2count1972--, _b5p6od9s += (__81fgg2step1972)) {

							{
								
								if ((*(_7e60fcso+(_b5p6od9s - 1)) == _d0547bi2) | (*(_7nk40y8b+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)) == _d0547bi2))
								{
									
									*(_dqanbbw3+(_b5p6od9s - 1)) = _d0547bi2;
								}
								else
								{
									
									*(_dqanbbw3+(_b5p6od9s - 1)) = (((*(_7nk40y8b+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)) * *(_7e60fcso+(_b5p6od9s - 1))) / (_a1q56mil(ref Unsafe.AsRef(*(_7nk40y8b+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9))) ,ref _ydtysk30 ) - _ci8g9yb9)) / (*(_7nk40y8b+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)) + _a67yxlco));
								}
								
Mark30:;
								// continue
							}
														}						}
						{
							System.Int32 __81fgg2dlsvn1973 = (System.Int32)((_znpjgsef + (int)1));
							const System.Int32 __81fgg2step1973 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1973;
							for (__81fgg2count1973 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1973 + __81fgg2step1973) / __81fgg2step1973)), _b5p6od9s = __81fgg2dlsvn1973; __81fgg2count1973 != 0; __81fgg2count1973--, _b5p6od9s += (__81fgg2step1973)) {

							{
								
								if ((*(_7e60fcso+(_b5p6od9s - 1)) == _d0547bi2) | (*(_7nk40y8b+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)) == _d0547bi2))
								{
									
									*(_dqanbbw3+(_b5p6od9s - 1)) = _d0547bi2;
								}
								else
								{
									
									*(_dqanbbw3+(_b5p6od9s - 1)) = (((*(_7nk40y8b+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)) * *(_7e60fcso+(_b5p6od9s - 1))) / (_a1q56mil(ref Unsafe.AsRef(*(_7nk40y8b+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9))) ,ref _uzyphv61 ) + _w6j9q0j8)) / (*(_7nk40y8b+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)) + _a67yxlco));
								}
								
Mark40:;
								// continue
							}
														}						}
						*(_dqanbbw3+((int)1 - 1)) = _2v8pp9oq;
						_1ajfmh55 = _z20xbrro(ref _umlkckdg ,_dqanbbw3 ,ref Unsafe.AsRef((int)1) );//* 
						//*              Since B and BX are complex, the following call to SGEMV 
						//*              is performed in two steps (real and imaginary parts). 
						//* 
						//*              CALL SGEMV( 'T', K, NRHS, ONE, BX, LDBX, WORK, 1, ZERO, 
						//*    $                     B( J, 1 ), LDB ) 
						//* 
						
						_b5p6od9s = (_umlkckdg + (_3nayvi7h * (int)2));
						{
							System.Int32 __81fgg2dlsvn1974 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step1974 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1974;
							for (__81fgg2count1974 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn1974 + __81fgg2step1974) / __81fgg2step1974)), _vmeebitr = __81fgg2dlsvn1974; __81fgg2count1974 != 0; __81fgg2count1974--, _vmeebitr += (__81fgg2step1974)) {

							{
								
								{
									System.Int32 __81fgg2dlsvn1975 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1975 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1975;
									for (__81fgg2count1975 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1975 + __81fgg2step1975) / __81fgg2step1975)), _hgi9ttgs = __81fgg2dlsvn1975; __81fgg2count1975 != 0; __81fgg2count1975--, _hgi9ttgs += (__81fgg2step1975)) {

									{
										
										_b5p6od9s = (_b5p6od9s + (int)1);
										*(_dqanbbw3+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.REAL(*(_uqckf55l+(_hgi9ttgs - 1) + (_vmeebitr - 1) * 1 * (_hde8nv3t)) );
Mark50:;
										// continue
									}
																		}								}
Mark60:;
								// continue
							}
														}						}
						_9mvi1n8m("T" ,ref _umlkckdg ,ref _3nayvi7h ,ref Unsafe.AsRef(_kxg5drh2) ,(_dqanbbw3+(((int)1 + _umlkckdg) + (_3nayvi7h * (int)2) - 1)),ref _umlkckdg ,(_dqanbbw3+((int)1 - 1)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+((int)1 + _umlkckdg - 1)),ref Unsafe.AsRef((int)1) );
						_b5p6od9s = (_umlkckdg + (_3nayvi7h * (int)2));
						{
							System.Int32 __81fgg2dlsvn1976 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step1976 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1976;
							for (__81fgg2count1976 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn1976 + __81fgg2step1976) / __81fgg2step1976)), _vmeebitr = __81fgg2dlsvn1976; __81fgg2count1976 != 0; __81fgg2count1976--, _vmeebitr += (__81fgg2step1976)) {

							{
								
								{
									System.Int32 __81fgg2dlsvn1977 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1977 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1977;
									for (__81fgg2count1977 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1977 + __81fgg2step1977) / __81fgg2step1977)), _hgi9ttgs = __81fgg2dlsvn1977; __81fgg2count1977 != 0; __81fgg2count1977--, _hgi9ttgs += (__81fgg2step1977)) {

									{
										
										_b5p6od9s = (_b5p6od9s + (int)1);
										*(_dqanbbw3+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.AIMAG(*(_uqckf55l+(_hgi9ttgs - 1) + (_vmeebitr - 1) * 1 * (_hde8nv3t)) );
Mark70:;
										// continue
									}
																		}								}
Mark80:;
								// continue
							}
														}						}
						_9mvi1n8m("T" ,ref _umlkckdg ,ref _3nayvi7h ,ref Unsafe.AsRef(_kxg5drh2) ,(_dqanbbw3+(((int)1 + _umlkckdg) + (_3nayvi7h * (int)2) - 1)),ref _umlkckdg ,(_dqanbbw3+((int)1 - 1)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+(((int)1 + _umlkckdg) + _3nayvi7h - 1)),ref Unsafe.AsRef((int)1) );
						{
							System.Int32 __81fgg2dlsvn1978 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step1978 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1978;
							for (__81fgg2count1978 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn1978 + __81fgg2step1978) / __81fgg2step1978)), _vmeebitr = __81fgg2dlsvn1978; __81fgg2count1978 != 0; __81fgg2count1978--, _vmeebitr += (__81fgg2step1978)) {

							{
								
								*(_p9n405a5+(_znpjgsef - 1) + (_vmeebitr - 1) * 1 * (_ly9opahg)) = ILNumerics.F2NET.Intrinsics.CMPLX(*(_dqanbbw3+(_vmeebitr + _umlkckdg - 1)) ,*(_dqanbbw3+((_vmeebitr + _umlkckdg) + _3nayvi7h - 1)) );
Mark90:;
								// continue
							}
														}						}
						_0asigtd4("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _1ajfmh55 ,ref Unsafe.AsRef(_kxg5drh2) ,ref Unsafe.AsRef((int)1) ,ref _3nayvi7h ,(_p9n405a5+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref _gro5yvfo );
Mark100:;
						// continue
					}
										}				}
			}
			//* 
			//*        Move the deflated rows of BX to B also. 
			//* 
			
			if (_umlkckdg < ILNumerics.F2NET.Intrinsics.MAX(_ev4xhht5 ,_dxpq0xkr ))
			_szaic8qw("A" ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref _3nayvi7h ,(_uqckf55l+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t ,(_p9n405a5+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg );
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
				
				_33e0jk6i(ref _3nayvi7h ,_p9n405a5 ,ref _ly9opahg ,_uqckf55l ,ref _hde8nv3t );
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn1979 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1979 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1979;
					for (__81fgg2count1979 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1979 + __81fgg2step1979) / __81fgg2step1979)), _znpjgsef = __81fgg2dlsvn1979; __81fgg2count1979 != 0; __81fgg2count1979--, _znpjgsef += (__81fgg2step1979)) {

					{
						
						_ydtysk30 = *(_7nk40y8b+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_jlfchtn9));
						if (*(_7e60fcso+(_znpjgsef - 1)) == _d0547bi2)
						{
							
							*(_dqanbbw3+(_znpjgsef - 1)) = _d0547bi2;
						}
						else
						{
							
							*(_dqanbbw3+(_znpjgsef - 1)) = (-((((*(_7e60fcso+(_znpjgsef - 1)) / *(_i8976ehd+(_znpjgsef - 1))) / (_ydtysk30 + *(_7nk40y8b+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_jlfchtn9)))) / *(_doljbvm2+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)))));
						}
						
						{
							System.Int32 __81fgg2dlsvn1980 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step1980 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1980;
							for (__81fgg2count1980 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn1980 + __81fgg2step1980) / __81fgg2step1980)), _b5p6od9s = __81fgg2dlsvn1980; __81fgg2count1980 != 0; __81fgg2count1980--, _b5p6od9s += (__81fgg2step1980)) {

							{
								
								if (*(_7e60fcso+(_znpjgsef - 1)) == _d0547bi2)
								{
									
									*(_dqanbbw3+(_b5p6od9s - 1)) = _d0547bi2;
								}
								else
								{
									
									*(_dqanbbw3+(_b5p6od9s - 1)) = (((*(_7e60fcso+(_znpjgsef - 1)) / (_a1q56mil(ref _ydtysk30 ,ref Unsafe.AsRef(-(*(_7nk40y8b+(_b5p6od9s + (int)1 - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)))) ) - *(_doljbvm2+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_jlfchtn9)))) / (_ydtysk30 + *(_7nk40y8b+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_jlfchtn9)))) / *(_doljbvm2+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)));
								}
								
Mark110:;
								// continue
							}
														}						}
						{
							System.Int32 __81fgg2dlsvn1981 = (System.Int32)((_znpjgsef + (int)1));
							const System.Int32 __81fgg2step1981 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1981;
							for (__81fgg2count1981 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1981 + __81fgg2step1981) / __81fgg2step1981)), _b5p6od9s = __81fgg2dlsvn1981; __81fgg2count1981 != 0; __81fgg2count1981--, _b5p6od9s += (__81fgg2step1981)) {

							{
								
								if (*(_7e60fcso+(_znpjgsef - 1)) == _d0547bi2)
								{
									
									*(_dqanbbw3+(_b5p6od9s - 1)) = _d0547bi2;
								}
								else
								{
									
									*(_dqanbbw3+(_b5p6od9s - 1)) = (((*(_7e60fcso+(_znpjgsef - 1)) / (_a1q56mil(ref _ydtysk30 ,ref Unsafe.AsRef(-(*(_7nk40y8b+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)))) ) - *(_i8976ehd+(_b5p6od9s - 1)))) / (_ydtysk30 + *(_7nk40y8b+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_jlfchtn9)))) / *(_doljbvm2+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)));
								}
								
Mark120:;
								// continue
							}
														}						}//* 
						//*              Since B and BX are complex, the following call to SGEMV 
						//*              is performed in two steps (real and imaginary parts). 
						//* 
						//*              CALL SGEMV( 'T', K, NRHS, ONE, B, LDB, WORK, 1, ZERO, 
						//*    $                     BX( J, 1 ), LDBX ) 
						//* 
						
						_b5p6od9s = (_umlkckdg + (_3nayvi7h * (int)2));
						{
							System.Int32 __81fgg2dlsvn1982 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step1982 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1982;
							for (__81fgg2count1982 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn1982 + __81fgg2step1982) / __81fgg2step1982)), _vmeebitr = __81fgg2dlsvn1982; __81fgg2count1982 != 0; __81fgg2count1982--, _vmeebitr += (__81fgg2step1982)) {

							{
								
								{
									System.Int32 __81fgg2dlsvn1983 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1983 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1983;
									for (__81fgg2count1983 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1983 + __81fgg2step1983) / __81fgg2step1983)), _hgi9ttgs = __81fgg2dlsvn1983; __81fgg2count1983 != 0; __81fgg2count1983--, _hgi9ttgs += (__81fgg2step1983)) {

									{
										
										_b5p6od9s = (_b5p6od9s + (int)1);
										*(_dqanbbw3+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.REAL(*(_p9n405a5+(_hgi9ttgs - 1) + (_vmeebitr - 1) * 1 * (_ly9opahg)) );
Mark130:;
										// continue
									}
																		}								}
Mark140:;
								// continue
							}
														}						}
						_9mvi1n8m("T" ,ref _umlkckdg ,ref _3nayvi7h ,ref Unsafe.AsRef(_kxg5drh2) ,(_dqanbbw3+(((int)1 + _umlkckdg) + (_3nayvi7h * (int)2) - 1)),ref _umlkckdg ,(_dqanbbw3+((int)1 - 1)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+((int)1 + _umlkckdg - 1)),ref Unsafe.AsRef((int)1) );
						_b5p6od9s = (_umlkckdg + (_3nayvi7h * (int)2));
						{
							System.Int32 __81fgg2dlsvn1984 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step1984 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1984;
							for (__81fgg2count1984 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn1984 + __81fgg2step1984) / __81fgg2step1984)), _vmeebitr = __81fgg2dlsvn1984; __81fgg2count1984 != 0; __81fgg2count1984--, _vmeebitr += (__81fgg2step1984)) {

							{
								
								{
									System.Int32 __81fgg2dlsvn1985 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1985 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1985;
									for (__81fgg2count1985 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1985 + __81fgg2step1985) / __81fgg2step1985)), _hgi9ttgs = __81fgg2dlsvn1985; __81fgg2count1985 != 0; __81fgg2count1985--, _hgi9ttgs += (__81fgg2step1985)) {

									{
										
										_b5p6od9s = (_b5p6od9s + (int)1);
										*(_dqanbbw3+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.AIMAG(*(_p9n405a5+(_hgi9ttgs - 1) + (_vmeebitr - 1) * 1 * (_ly9opahg)) );
Mark150:;
										// continue
									}
																		}								}
Mark160:;
								// continue
							}
														}						}
						_9mvi1n8m("T" ,ref _umlkckdg ,ref _3nayvi7h ,ref Unsafe.AsRef(_kxg5drh2) ,(_dqanbbw3+(((int)1 + _umlkckdg) + (_3nayvi7h * (int)2) - 1)),ref _umlkckdg ,(_dqanbbw3+((int)1 - 1)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+(((int)1 + _umlkckdg) + _3nayvi7h - 1)),ref Unsafe.AsRef((int)1) );
						{
							System.Int32 __81fgg2dlsvn1986 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step1986 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1986;
							for (__81fgg2count1986 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn1986 + __81fgg2step1986) / __81fgg2step1986)), _vmeebitr = __81fgg2dlsvn1986; __81fgg2count1986 != 0; __81fgg2count1986--, _vmeebitr += (__81fgg2step1986)) {

							{
								
								*(_uqckf55l+(_znpjgsef - 1) + (_vmeebitr - 1) * 1 * (_hde8nv3t)) = ILNumerics.F2NET.Intrinsics.CMPLX(*(_dqanbbw3+(_vmeebitr + _umlkckdg - 1)) ,*(_dqanbbw3+((_vmeebitr + _umlkckdg) + _3nayvi7h - 1)) );
Mark170:;
								// continue
							}
														}						}
Mark180:;
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
				
				_33e0jk6i(ref _3nayvi7h ,(_p9n405a5+(_ev4xhht5 - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_uqckf55l+(_ev4xhht5 - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t );
				_fvro911c(ref _3nayvi7h ,(_uqckf55l+((int)1 - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t ,(_uqckf55l+(_ev4xhht5 - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t ,ref _3crf0qn3 ,ref _irk8i6qr );
			}
			
			if (_umlkckdg < ILNumerics.F2NET.Intrinsics.MAX(_ev4xhht5 ,_dxpq0xkr ))
			_szaic8qw("A" ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref _3nayvi7h ,(_p9n405a5+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_uqckf55l+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t );//* 
			//*        Step (3R): permute rows of B. 
			//* 
			
			_33e0jk6i(ref _3nayvi7h ,(_uqckf55l+((int)1 - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t ,(_p9n405a5+(_qwh8ts9f - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg );
			if (_9qyq7j3e == (int)1)
			{
				
				_33e0jk6i(ref _3nayvi7h ,(_uqckf55l+(_ev4xhht5 - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t ,(_p9n405a5+(_ev4xhht5 - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg );
			}
			
			{
				System.Int32 __81fgg2dlsvn1987 = (System.Int32)((int)2);
				const System.Int32 __81fgg2step1987 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1987;
				for (__81fgg2count1987 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1987 + __81fgg2step1987) / __81fgg2step1987)), _b5p6od9s = __81fgg2dlsvn1987; __81fgg2count1987 != 0; __81fgg2count1987--, _b5p6od9s += (__81fgg2step1987)) {

				{
					
					_33e0jk6i(ref _3nayvi7h ,(_uqckf55l+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t ,(_p9n405a5+(*(_umao48xu+(_b5p6od9s - 1)) - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg );
Mark190:;
					// continue
				}
								}			}//* 
			//*        Step (4R): apply back the Givens rotations performed. 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn1988 = (System.Int32)(_8vecpt74);
				System.Int32 __81fgg2step1988 = (System.Int32)((int)-1);
				System.Int32 __81fgg2count1988;
				for (__81fgg2count1988 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1988 + __81fgg2step1988) / __81fgg2step1988)), _b5p6od9s = __81fgg2dlsvn1988; __81fgg2count1988 != 0; __81fgg2count1988--, _b5p6od9s += (__81fgg2step1988)) {

				{
					
					_fvro911c(ref _3nayvi7h ,(_p9n405a5+(*(_0zwn6fsy+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_uhi0ls8i)) - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_p9n405a5+(*(_0zwn6fsy+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_uhi0ls8i)) - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(*(_gh266ol1+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9))) ,ref Unsafe.AsRef(-(*(_gh266ol1+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_jlfchtn9)))) );
Mark200:;
					// continue
				}
								}			}
		}
		//* 
		
		return;//* 
		//*     End of CLALS0 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
