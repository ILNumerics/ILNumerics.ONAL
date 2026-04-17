
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
//*> \brief \b SCOMBSSQ adds two scaled sum of squares quantities 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SCOMBSSQ( V1, V2 ) 
//* 
//*       .. Array Arguments .. 
//*       REAL               V1( 2 ), V2( 2 ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SCOMBSSQ adds two scaled sum of squares quantities, V1 := V1 + V2. 
//*> That is, 
//*> 
//*>    V1_scale**2 * V1_sumsq := V1_scale**2 * V1_sumsq 
//*>                            + V2_scale**2 * V2_sumsq 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in,out] V1 
//*> \verbatim 
//*>          V1 is REAL array, dimension (2). 
//*>          The first scaled sum. 
//*>          V1(1) = V1_scale, V1(2) = V1_sumsq. 
//*> \endverbatim 
//*> 
//*> \param[in] V2 
//*> \verbatim 
//*>          V2 is REAL array, dimension (2). 
//*>          The second scaled sum. 
//*>          V2(1) = V2_scale, V2(2) = V2_sumsq. 
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
//*> \date November 2018 
//* 
//*> \ingroup OTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static void _4hcafcgj(Single* _z1p8kxoj, Single* _j3ir6imc)
	{
#region variable declarations
Single _d0547bi2 =  Convert.ToSingle(0d);
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK auxiliary routine (version 3.7.0) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     November 2018 
		//* 
		//*     .. Array Arguments .. 
		//*     .. 
		//* 
		//* ===================================================================== 
		//* 
		//*     .. Parameters .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		if (*(_z1p8kxoj+((int)1 - 1)) >= *(_j3ir6imc+((int)1 - 1)))
		{
			
			if (*(_z1p8kxoj+((int)1 - 1)) != _d0547bi2)
			{
				
				*(_z1p8kxoj+((int)2 - 1)) = (*(_z1p8kxoj+((int)2 - 1)) + (__POW2((*(_j3ir6imc+((int)1 - 1)) / *(_z1p8kxoj+((int)1 - 1)))) * *(_j3ir6imc+((int)2 - 1))));
			}
			
		}
		else
		{
			
			*(_z1p8kxoj+((int)2 - 1)) = (*(_j3ir6imc+((int)2 - 1)) + (__POW2((*(_z1p8kxoj+((int)1 - 1)) / *(_j3ir6imc+((int)1 - 1)))) * *(_z1p8kxoj+((int)2 - 1))));
			*(_z1p8kxoj+((int)1 - 1)) = *(_j3ir6imc+((int)1 - 1));
		}
		
		return;//* 
		//*     End of SCOMBSSQ 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
