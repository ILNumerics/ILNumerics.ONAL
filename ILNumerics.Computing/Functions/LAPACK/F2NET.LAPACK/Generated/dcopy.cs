
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
//*> \brief \b DCOPY 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DCOPY(N,DX,INCX,DY,INCY) 
//* 
//*       .. Scalar Arguments .. 
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
//*>    DCOPY copies a vector, x, to a vector, y. 
//*>    uses unrolled loops for increments equal to 1. 
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
//*> \param[in] DX 
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
//*> \param[out] DY 
//*> \verbatim 
//*>          DY is DOUBLE PRECISION array, dimension ( 1 + ( N - 1 )*abs( INCY ) ) 
//*> \endverbatim 
//*> 
//*> \param[in] INCY 
//*> \verbatim 
//*>          INCY is INTEGER 
//*>         storage spacing between elements of DY 
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

	 
	public static void _gvjhlct0(ref Int32 _dxpq0xkr, Double* _dr47k4na, ref Int32 _1eqjusqc, Double* _u3nd6g4l, ref Int32 _bbrxgmj7)
	{
#region variable declarations
Int32 _b5p6od9s =  default;
Int32 _b69ritwm =  default;
Int32 _821h5yui =  default;
Int32 _ev4xhht5 =  default;
Int32 _97on0doo =  default;
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
		//*     .. Intrinsic Functions .. 
		//*     .. 
		
		if (_dxpq0xkr <= (int)0)
		return;
		if ((_1eqjusqc == (int)1) & (_bbrxgmj7 == (int)1))
		{
			//* 
			//*        code for both increments equal to 1 
			//* 
			//* 
			//*        clean-up loop 
			//* 
			
			_ev4xhht5 = ILNumerics.F2NET.Intrinsics.MOD(_dxpq0xkr ,(int)7 );
			if (_ev4xhht5 != (int)0)
			{
				
				{
					System.Int32 __81fgg2dlsvn179 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step179 = (System.Int32)((int)1);
					System.Int32 __81fgg2count179;
					for (__81fgg2count179 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn179 + __81fgg2step179) / __81fgg2step179)), _b5p6od9s = __81fgg2dlsvn179; __81fgg2count179 != 0; __81fgg2count179--, _b5p6od9s += (__81fgg2step179)) {

					{
						
						*(_u3nd6g4l+(_b5p6od9s - 1)) = *(_dr47k4na+(_b5p6od9s - 1));
					}
										}				}
				if (_dxpq0xkr < (int)7)
				return;
			}
			
			_97on0doo = (_ev4xhht5 + (int)1);
			{
				System.Int32 __81fgg2dlsvn180 = (System.Int32)(_97on0doo);
				System.Int32 __81fgg2step180 = (System.Int32)((int)7);
				System.Int32 __81fgg2count180;
				for (__81fgg2count180 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn180 + __81fgg2step180) / __81fgg2step180)), _b5p6od9s = __81fgg2dlsvn180; __81fgg2count180 != 0; __81fgg2count180--, _b5p6od9s += (__81fgg2step180)) {

				{
					
					*(_u3nd6g4l+(_b5p6od9s - 1)) = *(_dr47k4na+(_b5p6od9s - 1));
					*(_u3nd6g4l+(_b5p6od9s + (int)1 - 1)) = *(_dr47k4na+(_b5p6od9s + (int)1 - 1));
					*(_u3nd6g4l+(_b5p6od9s + (int)2 - 1)) = *(_dr47k4na+(_b5p6od9s + (int)2 - 1));
					*(_u3nd6g4l+(_b5p6od9s + (int)3 - 1)) = *(_dr47k4na+(_b5p6od9s + (int)3 - 1));
					*(_u3nd6g4l+(_b5p6od9s + (int)4 - 1)) = *(_dr47k4na+(_b5p6od9s + (int)4 - 1));
					*(_u3nd6g4l+(_b5p6od9s + (int)5 - 1)) = *(_dr47k4na+(_b5p6od9s + (int)5 - 1));
					*(_u3nd6g4l+(_b5p6od9s + (int)6 - 1)) = *(_dr47k4na+(_b5p6od9s + (int)6 - 1));
				}
								}			}
		}
		else
		{
			//* 
			//*        code for unequal increments or equal increments 
			//*          not equal to 1 
			//* 
			
			_b69ritwm = (int)1;
			_821h5yui = (int)1;
			if (_1eqjusqc < (int)0)
			_b69ritwm = ((((-(_dxpq0xkr)) + (int)1) * _1eqjusqc) + (int)1);
			if (_bbrxgmj7 < (int)0)
			_821h5yui = ((((-(_dxpq0xkr)) + (int)1) * _bbrxgmj7) + (int)1);
			{
				System.Int32 __81fgg2dlsvn181 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step181 = (System.Int32)((int)1);
				System.Int32 __81fgg2count181;
				for (__81fgg2count181 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn181 + __81fgg2step181) / __81fgg2step181)), _b5p6od9s = __81fgg2dlsvn181; __81fgg2count181 != 0; __81fgg2count181--, _b5p6od9s += (__81fgg2step181)) {

				{
					
					*(_u3nd6g4l+(_821h5yui - 1)) = *(_dr47k4na+(_b69ritwm - 1));
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
