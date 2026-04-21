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
//*> \brief \b DZSUM1 forms the 1-norm of the complex vector using the true absolute value. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DZSUM1 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dzsum1.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dzsum1.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dzsum1.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       DOUBLE PRECISION FUNCTION DZSUM1( N, CX, INCX ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INCX, N 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX*16         CX( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DZSUM1 takes the sum of the absolute values of a complex 
//*> vector and returns a double precision result. 
//*> 
//*> Based on DZASUM from the Level 1 BLAS. 
//*> The change is to use the 'genuine' absolute value. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of elements in the vector CX. 
//*> \endverbatim 
//*> 
//*> \param[in] CX 
//*> \verbatim 
//*>          CX is COMPLEX*16 array, dimension (N) 
//*>          The vector whose elements will be summed. 
//*> \endverbatim 
//*> 
//*> \param[in] INCX 
//*> \verbatim 
//*>          INCX is INTEGER 
//*>          The spacing between successive values of CX.  INCX > 0. 
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
//*> \par Contributors: 
//*  ================== 
//*> 
//*> Nick Higham for use with ZLACON. 
//* 
//*  ===================================================================== 

	 
	public static Double _re44grwz(ref Int32 _dxpq0xkr, complex* _c9wzt3lw, ref Int32 _1eqjusqc)
	{
#region variable declarations
Double _re44grwz = default;
Int32 _b5p6od9s =  default;
Int32 _dbc5n539 =  default;
Double _chiot9on =  default;
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
		//*  ===================================================================== 
		//* 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		_re44grwz = 0d;
		_chiot9on = 0d;
		if (_dxpq0xkr <= (int)0)
		return _re44grwz;
		if (_1eqjusqc == (int)1)goto Mark20;//* 
		//*     CODE FOR INCREMENT NOT EQUAL TO 1 
		//* 
		
		_dbc5n539 = (_dxpq0xkr * _1eqjusqc);
		{
			System.Int32 __81fgg2dlsvn2806 = (System.Int32)((int)1);
			System.Int32 __81fgg2step2806 = (System.Int32)(_1eqjusqc);
			System.Int32 __81fgg2count2806;
			for (__81fgg2count2806 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dbc5n539) - __81fgg2dlsvn2806 + __81fgg2step2806) / __81fgg2step2806)), _b5p6od9s = __81fgg2dlsvn2806; __81fgg2count2806 != 0; __81fgg2count2806--, _b5p6od9s += (__81fgg2step2806)) {

			{
				//* 
				//*        NEXT LINE MODIFIED. 
				//* 
				
				_chiot9on = (_chiot9on + ILNumerics.F2NET.Intrinsics.ABS(*(_c9wzt3lw+(_b5p6od9s - 1)) ));
Mark10:;
				// continue
			}
						}		}
		_re44grwz = _chiot9on;
		return _re44grwz;//* 
		//*     CODE FOR INCREMENT EQUAL TO 1 
		//* 
		
Mark20:;
		// continue
		{
			System.Int32 __81fgg2dlsvn2807 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2807 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2807;
			for (__81fgg2count2807 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2807 + __81fgg2step2807) / __81fgg2step2807)), _b5p6od9s = __81fgg2dlsvn2807; __81fgg2count2807 != 0; __81fgg2count2807--, _b5p6od9s += (__81fgg2step2807)) {

			{
				//* 
				//*        NEXT LINE MODIFIED. 
				//* 
				
				_chiot9on = (_chiot9on + ILNumerics.F2NET.Intrinsics.ABS(*(_c9wzt3lw+(_b5p6od9s - 1)) ));
Mark30:;
				// continue
			}
						}		}
		_re44grwz = _chiot9on;
		return _re44grwz;//* 
		//*     End of DZSUM1 
		//* 
		
	}
	
	return _re44grwz;
	} // 177

} // end class 
} // end namespace
#endif
