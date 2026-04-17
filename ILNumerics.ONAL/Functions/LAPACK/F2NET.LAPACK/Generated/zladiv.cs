
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
//*> \brief \b ZLADIV performs complex division in real arithmetic, avoiding unnecessary overflow. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZLADIV + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zladiv.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zladiv.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zladiv.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       COMPLEX*16     FUNCTION ZLADIV( X, Y ) 
//* 
//*       .. Scalar Arguments .. 
//*       COMPLEX*16         X, Y 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZLADIV := X / Y, where X and Y are complex.  The computation of X / Y 
//*> will not overflow on an intermediary step unless the results 
//*> overflows. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] X 
//*> \verbatim 
//*>          X is COMPLEX*16 
//*> \endverbatim 
//*> 
//*> \param[in] Y 
//*> \verbatim 
//*>          Y is COMPLEX*16 
//*>          The complex scalars X and Y. 
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

	 
	public static complex _530gk4dk(ref complex _ta7zuy9k, ref complex _f3z3edv0)
	{
#region variable declarations
complex _530gk4dk = default;
Double _yjkkx9lx =  default;
Double _4f05y2dr =  default;
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
		//*  ===================================================================== 
		//* 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		_x0fujx9g(ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.DBLE(_ta7zuy9k )) ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.DIMAG(_ta7zuy9k )) ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.DBLE(_f3z3edv0 )) ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.DIMAG(_f3z3edv0 )) ,ref _4f05y2dr ,ref _yjkkx9lx );
		_530gk4dk = ILNumerics.F2NET.Intrinsics.DCMPLX(_4f05y2dr ,_yjkkx9lx );//* 
		
		return _530gk4dk;//* 
		//*     End of ZLADIV 
		//* 
		
	}
	
	return _530gk4dk;
	} // 177

} // end class 
} // end namespace
#endif
