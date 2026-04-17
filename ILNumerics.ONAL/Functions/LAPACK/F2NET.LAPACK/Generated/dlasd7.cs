
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
//*> \brief \b DLASD7 merges the two sets of singular values together into a single sorted set. Then it tries to deflate the size of the problem. Used by sbdsdc. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLASD7 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlasd7.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlasd7.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlasd7.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLASD7( ICOMPQ, NL, NR, SQRE, K, D, Z, ZW, VF, VFW, VL, 
//*                          VLW, ALPHA, BETA, DSIGMA, IDX, IDXP, IDXQ, 
//*                          PERM, GIVPTR, GIVCOL, LDGCOL, GIVNUM, LDGNUM, 
//*                          C, S, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            GIVPTR, ICOMPQ, INFO, K, LDGCOL, LDGNUM, NL, 
//*      $                   NR, SQRE 
//*       DOUBLE PRECISION   ALPHA, BETA, C, S 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            GIVCOL( LDGCOL, * ), IDX( * ), IDXP( * ), 
//*      $                   IDXQ( * ), PERM( * ) 
//*       DOUBLE PRECISION   D( * ), DSIGMA( * ), GIVNUM( LDGNUM, * ), 
//*      $                   VF( * ), VFW( * ), VL( * ), VLW( * ), Z( * ), 
//*      $                   ZW( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLASD7 merges the two sets of singular values together into a single 
//*> sorted set. Then it tries to deflate the size of the problem. There 
//*> are two ways in which deflation can occur:  when two or more singular 
//*> values are close together or if there is a tiny entry in the Z 
//*> vector. For each such occurrence the order of the related 
//*> secular equation problem is reduced by one. 
//*> 
//*> DLASD7 is called from DLASD6. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] ICOMPQ 
//*> \verbatim 
//*>          ICOMPQ is INTEGER 
//*>          Specifies whether singular vectors are to be computed 
//*>          in compact form, as follows: 
//*>          = 0: Compute singular values only. 
//*>          = 1: Compute singular vectors of upper 
//*>               bidiagonal matrix in compact form. 
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
//*>         The bidiagonal matrix has 
//*>         N = NL + NR + 1 rows and 
//*>         M = N + SQRE >= N columns. 
//*> \endverbatim 
//*> 
//*> \param[out] K 
//*> \verbatim 
//*>          K is INTEGER 
//*>         Contains the dimension of the non-deflated matrix, this is 
//*>         the order of the related secular equation. 1 <= K <=N. 
//*> \endverbatim 
//*> 
//*> \param[in,out] D 
//*> \verbatim 
//*>          D is DOUBLE PRECISION array, dimension ( N ) 
//*>         On entry D contains the singular values of the two submatrices 
//*>         to be combined. On exit D contains the trailing (N-K) updated 
//*>         singular values (those which were deflated) sorted into 
//*>         increasing order. 
//*> \endverbatim 
//*> 
//*> \param[out] Z 
//*> \verbatim 
//*>          Z is DOUBLE PRECISION array, dimension ( M ) 
//*>         On exit Z contains the updating row vector in the secular 
//*>         equation. 
//*> \endverbatim 
//*> 
//*> \param[out] ZW 
//*> \verbatim 
//*>          ZW is DOUBLE PRECISION array, dimension ( M ) 
//*>         Workspace for Z. 
//*> \endverbatim 
//*> 
//*> \param[in,out] VF 
//*> \verbatim 
//*>          VF is DOUBLE PRECISION array, dimension ( M ) 
//*>         On entry, VF(1:NL+1) contains the first components of all 
//*>         right singular vectors of the upper block; and VF(NL+2:M) 
//*>         contains the first components of all right singular vectors 
//*>         of the lower block. On exit, VF contains the first components 
//*>         of all right singular vectors of the bidiagonal matrix. 
//*> \endverbatim 
//*> 
//*> \param[out] VFW 
//*> \verbatim 
//*>          VFW is DOUBLE PRECISION array, dimension ( M ) 
//*>         Workspace for VF. 
//*> \endverbatim 
//*> 
//*> \param[in,out] VL 
//*> \verbatim 
//*>          VL is DOUBLE PRECISION array, dimension ( M ) 
//*>         On entry, VL(1:NL+1) contains the  last components of all 
//*>         right singular vectors of the upper block; and VL(NL+2:M) 
//*>         contains the last components of all right singular vectors 
//*>         of the lower block. On exit, VL contains the last components 
//*>         of all right singular vectors of the bidiagonal matrix. 
//*> \endverbatim 
//*> 
//*> \param[out] VLW 
//*> \verbatim 
//*>          VLW is DOUBLE PRECISION array, dimension ( M ) 
//*>         Workspace for VL. 
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
//*> \param[out] DSIGMA 
//*> \verbatim 
//*>          DSIGMA is DOUBLE PRECISION array, dimension ( N ) 
//*>         Contains a copy of the diagonal elements (K-1 singular values 
//*>         and one zero) in the secular equation. 
//*> \endverbatim 
//*> 
//*> \param[out] IDX 
//*> \verbatim 
//*>          IDX is INTEGER array, dimension ( N ) 
//*>         This will contain the permutation used to sort the contents of 
//*>         D into ascending order. 
//*> \endverbatim 
//*> 
//*> \param[out] IDXP 
//*> \verbatim 
//*>          IDXP is INTEGER array, dimension ( N ) 
//*>         This will contain the permutation used to place deflated 
//*>         values of D at the end of the array. On output IDXP(2:K) 
//*>         points to the nondeflated D-values and IDXP(K+1:N) 
//*>         points to the deflated singular values. 
//*> \endverbatim 
//*> 
//*> \param[in] IDXQ 
//*> \verbatim 
//*>          IDXQ is INTEGER array, dimension ( N ) 
//*>         This contains the permutation which separately sorts the two 
//*>         sub-problems in D into ascending order.  Note that entries in 
//*>         the first half of this permutation must first be moved one 
//*>         position backward; and entries in the second half 
//*>         must first have NL+1 added to their values. 
//*> \endverbatim 
//*> 
//*> \param[out] PERM 
//*> \verbatim 
//*>          PERM is INTEGER array, dimension ( N ) 
//*>         The permutations (from deflation and sorting) to be applied 
//*>         to each singular block. Not referenced if ICOMPQ = 0. 
//*> \endverbatim 
//*> 
//*> \param[out] GIVPTR 
//*> \verbatim 
//*>          GIVPTR is INTEGER 
//*>         The number of Givens rotations which took place in this 
//*>         subproblem. Not referenced if ICOMPQ = 0. 
//*> \endverbatim 
//*> 
//*> \param[out] GIVCOL 
//*> \verbatim 
//*>          GIVCOL is INTEGER array, dimension ( LDGCOL, 2 ) 
//*>         Each pair of numbers indicates a pair of columns to take place 
//*>         in a Givens rotation. Not referenced if ICOMPQ = 0. 
//*> \endverbatim 
//*> 
//*> \param[in] LDGCOL 
//*> \verbatim 
//*>          LDGCOL is INTEGER 
//*>         The leading dimension of GIVCOL, must be at least N. 
//*> \endverbatim 
//*> 
//*> \param[out] GIVNUM 
//*> \verbatim 
//*>          GIVNUM is DOUBLE PRECISION array, dimension ( LDGNUM, 2 ) 
//*>         Each number indicates the C or S value to be used in the 
//*>         corresponding Givens rotation. Not referenced if ICOMPQ = 0. 
//*> \endverbatim 
//*> 
//*> \param[in] LDGNUM 
//*> \verbatim 
//*>          LDGNUM is INTEGER 
//*>         The leading dimension of GIVNUM, must be at least N. 
//*> \endverbatim 
//*> 
//*> \param[out] C 
//*> \verbatim 
//*>          C is DOUBLE PRECISION 
//*>         C contains garbage if SQRE =0 and the C-value of a Givens 
//*>         rotation related to the right null space if SQRE = 1. 
//*> \endverbatim 
//*> 
//*> \param[out] S 
//*> \verbatim 
//*>          S is DOUBLE PRECISION 
//*>         S contains garbage if SQRE =0 and the S-value of a Givens 
//*>         rotation related to the right null space if SQRE = 1. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>         = 0:  successful exit. 
//*>         < 0:  if INFO = -i, the i-th argument had an illegal value. 
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

	 
	public static void _72ff6nlx(ref Int32 _y1be9goe, ref Int32 _zx57w4aj, ref Int32 _oqpc3yjg, ref Int32 _9qyq7j3e, ref Int32 _umlkckdg, Double* _plfm7z8g, Double* _7e60fcso, Double* _pcr839kq, Double* _w7xcjdw0, Double* _w7lqcgt7, Double* _ppzorcqs, Double* _2agq5txd, ref Double _r7cfteg3, ref Double _bafcbx97, Double* _1r8q3o4r, Int32* _diodrai4, Int32* _u0tzpo1z, Int32* _ca64lzpg, Int32* _umao48xu, ref Int32 _8vecpt74, Int32* _0zwn6fsy, ref Int32 _uhi0ls8i, Double* _gh266ol1, ref Int32 _jlfchtn9, ref Double _3crf0qn3, ref Double _irk8i6qr, ref Int32 _gro5yvfo)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Double _5m0mjfxm =  2d;
Double _2j4711hv =  8d;
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
Double _p1iqarg6 =  default;
Double _k69jo0rq =  default;
Double _0446f4de =  default;
Double _txq1gp7u =  default;
Double _rm9rpu83 =  default;
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
		//* 
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
		_dxpq0xkr = ((_zx57w4aj + _oqpc3yjg) + (int)1);
		_ev4xhht5 = (_dxpq0xkr + _9qyq7j3e);//* 
		
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
		if (_uhi0ls8i < _dxpq0xkr)
		{
			
			_gro5yvfo = (int)-22;
		}
		else
		if (_jlfchtn9 < _dxpq0xkr)
		{
			
			_gro5yvfo = (int)-24;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("DLASD7" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		
		_qwh8ts9f = (_zx57w4aj + (int)1);
		_3lfkfha8 = (_zx57w4aj + (int)2);
		if (_y1be9goe == (int)1)
		{
			
			_8vecpt74 = (int)0;
		}
		//* 
		//*     Generate the first part of the vector Z and move the singular 
		//*     values in the first part of D one position backward. 
		//* 
		
		_rm9rpu83 = (_r7cfteg3 * *(_ppzorcqs+(_qwh8ts9f - 1)));
		*(_ppzorcqs+(_qwh8ts9f - 1)) = _d0547bi2;
		_0446f4de = *(_w7xcjdw0+(_qwh8ts9f - 1));
		{
			System.Int32 __81fgg2dlsvn368 = (System.Int32)(_zx57w4aj);
			System.Int32 __81fgg2step368 = (System.Int32)((int)-1);
			System.Int32 __81fgg2count368;
			for (__81fgg2count368 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn368 + __81fgg2step368) / __81fgg2step368)), _b5p6od9s = __81fgg2dlsvn368; __81fgg2count368 != 0; __81fgg2count368--, _b5p6od9s += (__81fgg2step368)) {

			{
				
				*(_7e60fcso+(_b5p6od9s + (int)1 - 1)) = (_r7cfteg3 * *(_ppzorcqs+(_b5p6od9s - 1)));
				*(_ppzorcqs+(_b5p6od9s - 1)) = _d0547bi2;
				*(_w7xcjdw0+(_b5p6od9s + (int)1 - 1)) = *(_w7xcjdw0+(_b5p6od9s - 1));
				*(_plfm7z8g+(_b5p6od9s + (int)1 - 1)) = *(_plfm7z8g+(_b5p6od9s - 1));
				*(_ca64lzpg+(_b5p6od9s + (int)1 - 1)) = (*(_ca64lzpg+(_b5p6od9s - 1)) + (int)1);
Mark10:;
				// continue
			}
						}		}
		*(_w7xcjdw0+((int)1 - 1)) = _0446f4de;//* 
		//*     Generate the second part of the vector Z. 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn369 = (System.Int32)(_3lfkfha8);
			const System.Int32 __81fgg2step369 = (System.Int32)((int)1);
			System.Int32 __81fgg2count369;
			for (__81fgg2count369 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn369 + __81fgg2step369) / __81fgg2step369)), _b5p6od9s = __81fgg2dlsvn369; __81fgg2count369 != 0; __81fgg2count369--, _b5p6od9s += (__81fgg2step369)) {

			{
				
				*(_7e60fcso+(_b5p6od9s - 1)) = (_bafcbx97 * *(_w7xcjdw0+(_b5p6od9s - 1)));
				*(_w7xcjdw0+(_b5p6od9s - 1)) = _d0547bi2;
Mark20:;
				// continue
			}
						}		}//* 
		//*     Sort the singular values into increasing order 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn370 = (System.Int32)(_3lfkfha8);
			const System.Int32 __81fgg2step370 = (System.Int32)((int)1);
			System.Int32 __81fgg2count370;
			for (__81fgg2count370 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn370 + __81fgg2step370) / __81fgg2step370)), _b5p6od9s = __81fgg2dlsvn370; __81fgg2count370 != 0; __81fgg2count370--, _b5p6od9s += (__81fgg2step370)) {

			{
				
				*(_ca64lzpg+(_b5p6od9s - 1)) = (*(_ca64lzpg+(_b5p6od9s - 1)) + _qwh8ts9f);
Mark30:;
				// continue
			}
						}		}//* 
		//*     DSIGMA, IDXC, IDXC, and ZW are used as storage space. 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn371 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step371 = (System.Int32)((int)1);
			System.Int32 __81fgg2count371;
			for (__81fgg2count371 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn371 + __81fgg2step371) / __81fgg2step371)), _b5p6od9s = __81fgg2dlsvn371; __81fgg2count371 != 0; __81fgg2count371--, _b5p6od9s += (__81fgg2step371)) {

			{
				
				*(_1r8q3o4r+(_b5p6od9s - 1)) = *(_plfm7z8g+(*(_ca64lzpg+(_b5p6od9s - 1)) - 1));
				*(_pcr839kq+(_b5p6od9s - 1)) = *(_7e60fcso+(*(_ca64lzpg+(_b5p6od9s - 1)) - 1));
				*(_w7lqcgt7+(_b5p6od9s - 1)) = *(_w7xcjdw0+(*(_ca64lzpg+(_b5p6od9s - 1)) - 1));
				*(_2agq5txd+(_b5p6od9s - 1)) = *(_ppzorcqs+(*(_ca64lzpg+(_b5p6od9s - 1)) - 1));
Mark40:;
				// continue
			}
						}		}//* 
		
		_csi3ymnh(ref _zx57w4aj ,ref _oqpc3yjg ,(_1r8q3o4r+((int)2 - 1)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef((int)1) ,(_diodrai4+((int)2 - 1)));//* 
		
		{
			System.Int32 __81fgg2dlsvn372 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step372 = (System.Int32)((int)1);
			System.Int32 __81fgg2count372;
			for (__81fgg2count372 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn372 + __81fgg2step372) / __81fgg2step372)), _b5p6od9s = __81fgg2dlsvn372; __81fgg2count372 != 0; __81fgg2count372--, _b5p6od9s += (__81fgg2step372)) {

			{
				
				_s0xtt601 = ((int)1 + *(_diodrai4+(_b5p6od9s - 1)));
				*(_plfm7z8g+(_b5p6od9s - 1)) = *(_1r8q3o4r+(_s0xtt601 - 1));
				*(_7e60fcso+(_b5p6od9s - 1)) = *(_pcr839kq+(_s0xtt601 - 1));
				*(_w7xcjdw0+(_b5p6od9s - 1)) = *(_w7lqcgt7+(_s0xtt601 - 1));
				*(_ppzorcqs+(_b5p6od9s - 1)) = *(_2agq5txd+(_s0xtt601 - 1));
Mark50:;
				// continue
			}
						}		}//* 
		//*     Calculate the allowable deflation tolerance 
		//* 
		
		_p1iqarg6 = _f43eg0w0("Epsilon" );
		_txq1gp7u = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_r7cfteg3 ) ,ILNumerics.F2NET.Intrinsics.ABS(_bafcbx97 ) );
		_txq1gp7u = (((_2j4711hv * _2j4711hv) * _p1iqarg6) * ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_dxpq0xkr - 1)) ) ,_txq1gp7u ));//* 
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
			System.Int32 __81fgg2dlsvn373 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step373 = (System.Int32)((int)1);
			System.Int32 __81fgg2count373;
			for (__81fgg2count373 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn373 + __81fgg2step373) / __81fgg2step373)), _znpjgsef = __81fgg2dlsvn373; __81fgg2count373 != 0; __81fgg2count373--, _znpjgsef += (__81fgg2step373)) {

			{
				
				if (ILNumerics.F2NET.Intrinsics.ABS(*(_7e60fcso+(_znpjgsef - 1)) ) <= _txq1gp7u)
				{
					//* 
					//*           Deflate due to small z component. 
					//* 
					
					_bddf4r7h = (_bddf4r7h - (int)1);
					*(_u0tzpo1z+(_bddf4r7h - 1)) = _znpjgsef;
					if (_znpjgsef == _dxpq0xkr)goto Mark100;
				}
				else
				{
					
					_9v3wcik1 = _znpjgsef;goto Mark70;
				}
				
Mark60:;
				// continue
			}
						}		}
Mark70:;
		// continue
		_znpjgsef = _9v3wcik1;
Mark80:;
		// continue
		_znpjgsef = (_znpjgsef + (int)1);
		if (_znpjgsef > _dxpq0xkr)goto Mark90;
		if (ILNumerics.F2NET.Intrinsics.ABS(*(_7e60fcso+(_znpjgsef - 1)) ) <= _txq1gp7u)
		{
			//* 
			//*        Deflate due to small z component. 
			//* 
			
			_bddf4r7h = (_bddf4r7h - (int)1);
			*(_u0tzpo1z+(_bddf4r7h - 1)) = _znpjgsef;
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
				*(_7e60fcso+(_znpjgsef - 1)) = _0446f4de;
				*(_7e60fcso+(_9v3wcik1 - 1)) = _d0547bi2;
				_3crf0qn3 = (_3crf0qn3 / _0446f4de);
				_irk8i6qr = (-((_irk8i6qr / _0446f4de)));//* 
				//*           Record the appropriate Givens rotation 
				//* 
				
				if (_y1be9goe == (int)1)
				{
					
					_8vecpt74 = (_8vecpt74 + (int)1);
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
					
					*(_0zwn6fsy+(_8vecpt74 - 1) + ((int)2 - 1) * 1 * (_uhi0ls8i)) = _bcl5dcha;
					*(_0zwn6fsy+(_8vecpt74 - 1) + ((int)1 - 1) * 1 * (_uhi0ls8i)) = _z1w9cjgb;
					*(_gh266ol1+(_8vecpt74 - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)) = _3crf0qn3;
					*(_gh266ol1+(_8vecpt74 - 1) + ((int)1 - 1) * 1 * (_jlfchtn9)) = _irk8i6qr;
				}
				
				_2197fa5i(ref Unsafe.AsRef((int)1) ,(_w7xcjdw0+(_9v3wcik1 - 1)),ref Unsafe.AsRef((int)1) ,(_w7xcjdw0+(_znpjgsef - 1)),ref Unsafe.AsRef((int)1) ,ref _3crf0qn3 ,ref _irk8i6qr );
				_2197fa5i(ref Unsafe.AsRef((int)1) ,(_ppzorcqs+(_9v3wcik1 - 1)),ref Unsafe.AsRef((int)1) ,(_ppzorcqs+(_znpjgsef - 1)),ref Unsafe.AsRef((int)1) ,ref _3crf0qn3 ,ref _irk8i6qr );
				_bddf4r7h = (_bddf4r7h - (int)1);
				*(_u0tzpo1z+(_bddf4r7h - 1)) = _9v3wcik1;
				_9v3wcik1 = _znpjgsef;
			}
			else
			{
				
				_umlkckdg = (_umlkckdg + (int)1);
				*(_pcr839kq+(_umlkckdg - 1)) = *(_7e60fcso+(_9v3wcik1 - 1));
				*(_1r8q3o4r+(_umlkckdg - 1)) = *(_plfm7z8g+(_9v3wcik1 - 1));
				*(_u0tzpo1z+(_umlkckdg - 1)) = _9v3wcik1;
				_9v3wcik1 = _znpjgsef;
			}
			
		}
		goto Mark80;
Mark90:;
		// continue//* 
		//*     Record the last singular value. 
		//* 
		
		_umlkckdg = (_umlkckdg + (int)1);
		*(_pcr839kq+(_umlkckdg - 1)) = *(_7e60fcso+(_9v3wcik1 - 1));
		*(_1r8q3o4r+(_umlkckdg - 1)) = *(_plfm7z8g+(_9v3wcik1 - 1));
		*(_u0tzpo1z+(_umlkckdg - 1)) = _9v3wcik1;//* 
		
Mark100:;
		// continue//* 
		//*     Sort the singular values into DSIGMA. The singular values which 
		//*     were not deflated go into the first K slots of DSIGMA, except 
		//*     that DSIGMA(1) is treated separately. 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn374 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step374 = (System.Int32)((int)1);
			System.Int32 __81fgg2count374;
			for (__81fgg2count374 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn374 + __81fgg2step374) / __81fgg2step374)), _znpjgsef = __81fgg2dlsvn374; __81fgg2count374 != 0; __81fgg2count374--, _znpjgsef += (__81fgg2step374)) {

			{
				
				_c2zk3fjj = *(_u0tzpo1z+(_znpjgsef - 1));
				*(_1r8q3o4r+(_znpjgsef - 1)) = *(_plfm7z8g+(_c2zk3fjj - 1));
				*(_w7lqcgt7+(_znpjgsef - 1)) = *(_w7xcjdw0+(_c2zk3fjj - 1));
				*(_2agq5txd+(_znpjgsef - 1)) = *(_ppzorcqs+(_c2zk3fjj - 1));
Mark110:;
				// continue
			}
						}		}
		if (_y1be9goe == (int)1)
		{
			
			{
				System.Int32 __81fgg2dlsvn375 = (System.Int32)((int)2);
				const System.Int32 __81fgg2step375 = (System.Int32)((int)1);
				System.Int32 __81fgg2count375;
				for (__81fgg2count375 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn375 + __81fgg2step375) / __81fgg2step375)), _znpjgsef = __81fgg2dlsvn375; __81fgg2count375 != 0; __81fgg2count375--, _znpjgsef += (__81fgg2step375)) {

				{
					
					_c2zk3fjj = *(_u0tzpo1z+(_znpjgsef - 1));
					*(_umao48xu+(_znpjgsef - 1)) = *(_ca64lzpg+(*(_diodrai4+(_c2zk3fjj - 1)) + (int)1 - 1));
					if (*(_umao48xu+(_znpjgsef - 1)) <= _qwh8ts9f)
					{
						
						*(_umao48xu+(_znpjgsef - 1)) = (*(_umao48xu+(_znpjgsef - 1)) - (int)1);
					}
					
Mark120:;
					// continue
				}
								}			}
		}
		//* 
		//*     The deflated singular values go back into the last N - K slots of 
		//*     D. 
		//* 
		
		_gvjhlct0(ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,(_1r8q3o4r+(_umlkckdg + (int)1 - 1)),ref Unsafe.AsRef((int)1) ,(_plfm7z8g+(_umlkckdg + (int)1 - 1)),ref Unsafe.AsRef((int)1) );//* 
		//*     Determine DSIGMA(1), DSIGMA(2), Z(1), VF(1), VL(1), VF(M), and 
		//*     VL(M). 
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
				_irk8i6qr = (-((*(_7e60fcso+(_ev4xhht5 - 1)) / *(_7e60fcso+((int)1 - 1)))));
			}
			
			_2197fa5i(ref Unsafe.AsRef((int)1) ,(_w7xcjdw0+(_ev4xhht5 - 1)),ref Unsafe.AsRef((int)1) ,(_w7xcjdw0+((int)1 - 1)),ref Unsafe.AsRef((int)1) ,ref _3crf0qn3 ,ref _irk8i6qr );
			_2197fa5i(ref Unsafe.AsRef((int)1) ,(_ppzorcqs+(_ev4xhht5 - 1)),ref Unsafe.AsRef((int)1) ,(_ppzorcqs+((int)1 - 1)),ref Unsafe.AsRef((int)1) ,ref _3crf0qn3 ,ref _irk8i6qr );
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
		//*     Restore Z, VF, and VL. 
		//* 
		
		_gvjhlct0(ref Unsafe.AsRef(_umlkckdg - (int)1) ,(_pcr839kq+((int)2 - 1)),ref Unsafe.AsRef((int)1) ,(_7e60fcso+((int)2 - 1)),ref Unsafe.AsRef((int)1) );
		_gvjhlct0(ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,(_w7lqcgt7+((int)2 - 1)),ref Unsafe.AsRef((int)1) ,(_w7xcjdw0+((int)2 - 1)),ref Unsafe.AsRef((int)1) );
		_gvjhlct0(ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,(_2agq5txd+((int)2 - 1)),ref Unsafe.AsRef((int)1) ,(_ppzorcqs+((int)2 - 1)),ref Unsafe.AsRef((int)1) );//* 
		
		return;//* 
		//*     End of DLASD7 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
