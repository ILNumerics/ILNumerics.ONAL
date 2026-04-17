
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
//*> \brief \b CLADIV performs complex division in real arithmetic, avoiding unnecessary overflow. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CLADIV + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/cladiv.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/cladiv.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/cladiv.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       COMPLEX FUNCTION CLADIV( X, Y ) 
//* 
//*       .. Scalar Arguments .. 
//*       COMPLEX            X, Y 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CLADIV := X / Y, where X and Y are complex.  The computation of X / Y 
//*> will not overflow on an intermediary step unless the results 
//*> overflows. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] X 
//*> \verbatim 
//*>          X is COMPLEX 
//*> \endverbatim 
//*> 
//*> \param[in] Y 
//*> \verbatim 
//*>          Y is COMPLEX 
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
//*> \ingroup complexOTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static fcomplex _r6l3poxb(ref fcomplex _ta7zuy9k, ref fcomplex _f3z3edv0)
	{
#region variable declarations
fcomplex _r6l3poxb = default;
Single _yjkkx9lx =  default;
Single _4f05y2dr =  default;
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
		
		_d2d71xeq(ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.REAL(_ta7zuy9k )) ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.AIMAG(_ta7zuy9k )) ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.REAL(_f3z3edv0 )) ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.AIMAG(_f3z3edv0 )) ,ref _4f05y2dr ,ref _yjkkx9lx );
		_r6l3poxb = ILNumerics.F2NET.Intrinsics.CMPLX(_4f05y2dr ,_yjkkx9lx );//* 
		
		return _r6l3poxb;//* 
		//*     End of CLADIV 
		//* 
		
	}
	
	return _r6l3poxb;
	} // 177

} // end class 
} // end namespace
#endif
