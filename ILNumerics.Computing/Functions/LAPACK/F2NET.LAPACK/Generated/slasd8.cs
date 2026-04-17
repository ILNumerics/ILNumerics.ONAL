
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
//*> \brief \b SLASD8 finds the square roots of the roots of the secular equation, and stores, for each element in D, the distance to its two nearest poles. Used by sbdsdc. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLASD8 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slasd8.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slasd8.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slasd8.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLASD8( ICOMPQ, K, D, Z, VF, VL, DIFL, DIFR, LDDIFR, 
//*                          DSIGMA, WORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            ICOMPQ, INFO, K, LDDIFR 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               D( * ), DIFL( * ), DIFR( LDDIFR, * ), 
//*      $                   DSIGMA( * ), VF( * ), VL( * ), WORK( * ), 
//*      $                   Z( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SLASD8 finds the square roots of the roots of the secular equation, 
//*> as defined by the values in DSIGMA and Z. It makes the appropriate 
//*> calls to SLASD4, and stores, for each  element in D, the distance 
//*> to its two nearest poles (elements in DSIGMA). It also updates 
//*> the arrays VF and VL, the first and last components of all the 
//*> right singular vectors of the original bidiagonal matrix. 
//*> 
//*> SLASD8 is called from SLASD6. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] ICOMPQ 
//*> \verbatim 
//*>          ICOMPQ is INTEGER 
//*>          Specifies whether singular vectors are to be computed in 
//*>          factored form in the calling routine: 
//*>          = 0: Compute singular values only. 
//*>          = 1: Compute singular vectors in factored form as well. 
//*> \endverbatim 
//*> 
//*> \param[in] K 
//*> \verbatim 
//*>          K is INTEGER 
//*>          The number of terms in the rational function to be solved 
//*>          by SLASD4.  K >= 1. 
//*> \endverbatim 
//*> 
//*> \param[out] D 
//*> \verbatim 
//*>          D is REAL array, dimension ( K ) 
//*>          On output, D contains the updated singular values. 
//*> \endverbatim 
//*> 
//*> \param[in,out] Z 
//*> \verbatim 
//*>          Z is REAL array, dimension ( K ) 
//*>          On entry, the first K elements of this array contain the 
//*>          components of the deflation-adjusted updating row vector. 
//*>          On exit, Z is updated. 
//*> \endverbatim 
//*> 
//*> \param[in,out] VF 
//*> \verbatim 
//*>          VF is REAL array, dimension ( K ) 
//*>          On entry, VF contains  information passed through DBEDE8. 
//*>          On exit, VF contains the first K components of the first 
//*>          components of all right singular vectors of the bidiagonal 
//*>          matrix. 
//*> \endverbatim 
//*> 
//*> \param[in,out] VL 
//*> \verbatim 
//*>          VL is REAL array, dimension ( K ) 
//*>          On entry, VL contains  information passed through DBEDE8. 
//*>          On exit, VL contains the first K components of the last 
//*>          components of all right singular vectors of the bidiagonal 
//*>          matrix. 
//*> \endverbatim 
//*> 
//*> \param[out] DIFL 
//*> \verbatim 
//*>          DIFL is REAL array, dimension ( K ) 
//*>          On exit, DIFL(I) = D(I) - DSIGMA(I). 
//*> \endverbatim 
//*> 
//*> \param[out] DIFR 
//*> \verbatim 
//*>          DIFR is REAL array, 
//*>                   dimension ( LDDIFR, 2 ) if ICOMPQ = 1 and 
//*>                   dimension ( K ) if ICOMPQ = 0. 
//*>          On exit, DIFR(I,1) = D(I) - DSIGMA(I+1), DIFR(K,1) is not 
//*>          defined and will not be referenced. 
//*> 
//*>          If ICOMPQ = 1, DIFR(1:K,2) is an array containing the 
//*>          normalizing factors for the right singular vector matrix. 
//*> \endverbatim 
//*> 
//*> \param[in] LDDIFR 
//*> \verbatim 
//*>          LDDIFR is INTEGER 
//*>          The leading dimension of DIFR, must be at least K. 
//*> \endverbatim 
//*> 
//*> \param[in,out] DSIGMA 
//*> \verbatim 
//*>          DSIGMA is REAL array, dimension ( K ) 
//*>          On entry, the first K elements of this array contain the old 
//*>          roots of the deflated updating problem.  These are the poles 
//*>          of the secular equation. 
//*>          On exit, the elements of DSIGMA may be very slightly altered 
//*>          in value. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is REAL array, dimension (3*K) 
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

	 
	public static void _1al7xefr(ref Int32 _y1be9goe, ref Int32 _umlkckdg, Single* _plfm7z8g, Single* _7e60fcso, Single* _w7xcjdw0, Single* _ppzorcqs, Single* _i8976ehd, Single* _doljbvm2, ref Int32 _3hegpgr7, Single* _1r8q3o4r, Single* _apig8meb, ref Int32 _gro5yvfo)
	{
#region variable declarations
Single _kxg5drh2 =  1f;
Int32 _b5p6od9s =  default;
Int32 _e22nb277 =  default;
Int32 _syt9c4vh =  default;
Int32 _6qz0yf25 =  default;
Int32 _pnev5eap =  default;
Int32 _pnmkng5a =  default;
Int32 _znpjgsef =  default;
Single _ci8g9yb9 =  default;
Single _w6j9q0j8 =  default;
Single _a67yxlco =  default;
Single _ydtysk30 =  default;
Single _uzyphv61 =  default;
Single _4qwfue8o =  default;
Single _1ajfmh55 =  default;
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
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
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
		if (_umlkckdg < (int)1)
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_3hegpgr7 < _umlkckdg)
		{
			
			_gro5yvfo = (int)-9;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("SLASD8" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if (_umlkckdg == (int)1)
		{
			
			*(_plfm7z8g+((int)1 - 1)) = ILNumerics.F2NET.Intrinsics.ABS(*(_7e60fcso+((int)1 - 1)) );
			*(_i8976ehd+((int)1 - 1)) = *(_plfm7z8g+((int)1 - 1));
			if (_y1be9goe == (int)1)
			{
				
				*(_i8976ehd+((int)2 - 1)) = _kxg5drh2;
				*(_doljbvm2+((int)1 - 1) + ((int)2 - 1) * 1 * (_3hegpgr7)) = _kxg5drh2;
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
		//*     2*DLAMBDA(I) to prevent optimizing compilers from eliminating 
		//*     this code. 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn751 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step751 = (System.Int32)((int)1);
			System.Int32 __81fgg2count751;
			for (__81fgg2count751 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn751 + __81fgg2step751) / __81fgg2step751)), _b5p6od9s = __81fgg2dlsvn751; __81fgg2count751 != 0; __81fgg2count751--, _b5p6od9s += (__81fgg2step751)) {

			{
				
				*(_1r8q3o4r+(_b5p6od9s - 1)) = (_a1q56mil(ref Unsafe.AsRef(*(_1r8q3o4r+(_b5p6od9s - 1))) ,ref Unsafe.AsRef(*(_1r8q3o4r+(_b5p6od9s - 1))) ) - *(_1r8q3o4r+(_b5p6od9s - 1)));
Mark10:;
				// continue
			}
						}		}//* 
		//*     Book keeping. 
		//* 
		
		_e22nb277 = (int)1;
		_syt9c4vh = (_e22nb277 + _umlkckdg);
		_pnev5eap = (_syt9c4vh + _umlkckdg);
		_6qz0yf25 = (_syt9c4vh - (int)1);
		_pnmkng5a = (_pnev5eap - (int)1);//* 
		//*     Normalize Z. 
		//* 
		
		_4qwfue8o = _z20xbrro(ref _umlkckdg ,_7e60fcso ,ref Unsafe.AsRef((int)1) );
		_cq2kgmi4("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _4qwfue8o ,ref Unsafe.AsRef(_kxg5drh2) ,ref _umlkckdg ,ref Unsafe.AsRef((int)1) ,_7e60fcso ,ref _umlkckdg ,ref _gro5yvfo );
		_4qwfue8o = (_4qwfue8o * _4qwfue8o);//* 
		//*     Initialize WORK(IWK3). 
		//* 
		
		_t013e1c8("A" ,ref _umlkckdg ,ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,ref Unsafe.AsRef(_kxg5drh2) ,(_apig8meb+(_pnev5eap - 1)),ref _umlkckdg );//* 
		//*     Compute the updated singular values, the arrays DIFL, DIFR, 
		//*     and the updated Z. 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn752 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step752 = (System.Int32)((int)1);
			System.Int32 __81fgg2count752;
			for (__81fgg2count752 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn752 + __81fgg2step752) / __81fgg2step752)), _znpjgsef = __81fgg2dlsvn752; __81fgg2count752 != 0; __81fgg2count752--, _znpjgsef += (__81fgg2step752)) {

			{
				
				_bnshiskn(ref _umlkckdg ,ref _znpjgsef ,_1r8q3o4r ,_7e60fcso ,(_apig8meb+(_e22nb277 - 1)),ref _4qwfue8o ,ref Unsafe.AsRef(*(_plfm7z8g+(_znpjgsef - 1))) ,(_apig8meb+(_syt9c4vh - 1)),ref _gro5yvfo );//* 
				//*        If the root finder fails, report the convergence failure. 
				//* 
				
				if (_gro5yvfo != (int)0)
				{
					
					return;
				}
				
				*(_apig8meb+(_pnmkng5a + _znpjgsef - 1)) = ((*(_apig8meb+(_pnmkng5a + _znpjgsef - 1)) * *(_apig8meb+(_znpjgsef - 1))) * *(_apig8meb+(_6qz0yf25 + _znpjgsef - 1)));
				*(_i8976ehd+(_znpjgsef - 1)) = (-(*(_apig8meb+(_znpjgsef - 1))));
				*(_doljbvm2+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_3hegpgr7)) = (-(*(_apig8meb+(_znpjgsef + (int)1 - 1))));
				{
					System.Int32 __81fgg2dlsvn753 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step753 = (System.Int32)((int)1);
					System.Int32 __81fgg2count753;
					for (__81fgg2count753 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn753 + __81fgg2step753) / __81fgg2step753)), _b5p6od9s = __81fgg2dlsvn753; __81fgg2count753 != 0; __81fgg2count753--, _b5p6od9s += (__81fgg2step753)) {

					{
						
						*(_apig8meb+(_pnmkng5a + _b5p6od9s - 1)) = ((((*(_apig8meb+(_pnmkng5a + _b5p6od9s - 1)) * *(_apig8meb+(_b5p6od9s - 1))) * *(_apig8meb+(_6qz0yf25 + _b5p6od9s - 1))) / (*(_1r8q3o4r+(_b5p6od9s - 1)) - *(_1r8q3o4r+(_znpjgsef - 1)))) / (*(_1r8q3o4r+(_b5p6od9s - 1)) + *(_1r8q3o4r+(_znpjgsef - 1))));
Mark20:;
						// continue
					}
										}				}
				{
					System.Int32 __81fgg2dlsvn754 = (System.Int32)((_znpjgsef + (int)1));
					const System.Int32 __81fgg2step754 = (System.Int32)((int)1);
					System.Int32 __81fgg2count754;
					for (__81fgg2count754 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn754 + __81fgg2step754) / __81fgg2step754)), _b5p6od9s = __81fgg2dlsvn754; __81fgg2count754 != 0; __81fgg2count754--, _b5p6od9s += (__81fgg2step754)) {

					{
						
						*(_apig8meb+(_pnmkng5a + _b5p6od9s - 1)) = ((((*(_apig8meb+(_pnmkng5a + _b5p6od9s - 1)) * *(_apig8meb+(_b5p6od9s - 1))) * *(_apig8meb+(_6qz0yf25 + _b5p6od9s - 1))) / (*(_1r8q3o4r+(_b5p6od9s - 1)) - *(_1r8q3o4r+(_znpjgsef - 1)))) / (*(_1r8q3o4r+(_b5p6od9s - 1)) + *(_1r8q3o4r+(_znpjgsef - 1))));
Mark30:;
						// continue
					}
										}				}
Mark40:;
				// continue
			}
						}		}//* 
		//*     Compute updated Z. 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn755 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step755 = (System.Int32)((int)1);
			System.Int32 __81fgg2count755;
			for (__81fgg2count755 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn755 + __81fgg2step755) / __81fgg2step755)), _b5p6od9s = __81fgg2dlsvn755; __81fgg2count755 != 0; __81fgg2count755--, _b5p6od9s += (__81fgg2step755)) {

			{
				
				*(_7e60fcso+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.SIGN(ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS(*(_apig8meb+(_pnmkng5a + _b5p6od9s - 1)) ) ) ,*(_7e60fcso+(_b5p6od9s - 1)) );
Mark50:;
				// continue
			}
						}		}//* 
		//*     Update VF and VL. 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn756 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step756 = (System.Int32)((int)1);
			System.Int32 __81fgg2count756;
			for (__81fgg2count756 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn756 + __81fgg2step756) / __81fgg2step756)), _znpjgsef = __81fgg2dlsvn756; __81fgg2count756 != 0; __81fgg2count756--, _znpjgsef += (__81fgg2step756)) {

			{
				
				_ci8g9yb9 = *(_i8976ehd+(_znpjgsef - 1));
				_a67yxlco = *(_plfm7z8g+(_znpjgsef - 1));
				_ydtysk30 = (-(*(_1r8q3o4r+(_znpjgsef - 1))));
				if (_znpjgsef < _umlkckdg)
				{
					
					_w6j9q0j8 = (-(*(_doljbvm2+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_3hegpgr7))));
					_uzyphv61 = (-(*(_1r8q3o4r+(_znpjgsef + (int)1 - 1))));
				}
				
				*(_apig8meb+(_znpjgsef - 1)) = (-(((*(_7e60fcso+(_znpjgsef - 1)) / _ci8g9yb9) / (*(_1r8q3o4r+(_znpjgsef - 1)) + _a67yxlco))));
				{
					System.Int32 __81fgg2dlsvn757 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step757 = (System.Int32)((int)1);
					System.Int32 __81fgg2count757;
					for (__81fgg2count757 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn757 + __81fgg2step757) / __81fgg2step757)), _b5p6od9s = __81fgg2dlsvn757; __81fgg2count757 != 0; __81fgg2count757--, _b5p6od9s += (__81fgg2step757)) {

					{
						
						*(_apig8meb+(_b5p6od9s - 1)) = ((*(_7e60fcso+(_b5p6od9s - 1)) / (_a1q56mil(ref Unsafe.AsRef(*(_1r8q3o4r+(_b5p6od9s - 1))) ,ref _ydtysk30 ) - _ci8g9yb9)) / (*(_1r8q3o4r+(_b5p6od9s - 1)) + _a67yxlco));
Mark60:;
						// continue
					}
										}				}
				{
					System.Int32 __81fgg2dlsvn758 = (System.Int32)((_znpjgsef + (int)1));
					const System.Int32 __81fgg2step758 = (System.Int32)((int)1);
					System.Int32 __81fgg2count758;
					for (__81fgg2count758 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn758 + __81fgg2step758) / __81fgg2step758)), _b5p6od9s = __81fgg2dlsvn758; __81fgg2count758 != 0; __81fgg2count758--, _b5p6od9s += (__81fgg2step758)) {

					{
						
						*(_apig8meb+(_b5p6od9s - 1)) = ((*(_7e60fcso+(_b5p6od9s - 1)) / (_a1q56mil(ref Unsafe.AsRef(*(_1r8q3o4r+(_b5p6od9s - 1))) ,ref _uzyphv61 ) + _w6j9q0j8)) / (*(_1r8q3o4r+(_b5p6od9s - 1)) + _a67yxlco));
Mark70:;
						// continue
					}
										}				}
				_1ajfmh55 = _z20xbrro(ref _umlkckdg ,_apig8meb ,ref Unsafe.AsRef((int)1) );
				*(_apig8meb+(_6qz0yf25 + _znpjgsef - 1)) = (_j4n7j2pu(ref _umlkckdg ,_apig8meb ,ref Unsafe.AsRef((int)1) ,_w7xcjdw0 ,ref Unsafe.AsRef((int)1) ) / _1ajfmh55);
				*(_apig8meb+(_pnmkng5a + _znpjgsef - 1)) = (_j4n7j2pu(ref _umlkckdg ,_apig8meb ,ref Unsafe.AsRef((int)1) ,_ppzorcqs ,ref Unsafe.AsRef((int)1) ) / _1ajfmh55);
				if (_y1be9goe == (int)1)
				{
					
					*(_doljbvm2+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_3hegpgr7)) = _1ajfmh55;
				}
				
Mark80:;
				// continue
			}
						}		}//* 
		
		_wcs7ne88(ref _umlkckdg ,(_apig8meb+(_syt9c4vh - 1)),ref Unsafe.AsRef((int)1) ,_w7xcjdw0 ,ref Unsafe.AsRef((int)1) );
		_wcs7ne88(ref _umlkckdg ,(_apig8meb+(_pnev5eap - 1)),ref Unsafe.AsRef((int)1) ,_ppzorcqs ,ref Unsafe.AsRef((int)1) );//* 
		
		return;//* 
		//*     End of SLASD8 
		//* 
		
	}
	
	} // 177
// 

} // end class 
} // end namespace
#endif
