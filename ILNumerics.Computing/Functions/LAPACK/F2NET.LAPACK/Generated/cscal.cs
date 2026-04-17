
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
//*> \brief \b CSCAL 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CSCAL(N,CA,CX,INCX) 
//* 
//*       .. Scalar Arguments .. 
//*       COMPLEX CA 
//*       INTEGER INCX,N 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX CX(*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*>    CSCAL scales a vector by a constant. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>         number of elements in input vector(s) 
//*> \endverbatim 
//*> 
//*> \param[in] CA 
//*> \verbatim 
//*>          CA is COMPLEX 
//*>           On entry, CA specifies the scalar alpha. 
//*> \endverbatim 
//*> 
//*> \param[in,out] CX 
//*> \verbatim 
//*>          CX is COMPLEX array, dimension ( 1 + ( N - 1 )*abs( INCX ) ) 
//*> \endverbatim 
//*> 
//*> \param[in] INCX 
//*> \verbatim 
//*>          INCX is INTEGER 
//*>         storage spacing between elements of CX 
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
//*> \ingroup complex_blas_level1 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>     jack dongarra, linpack,  3/11/78. 
//*>     modified 3/93 to return if incx .le. 0. 
//*>     modified 12/3/93, array(1) declarations changed to array(*) 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _00l5hgpk(ref Int32 _dxpq0xkr, ref fcomplex _han26hfi, fcomplex* _c9wzt3lw, ref Int32 _1eqjusqc)
	{
#region variable declarations
Int32 _b5p6od9s =  default;
Int32 _dbc5n539 =  default;
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
		//*     .. Array Arguments .. 
		//*     .. 
		//* 
		//*  ===================================================================== 
		//* 
		//*     .. Local Scalars .. 
		//*     .. 
		
		if ((_dxpq0xkr <= (int)0) | (_1eqjusqc <= (int)0))
		return;
		if (_1eqjusqc == (int)1)
		{
			//* 
			//*        code for increment equal to 1 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn40 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step40 = (System.Int32)((int)1);
				System.Int32 __81fgg2count40;
				for (__81fgg2count40 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn40 + __81fgg2step40) / __81fgg2step40)), _b5p6od9s = __81fgg2dlsvn40; __81fgg2count40 != 0; __81fgg2count40--, _b5p6od9s += (__81fgg2step40)) {

				{
					
					*(_c9wzt3lw+(_b5p6od9s - 1)) = (_han26hfi * *(_c9wzt3lw+(_b5p6od9s - 1)));
				}
								}			}
		}
		else
		{
			//* 
			//*        code for increment not equal to 1 
			//* 
			
			_dbc5n539 = (_dxpq0xkr * _1eqjusqc);
			{
				System.Int32 __81fgg2dlsvn41 = (System.Int32)((int)1);
				System.Int32 __81fgg2step41 = (System.Int32)(_1eqjusqc);
				System.Int32 __81fgg2count41;
				for (__81fgg2count41 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dbc5n539) - __81fgg2dlsvn41 + __81fgg2step41) / __81fgg2step41)), _b5p6od9s = __81fgg2dlsvn41; __81fgg2count41 != 0; __81fgg2count41--, _b5p6od9s += (__81fgg2step41)) {

				{
					
					*(_c9wzt3lw+(_b5p6od9s - 1)) = (_han26hfi * *(_c9wzt3lw+(_b5p6od9s - 1)));
				}
								}			}
		}
		
		return;
	}
	
	} // 177

} // end class 
} // end namespace
#endif
