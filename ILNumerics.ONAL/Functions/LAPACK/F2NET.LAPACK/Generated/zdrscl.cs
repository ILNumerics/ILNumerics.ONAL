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
//*> \brief \b ZDRSCL multiplies a vector by the reciprocal of a real scalar. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZDRSCL + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zdrscl.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zdrscl.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zdrscl.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZDRSCL( N, SA, SX, INCX ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INCX, N 
//*       DOUBLE PRECISION   SA 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX*16         SX( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZDRSCL multiplies an n-element complex vector x by the real scalar 
//*> 1/a.  This is done without overflow or underflow as long as 
//*> the final result x/a does not overflow or underflow. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of components of the vector x. 
//*> \endverbatim 
//*> 
//*> \param[in] SA 
//*> \verbatim 
//*>          SA is DOUBLE PRECISION 
//*>          The scalar a which is used to divide each component of x. 
//*>          SA must be >= 0, or the subroutine will divide by zero. 
//*> \endverbatim 
//*> 
//*> \param[in,out] SX 
//*> \verbatim 
//*>          SX is COMPLEX*16 array, dimension 
//*>                         (1+(N-1)*abs(INCX)) 
//*>          The n-element vector x. 
//*> \endverbatim 
//*> 
//*> \param[in] INCX 
//*> \verbatim 
//*>          INCX is INTEGER 
//*>          The increment between successive values of the vector SX. 
//*>          > 0:  SX(1) = X(1) and SX(1+(i-1)*INCX) = x(i),     1< i<= n 
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
//*  ===================================================================== 

	 
	public static void _pvlp8a6j(ref Int32 _dxpq0xkr, ref Double _xr97pjos, complex* _s66poh0u, ref Int32 _1eqjusqc)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Boolean _xzeqhzgj =  default;
Double _av7j8yda =  default;
Double _3kf3infe =  default;
Double _gcmgj4gb =  default;
Double _6eq7h5bj =  default;
Double _4c97vmi4 =  default;
Double _5j9y0nep =  default;
Double _bogm0gwy =  default;
Int32 _n8tvvlnh =  default;
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
		//* ===================================================================== 
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
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr <= (int)0)
		return;//* 
		//*     Get machine parameters 
		//* 
		
		_bogm0gwy = _f43eg0w0("S" );
		_av7j8yda = (_kxg5drh2 / _bogm0gwy);
		_to4dtyqc(ref _bogm0gwy ,ref _av7j8yda );//* 
		//*     Initialize the denominator to SA and the numerator to 1. 
		//* 
		
		_3kf3infe = _xr97pjos;
		_6eq7h5bj = _kxg5drh2;//* 
		
Mark10:;
		// continue
		_gcmgj4gb = (_3kf3infe * _bogm0gwy);
		_4c97vmi4 = (_6eq7h5bj / _av7j8yda);
		if ((ILNumerics.F2NET.Intrinsics.ABS(_gcmgj4gb ) > ILNumerics.F2NET.Intrinsics.ABS(_6eq7h5bj )) & (_6eq7h5bj != _d0547bi2))
		{
			//* 
			//*        Pre-multiply X by SMLNUM if CDEN is large compared to CNUM. 
			//* 
			
			_5j9y0nep = _bogm0gwy;
			_xzeqhzgj = false;
			_3kf3infe = _gcmgj4gb;
		}
		else
		if (ILNumerics.F2NET.Intrinsics.ABS(_4c97vmi4 ) > ILNumerics.F2NET.Intrinsics.ABS(_3kf3infe ))
		{
			//* 
			//*        Pre-multiply X by BIGNUM if CDEN is small compared to CNUM. 
			//* 
			
			_5j9y0nep = _av7j8yda;
			_xzeqhzgj = false;
			_6eq7h5bj = _4c97vmi4;
		}
		else
		{
			//* 
			//*        Multiply X by CNUM / CDEN and return. 
			//* 
			
			_5j9y0nep = (_6eq7h5bj / _3kf3infe);
			_xzeqhzgj = true;
		}
		//* 
		//*     Scale the vector X by MUL 
		//* 
		
		_z5tkm94d(ref _dxpq0xkr ,ref _5j9y0nep ,_s66poh0u ,ref _1eqjusqc );//* 
		
		if (!(_xzeqhzgj))goto Mark10;//* 
		
		return;//* 
		//*     End of ZDRSCL 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
