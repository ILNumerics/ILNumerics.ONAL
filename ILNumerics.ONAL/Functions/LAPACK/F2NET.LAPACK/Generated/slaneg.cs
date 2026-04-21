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
//*> \brief \b SLANEG computes the Sturm count. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLANEG + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slaneg.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slaneg.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slaneg.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       INTEGER FUNCTION SLANEG( N, D, LLD, SIGMA, PIVMIN, R ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            N, R 
//*       REAL               PIVMIN, SIGMA 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               D( * ), LLD( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SLANEG computes the Sturm count, the number of negative pivots 
//*> encountered while factoring tridiagonal T - sigma I = L D L^T. 
//*> This implementation works directly on the factors without forming 
//*> the tridiagonal matrix T.  The Sturm count is also the number of 
//*> eigenvalues of T less than sigma. 
//*> 
//*> This routine is called from SLARRB. 
//*> 
//*> The current routine does not use the PIVMIN parameter but rather 
//*> requires IEEE-754 propagation of Infinities and NaNs.  This 
//*> routine also has no input range restrictions but does require 
//*> default exception handling such that x/0 produces Inf when x is 
//*> non-zero, and Inf/Inf produces NaN.  For more information, see: 
//*> 
//*>   Marques, Riedy, and Voemel, "Benefits of IEEE-754 Features in 
//*>   Modern Symmetric Tridiagonal Eigensolvers," SIAM Journal on 
//*>   Scientific Computing, v28, n5, 2006.  DOI 10.1137/050641624 
//*>   (Tech report version in LAWN 172 with the same title.) 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix. 
//*> \endverbatim 
//*> 
//*> \param[in] D 
//*> \verbatim 
//*>          D is REAL array, dimension (N) 
//*>          The N diagonal elements of the diagonal matrix D. 
//*> \endverbatim 
//*> 
//*> \param[in] LLD 
//*> \verbatim 
//*>          LLD is REAL array, dimension (N-1) 
//*>          The (N-1) elements L(i)*L(i)*D(i). 
//*> \endverbatim 
//*> 
//*> \param[in] SIGMA 
//*> \verbatim 
//*>          SIGMA is REAL 
//*>          Shift amount in T - sigma I = L D L^T. 
//*> \endverbatim 
//*> 
//*> \param[in] PIVMIN 
//*> \verbatim 
//*>          PIVMIN is REAL 
//*>          The minimum pivot in the Sturm sequence.  May be used 
//*>          when zero pivots are encountered on non-IEEE-754 
//*>          architectures. 
//*> \endverbatim 
//*> 
//*> \param[in] R 
//*> \verbatim 
//*>          R is INTEGER 
//*>          The twist index for the twisted factorization that is used 
//*>          for the negcount. 
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
//*>     Osni Marques, LBNL/NERSC, USA \n 
//*>     Christof Voemel, University of California, Berkeley, USA \n 
//*>     Jason Riedy, University of California, Berkeley, USA \n 
//*> 
//*  ===================================================================== 

	 
	public static Int32 _escttm7a(ref Int32 _dxpq0xkr, Single* _plfm7z8g, Single* _4sixt94s, ref Single _91a1vq5f, ref Single _3aphllyg, ref Int32 _q2vwp05i)
	{
#region variable declarations
Int32 _escttm7a = default;
Single _d0547bi2 =  0f;
Single _kxg5drh2 =  1f;
Int32 _63l3avcc =  (int)128;
Int32 _ll1zj14p =  default;
Int32 _znpjgsef =  default;
Int32 _5cvwo7gs =  default;
Int32 _dvyydhuh =  default;
Int32 _bfbhh57k =  default;
Single _yx9kloyq =  default;
Single _poefthpn =  default;
Single _f8wr73cc =  default;
Single _zf88apxo =  default;
Single _ejwydfmr =  default;
Single _2ivtt43r =  default;
Single _2qcyvkhx =  default;
Boolean _neha2b96 =  default;
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
		//*     Some architectures propagate Infinities and NaNs very slowly, so 
		//*     the code computes counts in BLKLEN chunks.  Then a NaN can 
		//*     propagate at most BLKLEN columns before being detected.  This is 
		//*     not a general tuning parameter; it needs only to be just large 
		//*     enough that the overhead is tiny in common cases. 
		//*     .. 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		// 
		
		_bfbhh57k = (int)0;// 
		//*     I) upper part: L D L^T - SIGMA I = L+ D+ L+^T 
		
		_2ivtt43r = (-(_91a1vq5f));
		{
			System.Int32 __81fgg2dlsvn3363 = (System.Int32)((int)1);
			System.Int32 __81fgg2step3363 = (System.Int32)(_63l3avcc);
			System.Int32 __81fgg2count3363;
			for (__81fgg2count3363 = System.Math.Max(0, (System.Int32)(((System.Int32)(_q2vwp05i - (int)1) - __81fgg2dlsvn3363 + __81fgg2step3363) / __81fgg2step3363)), _ll1zj14p = __81fgg2dlsvn3363; __81fgg2count3363 != 0; __81fgg2count3363--, _ll1zj14p += (__81fgg2step3363)) {

			{
				
				_5cvwo7gs = (int)0;
				_yx9kloyq = _2ivtt43r;
				{
					System.Int32 __81fgg2dlsvn3364 = (System.Int32)(_ll1zj14p);
					const System.Int32 __81fgg2step3364 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3364;
					for (__81fgg2count3364 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MIN((_ll1zj14p + _63l3avcc) - (int)1 ,_q2vwp05i - (int)1 )) - __81fgg2dlsvn3364 + __81fgg2step3364) / __81fgg2step3364)), _znpjgsef = __81fgg2dlsvn3364; __81fgg2count3364 != 0; __81fgg2count3364--, _znpjgsef += (__81fgg2step3364)) {

					{
						
						_f8wr73cc = (*(_plfm7z8g+(_znpjgsef - 1)) + _2ivtt43r);
						if (_f8wr73cc < _d0547bi2)
						_5cvwo7gs = (_5cvwo7gs + (int)1);
						_2qcyvkhx = (_2ivtt43r / _f8wr73cc);
						_2ivtt43r = ((_2qcyvkhx * *(_4sixt94s+(_znpjgsef - 1))) - _91a1vq5f);
Mark21:;
						// continue
					}
										}				}
				_neha2b96 = _lilv8egi(ref _2ivtt43r );//*     Run a slower version of the above loop if a NaN is detected. 
				//*     A NaN should occur only with a zero pivot after an infinite 
				//*     pivot.  In that case, substituting 1 for T/DPLUS is the 
				//*     correct limit. 
				
				if (_neha2b96)
				{
					
					_5cvwo7gs = (int)0;
					_2ivtt43r = _yx9kloyq;
					{
						System.Int32 __81fgg2dlsvn3365 = (System.Int32)(_ll1zj14p);
						const System.Int32 __81fgg2step3365 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3365;
						for (__81fgg2count3365 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MIN((_ll1zj14p + _63l3avcc) - (int)1 ,_q2vwp05i - (int)1 )) - __81fgg2dlsvn3365 + __81fgg2step3365) / __81fgg2step3365)), _znpjgsef = __81fgg2dlsvn3365; __81fgg2count3365 != 0; __81fgg2count3365--, _znpjgsef += (__81fgg2step3365)) {

						{
							
							_f8wr73cc = (*(_plfm7z8g+(_znpjgsef - 1)) + _2ivtt43r);
							if (_f8wr73cc < _d0547bi2)
							_5cvwo7gs = (_5cvwo7gs + (int)1);
							_2qcyvkhx = (_2ivtt43r / _f8wr73cc);
							if (_lilv8egi(ref _2qcyvkhx ))
							_2qcyvkhx = _kxg5drh2;
							_2ivtt43r = ((_2qcyvkhx * *(_4sixt94s+(_znpjgsef - 1))) - _91a1vq5f);
Mark22:;
							// continue
						}
												}					}
				}
				
				_bfbhh57k = (_bfbhh57k + _5cvwo7gs);
Mark210:;
				// continue
			}
						}		}//* 
		//*     II) lower part: L D L^T - SIGMA I = U- D- U-^T 
		
		_ejwydfmr = (*(_plfm7z8g+(_dxpq0xkr - 1)) - _91a1vq5f);
		{
			System.Int32 __81fgg2dlsvn3366 = (System.Int32)((_dxpq0xkr - (int)1));
			System.Int32 __81fgg2step3366 = (System.Int32)(-(_63l3avcc));
			System.Int32 __81fgg2count3366;
			for (__81fgg2count3366 = System.Math.Max(0, (System.Int32)(((System.Int32)(_q2vwp05i) - __81fgg2dlsvn3366 + __81fgg2step3366) / __81fgg2step3366)), _ll1zj14p = __81fgg2dlsvn3366; __81fgg2count3366 != 0; __81fgg2count3366--, _ll1zj14p += (__81fgg2step3366)) {

			{
				
				_dvyydhuh = (int)0;
				_yx9kloyq = _ejwydfmr;
				{
					System.Int32 __81fgg2dlsvn3367 = (System.Int32)(_ll1zj14p);
					System.Int32 __81fgg2step3367 = (System.Int32)((int)-1);
					System.Int32 __81fgg2count3367;
					for (__81fgg2count3367 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MAX((_ll1zj14p - _63l3avcc) + (int)1 ,_q2vwp05i )) - __81fgg2dlsvn3367 + __81fgg2step3367) / __81fgg2step3367)), _znpjgsef = __81fgg2dlsvn3367; __81fgg2count3367 != 0; __81fgg2count3367--, _znpjgsef += (__81fgg2step3367)) {

					{
						
						_poefthpn = (*(_4sixt94s+(_znpjgsef - 1)) + _ejwydfmr);
						if (_poefthpn < _d0547bi2)
						_dvyydhuh = (_dvyydhuh + (int)1);
						_2qcyvkhx = (_ejwydfmr / _poefthpn);
						_ejwydfmr = ((_2qcyvkhx * *(_plfm7z8g+(_znpjgsef - 1))) - _91a1vq5f);
Mark23:;
						// continue
					}
										}				}
				_neha2b96 = _lilv8egi(ref _ejwydfmr );//*     As above, run a slower version that substitutes 1 for Inf/Inf. 
				//* 
				
				if (_neha2b96)
				{
					
					_dvyydhuh = (int)0;
					_ejwydfmr = _yx9kloyq;
					{
						System.Int32 __81fgg2dlsvn3368 = (System.Int32)(_ll1zj14p);
						System.Int32 __81fgg2step3368 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count3368;
						for (__81fgg2count3368 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MAX((_ll1zj14p - _63l3avcc) + (int)1 ,_q2vwp05i )) - __81fgg2dlsvn3368 + __81fgg2step3368) / __81fgg2step3368)), _znpjgsef = __81fgg2dlsvn3368; __81fgg2count3368 != 0; __81fgg2count3368--, _znpjgsef += (__81fgg2step3368)) {

						{
							
							_poefthpn = (*(_4sixt94s+(_znpjgsef - 1)) + _ejwydfmr);
							if (_poefthpn < _d0547bi2)
							_dvyydhuh = (_dvyydhuh + (int)1);
							_2qcyvkhx = (_ejwydfmr / _poefthpn);
							if (_lilv8egi(ref _2qcyvkhx ))
							_2qcyvkhx = _kxg5drh2;
							_ejwydfmr = ((_2qcyvkhx * *(_plfm7z8g+(_znpjgsef - 1))) - _91a1vq5f);
Mark24:;
							// continue
						}
												}					}
				}
				
				_bfbhh57k = (_bfbhh57k + _dvyydhuh);
Mark230:;
				// continue
			}
						}		}//* 
		//*     III) Twist index 
		//*       T was shifted by SIGMA initially. 
		
		_zf88apxo = ((_2ivtt43r + _91a1vq5f) + _ejwydfmr);
		if (_zf88apxo < _d0547bi2)
		_bfbhh57k = (_bfbhh57k + (int)1);// 
		
		_escttm7a = _bfbhh57k;
	}
	
	return _escttm7a;
	} // 177

} // end class 
} // end namespace
#endif
