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
//*> \brief \b SLARRK computes one eigenvalue of a symmetric tridiagonal matrix T to suitable accuracy. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLARRK + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slarrk.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slarrk.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slarrk.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLARRK( N, IW, GL, GU, 
//*                           D, E2, PIVMIN, RELTOL, W, WERR, INFO) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER   INFO, IW, N 
//*       REAL                PIVMIN, RELTOL, GL, GU, W, WERR 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               D( * ), E2( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SLARRK computes one eigenvalue of a symmetric tridiagonal 
//*> matrix T to suitable accuracy. This is an auxiliary code to be 
//*> called from SSTEMR. 
//*> 
//*> To avoid overflow, the matrix must be scaled so that its 
//*> largest element is no greater than overflow**(1/2) * underflow**(1/4) in absolute value, and for greatest 
//*> accuracy, it should not be much smaller than that. 
//*> 
//*> See W. Kahan "Accurate Eigenvalues of a Symmetric Tridiagonal 
//*> Matrix", Report CS41, Computer Science Dept., Stanford 
//*> University, July 21, 1966. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the tridiagonal matrix T.  N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] IW 
//*> \verbatim 
//*>          IW is INTEGER 
//*>          The index of the eigenvalues to be returned. 
//*> \endverbatim 
//*> 
//*> \param[in] GL 
//*> \verbatim 
//*>          GL is REAL 
//*> \endverbatim 
//*> 
//*> \param[in] GU 
//*> \verbatim 
//*>          GU is REAL 
//*>          An upper and a lower bound on the eigenvalue. 
//*> \endverbatim 
//*> 
//*> \param[in] D 
//*> \verbatim 
//*>          D is REAL array, dimension (N) 
//*>          The n diagonal elements of the tridiagonal matrix T. 
//*> \endverbatim 
//*> 
//*> \param[in] E2 
//*> \verbatim 
//*>          E2 is REAL array, dimension (N-1) 
//*>          The (n-1) squared off-diagonal elements of the tridiagonal matrix T. 
//*> \endverbatim 
//*> 
//*> \param[in] PIVMIN 
//*> \verbatim 
//*>          PIVMIN is REAL 
//*>          The minimum pivot allowed in the Sturm sequence for T. 
//*> \endverbatim 
//*> 
//*> \param[in] RELTOL 
//*> \verbatim 
//*>          RELTOL is REAL 
//*>          The minimum relative width of an interval.  When an interval 
//*>          is narrower than RELTOL times the larger (in 
//*>          magnitude) endpoint, then it is considered to be 
//*>          sufficiently small, i.e., converged.  Note: this should 
//*>          always be at least radix*machine epsilon. 
//*> \endverbatim 
//*> 
//*> \param[out] W 
//*> \verbatim 
//*>          W is REAL 
//*> \endverbatim 
//*> 
//*> \param[out] WERR 
//*> \verbatim 
//*>          WERR is REAL 
//*>          The error bound on the corresponding eigenvalue approximation 
//*>          in W. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:       Eigenvalue converged 
//*>          = -1:      Eigenvalue did NOT converge 
//*> \endverbatim 
//* 
//*> \par Internal Parameters: 
//*  ========================= 
//*> 
//*> \verbatim 
//*>  FUDGE   REAL            , default = 2 
//*>          A "fudge factor" to widen the Gershgorin intervals. 
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
//*  ===================================================================== 

	 
	public static void _qg0gy92s(ref Int32 _dxpq0xkr, ref Int32 _11qhqs00, ref Single _lr8ennxn, ref Single _jf74kq7y, Single* _plfm7z8g, Single* _0maek8rz, ref Single _3aphllyg, ref Single _brq3wv6n, ref Single _z1ioc3c8, ref Single _sgbqptwj, ref Int32 _gro5yvfo)
	{
#region variable declarations
Single _5m0mjfxm =  2f;
Single _gbf4169i =  0.5f;
Single _7fnb0l4r =  _5m0mjfxm;
Single _d0547bi2 =  0f;
Int32 _b5p6od9s =  default;
Int32 _acwgen0p =  default;
Int32 _7u74ue5o =  default;
Int32 _bfbhh57k =  default;
Single _jmec0f4x =  default;
Single _p1iqarg6 =  default;
Single _pvwxvshr =  default;
Single _kdbjkqmm =  default;
Single _ruhusobv =  default;
Single _eki85d4y =  default;
Single _c0o9kuh7 =  default;
Single _ww3bdyup =  default;
Single _6t1khtu3 =  default;
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
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr <= (int)0)
		{
			
			_gro5yvfo = (int)0;
			return;
		}
		//* 
		//*     Get machine constants 
		
		_p1iqarg6 = _d5tu038y("P" );// 
		
		_6t1khtu3 = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_lr8ennxn ) ,ILNumerics.F2NET.Intrinsics.ABS(_jf74kq7y ) );
		_eki85d4y = _brq3wv6n;
		_jmec0f4x = ((_7fnb0l4r * _5m0mjfxm) * _3aphllyg);// 
		
		_7u74ue5o = (ILNumerics.F2NET.Intrinsics.INT((ILNumerics.F2NET.Intrinsics.LOG(_6t1khtu3 + _3aphllyg ) - ILNumerics.F2NET.Intrinsics.LOG(_3aphllyg )) / ILNumerics.F2NET.Intrinsics.LOG(_5m0mjfxm ) ) + (int)2);// 
		
		_gro5yvfo = (int)-1;// 
		
		_pvwxvshr = ((_lr8ennxn - (((_7fnb0l4r * _6t1khtu3) * _p1iqarg6) * _dxpq0xkr)) - ((_7fnb0l4r * _5m0mjfxm) * _3aphllyg));
		_ruhusobv = ((_jf74kq7y + (((_7fnb0l4r * _6t1khtu3) * _p1iqarg6) * _dxpq0xkr)) + ((_7fnb0l4r * _5m0mjfxm) * _3aphllyg));
		_acwgen0p = (int)0;// 
		
Mark10:;
		// continue//* 
		//*     Check if interval converged or maximum number of iterations reached 
		//* 
		
		_c0o9kuh7 = ILNumerics.F2NET.Intrinsics.ABS(_ruhusobv - _pvwxvshr );
		_ww3bdyup = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_ruhusobv ) ,ILNumerics.F2NET.Intrinsics.ABS(_pvwxvshr ) );
		if (_c0o9kuh7 < ILNumerics.F2NET.Intrinsics.MAX(_jmec0f4x ,_3aphllyg ,_eki85d4y * _ww3bdyup ))
		{
			
			_gro5yvfo = (int)0;goto Mark30;
		}
		
		if (_acwgen0p > _7u74ue5o)goto Mark30;// 
		//* 
		//*     Count number of negative pivots for mid-point 
		//* 
		
		_acwgen0p = (_acwgen0p + (int)1);
		_kdbjkqmm = (_gbf4169i * (_pvwxvshr + _ruhusobv));
		_bfbhh57k = (int)0;
		_c0o9kuh7 = (*(_plfm7z8g+((int)1 - 1)) - _kdbjkqmm);
		if (ILNumerics.F2NET.Intrinsics.ABS(_c0o9kuh7 ) < _3aphllyg)
		_c0o9kuh7 = (-(_3aphllyg));
		if (_c0o9kuh7 <= _d0547bi2)
		_bfbhh57k = (_bfbhh57k + (int)1);//* 
		
		{
			System.Int32 __81fgg2dlsvn3384 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step3384 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3384;
			for (__81fgg2count3384 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3384 + __81fgg2step3384) / __81fgg2step3384)), _b5p6od9s = __81fgg2dlsvn3384; __81fgg2count3384 != 0; __81fgg2count3384--, _b5p6od9s += (__81fgg2step3384)) {

			{
				
				_c0o9kuh7 = ((*(_plfm7z8g+(_b5p6od9s - 1)) - (*(_0maek8rz+(_b5p6od9s - (int)1 - 1)) / _c0o9kuh7)) - _kdbjkqmm);
				if (ILNumerics.F2NET.Intrinsics.ABS(_c0o9kuh7 ) < _3aphllyg)
				_c0o9kuh7 = (-(_3aphllyg));
				if (_c0o9kuh7 <= _d0547bi2)
				_bfbhh57k = (_bfbhh57k + (int)1);
Mark20:;
				// continue
			}
						}		}// 
		
		if (_bfbhh57k >= _11qhqs00)
		{
			
			_ruhusobv = _kdbjkqmm;
		}
		else
		{
			
			_pvwxvshr = _kdbjkqmm;
		}
		goto Mark10;// 
		
Mark30:;
		// continue//* 
		//*     Converged or maximum number of iterations reached 
		//* 
		
		_z1ioc3c8 = (_gbf4169i * (_pvwxvshr + _ruhusobv));
		_sgbqptwj = (_gbf4169i * ILNumerics.F2NET.Intrinsics.ABS(_ruhusobv - _pvwxvshr ));// 
		
		return;//* 
		//*     End of SLARRK 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
