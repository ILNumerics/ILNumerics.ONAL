
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
//*> \brief \b LSAMEN 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download LSAMEN + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/lsamen.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/lsamen.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/lsamen.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       LOGICAL          FUNCTION LSAMEN( N, CA, CB ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER*( * )    CA, CB 
//*       INTEGER            N 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> LSAMEN  tests if the first N letters of CA are the same as the 
//*> first N letters of CB, regardless of case. 
//*> LSAMEN returns .TRUE. if CA and CB are equivalent except for case 
//*> and .FALSE. otherwise.  LSAMEN also returns .FALSE. if LEN( CA ) 
//*> or LEN( CB ) is less than N. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of characters in CA and CB to be compared. 
//*> \endverbatim 
//*> 
//*> \param[in] CA 
//*> \verbatim 
//*>          CA is CHARACTER*(*) 
//*> \endverbatim 
//*> 
//*> \param[in] CB 
//*> \verbatim 
//*>          CB is CHARACTER*(*) 
//*>          CA and CB specify two character strings of length at least N. 
//*>          Only the first N characters of each string will be accessed. 
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
//*  ===================================================================== 

	 
	public static Boolean _tr1i8ubx(ref Int32 _dxpq0xkr, FString _han26hfi, FString _k9gii4ij)
	{
#region variable declarations
Boolean _tr1i8ubx = default;
Int32 _b5p6od9s =  default;
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
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		_tr1i8ubx = false;
		if ((ILNumerics.F2NET.Intrinsics.LEN(_han26hfi ) < _dxpq0xkr) | (ILNumerics.F2NET.Intrinsics.LEN(_k9gii4ij ) < _dxpq0xkr))goto Mark20;//* 
		//*     Do for each character in the two strings. 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn3966 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step3966 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3966;
			for (__81fgg2count3966 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3966 + __81fgg2step3966) / __81fgg2step3966)), _b5p6od9s = __81fgg2dlsvn3966; __81fgg2count3966 != 0; __81fgg2count3966--, _b5p6od9s += (__81fgg2step3966)) {

			{
				//* 
				//*        Test if the characters are equal using LSAME. 
				//* 
				
				if (!(_w8y2rzgy(_han26hfi[_b5p6od9s,_b5p6od9s] ,_k9gii4ij[_b5p6od9s,_b5p6od9s] )))goto Mark20;//* 
				
Mark10:;
				// continue
			}
						}		}
		_tr1i8ubx = true;//* 
		
Mark20:;
		// continue
		return _tr1i8ubx;//* 
		//*     End of LSAMEN 
		//* 
		
	}
	
	return _tr1i8ubx;
	} // 177

} // end class 
} // end namespace
#endif
