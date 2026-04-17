
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
//*> \brief \b SLAR1V computes the (scaled) r-th column of the inverse of the submatrix in rows b1 through bn of the tridiagonal matrix LDLT - λI. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLAR1V + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slar1v.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slar1v.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slar1v.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLAR1V( N, B1, BN, LAMBDA, D, L, LD, LLD, 
//*                  PIVMIN, GAPTOL, Z, WANTNC, NEGCNT, ZTZ, MINGMA, 
//*                  R, ISUPPZ, NRMINV, RESID, RQCORR, WORK ) 
//* 
//*       .. Scalar Arguments .. 
//*       LOGICAL            WANTNC 
//*       INTEGER   B1, BN, N, NEGCNT, R 
//*       REAL               GAPTOL, LAMBDA, MINGMA, NRMINV, PIVMIN, RESID, 
//*      $                   RQCORR, ZTZ 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            ISUPPZ( * ) 
//*       REAL               D( * ), L( * ), LD( * ), LLD( * ), 
//*      $                  WORK( * ) 
//*       REAL             Z( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SLAR1V computes the (scaled) r-th column of the inverse of 
//*> the sumbmatrix in rows B1 through BN of the tridiagonal matrix 
//*> L D L**T - sigma I. When sigma is close to an eigenvalue, the 
//*> computed vector is an accurate eigenvector. Usually, r corresponds 
//*> to the index where the eigenvector is largest in magnitude. 
//*> The following steps accomplish this computation : 
//*> (a) Stationary qd transform,  L D L**T - sigma I = L(+) D(+) L(+)**T, 
//*> (b) Progressive qd transform, L D L**T - sigma I = U(-) D(-) U(-)**T, 
//*> (c) Computation of the diagonal elements of the inverse of 
//*>     L D L**T - sigma I by combining the above transforms, and choosing 
//*>     r as the index where the diagonal of the inverse is (one of the) 
//*>     largest in magnitude. 
//*> (d) Computation of the (scaled) r-th column of the inverse using the 
//*>     twisted factorization obtained by combining the top part of the 
//*>     the stationary and the bottom part of the progressive transform. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>           The order of the matrix L D L**T. 
//*> \endverbatim 
//*> 
//*> \param[in] B1 
//*> \verbatim 
//*>          B1 is INTEGER 
//*>           First index of the submatrix of L D L**T. 
//*> \endverbatim 
//*> 
//*> \param[in] BN 
//*> \verbatim 
//*>          BN is INTEGER 
//*>           Last index of the submatrix of L D L**T. 
//*> \endverbatim 
//*> 
//*> \param[in] LAMBDA 
//*> \verbatim 
//*>          LAMBDA is REAL 
//*>           The shift. In order to compute an accurate eigenvector, 
//*>           LAMBDA should be a good approximation to an eigenvalue 
//*>           of L D L**T. 
//*> \endverbatim 
//*> 
//*> \param[in] L 
//*> \verbatim 
//*>          L is REAL array, dimension (N-1) 
//*>           The (n-1) subdiagonal elements of the unit bidiagonal matrix 
//*>           L, in elements 1 to N-1. 
//*> \endverbatim 
//*> 
//*> \param[in] D 
//*> \verbatim 
//*>          D is REAL array, dimension (N) 
//*>           The n diagonal elements of the diagonal matrix D. 
//*> \endverbatim 
//*> 
//*> \param[in] LD 
//*> \verbatim 
//*>          LD is REAL array, dimension (N-1) 
//*>           The n-1 elements L(i)*D(i). 
//*> \endverbatim 
//*> 
//*> \param[in] LLD 
//*> \verbatim 
//*>          LLD is REAL array, dimension (N-1) 
//*>           The n-1 elements L(i)*L(i)*D(i). 
//*> \endverbatim 
//*> 
//*> \param[in] PIVMIN 
//*> \verbatim 
//*>          PIVMIN is REAL 
//*>           The minimum pivot in the Sturm sequence. 
//*> \endverbatim 
//*> 
//*> \param[in] GAPTOL 
//*> \verbatim 
//*>          GAPTOL is REAL 
//*>           Tolerance that indicates when eigenvector entries are negligible 
//*>           w.r.t. their contribution to the residual. 
//*> \endverbatim 
//*> 
//*> \param[in,out] Z 
//*> \verbatim 
//*>          Z is REAL array, dimension (N) 
//*>           On input, all entries of Z must be set to 0. 
//*>           On output, Z contains the (scaled) r-th column of the 
//*>           inverse. The scaling is such that Z(R) equals 1. 
//*> \endverbatim 
//*> 
//*> \param[in] WANTNC 
//*> \verbatim 
//*>          WANTNC is LOGICAL 
//*>           Specifies whether NEGCNT has to be computed. 
//*> \endverbatim 
//*> 
//*> \param[out] NEGCNT 
//*> \verbatim 
//*>          NEGCNT is INTEGER 
//*>           If WANTNC is .TRUE. then NEGCNT = the number of pivots < pivmin 
//*>           in the  matrix factorization L D L**T, and NEGCNT = -1 otherwise. 
//*> \endverbatim 
//*> 
//*> \param[out] ZTZ 
//*> \verbatim 
//*>          ZTZ is REAL 
//*>           The square of the 2-norm of Z. 
//*> \endverbatim 
//*> 
//*> \param[out] MINGMA 
//*> \verbatim 
//*>          MINGMA is REAL 
//*>           The reciprocal of the largest (in magnitude) diagonal 
//*>           element of the inverse of L D L**T - sigma I. 
//*> \endverbatim 
//*> 
//*> \param[in,out] R 
//*> \verbatim 
//*>          R is INTEGER 
//*>           The twist index for the twisted factorization used to 
//*>           compute Z. 
//*>           On input, 0 <= R <= N. If R is input as 0, R is set to 
//*>           the index where (L D L**T - sigma I)^{-1} is largest 
//*>           in magnitude. If 1 <= R <= N, R is unchanged. 
//*>           On output, R contains the twist index used to compute Z. 
//*>           Ideally, R designates the position of the maximum entry in the 
//*>           eigenvector. 
//*> \endverbatim 
//*> 
//*> \param[out] ISUPPZ 
//*> \verbatim 
//*>          ISUPPZ is INTEGER array, dimension (2) 
//*>           The support of the vector in Z, i.e., the vector Z is 
//*>           nonzero only in elements ISUPPZ(1) through ISUPPZ( 2 ). 
//*> \endverbatim 
//*> 
//*> \param[out] NRMINV 
//*> \verbatim 
//*>          NRMINV is REAL 
//*>           NRMINV = 1/SQRT( ZTZ ) 
//*> \endverbatim 
//*> 
//*> \param[out] RESID 
//*> \verbatim 
//*>          RESID is REAL 
//*>           The residual of the FP vector. 
//*>           RESID = ABS( MINGMA )/SQRT( ZTZ ) 
//*> \endverbatim 
//*> 
//*> \param[out] RQCORR 
//*> \verbatim 
//*>          RQCORR is REAL 
//*>           The Rayleigh Quotient correction to LAMBDA. 
//*>           RQCORR = MINGMA*TMP 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is REAL array, dimension (4*N) 
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
//*> \ingroup realOTHERauxiliary 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*> Beresford Parlett, University of California, Berkeley, USA \n 
//*> Jim Demmel, University of California, Berkeley, USA \n 
//*> Inderjit Dhillon, University of Texas, Austin, USA \n 
//*> Osni Marques, LBNL/NERSC, USA \n 
//*> Christof Voemel, University of California, Berkeley, USA 
//* 
//*  ===================================================================== 

	 
	public static void _fluj36w4(ref Int32 _dxpq0xkr, ref Int32 _an5h6pnm, ref Int32 _febv6765, ref Single _w637r8jo, Single* _plfm7z8g, Single* _68ec3gbh, Single* _3sgad2fi, Single* _4sixt94s, ref Single _3aphllyg, ref Single _nljum38y, Single* _7e60fcso, ref Boolean _mpzc1yvt, ref Int32 _bfbhh57k, ref Single _1qk23co8, ref Single _rqh268z3, ref Int32 _q2vwp05i, Int32* _nr4g8ae2, ref Single _mlp50vl9, ref Single _pjqtyez1, ref Single _seewpnfp, Single* _apig8meb)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Single _kxg5drh2 =  1f;
Boolean _eiiwuvee =  default;
Boolean _a4ehq062 =  default;
Int32 _b5p6od9s =  default;
Int32 _bi4o2jh1 =  default;
Int32 _ndoq1rqc =  default;
Int32 _c5wza3ci =  default;
Int32 _dolnenxf =  default;
Int32 _5cvwo7gs =  default;
Int32 _dvyydhuh =  default;
Int32 _ntcc3h1m =  default;
Int32 _o7mrehk9 =  default;
Single _poefthpn =  default;
Single _f8wr73cc =  default;
Single _p1iqarg6 =  default;
Single _irk8i6qr =  default;
Single _2qcyvkhx =  default;
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
		// 
		//*     .. 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		_p1iqarg6 = _d5tu038y("Precision" );// 
		// 
		
		if (_q2vwp05i == (int)0)
		{
			
			_ntcc3h1m = _an5h6pnm;
			_o7mrehk9 = _febv6765;
		}
		else
		{
			
			_ntcc3h1m = _q2vwp05i;
			_o7mrehk9 = _q2vwp05i;
		}
		// 
		//*     Storage for LPLUS 
		
		_bi4o2jh1 = (int)0;//*     Storage for UMINUS 
		
		_dolnenxf = _dxpq0xkr;
		_c5wza3ci = (((int)2 * _dxpq0xkr) + (int)1);
		_ndoq1rqc = (((int)3 * _dxpq0xkr) + (int)1);// 
		
		if (_an5h6pnm == (int)1)
		{
			
			*(_apig8meb+(_c5wza3ci - 1)) = _d0547bi2;
		}
		else
		{
			
			*(_apig8meb+((_c5wza3ci + _an5h6pnm) - (int)1 - 1)) = *(_4sixt94s+(_an5h6pnm - (int)1 - 1));
		}
		// 
		//* 
		//*     Compute the stationary transform (using the differential form) 
		//*     until the index R2. 
		//* 
		
		_eiiwuvee = false;
		_5cvwo7gs = (int)0;
		_irk8i6qr = (*(_apig8meb+((_c5wza3ci + _an5h6pnm) - (int)1 - 1)) - _w637r8jo);
		{
			System.Int32 __81fgg2dlsvn3405 = (System.Int32)(_an5h6pnm);
			const System.Int32 __81fgg2step3405 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3405;
			for (__81fgg2count3405 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ntcc3h1m - (int)1) - __81fgg2dlsvn3405 + __81fgg2step3405) / __81fgg2step3405)), _b5p6od9s = __81fgg2dlsvn3405; __81fgg2count3405 != 0; __81fgg2count3405--, _b5p6od9s += (__81fgg2step3405)) {

			{
				
				_f8wr73cc = (*(_plfm7z8g+(_b5p6od9s - 1)) + _irk8i6qr);
				*(_apig8meb+(_bi4o2jh1 + _b5p6od9s - 1)) = (*(_3sgad2fi+(_b5p6od9s - 1)) / _f8wr73cc);
				if (_f8wr73cc < _d0547bi2)
				_5cvwo7gs = (_5cvwo7gs + (int)1);
				*(_apig8meb+(_c5wza3ci + _b5p6od9s - 1)) = ((_irk8i6qr * *(_apig8meb+(_bi4o2jh1 + _b5p6od9s - 1))) * *(_68ec3gbh+(_b5p6od9s - 1)));
				_irk8i6qr = (*(_apig8meb+(_c5wza3ci + _b5p6od9s - 1)) - _w637r8jo);
Mark50:;
				// continue
			}
						}		}
		_eiiwuvee = _lilv8egi(ref _irk8i6qr );
		if (_eiiwuvee)goto Mark60;
		{
			System.Int32 __81fgg2dlsvn3406 = (System.Int32)(_ntcc3h1m);
			const System.Int32 __81fgg2step3406 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3406;
			for (__81fgg2count3406 = System.Math.Max(0, (System.Int32)(((System.Int32)(_o7mrehk9 - (int)1) - __81fgg2dlsvn3406 + __81fgg2step3406) / __81fgg2step3406)), _b5p6od9s = __81fgg2dlsvn3406; __81fgg2count3406 != 0; __81fgg2count3406--, _b5p6od9s += (__81fgg2step3406)) {

			{
				
				_f8wr73cc = (*(_plfm7z8g+(_b5p6od9s - 1)) + _irk8i6qr);
				*(_apig8meb+(_bi4o2jh1 + _b5p6od9s - 1)) = (*(_3sgad2fi+(_b5p6od9s - 1)) / _f8wr73cc);
				*(_apig8meb+(_c5wza3ci + _b5p6od9s - 1)) = ((_irk8i6qr * *(_apig8meb+(_bi4o2jh1 + _b5p6od9s - 1))) * *(_68ec3gbh+(_b5p6od9s - 1)));
				_irk8i6qr = (*(_apig8meb+(_c5wza3ci + _b5p6od9s - 1)) - _w637r8jo);
Mark51:;
				// continue
			}
						}		}
		_eiiwuvee = _lilv8egi(ref _irk8i6qr );//* 
		
Mark60:;
		// continue
		if (_eiiwuvee)
		{
			//*        Runs a slower version of the above loop if a NaN is detected 
			
			_5cvwo7gs = (int)0;
			_irk8i6qr = (*(_apig8meb+((_c5wza3ci + _an5h6pnm) - (int)1 - 1)) - _w637r8jo);
			{
				System.Int32 __81fgg2dlsvn3407 = (System.Int32)(_an5h6pnm);
				const System.Int32 __81fgg2step3407 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3407;
				for (__81fgg2count3407 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ntcc3h1m - (int)1) - __81fgg2dlsvn3407 + __81fgg2step3407) / __81fgg2step3407)), _b5p6od9s = __81fgg2dlsvn3407; __81fgg2count3407 != 0; __81fgg2count3407--, _b5p6od9s += (__81fgg2step3407)) {

				{
					
					_f8wr73cc = (*(_plfm7z8g+(_b5p6od9s - 1)) + _irk8i6qr);
					if (ILNumerics.F2NET.Intrinsics.ABS(_f8wr73cc ) < _3aphllyg)
					_f8wr73cc = (-(_3aphllyg));
					*(_apig8meb+(_bi4o2jh1 + _b5p6od9s - 1)) = (*(_3sgad2fi+(_b5p6od9s - 1)) / _f8wr73cc);
					if (_f8wr73cc < _d0547bi2)
					_5cvwo7gs = (_5cvwo7gs + (int)1);
					*(_apig8meb+(_c5wza3ci + _b5p6od9s - 1)) = ((_irk8i6qr * *(_apig8meb+(_bi4o2jh1 + _b5p6od9s - 1))) * *(_68ec3gbh+(_b5p6od9s - 1)));
					if (*(_apig8meb+(_bi4o2jh1 + _b5p6od9s - 1)) == _d0547bi2)
					*(_apig8meb+(_c5wza3ci + _b5p6od9s - 1)) = *(_4sixt94s+(_b5p6od9s - 1));
					_irk8i6qr = (*(_apig8meb+(_c5wza3ci + _b5p6od9s - 1)) - _w637r8jo);
Mark70:;
					// continue
				}
								}			}
			{
				System.Int32 __81fgg2dlsvn3408 = (System.Int32)(_ntcc3h1m);
				const System.Int32 __81fgg2step3408 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3408;
				for (__81fgg2count3408 = System.Math.Max(0, (System.Int32)(((System.Int32)(_o7mrehk9 - (int)1) - __81fgg2dlsvn3408 + __81fgg2step3408) / __81fgg2step3408)), _b5p6od9s = __81fgg2dlsvn3408; __81fgg2count3408 != 0; __81fgg2count3408--, _b5p6od9s += (__81fgg2step3408)) {

				{
					
					_f8wr73cc = (*(_plfm7z8g+(_b5p6od9s - 1)) + _irk8i6qr);
					if (ILNumerics.F2NET.Intrinsics.ABS(_f8wr73cc ) < _3aphllyg)
					_f8wr73cc = (-(_3aphllyg));
					*(_apig8meb+(_bi4o2jh1 + _b5p6od9s - 1)) = (*(_3sgad2fi+(_b5p6od9s - 1)) / _f8wr73cc);
					*(_apig8meb+(_c5wza3ci + _b5p6od9s - 1)) = ((_irk8i6qr * *(_apig8meb+(_bi4o2jh1 + _b5p6od9s - 1))) * *(_68ec3gbh+(_b5p6od9s - 1)));
					if (*(_apig8meb+(_bi4o2jh1 + _b5p6od9s - 1)) == _d0547bi2)
					*(_apig8meb+(_c5wza3ci + _b5p6od9s - 1)) = *(_4sixt94s+(_b5p6od9s - 1));
					_irk8i6qr = (*(_apig8meb+(_c5wza3ci + _b5p6od9s - 1)) - _w637r8jo);
Mark71:;
					// continue
				}
								}			}
		}
		//* 
		//*     Compute the progressive transform (using the differential form) 
		//*     until the index R1 
		//* 
		
		_a4ehq062 = false;
		_dvyydhuh = (int)0;
		*(_apig8meb+((_ndoq1rqc + _febv6765) - (int)1 - 1)) = (*(_plfm7z8g+(_febv6765 - 1)) - _w637r8jo);
		{
			System.Int32 __81fgg2dlsvn3409 = (System.Int32)((_febv6765 - (int)1));
			System.Int32 __81fgg2step3409 = (System.Int32)((int)-1);
			System.Int32 __81fgg2count3409;
			for (__81fgg2count3409 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ntcc3h1m) - __81fgg2dlsvn3409 + __81fgg2step3409) / __81fgg2step3409)), _b5p6od9s = __81fgg2dlsvn3409; __81fgg2count3409 != 0; __81fgg2count3409--, _b5p6od9s += (__81fgg2step3409)) {

			{
				
				_poefthpn = (*(_4sixt94s+(_b5p6od9s - 1)) + *(_apig8meb+(_ndoq1rqc + _b5p6od9s - 1)));
				_2qcyvkhx = (*(_plfm7z8g+(_b5p6od9s - 1)) / _poefthpn);
				if (_poefthpn < _d0547bi2)
				_dvyydhuh = (_dvyydhuh + (int)1);
				*(_apig8meb+(_dolnenxf + _b5p6od9s - 1)) = (*(_68ec3gbh+(_b5p6od9s - 1)) * _2qcyvkhx);
				*(_apig8meb+((_ndoq1rqc + _b5p6od9s) - (int)1 - 1)) = ((*(_apig8meb+(_ndoq1rqc + _b5p6od9s - 1)) * _2qcyvkhx) - _w637r8jo);
Mark80:;
				// continue
			}
						}		}
		_2qcyvkhx = *(_apig8meb+((_ndoq1rqc + _ntcc3h1m) - (int)1 - 1));
		_a4ehq062 = _lilv8egi(ref _2qcyvkhx );// 
		
		if (_a4ehq062)
		{
			//*        Runs a slower version of the above loop if a NaN is detected 
			
			_dvyydhuh = (int)0;
			{
				System.Int32 __81fgg2dlsvn3410 = (System.Int32)((_febv6765 - (int)1));
				System.Int32 __81fgg2step3410 = (System.Int32)((int)-1);
				System.Int32 __81fgg2count3410;
				for (__81fgg2count3410 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ntcc3h1m) - __81fgg2dlsvn3410 + __81fgg2step3410) / __81fgg2step3410)), _b5p6od9s = __81fgg2dlsvn3410; __81fgg2count3410 != 0; __81fgg2count3410--, _b5p6od9s += (__81fgg2step3410)) {

				{
					
					_poefthpn = (*(_4sixt94s+(_b5p6od9s - 1)) + *(_apig8meb+(_ndoq1rqc + _b5p6od9s - 1)));
					if (ILNumerics.F2NET.Intrinsics.ABS(_poefthpn ) < _3aphllyg)
					_poefthpn = (-(_3aphllyg));
					_2qcyvkhx = (*(_plfm7z8g+(_b5p6od9s - 1)) / _poefthpn);
					if (_poefthpn < _d0547bi2)
					_dvyydhuh = (_dvyydhuh + (int)1);
					*(_apig8meb+(_dolnenxf + _b5p6od9s - 1)) = (*(_68ec3gbh+(_b5p6od9s - 1)) * _2qcyvkhx);
					*(_apig8meb+((_ndoq1rqc + _b5p6od9s) - (int)1 - 1)) = ((*(_apig8meb+(_ndoq1rqc + _b5p6od9s - 1)) * _2qcyvkhx) - _w637r8jo);
					if (_2qcyvkhx == _d0547bi2)
					*(_apig8meb+((_ndoq1rqc + _b5p6od9s) - (int)1 - 1)) = (*(_plfm7z8g+(_b5p6od9s - 1)) - _w637r8jo);
Mark100:;
					// continue
				}
								}			}
		}
		//* 
		//*     Find the index (from R1 to R2) of the largest (in magnitude) 
		//*     diagonal element of the inverse 
		//* 
		
		_rqh268z3 = (*(_apig8meb+((_c5wza3ci + _ntcc3h1m) - (int)1 - 1)) + *(_apig8meb+((_ndoq1rqc + _ntcc3h1m) - (int)1 - 1)));
		if (_rqh268z3 < _d0547bi2)
		_5cvwo7gs = (_5cvwo7gs + (int)1);
		if (_mpzc1yvt)
		{
			
			_bfbhh57k = (_5cvwo7gs + _dvyydhuh);
		}
		else
		{
			
			_bfbhh57k = (int)-1;
		}
		
		if (ILNumerics.F2NET.Intrinsics.ABS(_rqh268z3 ) == _d0547bi2)
		_rqh268z3 = (_p1iqarg6 * *(_apig8meb+((_c5wza3ci + _ntcc3h1m) - (int)1 - 1)));
		_q2vwp05i = _ntcc3h1m;
		{
			System.Int32 __81fgg2dlsvn3411 = (System.Int32)(_ntcc3h1m);
			const System.Int32 __81fgg2step3411 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3411;
			for (__81fgg2count3411 = System.Math.Max(0, (System.Int32)(((System.Int32)(_o7mrehk9 - (int)1) - __81fgg2dlsvn3411 + __81fgg2step3411) / __81fgg2step3411)), _b5p6od9s = __81fgg2dlsvn3411; __81fgg2count3411 != 0; __81fgg2count3411--, _b5p6od9s += (__81fgg2step3411)) {

			{
				
				_2qcyvkhx = (*(_apig8meb+(_c5wza3ci + _b5p6od9s - 1)) + *(_apig8meb+(_ndoq1rqc + _b5p6od9s - 1)));
				if (_2qcyvkhx == _d0547bi2)
				_2qcyvkhx = (_p1iqarg6 * *(_apig8meb+(_c5wza3ci + _b5p6od9s - 1)));
				if (ILNumerics.F2NET.Intrinsics.ABS(_2qcyvkhx ) <= ILNumerics.F2NET.Intrinsics.ABS(_rqh268z3 ))
				{
					
					_rqh268z3 = _2qcyvkhx;
					_q2vwp05i = (_b5p6od9s + (int)1);
				}
				
Mark110:;
				// continue
			}
						}		}//* 
		//*     Compute the FP vector: solve N^T v = e_r 
		//* 
		
		*(_nr4g8ae2+((int)1 - 1)) = _an5h6pnm;
		*(_nr4g8ae2+((int)2 - 1)) = _febv6765;
		*(_7e60fcso+(_q2vwp05i - 1)) = _kxg5drh2;
		_1qk23co8 = _kxg5drh2;//* 
		//*     Compute the FP vector upwards from R 
		//* 
		
		if ((!(_eiiwuvee)) & (!(_a4ehq062)))
		{
			
			{
				System.Int32 __81fgg2dlsvn3412 = (System.Int32)((_q2vwp05i - (int)1));
				System.Int32 __81fgg2step3412 = (System.Int32)((int)-1);
				System.Int32 __81fgg2count3412;
				for (__81fgg2count3412 = System.Math.Max(0, (System.Int32)(((System.Int32)(_an5h6pnm) - __81fgg2dlsvn3412 + __81fgg2step3412) / __81fgg2step3412)), _b5p6od9s = __81fgg2dlsvn3412; __81fgg2count3412 != 0; __81fgg2count3412--, _b5p6od9s += (__81fgg2step3412)) {

				{
					
					*(_7e60fcso+(_b5p6od9s - 1)) = (-((*(_apig8meb+(_bi4o2jh1 + _b5p6od9s - 1)) * *(_7e60fcso+(_b5p6od9s + (int)1 - 1)))));
					if (((ILNumerics.F2NET.Intrinsics.ABS(*(_7e60fcso+(_b5p6od9s - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_7e60fcso+(_b5p6od9s + (int)1 - 1)) )) * ILNumerics.F2NET.Intrinsics.ABS(*(_3sgad2fi+(_b5p6od9s - 1)) )) < _nljum38y)
					{
						
						*(_7e60fcso+(_b5p6od9s - 1)) = _d0547bi2;
						*(_nr4g8ae2+((int)1 - 1)) = (_b5p6od9s + (int)1);goto Mark220;
					}
					
					_1qk23co8 = (_1qk23co8 + (*(_7e60fcso+(_b5p6od9s - 1)) * *(_7e60fcso+(_b5p6od9s - 1))));
Mark210:;
					// continue
				}
								}			}
Mark220:;
			// continue
		}
		else
		{
			//*        Run slower loop if NaN occurred. 
			
			{
				System.Int32 __81fgg2dlsvn3413 = (System.Int32)((_q2vwp05i - (int)1));
				System.Int32 __81fgg2step3413 = (System.Int32)((int)-1);
				System.Int32 __81fgg2count3413;
				for (__81fgg2count3413 = System.Math.Max(0, (System.Int32)(((System.Int32)(_an5h6pnm) - __81fgg2dlsvn3413 + __81fgg2step3413) / __81fgg2step3413)), _b5p6od9s = __81fgg2dlsvn3413; __81fgg2count3413 != 0; __81fgg2count3413--, _b5p6od9s += (__81fgg2step3413)) {

				{
					
					if (*(_7e60fcso+(_b5p6od9s + (int)1 - 1)) == _d0547bi2)
					{
						
						*(_7e60fcso+(_b5p6od9s - 1)) = (-(((*(_3sgad2fi+(_b5p6od9s + (int)1 - 1)) / *(_3sgad2fi+(_b5p6od9s - 1))) * *(_7e60fcso+(_b5p6od9s + (int)2 - 1)))));
					}
					else
					{
						
						*(_7e60fcso+(_b5p6od9s - 1)) = (-((*(_apig8meb+(_bi4o2jh1 + _b5p6od9s - 1)) * *(_7e60fcso+(_b5p6od9s + (int)1 - 1)))));
					}
					
					if (((ILNumerics.F2NET.Intrinsics.ABS(*(_7e60fcso+(_b5p6od9s - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_7e60fcso+(_b5p6od9s + (int)1 - 1)) )) * ILNumerics.F2NET.Intrinsics.ABS(*(_3sgad2fi+(_b5p6od9s - 1)) )) < _nljum38y)
					{
						
						*(_7e60fcso+(_b5p6od9s - 1)) = _d0547bi2;
						*(_nr4g8ae2+((int)1 - 1)) = (_b5p6od9s + (int)1);goto Mark240;
					}
					
					_1qk23co8 = (_1qk23co8 + (*(_7e60fcso+(_b5p6od9s - 1)) * *(_7e60fcso+(_b5p6od9s - 1))));
Mark230:;
					// continue
				}
								}			}
Mark240:;
			// continue
		}
		// 
		//*     Compute the FP vector downwards from R in blocks of size BLKSIZ 
		
		if ((!(_eiiwuvee)) & (!(_a4ehq062)))
		{
			
			{
				System.Int32 __81fgg2dlsvn3414 = (System.Int32)(_q2vwp05i);
				const System.Int32 __81fgg2step3414 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3414;
				for (__81fgg2count3414 = System.Math.Max(0, (System.Int32)(((System.Int32)(_febv6765 - (int)1) - __81fgg2dlsvn3414 + __81fgg2step3414) / __81fgg2step3414)), _b5p6od9s = __81fgg2dlsvn3414; __81fgg2count3414 != 0; __81fgg2count3414--, _b5p6od9s += (__81fgg2step3414)) {

				{
					
					*(_7e60fcso+(_b5p6od9s + (int)1 - 1)) = (-((*(_apig8meb+(_dolnenxf + _b5p6od9s - 1)) * *(_7e60fcso+(_b5p6od9s - 1)))));
					if (((ILNumerics.F2NET.Intrinsics.ABS(*(_7e60fcso+(_b5p6od9s - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_7e60fcso+(_b5p6od9s + (int)1 - 1)) )) * ILNumerics.F2NET.Intrinsics.ABS(*(_3sgad2fi+(_b5p6od9s - 1)) )) < _nljum38y)
					{
						
						*(_7e60fcso+(_b5p6od9s + (int)1 - 1)) = _d0547bi2;
						*(_nr4g8ae2+((int)2 - 1)) = _b5p6od9s;goto Mark260;
					}
					
					_1qk23co8 = (_1qk23co8 + (*(_7e60fcso+(_b5p6od9s + (int)1 - 1)) * *(_7e60fcso+(_b5p6od9s + (int)1 - 1))));
Mark250:;
					// continue
				}
								}			}
Mark260:;
			// continue
		}
		else
		{
			//*        Run slower loop if NaN occurred. 
			
			{
				System.Int32 __81fgg2dlsvn3415 = (System.Int32)(_q2vwp05i);
				const System.Int32 __81fgg2step3415 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3415;
				for (__81fgg2count3415 = System.Math.Max(0, (System.Int32)(((System.Int32)(_febv6765 - (int)1) - __81fgg2dlsvn3415 + __81fgg2step3415) / __81fgg2step3415)), _b5p6od9s = __81fgg2dlsvn3415; __81fgg2count3415 != 0; __81fgg2count3415--, _b5p6od9s += (__81fgg2step3415)) {

				{
					
					if (*(_7e60fcso+(_b5p6od9s - 1)) == _d0547bi2)
					{
						
						*(_7e60fcso+(_b5p6od9s + (int)1 - 1)) = (-(((*(_3sgad2fi+(_b5p6od9s - (int)1 - 1)) / *(_3sgad2fi+(_b5p6od9s - 1))) * *(_7e60fcso+(_b5p6od9s - (int)1 - 1)))));
					}
					else
					{
						
						*(_7e60fcso+(_b5p6od9s + (int)1 - 1)) = (-((*(_apig8meb+(_dolnenxf + _b5p6od9s - 1)) * *(_7e60fcso+(_b5p6od9s - 1)))));
					}
					
					if (((ILNumerics.F2NET.Intrinsics.ABS(*(_7e60fcso+(_b5p6od9s - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_7e60fcso+(_b5p6od9s + (int)1 - 1)) )) * ILNumerics.F2NET.Intrinsics.ABS(*(_3sgad2fi+(_b5p6od9s - 1)) )) < _nljum38y)
					{
						
						*(_7e60fcso+(_b5p6od9s + (int)1 - 1)) = _d0547bi2;
						*(_nr4g8ae2+((int)2 - 1)) = _b5p6od9s;goto Mark280;
					}
					
					_1qk23co8 = (_1qk23co8 + (*(_7e60fcso+(_b5p6od9s + (int)1 - 1)) * *(_7e60fcso+(_b5p6od9s + (int)1 - 1))));
Mark270:;
					// continue
				}
								}			}
Mark280:;
			// continue
		}
		//* 
		//*     Compute quantities for convergence test 
		//* 
		
		_2qcyvkhx = (_kxg5drh2 / _1qk23co8);
		_mlp50vl9 = ILNumerics.F2NET.Intrinsics.SQRT(_2qcyvkhx );
		_pjqtyez1 = (ILNumerics.F2NET.Intrinsics.ABS(_rqh268z3 ) * _mlp50vl9);
		_seewpnfp = (_rqh268z3 * _2qcyvkhx);//* 
		//* 
		
		return;//* 
		//*     End of SLAR1V 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
