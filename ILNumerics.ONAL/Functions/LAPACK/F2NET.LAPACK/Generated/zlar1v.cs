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
//*> \brief \b ZLAR1V computes the (scaled) r-th column of the inverse of the submatrix in rows b1 through bn of the tridiagonal matrix LDLT - Î»I. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZLAR1V + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zlar1v.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zlar1v.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zlar1v.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZLAR1V( N, B1, BN, LAMBDA, D, L, LD, LLD, 
//*                  PIVMIN, GAPTOL, Z, WANTNC, NEGCNT, ZTZ, MINGMA, 
//*                  R, ISUPPZ, NRMINV, RESID, RQCORR, WORK ) 
//* 
//*       .. Scalar Arguments .. 
//*       LOGICAL            WANTNC 
//*       INTEGER   B1, BN, N, NEGCNT, R 
//*       DOUBLE PRECISION   GAPTOL, LAMBDA, MINGMA, NRMINV, PIVMIN, RESID, 
//*      $                   RQCORR, ZTZ 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            ISUPPZ( * ) 
//*       DOUBLE PRECISION   D( * ), L( * ), LD( * ), LLD( * ), 
//*      $                  WORK( * ) 
//*       COMPLEX*16       Z( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZLAR1V computes the (scaled) r-th column of the inverse of 
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
//*>          LAMBDA is DOUBLE PRECISION 
//*>           The shift. In order to compute an accurate eigenvector, 
//*>           LAMBDA should be a good approximation to an eigenvalue 
//*>           of L D L**T. 
//*> \endverbatim 
//*> 
//*> \param[in] L 
//*> \verbatim 
//*>          L is DOUBLE PRECISION array, dimension (N-1) 
//*>           The (n-1) subdiagonal elements of the unit bidiagonal matrix 
//*>           L, in elements 1 to N-1. 
//*> \endverbatim 
//*> 
//*> \param[in] D 
//*> \verbatim 
//*>          D is DOUBLE PRECISION array, dimension (N) 
//*>           The n diagonal elements of the diagonal matrix D. 
//*> \endverbatim 
//*> 
//*> \param[in] LD 
//*> \verbatim 
//*>          LD is DOUBLE PRECISION array, dimension (N-1) 
//*>           The n-1 elements L(i)*D(i). 
//*> \endverbatim 
//*> 
//*> \param[in] LLD 
//*> \verbatim 
//*>          LLD is DOUBLE PRECISION array, dimension (N-1) 
//*>           The n-1 elements L(i)*L(i)*D(i). 
//*> \endverbatim 
//*> 
//*> \param[in] PIVMIN 
//*> \verbatim 
//*>          PIVMIN is DOUBLE PRECISION 
//*>           The minimum pivot in the Sturm sequence. 
//*> \endverbatim 
//*> 
//*> \param[in] GAPTOL 
//*> \verbatim 
//*>          GAPTOL is DOUBLE PRECISION 
//*>           Tolerance that indicates when eigenvector entries are negligible 
//*>           w.r.t. their contribution to the residual. 
//*> \endverbatim 
//*> 
//*> \param[in,out] Z 
//*> \verbatim 
//*>          Z is COMPLEX*16 array, dimension (N) 
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
//*>          ZTZ is DOUBLE PRECISION 
//*>           The square of the 2-norm of Z. 
//*> \endverbatim 
//*> 
//*> \param[out] MINGMA 
//*> \verbatim 
//*>          MINGMA is DOUBLE PRECISION 
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
//*>          NRMINV is DOUBLE PRECISION 
//*>           NRMINV = 1/SQRT( ZTZ ) 
//*> \endverbatim 
//*> 
//*> \param[out] RESID 
//*> \verbatim 
//*>          RESID is DOUBLE PRECISION 
//*>           The residual of the FP vector. 
//*>           RESID = ABS( MINGMA )/SQRT( ZTZ ) 
//*> \endverbatim 
//*> 
//*> \param[out] RQCORR 
//*> \verbatim 
//*>          RQCORR is DOUBLE PRECISION 
//*>           The Rayleigh Quotient correction to LAMBDA. 
//*>           RQCORR = MINGMA*TMP 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is DOUBLE PRECISION array, dimension (4*N) 
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
//*> \ingroup complex16OTHERauxiliary 
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

	 
	public static void _45aphjn6(ref Int32 _dxpq0xkr, ref Int32 _an5h6pnm, ref Int32 _febv6765, ref Double _w637r8jo, Double* _plfm7z8g, Double* _68ec3gbh, Double* _3sgad2fi, Double* _4sixt94s, ref Double _3aphllyg, ref Double _nljum38y, complex* _7e60fcso, ref Boolean _mpzc1yvt, ref Int32 _bfbhh57k, ref Double _1qk23co8, ref Double _rqh268z3, ref Int32 _q2vwp05i, Int32* _nr4g8ae2, ref Double _mlp50vl9, ref Double _pjqtyez1, ref Double _seewpnfp, Double* _apig8meb)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
complex _40vhxf9f =   new fcomplex(1f,0f);
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
Double _poefthpn =  default;
Double _f8wr73cc =  default;
Double _p1iqarg6 =  default;
Double _irk8i6qr =  default;
Double _2qcyvkhx =  default;
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
		
		_p1iqarg6 = _f43eg0w0("Precision" );// 
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
			System.Int32 __81fgg2dlsvn3692 = (System.Int32)(_an5h6pnm);
			const System.Int32 __81fgg2step3692 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3692;
			for (__81fgg2count3692 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ntcc3h1m - (int)1) - __81fgg2dlsvn3692 + __81fgg2step3692) / __81fgg2step3692)), _b5p6od9s = __81fgg2dlsvn3692; __81fgg2count3692 != 0; __81fgg2count3692--, _b5p6od9s += (__81fgg2step3692)) {

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
		_eiiwuvee = _fk98jwhi(ref _irk8i6qr );
		if (_eiiwuvee)goto Mark60;
		{
			System.Int32 __81fgg2dlsvn3693 = (System.Int32)(_ntcc3h1m);
			const System.Int32 __81fgg2step3693 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3693;
			for (__81fgg2count3693 = System.Math.Max(0, (System.Int32)(((System.Int32)(_o7mrehk9 - (int)1) - __81fgg2dlsvn3693 + __81fgg2step3693) / __81fgg2step3693)), _b5p6od9s = __81fgg2dlsvn3693; __81fgg2count3693 != 0; __81fgg2count3693--, _b5p6od9s += (__81fgg2step3693)) {

			{
				
				_f8wr73cc = (*(_plfm7z8g+(_b5p6od9s - 1)) + _irk8i6qr);
				*(_apig8meb+(_bi4o2jh1 + _b5p6od9s - 1)) = (*(_3sgad2fi+(_b5p6od9s - 1)) / _f8wr73cc);
				*(_apig8meb+(_c5wza3ci + _b5p6od9s - 1)) = ((_irk8i6qr * *(_apig8meb+(_bi4o2jh1 + _b5p6od9s - 1))) * *(_68ec3gbh+(_b5p6od9s - 1)));
				_irk8i6qr = (*(_apig8meb+(_c5wza3ci + _b5p6od9s - 1)) - _w637r8jo);
Mark51:;
				// continue
			}
						}		}
		_eiiwuvee = _fk98jwhi(ref _irk8i6qr );//* 
		
Mark60:;
		// continue
		if (_eiiwuvee)
		{
			//*        Runs a slower version of the above loop if a NaN is detected 
			
			_5cvwo7gs = (int)0;
			_irk8i6qr = (*(_apig8meb+((_c5wza3ci + _an5h6pnm) - (int)1 - 1)) - _w637r8jo);
			{
				System.Int32 __81fgg2dlsvn3694 = (System.Int32)(_an5h6pnm);
				const System.Int32 __81fgg2step3694 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3694;
				for (__81fgg2count3694 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ntcc3h1m - (int)1) - __81fgg2dlsvn3694 + __81fgg2step3694) / __81fgg2step3694)), _b5p6od9s = __81fgg2dlsvn3694; __81fgg2count3694 != 0; __81fgg2count3694--, _b5p6od9s += (__81fgg2step3694)) {

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
				System.Int32 __81fgg2dlsvn3695 = (System.Int32)(_ntcc3h1m);
				const System.Int32 __81fgg2step3695 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3695;
				for (__81fgg2count3695 = System.Math.Max(0, (System.Int32)(((System.Int32)(_o7mrehk9 - (int)1) - __81fgg2dlsvn3695 + __81fgg2step3695) / __81fgg2step3695)), _b5p6od9s = __81fgg2dlsvn3695; __81fgg2count3695 != 0; __81fgg2count3695--, _b5p6od9s += (__81fgg2step3695)) {

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
			System.Int32 __81fgg2dlsvn3696 = (System.Int32)((_febv6765 - (int)1));
			System.Int32 __81fgg2step3696 = (System.Int32)((int)-1);
			System.Int32 __81fgg2count3696;
			for (__81fgg2count3696 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ntcc3h1m) - __81fgg2dlsvn3696 + __81fgg2step3696) / __81fgg2step3696)), _b5p6od9s = __81fgg2dlsvn3696; __81fgg2count3696 != 0; __81fgg2count3696--, _b5p6od9s += (__81fgg2step3696)) {

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
		_a4ehq062 = _fk98jwhi(ref _2qcyvkhx );// 
		
		if (_a4ehq062)
		{
			//*        Runs a slower version of the above loop if a NaN is detected 
			
			_dvyydhuh = (int)0;
			{
				System.Int32 __81fgg2dlsvn3697 = (System.Int32)((_febv6765 - (int)1));
				System.Int32 __81fgg2step3697 = (System.Int32)((int)-1);
				System.Int32 __81fgg2count3697;
				for (__81fgg2count3697 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ntcc3h1m) - __81fgg2dlsvn3697 + __81fgg2step3697) / __81fgg2step3697)), _b5p6od9s = __81fgg2dlsvn3697; __81fgg2count3697 != 0; __81fgg2count3697--, _b5p6od9s += (__81fgg2step3697)) {

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
			System.Int32 __81fgg2dlsvn3698 = (System.Int32)(_ntcc3h1m);
			const System.Int32 __81fgg2step3698 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3698;
			for (__81fgg2count3698 = System.Math.Max(0, (System.Int32)(((System.Int32)(_o7mrehk9 - (int)1) - __81fgg2dlsvn3698 + __81fgg2step3698) / __81fgg2step3698)), _b5p6od9s = __81fgg2dlsvn3698; __81fgg2count3698 != 0; __81fgg2count3698--, _b5p6od9s += (__81fgg2step3698)) {

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
		*(_7e60fcso+(_q2vwp05i - 1)) = _40vhxf9f;
		_1qk23co8 = _kxg5drh2;//* 
		//*     Compute the FP vector upwards from R 
		//* 
		
		if ((!(_eiiwuvee)) & (!(_a4ehq062)))
		{
			
			{
				System.Int32 __81fgg2dlsvn3699 = (System.Int32)((_q2vwp05i - (int)1));
				System.Int32 __81fgg2step3699 = (System.Int32)((int)-1);
				System.Int32 __81fgg2count3699;
				for (__81fgg2count3699 = System.Math.Max(0, (System.Int32)(((System.Int32)(_an5h6pnm) - __81fgg2dlsvn3699 + __81fgg2step3699) / __81fgg2step3699)), _b5p6od9s = __81fgg2dlsvn3699; __81fgg2count3699 != 0; __81fgg2count3699--, _b5p6od9s += (__81fgg2step3699)) {

				{
					
					*(_7e60fcso+(_b5p6od9s - 1)) = (-((*(_apig8meb+(_bi4o2jh1 + _b5p6od9s - 1)) * *(_7e60fcso+(_b5p6od9s + (int)1 - 1)))));
					if (((ILNumerics.F2NET.Intrinsics.ABS(*(_7e60fcso+(_b5p6od9s - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_7e60fcso+(_b5p6od9s + (int)1 - 1)) )) * ILNumerics.F2NET.Intrinsics.ABS(*(_3sgad2fi+(_b5p6od9s - 1)) )) < _nljum38y)
					{
						
						*(_7e60fcso+(_b5p6od9s - 1)) = DCMPLX(_d0547bi2);
						*(_nr4g8ae2+((int)1 - 1)) = (_b5p6od9s + (int)1);goto Mark220;
					}
					
					_1qk23co8 = (_1qk23co8 + ILNumerics.F2NET.Intrinsics.DBLE(*(_7e60fcso+(_b5p6od9s - 1)) * *(_7e60fcso+(_b5p6od9s - 1)) ));
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
				System.Int32 __81fgg2dlsvn3700 = (System.Int32)((_q2vwp05i - (int)1));
				System.Int32 __81fgg2step3700 = (System.Int32)((int)-1);
				System.Int32 __81fgg2count3700;
				for (__81fgg2count3700 = System.Math.Max(0, (System.Int32)(((System.Int32)(_an5h6pnm) - __81fgg2dlsvn3700 + __81fgg2step3700) / __81fgg2step3700)), _b5p6od9s = __81fgg2dlsvn3700; __81fgg2count3700 != 0; __81fgg2count3700--, _b5p6od9s += (__81fgg2step3700)) {

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
						
						*(_7e60fcso+(_b5p6od9s - 1)) = DCMPLX(_d0547bi2);
						*(_nr4g8ae2+((int)1 - 1)) = (_b5p6od9s + (int)1);goto Mark240;
					}
					
					_1qk23co8 = (_1qk23co8 + ILNumerics.F2NET.Intrinsics.DBLE(*(_7e60fcso+(_b5p6od9s - 1)) * *(_7e60fcso+(_b5p6od9s - 1)) ));
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
				System.Int32 __81fgg2dlsvn3701 = (System.Int32)(_q2vwp05i);
				const System.Int32 __81fgg2step3701 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3701;
				for (__81fgg2count3701 = System.Math.Max(0, (System.Int32)(((System.Int32)(_febv6765 - (int)1) - __81fgg2dlsvn3701 + __81fgg2step3701) / __81fgg2step3701)), _b5p6od9s = __81fgg2dlsvn3701; __81fgg2count3701 != 0; __81fgg2count3701--, _b5p6od9s += (__81fgg2step3701)) {

				{
					
					*(_7e60fcso+(_b5p6od9s + (int)1 - 1)) = (-((*(_apig8meb+(_dolnenxf + _b5p6od9s - 1)) * *(_7e60fcso+(_b5p6od9s - 1)))));
					if (((ILNumerics.F2NET.Intrinsics.ABS(*(_7e60fcso+(_b5p6od9s - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_7e60fcso+(_b5p6od9s + (int)1 - 1)) )) * ILNumerics.F2NET.Intrinsics.ABS(*(_3sgad2fi+(_b5p6od9s - 1)) )) < _nljum38y)
					{
						
						*(_7e60fcso+(_b5p6od9s + (int)1 - 1)) = DCMPLX(_d0547bi2);
						*(_nr4g8ae2+((int)2 - 1)) = _b5p6od9s;goto Mark260;
					}
					
					_1qk23co8 = (_1qk23co8 + ILNumerics.F2NET.Intrinsics.DBLE(*(_7e60fcso+(_b5p6od9s + (int)1 - 1)) * *(_7e60fcso+(_b5p6od9s + (int)1 - 1)) ));
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
				System.Int32 __81fgg2dlsvn3702 = (System.Int32)(_q2vwp05i);
				const System.Int32 __81fgg2step3702 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3702;
				for (__81fgg2count3702 = System.Math.Max(0, (System.Int32)(((System.Int32)(_febv6765 - (int)1) - __81fgg2dlsvn3702 + __81fgg2step3702) / __81fgg2step3702)), _b5p6od9s = __81fgg2dlsvn3702; __81fgg2count3702 != 0; __81fgg2count3702--, _b5p6od9s += (__81fgg2step3702)) {

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
						
						*(_7e60fcso+(_b5p6od9s + (int)1 - 1)) = DCMPLX(_d0547bi2);
						*(_nr4g8ae2+((int)2 - 1)) = _b5p6od9s;goto Mark280;
					}
					
					_1qk23co8 = (_1qk23co8 + ILNumerics.F2NET.Intrinsics.DBLE(*(_7e60fcso+(_b5p6od9s + (int)1 - 1)) * *(_7e60fcso+(_b5p6od9s + (int)1 - 1)) ));
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
		//*     End of ZLAR1V 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
