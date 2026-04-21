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
//*> \brief \b SLAMCH 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*      REAL             FUNCTION SLAMCH( CMACH ) 
//* 
//*     .. Scalar Arguments .. 
//*      CHARACTER          CMACH 
//*     .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SLAMCH determines single precision machine parameters. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] CMACH 
//*> \verbatim 
//*>          CMACH is CHARACTER*1 
//*>          Specifies the value to be returned by SLAMCH: 
//*>          = 'E' or 'e',   SLAMCH := eps 
//*>          = 'S' or 's ,   SLAMCH := sfmin 
//*>          = 'B' or 'b',   SLAMCH := base 
//*>          = 'P' or 'p',   SLAMCH := eps*base 
//*>          = 'N' or 'n',   SLAMCH := t 
//*>          = 'R' or 'r',   SLAMCH := rnd 
//*>          = 'M' or 'm',   SLAMCH := emin 
//*>          = 'U' or 'u',   SLAMCH := rmin 
//*>          = 'L' or 'l',   SLAMCH := emax 
//*>          = 'O' or 'o',   SLAMCH := rmax 
//*>          where 
//*>          eps   = relative machine precision 
//*>          sfmin = safe minimum, such that 1/sfmin does not overflow 
//*>          base  = base of the machine 
//*>          prec  = eps*base 
//*>          t     = number of (base) digits in the mantissa 
//*>          rnd   = 1.0 when rounding occurs in addition, 0.0 otherwise 
//*>          emin  = minimum exponent before (gradual) underflow 
//*>          rmin  = underflow threshold - base**(emin-1) 
//*>          emax  = largest exponent before overflow 
//*>          rmax  = overflow threshold  - (base**emax)*(1-eps) 
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
//*> \ingroup auxOTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static Single _d5tu038y(FString _6vkdxd1o)
	{
#region variable declarations
Single _d5tu038y = default;
Single _kxg5drh2 =  1f;
Single _d0547bi2 =  0f;
Single _yfe9rs0f =  default;
Single _p1iqarg6 =  default;
Single _ptpa0vax =  default;
Single _8ryg9y0e =  default;
Single _wx15ftlo =  default;
string fLanavab = default;
#endregion  variable declarations
_6vkdxd1o = _6vkdxd1o.Convert(1);

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
		//* ===================================================================== 
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
		//* 
		//*     Assume rounding, not chopping. Always. 
		//* 
		
		_yfe9rs0f = _kxg5drh2;//* 
		
		if (_kxg5drh2 == _yfe9rs0f)
		{
			
			_p1iqarg6 = (ILNumerics.F2NET.Intrinsics.EPSILON(_d0547bi2 ) * 0.5f);
		}
		else
		{
			
			_p1iqarg6 = ILNumerics.F2NET.Intrinsics.EPSILON(_d0547bi2 );
		}
		//* 
		
		if (_w8y2rzgy(_6vkdxd1o ,"E" ))
		{
			
			_wx15ftlo = _p1iqarg6;
		}
		else
		if (_w8y2rzgy(_6vkdxd1o ,"S" ))
		{
			
			_ptpa0vax = ILNumerics.F2NET.Intrinsics.TINY(_d0547bi2 );
			_8ryg9y0e = (_kxg5drh2 / ILNumerics.F2NET.Intrinsics.HUGE(_d0547bi2 ));
			if (_8ryg9y0e >= _ptpa0vax)
			{
				//* 
				//*           Use SMALL plus a bit, to avoid the possibility of rounding 
				//*           causing overflow when computing  1/sfmin. 
				//* 
				
				_ptpa0vax = (_8ryg9y0e * (_kxg5drh2 + _p1iqarg6));
			}
			
			_wx15ftlo = _ptpa0vax;
		}
		else
		if (_w8y2rzgy(_6vkdxd1o ,"B" ))
		{
			
			_wx15ftlo = REAL(ILNumerics.F2NET.Intrinsics.RADIX(_d0547bi2 ));
		}
		else
		if (_w8y2rzgy(_6vkdxd1o ,"P" ))
		{
			
			_wx15ftlo = (_p1iqarg6 * ILNumerics.F2NET.Intrinsics.RADIX(_d0547bi2 ));
		}
		else
		if (_w8y2rzgy(_6vkdxd1o ,"N" ))
		{
			
			_wx15ftlo = REAL(ILNumerics.F2NET.Intrinsics.DIGITS(_d0547bi2 ));
		}
		else
		if (_w8y2rzgy(_6vkdxd1o ,"R" ))
		{
			
			_wx15ftlo = _yfe9rs0f;
		}
		else
		if (_w8y2rzgy(_6vkdxd1o ,"M" ))
		{
			
			_wx15ftlo = REAL(ILNumerics.F2NET.Intrinsics.MINEXPONENT(_d0547bi2 ));
		}
		else
		if (_w8y2rzgy(_6vkdxd1o ,"U" ))
		{
			
			_wx15ftlo = ILNumerics.F2NET.Intrinsics.TINY(_d0547bi2 );
		}
		else
		if (_w8y2rzgy(_6vkdxd1o ,"L" ))
		{
			
			_wx15ftlo = REAL(ILNumerics.F2NET.Intrinsics.MAXEXPONENT(_d0547bi2 ));
		}
		else
		if (_w8y2rzgy(_6vkdxd1o ,"O" ))
		{
			
			_wx15ftlo = ILNumerics.F2NET.Intrinsics.HUGE(_d0547bi2 );
		}
		else
		{
			
			_wx15ftlo = _d0547bi2;
		}
		//* 
		
		_d5tu038y = _wx15ftlo;
		return _d5tu038y;//* 
		//*     End of SLAMCH 
		//* 
		
	}
	
	return _d5tu038y;
	} // 177
//************************************************************************ 
//*> \brief \b SLAMC3 
//*> \details 
//*> \b Purpose: 
//*> \verbatim 
//*> SLAMC3  is intended to force  A  and  B  to be stored prior to doing 
//*> the addition of  A  and  B ,  for use in situations where optimizers 
//*> might hold one of these in a register. 
//*> \endverbatim 
//*> \author LAPACK is a software package provided by Univ. of Tennessee, Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd.. 
//*> \date December 2016 
//*> \ingroup auxOTHERauxiliary 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*> \endverbatim 
//*> 
//*> \param[in] B 
//*> \verbatim 
//*>          The values A and B. 
//*> \endverbatim 
//*> 
//* 

	 
	public static Single _a1q56mil(ref Single _vxfgpup9, ref Single _p9n405a5)
	{
#region variable declarations
Single _a1q56mil = default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK auxiliary routine (version 3.7.0) -- 
		//*     Univ. of Tennessee, Univ. of California Berkeley and NAG Ltd.. 
		//*     November 2010 
		//* 
		//*     .. Scalar Arguments .. 
		//*     .. 
		//* ===================================================================== 
		//* 
		//*     .. Executable Statements .. 
		//* 
		
		_a1q56mil = (_vxfgpup9 + _p9n405a5);//* 
		
		return _a1q56mil;//* 
		//*     End of SLAMC3 
		//* 
		
	}
	
	return _a1q56mil;
	} // 177
//* 
//************************************************************************ 

} // end class 
} // end namespace
#endif
