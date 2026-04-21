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
//*> \brief \b DASUM 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       DOUBLE PRECISION FUNCTION DASUM(N,DX,INCX) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER INCX,N 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION DX(*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*>    DASUM takes the sum of the absolute values. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>         number of elements in input vector(s) 
//*> \endverbatim 
//*> 
//*> \param[in] DX 
//*> \verbatim 
//*>          DX is DOUBLE PRECISION array, dimension ( 1 + ( N - 1 )*abs( INCX ) ) 
//*> \endverbatim 
//*> 
//*> \param[in] INCX 
//*> \verbatim 
//*>          INCX is INTEGER 
//*>         storage spacing between elements of DX 
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
//*> \date November 2017 
//* 
//*> \ingroup double_blas_level1 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>     jack dongarra, linpack, 3/11/78. 
//*>     modified 3/93 to return if incx .le. 0. 
//*>     modified 12/3/93, array(1) declarations changed to array(*) 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static Double _seesn96r(ref Int32 _dxpq0xkr, Double* _dr47k4na, ref Int32 _1eqjusqc)
	{
#region variable declarations
Double _seesn96r = default;
Double _7gr3yztw =  default;
Int32 _b5p6od9s =  default;
Int32 _ev4xhht5 =  default;
Int32 _97on0doo =  default;
Int32 _dbc5n539 =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- Reference BLAS level1 routine (version 3.8.0) -- 
		//*  -- Reference BLAS is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     November 2017 
		//* 
		//*     .. Scalar Arguments .. 
		//*     .. 
		//*     .. Array Arguments .. 
		//*     .. 
		//* 
		//*  ===================================================================== 
		//* 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		
		_seesn96r = 0d;
		_7gr3yztw = 0d;
		if ((_dxpq0xkr <= (int)0) | (_1eqjusqc <= (int)0))
		return _seesn96r;
		if (_1eqjusqc == (int)1)
		{
			//*        code for increment equal to 1 
			//* 
			//* 
			//*        clean-up loop 
			//* 
			
			_ev4xhht5 = ILNumerics.F2NET.Intrinsics.MOD(_dxpq0xkr ,(int)6 );
			if (_ev4xhht5 != (int)0)
			{
				
				{
					System.Int32 __81fgg2dlsvn2328 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step2328 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2328;
					for (__81fgg2count2328 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn2328 + __81fgg2step2328) / __81fgg2step2328)), _b5p6od9s = __81fgg2dlsvn2328; __81fgg2count2328 != 0; __81fgg2count2328--, _b5p6od9s += (__81fgg2step2328)) {

					{
						
						_7gr3yztw = (_7gr3yztw + ILNumerics.F2NET.Intrinsics.DABS(*(_dr47k4na+(_b5p6od9s - 1)) ));
					}
										}				}
				if (_dxpq0xkr < (int)6)
				{
					
					_seesn96r = _7gr3yztw;
					return _seesn96r;
				}
				
			}
			
			_97on0doo = (_ev4xhht5 + (int)1);
			{
				System.Int32 __81fgg2dlsvn2329 = (System.Int32)(_97on0doo);
				System.Int32 __81fgg2step2329 = (System.Int32)((int)6);
				System.Int32 __81fgg2count2329;
				for (__81fgg2count2329 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2329 + __81fgg2step2329) / __81fgg2step2329)), _b5p6od9s = __81fgg2dlsvn2329; __81fgg2count2329 != 0; __81fgg2count2329--, _b5p6od9s += (__81fgg2step2329)) {

				{
					
					_7gr3yztw = ((((((_7gr3yztw + ILNumerics.F2NET.Intrinsics.DABS(*(_dr47k4na+(_b5p6od9s - 1)) )) + ILNumerics.F2NET.Intrinsics.DABS(*(_dr47k4na+(_b5p6od9s + (int)1 - 1)) )) + ILNumerics.F2NET.Intrinsics.DABS(*(_dr47k4na+(_b5p6od9s + (int)2 - 1)) )) + ILNumerics.F2NET.Intrinsics.DABS(*(_dr47k4na+(_b5p6od9s + (int)3 - 1)) )) + ILNumerics.F2NET.Intrinsics.DABS(*(_dr47k4na+(_b5p6od9s + (int)4 - 1)) )) + ILNumerics.F2NET.Intrinsics.DABS(*(_dr47k4na+(_b5p6od9s + (int)5 - 1)) ));
				}
								}			}
		}
		else
		{
			//* 
			//*        code for increment not equal to 1 
			//* 
			
			_dbc5n539 = (_dxpq0xkr * _1eqjusqc);
			{
				System.Int32 __81fgg2dlsvn2330 = (System.Int32)((int)1);
				System.Int32 __81fgg2step2330 = (System.Int32)(_1eqjusqc);
				System.Int32 __81fgg2count2330;
				for (__81fgg2count2330 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dbc5n539) - __81fgg2dlsvn2330 + __81fgg2step2330) / __81fgg2step2330)), _b5p6od9s = __81fgg2dlsvn2330; __81fgg2count2330 != 0; __81fgg2count2330--, _b5p6od9s += (__81fgg2step2330)) {

				{
					
					_7gr3yztw = (_7gr3yztw + ILNumerics.F2NET.Intrinsics.DABS(*(_dr47k4na+(_b5p6od9s - 1)) ));
				}
								}			}
		}
		
		_seesn96r = _7gr3yztw;
		return _seesn96r;
	}
	
	return _seesn96r;
	} // 177

} // end class 
} // end namespace
#endif
