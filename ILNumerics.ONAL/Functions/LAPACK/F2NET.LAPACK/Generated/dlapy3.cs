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
//*> \brief \b DLAPY3 returns sqrt(x2+y2+z2). 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLAPY3 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlapy3.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlapy3.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlapy3.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       DOUBLE PRECISION FUNCTION DLAPY3( X, Y, Z ) 
//* 
//*       .. Scalar Arguments .. 
//*       DOUBLE PRECISION   X, Y, Z 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLAPY3 returns sqrt(x**2+y**2+z**2), taking care not to cause 
//*> unnecessary overflow. 
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
//*> \endverbatim 
//*> 
//*> \param[in] Z 
//*> \verbatim 
//*>          Z is DOUBLE PRECISION 
//*>          X, Y and Z specify the values x, y and z. 
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
//*  ===================================================================== 

	 
	public static Double _b8sey8kb(ref Double _ta7zuy9k, ref Double _f3z3edv0, ref Double _7e60fcso)
	{
#region variable declarations
Double _2td6fiva =  default;
Double _b8sey8kb = default;
Double _d0547bi2 =  0d;
Double _z1ioc3c8 =  default;
Double _w2j6wbyd =  default;
Double _s47fpvoj =  default;
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
		//*  ===================================================================== 
		//* 
		//*     .. Parameters .. 
		//*     .. 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		_w2j6wbyd = ILNumerics.F2NET.Intrinsics.ABS(_ta7zuy9k );
		_s47fpvoj = ILNumerics.F2NET.Intrinsics.ABS(_f3z3edv0 );
		_2td6fiva = ILNumerics.F2NET.Intrinsics.ABS(_7e60fcso );
		_z1ioc3c8 = ILNumerics.F2NET.Intrinsics.MAX(_w2j6wbyd ,_s47fpvoj ,_2td6fiva );
		if (_z1ioc3c8 == _d0547bi2)
		{
			//*     W can be zero for max(0,nan,0) 
			//*     adding all three entries together will make sure 
			//*     NaN will not disappear. 
			
			_b8sey8kb = ((_w2j6wbyd + _s47fpvoj) + _2td6fiva);
		}
		else
		{
			
			_b8sey8kb = (_z1ioc3c8 * ILNumerics.F2NET.Intrinsics.SQRT((__POW2((_w2j6wbyd / _z1ioc3c8)) + __POW2((_s47fpvoj / _z1ioc3c8))) + __POW2((_2td6fiva / _z1ioc3c8)) ));
		}
		
		return _b8sey8kb;//* 
		//*     End of DLAPY3 
		//* 
		
	}
	
	return _b8sey8kb;
	} // 177

} // end class 
} // end namespace
#endif
