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
//*> \brief \b DLADIV performs complex division in real arithmetic, avoiding unnecessary overflow. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLADIV + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dladiv.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dladiv.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dladiv.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLADIV( A, B, C, D, P, Q ) 
//* 
//*       .. Scalar Arguments .. 
//*       DOUBLE PRECISION   A, B, C, D, P, Q 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLADIV performs complex division in  real arithmetic 
//*> 
//*>                       a + i*b 
//*>            p + i*q = --------- 
//*>                       c + i*d 
//*> 
//*> The algorithm is due to Michael Baudin and Robert L. Smith 
//*> and can be found in the paper 
//*> "A Robust Complex Division in Scilab" 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] A 
//*> \verbatim 
//*>          A is DOUBLE PRECISION 
//*> \endverbatim 
//*> 
//*> \param[in] B 
//*> \verbatim 
//*>          B is DOUBLE PRECISION 
//*> \endverbatim 
//*> 
//*> \param[in] C 
//*> \verbatim 
//*>          C is DOUBLE PRECISION 
//*> \endverbatim 
//*> 
//*> \param[in] D 
//*> \verbatim 
//*>          D is DOUBLE PRECISION 
//*>          The scalars a, b, c, and d in the above expression. 
//*> \endverbatim 
//*> 
//*> \param[out] P 
//*> \verbatim 
//*>          P is DOUBLE PRECISION 
//*> \endverbatim 
//*> 
//*> \param[out] Q 
//*> \verbatim 
//*>          Q is DOUBLE PRECISION 
//*>          The scalars p and q in the above expression. 
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
//*> \date January 2013 
//* 
//*> \ingroup doubleOTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static void _x0fujx9g(ref Double _vxfgpup9, ref Double _p9n405a5, ref Double _3crf0qn3, ref Double _plfm7z8g, ref Double _ejwydfmr, ref Double _atumjwo3)
	{
#region variable declarations
Double _sr6duauw =  2d;
Double _gbf4169i =  0.5d;
Double _5m0mjfxm =  2d;
Double _zwm0s9sq =  default;
Double _uv0s0qmf =  default;
Double _985e9e9b =  default;
Double _f4rvsg6o =  default;
Double _9tcc7f14 =  default;
Double _265fi0iq =  default;
Double _irk8i6qr =  default;
Double _ojpk0a52 =  default;
Double _r5g2uj45 =  default;
Double _tshpf2lm =  default;
Double _p1iqarg6 =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK auxiliary routine (version 3.7.0) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     January 2013 
		//* 
		//*     .. Scalar Arguments .. 
		//*     .. 
		//* 
		//*  ===================================================================== 
		//* 
		//*     .. Parameters .. 
		//* 
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
		
		_zwm0s9sq = _vxfgpup9;
		_uv0s0qmf = _p9n405a5;
		_985e9e9b = _3crf0qn3;
		_f4rvsg6o = _plfm7z8g;
		_9tcc7f14 = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_vxfgpup9 ) ,ILNumerics.F2NET.Intrinsics.ABS(_p9n405a5 ) );
		_265fi0iq = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_3crf0qn3 ) ,ILNumerics.F2NET.Intrinsics.ABS(_plfm7z8g ) );
		_irk8i6qr = 1d;// 
		
		_ojpk0a52 = _f43eg0w0("Overflow threshold" );
		_r5g2uj45 = _f43eg0w0("Safe minimum" );
		_p1iqarg6 = _f43eg0w0("Epsilon" );
		_tshpf2lm = (_sr6duauw / (_p1iqarg6 * _p1iqarg6));// 
		
		if (_9tcc7f14 >= (_gbf4169i * _ojpk0a52))
		{
			
			_zwm0s9sq = (_gbf4169i * _zwm0s9sq);
			_uv0s0qmf = (_gbf4169i * _uv0s0qmf);
			_irk8i6qr = (_5m0mjfxm * _irk8i6qr);
		}
		
		if (_265fi0iq >= (_gbf4169i * _ojpk0a52))
		{
			
			_985e9e9b = (_gbf4169i * _985e9e9b);
			_f4rvsg6o = (_gbf4169i * _f4rvsg6o);
			_irk8i6qr = (_gbf4169i * _irk8i6qr);
		}
		
		if (_9tcc7f14 <= ((_r5g2uj45 * _sr6duauw) / _p1iqarg6))
		{
			
			_zwm0s9sq = (_zwm0s9sq * _tshpf2lm);
			_uv0s0qmf = (_uv0s0qmf * _tshpf2lm);
			_irk8i6qr = (_irk8i6qr / _tshpf2lm);
		}
		
		if (_265fi0iq <= ((_r5g2uj45 * _sr6duauw) / _p1iqarg6))
		{
			
			_985e9e9b = (_985e9e9b * _tshpf2lm);
			_f4rvsg6o = (_f4rvsg6o * _tshpf2lm);
			_irk8i6qr = (_irk8i6qr * _tshpf2lm);
		}
		
		if (ILNumerics.F2NET.Intrinsics.ABS(_plfm7z8g ) <= ILNumerics.F2NET.Intrinsics.ABS(_3crf0qn3 ))
		{
			
			_9jiec0yi(ref _zwm0s9sq ,ref _uv0s0qmf ,ref _985e9e9b ,ref _f4rvsg6o ,ref _ejwydfmr ,ref _atumjwo3 );
		}
		else
		{
			
			_9jiec0yi(ref _uv0s0qmf ,ref _zwm0s9sq ,ref _f4rvsg6o ,ref _985e9e9b ,ref _ejwydfmr ,ref _atumjwo3 );
			_atumjwo3 = (-(_atumjwo3));
		}
		
		_ejwydfmr = (_ejwydfmr * _irk8i6qr);
		_atumjwo3 = (_atumjwo3 * _irk8i6qr);//* 
		
		return;//* 
		//*     End of DLADIV 
		//* 
		
	}
	
	} // 177
// 
//*> \ingroup doubleOTHERauxiliary 
// 
// 

	 
	public static void _9jiec0yi(ref Double _vxfgpup9, ref Double _p9n405a5, ref Double _3crf0qn3, ref Double _plfm7z8g, ref Double _ejwydfmr, ref Double _atumjwo3)
	{
#region variable declarations
Double _kxg5drh2 =  1d;
Double _q2vwp05i =  default;
Double _2ivtt43r =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK auxiliary routine (version 3.7.0) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     January 2013 
		//* 
		//*     .. Scalar Arguments .. 
		//*     .. 
		//* 
		//*  ===================================================================== 
		//* 
		//*     .. Parameters .. 
		//* 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		_q2vwp05i = (_plfm7z8g / _3crf0qn3);
		_2ivtt43r = (_kxg5drh2 / (_3crf0qn3 + (_plfm7z8g * _q2vwp05i)));
		_ejwydfmr = _qpovmv9p(ref _vxfgpup9 ,ref _p9n405a5 ,ref _3crf0qn3 ,ref _plfm7z8g ,ref _q2vwp05i ,ref _2ivtt43r );
		_vxfgpup9 = (-(_vxfgpup9));
		_atumjwo3 = _qpovmv9p(ref _p9n405a5 ,ref _vxfgpup9 ,ref _3crf0qn3 ,ref _plfm7z8g ,ref _q2vwp05i ,ref _2ivtt43r );//* 
		
		return;//* 
		//*     End of DLADIV1 
		//* 
		
	}
	
	} // 177
// 
//*> \ingroup doubleOTHERauxiliary 
// 

	 
	public static Double _qpovmv9p(ref Double _vxfgpup9, ref Double _p9n405a5, ref Double _3crf0qn3, ref Double _plfm7z8g, ref Double _q2vwp05i, ref Double _2ivtt43r)
	{
#region variable declarations
Double _qpovmv9p = default;
Double _d0547bi2 =  0d;
Double _8ydx9d3t =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK auxiliary routine (version 3.7.0) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     January 2013 
		//* 
		//*     .. Scalar Arguments .. 
		//*     .. 
		//* 
		//*  ===================================================================== 
		//* 
		//*     .. Parameters .. 
		//* 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		if (_q2vwp05i != _d0547bi2)
		{
			
			_8ydx9d3t = (_p9n405a5 * _q2vwp05i);
			if (_8ydx9d3t != _d0547bi2)
			{
				
				_qpovmv9p = ((_vxfgpup9 + _8ydx9d3t) * _2ivtt43r);
			}
			else
			{
				
				_qpovmv9p = ((_vxfgpup9 * _2ivtt43r) + ((_p9n405a5 * _2ivtt43r) * _q2vwp05i));
			}
			
		}
		else
		{
			
			_qpovmv9p = ((_vxfgpup9 + (_plfm7z8g * (_p9n405a5 / _3crf0qn3))) * _2ivtt43r);
		}
		//* 
		
		return _qpovmv9p;//* 
		//*     End of DLADIV12 
		//* 
		
	}
	
	return _qpovmv9p;
	} // 177

} // end class 
} // end namespace
#endif
