
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
//*> \brief \b DLAISNAN tests input for NaN by comparing two arguments for inequality. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLAISNAN + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlaisnan.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlaisnan.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlaisnan.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       LOGICAL FUNCTION DLAISNAN( DIN1, DIN2 ) 
//* 
//*       .. Scalar Arguments .. 
//*       DOUBLE PRECISION, INTENT(IN) :: DIN1, DIN2 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> This routine is not for general use.  It exists solely to avoid 
//*> over-optimization in DISNAN. 
//*> 
//*> DLAISNAN checks for NaNs by comparing its two arguments for 
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
//*> \param[in] DIN1 
//*> \verbatim 
//*>          DIN1 is DOUBLE PRECISION 
//*> \endverbatim 
//*> 
//*> \param[in] DIN2 
//*> \verbatim 
//*>          DIN2 is DOUBLE PRECISION 
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

	 
	public static Boolean _bwmlw545(ref Double _lqv5iwvc, ref Double _kzs3gohg)
	{
#region variable declarations
Boolean _bwmlw545 = default;
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
		
		_bwmlw545 = (_lqv5iwvc != _kzs3gohg);
		return _bwmlw545;
	}
	
	return _bwmlw545;
	} // 177

} // end class 
} // end namespace
#endif
