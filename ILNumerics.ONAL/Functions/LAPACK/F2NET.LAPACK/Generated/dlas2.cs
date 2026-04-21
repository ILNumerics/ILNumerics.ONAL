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
//*> \brief \b DLAS2 computes singular values of a 2-by-2 triangular matrix. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLAS2 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlas2.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlas2.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlas2.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLAS2( F, G, H, SSMIN, SSMAX ) 
//* 
//*       .. Scalar Arguments .. 
//*       DOUBLE PRECISION   F, G, H, SSMAX, SSMIN 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLAS2  computes the singular values of the 2-by-2 matrix 
//*>    [  F   G  ] 
//*>    [  0   H  ]. 
//*> On return, SSMIN is the smaller singular value and SSMAX is the 
//*> larger singular value. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] F 
//*> \verbatim 
//*>          F is DOUBLE PRECISION 
//*>          The (1,1) element of the 2-by-2 matrix. 
//*> \endverbatim 
//*> 
//*> \param[in] G 
//*> \verbatim 
//*>          G is DOUBLE PRECISION 
//*>          The (1,2) element of the 2-by-2 matrix. 
//*> \endverbatim 
//*> 
//*> \param[in] H 
//*> \verbatim 
//*>          H is DOUBLE PRECISION 
//*>          The (2,2) element of the 2-by-2 matrix. 
//*> \endverbatim 
//*> 
//*> \param[out] SSMIN 
//*> \verbatim 
//*>          SSMIN is DOUBLE PRECISION 
//*>          The smaller singular value. 
//*> \endverbatim 
//*> 
//*> \param[out] SSMAX 
//*> \verbatim 
//*>          SSMAX is DOUBLE PRECISION 
//*>          The larger singular value. 
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
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  Barring over/underflow, all output quantities are correct to within 
//*>  a few units in the last place (ulps), even in the absence of a guard 
//*>  digit in addition/subtraction. 
//*> 
//*>  In IEEE arithmetic, the code works correctly if one matrix element is 
//*>  infinite. 
//*> 
//*>  Overflow will not occur unless the largest singular value itself 
//*>  overflows, or is within a few ulps of overflow. (On machines with 
//*>  partial overflow, like the Cray, overflow may occur if the largest 
//*>  singular value is within a factor of 2 of overflow.) 
//*> 
//*>  Underflow is harmless if underflow is gradual. Otherwise, results 
//*>  may correspond to a matrix modified by perturbations of size near 
//*>  the underflow threshold. 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _i0q1t486(ref Double _8plnuphw, ref Double _mu73se41, ref Double _ogkjl6gu, ref Double _b7jlbaxv, ref Double _c5hmpmkx)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Double _5m0mjfxm =  2d;
Double _j9zcc7cu =  default;
Double _w0m19i3q =  default;
Double _8zlj2uvh =  default;
Double _3crf0qn3 =  default;
Double _d5yxxwr2 =  default;
Double _wwhduize =  default;
Double _5zyly5w3 =  default;
Double _c0nzba5y =  default;
Double _nr9hio6a =  default;
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
		//* 
		//*  ==================================================================== 
		//* 
		//*     .. Parameters .. 
		//*     .. 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		_d5yxxwr2 = ILNumerics.F2NET.Intrinsics.ABS(_8plnuphw );
		_c0nzba5y = ILNumerics.F2NET.Intrinsics.ABS(_mu73se41 );
		_nr9hio6a = ILNumerics.F2NET.Intrinsics.ABS(_ogkjl6gu );
		_wwhduize = ILNumerics.F2NET.Intrinsics.MIN(_d5yxxwr2 ,_nr9hio6a );
		_5zyly5w3 = ILNumerics.F2NET.Intrinsics.MAX(_d5yxxwr2 ,_nr9hio6a );
		if (_wwhduize == _d0547bi2)
		{
			
			_b7jlbaxv = _d0547bi2;
			if (_5zyly5w3 == _d0547bi2)
			{
				
				_c5hmpmkx = _c0nzba5y;
			}
			else
			{
				
				_c5hmpmkx = (ILNumerics.F2NET.Intrinsics.MAX(_5zyly5w3 ,_c0nzba5y ) * ILNumerics.F2NET.Intrinsics.SQRT(_kxg5drh2 + __POW2((ILNumerics.F2NET.Intrinsics.MIN(_5zyly5w3 ,_c0nzba5y ) / ILNumerics.F2NET.Intrinsics.MAX(_5zyly5w3 ,_c0nzba5y ))) ));
			}
			
		}
		else
		{
			
			if (_c0nzba5y < _5zyly5w3)
			{
				
				_j9zcc7cu = (_kxg5drh2 + (_wwhduize / _5zyly5w3));
				_w0m19i3q = ((_5zyly5w3 - _wwhduize) / _5zyly5w3);
				_8zlj2uvh = __POW2((_c0nzba5y / _5zyly5w3));
				_3crf0qn3 = (_5m0mjfxm / (ILNumerics.F2NET.Intrinsics.SQRT((_j9zcc7cu * _j9zcc7cu) + _8zlj2uvh ) + ILNumerics.F2NET.Intrinsics.SQRT((_w0m19i3q * _w0m19i3q) + _8zlj2uvh )));
				_b7jlbaxv = (_wwhduize * _3crf0qn3);
				_c5hmpmkx = (_5zyly5w3 / _3crf0qn3);
			}
			else
			{
				
				_8zlj2uvh = (_5zyly5w3 / _c0nzba5y);
				if (_8zlj2uvh == _d0547bi2)
				{
					//* 
					//*              Avoid possible harmful underflow if exponent range 
					//*              asymmetric (true SSMIN may not underflow even if 
					//*              AU underflows) 
					//* 
					
					_b7jlbaxv = ((_wwhduize * _5zyly5w3) / _c0nzba5y);
					_c5hmpmkx = _c0nzba5y;
				}
				else
				{
					
					_j9zcc7cu = (_kxg5drh2 + (_wwhduize / _5zyly5w3));
					_w0m19i3q = ((_5zyly5w3 - _wwhduize) / _5zyly5w3);
					_3crf0qn3 = (_kxg5drh2 / (ILNumerics.F2NET.Intrinsics.SQRT(_kxg5drh2 + __POW2((_j9zcc7cu * _8zlj2uvh)) ) + ILNumerics.F2NET.Intrinsics.SQRT(_kxg5drh2 + __POW2((_w0m19i3q * _8zlj2uvh)) )));
					_b7jlbaxv = ((_wwhduize * _3crf0qn3) * _8zlj2uvh);
					_b7jlbaxv = (_b7jlbaxv + _b7jlbaxv);
					_c5hmpmkx = (_c0nzba5y / (_3crf0qn3 + _3crf0qn3));
				}
				
			}
			
		}
		
		return;//* 
		//*     End of DLAS2 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
