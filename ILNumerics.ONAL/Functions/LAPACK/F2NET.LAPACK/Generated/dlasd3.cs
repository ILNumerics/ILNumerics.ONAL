
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
//*> \brief \b DLASD3 finds all square roots of the roots of the secular equation, as defined by the values in D and Z, and then updates the singular vectors by matrix multiplication. Used by sbdsdc. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLASD3 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlasd3.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlasd3.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlasd3.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLASD3( NL, NR, SQRE, K, D, Q, LDQ, DSIGMA, U, LDU, U2, 
//*                          LDU2, VT, LDVT, VT2, LDVT2, IDXC, CTOT, Z, 
//*                          INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INFO, K, LDQ, LDU, LDU2, LDVT, LDVT2, NL, NR, 
//*      $                   SQRE 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            CTOT( * ), IDXC( * ) 
//*       DOUBLE PRECISION   D( * ), DSIGMA( * ), Q( LDQ, * ), U( LDU, * ), 
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
//*> DLASD3 finds all the square roots of the roots of the secular 
//*> equation, as defined by the values in D and Z.  It makes the 
//*> appropriate calls to DLASD4 and then updates the singular 
//*> vectors by matrix multiplication. 
//*> 
//*> This code makes very mild assumptions about floating point 
//*> arithmetic. It will work on machines with a guard digit in 
//*> add/subtract, or on those binary machines without guard digits 
//*> which subtract like the Cray XMP, Cray YMP, Cray C 90, or Cray 2. 
//*> It could conceivably fail on hexadecimal or decimal machines 
//*> without guard digits, but we know of none. 
//*> 
//*> DLASD3 is called from DLASD1. 
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
//*> \param[in] K 
//*> \verbatim 
//*>          K is INTEGER 
//*>         The size of the secular equation, 1 =< K = < N. 
//*> \endverbatim 
//*> 
//*> \param[out] D 
//*> \verbatim 
//*>          D is DOUBLE PRECISION array, dimension(K) 
//*>         On exit the square roots of the roots of the secular equation, 
//*>         in ascending order. 
//*> \endverbatim 
//*> 
//*> \param[out] Q 
//*> \verbatim 
//*>          Q is DOUBLE PRECISION array, dimension (LDQ,K) 
//*> \endverbatim 
//*> 
//*> \param[in] LDQ 
//*> \verbatim 
//*>          LDQ is INTEGER 
//*>         The leading dimension of the array Q.  LDQ >= K. 
//*> \endverbatim 
//*> 
//*> \param[in,out] DSIGMA 
//*> \verbatim 
//*>          DSIGMA is DOUBLE PRECISION array, dimension(K) 
//*>         The first K elements of this array contain the old roots 
//*>         of the deflated updating problem.  These are the poles 
//*>         of the secular equation. 
//*> \endverbatim 
//*> 
//*> \param[out] U 
//*> \verbatim 
//*>          U is DOUBLE PRECISION array, dimension (LDU, N) 
//*>         The last N - K columns of this matrix contain the deflated 
//*>         left singular vectors. 
//*> \endverbatim 
//*> 
//*> \param[in] LDU 
//*> \verbatim 
//*>          LDU is INTEGER 
//*>         The leading dimension of the array U.  LDU >= N. 
//*> \endverbatim 
//*> 
//*> \param[in] U2 
//*> \verbatim 
//*>          U2 is DOUBLE PRECISION array, dimension (LDU2, N) 
//*>         The first K columns of this matrix contain the non-deflated 
//*>         left singular vectors for the split problem. 
//*> \endverbatim 
//*> 
//*> \param[in] LDU2 
//*> \verbatim 
//*>          LDU2 is INTEGER 
//*>         The leading dimension of the array U2.  LDU2 >= N. 
//*> \endverbatim 
//*> 
//*> \param[out] VT 
//*> \verbatim 
//*>          VT is DOUBLE PRECISION array, dimension (LDVT, M) 
//*>         The last M - K columns of VT**T contain the deflated 
//*>         right singular vectors. 
//*> \endverbatim 
//*> 
//*> \param[in] LDVT 
//*> \verbatim 
//*>          LDVT is INTEGER 
//*>         The leading dimension of the array VT.  LDVT >= N. 
//*> \endverbatim 
//*> 
//*> \param[in,out] VT2 
//*> \verbatim 
//*>          VT2 is DOUBLE PRECISION array, dimension (LDVT2, N) 
//*>         The first K columns of VT2**T contain the non-deflated 
//*>         right singular vectors for the split problem. 
//*> \endverbatim 
//*> 
//*> \param[in] LDVT2 
//*> \verbatim 
//*>          LDVT2 is INTEGER 
//*>         The leading dimension of the array VT2.  LDVT2 >= N. 
//*> \endverbatim 
//*> 
//*> \param[in] IDXC 
//*> \verbatim 
//*>          IDXC is INTEGER array, dimension ( N ) 
//*>         The permutation used to arrange the columns of U (and rows of 
//*>         VT) into three groups:  the first group contains non-zero 
//*>         entries only at and above (or before) NL +1; the second 
//*>         contains non-zero entries only at and below (or after) NL+2; 
//*>         and the third is dense. The first column of U and the row of 
//*>         VT are treated separately, however. 
//*> 
//*>         The rows of the singular vectors found by DLASD4 
//*>         must be likewise permuted before the matrix multiplies can 
//*>         take place. 
//*> \endverbatim 
//*> 
//*> \param[in] CTOT 
//*> \verbatim 
//*>          CTOT is INTEGER array, dimension ( 4 ) 
//*>         A count of the total number of the various types of columns 
//*>         in U (or rows in VT), as described in IDXC. The fourth column 
//*>         type is any column which has been deflated. 
//*> \endverbatim 
//*> 
//*> \param[in,out] Z 
//*> \verbatim 
//*>          Z is DOUBLE PRECISION array, dimension (K) 
//*>         The first K elements of this array contain the components 
//*>         of the deflation-adjusted updating row vector. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>         = 0:  successful exit. 
//*>         < 0:  if INFO = -i, the i-th argument had an illegal value. 
//*>         > 0:  if INFO = 1, a singular value did not converge 
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

	 
	public static void _s3dkv1uz(ref Int32 _zx57w4aj, ref Int32 _oqpc3yjg, ref Int32 _9qyq7j3e, ref Int32 _umlkckdg, Double* _plfm7z8g, Double* _atumjwo3, ref Int32 _u3fpniqy, Double* _1r8q3o4r, Double* _7u55mqkq, ref Int32 _u6e6d39b, Double* _s6mwvivs, ref Int32 _qz188u0m, Double* _xdbczr8u, ref Int32 _h4ibbatv, Double* _1469pg8i, ref Int32 _sh6ez9uf, Int32* _dzf4x6zd, Int32* _9wgmm024, Double* _7e60fcso, ref Int32 _gro5yvfo)
	{
#region variable declarations
Double _kxg5drh2 =  1d;
Double _d0547bi2 =  0d;
Double _2v8pp9oq =  -1d;
Int32 _r0ocrtbh =  default;
Int32 _b5p6od9s =  default;
Int32 _znpjgsef =  default;
Int32 _aynldcwj =  default;
Int32 _ija9scun =  default;
Int32 _ev4xhht5 =  default;
Int32 _dxpq0xkr =  default;
Int32 _qwh8ts9f =  default;
Int32 _3lfkfha8 =  default;
Int32 _bds60snh =  default;
Double _4qwfue8o =  default;
Double _1ajfmh55 =  default;
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
		_ev4xhht5 = (_dxpq0xkr + _9qyq7j3e);
		_qwh8ts9f = (_zx57w4aj + (int)1);
		_3lfkfha8 = (_zx57w4aj + (int)2);//* 
		
		if ((_umlkckdg < (int)1) | (_umlkckdg > _dxpq0xkr))
		{
			
			_gro5yvfo = (int)-4;
		}
		else
		if (_u3fpniqy < _umlkckdg)
		{
			
			_gro5yvfo = (int)-7;
		}
		else
		if (_u6e6d39b < _dxpq0xkr)
		{
			
			_gro5yvfo = (int)-10;
		}
		else
		if (_qz188u0m < _dxpq0xkr)
		{
			
			_gro5yvfo = (int)-12;
		}
		else
		if (_h4ibbatv < _ev4xhht5)
		{
			
			_gro5yvfo = (int)-14;
		}
		else
		if (_sh6ez9uf < _ev4xhht5)
		{
			
			_gro5yvfo = (int)-16;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("DLASD3" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if (_umlkckdg == (int)1)
		{
			
			*(_plfm7z8g+((int)1 - 1)) = ILNumerics.F2NET.Intrinsics.ABS(*(_7e60fcso+((int)1 - 1)) );
			_gvjhlct0(ref _ev4xhht5 ,(_1469pg8i+((int)1 - 1) + ((int)1 - 1) * 1 * (_sh6ez9uf)),ref _sh6ez9uf ,(_xdbczr8u+((int)1 - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv );
			if (*(_7e60fcso+((int)1 - 1)) > _d0547bi2)
			{
				
				_gvjhlct0(ref _dxpq0xkr ,(_s6mwvivs+((int)1 - 1) + ((int)1 - 1) * 1 * (_qz188u0m)),ref Unsafe.AsRef((int)1) ,(_7u55mqkq+((int)1 - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)),ref Unsafe.AsRef((int)1) );
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn236 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step236 = (System.Int32)((int)1);
					System.Int32 __81fgg2count236;
					for (__81fgg2count236 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn236 + __81fgg2step236) / __81fgg2step236)), _b5p6od9s = __81fgg2dlsvn236; __81fgg2count236 != 0; __81fgg2count236--, _b5p6od9s += (__81fgg2step236)) {

					{
						
						*(_7u55mqkq+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)) = (-(*(_s6mwvivs+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_qz188u0m))));
Mark10:;
						// continue
					}
										}				}
			}
			
			return;
		}
		//* 
		//*     Modify values DSIGMA(i) to make sure all DSIGMA(i)-DSIGMA(j) can 
		//*     be computed with high relative accuracy (barring over/underflow). 
		//*     This is a problem on machines without a guard digit in 
		//*     add/subtract (Cray XMP, Cray YMP, Cray C 90 and Cray 2). 
		//*     The following code replaces DSIGMA(I) by 2*DSIGMA(I)-DSIGMA(I), 
		//*     which on any of these machines zeros out the bottommost 
		//*     bit of DSIGMA(I) if it is 1; this makes the subsequent 
		//*     subtractions DSIGMA(I)-DSIGMA(J) unproblematic when cancellation 
		//*     occurs. On binary machines with a guard digit (almost all 
		//*     machines) it does not change DSIGMA(I) at all. On hexadecimal 
		//*     and decimal machines with a guard digit, it slightly 
		//*     changes the bottommost bits of DSIGMA(I). It does not account 
		//*     for hexadecimal or decimal machines without guard digits 
		//*     (we know of none). We use a subroutine call to compute 
		//*     2*DSIGMA(I) to prevent optimizing compilers from eliminating 
		//*     this code. 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn237 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step237 = (System.Int32)((int)1);
			System.Int32 __81fgg2count237;
			for (__81fgg2count237 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn237 + __81fgg2step237) / __81fgg2step237)), _b5p6od9s = __81fgg2dlsvn237; __81fgg2count237 != 0; __81fgg2count237--, _b5p6od9s += (__81fgg2step237)) {

			{
				
				*(_1r8q3o4r+(_b5p6od9s - 1)) = (_mfhxzi0a(ref Unsafe.AsRef(*(_1r8q3o4r+(_b5p6od9s - 1))) ,ref Unsafe.AsRef(*(_1r8q3o4r+(_b5p6od9s - 1))) ) - *(_1r8q3o4r+(_b5p6od9s - 1)));
Mark20:;
				// continue
			}
						}		}//* 
		//*     Keep a copy of Z. 
		//* 
		
		_gvjhlct0(ref _umlkckdg ,_7e60fcso ,ref Unsafe.AsRef((int)1) ,_atumjwo3 ,ref Unsafe.AsRef((int)1) );//* 
		//*     Normalize Z. 
		//* 
		
		_4qwfue8o = _gmlreqin(ref _umlkckdg ,_7e60fcso ,ref Unsafe.AsRef((int)1) );
		_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _4qwfue8o ,ref Unsafe.AsRef(_kxg5drh2) ,ref _umlkckdg ,ref Unsafe.AsRef((int)1) ,_7e60fcso ,ref _umlkckdg ,ref _gro5yvfo );
		_4qwfue8o = (_4qwfue8o * _4qwfue8o);//* 
		//*     Find the new singular values. 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn238 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step238 = (System.Int32)((int)1);
			System.Int32 __81fgg2count238;
			for (__81fgg2count238 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn238 + __81fgg2step238) / __81fgg2step238)), _znpjgsef = __81fgg2dlsvn238; __81fgg2count238 != 0; __81fgg2count238--, _znpjgsef += (__81fgg2step238)) {

			{
				
				_ek29mhsz(ref _umlkckdg ,ref _znpjgsef ,_1r8q3o4r ,_7e60fcso ,(_7u55mqkq+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_u6e6d39b)),ref _4qwfue8o ,ref Unsafe.AsRef(*(_plfm7z8g+(_znpjgsef - 1))) ,(_xdbczr8u+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_h4ibbatv)),ref _gro5yvfo );//* 
				//*        If the zero finder fails, report the convergence failure. 
				//* 
				
				if (_gro5yvfo != (int)0)
				{
					
					return;
				}
				
Mark30:;
				// continue
			}
						}		}//* 
		//*     Compute updated Z. 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn239 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step239 = (System.Int32)((int)1);
			System.Int32 __81fgg2count239;
			for (__81fgg2count239 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn239 + __81fgg2step239) / __81fgg2step239)), _b5p6od9s = __81fgg2dlsvn239; __81fgg2count239 != 0; __81fgg2count239--, _b5p6od9s += (__81fgg2step239)) {

			{
				
				*(_7e60fcso+(_b5p6od9s - 1)) = (*(_7u55mqkq+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_u6e6d39b)) * *(_xdbczr8u+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_h4ibbatv)));
				{
					System.Int32 __81fgg2dlsvn240 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step240 = (System.Int32)((int)1);
					System.Int32 __81fgg2count240;
					for (__81fgg2count240 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s - (int)1) - __81fgg2dlsvn240 + __81fgg2step240) / __81fgg2step240)), _znpjgsef = __81fgg2dlsvn240; __81fgg2count240 != 0; __81fgg2count240--, _znpjgsef += (__81fgg2step240)) {

					{
						
						*(_7e60fcso+(_b5p6od9s - 1)) = (*(_7e60fcso+(_b5p6od9s - 1)) * (((*(_7u55mqkq+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_u6e6d39b)) * *(_xdbczr8u+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_h4ibbatv))) / (*(_1r8q3o4r+(_b5p6od9s - 1)) - *(_1r8q3o4r+(_znpjgsef - 1)))) / (*(_1r8q3o4r+(_b5p6od9s - 1)) + *(_1r8q3o4r+(_znpjgsef - 1)))));
Mark40:;
						// continue
					}
										}				}
				{
					System.Int32 __81fgg2dlsvn241 = (System.Int32)(_b5p6od9s);
					const System.Int32 __81fgg2step241 = (System.Int32)((int)1);
					System.Int32 __81fgg2count241;
					for (__81fgg2count241 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg - (int)1) - __81fgg2dlsvn241 + __81fgg2step241) / __81fgg2step241)), _znpjgsef = __81fgg2dlsvn241; __81fgg2count241 != 0; __81fgg2count241--, _znpjgsef += (__81fgg2step241)) {

					{
						
						*(_7e60fcso+(_b5p6od9s - 1)) = (*(_7e60fcso+(_b5p6od9s - 1)) * (((*(_7u55mqkq+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_u6e6d39b)) * *(_xdbczr8u+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_h4ibbatv))) / (*(_1r8q3o4r+(_b5p6od9s - 1)) - *(_1r8q3o4r+(_znpjgsef + (int)1 - 1)))) / (*(_1r8q3o4r+(_b5p6od9s - 1)) + *(_1r8q3o4r+(_znpjgsef + (int)1 - 1)))));
Mark50:;
						// continue
					}
										}				}
				*(_7e60fcso+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.SIGN(ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS(*(_7e60fcso+(_b5p6od9s - 1)) ) ) ,*(_atumjwo3+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_u3fpniqy)) );
Mark60:;
				// continue
			}
						}		}//* 
		//*     Compute left singular vectors of the modified diagonal matrix, 
		//*     and store related information for the right singular vectors. 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn242 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step242 = (System.Int32)((int)1);
			System.Int32 __81fgg2count242;
			for (__81fgg2count242 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn242 + __81fgg2step242) / __81fgg2step242)), _b5p6od9s = __81fgg2dlsvn242; __81fgg2count242 != 0; __81fgg2count242--, _b5p6od9s += (__81fgg2step242)) {

			{
				
				*(_xdbczr8u+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_h4ibbatv)) = ((*(_7e60fcso+((int)1 - 1)) / *(_7u55mqkq+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_u6e6d39b))) / *(_xdbczr8u+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_h4ibbatv)));
				*(_7u55mqkq+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_u6e6d39b)) = _2v8pp9oq;
				{
					System.Int32 __81fgg2dlsvn243 = (System.Int32)((int)2);
					const System.Int32 __81fgg2step243 = (System.Int32)((int)1);
					System.Int32 __81fgg2count243;
					for (__81fgg2count243 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn243 + __81fgg2step243) / __81fgg2step243)), _znpjgsef = __81fgg2dlsvn243; __81fgg2count243 != 0; __81fgg2count243--, _znpjgsef += (__81fgg2step243)) {

					{
						
						*(_xdbczr8u+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_h4ibbatv)) = ((*(_7e60fcso+(_znpjgsef - 1)) / *(_7u55mqkq+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_u6e6d39b))) / *(_xdbczr8u+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_h4ibbatv)));
						*(_7u55mqkq+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_u6e6d39b)) = (*(_1r8q3o4r+(_znpjgsef - 1)) * *(_xdbczr8u+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_h4ibbatv)));
Mark70:;
						// continue
					}
										}				}
				_1ajfmh55 = _gmlreqin(ref _umlkckdg ,(_7u55mqkq+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_u6e6d39b)),ref Unsafe.AsRef((int)1) );
				*(_atumjwo3+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_u3fpniqy)) = (*(_7u55mqkq+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_u6e6d39b)) / _1ajfmh55);
				{
					System.Int32 __81fgg2dlsvn244 = (System.Int32)((int)2);
					const System.Int32 __81fgg2step244 = (System.Int32)((int)1);
					System.Int32 __81fgg2count244;
					for (__81fgg2count244 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn244 + __81fgg2step244) / __81fgg2step244)), _znpjgsef = __81fgg2dlsvn244; __81fgg2count244 != 0; __81fgg2count244--, _znpjgsef += (__81fgg2step244)) {

					{
						
						_aynldcwj = *(_dzf4x6zd+(_znpjgsef - 1));
						*(_atumjwo3+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_u3fpniqy)) = (*(_7u55mqkq+(_aynldcwj - 1) + (_b5p6od9s - 1) * 1 * (_u6e6d39b)) / _1ajfmh55);
Mark80:;
						// continue
					}
										}				}
Mark90:;
				// continue
			}
						}		}//* 
		//*     Update the left singular vector matrix. 
		//* 
		
		if (_umlkckdg == (int)2)
		{
			
			_5nsxi69c("N" ,"N" ,ref _dxpq0xkr ,ref _umlkckdg ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_s6mwvivs ,ref _qz188u0m ,_atumjwo3 ,ref _u3fpniqy ,ref Unsafe.AsRef(_d0547bi2) ,_7u55mqkq ,ref _u6e6d39b );goto Mark100;
		}
		
		if (*(_9wgmm024+((int)1 - 1)) > (int)0)
		{
			
			_5nsxi69c("N" ,"N" ,ref _zx57w4aj ,ref _umlkckdg ,ref Unsafe.AsRef(*(_9wgmm024+((int)1 - 1))) ,ref Unsafe.AsRef(_kxg5drh2) ,(_s6mwvivs+((int)1 - 1) + ((int)2 - 1) * 1 * (_qz188u0m)),ref _qz188u0m ,(_atumjwo3+((int)2 - 1) + ((int)1 - 1) * 1 * (_u3fpniqy)),ref _u3fpniqy ,ref Unsafe.AsRef(_d0547bi2) ,(_7u55mqkq+((int)1 - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b );
			if (*(_9wgmm024+((int)3 - 1)) > (int)0)
			{
				
				_ija9scun = (((int)2 + *(_9wgmm024+((int)1 - 1))) + *(_9wgmm024+((int)2 - 1)));
				_5nsxi69c("N" ,"N" ,ref _zx57w4aj ,ref _umlkckdg ,ref Unsafe.AsRef(*(_9wgmm024+((int)3 - 1))) ,ref Unsafe.AsRef(_kxg5drh2) ,(_s6mwvivs+((int)1 - 1) + (_ija9scun - 1) * 1 * (_qz188u0m)),ref _qz188u0m ,(_atumjwo3+(_ija9scun - 1) + ((int)1 - 1) * 1 * (_u3fpniqy)),ref _u3fpniqy ,ref Unsafe.AsRef(_kxg5drh2) ,(_7u55mqkq+((int)1 - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b );
			}
			
		}
		else
		if (*(_9wgmm024+((int)3 - 1)) > (int)0)
		{
			
			_ija9scun = (((int)2 + *(_9wgmm024+((int)1 - 1))) + *(_9wgmm024+((int)2 - 1)));
			_5nsxi69c("N" ,"N" ,ref _zx57w4aj ,ref _umlkckdg ,ref Unsafe.AsRef(*(_9wgmm024+((int)3 - 1))) ,ref Unsafe.AsRef(_kxg5drh2) ,(_s6mwvivs+((int)1 - 1) + (_ija9scun - 1) * 1 * (_qz188u0m)),ref _qz188u0m ,(_atumjwo3+(_ija9scun - 1) + ((int)1 - 1) * 1 * (_u3fpniqy)),ref _u3fpniqy ,ref Unsafe.AsRef(_d0547bi2) ,(_7u55mqkq+((int)1 - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b );
		}
		else
		{
			
			_hhtvj1kb("F" ,ref _zx57w4aj ,ref _umlkckdg ,_s6mwvivs ,ref _qz188u0m ,_7u55mqkq ,ref _u6e6d39b );
		}
		
		_gvjhlct0(ref _umlkckdg ,(_atumjwo3+((int)1 - 1) + ((int)1 - 1) * 1 * (_u3fpniqy)),ref _u3fpniqy ,(_7u55mqkq+(_qwh8ts9f - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b );
		_ija9scun = ((int)2 + *(_9wgmm024+((int)1 - 1)));
		_r0ocrtbh = (*(_9wgmm024+((int)2 - 1)) + *(_9wgmm024+((int)3 - 1)));
		_5nsxi69c("N" ,"N" ,ref _oqpc3yjg ,ref _umlkckdg ,ref _r0ocrtbh ,ref Unsafe.AsRef(_kxg5drh2) ,(_s6mwvivs+(_3lfkfha8 - 1) + (_ija9scun - 1) * 1 * (_qz188u0m)),ref _qz188u0m ,(_atumjwo3+(_ija9scun - 1) + ((int)1 - 1) * 1 * (_u3fpniqy)),ref _u3fpniqy ,ref Unsafe.AsRef(_d0547bi2) ,(_7u55mqkq+(_3lfkfha8 - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b );//* 
		//*     Generate the right singular vectors. 
		//* 
		
Mark100:;
		// continue
		{
			System.Int32 __81fgg2dlsvn245 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step245 = (System.Int32)((int)1);
			System.Int32 __81fgg2count245;
			for (__81fgg2count245 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn245 + __81fgg2step245) / __81fgg2step245)), _b5p6od9s = __81fgg2dlsvn245; __81fgg2count245 != 0; __81fgg2count245--, _b5p6od9s += (__81fgg2step245)) {

			{
				
				_1ajfmh55 = _gmlreqin(ref _umlkckdg ,(_xdbczr8u+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_h4ibbatv)),ref Unsafe.AsRef((int)1) );
				*(_atumjwo3+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_u3fpniqy)) = (*(_xdbczr8u+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_h4ibbatv)) / _1ajfmh55);
				{
					System.Int32 __81fgg2dlsvn246 = (System.Int32)((int)2);
					const System.Int32 __81fgg2step246 = (System.Int32)((int)1);
					System.Int32 __81fgg2count246;
					for (__81fgg2count246 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn246 + __81fgg2step246) / __81fgg2step246)), _znpjgsef = __81fgg2dlsvn246; __81fgg2count246 != 0; __81fgg2count246--, _znpjgsef += (__81fgg2step246)) {

					{
						
						_aynldcwj = *(_dzf4x6zd+(_znpjgsef - 1));
						*(_atumjwo3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_u3fpniqy)) = (*(_xdbczr8u+(_aynldcwj - 1) + (_b5p6od9s - 1) * 1 * (_h4ibbatv)) / _1ajfmh55);
Mark110:;
						// continue
					}
										}				}
Mark120:;
				// continue
			}
						}		}//* 
		//*     Update the right singular vector matrix. 
		//* 
		
		if (_umlkckdg == (int)2)
		{
			
			_5nsxi69c("N" ,"N" ,ref _umlkckdg ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_atumjwo3 ,ref _u3fpniqy ,_1469pg8i ,ref _sh6ez9uf ,ref Unsafe.AsRef(_d0547bi2) ,_xdbczr8u ,ref _h4ibbatv );
			return;
		}
		
		_ija9scun = ((int)1 + *(_9wgmm024+((int)1 - 1)));
		_5nsxi69c("N" ,"N" ,ref _umlkckdg ,ref _qwh8ts9f ,ref _ija9scun ,ref Unsafe.AsRef(_kxg5drh2) ,(_atumjwo3+((int)1 - 1) + ((int)1 - 1) * 1 * (_u3fpniqy)),ref _u3fpniqy ,(_1469pg8i+((int)1 - 1) + ((int)1 - 1) * 1 * (_sh6ez9uf)),ref _sh6ez9uf ,ref Unsafe.AsRef(_d0547bi2) ,(_xdbczr8u+((int)1 - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv );
		_ija9scun = (((int)2 + *(_9wgmm024+((int)1 - 1))) + *(_9wgmm024+((int)2 - 1)));
		if (_ija9scun <= _sh6ez9uf)
		_5nsxi69c("N" ,"N" ,ref _umlkckdg ,ref _qwh8ts9f ,ref Unsafe.AsRef(*(_9wgmm024+((int)3 - 1))) ,ref Unsafe.AsRef(_kxg5drh2) ,(_atumjwo3+((int)1 - 1) + (_ija9scun - 1) * 1 * (_u3fpniqy)),ref _u3fpniqy ,(_1469pg8i+(_ija9scun - 1) + ((int)1 - 1) * 1 * (_sh6ez9uf)),ref _sh6ez9uf ,ref Unsafe.AsRef(_kxg5drh2) ,(_xdbczr8u+((int)1 - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv );//* 
		
		_ija9scun = (*(_9wgmm024+((int)1 - 1)) + (int)1);
		_bds60snh = (_oqpc3yjg + _9qyq7j3e);
		if (_ija9scun > (int)1)
		{
			
			{
				System.Int32 __81fgg2dlsvn247 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step247 = (System.Int32)((int)1);
				System.Int32 __81fgg2count247;
				for (__81fgg2count247 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn247 + __81fgg2step247) / __81fgg2step247)), _b5p6od9s = __81fgg2dlsvn247; __81fgg2count247 != 0; __81fgg2count247--, _b5p6od9s += (__81fgg2step247)) {

				{
					
					*(_atumjwo3+(_b5p6od9s - 1) + (_ija9scun - 1) * 1 * (_u3fpniqy)) = *(_atumjwo3+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_u3fpniqy));
Mark130:;
					// continue
				}
								}			}
			{
				System.Int32 __81fgg2dlsvn248 = (System.Int32)(_3lfkfha8);
				const System.Int32 __81fgg2step248 = (System.Int32)((int)1);
				System.Int32 __81fgg2count248;
				for (__81fgg2count248 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn248 + __81fgg2step248) / __81fgg2step248)), _b5p6od9s = __81fgg2dlsvn248; __81fgg2count248 != 0; __81fgg2count248--, _b5p6od9s += (__81fgg2step248)) {

				{
					
					*(_1469pg8i+(_ija9scun - 1) + (_b5p6od9s - 1) * 1 * (_sh6ez9uf)) = *(_1469pg8i+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_sh6ez9uf));
Mark140:;
					// continue
				}
								}			}
		}
		
		_r0ocrtbh = (((int)1 + *(_9wgmm024+((int)2 - 1))) + *(_9wgmm024+((int)3 - 1)));
		_5nsxi69c("N" ,"N" ,ref _umlkckdg ,ref _bds60snh ,ref _r0ocrtbh ,ref Unsafe.AsRef(_kxg5drh2) ,(_atumjwo3+((int)1 - 1) + (_ija9scun - 1) * 1 * (_u3fpniqy)),ref _u3fpniqy ,(_1469pg8i+(_ija9scun - 1) + (_3lfkfha8 - 1) * 1 * (_sh6ez9uf)),ref _sh6ez9uf ,ref Unsafe.AsRef(_d0547bi2) ,(_xdbczr8u+((int)1 - 1) + (_3lfkfha8 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv );//* 
		
		return;//* 
		//*     End of DLASD3 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
