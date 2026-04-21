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
//*> \brief \b DLAPY2 returns sqrt(x2+y2). 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLAPY2 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlapy2.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlapy2.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlapy2.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       DOUBLE PRECISION FUNCTION DLAPY2( X, Y ) 
//* 
//*       .. Scalar Arguments .. 
//*       DOUBLE PRECISION   X, Y 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLAPY2 returns sqrt(x**2+y**2), taking care not to cause unnecessary 
//*> overflow. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] X 
//*> \verbatim 
//*>          X is DOUBLE PRECISION 
//*> \endverbatim 
//*> 
//*> \param[in] Y 
//*> \verbatim 
//*>          Y is DOUBLE PRECISION 
//*>          X and Y specify the values x and y. 
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

	 
	public static Double _1uc27645(ref Double _ta7zuy9k, ref Double _f3z3edv0)
	{
#region variable declarations
Double _1uc27645 = default;
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Double _z1ioc3c8 =  default;
Double _w2j6wbyd =  default;
Double _s47fpvoj =  default;
Double _7e60fcso =  default;
Boolean _dncp1tun =  default;
Boolean _ov32iss0 =  default;
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
		
		_dncp1tun = _fk98jwhi(ref _ta7zuy9k );
		_ov32iss0 = _fk98jwhi(ref _f3z3edv0 );
		if (_dncp1tun)
		_1uc27645 = _ta7zuy9k;
		if (_ov32iss0)
		_1uc27645 = _f3z3edv0;//* 
		
		if (!((_dncp1tun | _ov32iss0)))
		{
			
			_w2j6wbyd = ILNumerics.F2NET.Intrinsics.ABS(_ta7zuy9k );
			_s47fpvoj = ILNumerics.F2NET.Intrinsics.ABS(_f3z3edv0 );
			_z1ioc3c8 = ILNumerics.F2NET.Intrinsics.MAX(_w2j6wbyd ,_s47fpvoj );
			_7e60fcso = ILNumerics.F2NET.Intrinsics.MIN(_w2j6wbyd ,_s47fpvoj );
			if (_7e60fcso == _d0547bi2)
			{
				
				_1uc27645 = _z1ioc3c8;
			}
			else
			{
				
				_1uc27645 = (_z1ioc3c8 * ILNumerics.F2NET.Intrinsics.SQRT(_kxg5drh2 + __POW2((_7e60fcso / _z1ioc3c8)) ));
			}
			
		}
		
		return _1uc27645;//* 
		//*     End of DLAPY2 
		//* 
		
	}
	
	return _1uc27645;
	} // 177

} // end class 
} // end namespace
#endif
