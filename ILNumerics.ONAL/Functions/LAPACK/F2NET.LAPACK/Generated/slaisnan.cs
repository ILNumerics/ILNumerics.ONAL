
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
//*> \brief \b SLAISNAN tests input for NaN by comparing two arguments for inequality. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLAISNAN + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slaisnan.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slaisnan.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slaisnan.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       LOGICAL FUNCTION SLAISNAN( SIN1, SIN2 ) 
//* 
//*       .. Scalar Arguments .. 
//*       REAL, INTENT(IN) :: SIN1, SIN2 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> This routine is not for general use.  It exists solely to avoid 
//*> over-optimization in SISNAN. 
//*> 
//*> SLAISNAN checks for NaNs by comparing its two arguments for 
//*> inequality.  NaN is the only floating-point value where NaN != NaN 
//*> returns .TRUE.  To check for NaNs, pass the same variable as both 
//*> arguments. 
//*> 
//*> A compiler must assume that the two arguments are 
//*> not the same variable, and the test will not be optimized away. 
//*> Interprocedural or whole-program optimization may delete this 
//*> test.  The ISNAN functions will be replaced by the correct 
//*> Fortran 03 intrinsic once the intrinsic is widely available. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] SIN1 
//*> \verbatim 
//*>          SIN1 is REAL 
//*> \endverbatim 
//*> 
//*> \param[in] SIN2 
//*> \verbatim 
//*>          SIN2 is REAL 
//*>          Two numbers to compare for inequality. 
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
//*> \date June 2017 
//* 
//*> \ingroup OTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static Boolean _q8wyrqx9(ref Single _snnn8wyy, ref Single _g175plm9)
	{
#region variable declarations
Boolean _q8wyrqx9 = default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK auxiliary routine (version 3.7.1) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     June 2017 
		//* 
		//*     .. Scalar Arguments .. 
		//*     .. 
		//* 
		//*  ===================================================================== 
		//* 
		//*  .. Executable Statements .. 
		
		_q8wyrqx9 = (_snnn8wyy != _g175plm9);
		return _q8wyrqx9;
	}
	
	return _q8wyrqx9;
	} // 177

} // end class 
} // end namespace
#endif
