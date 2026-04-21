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
//*> \brief \b SLASSQ updates a sum of squares represented in scaled form. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLASSQ + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slassq.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slassq.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slassq.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLASSQ( N, X, INCX, SCALE, SUMSQ ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INCX, N 
//*       REAL               SCALE, SUMSQ 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               X( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SLASSQ  returns the values  scl  and  smsq  such that 
//*> 
//*>    ( scl**2 )*smsq = x( 1 )**2 +...+ x( n )**2 + ( scale**2 )*sumsq, 
//*> 
//*> where  x( i ) = X( 1 + ( i - 1 )*INCX ). The value of  sumsq  is 
//*> assumed to be non-negative and  scl  returns the value 
//*> 
//*>    scl = max( scale, abs( x( i ) ) ). 
//*> 
//*> scale and sumsq must be supplied in SCALE and SUMSQ and 
//*> scl and smsq are overwritten on SCALE and SUMSQ respectively. 
//*> 
//*> The routine makes only one pass through the vector x. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of elements to be used from the vector X. 
//*> \endverbatim 
//*> 
//*> \param[in] X 
//*> \verbatim 
//*>          X is REAL array, dimension (1+(N-1)*INCX) 
//*>          The vector for which a scaled sum of squares is computed. 
//*>             x( i )  = X( 1 + ( i - 1 )*INCX ), 1 <= i <= n. 
//*> \endverbatim 
//*> 
//*> \param[in] INCX 
//*> \verbatim 
//*>          INCX is INTEGER 
//*>          The increment between successive values of the vector X. 
//*>          INCX > 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] SCALE 
//*> \verbatim 
//*>          SCALE is REAL 
//*>          On entry, the value  scale  in the equation above. 
//*>          On exit, SCALE is overwritten with  scl , the scaling factor 
//*>          for the sum of squares. 
//*> \endverbatim 
//*> 
//*> \param[in,out] SUMSQ 
//*> \verbatim 
//*>          SUMSQ is REAL 
//*>          On entry, the value  sumsq  in the equation above. 
//*>          On exit, SUMSQ is overwritten with  smsq , the basic sum of 
//*>          squares from which  scl  has been factored out. 
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

	 
	public static void _0mgnvelf(ref Int32 _dxpq0xkr, Single* _ta7zuy9k, ref Int32 _1eqjusqc, ref Single _1m44vtuk, ref Single _juhpuov6)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Int32 _b69ritwm =  default;
Single _zma2tpvz =  default;
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
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		if (_dxpq0xkr > (int)0)
		{
			
			{
				System.Int32 __81fgg2dlsvn548 = (System.Int32)((int)1);
				System.Int32 __81fgg2step548 = (System.Int32)(_1eqjusqc);
				System.Int32 __81fgg2count548;
				for (__81fgg2count548 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1 + ((_dxpq0xkr - (int)1) * _1eqjusqc)) - __81fgg2dlsvn548 + __81fgg2step548) / __81fgg2step548)), _b69ritwm = __81fgg2dlsvn548; __81fgg2count548 != 0; __81fgg2count548--, _b69ritwm += (__81fgg2step548)) {

				{
					
					_zma2tpvz = ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_b69ritwm - 1)) );
					if ((_zma2tpvz > _d0547bi2) | _lilv8egi(ref _zma2tpvz ))
					{
						
						if (_1m44vtuk < _zma2tpvz)
						{
							
							_juhpuov6 = ((int)1 + (_juhpuov6 * __POW2((_1m44vtuk / _zma2tpvz))));
							_1m44vtuk = _zma2tpvz;
						}
						else
						{
							
							_juhpuov6 = (_juhpuov6 + __POW2((_zma2tpvz / _1m44vtuk)));
						}
						
					}
					
Mark10:;
					// continue
				}
								}			}
		}
		
		return;//* 
		//*     End of SLASSQ 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
