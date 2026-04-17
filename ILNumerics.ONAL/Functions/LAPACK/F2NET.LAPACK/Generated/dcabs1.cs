
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
//*> \brief \b DCABS1 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       DOUBLE PRECISION FUNCTION DCABS1(Z) 
//* 
//*       .. Scalar Arguments .. 
//*       COMPLEX*16 Z 
//*       .. 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DCABS1 computes |Re(.)| + |Im(.)| of a double complex number 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] Z 
//*> \verbatim 
//*>          Z is COMPLEX*16 
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
//*  ===================================================================== 

	 
	public static Double _xut7i178(ref complex _7e60fcso)
	{
#region variable declarations
Double _xut7i178 = default;
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
		//*     .. 
		//*  ===================================================================== 
		//* 
		//*     .. Intrinsic Functions .. 
		//* 
		
		_xut7i178 = (ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.DBLE(_7e60fcso ) ) + ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.DIMAG(_7e60fcso ) ));
		return _xut7i178;
	}
	
	return _xut7i178;
	} // 177

} // end class 
} // end namespace
#endif
