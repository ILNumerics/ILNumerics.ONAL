
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
//*> \brief \b DROT 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DROT(N,DX,INCX,DY,INCY,C,S) 
//* 
//*       .. Scalar Arguments .. 
//*       DOUBLE PRECISION C,S 
//*       INTEGER INCX,INCY,N 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION DX(*),DY(*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*>    DROT applies a plane rotation. 
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
//*> \param[in,out] DX 
//*> \verbatim 
//*>          DX is DOUBLE PRECISION array, dimension ( 1 + ( N - 1 )*abs( INCX ) ) 
//*> \endverbatim 
//*> 
//*> \param[in] INCX 
//*> \verbatim 
//*>          INCX is INTEGER 
//*>         storage spacing between elements of DX 
//*> \endverbatim 
//*> 
//*> \param[in,out] DY 
//*> \verbatim 
//*>          DY is DOUBLE PRECISION array, dimension ( 1 + ( N - 1 )*abs( INCY ) ) 
//*> \endverbatim 
//*> 
//*> \param[in] INCY 
//*> \verbatim 
//*>          INCY is INTEGER 
//*>         storage spacing between elements of DY 
//*> \endverbatim 
//*> 
//*> \param[in] C 
//*> \verbatim 
//*>          C is DOUBLE PRECISION 
//*> \endverbatim 
//*> 
//*> \param[in] S 
//*> \verbatim 
//*>          S is DOUBLE PRECISION 
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
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>     jack dongarra, linpack, 3/11/78. 
//*>     modified 12/3/93, array(1) declarations changed to array(*) 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _2197fa5i(ref Int32 _dxpq0xkr, Double* _dr47k4na, ref Int32 _1eqjusqc, Double* _u3nd6g4l, ref Int32 _bbrxgmj7, ref Double _3crf0qn3, ref Double _irk8i6qr)
	{
#region variable declarations
Double _7gr3yztw =  default;
Int32 _b5p6od9s =  default;
Int32 _b69ritwm =  default;
Int32 _821h5yui =  default;
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
		
		if (_dxpq0xkr <= (int)0)
		return;
		if ((_1eqjusqc == (int)1) & (_bbrxgmj7 == (int)1))
		{
			//* 
			//*       code for both increments equal to 1 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn234 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step234 = (System.Int32)((int)1);
				System.Int32 __81fgg2count234;
				for (__81fgg2count234 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn234 + __81fgg2step234) / __81fgg2step234)), _b5p6od9s = __81fgg2dlsvn234; __81fgg2count234 != 0; __81fgg2count234--, _b5p6od9s += (__81fgg2step234)) {

				{
					
					_7gr3yztw = ((_3crf0qn3 * *(_dr47k4na+(_b5p6od9s - 1))) + (_irk8i6qr * *(_u3nd6g4l+(_b5p6od9s - 1))));
					*(_u3nd6g4l+(_b5p6od9s - 1)) = ((_3crf0qn3 * *(_u3nd6g4l+(_b5p6od9s - 1))) - (_irk8i6qr * *(_dr47k4na+(_b5p6od9s - 1))));
					*(_dr47k4na+(_b5p6od9s - 1)) = _7gr3yztw;
				}
								}			}
		}
		else
		{
			//* 
			//*       code for unequal increments or equal increments not equal 
			//*         to 1 
			//* 
			
			_b69ritwm = (int)1;
			_821h5yui = (int)1;
			if (_1eqjusqc < (int)0)
			_b69ritwm = ((((-(_dxpq0xkr)) + (int)1) * _1eqjusqc) + (int)1);
			if (_bbrxgmj7 < (int)0)
			_821h5yui = ((((-(_dxpq0xkr)) + (int)1) * _bbrxgmj7) + (int)1);
			{
				System.Int32 __81fgg2dlsvn235 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step235 = (System.Int32)((int)1);
				System.Int32 __81fgg2count235;
				for (__81fgg2count235 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn235 + __81fgg2step235) / __81fgg2step235)), _b5p6od9s = __81fgg2dlsvn235; __81fgg2count235 != 0; __81fgg2count235--, _b5p6od9s += (__81fgg2step235)) {

				{
					
					_7gr3yztw = ((_3crf0qn3 * *(_dr47k4na+(_b69ritwm - 1))) + (_irk8i6qr * *(_u3nd6g4l+(_821h5yui - 1))));
					*(_u3nd6g4l+(_821h5yui - 1)) = ((_3crf0qn3 * *(_u3nd6g4l+(_821h5yui - 1))) - (_irk8i6qr * *(_dr47k4na+(_b69ritwm - 1))));
					*(_dr47k4na+(_b69ritwm - 1)) = _7gr3yztw;
					_b69ritwm = (_b69ritwm + _1eqjusqc);
					_821h5yui = (_821h5yui + _bbrxgmj7);
				}
								}			}
		}
		
		return;
	}
	
	} // 177

} // end class 
} // end namespace
#endif
