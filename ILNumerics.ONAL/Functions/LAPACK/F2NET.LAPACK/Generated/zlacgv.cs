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
//*> \brief \b ZLACGV conjugates a complex vector. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZLACGV + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zlacgv.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zlacgv.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zlacgv.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZLACGV( N, X, INCX ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INCX, N 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX*16         X( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZLACGV conjugates a complex vector of length N. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The length of the vector X.  N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] X 
//*> \verbatim 
//*>          X is COMPLEX*16 array, dimension 
//*>                         (1+(N-1)*abs(INCX)) 
//*>          On entry, the vector of length N to be conjugated. 
//*>          On exit, X is overwritten with conjg(X). 
//*> \endverbatim 
//*> 
//*> \param[in] INCX 
//*> \verbatim 
//*>          INCX is INTEGER 
//*>          The spacing between successive elements of X. 
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

	 
	public static void _42wgkyoq(ref Int32 _dxpq0xkr, complex* _ta7zuy9k, ref Int32 _1eqjusqc)
	{
#region variable declarations
Int32 _b5p6od9s =  default;
Int32 _kk45tx7l =  default;
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
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		if (_1eqjusqc == (int)1)
		{
			
			{
				System.Int32 __81fgg2dlsvn1140 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1140 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1140;
				for (__81fgg2count1140 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1140 + __81fgg2step1140) / __81fgg2step1140)), _b5p6od9s = __81fgg2dlsvn1140; __81fgg2count1140 != 0; __81fgg2count1140--, _b5p6od9s += (__81fgg2step1140)) {

				{
					
					*(_ta7zuy9k+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.DCONJG(*(_ta7zuy9k+(_b5p6od9s - 1)) );
Mark10:;
					// continue
				}
								}			}
		}
		else
		{
			
			_kk45tx7l = (int)1;
			if (_1eqjusqc < (int)0)
			_kk45tx7l = ((int)1 - ((_dxpq0xkr - (int)1) * _1eqjusqc));
			{
				System.Int32 __81fgg2dlsvn1141 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1141 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1141;
				for (__81fgg2count1141 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1141 + __81fgg2step1141) / __81fgg2step1141)), _b5p6od9s = __81fgg2dlsvn1141; __81fgg2count1141 != 0; __81fgg2count1141--, _b5p6od9s += (__81fgg2step1141)) {

				{
					
					*(_ta7zuy9k+(_kk45tx7l - 1)) = ILNumerics.F2NET.Intrinsics.DCONJG(*(_ta7zuy9k+(_kk45tx7l - 1)) );
					_kk45tx7l = (_kk45tx7l + _1eqjusqc);
Mark20:;
					// continue
				}
								}			}
		}
		
		return;//* 
		//*     End of ZLACGV 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
