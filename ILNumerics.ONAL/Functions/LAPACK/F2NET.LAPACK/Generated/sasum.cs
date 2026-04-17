
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
//*> \brief \b SASUM 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       REAL FUNCTION SASUM(N,SX,INCX) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER INCX,N 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL SX(*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*>    SASUM takes the sum of the absolute values. 
//*>    uses unrolled loops for increment equal to one. 
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
//*> \param[in] SX 
//*> \verbatim 
//*>          SX is REAL array, dimension ( 1 + ( N - 1 )*abs( INCX ) ) 
//*> \endverbatim 
//*> 
//*> \param[in] INCX 
//*> \verbatim 
//*>          INCX is INTEGER 
//*>         storage spacing between elements of SX 
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
//*> \ingroup single_blas_level1 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>     jack dongarra, linpack, 3/11/78. 
//*>     modified 3/93 to return if incx .le. 0. 
//*>     modified 12/3/93, array(1) declarations changed to array(*) 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static Single _04plmv36(ref Int32 _dxpq0xkr, Single* _s66poh0u, ref Int32 _1eqjusqc)
	{
#region variable declarations
Single _04plmv36 = default;
Single _chiot9on =  default;
Int32 _b5p6od9s =  default;
Int32 _ev4xhht5 =  default;
Int32 _97on0doo =  default;
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
		//*     .. Intrinsic Functions .. 
		//*     .. 
		
		_04plmv36 = 0f;
		_chiot9on = 0f;
		if ((_dxpq0xkr <= (int)0) | (_1eqjusqc <= (int)0))
		return _04plmv36;
		if (_1eqjusqc == (int)1)
		{
			//*        code for increment equal to 1 
			//* 
			//* 
			//*        clean-up loop 
			//* 
			
			_ev4xhht5 = ILNumerics.F2NET.Intrinsics.MOD(_dxpq0xkr ,(int)6 );
			if (_ev4xhht5 != (int)0)
			{
				
				{
					System.Int32 __81fgg2dlsvn2493 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step2493 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2493;
					for (__81fgg2count2493 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn2493 + __81fgg2step2493) / __81fgg2step2493)), _b5p6od9s = __81fgg2dlsvn2493; __81fgg2count2493 != 0; __81fgg2count2493--, _b5p6od9s += (__81fgg2step2493)) {

					{
						
						_chiot9on = (_chiot9on + ILNumerics.F2NET.Intrinsics.ABS(*(_s66poh0u+(_b5p6od9s - 1)) ));
					}
										}				}
				if (_dxpq0xkr < (int)6)
				{
					
					_04plmv36 = _chiot9on;
					return _04plmv36;
				}
				
			}
			
			_97on0doo = (_ev4xhht5 + (int)1);
			{
				System.Int32 __81fgg2dlsvn2494 = (System.Int32)(_97on0doo);
				System.Int32 __81fgg2step2494 = (System.Int32)((int)6);
				System.Int32 __81fgg2count2494;
				for (__81fgg2count2494 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2494 + __81fgg2step2494) / __81fgg2step2494)), _b5p6od9s = __81fgg2dlsvn2494; __81fgg2count2494 != 0; __81fgg2count2494--, _b5p6od9s += (__81fgg2step2494)) {

				{
					
					_chiot9on = ((((((_chiot9on + ILNumerics.F2NET.Intrinsics.ABS(*(_s66poh0u+(_b5p6od9s - 1)) )) + ILNumerics.F2NET.Intrinsics.ABS(*(_s66poh0u+(_b5p6od9s + (int)1 - 1)) )) + ILNumerics.F2NET.Intrinsics.ABS(*(_s66poh0u+(_b5p6od9s + (int)2 - 1)) )) + ILNumerics.F2NET.Intrinsics.ABS(*(_s66poh0u+(_b5p6od9s + (int)3 - 1)) )) + ILNumerics.F2NET.Intrinsics.ABS(*(_s66poh0u+(_b5p6od9s + (int)4 - 1)) )) + ILNumerics.F2NET.Intrinsics.ABS(*(_s66poh0u+(_b5p6od9s + (int)5 - 1)) ));
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
				System.Int32 __81fgg2dlsvn2495 = (System.Int32)((int)1);
				System.Int32 __81fgg2step2495 = (System.Int32)(_1eqjusqc);
				System.Int32 __81fgg2count2495;
				for (__81fgg2count2495 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dbc5n539) - __81fgg2dlsvn2495 + __81fgg2step2495) / __81fgg2step2495)), _b5p6od9s = __81fgg2dlsvn2495; __81fgg2count2495 != 0; __81fgg2count2495--, _b5p6od9s += (__81fgg2step2495)) {

				{
					
					_chiot9on = (_chiot9on + ILNumerics.F2NET.Intrinsics.ABS(*(_s66poh0u+(_b5p6od9s - 1)) ));
				}
								}			}
		}
		
		_04plmv36 = _chiot9on;
		return _04plmv36;
	}
	
	return _04plmv36;
	} // 177

} // end class 
} // end namespace
#endif
