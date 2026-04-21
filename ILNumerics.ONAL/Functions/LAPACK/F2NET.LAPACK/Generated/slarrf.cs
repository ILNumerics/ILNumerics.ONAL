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
//*> \brief \b SLARRF finds a new relatively robust representation such that at least one of the eigenvalues is relatively isolated. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLARRF + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slarrf.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slarrf.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slarrf.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLARRF( N, D, L, LD, CLSTRT, CLEND, 
//*                          W, WGAP, WERR, 
//*                          SPDIAM, CLGAPL, CLGAPR, PIVMIN, SIGMA, 
//*                          DPLUS, LPLUS, WORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            CLSTRT, CLEND, INFO, N 
//*       REAL               CLGAPL, CLGAPR, PIVMIN, SIGMA, SPDIAM 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               D( * ), DPLUS( * ), L( * ), LD( * ), 
//*      $          LPLUS( * ), W( * ), WGAP( * ), WERR( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> Given the initial representation L D L^T and its cluster of close 
//*> eigenvalues (in a relative measure), W( CLSTRT ), W( CLSTRT+1 ), ... 
//*> W( CLEND ), SLARRF finds a new relatively robust representation 
//*> L D L^T - SIGMA I = L(+) D(+) L(+)^T such that at least one of the 
//*> eigenvalues of L(+) D(+) L(+)^T is relatively isolated. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix (subblock, if the matrix split). 
//*> \endverbatim 
//*> 
//*> \param[in] D 
//*> \verbatim 
//*>          D is REAL array, dimension (N) 
//*>          The N diagonal elements of the diagonal matrix D. 
//*> \endverbatim 
//*> 
//*> \param[in] L 
//*> \verbatim 
//*>          L is REAL array, dimension (N-1) 
//*>          The (N-1) subdiagonal elements of the unit bidiagonal 
//*>          matrix L. 
//*> \endverbatim 
//*> 
//*> \param[in] LD 
//*> \verbatim 
//*>          LD is REAL array, dimension (N-1) 
//*>          The (N-1) elements L(i)*D(i). 
//*> \endverbatim 
//*> 
//*> \param[in] CLSTRT 
//*> \verbatim 
//*>          CLSTRT is INTEGER 
//*>          The index of the first eigenvalue in the cluster. 
//*> \endverbatim 
//*> 
//*> \param[in] CLEND 
//*> \verbatim 
//*>          CLEND is INTEGER 
//*>          The index of the last eigenvalue in the cluster. 
//*> \endverbatim 
//*> 
//*> \param[in] W 
//*> \verbatim 
//*>          W is REAL array, dimension 
//*>          dimension is >=  (CLEND-CLSTRT+1) 
//*>          The eigenvalue APPROXIMATIONS of L D L^T in ascending order. 
//*>          W( CLSTRT ) through W( CLEND ) form the cluster of relatively 
//*>          close eigenalues. 
//*> \endverbatim 
//*> 
//*> \param[in,out] WGAP 
//*> \verbatim 
//*>          WGAP is REAL array, dimension 
//*>          dimension is >=  (CLEND-CLSTRT+1) 
//*>          The separation from the right neighbor eigenvalue in W. 
//*> \endverbatim 
//*> 
//*> \param[in] WERR 
//*> \verbatim 
//*>          WERR is REAL array, dimension 
//*>          dimension is >=  (CLEND-CLSTRT+1) 
//*>          WERR contain the semiwidth of the uncertainty 
//*>          interval of the corresponding eigenvalue APPROXIMATION in W 
//*> \endverbatim 
//*> 
//*> \param[in] SPDIAM 
//*> \verbatim 
//*>          SPDIAM is REAL 
//*>          estimate of the spectral diameter obtained from the 
//*>          Gerschgorin intervals 
//*> \endverbatim 
//*> 
//*> \param[in] CLGAPL 
//*> \verbatim 
//*>          CLGAPL is REAL 
//*> \endverbatim 
//*> 
//*> \param[in] CLGAPR 
//*> \verbatim 
//*>          CLGAPR is REAL 
//*>          absolute gap on each end of the cluster. 
//*>          Set by the calling routine to protect against shifts too close 
//*>          to eigenvalues outside the cluster. 
//*> \endverbatim 
//*> 
//*> \param[in] PIVMIN 
//*> \verbatim 
//*>          PIVMIN is REAL 
//*>          The minimum pivot allowed in the Sturm sequence. 
//*> \endverbatim 
//*> 
//*> \param[out] SIGMA 
//*> \verbatim 
//*>          SIGMA is REAL 
//*>          The shift used to form L(+) D(+) L(+)^T. 
//*> \endverbatim 
//*> 
//*> \param[out] DPLUS 
//*> \verbatim 
//*>          DPLUS is REAL array, dimension (N) 
//*>          The N diagonal elements of the diagonal matrix D(+). 
//*> \endverbatim 
//*> 
//*> \param[out] LPLUS 
//*> \verbatim 
//*>          LPLUS is REAL array, dimension (N-1) 
//*>          The first (N-1) elements of LPLUS contain the subdiagonal 
//*>          elements of the unit bidiagonal matrix L(+). 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is REAL array, dimension (2*N) 
//*>          Workspace. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          Signals processing OK (=0) or failure (=1) 
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
//*> \date June 2016 
//* 
//*> \ingroup OTHERauxiliary 
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

	 
	public static void _aqviol3d(ref Int32 _dxpq0xkr, Single* _plfm7z8g, Single* _68ec3gbh, Single* _3sgad2fi, ref Int32 _pkh05wgt, ref Int32 _gul8om2e, Single* _z1ioc3c8, Single* _fwsre11k, Single* _sgbqptwj, ref Single _1v6i5d3q, ref Single _0bklt0fv, ref Single _hiujezoo, ref Single _3aphllyg, ref Single _91a1vq5f, Single* _f8wr73cc, Single* _3b0drp01, Single* _apig8meb, ref Int32 _gro5yvfo)
	{
#region variable declarations
Single _mefveyyn =  8f;
Single _gsqd9l7n =  8f;
Single _kxg5drh2 =  1f;
Single _3ukqphnh =  0.25f;
Single _5m0mjfxm =  2f;
Boolean _15aq6bc5 =  default;
Boolean _tavaokl5 =  default;
Boolean _7n1tni88 =  default;
Boolean _eiiwuvee =  default;
Boolean _a4ehq062 =  default;
Boolean _sp6own3v =  default;
Int32 _b5p6od9s =  default;
Int32 _6cly3pl8 =  default;
Int32 _6q9ugdog =  default;
Int32 _r8b7lul1 =  (int)1;
Int32 _pwvyfhz6 =  (int)1;
Int32 _4vloph9c =  (int)2;
Int32 _e8rfnip7 =  default;
Single _ripqcbhm =  default;
Single _0tjg4w2z =  default;
Single _0dzv290j =  default;
Single _p1iqarg6 =  default;
Single _883mcfng =  default;
Single _hyh62j33 =  default;
Single _n8x583is =  default;
Single _f2pks3fr =  default;
Single _rgrei4ju =  default;
Single _gc8go57r =  default;
Single _j6h3l5ke =  default;
Single _0lgk0dxn =  default;
Single _s0ov1zvt =  default;
Single _zdz65cek =  default;
Single _57bwwmfw =  default;
Single _5mo6x93c =  default;
Single _puxxrlzt =  default;
Single _lydigojy =  default;
Single _h6p2cb1g =  default;
Single _v2zply50 =  default;
Single _jwqloq2t =  default;
Single _irk8i6qr =  default;
Single _d0z0tbao =  default;
Single _2qcyvkhx =  default;
Single _2n9t77j0 =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK auxiliary routine (version 3.7.1) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     June 2016 
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
		
		_gro5yvfo = (int)0;//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr <= (int)0)
		{
			
			return;
		}
		//* 
		
		_883mcfng = ILNumerics.F2NET.Intrinsics.REAL((int)2 );
		_p1iqarg6 = _d5tu038y("Precision" );
		_e8rfnip7 = (int)0;
		_tavaokl5 = false;// 
		// 
		//*     Note that we cannot guarantee that for any of the shifts tried, 
		//*     the factorization has a small or even moderate element growth. 
		//*     There could be Ritz values at both ends of the cluster and despite 
		//*     backing off, there are examples where all factorizations tried 
		//*     (in IEEE mode, allowing zero pivots & infinities) have INFINITE 
		//*     element growth. 
		//*     For this reason, we should use PIVMIN in this subroutine so that at 
		//*     least the L D L^T factorization exists. It can be checked afterwards 
		//*     whether the element growth caused bad residuals/orthogonality. 
		// 
		//*     Decide whether the code should accept the best among all 
		//*     representations despite large element growth or signal INFO=1 
		//*     Setting NOFAIL to .FALSE. for quick fix for bug 113 
		
		_7n1tni88 = false;//* 
		// 
		//*     Compute the average gap length of the cluster 
		
		_0dzv290j = ((ILNumerics.F2NET.Intrinsics.ABS(*(_z1ioc3c8+(_gul8om2e - 1)) - *(_z1ioc3c8+(_pkh05wgt - 1)) ) + *(_sgbqptwj+(_gul8om2e - 1))) + *(_sgbqptwj+(_pkh05wgt - 1)));
		_ripqcbhm = (_0dzv290j / ILNumerics.F2NET.Intrinsics.REAL(_gul8om2e - _pkh05wgt ));
		_zdz65cek = ILNumerics.F2NET.Intrinsics.MIN(_0bklt0fv ,_hiujezoo );//*     Initial values for shifts to both ends of cluster 
		
		_j6h3l5ke = (ILNumerics.F2NET.Intrinsics.MIN(*(_z1ioc3c8+(_pkh05wgt - 1)) ,*(_z1ioc3c8+(_gul8om2e - 1)) ) - *(_sgbqptwj+(_pkh05wgt - 1)));
		_jwqloq2t = (ILNumerics.F2NET.Intrinsics.MAX(*(_z1ioc3c8+(_pkh05wgt - 1)) ,*(_z1ioc3c8+(_gul8om2e - 1)) ) + *(_sgbqptwj+(_gul8om2e - 1)));// 
		//*     Use a small fudge to make sure that we really shift to the outside 
		
		_j6h3l5ke = (_j6h3l5ke - ((ILNumerics.F2NET.Intrinsics.ABS(_j6h3l5ke ) * _5m0mjfxm) * _p1iqarg6));
		_jwqloq2t = (_jwqloq2t + ((ILNumerics.F2NET.Intrinsics.ABS(_jwqloq2t ) * _5m0mjfxm) * _p1iqarg6));// 
		//*     Compute upper bounds for how much to back off the initial shifts 
		
		_gc8go57r = ((_3ukqphnh * _zdz65cek) + (_5m0mjfxm * _3aphllyg));
		_lydigojy = ((_3ukqphnh * _zdz65cek) + (_5m0mjfxm * _3aphllyg));// 
		
		_rgrei4ju = (ILNumerics.F2NET.Intrinsics.MAX(_ripqcbhm ,*(_fwsre11k+(_pkh05wgt - 1)) ) / _883mcfng);
		_puxxrlzt = (ILNumerics.F2NET.Intrinsics.MAX(_ripqcbhm ,*(_fwsre11k+(_gul8om2e - (int)1 - 1)) ) / _883mcfng);//* 
		//*     Initialize the record of the best representation found 
		//* 
		
		_irk8i6qr = _d5tu038y("S" );
		_d0z0tbao = (_kxg5drh2 / _irk8i6qr);
		_hyh62j33 = ((ILNumerics.F2NET.Intrinsics.REAL(_dxpq0xkr - (int)1 ) * _zdz65cek) / (_1v6i5d3q * _p1iqarg6));
		_n8x583is = ((ILNumerics.F2NET.Intrinsics.REAL(_dxpq0xkr - (int)1 ) * _zdz65cek) / (_1v6i5d3q * ILNumerics.F2NET.Intrinsics.SQRT(_p1iqarg6 )));
		_0tjg4w2z = _j6h3l5ke;//* 
		//*     while (KTRY <= KTRYMAX) 
		
		_6q9ugdog = (int)0;
		_f2pks3fr = (_mefveyyn * _1v6i5d3q);// 
		
Mark5:;
		// continue
		_eiiwuvee = false;
		_a4ehq062 = false;//*     Ensure that we do not back off too much of the initial shifts 
		
		_rgrei4ju = ILNumerics.F2NET.Intrinsics.MIN(_gc8go57r ,_rgrei4ju );
		_puxxrlzt = ILNumerics.F2NET.Intrinsics.MIN(_lydigojy ,_puxxrlzt );// 
		//*     Compute the element growth when shifting to both ends of the cluster 
		//*     accept the shift if there is no element growth at one of the two ends 
		// 
		//*     Left end 
		
		_irk8i6qr = (-(_j6h3l5ke));
		*(_f8wr73cc+((int)1 - 1)) = (*(_plfm7z8g+((int)1 - 1)) + _irk8i6qr);
		if (ILNumerics.F2NET.Intrinsics.ABS(*(_f8wr73cc+((int)1 - 1)) ) < _3aphllyg)
		{
			
			*(_f8wr73cc+((int)1 - 1)) = (-(_3aphllyg));//*        Need to set SAWNAN1 because refined RRR test should not be used 
			//*        in this case 
			
			_eiiwuvee = true;
		}
		
		_0lgk0dxn = ILNumerics.F2NET.Intrinsics.ABS(*(_f8wr73cc+((int)1 - 1)) );
		{
			System.Int32 __81fgg2dlsvn9 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step9 = (System.Int32)((int)1);
			System.Int32 __81fgg2count9;
			for (__81fgg2count9 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn9 + __81fgg2step9) / __81fgg2step9)), _b5p6od9s = __81fgg2dlsvn9; __81fgg2count9 != 0; __81fgg2count9--, _b5p6od9s += (__81fgg2step9)) {

			{
				
				*(_3b0drp01+(_b5p6od9s - 1)) = (*(_3sgad2fi+(_b5p6od9s - 1)) / *(_f8wr73cc+(_b5p6od9s - 1)));
				_irk8i6qr = (((_irk8i6qr * *(_3b0drp01+(_b5p6od9s - 1))) * *(_68ec3gbh+(_b5p6od9s - 1))) - _j6h3l5ke);
				*(_f8wr73cc+(_b5p6od9s + (int)1 - 1)) = (*(_plfm7z8g+(_b5p6od9s + (int)1 - 1)) + _irk8i6qr);
				if (ILNumerics.F2NET.Intrinsics.ABS(*(_f8wr73cc+(_b5p6od9s + (int)1 - 1)) ) < _3aphllyg)
				{
					
					*(_f8wr73cc+(_b5p6od9s + (int)1 - 1)) = (-(_3aphllyg));//*           Need to set SAWNAN1 because refined RRR test should not be used 
					//*           in this case 
					
					_eiiwuvee = true;
				}
				
				_0lgk0dxn = ILNumerics.F2NET.Intrinsics.MAX(_0lgk0dxn ,ILNumerics.F2NET.Intrinsics.ABS(*(_f8wr73cc+(_b5p6od9s + (int)1 - 1)) ) );
Mark6:;
				// continue
			}
						}		}
		_eiiwuvee = (_eiiwuvee | _lilv8egi(ref _0lgk0dxn ));// 
		
		if (_tavaokl5 | ((_0lgk0dxn <= _f2pks3fr) & (!(_eiiwuvee))))
		{
			
			_91a1vq5f = _j6h3l5ke;
			_e8rfnip7 = _pwvyfhz6;goto Mark100;
		}
		// 
		//*     Right end 
		
		_irk8i6qr = (-(_jwqloq2t));
		*(_apig8meb+((int)1 - 1)) = (*(_plfm7z8g+((int)1 - 1)) + _irk8i6qr);
		if (ILNumerics.F2NET.Intrinsics.ABS(*(_apig8meb+((int)1 - 1)) ) < _3aphllyg)
		{
			
			*(_apig8meb+((int)1 - 1)) = (-(_3aphllyg));//*        Need to set SAWNAN2 because refined RRR test should not be used 
			//*        in this case 
			
			_a4ehq062 = true;
		}
		
		_s0ov1zvt = ILNumerics.F2NET.Intrinsics.ABS(*(_apig8meb+((int)1 - 1)) );
		{
			System.Int32 __81fgg2dlsvn10 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step10 = (System.Int32)((int)1);
			System.Int32 __81fgg2count10;
			for (__81fgg2count10 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn10 + __81fgg2step10) / __81fgg2step10)), _b5p6od9s = __81fgg2dlsvn10; __81fgg2count10 != 0; __81fgg2count10--, _b5p6od9s += (__81fgg2step10)) {

			{
				
				*(_apig8meb+(_dxpq0xkr + _b5p6od9s - 1)) = (*(_3sgad2fi+(_b5p6od9s - 1)) / *(_apig8meb+(_b5p6od9s - 1)));
				_irk8i6qr = (((_irk8i6qr * *(_apig8meb+(_dxpq0xkr + _b5p6od9s - 1))) * *(_68ec3gbh+(_b5p6od9s - 1))) - _jwqloq2t);
				*(_apig8meb+(_b5p6od9s + (int)1 - 1)) = (*(_plfm7z8g+(_b5p6od9s + (int)1 - 1)) + _irk8i6qr);
				if (ILNumerics.F2NET.Intrinsics.ABS(*(_apig8meb+(_b5p6od9s + (int)1 - 1)) ) < _3aphllyg)
				{
					
					*(_apig8meb+(_b5p6od9s + (int)1 - 1)) = (-(_3aphllyg));//*           Need to set SAWNAN2 because refined RRR test should not be used 
					//*           in this case 
					
					_a4ehq062 = true;
				}
				
				_s0ov1zvt = ILNumerics.F2NET.Intrinsics.MAX(_s0ov1zvt ,ILNumerics.F2NET.Intrinsics.ABS(*(_apig8meb+(_b5p6od9s + (int)1 - 1)) ) );
Mark7:;
				// continue
			}
						}		}
		_a4ehq062 = (_a4ehq062 | _lilv8egi(ref _s0ov1zvt ));// 
		
		if (_tavaokl5 | ((_s0ov1zvt <= _f2pks3fr) & (!(_a4ehq062))))
		{
			
			_91a1vq5f = _jwqloq2t;
			_e8rfnip7 = _4vloph9c;goto Mark100;
		}
		//*     If we are at this point, both shifts led to too much element growth 
		// 
		//*     Record the better of the two shifts (provided it didn't lead to NaN) 
		
		if (_eiiwuvee & _a4ehq062)
		{
			//*        both MAX1 and MAX2 are NaN 
			goto Mark50;
		}
		else
		{
			
			if (!(_eiiwuvee))
			{
				
				_6cly3pl8 = (int)1;
				if (_0lgk0dxn <= _d0z0tbao)
				{
					
					_d0z0tbao = _0lgk0dxn;
					_0tjg4w2z = _j6h3l5ke;
				}
				
			}
			
			if (!(_a4ehq062))
			{
				
				if (_eiiwuvee | (_s0ov1zvt <= _0lgk0dxn))
				_6cly3pl8 = (int)2;
				if (_s0ov1zvt <= _d0z0tbao)
				{
					
					_d0z0tbao = _s0ov1zvt;
					_0tjg4w2z = _jwqloq2t;
				}
				
			}
			
		}
		// 
		//*     If we are here, both the left and the right shift led to 
		//*     element growth. If the element growth is moderate, then 
		//*     we may still accept the representation, if it passes a 
		//*     refined test for RRR. This test supposes that no NaN occurred. 
		//*     Moreover, we use the refined RRR test only for isolated clusters. 
		
		if ((((_0dzv290j < (_zdz65cek / ILNumerics.F2NET.Intrinsics.REAL((int)128 ))) & (ILNumerics.F2NET.Intrinsics.MIN(_0lgk0dxn ,_s0ov1zvt ) < _n8x583is)) & (!(_eiiwuvee))) & (!(_a4ehq062)))
		{
			
			_15aq6bc5 = true;
		}
		else
		{
			
			_15aq6bc5 = false;
		}
		
		_sp6own3v = true;
		if (_sp6own3v & _15aq6bc5)
		{
			
			if (_6cly3pl8 == (int)1)
			{
				
				_2qcyvkhx = ILNumerics.F2NET.Intrinsics.ABS(*(_f8wr73cc+(_dxpq0xkr - 1)) );
				_2n9t77j0 = _kxg5drh2;
				_5mo6x93c = _kxg5drh2;
				_57bwwmfw = _kxg5drh2;
				{
					System.Int32 __81fgg2dlsvn11 = (System.Int32)((_dxpq0xkr - (int)1));
					System.Int32 __81fgg2step11 = (System.Int32)((int)-1);
					System.Int32 __81fgg2count11;
					for (__81fgg2count11 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn11 + __81fgg2step11) / __81fgg2step11)), _b5p6od9s = __81fgg2dlsvn11; __81fgg2count11 != 0; __81fgg2count11--, _b5p6od9s += (__81fgg2step11)) {

					{
						
						if (_5mo6x93c <= _p1iqarg6)
						{
							
							_5mo6x93c = (((*(_f8wr73cc+(_b5p6od9s + (int)1 - 1)) * *(_apig8meb+((_dxpq0xkr + _b5p6od9s) + (int)1 - 1))) / (*(_f8wr73cc+(_b5p6od9s - 1)) * *(_apig8meb+(_dxpq0xkr + _b5p6od9s - 1)))) * _57bwwmfw);
						}
						else
						{
							
							_5mo6x93c = (_5mo6x93c * ILNumerics.F2NET.Intrinsics.ABS(*(_apig8meb+(_dxpq0xkr + _b5p6od9s - 1)) ));
						}
						
						_57bwwmfw = _5mo6x93c;
						_2n9t77j0 = (_2n9t77j0 + __POW2(_5mo6x93c));
						_2qcyvkhx = ILNumerics.F2NET.Intrinsics.MAX(_2qcyvkhx ,ILNumerics.F2NET.Intrinsics.ABS(*(_f8wr73cc+(_b5p6od9s - 1)) * _5mo6x93c ) );
Mark15:;
						// continue
					}
										}				}
				_h6p2cb1g = (_2qcyvkhx / (_1v6i5d3q * ILNumerics.F2NET.Intrinsics.SQRT(_2n9t77j0 )));
				if (_h6p2cb1g <= _gsqd9l7n)
				{
					
					_91a1vq5f = _j6h3l5ke;
					_e8rfnip7 = _pwvyfhz6;goto Mark100;
				}
				
			}
			else
			if (_6cly3pl8 == (int)2)
			{
				
				_2qcyvkhx = ILNumerics.F2NET.Intrinsics.ABS(*(_apig8meb+(_dxpq0xkr - 1)) );
				_2n9t77j0 = _kxg5drh2;
				_5mo6x93c = _kxg5drh2;
				_57bwwmfw = _kxg5drh2;
				{
					System.Int32 __81fgg2dlsvn12 = (System.Int32)((_dxpq0xkr - (int)1));
					System.Int32 __81fgg2step12 = (System.Int32)((int)-1);
					System.Int32 __81fgg2count12;
					for (__81fgg2count12 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn12 + __81fgg2step12) / __81fgg2step12)), _b5p6od9s = __81fgg2dlsvn12; __81fgg2count12 != 0; __81fgg2count12--, _b5p6od9s += (__81fgg2step12)) {

					{
						
						if (_5mo6x93c <= _p1iqarg6)
						{
							
							_5mo6x93c = (((*(_apig8meb+(_b5p6od9s + (int)1 - 1)) * *(_3b0drp01+(_b5p6od9s + (int)1 - 1))) / (*(_apig8meb+(_b5p6od9s - 1)) * *(_3b0drp01+(_b5p6od9s - 1)))) * _57bwwmfw);
						}
						else
						{
							
							_5mo6x93c = (_5mo6x93c * ILNumerics.F2NET.Intrinsics.ABS(*(_3b0drp01+(_b5p6od9s - 1)) ));
						}
						
						_57bwwmfw = _5mo6x93c;
						_2n9t77j0 = (_2n9t77j0 + __POW2(_5mo6x93c));
						_2qcyvkhx = ILNumerics.F2NET.Intrinsics.MAX(_2qcyvkhx ,ILNumerics.F2NET.Intrinsics.ABS(*(_apig8meb+(_b5p6od9s - 1)) * _5mo6x93c ) );
Mark16:;
						// continue
					}
										}				}
				_v2zply50 = (_2qcyvkhx / (_1v6i5d3q * ILNumerics.F2NET.Intrinsics.SQRT(_2n9t77j0 )));
				if (_v2zply50 <= _gsqd9l7n)
				{
					
					_91a1vq5f = _jwqloq2t;
					_e8rfnip7 = _4vloph9c;goto Mark100;
				}
				
			}
			
		}
		// 
		
Mark50:;
		// continue// 
		
		if (_6q9ugdog < _r8b7lul1)
		{
			//*        If we are here, both shifts failed also the RRR test. 
			//*        Back off to the outside 
			
			_j6h3l5ke = ILNumerics.F2NET.Intrinsics.MAX(_j6h3l5ke - _rgrei4ju ,_j6h3l5ke - _gc8go57r );
			_jwqloq2t = ILNumerics.F2NET.Intrinsics.MIN(_jwqloq2t + _puxxrlzt ,_jwqloq2t + _lydigojy );
			_rgrei4ju = (_5m0mjfxm * _rgrei4ju);
			_puxxrlzt = (_5m0mjfxm * _puxxrlzt);
			_6q9ugdog = (_6q9ugdog + (int)1);goto Mark5;
		}
		else
		{
			//*        None of the representations investigated satisfied our 
			//*        criteria. Take the best one we found. 
			
			if ((_d0z0tbao < _hyh62j33) | _7n1tni88)
			{
				
				_j6h3l5ke = _0tjg4w2z;
				_jwqloq2t = _0tjg4w2z;
				_tavaokl5 = true;goto Mark5;
			}
			else
			{
				
				_gro5yvfo = (int)1;
				return;
			}
			
		}
		// 
		
Mark100:;
		// continue
		if (_e8rfnip7 == _pwvyfhz6)
		{
			
		}
		else
		if (_e8rfnip7 == _4vloph9c)
		{
			//*        store new L and D back into DPLUS, LPLUS 
			
			_wcs7ne88(ref _dxpq0xkr ,_apig8meb ,ref Unsafe.AsRef((int)1) ,_f8wr73cc ,ref Unsafe.AsRef((int)1) );
			_wcs7ne88(ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,(_apig8meb+(_dxpq0xkr + (int)1 - 1)),ref Unsafe.AsRef((int)1) ,_3b0drp01 ,ref Unsafe.AsRef((int)1) );
		}
		// 
		
		return;//* 
		//*     End of SLARRF 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
