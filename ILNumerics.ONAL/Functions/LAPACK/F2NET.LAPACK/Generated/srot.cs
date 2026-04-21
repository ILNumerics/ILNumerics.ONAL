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
//*> \brief \b SROT 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SROT(N,SX,INCX,SY,INCY,C,S) 
//* 
//*       .. Scalar Arguments .. 
//*       REAL C,S 
//*       INTEGER INCX,INCY,N 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL SX(*),SY(*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*>    applies a plane rotation. 
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
//*> \param[in,out] SX 
//*> \verbatim 
//*>          SX is REAL array, dimension ( 1 + ( N - 1 )*abs( INCX ) ) 
//*> \endverbatim 
//*> 
//*> \param[in] INCX 
//*> \verbatim 
//*>          INCX is INTEGER 
//*>         storage spacing between elements of SX 
//*> \endverbatim 
//*> 
//*> \param[in,out] SY 
//*> \verbatim 
//*>          SY is REAL array, dimension ( 1 + ( N - 1 )*abs( INCY ) ) 
//*> \endverbatim 
//*> 
//*> \param[in] INCY 
//*> \verbatim 
//*>          INCY is INTEGER 
//*>         storage spacing between elements of SY 
//*> \endverbatim 
//*> 
//*> \param[in] C 
//*> \verbatim 
//*>          C is REAL 
//*> \endverbatim 
//*> 
//*> \param[in] S 
//*> \verbatim 
//*>          S is REAL 
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
//*> \ingroup single_blas_level1 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>     jack dongarra, linpack, 3/11/78. 
//*>     modified 12/3/93, array(1) declarations changed to array(*) 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _5obdubpp(ref Int32 _dxpq0xkr, Single* _s66poh0u, ref Int32 _1eqjusqc, Single* _bljg4w68, ref Int32 _bbrxgmj7, ref Single _3crf0qn3, ref Single _irk8i6qr)
	{
#region variable declarations
Single _chiot9on =  default;
Int32 _b5p6od9s =  default;
Int32 _b69ritwm =  default;
Int32 _821h5yui =  default;
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
		
		if (_dxpq0xkr <= (int)0)
		return;
		if ((_1eqjusqc == (int)1) & (_bbrxgmj7 == (int)1))
		{
			//* 
			//*       code for both increments equal to 1 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn601 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step601 = (System.Int32)((int)1);
				System.Int32 __81fgg2count601;
				for (__81fgg2count601 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn601 + __81fgg2step601) / __81fgg2step601)), _b5p6od9s = __81fgg2dlsvn601; __81fgg2count601 != 0; __81fgg2count601--, _b5p6od9s += (__81fgg2step601)) {

				{
					
					_chiot9on = ((_3crf0qn3 * *(_s66poh0u+(_b5p6od9s - 1))) + (_irk8i6qr * *(_bljg4w68+(_b5p6od9s - 1))));
					*(_bljg4w68+(_b5p6od9s - 1)) = ((_3crf0qn3 * *(_bljg4w68+(_b5p6od9s - 1))) - (_irk8i6qr * *(_s66poh0u+(_b5p6od9s - 1))));
					*(_s66poh0u+(_b5p6od9s - 1)) = _chiot9on;
				}
								}			}
		}
		else
		{
			//* 
			//*       code for unequal increments or equal increments not equal 
			//*         to 1 
			//* 
			
			_b69ritwm = (int)1;
			_821h5yui = (int)1;
			if (_1eqjusqc < (int)0)
			_b69ritwm = ((((-(_dxpq0xkr)) + (int)1) * _1eqjusqc) + (int)1);
			if (_bbrxgmj7 < (int)0)
			_821h5yui = ((((-(_dxpq0xkr)) + (int)1) * _bbrxgmj7) + (int)1);
			{
				System.Int32 __81fgg2dlsvn602 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step602 = (System.Int32)((int)1);
				System.Int32 __81fgg2count602;
				for (__81fgg2count602 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn602 + __81fgg2step602) / __81fgg2step602)), _b5p6od9s = __81fgg2dlsvn602; __81fgg2count602 != 0; __81fgg2count602--, _b5p6od9s += (__81fgg2step602)) {

				{
					
					_chiot9on = ((_3crf0qn3 * *(_s66poh0u+(_b69ritwm - 1))) + (_irk8i6qr * *(_bljg4w68+(_821h5yui - 1))));
					*(_bljg4w68+(_821h5yui - 1)) = ((_3crf0qn3 * *(_bljg4w68+(_821h5yui - 1))) - (_irk8i6qr * *(_s66poh0u+(_b69ritwm - 1))));
					*(_s66poh0u+(_b69ritwm - 1)) = _chiot9on;
					_b69ritwm = (_b69ritwm + _1eqjusqc);
					_821h5yui = (_821h5yui + _bbrxgmj7);
				}
								}			}
		}
		
		return;
	}
	
	} // 177

} // end class 
} // end namespace
#endif
