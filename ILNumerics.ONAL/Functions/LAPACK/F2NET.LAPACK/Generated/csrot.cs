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
//*> \brief \b CSROT 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CSROT( N, CX, INCX, CY, INCY, C, S ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER           INCX, INCY, N 
//*       REAL              C, S 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX           CX( * ), CY( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CSROT applies a plane rotation, where the cos and sin (c and s) are real 
//*> and the vectors cx and cy are complex. 
//*> jack dongarra, linpack, 3/11/78. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>           On entry, N specifies the order of the vectors cx and cy. 
//*>           N must be at least zero. 
//*> \endverbatim 
//*> 
//*> \param[in,out] CX 
//*> \verbatim 
//*>          CX is COMPLEX array, dimension at least 
//*>           ( 1 + ( N - 1 )*abs( INCX ) ). 
//*>           Before entry, the incremented array CX must contain the n 
//*>           element vector cx. On exit, CX is overwritten by the updated 
//*>           vector cx. 
//*> \endverbatim 
//*> 
//*> \param[in] INCX 
//*> \verbatim 
//*>          INCX is INTEGER 
//*>           On entry, INCX specifies the increment for the elements of 
//*>           CX. INCX must not be zero. 
//*> \endverbatim 
//*> 
//*> \param[in,out] CY 
//*> \verbatim 
//*>          CY is COMPLEX array, dimension at least 
//*>           ( 1 + ( N - 1 )*abs( INCY ) ). 
//*>           Before entry, the incremented array CY must contain the n 
//*>           element vector cy. On exit, CY is overwritten by the updated 
//*>           vector cy. 
//*> \endverbatim 
//*> 
//*> \param[in] INCY 
//*> \verbatim 
//*>          INCY is INTEGER 
//*>           On entry, INCY specifies the increment for the elements of 
//*>           CY. INCY must not be zero. 
//*> \endverbatim 
//*> 
//*> \param[in] C 
//*> \verbatim 
//*>          C is REAL 
//*>           On entry, C specifies the cosine, cos. 
//*> \endverbatim 
//*> 
//*> \param[in] S 
//*> \verbatim 
//*>          S is REAL 
//*>           On entry, S specifies the sine, sin. 
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
//*> \ingroup complex_blas_level1 
//* 
//*  ===================================================================== 

	 
	public static void _fvro911c(ref Int32 _dxpq0xkr, fcomplex* _c9wzt3lw, ref Int32 _1eqjusqc, fcomplex* _8czizhxc, ref Int32 _bbrxgmj7, ref Single _3crf0qn3, ref Single _irk8i6qr)
	{
#region variable declarations
Int32 _b5p6od9s =  default;
Int32 _b69ritwm =  default;
Int32 _821h5yui =  default;
fcomplex _r0ocrtbh =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- Reference BLAS level1 routine (version 3.7.0) -- 
		//*  -- Reference BLAS is a software package provided by Univ. of Tennessee,    -- 
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
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		if (_dxpq0xkr <= (int)0)
		return;
		if ((_1eqjusqc == (int)1) & (_bbrxgmj7 == (int)1))
		{
			//* 
			//*        code for both increments equal to 1 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn1409 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1409 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1409;
				for (__81fgg2count1409 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1409 + __81fgg2step1409) / __81fgg2step1409)), _b5p6od9s = __81fgg2dlsvn1409; __81fgg2count1409 != 0; __81fgg2count1409--, _b5p6od9s += (__81fgg2step1409)) {

				{
					
					_r0ocrtbh = ((_3crf0qn3 * *(_c9wzt3lw+(_b5p6od9s - 1))) + (_irk8i6qr * *(_8czizhxc+(_b5p6od9s - 1))));
					*(_8czizhxc+(_b5p6od9s - 1)) = ((_3crf0qn3 * *(_8czizhxc+(_b5p6od9s - 1))) - (_irk8i6qr * *(_c9wzt3lw+(_b5p6od9s - 1))));
					*(_c9wzt3lw+(_b5p6od9s - 1)) = _r0ocrtbh;
				}
								}			}
		}
		else
		{
			//* 
			//*        code for unequal increments or equal increments not equal 
			//*          to 1 
			//* 
			
			_b69ritwm = (int)1;
			_821h5yui = (int)1;
			if (_1eqjusqc < (int)0)
			_b69ritwm = ((((-(_dxpq0xkr)) + (int)1) * _1eqjusqc) + (int)1);
			if (_bbrxgmj7 < (int)0)
			_821h5yui = ((((-(_dxpq0xkr)) + (int)1) * _bbrxgmj7) + (int)1);
			{
				System.Int32 __81fgg2dlsvn1410 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1410 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1410;
				for (__81fgg2count1410 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1410 + __81fgg2step1410) / __81fgg2step1410)), _b5p6od9s = __81fgg2dlsvn1410; __81fgg2count1410 != 0; __81fgg2count1410--, _b5p6od9s += (__81fgg2step1410)) {

				{
					
					_r0ocrtbh = ((_3crf0qn3 * *(_c9wzt3lw+(_b69ritwm - 1))) + (_irk8i6qr * *(_8czizhxc+(_821h5yui - 1))));
					*(_8czizhxc+(_821h5yui - 1)) = ((_3crf0qn3 * *(_8czizhxc+(_821h5yui - 1))) - (_irk8i6qr * *(_c9wzt3lw+(_b69ritwm - 1))));
					*(_c9wzt3lw+(_b69ritwm - 1)) = _r0ocrtbh;
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
