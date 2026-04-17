
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
//*> \brief \b ZLALS0 applies back multiplying factors in solving the least squares problem using divide and conquer SVD approach. Used by sgelsd. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZLALS0 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zlals0.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zlals0.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zlals0.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZLALS0( ICOMPQ, NL, NR, SQRE, NRHS, B, LDB, BX, LDBX, 
//*                          PERM, GIVPTR, GIVCOL, LDGCOL, GIVNUM, LDGNUM, 
//*                          POLES, DIFL, DIFR, Z, K, C, S, RWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            GIVPTR, ICOMPQ, INFO, K, LDB, LDBX, LDGCOL, 
//*      $                   LDGNUM, NL, NR, NRHS, SQRE 
//*       DOUBLE PRECISION   C, S 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            GIVCOL( LDGCOL, * ), PERM( * ) 
//*       DOUBLE PRECISION   DIFL( * ), DIFR( LDGNUM, * ), 
//*      $                   GIVNUM( LDGNUM, * ), POLES( LDGNUM, * ), 
//*      $                   RWORK( * ), Z( * ) 
//*       COMPLEX*16         B( LDB, * ), BX( LDBX, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZLALS0 applies back the multiplying factors of either the left or the 
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
//*>          B is COMPLEX*16 array, dimension ( LDB, NRHS ) 
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
//*>          BX is COMPLEX*16 array, dimension ( LDBX, NRHS ) 
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
//*> \param[out] RWORK 
//*> \verbatim 
//*>          RWORK is DOUBLE PRECISION array, dimension 
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
//*> \ingroup complex16OTHERcomputational 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>     Ming Gu and Ren-Cang Li, Computer Science Division, University of 
//*>       California at Berkeley, USA \n 
//*>     Osni Marques, LBNL/NERSC, USA \n 
//* 
//*  ===================================================================== 

	 
	public static void _f7b5qo7p(ref Int32 _y1be9goe, ref Int32 _zx57w4aj, ref Int32 _oqpc3yjg, ref Int32 _9qyq7j3e, ref Int32 _3nayvi7h, complex* _p9n405a5, ref Int32 _ly9opahg, complex* _uqckf55l, ref Int32 _hde8nv3t, Int32* _umao48xu, ref Int32 _8vecpt74, Int32* _0zwn6fsy, ref Int32 _uhi0ls8i, Double* _gh266ol1, ref Int32 _jlfchtn9, Double* _7nk40y8b, Double* _i8976ehd, Double* _doljbvm2, Double* _7e60fcso, ref Int32 _umlkckdg, ref Double _3crf0qn3, ref Double _irk8i6qr, Double* _dqanbbw3, ref Int32 _gro5yvfo)
	{
#region variable declarations
Double _kxg5drh2 =  1d;
Double _d0547bi2 =  0d;
Double _2v8pp9oq =  -1d;
Int32 _b5p6od9s =  default;
Int32 _znpjgsef =  default;
Int32 _vmeebitr =  default;
Int32 _hgi9ttgs =  default;
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
			
			_ut9qalzx("ZLALS0" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
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
				System.Int32 __81fgg2dlsvn2052 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2052 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2052;
				for (__81fgg2count2052 = System.Math.Max(0, (System.Int32)(((System.Int32)(_8vecpt74) - __81fgg2dlsvn2052 + __81fgg2step2052) / __81fgg2step2052)), _b5p6od9s = __81fgg2dlsvn2052; __81fgg2count2052 != 0; __81fgg2count2052--, _b5p6od9s += (__81fgg2step2052)) {

				{
					
					_yl1tlikm(ref _3nayvi7h ,(_p9n405a5+(*(_0zwn6fsy+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_uhi0ls8i)) - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_p9n405a5+(*(_0zwn6fsy+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_uhi0ls8i)) - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(*(_gh266ol1+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9))) ,ref Unsafe.AsRef(*(_gh266ol1+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_jlfchtn9))) );
Mark10:;
					// continue
				}
								}			}//* 
			//*        Step (2L): permute rows of B. 
			//* 
			
			_ly902k7t(ref _3nayvi7h ,(_p9n405a5+(_qwh8ts9f - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_uqckf55l+((int)1 - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t );
			{
				System.Int32 __81fgg2dlsvn2053 = (System.Int32)((int)2);
				const System.Int32 __81fgg2step2053 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2053;
				for (__81fgg2count2053 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2053 + __81fgg2step2053) / __81fgg2step2053)), _b5p6od9s = __81fgg2dlsvn2053; __81fgg2count2053 != 0; __81fgg2count2053--, _b5p6od9s += (__81fgg2step2053)) {

				{
					
					_ly902k7t(ref _3nayvi7h ,(_p9n405a5+(*(_umao48xu+(_b5p6od9s - 1)) - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_uqckf55l+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t );
Mark20:;
					// continue
				}
								}			}//* 
			//*        Step (3L): apply the inverse of the left singular vector 
			//*        matrix to BX. 
			//* 
			
			if (_umlkckdg == (int)1)
			{
				
				_ly902k7t(ref _3nayvi7h ,_uqckf55l ,ref _hde8nv3t ,_p9n405a5 ,ref _ly9opahg );
				if (*(_7e60fcso+((int)1 - 1)) < _d0547bi2)
				{
					
					_z5tkm94d(ref _3nayvi7h ,ref Unsafe.AsRef(_2v8pp9oq) ,_p9n405a5 ,ref _ly9opahg );
				}
				
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn2054 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step2054 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2054;
					for (__81fgg2count2054 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn2054 + __81fgg2step2054) / __81fgg2step2054)), _znpjgsef = __81fgg2dlsvn2054; __81fgg2count2054 != 0; __81fgg2count2054--, _znpjgsef += (__81fgg2step2054)) {

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
							System.Int32 __81fgg2dlsvn2055 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step2055 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2055;
							for (__81fgg2count2055 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn2055 + __81fgg2step2055) / __81fgg2step2055)), _b5p6od9s = __81fgg2dlsvn2055; __81fgg2count2055 != 0; __81fgg2count2055--, _b5p6od9s += (__81fgg2step2055)) {

							{
								
								if ((*(_7e60fcso+(_b5p6od9s - 1)) == _d0547bi2) | (*(_7nk40y8b+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)) == _d0547bi2))
								{
									
									*(_dqanbbw3+(_b5p6od9s - 1)) = _d0547bi2;
								}
								else
								{
									
									*(_dqanbbw3+(_b5p6od9s - 1)) = (((*(_7nk40y8b+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)) * *(_7e60fcso+(_b5p6od9s - 1))) / (_mfhxzi0a(ref Unsafe.AsRef(*(_7nk40y8b+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9))) ,ref _ydtysk30 ) - _ci8g9yb9)) / (*(_7nk40y8b+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)) + _a67yxlco));
								}
								
Mark30:;
								// continue
							}
														}						}
						{
							System.Int32 __81fgg2dlsvn2056 = (System.Int32)((_znpjgsef + (int)1));
							const System.Int32 __81fgg2step2056 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2056;
							for (__81fgg2count2056 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn2056 + __81fgg2step2056) / __81fgg2step2056)), _b5p6od9s = __81fgg2dlsvn2056; __81fgg2count2056 != 0; __81fgg2count2056--, _b5p6od9s += (__81fgg2step2056)) {

							{
								
								if ((*(_7e60fcso+(_b5p6od9s - 1)) == _d0547bi2) | (*(_7nk40y8b+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)) == _d0547bi2))
								{
									
									*(_dqanbbw3+(_b5p6od9s - 1)) = _d0547bi2;
								}
								else
								{
									
									*(_dqanbbw3+(_b5p6od9s - 1)) = (((*(_7nk40y8b+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)) * *(_7e60fcso+(_b5p6od9s - 1))) / (_mfhxzi0a(ref Unsafe.AsRef(*(_7nk40y8b+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9))) ,ref _uzyphv61 ) + _w6j9q0j8)) / (*(_7nk40y8b+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)) + _a67yxlco));
								}
								
Mark40:;
								// continue
							}
														}						}
						*(_dqanbbw3+((int)1 - 1)) = _2v8pp9oq;
						_1ajfmh55 = _gmlreqin(ref _umlkckdg ,_dqanbbw3 ,ref Unsafe.AsRef((int)1) );//* 
						//*              Since B and BX are complex, the following call to DGEMV 
						//*              is performed in two steps (real and imaginary parts). 
						//* 
						//*              CALL DGEMV( 'T', K, NRHS, ONE, BX, LDBX, WORK, 1, ZERO, 
						//*    $                     B( J, 1 ), LDB ) 
						//* 
						
						_b5p6od9s = (_umlkckdg + (_3nayvi7h * (int)2));
						{
							System.Int32 __81fgg2dlsvn2057 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step2057 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2057;
							for (__81fgg2count2057 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn2057 + __81fgg2step2057) / __81fgg2step2057)), _vmeebitr = __81fgg2dlsvn2057; __81fgg2count2057 != 0; __81fgg2count2057--, _vmeebitr += (__81fgg2step2057)) {

							{
								
								{
									System.Int32 __81fgg2dlsvn2058 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step2058 = (System.Int32)((int)1);
									System.Int32 __81fgg2count2058;
									for (__81fgg2count2058 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn2058 + __81fgg2step2058) / __81fgg2step2058)), _hgi9ttgs = __81fgg2dlsvn2058; __81fgg2count2058 != 0; __81fgg2count2058--, _hgi9ttgs += (__81fgg2step2058)) {

									{
										
										_b5p6od9s = (_b5p6od9s + (int)1);
										*(_dqanbbw3+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.DBLE(*(_uqckf55l+(_hgi9ttgs - 1) + (_vmeebitr - 1) * 1 * (_hde8nv3t)) );
Mark50:;
										// continue
									}
																		}								}
Mark60:;
								// continue
							}
														}						}
						_t5wmtd1j("T" ,ref _umlkckdg ,ref _3nayvi7h ,ref Unsafe.AsRef(_kxg5drh2) ,(_dqanbbw3+(((int)1 + _umlkckdg) + (_3nayvi7h * (int)2) - 1)),ref _umlkckdg ,(_dqanbbw3+((int)1 - 1)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+((int)1 + _umlkckdg - 1)),ref Unsafe.AsRef((int)1) );
						_b5p6od9s = (_umlkckdg + (_3nayvi7h * (int)2));
						{
							System.Int32 __81fgg2dlsvn2059 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step2059 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2059;
							for (__81fgg2count2059 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn2059 + __81fgg2step2059) / __81fgg2step2059)), _vmeebitr = __81fgg2dlsvn2059; __81fgg2count2059 != 0; __81fgg2count2059--, _vmeebitr += (__81fgg2step2059)) {

							{
								
								{
									System.Int32 __81fgg2dlsvn2060 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step2060 = (System.Int32)((int)1);
									System.Int32 __81fgg2count2060;
									for (__81fgg2count2060 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn2060 + __81fgg2step2060) / __81fgg2step2060)), _hgi9ttgs = __81fgg2dlsvn2060; __81fgg2count2060 != 0; __81fgg2count2060--, _hgi9ttgs += (__81fgg2step2060)) {

									{
										
										_b5p6od9s = (_b5p6od9s + (int)1);
										*(_dqanbbw3+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.DIMAG(*(_uqckf55l+(_hgi9ttgs - 1) + (_vmeebitr - 1) * 1 * (_hde8nv3t)) );
Mark70:;
										// continue
									}
																		}								}
Mark80:;
								// continue
							}
														}						}
						_t5wmtd1j("T" ,ref _umlkckdg ,ref _3nayvi7h ,ref Unsafe.AsRef(_kxg5drh2) ,(_dqanbbw3+(((int)1 + _umlkckdg) + (_3nayvi7h * (int)2) - 1)),ref _umlkckdg ,(_dqanbbw3+((int)1 - 1)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+(((int)1 + _umlkckdg) + _3nayvi7h - 1)),ref Unsafe.AsRef((int)1) );
						{
							System.Int32 __81fgg2dlsvn2061 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step2061 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2061;
							for (__81fgg2count2061 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn2061 + __81fgg2step2061) / __81fgg2step2061)), _vmeebitr = __81fgg2dlsvn2061; __81fgg2count2061 != 0; __81fgg2count2061--, _vmeebitr += (__81fgg2step2061)) {

							{
								
								*(_p9n405a5+(_znpjgsef - 1) + (_vmeebitr - 1) * 1 * (_ly9opahg)) = ILNumerics.F2NET.Intrinsics.DCMPLX(*(_dqanbbw3+(_vmeebitr + _umlkckdg - 1)) ,*(_dqanbbw3+((_vmeebitr + _umlkckdg) + _3nayvi7h - 1)) );
Mark90:;
								// continue
							}
														}						}
						_j6h8q4u5("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _1ajfmh55 ,ref Unsafe.AsRef(_kxg5drh2) ,ref Unsafe.AsRef((int)1) ,ref _3nayvi7h ,(_p9n405a5+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref _gro5yvfo );
Mark100:;
						// continue
					}
										}				}
			}
			//* 
			//*        Move the deflated rows of BX to B also. 
			//* 
			
			if (_umlkckdg < ILNumerics.F2NET.Intrinsics.MAX(_ev4xhht5 ,_dxpq0xkr ))
			_nihu9ses("A" ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref _3nayvi7h ,(_uqckf55l+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t ,(_p9n405a5+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg );
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
				
				_ly902k7t(ref _3nayvi7h ,_p9n405a5 ,ref _ly9opahg ,_uqckf55l ,ref _hde8nv3t );
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn2062 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step2062 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2062;
					for (__81fgg2count2062 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn2062 + __81fgg2step2062) / __81fgg2step2062)), _znpjgsef = __81fgg2dlsvn2062; __81fgg2count2062 != 0; __81fgg2count2062--, _znpjgsef += (__81fgg2step2062)) {

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
							System.Int32 __81fgg2dlsvn2063 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step2063 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2063;
							for (__81fgg2count2063 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn2063 + __81fgg2step2063) / __81fgg2step2063)), _b5p6od9s = __81fgg2dlsvn2063; __81fgg2count2063 != 0; __81fgg2count2063--, _b5p6od9s += (__81fgg2step2063)) {

							{
								
								if (*(_7e60fcso+(_znpjgsef - 1)) == _d0547bi2)
								{
									
									*(_dqanbbw3+(_b5p6od9s - 1)) = _d0547bi2;
								}
								else
								{
									
									*(_dqanbbw3+(_b5p6od9s - 1)) = (((*(_7e60fcso+(_znpjgsef - 1)) / (_mfhxzi0a(ref _ydtysk30 ,ref Unsafe.AsRef(-(*(_7nk40y8b+(_b5p6od9s + (int)1 - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)))) ) - *(_doljbvm2+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_jlfchtn9)))) / (_ydtysk30 + *(_7nk40y8b+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_jlfchtn9)))) / *(_doljbvm2+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)));
								}
								
Mark110:;
								// continue
							}
														}						}
						{
							System.Int32 __81fgg2dlsvn2064 = (System.Int32)((_znpjgsef + (int)1));
							const System.Int32 __81fgg2step2064 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2064;
							for (__81fgg2count2064 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn2064 + __81fgg2step2064) / __81fgg2step2064)), _b5p6od9s = __81fgg2dlsvn2064; __81fgg2count2064 != 0; __81fgg2count2064--, _b5p6od9s += (__81fgg2step2064)) {

							{
								
								if (*(_7e60fcso+(_znpjgsef - 1)) == _d0547bi2)
								{
									
									*(_dqanbbw3+(_b5p6od9s - 1)) = _d0547bi2;
								}
								else
								{
									
									*(_dqanbbw3+(_b5p6od9s - 1)) = (((*(_7e60fcso+(_znpjgsef - 1)) / (_mfhxzi0a(ref _ydtysk30 ,ref Unsafe.AsRef(-(*(_7nk40y8b+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)))) ) - *(_i8976ehd+(_b5p6od9s - 1)))) / (_ydtysk30 + *(_7nk40y8b+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_jlfchtn9)))) / *(_doljbvm2+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)));
								}
								
Mark120:;
								// continue
							}
														}						}//* 
						//*              Since B and BX are complex, the following call to DGEMV 
						//*              is performed in two steps (real and imaginary parts). 
						//* 
						//*              CALL DGEMV( 'T', K, NRHS, ONE, B, LDB, WORK, 1, ZERO, 
						//*    $                     BX( J, 1 ), LDBX ) 
						//* 
						
						_b5p6od9s = (_umlkckdg + (_3nayvi7h * (int)2));
						{
							System.Int32 __81fgg2dlsvn2065 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step2065 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2065;
							for (__81fgg2count2065 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn2065 + __81fgg2step2065) / __81fgg2step2065)), _vmeebitr = __81fgg2dlsvn2065; __81fgg2count2065 != 0; __81fgg2count2065--, _vmeebitr += (__81fgg2step2065)) {

							{
								
								{
									System.Int32 __81fgg2dlsvn2066 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step2066 = (System.Int32)((int)1);
									System.Int32 __81fgg2count2066;
									for (__81fgg2count2066 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn2066 + __81fgg2step2066) / __81fgg2step2066)), _hgi9ttgs = __81fgg2dlsvn2066; __81fgg2count2066 != 0; __81fgg2count2066--, _hgi9ttgs += (__81fgg2step2066)) {

									{
										
										_b5p6od9s = (_b5p6od9s + (int)1);
										*(_dqanbbw3+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.DBLE(*(_p9n405a5+(_hgi9ttgs - 1) + (_vmeebitr - 1) * 1 * (_ly9opahg)) );
Mark130:;
										// continue
									}
																		}								}
Mark140:;
								// continue
							}
														}						}
						_t5wmtd1j("T" ,ref _umlkckdg ,ref _3nayvi7h ,ref Unsafe.AsRef(_kxg5drh2) ,(_dqanbbw3+(((int)1 + _umlkckdg) + (_3nayvi7h * (int)2) - 1)),ref _umlkckdg ,(_dqanbbw3+((int)1 - 1)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+((int)1 + _umlkckdg - 1)),ref Unsafe.AsRef((int)1) );
						_b5p6od9s = (_umlkckdg + (_3nayvi7h * (int)2));
						{
							System.Int32 __81fgg2dlsvn2067 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step2067 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2067;
							for (__81fgg2count2067 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn2067 + __81fgg2step2067) / __81fgg2step2067)), _vmeebitr = __81fgg2dlsvn2067; __81fgg2count2067 != 0; __81fgg2count2067--, _vmeebitr += (__81fgg2step2067)) {

							{
								
								{
									System.Int32 __81fgg2dlsvn2068 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step2068 = (System.Int32)((int)1);
									System.Int32 __81fgg2count2068;
									for (__81fgg2count2068 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn2068 + __81fgg2step2068) / __81fgg2step2068)), _hgi9ttgs = __81fgg2dlsvn2068; __81fgg2count2068 != 0; __81fgg2count2068--, _hgi9ttgs += (__81fgg2step2068)) {

									{
										
										_b5p6od9s = (_b5p6od9s + (int)1);
										*(_dqanbbw3+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.DIMAG(*(_p9n405a5+(_hgi9ttgs - 1) + (_vmeebitr - 1) * 1 * (_ly9opahg)) );
Mark150:;
										// continue
									}
																		}								}
Mark160:;
								// continue
							}
														}						}
						_t5wmtd1j("T" ,ref _umlkckdg ,ref _3nayvi7h ,ref Unsafe.AsRef(_kxg5drh2) ,(_dqanbbw3+(((int)1 + _umlkckdg) + (_3nayvi7h * (int)2) - 1)),ref _umlkckdg ,(_dqanbbw3+((int)1 - 1)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+(((int)1 + _umlkckdg) + _3nayvi7h - 1)),ref Unsafe.AsRef((int)1) );
						{
							System.Int32 __81fgg2dlsvn2069 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step2069 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2069;
							for (__81fgg2count2069 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn2069 + __81fgg2step2069) / __81fgg2step2069)), _vmeebitr = __81fgg2dlsvn2069; __81fgg2count2069 != 0; __81fgg2count2069--, _vmeebitr += (__81fgg2step2069)) {

							{
								
								*(_uqckf55l+(_znpjgsef - 1) + (_vmeebitr - 1) * 1 * (_hde8nv3t)) = ILNumerics.F2NET.Intrinsics.DCMPLX(*(_dqanbbw3+(_vmeebitr + _umlkckdg - 1)) ,*(_dqanbbw3+((_vmeebitr + _umlkckdg) + _3nayvi7h - 1)) );
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
				
				_ly902k7t(ref _3nayvi7h ,(_p9n405a5+(_ev4xhht5 - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_uqckf55l+(_ev4xhht5 - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t );
				_yl1tlikm(ref _3nayvi7h ,(_uqckf55l+((int)1 - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t ,(_uqckf55l+(_ev4xhht5 - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t ,ref _3crf0qn3 ,ref _irk8i6qr );
			}
			
			if (_umlkckdg < ILNumerics.F2NET.Intrinsics.MAX(_ev4xhht5 ,_dxpq0xkr ))
			_nihu9ses("A" ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref _3nayvi7h ,(_p9n405a5+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_uqckf55l+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t );//* 
			//*        Step (3R): permute rows of B. 
			//* 
			
			_ly902k7t(ref _3nayvi7h ,(_uqckf55l+((int)1 - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t ,(_p9n405a5+(_qwh8ts9f - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg );
			if (_9qyq7j3e == (int)1)
			{
				
				_ly902k7t(ref _3nayvi7h ,(_uqckf55l+(_ev4xhht5 - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t ,(_p9n405a5+(_ev4xhht5 - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg );
			}
			
			{
				System.Int32 __81fgg2dlsvn2070 = (System.Int32)((int)2);
				const System.Int32 __81fgg2step2070 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2070;
				for (__81fgg2count2070 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2070 + __81fgg2step2070) / __81fgg2step2070)), _b5p6od9s = __81fgg2dlsvn2070; __81fgg2count2070 != 0; __81fgg2count2070--, _b5p6od9s += (__81fgg2step2070)) {

				{
					
					_ly902k7t(ref _3nayvi7h ,(_uqckf55l+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t ,(_p9n405a5+(*(_umao48xu+(_b5p6od9s - 1)) - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg );
Mark190:;
					// continue
				}
								}			}//* 
			//*        Step (4R): apply back the Givens rotations performed. 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn2071 = (System.Int32)(_8vecpt74);
				System.Int32 __81fgg2step2071 = (System.Int32)((int)-1);
				System.Int32 __81fgg2count2071;
				for (__81fgg2count2071 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn2071 + __81fgg2step2071) / __81fgg2step2071)), _b5p6od9s = __81fgg2dlsvn2071; __81fgg2count2071 != 0; __81fgg2count2071--, _b5p6od9s += (__81fgg2step2071)) {

				{
					
					_yl1tlikm(ref _3nayvi7h ,(_p9n405a5+(*(_0zwn6fsy+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_uhi0ls8i)) - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_p9n405a5+(*(_0zwn6fsy+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_uhi0ls8i)) - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(*(_gh266ol1+(_b5p6od9s - 1) + ((int)2 - 1) * 1 * (_jlfchtn9))) ,ref Unsafe.AsRef(-(*(_gh266ol1+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_jlfchtn9)))) );
Mark200:;
					// continue
				}
								}			}
		}
		//* 
		
		return;//* 
		//*     End of ZLALS0 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
