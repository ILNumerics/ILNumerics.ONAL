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
//*> \brief \b DLAE2 computes the eigenvalues of a 2-by-2 symmetric matrix. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLAE2 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlae2.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlae2.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlae2.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLAE2( A, B, C, RT1, RT2 ) 
//* 
//*       .. Scalar Arguments .. 
//*       DOUBLE PRECISION   A, B, C, RT1, RT2 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLAE2  computes the eigenvalues of a 2-by-2 symmetric matrix 
//*>    [  A   B  ] 
//*>    [  B   C  ]. 
//*> On return, RT1 is the eigenvalue of larger absolute value, and RT2 
//*> is the eigenvalue of smaller absolute value. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] A 
//*> \verbatim 
//*>          A is DOUBLE PRECISION 
//*>          The (1,1) element of the 2-by-2 matrix. 
//*> \endverbatim 
//*> 
//*> \param[in] B 
//*> \verbatim 
//*>          B is DOUBLE PRECISION 
//*>          The (1,2) and (2,1) elements of the 2-by-2 matrix. 
//*> \endverbatim 
//*> 
//*> \param[in] C 
//*> \verbatim 
//*>          C is DOUBLE PRECISION 
//*>          The (2,2) element of the 2-by-2 matrix. 
//*> \endverbatim 
//*> 
//*> \param[out] RT1 
//*> \verbatim 
//*>          RT1 is DOUBLE PRECISION 
//*>          The eigenvalue of larger absolute value. 
//*> \endverbatim 
//*> 
//*> \param[out] RT2 
//*> \verbatim 
//*>          RT2 is DOUBLE PRECISION 
//*>          The eigenvalue of smaller absolute value. 
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
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  RT1 is accurate to a few ulps barring over/underflow. 
//*> 
//*>  RT2 may be inaccurate if there is massive cancellation in the 
//*>  determinant A*C-B*B; higher precision or correctly rounded or 
//*>  correctly truncated arithmetic would be needed to compute RT2 
//*>  accurately in all cases. 
//*> 
//*>  Overflow is possible only if RT1 is within a factor of 5 of overflow. 
//*>  Underflow is harmless if the input data is 0 or exceeds 
//*>     underflow_threshold / macheps. 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _ud5j6s9u(ref Double _vxfgpup9, ref Double _p9n405a5, ref Double _3crf0qn3, ref Double _wwj82gep, ref Double _uwqai9pg)
	{
#region variable declarations
Double _kxg5drh2 =  1d;
Double _5m0mjfxm =  2d;
Double _d0547bi2 =  0d;
Double _gbf4169i =  0.5d;
Double _9tcc7f14 =  default;
Double _krbe1gdc =  default;
Double _2oh60kgi =  default;
Double _6insfuz5 =  default;
Double _yhyatpgz =  default;
Double _8sqzucpq =  default;
Double _2bpjs4aa =  default;
Double _2h7wt4lq =  default;
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
		//* ===================================================================== 
		//* 
		//*     .. Parameters .. 
		//*     .. 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Compute the eigenvalues 
		//* 
		
		_2bpjs4aa = (_vxfgpup9 + _3crf0qn3);
		_yhyatpgz = (_vxfgpup9 - _3crf0qn3);
		_6insfuz5 = ILNumerics.F2NET.Intrinsics.ABS(_yhyatpgz );
		_2h7wt4lq = (_p9n405a5 + _p9n405a5);
		_9tcc7f14 = ILNumerics.F2NET.Intrinsics.ABS(_2h7wt4lq );
		if (ILNumerics.F2NET.Intrinsics.ABS(_vxfgpup9 ) > ILNumerics.F2NET.Intrinsics.ABS(_3crf0qn3 ))
		{
			
			_2oh60kgi = _vxfgpup9;
			_krbe1gdc = _3crf0qn3;
		}
		else
		{
			
			_2oh60kgi = _3crf0qn3;
			_krbe1gdc = _vxfgpup9;
		}
		
		if (_6insfuz5 > _9tcc7f14)
		{
			
			_8sqzucpq = (_6insfuz5 * ILNumerics.F2NET.Intrinsics.SQRT(_kxg5drh2 + __POW2((_9tcc7f14 / _6insfuz5)) ));
		}
		else
		if (_6insfuz5 < _9tcc7f14)
		{
			
			_8sqzucpq = (_9tcc7f14 * ILNumerics.F2NET.Intrinsics.SQRT(_kxg5drh2 + __POW2((_6insfuz5 / _9tcc7f14)) ));
		}
		else
		{
			//* 
			//*        Includes case AB=ADF=0 
			//* 
			
			_8sqzucpq = (_9tcc7f14 * ILNumerics.F2NET.Intrinsics.SQRT(_5m0mjfxm ));
		}
		
		if (_2bpjs4aa < _d0547bi2)
		{
			
			_wwj82gep = (_gbf4169i * (_2bpjs4aa - _8sqzucpq));//* 
			//*        Order of execution important. 
			//*        To get fully accurate smaller eigenvalue, 
			//*        next line needs to be executed in higher precision. 
			//* 
			
			_uwqai9pg = (((_2oh60kgi / _wwj82gep) * _krbe1gdc) - ((_p9n405a5 / _wwj82gep) * _p9n405a5));
		}
		else
		if (_2bpjs4aa > _d0547bi2)
		{
			
			_wwj82gep = (_gbf4169i * (_2bpjs4aa + _8sqzucpq));//* 
			//*        Order of execution important. 
			//*        To get fully accurate smaller eigenvalue, 
			//*        next line needs to be executed in higher precision. 
			//* 
			
			_uwqai9pg = (((_2oh60kgi / _wwj82gep) * _krbe1gdc) - ((_p9n405a5 / _wwj82gep) * _p9n405a5));
		}
		else
		{
			//* 
			//*        Includes case RT1 = RT2 = 0 
			//* 
			
			_wwj82gep = (_gbf4169i * _8sqzucpq);
			_uwqai9pg = (-((_gbf4169i * _8sqzucpq)));
		}
		
		return;//* 
		//*     End of DLAE2 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
